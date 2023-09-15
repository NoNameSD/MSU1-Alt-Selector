<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MsuTrackAltControl
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
        ofdPathPcm = New OpenFileDialog()
        lblMsuTitle = New Label()
        txtMsuAltTitle = New TextBox()
        lblMsuTrackAltId = New Label()
        txtMsuFileName = New TextBox()
        nudMsuTrackAltId = New NumericUpDown()
        txtRelativeLocation = New TextBox()
        lblRelativeLocation = New Label()
        txtAbsoluteLocation = New TextBox()
        lblAbsoluteLocation = New Label()
        txtFilePath = New TextBox()
        lblFilePath = New Label()
        btnSelPathPcm = New Button()
        btnSelLocationPcm = New Button()
        fbdLocationPcm = New FolderBrowserDialog()
        txtAutoSwitch = New TextBox()
        lblAutoSwitch = New Label()
        ttpMsuTrackAltControl = New ToolTip(Me.components)
        CType(nudMsuTrackAltId, ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        ' 
        ' ofdPathPcm
        ' 
        ofdPathPcm.Filter = "All Files|*.*|MSU1 PCM Audio Track|*.pcm"
        ofdPathPcm.FilterIndex = 2
        ofdPathPcm.Title = "Select the .PCM file or the to use as an alternative version"
        ' 
        ' lblMsuTitle
        ' 
        lblMsuTitle.AutoSize = True
        lblMsuTitle.Location = New Drawing.Point(3, 14)
        lblMsuTitle.Name = "lblMsuTitle"
        lblMsuTitle.Size = New Drawing.Size(32, 15)
        lblMsuTitle.TabIndex = 0
        lblMsuTitle.Text = "Title:"
        ' 
        ' txtMsuAltTitle
        ' 
        txtMsuAltTitle.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtMsuAltTitle.Location = New Drawing.Point(41, 11)
        txtMsuAltTitle.Name = "txtMsuAltTitle"
        txtMsuAltTitle.Size = New Drawing.Size(256, 23)
        txtMsuAltTitle.TabIndex = 1
        ' 
        ' lblMsuTrackAltId
        ' 
        lblMsuTrackAltId.AutoSize = True
        lblMsuTrackAltId.Location = New Drawing.Point(3, 43)
        lblMsuTrackAltId.Name = "lblMsuTrackAltId"
        lblMsuTrackAltId.Size = New Drawing.Size(101, 15)
        lblMsuTrackAltId.TabIndex = 2
        lblMsuTrackAltId.Text = "Track alt. number:"
        ' 
        ' txtMsuFileName
        ' 
        txtMsuFileName.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtMsuFileName.Location = New Drawing.Point(166, 40)
        txtMsuFileName.MaxLength = 3
        txtMsuFileName.Name = "txtMsuFileName"
        txtMsuFileName.ReadOnly = True
        txtMsuFileName.Size = New Drawing.Size(131, 23)
        txtMsuFileName.TabIndex = 4
        ' 
        ' nudMsuTrackAltId
        ' 
        nudMsuTrackAltId.Location = New Drawing.Point(110, 40)
        nudMsuTrackAltId.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        nudMsuTrackAltId.Name = "nudMsuTrackAltId"
        nudMsuTrackAltId.Size = New Drawing.Size(50, 23)
        nudMsuTrackAltId.TabIndex = 3
        nudMsuTrackAltId.Value = New Decimal(New Integer() {65535, 0, 0, 0})
        ' 
        ' txtRelativeLocation
        ' 
        txtRelativeLocation.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtRelativeLocation.Location = New Drawing.Point(117, 69)
        txtRelativeLocation.Name = "txtRelativeLocation"
        txtRelativeLocation.ReadOnly = True
        txtRelativeLocation.Size = New Drawing.Size(180, 23)
        txtRelativeLocation.TabIndex = 6
        ' 
        ' lblRelativeLocation
        ' 
        lblRelativeLocation.AutoSize = True
        lblRelativeLocation.Location = New Drawing.Point(3, 72)
        lblRelativeLocation.Name = "lblRelativeLocation"
        lblRelativeLocation.Size = New Drawing.Size(108, 15)
        lblRelativeLocation.TabIndex = 5
        lblRelativeLocation.Text = "Location (Relative):"
        ' 
        ' txtAbsoluteLocation
        ' 
        txtAbsoluteLocation.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtAbsoluteLocation.Location = New Drawing.Point(117, 98)
        txtAbsoluteLocation.Name = "txtAbsoluteLocation"
        txtAbsoluteLocation.ReadOnly = True
        txtAbsoluteLocation.Size = New Drawing.Size(180, 23)
        txtAbsoluteLocation.TabIndex = 8
        ' 
        ' lblAbsoluteLocation
        ' 
        lblAbsoluteLocation.AutoSize = True
        lblAbsoluteLocation.Location = New Drawing.Point(3, 101)
        lblAbsoluteLocation.Name = "lblAbsoluteLocation"
        lblAbsoluteLocation.Size = New Drawing.Size(114, 15)
        lblAbsoluteLocation.TabIndex = 7
        lblAbsoluteLocation.Text = "Location (Absolute):"
        ' 
        ' txtFilePath
        ' 
        txtFilePath.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtFilePath.Location = New Drawing.Point(117, 127)
        txtFilePath.Name = "txtFilePath"
        txtFilePath.ReadOnly = True
        txtFilePath.Size = New Drawing.Size(180, 23)
        txtFilePath.TabIndex = 10
        ' 
        ' lblFilePath
        ' 
        lblFilePath.AutoSize = True
        lblFilePath.Location = New Drawing.Point(3, 130)
        lblFilePath.Name = "lblFilePath"
        lblFilePath.Size = New Drawing.Size(113, 15)
        lblFilePath.TabIndex = 9
        lblFilePath.Text = "File path (Absolute):"
        ' 
        ' btnSelPathPcm
        ' 
        btnSelPathPcm.Location = New Drawing.Point(3, 185)
        btnSelPathPcm.Name = "btnSelPathPcm"
        btnSelPathPcm.Size = New Drawing.Size(145, 25)
        btnSelPathPcm.TabIndex = 13
        btnSelPathPcm.Text = "Select alt. Track"
        btnSelPathPcm.UseVisualStyleBackColor = True
        ' 
        ' btnSelLocationPcm
        ' 
        btnSelLocationPcm.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnSelLocationPcm.Location = New Drawing.Point(152, 185)
        btnSelLocationPcm.Name = "btnSelLocationPcm"
        btnSelLocationPcm.Size = New Drawing.Size(145, 25)
        btnSelLocationPcm.TabIndex = 14
        btnSelLocationPcm.Text = "Select alt. Track Location"
        btnSelLocationPcm.UseVisualStyleBackColor = True
        ' 
        ' fbdLocationPcm
        ' 
        fbdLocationPcm.Description = "Select the folder the currently used .PCM should be stored in."
        fbdLocationPcm.RootFolder = Environment.SpecialFolder.Recent
        fbdLocationPcm.Tag = ""
        ' 
        ' txtAutoSwitch
        ' 
        txtAutoSwitch.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtAutoSwitch.Location = New Drawing.Point(117, 156)
        txtAutoSwitch.Name = "txtAutoSwitch"
        txtAutoSwitch.Size = New Drawing.Size(180, 23)
        txtAutoSwitch.TabIndex = 12
        ' 
        ' lblAutoSwitch
        ' 
        lblAutoSwitch.AutoSize = True
        lblAutoSwitch.Location = New Drawing.Point(3, 159)
        lblAutoSwitch.Name = "lblAutoSwitch"
        lblAutoSwitch.Size = New Drawing.Size(116, 15)
        lblAutoSwitch.TabIndex = 11
        lblAutoSwitch.Text = "Auto switch Track(s):"
        ' 
        ' ttpMsuTrackAltControl
        ' 
        ttpMsuTrackAltControl.AutoPopDelay = Integer.MaxValue
        ttpMsuTrackAltControl.InitialDelay = 250
        ttpMsuTrackAltControl.ReshowDelay = 50
        ' 
        ' MsuTrackAltControl
        ' 
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New Drawing.SizeF(7F, 15F)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.Controls.Add(txtAutoSwitch)
        Me.Controls.Add(lblAutoSwitch)
        Me.Controls.Add(btnSelLocationPcm)
        Me.Controls.Add(btnSelPathPcm)
        Me.Controls.Add(txtFilePath)
        Me.Controls.Add(lblFilePath)
        Me.Controls.Add(txtAbsoluteLocation)
        Me.Controls.Add(lblAbsoluteLocation)
        Me.Controls.Add(txtRelativeLocation)
        Me.Controls.Add(lblRelativeLocation)
        Me.Controls.Add(nudMsuTrackAltId)
        Me.Controls.Add(txtMsuFileName)
        Me.Controls.Add(lblMsuTrackAltId)
        Me.Controls.Add(txtMsuAltTitle)
        Me.Controls.Add(lblMsuTitle)
        Me.Name = "MsuTrackAltControl"
        Me.Size = New Drawing.Size(300, 212)
        CType(nudMsuTrackAltId, ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    'Friend WithEvents btnSelPathMsu As Button
    Friend WithEvents lblMsuTitle As Label
    Friend WithEvents txtMsuAltTitle As TextBox
    Friend WithEvents lblMsuTrackAltId As Label
    Friend WithEvents txtMsuFileName As TextBox
    Friend WithEvents nudMsuTrackAltId As NumericUpDown
    Friend WithEvents txtRelativeLocation As TextBox
    Friend WithEvents lblRelativeLocation As Label
    Friend WithEvents txtAbsoluteLocation As TextBox
    Friend WithEvents lblAbsoluteLocation As Label
    Friend WithEvents txtFilePath As TextBox
    Friend WithEvents lblFilePath As Label
    Friend WithEvents ofdPathPcm As OpenFileDialog
    Friend WithEvents btnSelPathPcm As Button
    Friend WithEvents btnSelLocationPcm As Button
    Friend WithEvents fbdLocationPcm As FolderBrowserDialog
    Friend WithEvents txtAutoSwitch As TextBox
    Friend WithEvents lblAutoSwitch As Label
    Friend WithEvents ttpMsuTrackAltControl As ToolTip
End Class
