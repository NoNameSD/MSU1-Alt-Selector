Option Compare Binary
Option Explicit On
Option Strict On
Imports MsuAltSelect.Msu

Public Class MsuSettingsForm
    Public Sub New(MsuTracks As Tracks.MsuTracks, Settings As Msu.Settings.Settings)

        ' This call is required by the designer.
        Call Me.InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.MsuTracks = MsuTracks
        Me.Settings = Settings
    End Sub

    Private _MsuTracks As Tracks.MsuTracks
    Private _Settings As Msu.Settings.Settings

    Public Property MsuTracks As Tracks.MsuTracks
        Get
            Return _MsuTracks
        End Get
        Private Set(value As Tracks.MsuTracks)
            _MsuTracks = value
        End Set
    End Property

    Public Property Settings As Msu.Settings.Settings
        Get
            Return _Settings
        End Get
        Private Set(value As Msu.Settings.Settings)
            _Settings = value
        End Set
    End Property

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        If Me.ApplyChanges() Then
            Me.DialogResult = DialogResult.OK
        Else
            Me.DialogResult = DialogResult.None
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        Call Me.ApplyChanges()
        Me.DialogResult = DialogResult.None
    End Sub

    Private Function ApplyChanges() As Boolean
        Try
            If TypeOf Me.SettingsControl Is MsuSettingsControl Then

                Call CType(Me.SettingsControl, MsuSettingsControl).ApplyChanges()

            ElseIf TypeOf Me.SettingsControl Is MsuTracksSettingsControl Then

                Dim e As New System.ComponentModel.CancelEventArgs

                Call CType(Me.SettingsControl, MsuTracksSettingsControl).ApplyChanges(e)

                If e.Cancel = True Then Return False
            End If

            Return True
            'Catch ex As MsuException
        Catch ex As Exception
            Call Msu.Ex.MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=ex)
            Return False
        End Try
    End Function

    Private Sub frmMsuSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MyBase.Text =
            String.Concat(
                My.Application.Info.ProductName,
                " Settings")
    End Sub

    Private Sub tvSettings_BeforeSelect(sender As Object, e As TreeViewCancelEventArgs) Handles tvSettings.BeforeSelect
        Dim SettingsControlAnchor As AnchorStyles = Me.SettingsControl.Anchor
        Dim SettingsControlLocation As Drawing.Point = Me.SettingsControl.Location
        Dim SettingsControlName As String = Me.SettingsControl.Name
        Dim SettingsControlSize As Drawing.Size = Me.SettingsControl.Size
        Dim SettingsControlTabIndex As Integer = SettingsControl.TabIndex

        Try
            Dim SettingsControlNew As UserControl = Nothing
            Select Case e.Node.Name
                Case "Application"
                    SettingsControlNew = New MsuSettingsControl(Me.Settings)
                Case "CurrentConfig"

                    If Me.MsuTracks Is Nothing Then
                        e.Cancel = True
                        Return
                    End If

                    SettingsControlNew = New MsuTracksSettingsControl(Me.MsuTracks)
            End Select
            Call Me.SettingsControl.Dispose()
            Me.SettingsControl = SettingsControlNew
            Me.SettingsControl.Anchor = SettingsControlAnchor
            Me.SettingsControl.Location = SettingsControlLocation
            Me.SettingsControl.Name = SettingsControlName
            Me.SettingsControl.Size = SettingsControlSize
            Me.SettingsControl.TabIndex = SettingsControlTabIndex
            scSettings.Panel2.Controls.Add(Me.SettingsControl)
        Catch ex As System.Exception
            e.Cancel = True
            Call Msu.Ex.MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=ex)
        End Try
    End Sub
End Class