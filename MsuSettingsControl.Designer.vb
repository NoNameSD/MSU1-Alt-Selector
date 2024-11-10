<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MsuSettingsControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New ComponentModel.Container()
        lblMsuPcmPath = New Label()
        txtMsuPcmPath = New TextBox()
        btnSelPathMsuPcm = New Button()
        ttpMsuSettings = New ToolTip(Me.components)
        lblMsuTrackMainVersionTitle = New Label()
        txtMsuTrackMainVersionTitle = New TextBox()
        txtMsuTrackMainVersionLocation = New TextBox()
        lblMsuTrackMainVersionLocation = New Label()
        ctrlAutoSetDisplayOnlyTracksWithAlts = New CheckBox()
        ctrlAutoSetAutoSwitch = New CheckBox()
        ctrlDisplayLoopPointInHexadecimal = New CheckBox()
        ctrlSaveMsuLocation = New CheckBox()
        btnDownloadMsuPcm = New Button()
        ofdPathMsuPcm = New OpenFileDialog()
        lblLogEntries = New Label()
        nudLogEntries = New NumericUpDown()
        CType(nudLogEntries, ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        ' 
        ' lblMsuPcmPath
        ' 
        lblMsuPcmPath.AutoSize = True
        lblMsuPcmPath.Location = New Drawing.Point(3, 14)
        lblMsuPcmPath.Name = "lblMsuPcmPath"
        lblMsuPcmPath.Size = New Drawing.Size(118, 15)
        lblMsuPcmPath.TabIndex = 0
        lblMsuPcmPath.Text = "Path of MSUPCM++:"
        ' 
        ' txtMsuPcmPath
        ' 
        txtMsuPcmPath.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtMsuPcmPath.Location = New Drawing.Point(123, 11)
        txtMsuPcmPath.Name = "txtMsuPcmPath"
        txtMsuPcmPath.PlaceholderText = "...\msupcm.exe"
        txtMsuPcmPath.Size = New Drawing.Size(176, 23)
        txtMsuPcmPath.TabIndex = 1
        ' 
        ' btnSelPathMsuPcm
        ' 
        btnSelPathMsuPcm.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnSelPathMsuPcm.Location = New Drawing.Point(305, 10)
        btnSelPathMsuPcm.Name = "btnSelPathMsuPcm"
        btnSelPathMsuPcm.Size = New Drawing.Size(50, 25)
        btnSelPathMsuPcm.TabIndex = 2
        btnSelPathMsuPcm.Text = "Select"
        btnSelPathMsuPcm.UseVisualStyleBackColor = True
        ' 
        ' ttpMsuSettings
        ' 
        ttpMsuSettings.AutoPopDelay = Integer.MaxValue
        ttpMsuSettings.InitialDelay = 250
        ttpMsuSettings.ReshowDelay = 50
        ' 
        ' lblMsuTrackMainVersionTitle
        ' 
        lblMsuTrackMainVersionTitle.AutoSize = True
        lblMsuTrackMainVersionTitle.Location = New Drawing.Point(3, 43)
        lblMsuTrackMainVersionTitle.Name = "lblMsuTrackMainVersionTitle"
        lblMsuTrackMainVersionTitle.Size = New Drawing.Size(172, 15)
        lblMsuTrackMainVersionTitle.TabIndex = 4
        lblMsuTrackMainVersionTitle.Text = "Title for Main/Default alt. Track:"
        ' 
        ' txtMsuTrackMainVersionTitle
        ' 
        txtMsuTrackMainVersionTitle.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtMsuTrackMainVersionTitle.Location = New Drawing.Point(181, 40)
        txtMsuTrackMainVersionTitle.Name = "txtMsuTrackMainVersionTitle"
        txtMsuTrackMainVersionTitle.Size = New Drawing.Size(246, 23)
        txtMsuTrackMainVersionTitle.TabIndex = 5
        ' 
        ' txtMsuTrackMainVersionLocation
        ' 
        txtMsuTrackMainVersionLocation.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtMsuTrackMainVersionLocation.Location = New Drawing.Point(181, 69)
        txtMsuTrackMainVersionLocation.Name = "txtMsuTrackMainVersionLocation"
        txtMsuTrackMainVersionLocation.Size = New Drawing.Size(246, 23)
        txtMsuTrackMainVersionLocation.TabIndex = 7
        ' 
        ' lblMsuTrackMainVersionLocation
        ' 
        lblMsuTrackMainVersionLocation.AutoSize = True
        lblMsuTrackMainVersionLocation.Location = New Drawing.Point(3, 72)
        lblMsuTrackMainVersionLocation.Name = "lblMsuTrackMainVersionLocation"
        lblMsuTrackMainVersionLocation.Size = New Drawing.Size(174, 15)
        lblMsuTrackMainVersionLocation.TabIndex = 6
        lblMsuTrackMainVersionLocation.Text = "Path for Main/Default alt. Track:"
        ' 
        ' ctrlAutoSetDisplayOnlyTracksWithAlts
        ' 
        ctrlAutoSetDisplayOnlyTracksWithAlts.AutoSize = True
        ctrlAutoSetDisplayOnlyTracksWithAlts.CheckAlign = Drawing.ContentAlignment.MiddleRight
        ctrlAutoSetDisplayOnlyTracksWithAlts.Location = New Drawing.Point(2, 98)
        ctrlAutoSetDisplayOnlyTracksWithAlts.Margin = New Padding(2, 3, 3, 3)
        ctrlAutoSetDisplayOnlyTracksWithAlts.Name = "ctrlAutoSetDisplayOnlyTracksWithAlts"
        ctrlAutoSetDisplayOnlyTracksWithAlts.RightToLeft = RightToLeft.No
        ctrlAutoSetDisplayOnlyTracksWithAlts.Size = New Drawing.Size(298, 19)
        ctrlAutoSetDisplayOnlyTracksWithAlts.TabIndex = 8
        ctrlAutoSetDisplayOnlyTracksWithAlts.Text = "Hide tracks with no alternative tracks automatically:"
        ctrlAutoSetDisplayOnlyTracksWithAlts.UseVisualStyleBackColor = True
        ' 
        ' ctrlAutoSetAutoSwitch
        ' 
        ctrlAutoSetAutoSwitch.AutoSize = True
        ctrlAutoSetAutoSwitch.CheckAlign = Drawing.ContentAlignment.MiddleRight
        ctrlAutoSetAutoSwitch.Location = New Drawing.Point(2, 127)
        ctrlAutoSetAutoSwitch.Margin = New Padding(2, 3, 3, 3)
        ctrlAutoSetAutoSwitch.Name = "ctrlAutoSetAutoSwitch"
        ctrlAutoSetAutoSwitch.Size = New Drawing.Size(246, 19)
        ctrlAutoSetAutoSwitch.TabIndex = 9
        ctrlAutoSetAutoSwitch.Text = "Enable/Disable AutoSwitch automatically:"
        ctrlAutoSetAutoSwitch.UseVisualStyleBackColor = True
        ' 
        ' ctrlDisplayLoopPointInHexadecimal
        ' 
        ctrlDisplayLoopPointInHexadecimal.AutoSize = True
        ctrlDisplayLoopPointInHexadecimal.CheckAlign = Drawing.ContentAlignment.MiddleRight
        ctrlDisplayLoopPointInHexadecimal.Location = New Drawing.Point(2, 156)
        ctrlDisplayLoopPointInHexadecimal.Margin = New Padding(2, 3, 3, 3)
        ctrlDisplayLoopPointInHexadecimal.Name = "ctrlDisplayLoopPointInHexadecimal"
        ctrlDisplayLoopPointInHexadecimal.Size = New Drawing.Size(246, 19)
        ctrlDisplayLoopPointInHexadecimal.TabIndex = 9
        ctrlDisplayLoopPointInHexadecimal.Text = "Display loop point in Hexadecimal:"
        ctrlDisplayLoopPointInHexadecimal.UseVisualStyleBackColor = True
        ' 
        ' ctrlSaveMsuLocation
        ' 
        ctrlSaveMsuLocation.AutoSize = True
        ctrlSaveMsuLocation.CheckAlign = Drawing.ContentAlignment.MiddleRight
        ctrlSaveMsuLocation.Location = New Drawing.Point(2, 185)
        ctrlSaveMsuLocation.Margin = New Padding(2, 3, 3, 3)
        ctrlSaveMsuLocation.Name = "ctrlSaveMsuLocation"
        ctrlSaveMsuLocation.Size = New Drawing.Size(246, 19)
        ctrlSaveMsuLocation.TabIndex = 9
        ctrlSaveMsuLocation.Text = "Save location of msu in configuration:"
        ctrlSaveMsuLocation.UseVisualStyleBackColor = True
        ' 
        ' btnDownloadMsuPcm
        ' 
        btnDownloadMsuPcm.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnDownloadMsuPcm.Location = New Drawing.Point(358, 10)
        btnDownloadMsuPcm.Name = "btnDownloadMsuPcm"
        btnDownloadMsuPcm.Size = New Drawing.Size(70, 25)
        btnDownloadMsuPcm.TabIndex = 3
        btnDownloadMsuPcm.Text = "Download"
        btnDownloadMsuPcm.UseVisualStyleBackColor = True
        ' 
        ' ofdPathMsuPcm
        ' 
        ofdPathMsuPcm.Filter = "All Files|*.*|MSUPCM++|*msupcm.exe"
        ofdPathMsuPcm.FilterIndex = 2
        ofdPathMsuPcm.Title = "Select executable for msupcm++"
        ' 
        ' lblLogEntries
        ' 
        lblLogEntries.AutoSize = True
        lblLogEntries.Location = New Drawing.Point(3, 214)
        lblLogEntries.Name = "lblLogEntries"
        lblLogEntries.Size = New Drawing.Size(128, 15)
        lblLogEntries.TabIndex = 10
        lblLogEntries.Text = "Maximum Log-Entries:"
        ' 
        ' nudLogEntries
        ' 
        nudLogEntries.Location = New Drawing.Point(137, 214)
        nudLogEntries.Maximum = New Decimal(New Integer() {-1, 0, 0, 0})
        nudLogEntries.Name = "nudLogEntries"
        nudLogEntries.Size = New Drawing.Size(57, 23)
        nudLogEntries.TabIndex = 11
        nudLogEntries.TextAlign = HorizontalAlignment.Right
        nudLogEntries.Value = New Decimal(New Integer() {1000, 0, 0, 0})
        ' 
        ' MsuSettingsControl
        ' 
        Me.AutoScaleDimensions = New Drawing.SizeF(7F, 15F)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.Controls.Add(lblLogEntries)
        Me.Controls.Add(nudLogEntries)
        Me.Controls.Add(btnDownloadMsuPcm)
        Me.Controls.Add(ctrlAutoSetAutoSwitch)
        Me.Controls.Add(ctrlAutoSetDisplayOnlyTracksWithAlts)
        Me.Controls.Add(ctrlDisplayLoopPointInHexadecimal)
        Me.Controls.Add(ctrlSaveMsuLocation)
        Me.Controls.Add(txtMsuTrackMainVersionLocation)
        Me.Controls.Add(lblMsuTrackMainVersionLocation)
        Me.Controls.Add(txtMsuTrackMainVersionTitle)
        Me.Controls.Add(lblMsuTrackMainVersionTitle)
        Me.Controls.Add(btnSelPathMsuPcm)
        Me.Controls.Add(txtMsuPcmPath)
        Me.Controls.Add(lblMsuPcmPath)
        Me.Name = "MsuSettingsControl"
        Me.Size = New Drawing.Size(430, 244)
        CType(nudLogEntries, ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    'Friend WithEvents btnSelPathMsu As Button
    Friend WithEvents lblMsuPcmPath As Label
    Friend WithEvents txtMsuPcmPath As TextBox
    Friend WithEvents btnSelPathMsuPcm As Button
    Friend WithEvents ttpMsuSettings As ToolTip
    Friend WithEvents lblMsuTrackMainVersionTitle As Label
    Friend WithEvents txtMsuTrackMainVersionTitle As TextBox
    Friend WithEvents txtMsuTrackMainVersionLocation As TextBox
    Friend WithEvents lblMsuTrackMainVersionLocation As Label
    Friend WithEvents ctrlAutoSetDisplayOnlyTracksWithAlts As CheckBox
    Friend WithEvents ctrlAutoSetAutoSwitch As CheckBox
    Friend WithEvents ctrlDisplayLoopPointInHexadecimal As CheckBox
    Friend WithEvents ctrlSaveMsuLocation As CheckBox
    Friend WithEvents btnDownloadMsuPcm As Button
    Friend WithEvents ofdPathMsuPcm As OpenFileDialog
    Private WithEvents lblLogEntries As Label
    Private WithEvents nudLogEntries As NumericUpDown
End Class
