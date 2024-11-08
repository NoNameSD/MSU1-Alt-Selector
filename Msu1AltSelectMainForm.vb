Option Compare Binary
Option Explicit On
Option Strict On
Imports MsuAltSelect.Msu
Imports MsuAltSelect.Msu.Tracks
Imports MsuAltSelect.Msu.Ex

Public Class Msu1AltSelectMainForm
    Private WithEvents _MsuTracks As MsuTracks
    Private _JsonFilePath As String
    Private _DisplayOnlyTracksWithAlts As Boolean
    Public Property Settings As Msu.Settings.Settings

    Private Property JsonFilePath As String
        Get
            Return _JsonFilePath
        End Get
        Set(value As String)
            _JsonFilePath = value
        End Set
    End Property

    Public Property MsuTracks As MsuTracks
        Get
            Return _MsuTracks
        End Get
        Private Set(value As MsuTracks)
            _MsuTracks = value
            Call Me.MsuTracksConfigLoadStateUpdate()
        End Set
    End Property

    Public ReadOnly Property Logger As MsuAltSelect.Logger.Logger
        Get
            Return objLogger
        End Get
    End Property

    Public Property EnableAutoSwitch As Boolean
        Get
            Return Me.tmrAutoSwitch.Enabled
        End Get
        Set(value As Boolean)
            If value <> Me.tmrAutoSwitch.Enabled Then
                If value Then
                    If Me.MsuTracks Is Nothing Then
                        Call System.Windows.Forms.MessageBox.Show(
                            owner:=Me,
                            text:="Cannot enable AutoSwitch with no MsuTracks loaded.",
                            caption:=My.Application.Info.ProductName,
                            buttons:=MessageBoxButtons.OK,
                            icon:=MessageBoxIcon.Warning)
                        value = False
                    End If
                End If
                If value Then
                    Try
                        Call Me.MsuTracks.AutoSwitchValidate()
                    Catch ex As Exception
                        Call MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=ex)
                        value = False
                    End Try
                End If
            End If
            Me.tmrAutoSwitch.Enabled = value
            Me.ctrlEnableAutoSwitch.Checked = Me.tmrAutoSwitch.Enabled
        End Set
    End Property

    Public Property DisplayOnlyTracksWithAlts As Boolean
        Get
            Return _DisplayOnlyTracksWithAlts 'Me.ctrlDisplayOnlyTracksWithAlts.Checked
        End Get
        Set(value As Boolean)
            If _DisplayOnlyTracksWithAlts <> value Then
                _DisplayOnlyTracksWithAlts = value
            End If
            If _DisplayOnlyTracksWithAlts <> Me.ctrlDisplayOnlyTracksWithAlts.Checked Then
                Me.ctrlDisplayOnlyTracksWithAlts.Checked = _DisplayOnlyTracksWithAlts
            End If
        End Set
    End Property

    Private Sub ctrlDisplayOnlyTracksWithAlts_CheckedChanged(sender As Object, e As EventArgs) Handles ctrlDisplayOnlyTracksWithAlts.CheckedChanged
        If Me.ctrlDisplayOnlyTracksWithAlts.Checked <> Me.DisplayOnlyTracksWithAlts Then
            Me.DisplayOnlyTracksWithAlts = Me.ctrlDisplayOnlyTracksWithAlts.Checked
            If Me.MsuTracks IsNot Nothing Then
                Call Me.FillTrackList()
            End If
        End If
    End Sub

    Private Sub DisplayOnlyTracksWithAltsAutoCheck()
        Dim trackIdSelected As Nullable(Of Byte) = Me.TrackIdSelected
        Dim trackDict As SortedDictionary(Of Byte, MsuTrack) = Me.MsuTracks.TrackDict
        Dim tracksWithAltsExist As Boolean

        For Each keyValuePair As KeyValuePair(Of Byte, MsuTrack) In trackDict

            If keyValuePair.Value.TrackAltArray.Length <= MsuHelper.OneByte Then
                Continue For
            End If

            tracksWithAltsExist = True
            Exit For
        Next

        Me.DisplayOnlyTracksWithAlts = tracksWithAltsExist
    End Sub

    Private Sub SelectMsuFileClicked(sender As System.Object, e As System.EventArgs) Handles btnSelPathMsu.Click
        Call SelectMsuFile()
    End Sub
    Private Sub SelectMsuFile()
        Call Me.ofdPathMsu.ShowDialog(owner:=Me)
    End Sub

    Private Sub ofdPathMsu_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ofdPathMsu.FileOk

        If TypeOf sender IsNot OpenFileDialog Then
            e.Cancel = True
            Return
        End If
        Dim openFileDialog As OpenFileDialog = DirectCast(sender, OpenFileDialog)

        Select Case openFileDialog.FileNames.Length
            Case 0
                e.Cancel = True
                Return
            Case 1
            Case Else
                e.Cancel = True
                Return
        End Select

        Dim ret As String = openFileDialog.FileNames(0)

        If String.IsNullOrWhiteSpace(ret) Then
            e.Cancel = True
            Return
        End If

        Call LoadFromSelectedFile(ret, e)
    End Sub

    Private Sub LoadFromSelectedFile(ByRef filePath As String)
        Call LoadFromSelectedFile(filePath, Nothing)
    End Sub
    Private Sub LoadFromSelectedFile(ByRef filePath As String, e As System.ComponentModel.CancelEventArgs)
        Dim directory As String = System.IO.Path.GetDirectoryName(filePath)
        Dim baseName As String = System.IO.Path.GetFileNameWithoutExtension(filePath)
        Dim fileExtL As String = System.IO.Path.GetExtension(filePath).ToLower

        Select Case fileExtL
            Case MsuHelper.JsonExtL
                ' Load from the provided JSON file
                Call LoadFromJson(filePath, e)
            Case Else
                ' Check if a JSON file with the same base name exists and load from it if yes
                Dim jsonFilePath As String = System.IO.Path.Combine(directory, String.Concat(baseName, MsuHelper.JsonExtL))
                If System.IO.File.Exists(jsonFilePath) Then
                    Call LoadFromJson(jsonFilePath, e)
                Else
                    If fileExtL.Equals(MsuHelper.PcmExtL, StringComparison.Ordinal) Then
                        ' Selected file is PCM file

                        Dim pcmPrefix = MsuTracks.GetPcmPrefix(filePath)
                        If pcmPrefix IsNot Nothing Then

                            ' Check if a JSON file with PcmPrefix exists
                            jsonFilePath = System.IO.Path.Combine(directory, String.Concat(pcmPrefix, MsuHelper.JsonExtL))
                            If System.IO.File.Exists(jsonFilePath) Then
                                Call LoadFromJson(jsonFilePath, e)
                                Exit Select
                            End If
                        End If
                    End If
                    Call ScanNewMsuFolder(filePath, e)
                    Me.JsonFilePath = jsonFilePath
                End If
        End Select
    End Sub

    Private Sub ScanNewMsuFolder(ByRef msuFilePath As String, e As System.ComponentModel.CancelEventArgs)
        Call DisposeMsuTracksObj()

        Dim msuTracks = New MsuTracks(msuFilePath, Me.Logger, Me.Settings)

        Call Me.Logger.AddToLog($"Generating new configuration using the file ""{msuFilePath}"".")

        ' Read all .pcm Tracks with alts in current folder (incl. sub folders)
        Call msuTracks.ScanMsuDirectoryForTracks()

        Call msuTracks.SetCurrentDirectoryToMsuLocation()

        Me.MsuTracks = msuTracks
        Me.txtPathMsu.Text = Me.MsuTracks.MsuFilePath
        If Me.Settings.TrackAltSettings.AutoSetDisplayOnlyTracksWithAlts Then Call Me.DisplayOnlyTracksWithAltsAutoCheck()
        Call Me.FillTrackList()
    End Sub

    Private Sub LoadFromJson(ByRef jsonFilePath As String, e As System.ComponentModel.CancelEventArgs)
        Call DisposeMsuTracksObj()

        Try
            Dim msuTracks = Msu.Tracks.MsuTracks.LoadFromJson(jsonFilePath, Me.Logger, Me.Settings)
            Me.JsonFilePath = jsonFilePath
            Call LoadFromJson(msuTracks, e)
        Catch ex As Exception
            Call Me.Logger.AddToLog(ex.ToString, entryColor:=Drawing.Color.Red)
            Call MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=ex)
            Call DisposeMsuTracksObj()
            If e IsNot Nothing Then e.Cancel = True
            Return
        End Try
    End Sub

    Private Sub LoadFromJson(ByRef parsedJsonData As MsuTracks)
        Call LoadFromJson(parsedJsonData, Nothing)
    End Sub

    Private Sub LoadFromJson(ByRef parsedJsonData As MsuTracks, e As System.ComponentModel.CancelEventArgs)
        Call DisposeMsuTracksObj()

        Try
            Me.MsuTracks = parsedJsonData
            Me.txtPathMsu.Text = Me.MsuTracks.MsuFilePath
            If Me.Settings.TrackAltSettings.AutoSetDisplayOnlyTracksWithAlts Then Call Me.DisplayOnlyTracksWithAltsAutoCheck()
            Call FillTrackList()
        Catch ex As Exception
            Call Me.Logger.AddToLog(ex.ToString, entryColor:=Drawing.Color.Red)
            Call MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=ex)
            If e IsNot Nothing Then e.Cancel = True
            Return
        End Try

        Call Me.Logger.AddToLog("JSON configuration loaded successfully. Checking if the paths are valid.")

        Try
            Call Me.MsuTracks.ValidateMsuTrackPaths()
        Catch ex As Exception
            Call Me.Logger.AddToLog(ex.ToString, entryColor:=Drawing.Color.Red)
            Call MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=ex)
        End Try

        If Me.Settings.TrackAltSettings.AutoSetAutoSwitch Then
            Me.EnableAutoSwitch = Me.MsuTracks.HasAltTracksWithAutoSwitch
        End If
        Call Me.CheckForNormalTrackVersion()
    End Sub

    ''' <summary>
    ''' Dispose of previous <see cref="MsuTracks" /> data
    ''' </summary>
    Private Sub DisposeMsuTracksObj()
        If _MsuTracks IsNot Nothing Then
            Call _MsuTracks.Dispose()
            Me.MsuTracks = Nothing
            Call Me.lstvTracks.Items.Clear()
            Call Me.lstvAltTracks.Items.Clear()
        End If
    End Sub

    Public Property TrackSelected As Tracks.MsuTrack
        Get
            If Me.MsuTracks Is Nothing Then Return Nothing
            Dim trackNumberN As Nullable(Of Byte) = Me.TrackIdSelected

            ' Cancel if no track number is selected
            If trackNumberN.HasValue Then
                Dim trackNumber As Byte = CByte(trackNumberN)

                Dim value As MsuTrack = Nothing
                If Me.MsuTracks.TrackDict.TryGetValue(trackNumber, value) Then
                    Return value
                End If
            End If

            Return Nothing
        End Get
        Set(value As Tracks.MsuTrack)
            If value Is Nothing Then
                Me.TrackIdSelected = Nothing
            Else
                Me.TrackIdSelected = value.TrackNumber
            End If
        End Set
    End Property

    ''' <returns>
    ''' Current TrackId (which has the alt. Tracks shown) selected by the user
    ''' </returns>
    Public Property TrackIdSelected As Nullable(Of Byte)
        Get
            Dim selectedItems As ListView.SelectedListViewItemCollection = lstvTracks.SelectedItems

            If selectedItems.Count = MsuHelper.ZeroByte Then
                Return Nothing
            End If

            Dim selectedItem As ListViewItem = selectedItems.Item(0)
            'Dim objSelSubItms As ListViewItem.ListViewSubItemCollection = objSelItm.SubItems()
            'Dim objSelSubItm As ListViewItem.ListViewSubItem = objSelSubItms.Item(0)
            'Dim strTrackNumber As String = objSelSubItm.Text()

            Dim TrackNumberStr As String = selectedItem.Group.Name

            Return CByte(TrackNumberStr)
        End Get
        Set(trackToSelect As Nullable(Of Byte))
            Dim selectedItems As ListView.SelectedListViewItemCollection = lstvTracks.SelectedItems

            ' If no Track is selected
            If selectedItems.Count = MsuHelper.ZeroByte Then
                ' No Item to select
                If trackToSelect.HasValue Then
                Else
                    Return ' No change
                End If
            End If

            ' Given value is not null
            If trackToSelect.HasValue Then

                ' Look for entry with the given alt. id
                For Each listViewItem As ListViewItem In lstvTracks.Items

                    Dim trackNumberStr As System.String = listViewItem.Group.Name
                    Dim trackNumber As System.Byte = CByte(trackNumberStr)

                    If trackNumber = trackToSelect Then

                        ' Track found
                        ' set selection
                        If listViewItem.Selected Then
                        Else
                            listViewItem.Selected = True
                        End If
                        Return
                    End If
                Next
            End If

            ' Track not found or null value given
            ' Unselect all
            Call RemoveTrackSelection()
        End Set
    End Property

    ''' <returns>
    ''' Current selected alt. Track for the currently selected TrackId
    ''' </returns>
    Public Property TrackAltSelected As MsuTrackAlt
        Get
            Dim trackIdSelected = Me.TrackSelected
            Dim trackAltIdSelectedN As Nullable(Of UInt16) = Me.TrackAltIdSelected

            If trackIdSelected IsNot Nothing _
       AndAlso trackAltIdSelectedN.HasValue Then

                Dim value As MsuTrackAlt = Nothing
                If trackIdSelected.TrackAltDict.TryGetValue(CUShort(trackAltIdSelectedN), value) Then
                    Return value
                End If
            End If
            Return Nothing
        End Get
        Set(value As MsuTrackAlt)
            If value Is Nothing Then
                Me.TrackAltIdSelected = Nothing
            Else
                Me.TrackAltIdSelected = value.AltNumber
            End If
        End Set
    End Property

    ''' <returns>
    ''' Current selected alt. Track for the currently selected TrackId
    ''' </returns>
    Public Property TrackAltIdSelected As Nullable(Of System.UInt16)
        Get
            Dim trackIdSelected As Nullable(Of Byte) = Me.TrackIdSelected

            If trackIdSelected.HasValue Then

                Dim selectedItems As ListView.SelectedListViewItemCollection = lstvAltTracks.SelectedItems

                If selectedItems.Count = MsuHelper.ZeroByte Then
                    Return Nothing
                End If

                Dim selectedItem As ListViewItem = selectedItems.Item(0)
                'Dim objSelSubItms As ListViewItem.ListViewSubItemCollection = objSelItm.SubItems()
                'Dim objSelSubItm As ListViewItem.ListViewSubItem = objSelSubItms.Item(0)
                'Dim strTrackAltNumber As String = objSelSubItm.Text()

                Dim trackAltNumberStr As String = selectedItem.Group.Name
                Return CUShort(trackAltNumberStr)
            End If

            Return Nothing
        End Get
        Set(trackAltIdToSelect As Nullable(Of System.UInt16))
            Dim selectedItems As ListView.SelectedListViewItemCollection = lstvAltTracks.SelectedItems

            ' If no alt. Track is selected
            If selectedItems.Count = MsuHelper.ZeroByte Then
                ' No Item to select
                If trackAltIdToSelect.HasValue Then
                Else
                    Return ' No change
                End If
            End If

            ' Given value is not null
            If trackAltIdToSelect.HasValue Then

                Dim trackAltNumberStr As String
                Dim trackAltNumber As System.UInt16

                ' Look for entry with the given alt. id
                For Each listViewItem As ListViewItem In lstvAltTracks.Items

                    'Dim objSubItms As ListViewItem.ListViewSubItemCollection = objLstItm.SubItems()
                    'Dim objSubItm As ListViewItem.ListViewSubItem = objSubItms.Item(0)
                    'strTrackAltNumber = objSubItm.Text()
                    trackAltNumberStr = listViewItem.Group.Name
                    trackAltNumber = CUShort(trackAltNumberStr)

                    If trackAltNumber = trackAltIdToSelect Then

                        ' alt. Track found
                        ' set selection
                        If listViewItem.Selected Then
                        Else
                            listViewItem.Selected = True
                        End If
                        Return
                    End If
                Next
            Else
                ' alt. Track not found or null value given
                ' Unselect all
                Call Me.RemoveAltTrackSelection()
            End If

        End Set
    End Property

    ''' <returns>
    ''' alt. Track, that is marked as the current alt. Track
    ''' </returns>
    Public Property TrackAltMarkedAsCurrent As MsuTrackAlt
        Get
            Dim trackIdSelected = Me.TrackSelected
            Dim trackAltIdMarkedAsCurrentN As Nullable(Of UInt16) = Me.TrackAltIdMarkedAsCurrent

            If trackIdSelected IsNot Nothing _
       AndAlso trackAltIdMarkedAsCurrentN.HasValue Then

                Dim value As MsuTrackAlt = Nothing
                If trackIdSelected.TrackAltDict.TryGetValue(CUShort(trackAltIdMarkedAsCurrentN), value) Then
                    Return value
                End If
            End If
            Return Nothing
        End Get
        Set(value As MsuTrackAlt)
            If value Is Nothing Then
                Me.TrackAltIdMarkedAsCurrent = Nothing
            Else
                Me.TrackAltIdMarkedAsCurrent = value.AltNumber
            End If
        End Set
    End Property

    ''' <returns>
    ''' alt. Track, that is marked as the current alt. Track
    ''' </returns>
    Public Property TrackAltIdMarkedAsCurrent As Nullable(Of System.UInt16)
        Get
            Dim checkedItems As ListView.CheckedListViewItemCollection = lstvAltTracks.CheckedItems

            If checkedItems.Count = MsuHelper.ZeroByte Then
                Return Nothing
            End If

            Dim checkedItem As ListViewItem = checkedItems.Item(0)

            Dim trackAltNumber As String = checkedItem.Group.Name
            Return CUShort(trackAltNumber)
        End Get
        Set(trackAltIdToMark As Nullable(Of System.UInt16))
            Dim checkedItems As ListView.CheckedListViewItemCollection = lstvAltTracks.CheckedItems

            ' If no alt. Track is selected
            If checkedItems.Count = MsuHelper.ZeroByte Then
                ' No Item to select
                If trackAltIdToMark.HasValue Then
                Else
                    Return ' No change
                End If
            End If

            ' Given value is not null
            If trackAltIdToMark.HasValue Then

                Dim newValue As System.UInt16 = CUShort(trackAltIdToMark)

                Dim trackAltNumberStr As String
                Dim trackAltNumber As System.UInt16

                ' Look for entry with the given alt. id
                For Each listViewItem As ListViewItem In lstvAltTracks.Items

                    trackAltNumberStr = listViewItem.Group.Name
                    trackAltNumber = CUShort(trackAltNumberStr)

                    listViewItem.Checked = trackAltNumber = newValue
                Next
            Else
                ' null value given
                ' uncheck all
                Call Me.RemoveAltTrackMarkedAsCurrent()
            End If

        End Set
    End Property

    Private Sub RemoveTrackSelection()
        For Each selectedItems As ListViewItem In lstvTracks.SelectedItems
            selectedItems.Selected = False
        Next
    End Sub

    Private Sub RemoveAltTrackSelection()
        For Each selectedItems As ListViewItem In lstvAltTracks.SelectedItems
            selectedItems.Selected = False
        Next
    End Sub

    Private Sub RemoveAltTrackMarkedAsCurrent()
        For Each checkedItems As ListViewItem In lstvAltTracks.CheckedItems
            checkedItems.Checked = False
        Next
    End Sub

    Private Sub FillTrackList()

        Dim trackIdSelectedSave As Nullable(Of Byte) = Me.TrackIdSelected
        Call Me.lstvTracks.Items.Clear()
        Dim trackDict As SortedDictionary(Of Byte, MsuTrack) = Me.MsuTracks.TrackDict

        For Each objDictItem As KeyValuePair(Of Byte, MsuTrack) In trackDict

            Dim msuTrack As MsuTrack = objDictItem.Value

            If Me.DisplayOnlyTracksWithAlts Then
                If msuTrack.TrackAltArray.Length <= MsuHelper.OneByte Then
                    Continue For
                End If
            End If
            Dim title As String

            If String.IsNullOrEmpty(msuTrack.Title) Then
                title = Me.MsuTracks.PcmPrefix & MsuHelper.HyphenChar & msuTrack.TrackNumber
            Else
                title = msuTrack.Title
            End If

            Dim lstVGrp As New ListViewGroup(CStr(msuTrack.TrackNumber), msuTrack.Title)

            Dim lstVit As New ListViewItem(lstVGrp)

            Dim listVitCol As ListViewItem.ListViewSubItemCollection = lstVit.SubItems
            Dim listVitId As ListViewItem.ListViewSubItem = lstVit.SubItems().Item(MsuHelper.ZeroByte)
            Dim listVitTitle As New ListViewItem.ListViewSubItem
            Call listVitCol.Add(listVitTitle)

            listVitId.Name = "TrackId"
            listVitId.Text = msuTrack.TrackNumber.ToString
            listVitTitle.Name = "TrackTitle"
            listVitTitle.Text = title

            Call lstvTracks.Items.Add(lstVit)
        Next

        Call FillAltTrackList()

        Me.TrackIdSelected = trackIdSelectedSave
    End Sub

    Private _FillingAltTrackList As Boolean

    Private Sub FillAltTrackList()
        Try
            _FillingAltTrackList = True
            Call _FillAltTrackList()
        Finally
            _FillingAltTrackList = False
        End Try
    End Sub

    Private Sub _FillAltTrackList()
        Call lstvAltTracks.Items.Clear()

        If lstvTracks.SelectedItems.Count = MsuHelper.ZeroByte Then
            Exit Sub
        End If

        Dim trackNumber As Byte = CByte(Me.TrackIdSelected)

        Dim msuTrack As MsuTrack = Me.MsuTracks.TrackDict(trackNumber)
        Dim trackAltDict As SortedDictionary(Of UShort, MsuTrackAlt) = msuTrack.TrackAltDict()

        ' Display AutoSwitch Column only if there is data to display
        If msuTrack.HasAltTracksWithAutoSwitch Then
            If lstvAltTracks.Columns.Contains(Me.chAltTrackAutoSwitch) Then
            Else
                Call lstvAltTracks.Columns.Add(Me.chAltTrackAutoSwitch)
            End If
        Else
            If lstvAltTracks.Columns.Contains(Me.chAltTrackAutoSwitch) Then
                Call lstvAltTracks.Columns.Remove(Me.chAltTrackAutoSwitch)
            End If
        End If

        For Each keyValuePair As KeyValuePair(Of UShort, MsuTrackAlt) In trackAltDict

            Dim msuTrackAlt As MsuTrackAlt = keyValuePair.Value

            Dim lstVGrp As New ListViewGroup(msuTrackAlt.AltNumber.ToString, msuTrackAlt.Title)

            Dim lstVit As New ListViewItem(lstVGrp)

            Dim listVitCol As ListViewItem.ListViewSubItemCollection = lstVit.SubItems

            ' Use first Item for Record as Id
            Dim listVitId As ListViewItem.ListViewSubItem = lstVit.SubItems().Item(0)

            ' Add Item to Record as Title
            Dim listVitTitle As New ListViewItem.ListViewSubItem
            Call listVitCol.Add(listVitTitle)

            ' Add Item to Record as Title
            Dim listVitAutoSwitch As New ListViewItem.ListViewSubItem
            Call listVitCol.Add(listVitAutoSwitch)

            listVitId.Name = "TrackId"
            listVitId.Text = msuTrackAlt.AltNumber.ToString
            listVitTitle.Name = "TrackTitle"
            listVitTitle.Text = msuTrackAlt.Title
            listVitAutoSwitch.Name = "AutoSwitch"
            listVitAutoSwitch.Text = msuTrackAlt.AutoSwitchTrackNumbersJson

            lstVit.ToolTipText = msuTrackAlt.FilePath

            Call lstvAltTracks.Items.Add(lstVit)
        Next

        Try
            Call Me.RefreshAltTrackSelection(False)
            Call msuTrack.ValidateMsuTrackPaths()
        Catch ex As Exception
            Call MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=ex)
        End Try
    End Sub

    Private Sub RefreshAltTrackSelection(ByRef bolMsgBoxForExceptions As Boolean)

        Try
            Dim msuTrack As MsuTrack = Me.TrackSelected
            If msuTrack Is Nothing Then
                Call RemoveAltTrackSelection()
                Return
            End If
            Dim msuTrackAlt As MsuTrackAlt = msuTrack.GetCurrentTrackAlt
            Me.TrackAltMarkedAsCurrent = msuTrackAlt
        Catch ex As Exception
            If bolMsgBoxForExceptions Then
                Call MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=ex)
            Else
                Throw
            End If
        End Try

    End Sub

