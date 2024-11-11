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
        Me.lblMsuPcmPath = New Label()
        Me.txtMsuPcmPath = New TextBox()
        Me.btnSelPathMsuPcm = New Button()
        Me.ttpMsuSettings = New ToolTip(Me.components)
        Me.lblMsuTrackMainVersionTitle = New Label()
        Me.txtMsuTrackMainVersionTitle = New TextBox()
        Me.txtMsuTrackMainVersionLocation = New TextBox()
        Me.lblMsuTrackMainVersionLocation = New Label()
        Me.ctrlAutoSetDisplayOnlyTracksWithAlts = New CheckBox()
        Me.ctrlAutoSetAutoSwitch = New CheckBox()
        Me.ctrlDisplayLoopPointInHexadecimal = New CheckBox()
        Me.ctrlSaveMsuLocation = New CheckBox()
        Me.btnDownloadMsuPcm = New Button()
        Me.ofdPathMsuPcm = New OpenFileDialog()
        Me.lblLogEntries = New Label()
        Me.nudLogEntries = New NumericUpDown()
        Me.ctrlSaveMsuLocationAuto = New CheckBox()
        CType(Me.nudLogEntries, ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        ' 
        ' lblMsuPcmPath
        ' 
        Me.lblMsuPcmPath.AutoSize = True
        Me.lblMsuPcmPath.Location = New System.Drawing.Point(3, 14)
        Me.lblMsuPcmPath.Name = "lblMsuPcmPath"
        Me.lblMsuPcmPath.Size = New System.Drawing.Size(118, 15)
        Me.lblMsuPcmPath.TabIndex = 0
        Me.lblMsuPcmPath.Text = "Path of MSUPCM++:"
        ' 
        ' txtMsuPcmPath
        ' 
        Me.txtMsuPcmPath.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Me.txtMsuPcmPath.Location = New System.Drawing.Point(123, 11)
        Me.txtMsuPcmPath.Name = "txtMsuPcmPath"
        Me.txtMsuPcmPath.PlaceholderText = "...\msupcm.exe"
        Me.txtMsuPcmPath.Size = New System.Drawing.Size(176, 23)
        Me.txtMsuPcmPath.TabIndex = 1
        ' 
        ' btnSelPathMsuPcm
        ' 
        Me.btnSelPathMsuPcm.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Me.btnSelPathMsuPcm.Location = New System.Drawing.Point(305, 10)
        Me.btnSelPathMsuPcm.Name = "btnSelPathMsuPcm"
        Me.btnSelPathMsuPcm.Size = New System.Drawing.Size(50, 25)
        Me.btnSelPathMsuPcm.TabIndex = 2
        Me.btnSelPathMsuPcm.Text = "Select"
        Me.btnSelPathMsuPcm.UseVisualStyleBackColor = True
        ' 
        ' ttpMsuSettings
        ' 
        Me.ttpMsuSettings.AutoPopDelay = Integer.MaxValue
        Me.ttpMsuSettings.InitialDelay = 250
        Me.ttpMsuSettings.ReshowDelay = 50
        ' 
        ' lblMsuTrackMainVersionTitle
        ' 
        Me.lblMsuTrackMainVersionTitle.AutoSize = True
        Me.lblMsuTrackMainVersionTitle.Location = New System.Drawing.Point(3, 43)
        Me.lblMsuTrackMainVersionTitle.Name = "lblMsuTrackMainVersionTitle"
        Me.lblMsuTrackMainVersionTitle.Size = New System.Drawing.Size(172, 15)
        Me.lblMsuTrackMainVersionTitle.TabIndex = 4
        Me.lblMsuTrackMainVersionTitle.Text = "Title for Main/Default alt. Track:"
        ' 
        ' txtMsuTrackMainVersionTitle
        ' 
        Me.txtMsuTrackMainVersionTitle.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Me.txtMsuTrackMainVersionTitle.Location = New System.Drawing.Point(181, 40)
        Me.txtMsuTrackMainVersionTitle.Name = "txtMsuTrackMainVersionTitle"
        Me.txtMsuTrackMainVersionTitle.Size = New System.Drawing.Size(246, 23)
        Me.txtMsuTrackMainVersionTitle.TabIndex = 5
        ' 
        ' txtMsuTrackMainVersionLocation
        ' 
        Me.txtMsuTrackMainVersionLocation.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Me.txtMsuTrackMainVersionLocation.Location = New System.Drawing.Point(181, 69)
        Me.txtMsuTrackMainVersionLocation.Name = "txtMsuTrackMainVersionLocation"
        Me.txtMsuTrackMainVersionLocation.Size = New System.Drawing.Size(246, 23)
        Me.txtMsuTrackMainVersionLocation.TabIndex = 7
        ' 
        ' lblMsuTrackMainVersionLocation
        ' 
        Me.lblMsuTrackMainVersionLocation.AutoSize = True
        Me.lblMsuTrackMainVersionLocation.Location = New System.Drawing.Point(3, 72)
        Me.lblMsuTrackMainVersionLocation.Name = "lblMsuTrackMainVersionLocation"
        Me.lblMsuTrackMainVersionLocation.Size = New System.Drawing.Size(174, 15)
        Me.lblMsuTrackMainVersionLocation.TabIndex = 6
        Me.lblMsuTrackMainVersionLocation.Text = "Path for Main/Default alt. Track:"
        ' 
        ' ctrlAutoSetDisplayOnlyTracksWithAlts
        ' 
        Me.ctrlAutoSetDisplayOnlyTracksWithAlts.AutoSize = True
        Me.ctrlAutoSetDisplayOnlyTracksWithAlts.CheckAlign = Drawing.ContentAlignment.MiddleRight
        Me.ctrlAutoSetDisplayOnlyTracksWithAlts.Location = New System.Drawing.Point(2, 98)
        Me.ctrlAutoSetDisplayOnlyTracksWithAlts.Margin = New Padding(2, 3, 3, 3)
        Me.ctrlAutoSetDisplayOnlyTracksWithAlts.Name = "ctrlAutoSetDisplayOnlyTracksWithAlts"
        Me.ctrlAutoSetDisplayOnlyTracksWithAlts.RightToLeft = RightToLeft.No
        Me.ctrlAutoSetDisplayOnlyTracksWithAlts.Size = New System.Drawing.Size(298, 19)
        Me.ctrlAutoSetDisplayOnlyTracksWithAlts.TabIndex = 8
        Me.ctrlAutoSetDisplayOnlyTracksWithAlts.Text = "Hide tracks with no alternative tracks automatically:"
        Me.ctrlAutoSetDisplayOnlyTracksWithAlts.UseVisualStyleBackColor = True
        ' 
        ' ctrlAutoSetAutoSwitch
        ' 
        Me.ctrlAutoSetAutoSwitch.AutoSize = True
        Me.ctrlAutoSetAutoSwitch.CheckAlign = Drawing.ContentAlignment.MiddleRight
        Me.ctrlAutoSetAutoSwitch.Location = New System.Drawing.Point(2, 127)
        Me.ctrlAutoSetAutoSwitch.Margin = New Padding(2, 3, 3, 3)
        Me.ctrlAutoSetAutoSwitch.Name = "ctrlAutoSetAutoSwitch"
        Me.ctrlAutoSetAutoSwitch.Size = New System.Drawing.Size(246, 19)
        Me.ctrlAutoSetAutoSwitch.TabIndex = 9
        Me.ctrlAutoSetAutoSwitch.Text = "Enable/Disable AutoSwitch automatically:"
        Me.ctrlAutoSetAutoSwitch.UseVisualStyleBackColor = True
        ' 
        ' ctrlDisplayLoopPointInHexadecimal
        ' 
        Me.ctrlDisplayLoopPointInHexadecimal.AutoSize = True
        Me.ctrlDisplayLoopPointInHexadecimal.CheckAlign = Drawing.ContentAlignment.MiddleRight
        Me.ctrlDisplayLoopPointInHexadecimal.Location = New System.Drawing.Point(2, 156)
        Me.ctrlDisplayLoopPointInHexadecimal.Margin = New Padding(2, 3, 3, 3)
        Me.ctrlDisplayLoopPointInHexadecimal.Name = "ctrlDisplayLoopPointInHexadecimal"
        Me.ctrlDisplayLoopPointInHexadecimal.Size = New System.Drawing.Size(210, 19)
        Me.ctrlDisplayLoopPointInHexadecimal.TabIndex = 9
        Me.ctrlDisplayLoopPointInHexadecimal.Text = "Display loop point in Hexadecimal:"
        Me.ctrlDisplayLoopPointInHexadecimal.UseVisualStyleBackColor = True
        ' 
        ' ctrlSaveMsuLocation
        ' 
        Me.ctrlSaveMsuLocation.AutoSize = True
        Me.ctrlSaveMsuLocation.CheckAlign = Drawing.ContentAlignment.MiddleRight
        Me.ctrlSaveMsuLocation.Location = New System.Drawing.Point(2, 185)
        Me.ctrlSaveMsuLocation.Margin = New Padding(2, 3, 3, 3)
        Me.ctrlSaveMsuLocation.Name = "ctrlSaveMsuLocation"
        Me.ctrlSaveMsuLocation.Size = New System.Drawing.Size(227, 19)
        Me.ctrlSaveMsuLocation.TabIndex = 9
        Me.ctrlSaveMsuLocation.Text = "Save location of msu in configuration:"
        Me.ctrlSaveMsuLocation.UseVisualStyleBackColor = True
        ' 
        ' btnDownloadMsuPcm
        ' 
        Me.btnDownloadMsuPcm.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Me.btnDownloadMsuPcm.Location = New System.Drawing.Point(358, 10)
        Me.btnDownloadMsuPcm.Name = "btnDownloadMsuPcm"
        Me.btnDownloadMsuPcm.Size = New System.Drawing.Size(70, 25)
        Me.btnDownloadMsuPcm.TabIndex = 3
        Me.btnDownloadMsuPcm.Text = "Download"
        Me.btnDownloadMsuPcm.UseVisualStyleBackColor = True
        ' 
        ' ofdPathMsuPcm
        ' 
        Me.ofdPathMsuPcm.Filter = "All Files|*.*|MSUPCM++|*msupcm.exe"
        Me.ofdPathMsuPcm.FilterIndex = 2
        Me.ofdPathMsuPcm.Title = "Select executable for msupcm++"
        ' 
        ' lblLogEntries
        ' 
        Me.lblLogEntries.AutoSize = True
        Me.lblLogEntries.Location = New System.Drawing.Point(3, 214)
        Me.lblLogEntries.Name = "lblLogEntries"
        Me.lblLogEntries.Size = New System.Drawing.Size(128, 15)
        Me.lblLogEntries.TabIndex = 10
        Me.lblLogEntries.Text = "Maximum Log-Entries:"
        ' 
        ' nudLogEntries
        ' 
        Me.nudLogEntries.Location = New System.Drawing.Point(137, 214)
        Me.nudLogEntries.Maximum = New Decimal(New Integer() {-1, 0, 0, 0})
        Me.nudLogEntries.Name = "nudLogEntries"
        Me.nudLogEntries.Size = New System.Drawing.Size(57, 23)
        Me.nudLogEntries.TabIndex = 11
        Me.nudLogEntries.TextAlign = HorizontalAlignment.Right
        Me.nudLogEntries.Value = New Decimal(New Integer() {1000, 0, 0, 0})
        ' 
        ' ctrlSaveMsuLocationAuto
        ' 
        Me.ctrlSaveMsuLocationAuto.AutoSize = True
        Me.ctrlSaveMsuLocationAuto.CheckAlign = Drawing.ContentAlignment.MiddleRight
        Me.ctrlSaveMsuLocationAuto.Location = New System.Drawing.Point(240, 185)
        Me.ctrlSaveMsuLocationAuto.Margin = New Padding(2, 3, 3, 3)
        Me.ctrlSaveMsuLocationAuto.Name = "ctrlSaveMsuLocationAuto"
        Me.ctrlSaveMsuLocationAuto.Size = New System.Drawing.Size(85, 19)
        Me.ctrlSaveMsuLocationAuto.TabIndex = 12
        Me.ctrlSaveMsuLocationAuto.Text = "Autodetect"
        Me.ctrlSaveMsuLocationAuto.UseVisualStyleBackColor = True
        ' 
        ' MsuSettingsControl
        ' 
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7F, 15F)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.Controls.Add(Me.ctrlSaveMsuLocationAuto)
        Me.Controls.Add(Me.lblLogEntries)
        Me.Controls.Add(Me.nudLogEntries)
        Me.Controls.Add(Me.btnDownloadMsuPcm)
        Me.Controls.Add(Me.ctrlAutoSetAutoSwitch)
        Me.Controls.Add(Me.ctrlAutoSetDisplayOnlyTracksWithAlts)
        Me.Controls.Add(Me.ctrlDisplayLoopPointInHexadecimal)
        Me.Controls.Add(Me.ctrlSaveMsuLocation)
        Me.Controls.Add(Me.txtMsuTrackMainVersionLocation)
        Me.Controls.Add(Me.lblMsuTrackMainVersionLocation)
        Me.Controls.Add(Me.txtMsuTrackMainVersionTitle)
        Me.Controls.Add(Me.lblMsuTrackMainVersionTitle)
        Me.Controls.Add(Me.btnSelPathMsuPcm)
        Me.Controls.Add(Me.txtMsuPcmPath)
        Me.Controls.Add(Me.lblMsuPcmPath)
        Me.Name = "MsuSettingsControl"
        Me.Size = New System.Drawing.Size(430, 244)
        CType(Me.nudLogEntries, ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents ctrlSaveMsuLocationAuto As CheckBox
End Class
