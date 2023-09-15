Option Compare Binary
Option Explicit On
Option Strict On
Imports MsuAltSelect.Msu.Tracks
Imports MsuAltSelect.Msu.Ex

Public Class MsuTrackAltControl

    'Inherits MsuControlBase

    Public Event AppliedChanges(sender As Object, e As EventArgs)
    Public Event UserChangedValue(sender As Object, e As EventArgs)
    Public Event FormDirty(sender As Object, e As EventArgs)

    Private _Dirty As Boolean
    Public Property Dirty As Boolean
        Get
            Return _Dirty
        End Get
        Set(dirtyNew As Boolean)
            If dirtyNew = _Dirty Then Return ' Unchanged

            If dirtyNew Then
            Else
                If Me.MsuTrackAltToEdit Is Nothing Then
                    Call AddNewMsuTrackAlt(Me.MsuTrack)
                Else
                    Call LoadMsuTrackAltToEdit(Me.MsuTrackAltToEdit)
                End If
            End If

            _Dirty = dirtyNew
        End Set
    End Property

    Public Property ChangedLocation As Boolean
        Get
            Return _ChangedLocation
        End Get
        Private Set(value As Boolean)
            _ChangedLocation = value
        End Set
    End Property

    Private _ChangedLocation As Boolean
    Private Property MsuTrackAltToEdit As MsuTrackAlt
    Private Property MsuTrackAltTmp As MsuTrackAlt
    Private Property MsuTrack As MsuTrack
    Private Property MsuTracks As MsuTracks

    Public Property MsuSelButtonForFullPathEnabled As Boolean
        Get
            Return Me.btnSelPathPcm.Enabled
        End Get
        Protected Set(value As Boolean)
            Me.btnSelPathPcm.Enabled = value
        End Set
    End Property

    Public Property MsuSelButtonForLocationEnabled As Boolean
        Get
            Return Me.btnSelLocationPcm.Enabled
        End Get
        Protected Set(value As Boolean)
            Me.btnSelLocationPcm.Enabled = value
        End Set
    End Property

    Public Sub AddNewMsuTrackAlt(ByRef msuTrackAddAlt As MsuTrack)
        Me.ChangedLocation = False
        Me.MsuSelButtonForFullPathEnabled = True
        Me.MsuSelButtonForLocationEnabled = Not Me.MsuSelButtonForFullPathEnabled

        Me.MsuTrack = msuTrackAddAlt
        Me.MsuTracks = Me.MsuTrack.Parent

        ' Create MsuTrackAlt Object to add the new data
        Me.MsuTrackAltTmp = New MsuTrackAlt(Me.MsuTrack, Me.MsuTrack.GetFirstAvailableAltNum, Nothing, Nothing)

        Call SetToolTips()

        Call RefreshTextFields()
    End Sub

    Public Sub LoadMsuTrackAltToEdit(ByRef objMsuTrackAltToEdit As MsuTrackAlt)
        Me.ChangedLocation = False
        Me.MsuTrackAltToEdit = objMsuTrackAltToEdit

        If Me.MsuTrackAltTmp IsNot Nothing Then
            Call Me.MsuTrackAltTmp.Dispose()
        End If

        Me.MsuSelButtonForFullPathEnabled = Me.MsuTrackAltToEdit.FilePathExists
        Me.MsuSelButtonForLocationEnabled = Not Me.MsuSelButtonForFullPathEnabled

        Me.MsuTrack = Me.MsuTrackAltToEdit.Parent
        Me.MsuTracks = Me.MsuTrack.Parent

        ' Create temporary copy of the MsuTrackAlt Object to edit
        Me.MsuTrackAltTmp = New MsuTrackAlt(Me.MsuTrack, Me.MsuTrackAltToEdit.AltNumber, Me.MsuTrackAltToEdit.LocationAbsolute, Me.MsuTrackAltToEdit.Title) With {
            .AutoSwitchTrackNumbers = Me.MsuTrackAltToEdit.AutoSwitchTrackNumbers
        }

        Call SetToolTips()
        Call RefreshTextFields()
    End Sub
    Private Sub Form_UserChangedValue(sender As Object, e As EventArgs)
        Call Me.RefreshTextFields()
        RaiseEvent FormDirty(sender, e)
    End Sub

    Protected Sub RefreshTextFields()

        If Me.txtMsuAltTitle.Text.Equals(Me.MsuTrackAltTmp.Title, StringComparison.Ordinal) Then
        Else
            Me.txtMsuAltTitle.Text = Me.MsuTrackAltTmp.Title
        End If

        Dim altNumber = Me.MsuTrackAltTmp.AltNumber
        Me.nudMsuTrackAltId.Value = altNumber

        Me.txtMsuFileName.Text = Me.MsuTrack.FileName

        Dim locationRelative = Me.MsuTrackAltTmp.LocationRelative
        Me.txtRelativeLocation.Text = locationRelative

        Dim locationAbsolute = Me.MsuTrackAltTmp.LocationAbsolute
        Me.txtAbsoluteLocation.Text = locationAbsolute

        Dim filePath = Me.MsuTrackAltTmp.FilePath
        Me.txtFilePath.Text = filePath

        Dim autoSwitchTrackNumbersJson = Me.MsuTrackAltTmp.AutoSwitchTrackNumbersJson
        Me.txtAutoSwitch.Text = autoSwitchTrackNumbersJson
    End Sub

    Public Sub ApplyChanges()

        ' Check for existing Track with this Id
        If Me.MsuTrack.TrackAltDict.ContainsKey(Me.MsuTrackAltTmp.AltNumber) Then
            Dim msuTrackAltExisting As MsuTrackAlt = Me.MsuTrack.TrackAltDict.Item(Me.MsuTrackAltTmp.AltNumber)

            If msuTrackAltExisting IsNot Me.MsuTrackAltToEdit Then
                Throw New MsuTrackAltAlreadyExistsException(msuTrackAltExisting, MsuTrackAltAlreadyExistsException.ExceptionType.DuplicateId)
            End If
        End If

        If Me.MsuTrack.TrackAltDict.Count <> 0 Then
            Dim msuTrackAltExisting As MsuTrackAlt = Me.MsuTrack.GetAltTrackByLocation(Me.MsuTrackAltTmp.LocationAbsolute)

            If msuTrackAltExisting IsNot Me.MsuTrackAltToEdit Then
                Throw New MsuTrackAltAlreadyExistsException(msuTrackAltExisting, MsuTrackAltAlreadyExistsException.ExceptionType.DuplicatePath)
            End If
        End If

        If String.IsNullOrWhiteSpace(Me.MsuTrackAltTmp.LocationAbsolute) Then
            Throw New ArgumentException("Location of alt. Track not selected.", "Location")
        End If

        If Me.MsuTrackAltTmp.AutoSwitchTrackNumbers IsNot Nothing Then

            For i As Integer = LBound(Me.MsuTrackAltTmp.AutoSwitchTrackNumbers) To UBound(Me.MsuTrackAltTmp.AutoSwitchTrackNumbers)

                If Me.MsuTracks.TrackDict.ContainsKey(Me.MsuTrackAltTmp.AutoSwitchTrackNumbers(i)) Then
                Else
                    ' MsuTrack doesn't exist. Ignore.
                    Continue For
                End If

                ' Get MsuTrack that will make the alt. Track switch to the current Track automatically
                Dim msuTrack As MsuTrack = Me.MsuTracks.TrackDict.Item(Me.MsuTrackAltTmp.AutoSwitchTrackNumbers(i))

                ' Check for existing alt. Tracks with AutoSwitch
                Dim dictAsatft As SortedDictionary(Of System.UInt16, MsuTrackAlt) = Me.MsuTrack.GetAutoSwitchAltTrackForTrackDict(msuTrack)

                If dictAsatft Is Nothing Then
                    Continue For
                End If

                Select Case dictAsatft.Count
                    Case 0
                        Continue For
                    Case 1
                        If dictAsatft.Values.First.AltNumber = Me.MsuTrackAltTmp.AltNumber Then
                            ' Existing alt. Track is the same as the one being edited.
                            Continue For
                        Else
                            Call dictAsatft.Add(Me.MsuTrackAltTmp.AltNumber, Me.MsuTrackAltTmp)
                            Throw New MsuAutoSwitchDataDuplicateException(dictAsatft.Values.ToArray, msuTrack)
                        End If
                    Case Else
                        If dictAsatft.ContainsKey(Me.MsuTrackAltTmp.AltNumber) Then
                            Throw New MsuAutoSwitchDataDuplicateException(dictAsatft.Values.ToArray, msuTrack)
                        Else
                            Call dictAsatft.Add(Me.MsuTrackAltTmp.AltNumber, Me.MsuTrackAltTmp)
                            Throw New MsuAutoSwitchDataDuplicateException(dictAsatft.Values.ToArray, msuTrack)
                        End If
                End Select
            Next
        End If

        If Me.MsuTrackAltToEdit Is Nothing Then

            ' Add New track

            Call Me.MsuTrack.AddPcmTrack(Me.MsuTrackAltTmp)
        Else

            ' Update existing track

            ' If the AltNumber was changed (Already checked if new AltNumber already exists)
            If Me.MsuTrackAltToEdit.AltNumber <> Me.MsuTrackAltTmp.AltNumber Then
                ' Change AltNumber and re-add to TrackAltDict to correct position 
                Call Me.MsuTrack.TrackAltDict.Remove(Me.MsuTrackAltToEdit.AltNumber)
                Me.MsuTrackAltToEdit.AltNumber = Me.MsuTrackAltTmp.AltNumber
                Call Me.MsuTrack.TrackAltDict.Add(Me.MsuTrackAltToEdit.AltNumber, Me.MsuTrackAltToEdit)
            End If

            Me.MsuTrackAltToEdit.Title = Me.MsuTrackAltTmp.Title
            Me.MsuTrackAltToEdit.AutoSwitchTrackNumbers = Me.MsuTrackAltTmp.AutoSwitchTrackNumbers
            If Me.ChangedLocation Then
                Me.MsuTrackAltToEdit.LocationAbsolute = Me.MsuTrackAltTmp.LocationAbsolute
                Call Me.MsuTrackAltToEdit.UpdateSavedLocation()
            End If
        End If

        Me.MsuSelButtonForFullPathEnabled = Me.MsuTrackAltTmp.FilePathExists
        Me.MsuSelButtonForLocationEnabled = Not Me.MsuSelButtonForFullPathEnabled

        RaiseEvent AppliedChanges(Me, Nothing)
    End Sub

    Private Sub Form_AppliedChanges(sender As Object, e As EventArgs)
        _Dirty = False
    End Sub

    Private Sub nudMsuTrackAltId_ValueChanged(sender As Object, e As EventArgs) Handles nudMsuTrackAltId.ValueChanged
        If Me.MsuTrackAltTmp Is Nothing Then Return
        Try
            Me.MsuTrackAltTmp.AltNumber = CUShort(Me.nudMsuTrackAltId.Value)
            RaiseEvent UserChangedValue(sender, e)
        Catch ex As System.OverflowException
            Me.nudMsuTrackAltId.Value = Me.MsuTrackAltTmp.AltNumber
        Catch ex As System.InvalidCastException
            Me.nudMsuTrackAltId.Value = Me.MsuTrackAltTmp.AltNumber
        End Try
    End Sub

    Private Sub txtMsuAltTitle_TextChanged(sender As Object, e As EventArgs) Handles txtMsuAltTitle.TextChanged
        If Me.MsuTrackAltTmp Is Nothing Then Return
        If String.IsNullOrWhiteSpace(Me.txtMsuAltTitle.Text) Then
            Me.MsuTrackAltTmp.Title = Nothing
        Else
            Me.MsuTrackAltTmp.Title = Me.txtMsuAltTitle.Text()
        End If
        RaiseEvent UserChangedValue(sender, e)
    End Sub

    Protected Overrides Sub Finalize()
        If Me.MsuTrackAltTmp IsNot Nothing Then
            Call Me.MsuTrackAltTmp.Dispose()
        End If
        MyBase.Finalize()
    End Sub

    Private Sub btnSelPathPcm_Click(sender As Object, e As EventArgs) Handles btnSelPathPcm.Click
        With Me.ofdPathPcm
            .Filter = $"MSU1 PCM Audio Track {MsuTrack.TrackNumber}|{Me.MsuTrack.FileName}"
            .Title = "Select the .PCM file Or the To use As an alternative version For Track " & MsuTrack.TrackNumber
            If String.IsNullOrEmpty(Me.MsuTrack.Title) Then
            Else
                .Title = $"{ .Title} ({MsuTrack.Title})"
            End If
            .ShowDialog(owner:=Me)
        End With
    End Sub

    Private Sub ofdPathPcm_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ofdPathPcm.FileOk
        If TypeOf sender IsNot OpenFileDialog Then
            e.Cancel = True
            Return
        End If
        Dim openFileDialog As OpenFileDialog = CType(sender, OpenFileDialog)

        Select Case openFileDialog.FileNames.Length
            Case 0
                e.Cancel = True
                Return
            Case 1
            Case Else
                e.Cancel = True
                Return
        End Select

        Dim pcmPath As String = openFileDialog.FileNames(0)

        If String.IsNullOrWhiteSpace(pcmPath) Then
            e.Cancel = True
            Return
        End If

        ' Check if the filename matches the specified filename
        Dim pcmFileName As String = System.IO.Path.GetFileName(pcmPath)

        If pcmFileName.Equals(value:=Me.MsuTrack.FileName, comparisonType:=StringComparison.OrdinalIgnoreCase) Then
        Else
            e.Cancel = True
            Return
        End If

        Dim locationAbsolute As String = System.IO.Path.GetDirectoryName(pcmPath)

        Call SetLocationAbsolute(locationAbsolute, sender, e)
    End Sub

    Private Sub btnSelLocationPcm_Click(sender As Object, objEA As EventArgs) Handles btnSelLocationPcm.Click
        With Me.fbdLocationPcm