#If False Then
    Private idleHandlerSet As Boolean = False

    ' See https://stackoverflow.com/a/1198358
#Disable Warning IDE1006 ' Naming Styles
    Private Sub lstvAltTracks_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lstvAltTracks.SelectedIndexChanged

        '' may fire more than once
        If Not idleHandlerSet Then
            idleHandlerSet = True
            AddHandler Application.Idle, New EventHandler(AddressOf lstvAltTracks_SelectionChanged)
        End If
    End Sub

    Private Sub lstvAltTracks_SelectionChanged(sender As System.Object, e As System.EventArgs)
        '' will only fire once
        idleHandlerSet = False
        RemoveHandler Application.Idle, New EventHandler(AddressOf lstvAltTracks_SelectionChanged)
        Call TrackAltSelectionChanged(sender, e)
    End Sub
#Enable Warning IDE1006 ' Naming Styles

    Private Sub TrackAltSelectionChanged(sender As System.Object, e As System.EventArgs) ' Handles lstvAltTracks.ItemSelectionChanged
        call SetSelectedAltTrackAsCurrent
    End Sub
#End If

    Private Sub SetAltTrackAsCurrent(ByRef trackAltSet As MsuTrackAlt)
        Call ArgumentNullException.ThrowIfNull(trackAltSet, NameOf(trackAltSet))
        Dim trackCurrent As MsuTrack = Me.TrackSelected
        Dim trackCurrentAlt As MsuTrackAlt
        Try
            trackCurrentAlt = trackCurrent.GetCurrentTrackAlt
        Catch ex As Exception
            Call MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=ex)
            Return
        End Try

        Try
            Call trackAltSet.SetAsCurrentAltTrack()
        Catch ex As Exception
            Call MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=ex)
            Me.TrackAltMarkedAsCurrent = trackCurrentAlt
        End Try
    End Sub

    Private Sub SetSelectedAltTrackAsCurrent()
        Dim trackAltSet As MsuTrackAlt = Me.TrackAltSelected
        If trackAltSet Is Nothing Then Return
        Call SetAltTrackAsCurrent(trackAltSet)
    End Sub

    Private Sub SetMarkedAltTrackAsCurrent()
        Dim trackAltSet As MsuTrackAlt = Me.TrackAltMarkedAsCurrent
        If trackAltSet Is Nothing Then Return
        Call SetAltTrackAsCurrent(trackAltSet)
    End Sub

    Private Sub FrmMSU1altSel_Resize()

        Me.btnSelPathMsu.Left = MyBase.Width - Me.btnSelPathMsu.Width - 20

        Me.txtPathMsu.Width = Me.btnSelPathMsu.Left - Me.txtPathMsu.Left - 6

    End Sub

    Private Sub TrackSelectionChanged(sender As Object, e As EventArgs) Handles lstvTracks.SelectedIndexChanged
