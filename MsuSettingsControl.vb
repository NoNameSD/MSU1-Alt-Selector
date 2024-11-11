Option Compare Binary
Option Explicit On
Option Strict On

Public Class MsuSettingsControl
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
                Call LoadSettingsToEdit(Me.SettingsToEdit)
            End If

            _Dirty = value
        End Set
    End Property

    Private Property SettingsToEdit As Msu.Settings.Settings
    Private Property SettingsTmp As Msu.Settings.Settings

    Public Sub New()
        ' This call is required by the designer.
        Call Me.InitializeComponent()
    End Sub

    Public Sub New(ByRef settingsToEdit As Msu.Settings.Settings)

        ' This call is required by the designer.
        Call Me.InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call LoadSettingsToEdit(settingsToEdit)
    End Sub

    Public Sub LoadSettingsToEdit(ByRef settingsToEdit As Msu.Settings.Settings)
        Me.SettingsToEdit = settingsToEdit

        If Me.SettingsTmp IsNot Nothing Then
            'Call Me.SettingsTmp.Dispose()
            SettingsTmp = Nothing
        End If

        ' Create temporary copy of the Settings Object to edit
        Me.SettingsTmp = New Msu.Settings.Settings

        Call Me.SettingsToEdit.CopyProperties(Me.SettingsTmp)

        'For Each [property] As System.ComponentModel.PropertyDescriptor In System.ComponentModel.TypeDescriptor.GetProperties(Me.SettingsToEdit)
        '    [property].SetValue(Me.SettingsTmp, [property].GetValue(Me.SettingsToEdit))
        'Next

        Call RefreshTextFields()
    End Sub
    Private Sub Form_UserChangedValue(sender As Object, e As EventArgs) Handles Me.UserChangedValue
        Call Me.RefreshTextFields()
        RaiseEvent FormDirty(sender, e)
    End Sub

    Protected Sub RefreshTextFields()

        Me.txtMsuPcmPath.Text = Me.SettingsTmp.AudioConversionSettings.MsuPcmPath
        Me.txtMsuTrackMainVersionTitle.Text = Me.SettingsTmp.TrackAltSettings.MsuTrackMainVersionTitle
        Me.txtMsuTrackMainVersionLocation.Text = Me.SettingsTmp.TrackAltSettings.MsuTrackMainVersionLocation
        Me.ctrlAutoSetDisplayOnlyTracksWithAlts.Checked = Me.SettingsTmp.TrackAltSettings.AutoSetDisplayOnlyTracksWithAlts
        Me.ctrlAutoSetAutoSwitch.Checked = Me.SettingsTmp.TrackAltSettings.AutoSetAutoSwitch
        Me.ctrlDisplayLoopPointInHexadecimal.Checked = Me.SettingsTmp.TrackAltSettings.DisplayLoopPointInHexadecimal
        Me.ctrlSaveMsuLocation.CheckState = Me.SettingsTmp.TrackAltSettings.SaveMsuLocation
        Me.ctrlSaveMsuLocation.Enabled = Me.ctrlSaveMsuLocation.CheckState <> CheckState.Indeterminate
        Me.ctrlSaveMsuLocationAuto.Checked = Me.ctrlSaveMsuLocation.CheckState = CheckState.Indeterminate
        Me.nudLogEntries.Value = Me.SettingsTmp.LoggerSettings.MaxEntries

    End Sub

    Public Sub ApplyChanges()
        Me.SettingsToEdit.AudioConversionSettings.MsuPcmPath = Me.SettingsTmp.AudioConversionSettings.MsuPcmPath

        If String.IsNullOrWhiteSpace(Me.SettingsToEdit.TrackAltSettings.MsuTrackMainVersionTitle) Then
            Me.SettingsTmp.TrackAltSettings.MsuTrackMainVersionTitle = Me.SettingsToEdit.TrackAltSettings.MsuTrackMainVersionTitle
        Else
            Me.SettingsToEdit.TrackAltSettings.MsuTrackMainVersionTitle = Me.SettingsTmp.TrackAltSettings.MsuTrackMainVersionTitle
        End If
        If String.IsNullOrWhiteSpace(Me.SettingsToEdit.TrackAltSettings.MsuTrackMainVersionLocation) Then
            Me.SettingsTmp.TrackAltSettings.MsuTrackMainVersionLocation = Me.SettingsToEdit.TrackAltSettings.MsuTrackMainVersionLocation
        Else
            Me.SettingsToEdit.TrackAltSettings.MsuTrackMainVersionLocation = Me.SettingsTmp.TrackAltSettings.MsuTrackMainVersionLocation
        End If

        Me.SettingsToEdit.TrackAltSettings.AutoSetDisplayOnlyTracksWithAlts = Me.SettingsTmp.TrackAltSettings.AutoSetDisplayOnlyTracksWithAlts
        Me.SettingsToEdit.TrackAltSettings.AutoSetAutoSwitch = Me.SettingsTmp.TrackAltSettings.AutoSetAutoSwitch

        Me.SettingsToEdit.TrackAltSettings.DisplayLoopPointInHexadecimal = Me.SettingsTmp.TrackAltSettings.DisplayLoopPointInHexadecimal
        Me.SettingsToEdit.TrackAltSettings.SaveMsuLocation = Me.SettingsTmp.TrackAltSettings.SaveMsuLocation

        Me.SettingsToEdit.LoggerSettings.MaxEntries = Me.SettingsTmp.LoggerSettings.MaxEntries

        RaiseEvent AppliedChanges(Me, Nothing)
    End Sub

    Private Sub Form_AppliedChanges(sender As Object, e As EventArgs) Handles Me.AppliedChanges
        Me.Dirty = False
        Call Me.SettingsToEdit.SaveToJson()
    End Sub

    Protected Overrides Sub Finalize()
        If Me.SettingsTmp IsNot Nothing Then
            'Call Me.MsuTrackAltTmp.Dispose()
            Me.SettingsTmp = Nothing
        End If
        MyBase.Finalize()
    End Sub

    Private Sub btnSelPathMsuPcm_Click(sender As Object, e As EventArgs) Handles btnSelPathMsuPcm.Click
        With Me.ofdPathMsuPcm
            .ShowDialog(owner:=Me)
        End With
    End Sub

    Private Sub ofdPathPcm_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ofdPathMsuPcm.FileOk
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

        Dim msuPcmPath As String = openFileDialog.FileNames(0)

        If String.IsNullOrWhiteSpace(msuPcmPath) Then
            e.Cancel = True
            Return
        End If

        Me.SettingsTmp.AudioConversionSettings.MsuPcmPath = msuPcmPath

        RaiseEvent UserChangedValue(sender, e)
    End Sub

    Private Sub txtMsuPcmPath_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtMsuPcmPath.Validating
        Me.SettingsTmp.AudioConversionSettings.MsuPcmPath = Me.txtMsuPcmPath.Text.Trim()
    End Sub

    Private Sub txtMsuPcmPath_Validated(sender As Object, e As EventArgs) Handles txtMsuPcmPath.Validated
        RaiseEvent UserChangedValue(sender, e)
    End Sub

    Private Sub txtMsuTrackMainVersionTitle_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtMsuTrackMainVersionTitle.Validating
        Me.SettingsTmp.TrackAltSettings.MsuTrackMainVersionTitle = Me.txtMsuTrackMainVersionTitle.Text.Trim()
    End Sub

    Private Sub txtMsuTrackMainVersionTitle_Validated(sender As Object, e As EventArgs) Handles txtMsuTrackMainVersionTitle.Validated
        RaiseEvent UserChangedValue(sender, e)
    End Sub

    Private Sub txtMsuTrackMainVersionLocation_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtMsuTrackMainVersionLocation.Validating
        Me.SettingsTmp.TrackAltSettings.MsuTrackMainVersionLocation = Me.txtMsuTrackMainVersionLocation.Text.Trim()
    End Sub

    Private Sub txtMsuTrackMainVersionLocation_Validated(sender As Object, e As EventArgs) Handles txtMsuTrackMainVersionLocation.Validated
        RaiseEvent UserChangedValue(sender, e)
    End Sub

    Private Sub ctrlAutoSetDisplayOnlyTracksWithAlts_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ctrlAutoSetDisplayOnlyTracksWithAlts.Validating
        Me.SettingsTmp.TrackAltSettings.AutoSetDisplayOnlyTracksWithAlts = Me.ctrlAutoSetDisplayOnlyTracksWithAlts.Checked()
    End Sub

    Private Sub ctrlAutoSetDisplayOnlyTracksWithAlts_Validated(sender As Object, e As EventArgs) Handles ctrlAutoSetDisplayOnlyTracksWithAlts.Validated
        RaiseEvent UserChangedValue(sender, e)
    End Sub

    Private Sub ctrlAutoSetAutoSwitch_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ctrlAutoSetAutoSwitch.Validating
        Me.SettingsTmp.TrackAltSettings.AutoSetAutoSwitch = Me.ctrlAutoSetAutoSwitch.Checked()
    End Sub
    Private Sub ctrlAutoSetAutoSwitch_Validated(sender As Object, e As EventArgs) Handles ctrlAutoSetAutoSwitch.Validated
        RaiseEvent UserChangedValue(sender, e)
    End Sub

    Private Sub ctrlDisplayLoopPointInHexadecimal_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ctrlDisplayLoopPointInHexadecimal.Validating
        Me.SettingsTmp.TrackAltSettings.DisplayLoopPointInHexadecimal = Me.ctrlDisplayLoopPointInHexadecimal.Checked()
    End Sub
    Private Sub ctrlDisplayLoopPointInHexadecimal_Validated(sender As Object, e As EventArgs) Handles ctrlDisplayLoopPointInHexadecimal.Validated
        RaiseEvent UserChangedValue(sender, e)
    End Sub

    Private Sub ctrlSaveMsuLocation_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ctrlSaveMsuLocation.Validating
        Me.SettingsTmp.TrackAltSettings.SaveMsuLocation = Me.ctrlSaveMsuLocation.CheckState
    End Sub
    Private Sub ctrlSaveMsuLocation_Validated(sender As Object, e As EventArgs) Handles ctrlSaveMsuLocation.Validated
        RaiseEvent UserChangedValue(sender, e)
    End Sub

    Private Sub ctrlSaveMsuLocationAuto_CheckedChanged(sender As Object, e As EventArgs) Handles ctrlSaveMsuLocationAuto.CheckedChanged
        If Me.ctrlSaveMsuLocationAuto.Checked Then
            Me.SettingsTmp.TrackAltSettings.SaveMsuLocation = CheckState.Indeterminate
        Else
            Me.SettingsTmp.TrackAltSettings.SaveMsuLocation = CheckState.Unchecked
        End If
        RaiseEvent UserChangedValue(sender, e)
    End Sub

    Private Sub nudLogEntries_ValueChanged(sender As Object, e As EventArgs) Handles nudLogEntries.ValueChanged
        If Me.SettingsTmp Is Nothing Then Return
        Me.SettingsTmp.LoggerSettings.MaxEntries = CUInt(nudLogEntries.Value)
    End Sub

    Private Sub nudLogEntries_Validated(sender As Object, e As EventArgs) Handles nudLogEntries.Validated
        RaiseEvent UserChangedValue(sender, e)
    End Sub

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call SetToolTips()
    End Sub

    Private Sub SetToolTips()
        With Me.ttpMsuSettings
            .SetToolTip(Me.lblMsuPcmPath,
                        "Path of the Application msupcm++.")
            .SetToolTip(Me.txtMsuPcmPath, .GetToolTip(Me.lblMsuPcmPath))

            .SetToolTip(Me.btnSelPathMsuPcm,
                        "Select the file msupcm.exe")

            .SetToolTip(Me.btnDownloadMsuPcm,
                        "Opens the GitHub Page for msupcm++. You can download the file msupcm.exe from the latest release.")

            .SetToolTip(Me.ctrlAutoSetDisplayOnlyTracksWithAlts,
                        "Automatically sets the flag ""Show only tracks with alts."" on the main form, if the loaded configuration contains at least one track, that has alternative versions.")

            .SetToolTip(Me.ctrlAutoSetAutoSwitch,
                        "Automatically enables automatic switching, if the loaded configuration contains at least one alt. track, that has data for automatic switching configured.")

            .SetToolTip(Me.ctrlDisplayLoopPointInHexadecimal,
                        "When displaying the loop point of the pcm files, the numbers are shown in hexadecimal instead of decimal.")

            .SetToolTip(Me.ctrlSaveMsuLocation,
                        "When saving a MSU config to JSON the folder path of the msu is saved." & System.Environment.NewLine &
                        "This allows putting the JSON config into another location than the msu itself.")

            .SetToolTip(Me.ctrlSaveMsuLocationAuto,
                        "Will automatically add the msu location to the saved JSON config, if that file is saved to a different folder than the msu location.")

            .SetToolTip(Me.lblMsuTrackMainVersionTitle,
                        "Title, that is used for the Main/Default Version of the MsuTrack, that is stored in the Main Folder.")
            .SetToolTip(Me.txtMsuTrackMainVersionTitle, .GetToolTip(Me.lblMsuTrackMainVersionTitle))

            .SetToolTip(Me.lblMsuTrackMainVersionLocation,
                        "Location, where the Main/Default Version of the MsuTrack will be stored, if it is switched out for another alt. Track.")
            .SetToolTip(Me.txtMsuTrackMainVersionLocation, .GetToolTip(Me.lblMsuTrackMainVersionLocation))

            .SetToolTip(Me.lblLogEntries,
            "Maximum amount of entries in the log on the main form. Larger values may decrease performance.")
            .SetToolTip(Me.nudLogEntries, .GetToolTip(Me.lblLogEntries))
        End With
    End Sub

    Private Sub btnDownloadMsuPcm_Click(sender As Object, e As EventArgs) Handles btnDownloadMsuPcm.Click
        Try
            Dim process As New System.Diagnostics.Process

            process.StartInfo.UseShellExecute = True
            process.StartInfo.FileName = "https://github.com/qwertymodo/msupcmplusplus/releases"
            Call process.Start()
        Catch ex As Exception
            Call Msu.Ex.MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=ex)
        End Try
    End Sub
End Class