ShowDialog:
            .SelectedPath = Constants.vbNullString
            .ShowDialog(owner:=Me)
            'MsgBox(.SelectedPath)

            Dim locationAbsolute As String = .SelectedPath

            Dim cEA As New System.ComponentModel.CancelEventArgs

            If String.IsNullOrWhiteSpace(locationAbsolute) Then
                Return
            End If

            Call SetLocationAbsolute(locationAbsolute, sender, cEA)

            If cEA.Cancel Then
                GoTo ShowDialog
            End If
        End With
    End Sub

    Private Sub SetLocationAbsolute(ByRef locationAbsolute As String, ByRef sender As Object, ByRef e As System.ComponentModel.CancelEventArgs)

        Dim locationAbsoluteSet = GetLocationAbsoluteToSet(locationAbsolute)

        If String.IsNullOrWhiteSpace(locationAbsoluteSet) Then
            e.Cancel = True
            Return
        End If

        Me.MsuTrackAltTmp.LocationAbsolute = locationAbsoluteSet

        Call Me.MsuTrackAltTmp.UpdateSavedLocation()
        Me.ChangedLocation = True

        If String.IsNullOrWhiteSpace(Me.MsuTrackAltTmp.Title) Then
            Me.MsuTrackAltTmp.Title = System.IO.Path.GetFileName(locationAbsolute)
        End If

        RaiseEvent UserChangedValue(sender, e)
    End Sub

    Private Function GetLocationAbsoluteToSet(ByRef locationAbsolute As String) As String
        If String.IsNullOrWhiteSpace(locationAbsolute) Then
            Return Constants.vbNullString
        End If