#If False Then
        Dim objSelItms As ListView.SelectedListViewItemCollection = lstvTracks.SelectedItems

        If objSelItms.Count = msuhelper.zerobyte Then
            Exit Sub
        End If
#End If
        Call FillAltTrackList()
    End Sub

    Private Sub SaveJsonClicked(sender As Object, e As EventArgs) Handles btnSaveJson.Click
        Call Me.SaveJson()
    End Sub

    Private Sub SaveJson()
        If Me.MsuTracks Is Nothing Then Return
        Call Me.MsuTracks.SaveToJson(Me.JsonFilePath)
    End Sub

    Private Sub SaveJsonAsClicked(sender As Object, e As EventArgs) Handles btnSaveJsonAs.Click
        Call Me.SaveJsonAs()
    End Sub
    Private Sub SaveJsonAs()
        If Me.MsuTracks Is Nothing Then Return

        Me.sfdJson.FileName = Me.MsuTracks.MsuName & Msu.MsuHelper.JsonExtL

        Call Me.sfdJson.ShowDialog(owner:=Me)

        Dim ret As String = Me.sfdJson.FileName

        Call Me.sfdJson.Dispose()

        If String.IsNullOrEmpty(ret) Then
            Return
        End If

        Me.JsonFilePath = ret
        Call SaveJson()
    End Sub

    Private Sub btnSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click
        Dim frmMsuSettings As New MsuSettingsForm(Me.MsuTracks, Me.Settings)
        Dim maxLoggerEntriesSave = Me.Settings.LoggerSettings.MaxEntries

        frmMsuSettings.Icon = Me.Icon

        Call frmMsuSettings.ShowDialog(owner:=Me)

        Call frmMsuSettings.Dispose()

        If Me.MsuTracks IsNot Nothing Then Call Me.FillTrackList()

        ' Set MSU File Path in the display text box again
        ' in case the path was changed manually in the settings
        If Me.MsuTracks IsNot Nothing Then Me.txtPathMsu.Text = Me.MsuTracks.MsuFilePath
        Call Me.SetToolTips()

        If Me.Settings IsNot Nothing AndAlso maxLoggerEntriesSave <> Me.Settings.LoggerSettings.MaxEntries Then
            Me.Logger.MaxEntries = Me.Settings.LoggerSettings.MaxEntries
            Me.nudLogEntries.Value = Me.Logger.MaxEntries
        End If
    End Sub

    Private Sub nudAutoSwitchInterval_ValueChanged(sender As Object, e As EventArgs) Handles nudAutoSwitchInterval.ValueChanged
        Me.tmrAutoSwitch.Interval = CInt(nudAutoSwitchInterval.Value)
    End Sub

    Private Sub ctrlEnableAutoSwitch_CheckedChanged(sender As Object, e As EventArgs) Handles ctrlEnableAutoSwitch.CheckedChanged
        Me.EnableAutoSwitch = ctrlEnableAutoSwitch.Checked
    End Sub

    Private Sub tmrAutoSwitch_Tick(sender As Object, e As EventArgs) Handles tmrAutoSwitch.Tick
        ' No AutoSwitching, when a BackgroundWorker is running
        If Me.BackgroundWorkerDelegate.IsBusy Then Exit Sub
        Static ownedModalForm As Boolean

        ' No Autoswitching, when a modal Subform is open
        For Each ownedForm As Form In Me.OwnedForms
            If ownedForm.Modal = True Then
                ownedModalForm = True
                Return
            End If
        Next

        ' No Autoswitching, when a modal Subform was open the last tick
        ' (Prevents an Exception when renaming the PCM files)
        If ownedModalForm = True Then
            ownedModalForm = False
            Return
        End If

        Try
            Call Me.MsuTracks.AutoSwitch()
        Catch ex As Exception
            Me.EnableAutoSwitch = False
            Call MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=ex)
        End Try
        'Call Me.RefreshAltTrackSelection(True)
    End Sub

    Private Sub MsuTrack_TrackAltSwitched(ByVal sender As Object, ByVal e As MsuTrack.TrackAltSwitchedEventArgs) Handles _MsuTracks.TrackAltSwitched
        Dim trackNumberN As Nullable(Of Byte) = Me.TrackIdSelected
        If trackNumberN.HasValue Then
        Else
            Return ' No TrackId is selected. No refresh needed
        End If

        Dim trackNumber As Byte = CByte(trackNumberN)

        If e.MsuTrack.TrackNumber <> trackNumber Then
            Return ' Other TrackId is selected. No refresh needed
        End If

        ' Set selected alt. Track in Control to the newly selected alt. Track
        Me.TrackAltIdMarkedAsCurrent = e.MsuTrackAltNew.AltNumber
    End Sub

    Private Sub OpenMsuLocationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenMsuLocationToolStripMenuItem.Click, txtPathMsu.DoubleClick
        Call OpenDirInExplorer(System.IO.Directory.GetCurrentDirectory())
    End Sub

    Private Sub OpenAltTrackLocationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenAltTrackLocationToolStripMenuItem.Click
        Dim trackAltSelected As MsuTrackAlt = Me.TrackAltSelected
        If trackAltSelected Is Nothing Then Return
        Call Me.OpenDirInExplorer(trackAltSelected.LocationAbsolute)
    End Sub

    Private Sub OpenDirInExplorer(ByVal directory As String)
        Dim processStartInfo As New ProcessStartInfo
        Dim process As New Process

        ' Replace "\\.\" with "\\?\"
        If Msu.MsuHelper.PathHasUncLocalPref(directory) Then
            If directory.Chars(2).CompareTo("."c) = 0 Then
                Mid(directory, 3, 1) = "?"c
            End If
        End If

        With processStartInfo
            .FileName = "explorer.exe"
            .CreateNoWindow = True
            .WorkingDirectory = directory
            .Arguments = directory
            .ErrorDialogParentHandle = Me.Handle
            .ErrorDialog = True
        End With
        process.StartInfo = processStartInfo

        Try
            Call process.Start()
        Catch ex As Exception
            Call MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=ex)
        End Try
    End Sub

    Private Sub RefreshRichTextLogHandler(sender As Object, e As EventArgs) Handles objLogger.LogEntriesUpdated
        Call RefreshRichTextLog()
    End Sub

    Private Delegate Sub RefreshRichTextLogCallback()

    Private Sub RefreshRichTextLog()
        If Me.rtbLog Is Nothing Then Return
        ' See https://stackoverflow.com/a/10775421
        If Me.rtbLog.InvokeRequired Then
            Dim RefreshRichTextLogCallback As RefreshRichTextLogCallback = New RefreshRichTextLogCallback(AddressOf RefreshRichTextLog)
            Me.Invoke(RefreshRichTextLogCallback)
        Else
            Me.rtbLog.Rtf = Me.Logger.GetLogAsRichText(False, True)
            If Me.ctrlLogAutoScroll.Checked Then Call Me.rtbLog.ScrollToBottom()
        End If
    End Sub

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Msu.MsuHelper.IsDevelopmentVersion Then
            MyBase.Text =
        String.Format(
            "{0} (Dev {1})",
            My.Application.Info.ProductName,
            String.Concat(Msu.MsuHelper.GetHashOfSelf))
        Else
            MyBase.Text =
        String.Format(
            "{0} (Version {1})",
            My.Application.Info.ProductName,
            String.Concat(My.Application.Info.Version.Major, "."c, My.Application.Info.Version.Minor, "."c, My.Application.Info.Version.Revision))
        End If
        Call Me.MsuTracksConfigLoadStateUpdate()

        Me.EnableAutoSwitch = False
        Call ctrlKeepCmdOpen_RefreshEnableCheck()

        Me.nudProcessCount.Value = Math.Ceiling(CDec(System.Environment.ProcessorCount / 2))

        objLogger = New Logger.Logger(Byte.MaxValue)
        Me.Settings = Msu.Settings.Settings.LoadOrCreateNew(Me.Logger)

        If String.IsNullOrWhiteSpace(Me.Settings.AudioConversionSettings.MsuPcmPath) Then
            Call Me.Settings.AudioConversionSettings.TryLocateMsuPcmPath()
        End If

        objLogger.MaxEntries = Me.Settings.LoggerSettings.MaxEntries
        Me.nudLogEntries.Value = objLogger.MaxEntries

        Call FindAndLoadMsu()

        Call Me.SetToolTips()

        Me.Logger.AddToLog(text:="Program started", entryColor:=Drawing.Color.FromArgb(1, 1, 1))
    End Sub

    Private Sub MsuTracksConfigLoadStateUpdate()
        Dim msuTracksLoaded = _MsuTracks IsNot Nothing

        Me.grpMsuTracks.AllowDrop = msuTracksLoaded

        If msuTracksLoaded Then
            Call Me.EnableDisabledControls()
        Else
            Call Me.DisableControlsNoMsu()
        End If

        Call Me.SetToolTips()
    End Sub

    Public Sub FindAndLoadMsu()
        FindAndLoadMsu(Application.StartupPath(), True)
    End Sub

    Public Sub FindAndLoadMsu(ByRef directory As String, ByRef isStartupPath As Boolean)

        Dim jsonFiles() As String = System.IO.Directory.GetFiles(
                 path:=directory,
        searchPattern:=System.String.Concat("*"c, MsuHelper.JsonExtL),
         searchOption:=System.IO.SearchOption.TopDirectoryOnly)

        Dim jsonLoadError As Boolean = False

        For Each JsonFile As String In jsonFiles
            Try
                Dim MsuConfig = MsuTracks.LoadFromJson(jsonFilePath:=JsonFile, logger:=Nothing, settings:=Me.Settings)

                If MsuConfig.TrackDict.Count = 0 Then
                    Continue For
                End If

                Dim e As New System.ComponentModel.CancelEventArgs

                MsuConfig.Logger = Me.Logger

                Call Me.LoadFromJson(parsedJsonData:=MsuConfig, e:=e)

                If e.Cancel Then
                    ' Loading failed
                    Call MsuConfig.Dispose()
                    jsonLoadError = True
                    Continue For
                End If
                MsuConfig = Nothing

                Me.JsonFilePath = JsonFile

                If isStartupPath Then
                    Call Me.Logger.AddToLog($"Loaded from MSU JsonConfig ""{System.IO.Path.GetFileName(JsonFile)}"" in startup path.")
                Else
                    Call Me.Logger.AddToLog($"Loaded from MSU JsonConfig ""{JsonFile}"".")
                End If
                Return
            Catch ex As Exception
                ' Ignore and try next
                jsonLoadError = True
            End Try
        Next

        ' Don't try to create a new config, if loading from JSON failed
        If jsonLoadError Then Return

        Dim msuFiles() As String = System.IO.Directory.GetFiles(
         path:=directory,
searchPattern:="*.msu",
 searchOption:=System.IO.SearchOption.TopDirectoryOnly)

        For Each msuFile As String In msuFiles
            Try
                Dim e As New ComponentModel.CancelEventArgs

                Call Me.ScanNewMsuFolder(msuFile, e)

                If e.Cancel Then
                    ' Load Cancelled
                Else
                    If isStartupPath Then
                        Call Me.Logger.AddToLog($"Created new config from MSU ""{System.IO.Path.GetFileName(msuFile)}"" in startup path.")
                    Else
                        Call Me.Logger.AddToLog($"Created new config from MSU ""{msuFile}"".")
                    End If
                    Return
                End If
            Catch ex As Exception
                ' Ignore and try next
            End Try
        Next

    End Sub

    Private Sub rtbLog_Resize(sender As Object, e As EventArgs) Handles rtbLog.Resize
        If Me.ctrlLogAutoScroll.Checked Then Call Me.rtbLog.ScrollToBottom()
    End Sub

    Private Sub btnScanMsuDirectory_Click(sender As Object, e As EventArgs) Handles btnScanMsuDirectory.Click
        If Me.MsuTracks IsNot Nothing Then
            Call Me.MsuTracks.ScanMsuDirectoryForTracks()
            Call Me.FillTrackList()
        End If
    End Sub

    Private Sub btnConvertPcm_Click(sender As Object, e As EventArgs) Handles btnConvertPcm.Click
        If Me.MsuTracks Is Nothing Then Return
        Dim i As Byte

        If String.IsNullOrWhiteSpace(Me.Settings.AudioConversionSettings.MsuPcmPath) Then
            i = 1 ' MSUPCM++ path empty in settings
        ElseIf Not System.IO.File.Exists(Me.Settings.AudioConversionSettings.MsuPcmPath) Then
            i = 2 ' MSUPCM++ path invalid in settings
        End If

        If i <> 0 Then
            Dim sErrTxt As String

            If i = 1 Then ' MSUPCM++ path empty in settings
                sErrTxt = $"MSUPCM++ is needed to convert the PCM files."
            Else ' i = 2  ' MSUPCM++ path invalid in settings
                sErrTxt = $"MSUPCM++ location ""{Me.Settings.AudioConversionSettings.MsuPcmPath}"" is invalid."
            End If

            sErrTxt += $"{System.Environment.NewLine}Please select the location in the settings."

            Call System.Windows.Forms.MessageBox.Show(
                    owner:=Me,
                    text:=sErrTxt,
                    caption:="MSUPCM++ needed",
                    buttons:=MessageBoxButtons.OK,
                    icon:=MessageBoxIcon.Asterisk
                )
            Return
        End If

        Dim msuPcmBulkConversion As MsuPcmBulkConversion
        Dim processWindowStyle As ProcessWindowStyle = ProcessWindowStyle.Hidden
        Dim keepProcessesOpen As Boolean = False

        If Me.ctrlDisplayCmd.Checked Then
            processWindowStyle = ProcessWindowStyle.Normal
            keepProcessesOpen = Me.ctrlKeepCmdOpen.Checked
        End If

        Try
            msuPcmBulkConversion =
                MsuPcmBulkConversion.NewFromExistingPcmTracksInConfig(
                    msuTracksConfig:=Me.MsuTracks,
                         sampleRate:=CUInt(Me.nudPcmResample.Value),
                   volumePercentage:=CUShort(Me.nudPcmVolume.Value),
                 processWindowStyle:=processWindowStyle,
                  keepProcessesOpen:=keepProcessesOpen,
                       parentHandle:=Me.Handle,
                       processCount:=CByte(Me.nudProcessCount.Value)
                )
        Catch ex As System.ArgumentException
            Call MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=ex, MessageBoxIcon.Exclamation)
            Return
        Catch ex As System.Exception
            Call MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=ex)
            Return
        End Try


        Try
            'Call Me.DisableControls()
            'Call BackgroundWorkerPcmConvert.RunWorkerAsync(argument:=MsuPcmBulkConversion)

            Dim executeCallback As MsuPcmBulkConversion.ExecuteCallback =
                New MsuPcmBulkConversion.ExecuteCallback(AddressOf msuPcmBulkConversion.Execute)

            Call Me.BackgroundWorkerDelegate.RunWorkerAsync(argument:=executeCallback)

        Catch ex As System.Exception
            Call MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=ex)
        End Try
    End Sub

    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs)
        Call Me.EnableDisabledControls()

        If e.Error Is Nothing Then
            ' Success
        Else
            Call MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=e.Error)
        End If
    End Sub

    Private Property TemporarilyDisabledControls As New Dictionary(Of Control, Boolean)

    Private Delegate Sub DisableControlsCallback()

    Private Sub DisableControlsNoMsu()
        Dim controlDisable As New List(Of Control)

        Call controlDisable.Add(Me.btnSaveJson)
        Call controlDisable.Add(Me.btnSaveJsonAs)
        Call controlDisable.Add(Me.btnScanMsuDirectory)
        Call controlDisable.Add(Me.ctrlDisplayOnlyTracksWithAlts)
        Call controlDisable.Add(Me.grpMsuTracks)
        Call controlDisable.Add(Me.grpPcmConvert)
        Call controlDisable.Add(Me.grpAutoSwitch)

        Call DisableControls(controlDisable.ToArray)
    End Sub

    Private Sub DisableControl(ByRef control As Control)

        If control.Enabled Then
            control.Enabled = False
            If TemporarilyDisabledControls.ContainsKey(control) Then
            Else
                Call TemporarilyDisabledControls.Add(control, control.Enabled)
            End If
        End If

    End Sub

    Private Sub DisableControls(ByRef controls As Control())
        For Each control As Control In controls
            Call DisableControl(control)
        Next
    End Sub

    Private Sub DisableControls()
        If Me.InvokeRequired Then
            Dim DisableControlsCallback As DisableControlsCallback = New DisableControlsCallback(AddressOf Me.DisableControls)
            Me.Invoke(DisableControlsCallback)
            Return
        End If
        Call DisableControls(Me, Me.Controls)
    End Sub

    Private Sub DisableControls(ByRef Parent As Control, ByRef Controls As Control.ControlCollection)
        Call TemporarilyDisabledControls.TryAdd(Parent, True)

        For i = 0 To Controls.Count - 1
            Dim Control = Controls.Item(i)

            If TemporarilyDisabledControls.ContainsKey(Control) Then
                Continue For
            End If

            ' Do not disable Settings for Log 
            If Control Is Me.grpLogSettings Then
                Continue For
            End If
            ' Do not disable RichTextBox for Log 
            If Control Is Me.rtbLog Then
                Continue For
            End If

            If TypeOf Control Is GroupBox Then
                Dim GroupBox = DirectCast(Control, GroupBox)
                Call DisableControls(Control, GroupBox.Controls)
                Continue For
            End If

            If TypeOf Control Is ContainerControl Then
                Dim ContainerControl = DirectCast(Control, ContainerControl)
                Call DisableControls(Control, ContainerControl.Controls)
                Continue For
            End If

            If TypeOf Control Is Panel Then
                Dim Panel = DirectCast(Control, Panel)
                Call DisableControls(Control, Panel.Controls)
                Continue For
            End If

            Debug.Print(Control.Name)

            If Control.Enabled Then
                Control.Enabled = False
                If TemporarilyDisabledControls.ContainsKey(Control) Then
                Else
                    Call TemporarilyDisabledControls.Add(Control, Control.Enabled)
                End If
            End If
        Next
    End Sub

    Private Delegate Sub EnableDisabledControlsCallback()

    Private Sub EnableDisabledControls()
        If TemporarilyDisabledControls Is Nothing Then Return

        If Me.InvokeRequired Then
            Dim EnableDisabledControlsCallback As EnableDisabledControlsCallback = New EnableDisabledControlsCallback(AddressOf EnableDisabledControls)
            Me.Invoke(EnableDisabledControlsCallback)
            Return
        End If

        For Each KeyValuePair In TemporarilyDisabledControls
            If KeyValuePair.Value Then
            Else
                KeyValuePair.Key.Enabled = True
            End If

            Call TemporarilyDisabledControls.Remove(KeyValuePair.Key)
        Next
    End Sub

    Private Sub nudProcessCount_ValueChanged(sender As Object, e As EventArgs) Handles nudProcessCount.ValueChanged
        Select Case nudProcessCount.Value
            Case 1
                Me.lblProcessCount.Text = "Process"
            Case Else
                Me.lblProcessCount.Text = "Processes"
        End Select
    End Sub

    Private Sub MsuTracks_ConvertedFilesSwitched(sender As Object, e As EventArgs) Handles _MsuTracks.ConvertedFilesSwitched
        Call Me.CheckForNormalTrackVersion()
    End Sub

    Private Delegate Sub CheckForNormalTrackVersionCallback()

    Private Sub CheckForNormalTrackVersion()
        If Me.MsuTracks Is Nothing Then Return

        If Me.InvokeRequired Then
            Dim CheckForNormalTrackVersionCallback As CheckForNormalTrackVersionCallback = New CheckForNormalTrackVersionCallback(AddressOf CheckForNormalTrackVersion)
            Me.Invoke(CheckForNormalTrackVersionCallback)
            Return
        End If

        If Me.MsuTracks.TrackFileWithNormalVersionSuffixExists Then
            Me.optPcmConverted.Checked = True
        Else
            Me.optPcmNormal.Checked = True
        End If
    End Sub

    Private Sub btnPcmToNormal_Click(sender As Object, e As EventArgs) Handles btnPcmToNormal.Click
        If Me.MsuTracks Is Nothing Then Return
        Dim SwitchConvertedFilesToNormalVersionCallback = New MsuTracks.SwitchConvertedFilesToNormalVersionCallback(AddressOf Me.MsuTracks.SwitchConvertedFilesToNormalVersion)

        Call BackgroundWorkerDelegate.RunWorkerAsync(SwitchConvertedFilesToNormalVersionCallback)
    End Sub

    Private Sub BackgroundWorkerDelegate_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerDelegate.RunWorkerCompleted
        Call Me.BackgroundWorker_RunWorkerCompleted(sender, e)
    End Sub

    Private Sub ctrlKeepCmdOpen_CheckedChanged(sender As Object, e As System.EventArgs) Handles ctrlKeepCmdOpen.CheckedChanged
        If ctrlKeepCmdOpen.Checked Then Else Return

        Dim msg As String =
            "By enabling this, all CMD windows for converting the .pcm audio files won't be automatically closed, when the process is finished." & Environment.NewLine &
            "This can be useful for troubleshooting, but you will have to close all windows manually." & Environment.NewLine & Environment.NewLine &
            "Do you want to continue?"

        DialogResult = MessageBox.Show(
            owner:=Me,
             text:=Msg,
          caption:=My.Application.Info.ProductName,
          buttons:=MessageBoxButtons.YesNo,
             icon:=MessageBoxIcon.Question, defaultButton:=MessageBoxDefaultButton.Button2
)

        If DialogResult <> DialogResult.Yes Then
            ctrlKeepCmdOpen.Checked = False
        End If
    End Sub

    Private Sub ctrlKeepCmdOpen_RefreshEnableCheck()
        ctrlKeepCmdOpen.Enabled = ctrlDisplayCmd.Checked
    End Sub

    Private Sub ctrlDisplayCmd_CheckedChanged(sender As Object, e As EventArgs) Handles ctrlDisplayCmd.CheckedChanged
        Call ctrlKeepCmdOpen_RefreshEnableCheck()
    End Sub

    Private Sub btnLogClear_Click(sender As Object, e As EventArgs) Handles btnLogClear.Click
        Call Me.Logger.Clear()
    End Sub

    Private Sub nudLogEntries_ValueChanged(sender As Object, e As System.EventArgs) Handles nudLogEntries.ValueChanged
        If Me.Logger Is Nothing Then Return
        Dim MaxEntries = CUInt(nudLogEntries.Value)
        If Me.Logger.MaxEntries <> MaxEntries Then
            Me.Logger.MaxEntries = MaxEntries
        End If
    End Sub

    Private Sub btnLogExport_Click(sender As Object, e As EventArgs) Handles btnLogExport.Click
        Dim DefaultFileNameSet As Boolean = False

        If Me.MsuTracks IsNot Nothing Then
            If String.IsNullOrWhiteSpace(Me.MsuTracks.MsuName) Then
            Else
                Me.sfdLogExport.FileName = String.Concat(Me.MsuTracks.MsuName, "_LogExport.", Me.sfdLogExport.DefaultExt)
                DefaultFileNameSet = True
            End If
        End If

        If DefaultFileNameSet Then
        Else
            Me.sfdLogExport.FileName = String.Concat("Msu_LogExport.", Me.sfdLogExport.DefaultExt)
        End If

        Call Me.sfdLogExport.ShowDialog(owner:=Me)
    End Sub

    Private Sub sfdLogExport_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles sfdLogExport.FileOk
        Try
            ' Create a new file
            Dim FileStream As _
            New System.IO.FileStream(
                path:=sfdLogExport.FileName,
              access:=IO.FileAccess.Write,
               share:=IO.FileShare.Read,
                mode:=IO.FileMode.Create)

            Dim WriterEncoding As System.Text.Encoding

            Dim WriteString As String = Constants.vbNullString

            Select Case Me.sfdLogExport.FilterIndex
                Case 1
                    ' Rich-Text-File

                    WriterEncoding = System.Text.Encoding.Latin1
                    WriteString = Me.Logger.GetLogAsRichText(False, True)
                    'WriteString = Me.rtbLog.Rtf.Replace("\\", "\'5c", StringComparison.Ordinal)

                Case Else ' 2
                    ' Plain-Text

                    WriterEncoding = New System.Text.UTF8Encoding(encoderShouldEmitUTF8Identifier:=True, throwOnInvalidBytes:=True)
                    WriteString = Me.Logger.GetLogAsPlainText(False, True)
            End Select

            'Dim Writer As New _
            '    System.IO.StreamWriter(
            '                stream:=FileStream,
            '                encoding:=WriterEncoding,
            '                leaveOpen:=False)
            Dim Writer As New _
                        System.IO.BinaryWriter(
                            output:=FileStream,
                            encoding:=WriterEncoding,
                            leaveOpen:=False)

            Call Writer.Write(WriteString.ToCharArray)

            Call Writer.Close()

        Catch ex As System.Exception
            Call MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=ex)
        End Try
    End Sub

    Private Sub ContextMenuStripTracks_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStripTracks.Opening, ContextMenuStripAltTracks.Opening
        e.Cancel = Me.MsuTracks Is Nothing : If e.Cancel Then Return

        If sender Is Me.ContextMenuStripTracks Then
            Dim HasTrackItemSelected As Boolean = Me.TrackIdSelected IsNot Nothing
            Me.EditTrackToolStripMenuItem.Enabled = HasTrackItemSelected
            Me.DeleteTrackToolStripMenuItem.Enabled = HasTrackItemSelected
        ElseIf sender Is Me.ContextMenuStripAltTracks Then
            Dim HasTrackItemSelected As Boolean = Me.TrackIdSelected IsNot Nothing
            Dim TrackAltSelected = Me.TrackAltSelected
            Dim HasTrackAltItemSelected As Boolean = TrackAltSelected IsNot Nothing
            If HasTrackAltItemSelected Then
                Me.SetAsCurrentTrackToolStripMenuItem.Enabled = TrackAltSelected.FilePathExists
            Else
                Me.SetAsCurrentTrackToolStripMenuItem.Enabled = HasTrackAltItemSelected
            End If
            Me.EditAltTrackToolStripMenuItem.Enabled = HasTrackAltItemSelected
            Me.DeleteAltTrackToolStripMenuItem.Enabled = HasTrackAltItemSelected
            Me.OpenAltTrackLocationToolStripMenuItem.Enabled = HasTrackAltItemSelected
            Me.AddNewAltTrackToolStripMenuItem.Enabled = HasTrackItemSelected
        Else
            Throw New NotImplementedException("Tried opening ContextMenu with invalid sender object")
        End If
    End Sub

    Private Sub DeleteTrackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteTrackToolStripMenuItem.Click
        If Me.MsuTracks Is Nothing Then Return
        Dim ToolStripMenuItem As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        Dim TrackSelected = Me.TrackSelected
        If TrackSelected Is Nothing Then Return

        Dim Msg As String =
                $"Deleting Track {TrackSelected.TrackNumber}"
        If String.IsNullOrWhiteSpace(TrackSelected.Title) Then
        Else
            Msg = String.Concat(Msg,
                $" (""{TrackSelected.Title}"")")
        End If

        If TrackSelected.TrackAltArray.Length <> 0 Then
            Msg = String.Concat(Msg,
                $" including the {TrackSelected.TrackAltArray.Length} associated alt. Tracks.")
        End If

        Msg = String.Concat(Msg, Environment.NewLine, Environment.NewLine,
                 $"Do you also want to delete the associated PCM files?")

        DialogResult = MessageBox.Show(
            owner:=Me,
             text:=Msg,
          caption:=My.Application.Info.ProductName,
          buttons:=MessageBoxButtons.YesNoCancel,
             icon:=MessageBoxIcon.Question, defaultButton:=MessageBoxDefaultButton.Button3)

        Dim DeleteFiles As Boolean

        Select Case DialogResult
            Case DialogResult.Yes
                DeleteFiles = True
            Case DialogResult.No
                DeleteFiles = False
            Case Else
                Return
        End Select

        If DeleteFiles Then
            Try
                ' Try deleting the file(s)
                Call TrackSelected.Delete()
            Catch ex As Exception
                Call MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=ex)
                Return
            End Try
        End If

        ' Remove Track from Dictionary of Parent
        Call TrackSelected.Parent.TrackDict.Remove(TrackSelected.TrackNumber)

        ' Dispose Track Object
        Call TrackSelected.Dispose()

        Call Me.FillTrackList()
    End Sub

    Private Sub DeleteAltTrackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteAltTrackToolStripMenuItem.Click
        If Me.MsuTracks Is Nothing Then Return
        Dim ToolStripMenuItem As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        Dim TrackAltSelected = Me.TrackAltSelected
        If TrackAltSelected Is Nothing Then Return

        Dim Msg As String =
                $"Deleting alt. Track {TrackAltSelected.AltNumber}"
        If String.IsNullOrWhiteSpace(TrackAltSelected.Title) Then
        Else
            Msg = String.Concat(Msg,
                $" (""{TrackAltSelected.Title}"")")
        End If

        Msg = String.Concat(Msg,
                $" for Track {TrackAltSelected.Parent.TrackNumber}")
        If String.IsNullOrWhiteSpace(TrackAltSelected.Parent.Title) Then
        Else
            Msg = String.Concat(Msg,
                $" (""{TrackAltSelected.Parent.Title}"")")
        End If

        Msg = String.Concat(Msg, Environment.NewLine, Environment.NewLine,
                 $"Do you also want to delete the associated PCM file?")

        DialogResult = MessageBox.Show(
            owner:=Me,
             text:=Msg,
          caption:=My.Application.Info.ProductName,
          buttons:=MessageBoxButtons.YesNoCancel,
             icon:=MessageBoxIcon.Question, defaultButton:=MessageBoxDefaultButton.Button3)

        Dim DeleteFile As Boolean

        Select Case DialogResult
            Case DialogResult.Yes
                DeleteFile = True
            Case DialogResult.No
                DeleteFile = False
            Case Else
                Return
        End Select

        If DeleteFile Then
            Try
                ' Try deleting the file
                Call TrackAltSelected.Delete()
            Catch ex As Exception
                Call MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=ex)
                Return
            End Try
        Else
            ' Set other alt. Track as current Track, if this is the current Track
            Try
                Call TrackAltSelected.UnSetAsCurrentAltTrack()
            Catch ex As Exception
                ' Ignore
            End Try
        End If

        ' Remove alt. Track from Dictionary of Parent
        Call TrackAltSelected.Parent.TrackAltDict.Remove(TrackAltSelected.AltNumber)

        ' Dispose alt. Track Object
        Call TrackAltSelected.Dispose()

        Call Me.FillAltTrackList()
    End Sub

    Private Sub EditTrackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditTrackToolStripMenuItem.Click
        Call OpenEditFormForSelectedMsuTrack()
    End Sub

    Private Sub EditAltTrackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditAltTrackToolStripMenuItem.Click
        Call OpenEditFormForMsuSelectedTrackAlt()
    End Sub

    Private Sub lstvTracks_DoubleClick(sender As Object, e As EventArgs) Handles lstvTracks.DoubleClick
        Call OpenEditFormForSelectedMsuTrack()
    End Sub

    Private Sub OpenEditFormForSelectedMsuTrack()
        Dim bytTrackNumberN As Nullable(Of Byte) = Me.TrackIdSelected

        ' Cancel if no track number is selected
        If bytTrackNumberN.HasValue Then
        Else
            Return
        End If

        Dim objMsuTrack As MsuTrack = Me.MsuTracks.TrackDict(CByte(bytTrackNumberN))

        Call OpenEditFormForMsuTrack(objMsuTrack)
    End Sub

    Private Sub OpenEditFormForMsuTrack(ByRef objMsuTrack As MsuTrack)
        Dim objFrmMsuTrackEdit As New MsuTrackEditForm With {
            .Icon = Me.Icon
        }

        Call objFrmMsuTrackEdit.MsuTrackControl.LoadMsuTrackToEdit(objMsuTrack)

        Call objFrmMsuTrackEdit.ShowDialog(owner:=Me)

        Call objFrmMsuTrackEdit.Dispose()

        Call Me.FillTrackList()
    End Sub

    Private Sub OpenEditFormForMsuSelectedTrackAlt()
        Dim MsuTrackAlt As MsuTrackAlt = Me.TrackAltSelected
        If MsuTrackAlt Is Nothing Then Return
        Call OpenEditFormForMsuTrackAlt(MsuTrackAlt)
    End Sub

    Private Sub OpenEditFormForMsuTrackAlt(ByRef MsuTrackAlt As MsuTrackAlt)
        Call OpenEditFormForMsuTrackAlt(DirectCast(MsuTrackAlt, MsuPcmFile))
    End Sub

    Private Sub OpenEditFormForMsuTrackAlt(ByRef MsuTrackAddNewAlt As MsuTrack)
        Call OpenEditFormForMsuTrackAlt(DirectCast(MsuTrackAddNewAlt, MsuPcmFile))
    End Sub

    Private Sub OpenEditFormForMsuTrackAlt(ByRef MsuPcmFile As MsuPcmFile)
        Dim MsuTrackAltEditForm As New MsuTrackAltEditForm With {
            .Icon = Me.Icon
        }

        If TypeOf MsuPcmFile Is MsuTrackAlt Then
            Call MsuTrackAltEditForm.MsuTrackAltControl.LoadMsuTrackAltToEdit(DirectCast(MsuPcmFile, MsuTrackAlt))
        Else
            Call MsuTrackAltEditForm.MsuTrackAltControl.AddNewMsuTrackAlt(DirectCast(MsuPcmFile, MsuTrack))
        End If

        Call MsuTrackAltEditForm.ShowDialog(owner:=Me)

        Call MsuTrackAltEditForm.Dispose()

        Call Me.FillAltTrackList()
    End Sub

    Private Sub AddNewAltTrackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddNewAltTrackToolStripMenuItem.Click
        Dim TrackSelected = Me.TrackSelected
        If TrackSelected Is Nothing Then Return
        Call OpenEditFormForMsuTrackAlt(TrackSelected)
    End Sub

    Private Sub SetAsCurrentTrackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetAsCurrentTrackToolStripMenuItem.Click
        Call SetSelectedAltTrackAsCurrent()
    End Sub

