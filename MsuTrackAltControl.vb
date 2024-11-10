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

    Private Property LoopPoint As Nullable(Of UInt32)
    Private Property LoopPointConverted As Nullable(Of UInt32)
    Private Property LoopPointToEdit As Nullable(Of UInt32)
    Private Property LoopPointConvertedToEdit As Nullable(Of UInt32)

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

        If Me.MsuTrackAltTmp.FilePathWithNormalVersionSuffixExists Then

            Me.LoopPoint = MsuPcmFile.GetLoopPoint(Me.MsuTrackAltTmp.FilePathWithNormalVersionSuffix)

            If Me.MsuTrackAltTmp.FilePathExists Then
                Me.LoopPointConverted = MsuPcmFile.GetLoopPoint(Me.MsuTrackAltTmp.FilePath)
            End If

        ElseIf Me.MsuTrackAltTmp.FilePathExists Then

            Me.LoopPoint = MsuPcmFile.GetLoopPoint(Me.MsuTrackAltTmp.FilePath)

        ElseIf Me.MsuTrack.FilePathWithNormalVersionSuffixExists Then

            Me.LoopPoint = MsuPcmFile.GetLoopPoint(Me.MsuTrack.FilePathWithNormalVersionSuffix)

            If Me.MsuTrack.FilePathExists Then
                Me.LoopPointConverted = MsuPcmFile.GetLoopPoint(Me.MsuTrack.FilePath)
            End If

        ElseIf Me.MsuTrack.FilePathExists Then

            Me.LoopPoint = MsuPcmFile.GetLoopPoint(Me.MsuTrack.FilePath)

        End If

        Me.LoopPointToEdit = Me.LoopPoint
        Me.LoopPointConvertedToEdit = Me.LoopPointConverted

        If Me.ctrlBase10.Checked OrElse Me.ctrlBase16.Checked Then
        Else
            If Me.MsuTracks.Settings.TrackAltSettings.DisplayLoopPointInHexadecimal Then
                Me.ctrlBase16.Checked = True
            Else
                Me.ctrlBase10.Checked = True
            End If
        End If

        Call SetToolTips()
        Call RefreshTextFields()
    End Sub
    Private Sub Form_UserChangedValue(sender As Object, e As EventArgs) Handles Me.UserChangedValue
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

        Me.grpLoopPointBase.Enabled = Not (Me.LoopPointConvertedToEdit Is Nothing AndAlso Me.LoopPointConvertedToEdit Is Nothing)

        If Me.LoopPointToEdit Is Nothing Then
            Me.nudLoopPoint.Enabled = False
            Me.lblLoopPoint.Enabled = False
            Me.nudLoopPoint.Text = vbNullString
        Else
            Me.nudLoopPoint.Value = CDec(Me.LoopPointToEdit)
            Me.nudLoopPoint.Enabled = True
            Me.lblLoopPoint.Enabled = True
        End If

        If Me.LoopPointConvertedToEdit Is Nothing Then
            Me.nudLoopPointConv.Enabled = False
            Me.lblLoopPointConv.Enabled = False
            Me.nudLoopPointConv.Text = vbNullString
        Else
            Me.nudLoopPointConv.Value = CDec(Me.LoopPointConvertedToEdit)
            Me.nudLoopPointConv.Enabled = True
            Me.lblLoopPointConv.Enabled = True
        End If

    End Sub
    Public Sub ApplyChanges()
        Dim msuTrackAltExisting As MsuTrackAlt = Nothing

        ' Check for existing Track with this Id
        If Me.MsuTrack.TrackAltDict.TryGetValue(Me.MsuTrackAltTmp.AltNumber, msuTrackAltExisting) Then

            If msuTrackAltExisting IsNot Me.MsuTrackAltToEdit Then
                Throw New MsuTrackAltAlreadyExistsException(msuTrackAltExisting, MsuTrackAltAlreadyExistsException.ExceptionType.DuplicateId)
            End If
        End If

        If Me.MsuTrack.TrackAltDict.Count <> 0 Then
            msuTrackAltExisting = Me.MsuTrack.GetAltTrackByLocation(Me.MsuTrackAltTmp.LocationAbsolute)

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

            ' Update loop point of pcm file if changed
            If Me.LoopPoint IsNot Nothing AndAlso Me.LoopPointToEdit IsNot Nothing _
       AndAlso Me.LoopPoint <> Me.LoopPointToEdit Then

                Dim sFilePathToModify As String

                If Me.MsuTrackAltTmp.FilePathWithNormalVersionSuffixExists Then

                    sFilePathToModify = Me.MsuTrackAltTmp.FilePathWithNormalVersionSuffix

                ElseIf Me.MsuTrackAltTmp.FilePathExists Then

                    sFilePathToModify = Me.MsuTrackAltTmp.FilePath

                ElseIf Me.MsuTrack.FilePathWithNormalVersionSuffixExists Then

                    sFilePathToModify = Me.MsuTrack.FilePathWithNormalVersionSuffix

                ElseIf Me.MsuTrack.FilePathExists Then

                    sFilePathToModify = Me.MsuTrack.FilePath

                Else
                    Throw New Exception("Loop point has been modified, but no possible file to modify exists.")
                End If

                Dim sText As String =
                        "The loop point has been modified." & System.Environment.NewLine &
                        "Do you want to write the new loop point into the pcm file?" & System.Environment.NewLine &
                        System.Environment.NewLine &
                       $"Old Loop Point:{vbTab} {CUInt(Me.LoopPoint).ToString("X8")} Hex / {Me.LoopPoint} Dec" & System.Environment.NewLine &
                       $"New Loop Point:{vbTab} {CUInt(Me.LoopPointToEdit).ToString("X8")} Hex / {Me.LoopPointToEdit} Dec" & System.Environment.NewLine &
                        System.Environment.NewLine &
                       $"Path of pcm file to modify: {System.Environment.NewLine}{sFilePathToModify}"

                If System.Windows.Forms.MessageBox.Show(
                        owner:=Me,
                        text:=sText,
                        caption:="Loop point modified",
                        buttons:=MessageBoxButtons.YesNo,
                        icon:=MessageBoxIcon.Question
                    ) = DialogResult.Yes Then

                    MsuPcmFile.SetLoopPoint(sFilePathToModify, CUInt(Me.LoopPointToEdit))
                    Me.LoopPoint = Me.LoopPointToEdit
                End If
            End If

            ' Update loop point of the converted pcm file if changed
            If Me.LoopPointConverted IsNot Nothing AndAlso Me.LoopPointConvertedToEdit IsNot Nothing _
       AndAlso Me.LoopPointConverted <> Me.LoopPointConvertedToEdit Then

                Dim sFilePathToModify As String
                Const sExTxt As String = "Loop point of converted pcm file has been modified, but no possible file to modify exists."

                If Me.MsuTrackAltTmp.FilePathWithNormalVersionSuffixExists Then

                    If Me.MsuTrackAltTmp.FilePathExists Then
                        sFilePathToModify = Me.MsuTrackAltTmp.FilePath
                    Else
                        Throw New Exception(sExTxt)
                    End If

                ElseIf Me.MsuTrackAltTmp.FilePathExists Then

                    Throw New Exception(sExTxt)

                ElseIf Me.MsuTrack.FilePathWithNormalVersionSuffixExists Then

                    If Me.MsuTrack.FilePathExists Then
                        sFilePathToModify = Me.MsuTrack.FilePath
                    Else
                        Throw New Exception(sExTxt)
                    End If
                Else
                    Throw New Exception(sExTxt)
                End If

                Dim sText As String =
                    "The loop point of the converted file has been modified." & System.Environment.NewLine &
                    "Do you want to write the new loop point into the pcm file?" & System.Environment.NewLine &
                    System.Environment.NewLine &
                   $"Old Loop Point:{vbTab} {CUInt(Me.LoopPointConverted).ToString("X8")} Hex / {Me.LoopPointConverted} Dec" & System.Environment.NewLine &
                   $"New Loop Point:{vbTab} {CUInt(Me.LoopPointConvertedToEdit).ToString("X8")} Hex / {Me.LoopPointConvertedToEdit} Dec" & System.Environment.NewLine &
                    System.Environment.NewLine &
                   $"Path of pcm file to modify: {System.Environment.NewLine}{sFilePathToModify}"

                If System.Windows.Forms.MessageBox.Show(
                    owner:=Me,
                    text:=sText,
                    caption:="Loop point modified",
                    buttons:=MessageBoxButtons.YesNo,
                    icon:=MessageBoxIcon.Question
                ) = DialogResult.Yes Then

                    MsuPcmFile.SetLoopPoint(sFilePathToModify, CUInt(Me.LoopPointConvertedToEdit))
                    Me.LoopPointConverted = Me.LoopPointConvertedToEdit
                End If
            End If
        End If

        Me.MsuSelButtonForFullPathEnabled = Me.MsuTrackAltTmp.FilePathExists
        Me.MsuSelButtonForLocationEnabled = Not Me.MsuSelButtonForFullPathEnabled

        RaiseEvent AppliedChanges(Me, Nothing)
    End Sub

    Private Sub Form_AppliedChanges(sender As Object, e As EventArgs) Handles Me.AppliedChanges
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


    Private Sub nudLoopPoint_ValueChanged(sender As Object, e As EventArgs) Handles nudLoopPoint.ValueChanged
        If Me.MsuTrackAltTmp Is Nothing Then Return
        Try
            Me.LoopPointToEdit = CUInt(Me.nudLoopPoint.Value)
            RaiseEvent UserChangedValue(sender, e)
        Catch ex As System.OverflowException
            Stop
            Me.nudLoopPoint.Value = CDec(Me.LoopPointToEdit)
        Catch ex As System.InvalidCastException
            Stop
            Me.nudLoopPoint.Value = CDec(Me.LoopPointToEdit)
        End Try
    End Sub

    Private Sub nudLoopPointConv_ValueChanged(sender As Object, e As EventArgs) Handles nudLoopPointConv.ValueChanged
        If Me.MsuTrackAltTmp Is Nothing Then Return
        Try
            Me.LoopPointConvertedToEdit = CUInt(Me.nudLoopPointConv.Value)
            RaiseEvent UserChangedValue(sender, e)
        Catch ex As System.OverflowException
            Me.nudLoopPointConv.Value = CDec(Me.LoopPointConvertedToEdit)
        Catch ex As System.InvalidCastException
            Me.nudLoopPointConv.Value = CDec(Me.LoopPointConvertedToEdit)
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
                        "If one of these TrackIds Is being played back, this program will switch To this alt. track automatically." & System.Environment.NewLine &
                        "Mostly relevant For DKC3.")
            .SetToolTip(Me.txtAutoSwitch, .GetToolTip(Me.lblAutoSwitch))


            .SetToolTip(Me.lblMsuTrackAltId,
                        "Unique Id for this alt. track For TrackId " & Me.MsuTrack.TrackNumber & "."c)
            .SetToolTip(Me.nudMsuTrackAltId, .GetToolTip(Me.lblMsuTrackAltId))


            .SetToolTip(Me.lblMsuTitle,
                        "Text describing what this alt. track Is.")
            .SetToolTip(Me.txtMsuAltTitle, .GetToolTip(Me.lblMsuTitle))

            .SetToolTip(Me.txtMsuFileName,
                        "Fixed filename the alt. track must have.")

            .SetToolTip(Me.lblLoopPoint,
                        "Loop point in samples of this track.")
            .SetToolTip(Me.nudLoopPoint, .GetToolTip(Me.lblLoopPoint))

            .SetToolTip(Me.lblLoopPointConv,
                        "Loop point in samples of the converted version of this track.")
            .SetToolTip(Me.nudLoopPointConv, .GetToolTip(Me.lblLoopPointConv))

            .SetToolTip(Me.grpLoopPointBase,
            "Switches between Decimal and Hexadecimal of the NumericUpDown for the loop point." & System.Environment.NewLine _
            & System.Environment.NewLine &
            "Note that .NET has the tendency to reset Hexadecimal values larger than 7FFFFFFF to 0.")
            .SetToolTip(Me.ctrlBase10, .GetToolTip(Me.grpLoopPointBase))
            .SetToolTip(Me.ctrlBase16, .GetToolTip(Me.grpLoopPointBase))
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

    Private Sub ctrlBase10or16_CheckedChanged(sender As Object, e As EventArgs) Handles ctrlBase10.CheckedChanged, ctrlBase16.CheckedChanged
        Me.nudLoopPoint.Hexadecimal = Me.ctrlBase16.Checked
        Me.nudLoopPointConv.Hexadecimal = Me.ctrlBase16.Checked
    End Sub

    Private Sub btnLoopPointToMax_Click(sender As Object, e As EventArgs) Handles btnLoopPointToMax.Click
        Me.LoopPointToEdit = UInt32.MaxValue
        Call RefreshTextFields()
    End Sub

    Private Sub btnLoopPointConvToMax_Click(sender As Object, e As EventArgs) Handles btnLoopPointConvToMax.Click
        Me.LoopPointConvertedToEdit = UInt32.MaxValue
        Call RefreshTextFields()
    End Sub

    Private Sub btnLoopPointReset_Click(sender As Object, e As EventArgs) Handles btnLoopPointReset.Click
        Me.LoopPointToEdit = Me.LoopPoint
        Call RefreshTextFields()
    End Sub

    Private Sub btnLoopPointConvReset_Click(sender As Object, e As EventArgs) Handles btnLoopPointConvReset.Click
        Me.LoopPointConvertedToEdit = Me.LoopPointConverted
        Call RefreshTextFields()
    End Sub


End Class