#If WINDOWS Then
        Dim locationAbsoluteRemLocUnc As String = Msu.MsuHelper.PathRemoveUncLocalPref(locationAbsolute)
        Dim msuTracksLocationRemLocUnc As String = Msu.MsuHelper.PathRemoveUncLocalPref(Me.MsuTracks.MsuLocation)
#Else
        Dim locationAbsoluteRemLocUnc As String = strLocationAbsolute
        Dim msuTracksLocationRemLocUnc As String = Me.MsuTracks.MsuLocation
#End If

        ' https://stackoverflow.com/a/66877177
        ' Get Relative Path of selected location compared to the MSU Directory
        Dim relativePathMsuLocation As String =
            System.IO.Path.GetRelativePath(
                relativeTo:=msuTracksLocationRemLocUnc,
                path:=locationAbsoluteRemLocUnc)

        Select Case relativePathMsuLocation.Length
            Case 0
                ' Should be impossible
                Throw New ApplicationException($"Relative Path between ""{msuTracksLocationRemLocUnc}"" And ""{locationAbsoluteRemLocUnc}"" empty?")
            Case 1
                If relativePathMsuLocation.Chars(0).CompareTo("."c) = 0 Then
                    ' Selected directory is MSU directory itlsef
                    Return Constants.vbNullString
                End If
        End Select

        If relativePathMsuLocation.Equals(value:=locationAbsoluteRemLocUnc, comparisonType:=StringComparison.OrdinalIgnoreCase) Then
            ' Selected "relative" path is not relative to MSU directory (Could be on different drive)
            Return locationAbsolute
        Else