#If False Then
    Private Sub lstvAltTracks_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstvAltTracks.ItemCheck
        Dim TrackAltMarkedAsCurrent = Me.TrackAltMarkedAsCurrent

        Dim ItemCheck = lstvAltTracks.Items().Item(e.Index)
        Dim ItemCheckTrackAltNumber As UInt16 = CUShort(ItemCheck.Group.Name)

        If e.NewValue = CheckState.Checked Then

            Dim TrackSelected As MsuTrack = Me.TrackSelected

            Try
                Call TrackSelected.SetCurrentAltTrack(ItemCheckTrackAltNumber)
            Catch ex As Exception
                Call MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=ex)
                e.NewValue = e.CurrentValue
            End Try
        Else
            If TrackAltMarkedAsCurrent Is Nothing OrElse ItemCheckTrackAltNumber = TrackAltMarkedAsCurrent Then
                e.NewValue = CheckState.Checked
            End If
        End If
    End Sub
#End If

    Private Sub lstvAltTracks_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles lstvAltTracks.ItemChecked
        Static checkInProgress As Boolean = False

        If checkInProgress Then
            Return ' Prevents nested execution
        End If
        If _FillingAltTrackList Then Return

        checkInProgress = True

        Dim ItemCheck = e.Item
        Dim ItemCheckTrackAltNumber As UInt16 = CUShort(ItemCheck.Group.Name)

        Dim TrackAltIdMarkedAsCurrent = Me.TrackAltIdMarkedAsCurrent

        If ItemCheck.Checked Then
            Dim TrackSelected As MsuTrack = Me.TrackSelected

            Me.TrackAltIdMarkedAsCurrent = ItemCheckTrackAltNumber

            Call SetMarkedAltTrackAsCurrent()
        Else
            If TrackAltIdMarkedAsCurrent Is Nothing OrElse ItemCheckTrackAltNumber = TrackAltIdMarkedAsCurrent Then
                Me.TrackAltIdMarkedAsCurrent = ItemCheckTrackAltNumber
            End If
        End If
        checkInProgress = False
    End Sub

    Private Sub grpMsuTracks_DragDrop(sender As Object, e As DragEventArgs) Handles grpMsuTracks.DragDrop
        If Me.MsuTracks Is Nothing Then
            e.Effect = DragDropEffects.None
            Return
        End If
        Dim PcmFiles() As String = Me.MsuTracks.CheckForValidPcmFiles(DirectCast(e.Data.GetData(DataFormats.FileDrop), String()))

        If PcmFiles Is Nothing OrElse PcmFiles.Length = 0 Then
            Return
        End If

        For Each PcmFile In PcmFiles

            Dim PcmFileLocationRemUnc = PathRemoveUncLocalPref(System.IO.Path.GetDirectoryName(PcmFile))
            Dim MsuLocationRemUnc = PathRemoveUncLocalPref(Me.MsuTracks.MsuLocation)

            If PcmFileLocationRemUnc.Equals(MsuLocationRemUnc, StringComparison.OrdinalIgnoreCase) Then
                Call Me.MsuTracks.AddPcmTrackIfMissing(PcmFile, True)
            Else
                Call Me.MsuTracks.AddPcmTrackIfMissing(PcmFile, False)
            End If
        Next

        Call Me.FillTrackList()
    End Sub

    Private Sub grpMsuTracks_DragEnter(sender As Object, e As DragEventArgs) Handles grpMsuTracks.DragEnter
        If Me.MsuTracks Is Nothing Then
            e.Effect = DragDropEffects.None
            Return
        End If
        Dim Files() As String = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())

        If Files Is Nothing OrElse Files.Length = 0 Then
            e.Effect = DragDropEffects.None
            Return
        End If

        Dim FilesChecked = Me.MsuTracks.CheckForValidPcmFiles(Files)

        If FilesChecked Is Nothing OrElse FilesChecked.Length = 0 Then
            e.Effect = DragDropEffects.None
            Return
        End If

        e.Effect = DragDropEffects.All
    End Sub


    Private Sub txtPathMsu_DragEnter(sender As Object, e As DragEventArgs) Handles txtPathMsu.DragEnter
        Dim Files() As String = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())

        If Files Is Nothing OrElse Files.Length = 0 Then
            e.Effect = DragDropEffects.None
            Return
        End If

        ' Only single file allowed for drag and drop
        If Files.Length <> 1 Then
            e.Effect = DragDropEffects.None
            Return
        End If

        e.Effect = DragDropEffects.All
    End Sub

    Private Sub txtPathMsu_DragDrop(sender As Object, e As DragEventArgs) Handles txtPathMsu.DragDrop
        Dim Files() As String = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())

        If Files Is Nothing OrElse Files.Length <> 1 Then
            Return
        End If

        Call Me.LoadFromSelectedFile(Files.Single)
    End Sub

    Private Sub SetToolTips()
        Dim ttpMsuAltSel = Me.ttpMsuAltSel
        Dim contextMenuHint = "Hint: You can use right-click to open the context menu."

        With ttpMsuAltSel

            .SetToolTip(Me.grpMsuTracks, $"List of MSU tracks and the alternative tracks.{Environment.NewLine}{contextMenuHint}")

            '.SetToolTip(Me.lstvTracks,
            '           $"List of tracks relevant to the current MSU.{Environment.NewLine}{contextMenuHint}")

            '.SetToolTip(Me.lstvAltTracks,
            '           $"List of alternative tracks for the currently selected MSU TrackId.{Environment.NewLine}{contextMenuHint}")

            .SetToolTip(Me.txtPathMsu,
                        "Path of the current MSU file.")

            .SetToolTip(Me.btnSelPathMsu,
                        "Select MSU/ROM or load from existing JSON configuration.")

            .SetToolTip(Me.btnSaveJson,
                       $"Serializes the current configuration as a JSON file.{Environment.NewLine}" &
                       $"Path: ""{Me.JsonFilePath}""")

            .SetToolTip(Me.btnSaveJsonAs,
                       $"Serializes the current configuration as a JSON file.{Environment.NewLine}" &
                       $"Opens the save dialog.")

            Dim btnScanMsuDirectoryToolTip As String =
                "Looks for new PCM files inside "
            If Me.MsuTracks Is Nothing Then
                btnScanMsuDirectoryToolTip = String.Concat(btnScanMsuDirectoryToolTip,
                "the MSU direcory.")
            Else
                btnScanMsuDirectoryToolTip = String.Concat(btnScanMsuDirectoryToolTip,
                $"""{Me.MsuTracks.MsuLocation}"".")
            End If

            .SetToolTip(Me.btnScanMsuDirectory, btnScanMsuDirectoryToolTip)

            .SetToolTip(Me.btnSettings, "Opens additional settings.")

            .SetToolTip(Me.ctrlDisplayOnlyTracksWithAlts,
                       $"Hide the tracks in the list below, that have no alternative tracks.{Environment.NewLine}" &
                       $"(Only has main/default track).")

            .SetToolTip(Me.grpPcmConvert,
                       $"Convert the pcm files with the specified parameters using MSUPCM++.")

            .SetToolTip(Me.nudPcmResample,
                       $"Playback rate of the outout files compared to 44100Hz.{Environment.NewLine}" &
                       $"(Will get resampled to 44100Hz afterwards, since the playback rate is fixed for MSU1).")
            .SetToolTip(Me.lblHz, .GetToolTip(Me.nudPcmResample))

            .SetToolTip(Me.nudPcmVolume,
                        "Volume percentage of the output files.")
            .SetToolTip(Me.lblPcmVolume, .GetToolTip(Me.nudPcmVolume))

            .SetToolTip(Me.nudProcessCount,
                        "Number of instances of MSUPCM++ running simultaneously.")
            .SetToolTip(Me.lblProcessCount, .GetToolTip(Me.nudProcessCount))

            .SetToolTip(Me.btnPcmToNormal,
                        "Change the converted PCM files back to the normal versions.")

            .SetToolTip(Me.btnConvertPcm,
                       $"Execute the conversion of the PCM files via MSUPCM++.{Environment.NewLine}." &
                       Environment.NewLine &
                       $"Hint:{Environment.NewLine}" &
                       $"{Constants.vbTab}The MSUPCM++ executable needs to be set in the settings.{Environment.NewLine}" &
                       $"{Constants.vbTab}Will be set automatically, if the program is in %PATH% or the same directory as this program.")

            .SetToolTip(Me.ctrlDisplayCmd,
                       $"Don't hide the CMD windows during the conversion with MSUPCM++.")

            .SetToolTip(Me.ctrlKeepCmdOpen,
                       $"Don't close the CMD windows automatically, when the conversion with MSUPCM++ has finished.{Environment.NewLine}" &
                       $"Can be useful for troubleshooting, if the conversion fails.")

            .SetToolTip(Me.grpAutoSwitch,
                       $"Set alt. Tracks automatically as the current track, if another TrackId is playing.{Environment.NewLine}" &
                        "(Mostly relevant for DKC3)")

            .SetToolTip(Me.nudLogEntries,
                       $"Maximum number of Log-Entries that will be kept.{Environment.NewLine}" &
                        "Too many Log-Entries will decrease performance.")
            .SetToolTip(Me.lblLogEntries, .GetToolTip(Me.nudLogEntries))

            .SetToolTip(Me.ctrlLogAutoScroll,
                       $"Automatically scroll to the last Log-Entry.")

            .SetToolTip(Me.btnLogClear,
                       $"Clear log.")

            .SetToolTip(Me.btnLogExport,
                       $"Export the log as a text file.")
        End With
    End Sub
End Class