Option Compare Binary
Option Explicit On
Option Strict On
Imports MsuAltSelect.Msu

Public Class MsuTracksSettingsControl
    Public Event AppliedChanges(sender As Object, e As EventArgs)
    Public Event UserChangedValue(sender As Object, e As EventArgs)
    Public Event FormDirty(sender As Object, e As EventArgs)

    Private _Dirty As Boolean
    Public Property Dirty As Boolean
        Get
            Return _Dirty
        End Get
        Set(value As Boolean)
            If value = _Dirty Then Return ' Unchanged

            If value Then
            Else
                Call LoadMsuTracksToEdit(Me.MsuTracksToEdit)
            End If

            _Dirty = value
        End Set
    End Property

    Private Property MsuTracksToEdit As Tracks.MsuTracks
    Private Property MsuTracksTmp As Tracks.MsuTracks

    Public Sub New()
        ' This call is required by the designer.
        Call Me.InitializeComponent()
    End Sub

    Public Sub New(ByRef msuTracksToEdit As Tracks.MsuTracks)

        ' This call is required by the designer.
        Call Me.InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call LoadMsuTracksToEdit(msuTracksToEdit)
    End Sub

    Public Sub LoadMsuTracksToEdit(ByRef msuTracksToEdit As Tracks.MsuTracks)
        Me.MsuTracksToEdit = msuTracksToEdit

        If Me.MsuTracksTmp IsNot Nothing Then
            Call Me.MsuTracksTmp.Dispose()
        End If

        ' Create temporary copy of the Settings Object to edit
        Me.MsuTracksTmp = New Tracks.MsuTracks(msuTracksToEdit.Logger, msuTracksToEdit.Settings) With {
            .MsuLocation = Me.MsuTracksToEdit.MsuLocation,
            .MsuName = Me.MsuTracksToEdit.MsuName,
            .PcmPrefix = Me.MsuTracksToEdit.PcmPrefix
        }

        Call RefreshTextFields()
    End Sub
    Private Sub Form_UserChangedValue(sender As Object, e As EventArgs)
        Call Me.RefreshTextFields()
        RaiseEvent FormDirty(sender, e)
    End Sub

    Protected Sub RefreshTextFields()

        Me.txtMsuLocation.Text = Me.MsuTracksTmp.MsuLocation
        Me.txtMsuName.Text = Me.MsuTracksTmp.MsuName
        Me.txtPcmPrefix.Text = Me.MsuTracksTmp.PcmPrefix

    End Sub

    Public Sub ApplyChanges()
        Call ApplyChanges(Nothing)
    End Sub

    Public Sub ApplyChanges(ByRef e As System.ComponentModel.CancelEventArgs)

        If String.IsNullOrWhiteSpace(Me.MsuTracksTmp.MsuName) Then
            Me.MsuTracksTmp.MsuName = Me.MsuTracksToEdit.MsuName
        End If
        If String.IsNullOrWhiteSpace(Me.MsuTracksTmp.PcmPrefix) Then
            Me.MsuTracksTmp.PcmPrefix = Me.MsuTracksToEdit.PcmPrefix
        End If

        If Me.MsuTracksTmp.MsuName.Equals(Me.MsuTracksTmp.PcmPrefix, StringComparison.OrdinalIgnoreCase) _
   AndAlso Me.MsuTracksToEdit.MsuName.Equals(Me.MsuTracksToEdit.PcmPrefix, StringComparison.OrdinalIgnoreCase) Then
            ' Old and new MsuName and PcmPrefix match

            If Me.MsuTracksTmp.MsuName.Equals(Me.MsuTracksToEdit.MsuName, StringComparison.OrdinalIgnoreCase) Then
            Else
                Dim getExistingMsuFiles = Me.MsuTracksToEdit.GetExistingMsuFiles
                Dim existingPcmFiles = Me.MsuTracksToEdit.GetExistingPcmTracks

                If getExistingMsuFiles.Length = 0 AndAlso existingPcmFiles.Length = 0 Then
                    ' No files to rename
                    Me.MsuTracksToEdit.MsuName = Me.MsuTracksTmp.MsuName
                    Me.MsuTracksToEdit.PcmPrefix = Me.MsuTracksTmp.PcmPrefix
                Else
                    Dim dialogResult As DialogResult =
                          MessageBox.Show(
                            owner:=Me,
                            text:=$"The name of the MSU and the prefix for the PCM files was changed from ""{Me.MsuTracksToEdit.MsuName}"" to ""{Me.MsuTracksTmp.MsuName}""{System.Environment.NewLine}" &
                                  $"Do you want to rename the files as well?",
                            caption:=My.Application.Info.AssemblyName,
                            buttons:=MessageBoxButtons.YesNoCancel,
                            icon:=MessageBoxIcon.Question)

                    Select Case dialogResult
                        Case DialogResult.Yes

                            Call Me.MsuTracksToEdit.RenameMsu(Me.MsuTracksTmp.MsuName)
                            Call Me.MsuTracksToEdit.RenamePcmFiles(Me.MsuTracksTmp.PcmPrefix)

                        Case DialogResult.No
                            Me.MsuTracksToEdit.MsuName = Me.MsuTracksTmp.MsuName
                            Me.MsuTracksToEdit.PcmPrefix = Me.MsuTracksTmp.PcmPrefix
                        Case Else 'DialogResult.Cancel

                            If e IsNot Nothing Then
                                e.Cancel = True
                            End If
                            Call Me.RefreshTextFields()
                            Return
                    End Select
                End If
            End If
        Else
            If Me.MsuTracksTmp.MsuName.Equals(Me.MsuTracksToEdit.MsuName, StringComparison.OrdinalIgnoreCase) Then
            Else
                Dim getExistingMsuFiles = Me.MsuTracksToEdit.GetExistingMsuFiles

                If getExistingMsuFiles.Length = 0 Then
                    ' No files to rename
                    Me.MsuTracksToEdit.MsuName = Me.MsuTracksTmp.MsuName
                Else
                    Dim dialogResult As DialogResult =
                          MessageBox.Show(
                            owner:=Me,
                            text:=$"The name of the MSU was changed from ""{Me.MsuTracksToEdit.MsuName}"" to ""{Me.MsuTracksTmp.MsuName}""{System.Environment.NewLine}" &
                                  $"Do you want to rename the files as well?{System.Environment.NewLine}" &
                                  $"(PCM files are not included)",
                            caption:=My.Application.Info.AssemblyName,
                            buttons:=MessageBoxButtons.YesNoCancel,
                            icon:=MessageBoxIcon.Question)

                    Select Case dialogResult
                        Case DialogResult.Yes

                            Call Me.MsuTracksToEdit.RenameMsu(Me.MsuTracksTmp.MsuName)

                        Case DialogResult.No

                            Me.MsuTracksToEdit.MsuName = Me.MsuTracksTmp.MsuName

                        Case Else 'DialogResult.Cancel

                            If e IsNot Nothing Then
                                e.Cancel = True
                            End If
                            Call Me.RefreshTextFields()
                            Return
                    End Select
                End If
            End If
            If Me.MsuTracksTmp.PcmPrefix.Equals(Me.MsuTracksToEdit.PcmPrefix, StringComparison.OrdinalIgnoreCase) Then
            Else
                Dim existingPcmFiles = Me.MsuTracksToEdit.GetExistingPcmTracks

                If existingPcmFiles.Length = 0 Then
                    ' No files to rename
                    Me.MsuTracksToEdit.PcmPrefix = Me.MsuTracksTmp.PcmPrefix
                Else
                    Dim dialogResult As DialogResult =
                          MessageBox.Show(
                            owner:=Me,
                            text:=$"The prefix for the PCM files was changed from ""{Me.MsuTracksToEdit.PcmPrefix}"" to ""{Me.MsuTracksTmp.PcmPrefix}""{System.Environment.NewLine}" &
                                  $"Do you want to rename the PCM files as well?",
                            caption:=My.Application.Info.AssemblyName,
                            buttons:=MessageBoxButtons.YesNoCancel,
                            icon:=MessageBoxIcon.Question)

                    Select Case dialogResult
                        Case DialogResult.Yes

                            Call Me.MsuTracksToEdit.RenamePcmFiles(Me.MsuTracksTmp.PcmPrefix)

                        Case DialogResult.No

                            Me.MsuTracksToEdit.PcmPrefix = Me.MsuTracksTmp.PcmPrefix
                        Case Else 'DialogResult.Cancel

                            If e IsNot Nothing Then
                                e.Cancel = True
                            End If
                            Call Me.RefreshTextFields()
                            Return
                    End Select
                End If
            End If
        End If

        If String.IsNullOrWhiteSpace(Me.MsuTracksTmp.MsuLocation) Then
            Me.MsuTracksTmp.MsuLocation = Me.MsuTracksToEdit.MsuLocation
        Else
            Me.MsuTracksToEdit.MsuLocation = Me.MsuTracksTmp.MsuLocation
        End If

        RaiseEvent AppliedChanges(Me, Nothing)
    End Sub

    Private Sub Form_AppliedChanges(sender As Object, e As EventArgs)
        Me.Dirty = False
    End Sub

    Protected Overrides Sub Finalize()
        If Me.MsuTracksTmp IsNot Nothing Then
            Call Me.MsuTracksTmp.Dispose()
            Me.MsuTracksTmp = Nothing
        End If
        MyBase.Finalize()
    End Sub

    Private Sub txtMsuLocation_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtMsuLocation.Validating
        Me.MsuTracksTmp.MsuLocation = Me.txtMsuLocation.Text.Trim()
        RaiseEvent UserChangedValue(sender, e)
    End Sub

    Private Sub txtMsuName_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtMsuName.Validating
        If Me.MsuTracksTmp.MsuName.Trim() = Me.MsuTracksTmp.PcmPrefix.Trim() Then
            Me.MsuTracksTmp.PcmPrefix = Me.txtMsuName.Text.Trim()
        End If
        Me.MsuTracksTmp.MsuName = Me.txtMsuName.Text.Trim()
        RaiseEvent UserChangedValue(sender, e)
    End Sub

    Private Sub txtPcmPrefix_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtPcmPrefix.Validating
        Me.MsuTracksTmp.PcmPrefix = Me.txtPcmPrefix.Text.Trim()
        RaiseEvent UserChangedValue(sender, e)
    End Sub

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call SetToolTips()
    End Sub

    Private Sub SetToolTips()
        With Me.ttpMsuSettings
            .SetToolTip(Me.lblMsuLocation,
                        "Absolute location of the MSU/ROM File.")
            .SetToolTip(Me.txtMsuLocation, .GetToolTip(Me.lblMsuLocation))

            .SetToolTip(Me.lblMsuName,
                        "Filename (without extension) of the MSU File and the ROM File.")
            .SetToolTip(Me.txtMsuName, .GetToolTip(Me.lblMsuName))

            .SetToolTip(Me.lblPcmPrefix,
                        "Prefix used for the PCM Files. Normally identical to the MSU/ROM Name.")
            .SetToolTip(Me.txtPcmPrefix, .GetToolTip(Me.lblPcmPrefix))
        End With
    End Sub

End Class