#If WINDOWS Then
            If Msu.MsuHelper.PathHasUncLocalPref(Me.MsuTracks.MsuLocation) Then

                ' Add Local UNC Prefix from MSU directory to selected directory
                Return String.Concat(Strings.Left(Me.MsuTracks.MsuLocation, 4), locationAbsoluteRemLocUnc)
            Else
                ' Use selected directory without local UNC prefix
                Return locationAbsoluteRemLocUnc
            End If
#Else
            Return = locationAbsolute
#End If
        End If
    End Function

    Private Sub SetToolTips()
        With Me.ttpMsuTrackAltControl
            .SetToolTip(Me.lblAbsoluteLocation,
                        "Absolute path To the location where the .PCM file Is stored, If unused.")
            .SetToolTip(Me.txtAbsoluteLocation, .GetToolTip(Me.lblAbsoluteLocation))


            .SetToolTip(Me.lblRelativeLocation,
                        "Path relative To the MSU directory To the location where the .PCM file Is stored, If unused.")
            .SetToolTip(Me.txtRelativeLocation, .GetToolTip(Me.lblRelativeLocation))


            .SetToolTip(Me.lblFilePath,
                        "Full Filepath Of the .PCM file where it Is stored, If unused.")
            .SetToolTip(Me.txtFilePath, .GetToolTip(Me.lblFilePath))


            .SetToolTip(Me.lblAutoSwitch,
                        "If one Of these TrackIds Is being played back, this program will switch To this alt. track automatically." & System.Environment.NewLine &
                        "Mostly relevant For DKC3.")
            .SetToolTip(Me.txtAutoSwitch, .GetToolTip(Me.lblAutoSwitch))


            .SetToolTip(Me.lblMsuTrackAltId,
                        "Unique Id For this alt. track For TrackId " & Me.MsuTrack.TrackNumber & "."c)
            .SetToolTip(Me.nudMsuTrackAltId, .GetToolTip(Me.lblMsuTrackAltId))


            .SetToolTip(Me.lblMsuTitle,
                        "Text describing what this alt. track Is.")
            .SetToolTip(Me.txtMsuAltTitle, .GetToolTip(Me.lblMsuTitle))

            .SetToolTip(Me.txtMsuFileName,
                        "Fixed filename the alt. track must have.")
        End With
    End Sub

    Private Sub txtAutoSwitch_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtAutoSwitch.Validating
        Dim text As String = Strings.Trim(Me.txtAutoSwitch.Text)

        If String.IsNullOrWhiteSpace(text) Then
            If Me.MsuTrackAltTmp.AutoSwitchTrackNumbers IsNot Nothing Then
                Me.MsuTrackAltTmp.AutoSwitchTrackNumbers = Nothing
                RaiseEvent UserChangedValue(sender, e)
            End If
            Return
        End If

        ' If String only contains Numbers
        If System.Text.RegularExpressions.Regex.IsMatch(text, "^[0-9 ]+$") Then
            Try
                ' Try parsing single MSU Id
                Dim msuId As Byte = CByte(text)

                Dim autoSwitchTrackNumbers(0) As Byte

                autoSwitchTrackNumbers(0) = msuId
                Me.MsuTrackAltTmp.AutoSwitchTrackNumbers = autoSwitchTrackNumbers
                RaiseEvent UserChangedValue(sender, e)
                Return
            Catch ex As System.OverflowException
                e.Cancel = True
                Call MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=ex, icon:=MessageBoxIcon.Exclamation)
                Return
            Catch ex As System.InvalidCastException
                ' Ignore
            End Try
        End If

        Try
            Me.MsuTrackAltTmp.AutoSwitchTrackNumbersJson = text
        Catch ex As System.Exception
            e.Cancel = True
            Call MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=ex, icon:=MessageBoxIcon.Exclamation)
            Return
        End Try

        RaiseEvent UserChangedValue(sender, e)
    End Sub

    Private Function CheckForValidPcmFiles(ByRef filesCheck As String()) As String()
        Return Me.MsuTracks.CheckForValidPcmFiles(filesCheck:=filesCheck, trackNumberCheck:=Me.MsuTrack.TrackNumber)
    End Function

    Private Sub MsuTrackAltControl_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop
        If MsuSelButtonForFullPathEnabled Then
        Else
            Return
        End If

        Dim PcmFiles() As String = Me.CheckForValidPcmFiles(DirectCast(e.Data.GetData(DataFormats.FileDrop), String()))

        If PcmFiles Is Nothing OrElse PcmFiles.Length <> 1 Then
            Return ' No PCM files or multiple files
        End If

        Dim eCEA As New ComponentModel.CancelEventArgs

        Call Me.SetLocationAbsolute(
                locationAbsolute:=System.IO.Path.GetDirectoryName(PcmFiles(PcmFiles.GetLowerBound(0))),
                          sender:=sender,
                               e:=eCEA)
    End Sub

    Private Sub MsuTrackAltControl_DragEnter(sender As Object, e As DragEventArgs) Handles MyBase.DragEnter
        If MsuSelButtonForFullPathEnabled Then
        Else
            e.Effect = DragDropEffects.None
            Return
        End If
        Dim PcmFiles() As String = Me.CheckForValidPcmFiles(DirectCast(e.Data.GetData(DataFormats.FileDrop), String()))

        If PcmFiles Is Nothing OrElse PcmFiles.Length <> 1 Then
            e.Effect = DragDropEffects.None
            Return ' No PCM files or multiple files
        End If

        Dim locationAbsoluteSet = GetLocationAbsoluteToSet(System.IO.Path.GetDirectoryName(PcmFiles(PcmFiles.GetLowerBound(0))))

        If String.IsNullOrWhiteSpace(locationAbsoluteSet) Then
            e.Effect = DragDropEffects.None
            Return ' Path of PCM file is invalid (Probably in the main MSU direcory)
        End If

        e.Effect = DragDropEffects.All
    End Sub
End Class