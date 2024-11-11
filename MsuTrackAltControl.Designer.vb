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
        Me.ofdPathPcm = New OpenFileDialog()
        Me.lblMsuTitle = New Label()
        Me.txtMsuAltTitle = New TextBox()
        Me.lblMsuTrackAltId = New Label()
        Me.txtMsuFileName = New TextBox()
        Me.nudMsuTrackAltId = New NumericUpDown()
        Me.txtRelativeLocation = New TextBox()
        Me.lblRelativeLocation = New Label()
        Me.txtAbsoluteLocation = New TextBox()
        Me.lblAbsoluteLocation = New Label()
        Me.txtFilePath = New TextBox()
        Me.lblFilePath = New Label()
        Me.btnSelPathPcm = New Button()
        Me.btnSelLocationPcm = New Button()
        Me.fbdLocationPcm = New FolderBrowserDialog()
        Me.txtAutoSwitch = New TextBox()
        Me.lblAutoSwitch = New Label()
        Me.ttpMsuTrackAltControl = New ToolTip(Me.components)
        Me.lblLoopPoint = New Label()
        Me.lblLoopPointConv = New Label()
        Me.nudLoopPoint = New NumericUpDown()
        Me.nudLoopPointConv = New NumericUpDown()
        Me.grpLoopPointBase = New GroupBox()
        Me.ctrlBase16 = New RadioButton()
        Me.ctrlBase10 = New RadioButton()
        Me.btnLoopPointToMax = New Button()
        Me.btnLoopPointConvToMax = New Button()
        Me.btnLoopPointReset = New Button()
        Me.btnLoopPointConvReset = New Button()
        CType(Me.nudMsuTrackAltId, ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudLoopPoint, ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudLoopPointConv, ComponentModel.ISupportInitialize).BeginInit()
        Me.grpLoopPointBase.SuspendLayout()
        Me.SuspendLayout()
        ' 
        ' ofdPathPcm
        ' 
        Me.ofdPathPcm.Filter = "All Files|*.*|MSU1 PCM Audio Track|*.pcm"
        Me.ofdPathPcm.FilterIndex = 2
        Me.ofdPathPcm.Title = "Select the .PCM file or the to use as an alternative version"
        ' 
        ' lblMsuTitle
        ' 
        Me.lblMsuTitle.AutoSize = True
        Me.lblMsuTitle.Location = New System.Drawing.Point(3, 14)
        Me.lblMsuTitle.Name = "lblMsuTitle"
        Me.lblMsuTitle.Size = New System.Drawing.Size(32, 15)
        Me.lblMsuTitle.TabIndex = 0
        Me.lblMsuTitle.Text = "Title:"
        ' 
        ' txtMsuAltTitle
        ' 
        Me.txtMsuAltTitle.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Me.txtMsuAltTitle.Location = New System.Drawing.Point(41, 11)
        Me.txtMsuAltTitle.Name = "txtMsuAltTitle"
        Me.txtMsuAltTitle.Size = New System.Drawing.Size(256, 23)
        Me.txtMsuAltTitle.TabIndex = 1
        ' 
        ' lblMsuTrackAltId
        ' 
        Me.lblMsuTrackAltId.AutoSize = True
        Me.lblMsuTrackAltId.Location = New System.Drawing.Point(3, 43)
        Me.lblMsuTrackAltId.Name = "lblMsuTrackAltId"
        Me.lblMsuTrackAltId.Size = New System.Drawing.Size(101, 15)
        Me.lblMsuTrackAltId.TabIndex = 2
        Me.lblMsuTrackAltId.Text = "Track alt. number:"
        ' 
        ' txtMsuFileName
        ' 
        Me.txtMsuFileName.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Me.txtMsuFileName.Location = New System.Drawing.Point(166, 40)
        Me.txtMsuFileName.MaxLength = 3
        Me.txtMsuFileName.Name = "txtMsuFileName"
        Me.txtMsuFileName.ReadOnly = True
        Me.txtMsuFileName.Size = New System.Drawing.Size(131, 23)
        Me.txtMsuFileName.TabIndex = 4
        ' 
        ' nudMsuTrackAltId
        ' 
        Me.nudMsuTrackAltId.Location = New System.Drawing.Point(110, 40)
        Me.nudMsuTrackAltId.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nudMsuTrackAltId.Name = "nudMsuTrackAltId"
        Me.nudMsuTrackAltId.Size = New System.Drawing.Size(50, 23)
        Me.nudMsuTrackAltId.TabIndex = 3
        Me.nudMsuTrackAltId.Value = New Decimal(New Integer() {65535, 0, 0, 0})
        ' 
        ' txtRelativeLocation
        ' 
        Me.txtRelativeLocation.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Me.txtRelativeLocation.Location = New System.Drawing.Point(117, 69)
        Me.txtRelativeLocation.Name = "txtRelativeLocation"
        Me.txtRelativeLocation.ReadOnly = True
        Me.txtRelativeLocation.Size = New System.Drawing.Size(180, 23)
        Me.txtRelativeLocation.TabIndex = 6
        ' 
        ' lblRelativeLocation
        ' 
        Me.lblRelativeLocation.AutoSize = True
        Me.lblRelativeLocation.Location = New System.Drawing.Point(3, 72)
        Me.lblRelativeLocation.Name = "lblRelativeLocation"
        Me.lblRelativeLocation.Size = New System.Drawing.Size(108, 15)
        Me.lblRelativeLocation.TabIndex = 5
        Me.lblRelativeLocation.Text = "Location (Relative):"
        ' 
        ' txtAbsoluteLocation
        ' 
        Me.txtAbsoluteLocation.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Me.txtAbsoluteLocation.Location = New System.Drawing.Point(117, 98)
        Me.txtAbsoluteLocation.Name = "txtAbsoluteLocation"
        Me.txtAbsoluteLocation.ReadOnly = True
        Me.txtAbsoluteLocation.Size = New System.Drawing.Size(180, 23)
        Me.txtAbsoluteLocation.TabIndex = 8
        ' 
        ' lblAbsoluteLocation
        ' 
        Me.lblAbsoluteLocation.AutoSize = True
        Me.lblAbsoluteLocation.Location = New System.Drawing.Point(3, 101)
        Me.lblAbsoluteLocation.Name = "lblAbsoluteLocation"
        Me.lblAbsoluteLocation.Size = New System.Drawing.Size(114, 15)
        Me.lblAbsoluteLocation.TabIndex = 7
        Me.lblAbsoluteLocation.Text = "Location (Absolute):"
        ' 
        ' txtFilePath
        ' 
        Me.txtFilePath.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Me.txtFilePath.Location = New System.Drawing.Point(117, 127)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.ReadOnly = True
        Me.txtFilePath.Size = New System.Drawing.Size(180, 23)
        Me.txtFilePath.TabIndex = 10
        ' 
        ' lblFilePath
        ' 
        Me.lblFilePath.AutoSize = True
        Me.lblFilePath.Location = New System.Drawing.Point(3, 130)
        Me.lblFilePath.Name = "lblFilePath"
        Me.lblFilePath.Size = New System.Drawing.Size(113, 15)
        Me.lblFilePath.TabIndex = 9
        Me.lblFilePath.Text = "File path (Absolute):"
        ' 
        ' btnSelPathPcm
        ' 
        Me.btnSelPathPcm.Location = New System.Drawing.Point(3, 243)
        Me.btnSelPathPcm.Name = "btnSelPathPcm"
        Me.btnSelPathPcm.Size = New System.Drawing.Size(145, 25)
        Me.btnSelPathPcm.TabIndex = 98
        Me.btnSelPathPcm.Text = "Select alt. Track"
        Me.btnSelPathPcm.UseVisualStyleBackColor = True
        ' 
        ' btnSelLocationPcm
        ' 
        Me.btnSelLocationPcm.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Me.btnSelLocationPcm.Location = New System.Drawing.Point(152, 243)
        Me.btnSelLocationPcm.Name = "btnSelLocationPcm"
        Me.btnSelLocationPcm.Size = New System.Drawing.Size(145, 25)
        Me.btnSelLocationPcm.TabIndex = 99
        Me.btnSelLocationPcm.Text = "Select alt. Track Location"
        Me.btnSelLocationPcm.UseVisualStyleBackColor = True
        ' 
        ' fbdLocationPcm
        ' 
        Me.fbdLocationPcm.Description = "Select the folder the currently used .PCM should be stored in."
        Me.fbdLocationPcm.RootFolder = Environment.SpecialFolder.Recent
        Me.fbdLocationPcm.Tag = ""
        ' 
        ' txtAutoSwitch
        ' 
        Me.txtAutoSwitch.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Me.txtAutoSwitch.Location = New System.Drawing.Point(117, 156)
        Me.txtAutoSwitch.Name = "txtAutoSwitch"
        Me.txtAutoSwitch.Size = New System.Drawing.Size(180, 23)
        Me.txtAutoSwitch.TabIndex = 12
        ' 
        ' lblAutoSwitch
        ' 
        Me.lblAutoSwitch.AutoSize = True
        Me.lblAutoSwitch.Location = New System.Drawing.Point(3, 159)
        Me.lblAutoSwitch.Name = "lblAutoSwitch"
        Me.lblAutoSwitch.Size = New System.Drawing.Size(116, 15)
        Me.lblAutoSwitch.TabIndex = 11
        Me.lblAutoSwitch.Text = "Auto switch Track(s):"
        ' 
        ' ttpMsuTrackAltControl
        ' 
        Me.ttpMsuTrackAltControl.AutoPopDelay = Integer.MaxValue
        Me.ttpMsuTrackAltControl.InitialDelay = 250
        Me.ttpMsuTrackAltControl.ReshowDelay = 50
        ' 
        ' lblLoopPoint
        ' 
        Me.lblLoopPoint.AutoSize = True
        Me.lblLoopPoint.Location = New System.Drawing.Point(3, 188)
        Me.lblLoopPoint.Name = "lblLoopPoint"
        Me.lblLoopPoint.Size = New System.Drawing.Size(68, 15)
        Me.lblLoopPoint.TabIndex = 15
        Me.lblLoopPoint.Text = "Loop point:"
        ' 
        ' lblLoopPointConv
        ' 
        Me.lblLoopPointConv.AutoSize = True
        Me.lblLoopPointConv.Location = New System.Drawing.Point(3, 217)
        Me.lblLoopPointConv.Name = "lblLoopPointConv"
        Me.lblLoopPointConv.Size = New System.Drawing.Size(134, 15)
        Me.lblLoopPointConv.TabIndex = 17
        Me.lblLoopPointConv.Text = "Loop point (Converted):"
        ' 
        ' nudLoopPoint
        ' 
        Me.nudLoopPoint.CausesValidation = False
        Me.nudLoopPoint.Hexadecimal = True
        Me.nudLoopPoint.Location = New System.Drawing.Point(143, 185)
        Me.nudLoopPoint.Maximum = New Decimal(New Integer() {-1, 0, 0, 0})
        Me.nudLoopPoint.Name = "nudLoopPoint"
        Me.nudLoopPoint.Size = New System.Drawing.Size(80, 23)
        Me.nudLoopPoint.TabIndex = 18
        Me.nudLoopPoint.Value = New Decimal(New Integer() {-1, 0, 0, 0})
        ' 
        ' nudLoopPointConv
        ' 
        Me.nudLoopPointConv.Location = New System.Drawing.Point(143, 214)
        Me.nudLoopPointConv.Maximum = New Decimal(New Integer() {-1, 0, 0, 0})
        Me.nudLoopPointConv.Name = "nudLoopPointConv"
        Me.nudLoopPointConv.Size = New System.Drawing.Size(80, 23)
        Me.nudLoopPointConv.TabIndex = 21
        Me.nudLoopPointConv.Value = New Decimal(New Integer() {-1, 0, 0, 0})
        ' 
        ' grpLoopPointBase
        ' 
        Me.grpLoopPointBase.Controls.Add(Me.ctrlBase16)
        Me.grpLoopPointBase.Controls.Add(Me.ctrlBase10)
        Me.grpLoopPointBase.Location = New System.Drawing.Point(318, 178)
        Me.grpLoopPointBase.Name = "grpLoopPointBase"
        Me.grpLoopPointBase.Size = New System.Drawing.Size(50, 59)
        Me.grpLoopPointBase.TabIndex = 25
        Me.grpLoopPointBase.TabStop = False
        Me.grpLoopPointBase.Text = "Base"
        ' 
        ' ctrlBase16
        ' 
        Me.ctrlBase16.AutoSize = True
        Me.ctrlBase16.Location = New System.Drawing.Point(6, 34)
        Me.ctrlBase16.Name = "ctrlBase16"
        Me.ctrlBase16.Size = New System.Drawing.Size(37, 19)
        Me.ctrlBase16.TabIndex = 1
        Me.ctrlBase16.TabStop = True
        Me.ctrlBase16.Text = "16"
        Me.ctrlBase16.UseVisualStyleBackColor = True
        ' 
        ' ctrlBase10
        ' 
        Me.ctrlBase10.AutoSize = True
        Me.ctrlBase10.Location = New System.Drawing.Point(6, 13)
        Me.ctrlBase10.Name = "ctrlBase10"
        Me.ctrlBase10.Size = New System.Drawing.Size(37, 19)
        Me.ctrlBase10.TabIndex = 0
        Me.ctrlBase10.TabStop = True
        Me.ctrlBase10.Text = "10"
        Me.ctrlBase10.UseVisualStyleBackColor = True
        ' 
        ' btnLoopPointToMax
        ' 
        Me.btnLoopPointToMax.Location = New System.Drawing.Point(229, 184)
        Me.btnLoopPointToMax.Name = "btnLoopPointToMax"
        Me.btnLoopPointToMax.Size = New System.Drawing.Size(38, 25)
        Me.btnLoopPointToMax.TabIndex = 19
        Me.btnLoopPointToMax.Text = "Max"
        Me.btnLoopPointToMax.UseVisualStyleBackColor = True
        ' 
        ' btnLoopPointConvToMax
        ' 
        Me.btnLoopPointConvToMax.Location = New System.Drawing.Point(229, 213)
        Me.btnLoopPointConvToMax.Name = "btnLoopPointConvToMax"
        Me.btnLoopPointConvToMax.Size = New System.Drawing.Size(38, 25)
        Me.btnLoopPointConvToMax.TabIndex = 22
        Me.btnLoopPointConvToMax.Text = "Max"
        Me.btnLoopPointConvToMax.UseVisualStyleBackColor = True
        ' 
        ' btnLoopPointReset
        ' 
        Me.btnLoopPointReset.Location = New System.Drawing.Point(270, 184)
        Me.btnLoopPointReset.Name = "btnLoopPointReset"
        Me.btnLoopPointReset.Size = New System.Drawing.Size(43, 25)
        Me.btnLoopPointReset.TabIndex = 20
        Me.btnLoopPointReset.Text = "Reset"
        Me.btnLoopPointReset.UseVisualStyleBackColor = True
        ' 
        ' btnLoopPointConvReset
        ' 
        Me.btnLoopPointConvReset.Location = New System.Drawing.Point(270, 213)
        Me.btnLoopPointConvReset.Name = "btnLoopPointConvReset"
        Me.btnLoopPointConvReset.Size = New System.Drawing.Size(43, 25)
        Me.btnLoopPointConvReset.TabIndex = 23
        Me.btnLoopPointConvReset.Text = "Reset"
        Me.btnLoopPointConvReset.UseVisualStyleBackColor = True
        ' 
        ' MsuTrackAltControl
        ' 
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7F, 15F)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.Controls.Add(Me.btnLoopPointConvReset)
        Me.Controls.Add(Me.btnLoopPointReset)
        Me.Controls.Add(Me.btnLoopPointConvToMax)
        Me.Controls.Add(Me.btnLoopPointToMax)
        Me.Controls.Add(Me.txtAutoSwitch)
        Me.Controls.Add(Me.grpLoopPointBase)
        Me.Controls.Add(Me.nudLoopPointConv)
        Me.Controls.Add(Me.nudLoopPoint)
        Me.Controls.Add(Me.lblLoopPointConv)
        Me.Controls.Add(Me.lblLoopPoint)
        Me.Controls.Add(Me.lblAutoSwitch)
        Me.Controls.Add(Me.btnSelLocationPcm)
        Me.Controls.Add(Me.btnSelPathPcm)
        Me.Controls.Add(Me.txtFilePath)
        Me.Controls.Add(Me.lblFilePath)
        Me.Controls.Add(Me.txtAbsoluteLocation)
        Me.Controls.Add(Me.lblAbsoluteLocation)
        Me.Controls.Add(Me.txtRelativeLocation)
        Me.Controls.Add(Me.lblRelativeLocation)
        Me.Controls.Add(Me.nudMsuTrackAltId)
        Me.Controls.Add(Me.txtMsuFileName)
        Me.Controls.Add(Me.lblMsuTrackAltId)
        Me.Controls.Add(Me.txtMsuAltTitle)
        Me.Controls.Add(Me.lblMsuTitle)
        Me.Name = "MsuTrackAltControl"
        Me.Size = New System.Drawing.Size(300, 272)
        CType(Me.nudMsuTrackAltId, ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudLoopPoint, ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudLoopPointConv, ComponentModel.ISupportInitialize).EndInit()
        Me.grpLoopPointBase.ResumeLayout(False)
        Me.grpLoopPointBase.PerformLayout()
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
    Friend WithEvents lblLoopPoint As Label
    Friend WithEvents lblLoopPointConv As Label
    Friend WithEvents nudLoopPoint As NumericUpDown
    Friend WithEvents nudLoopPointConv As NumericUpDown
    Friend WithEvents grpLoopPointBase As GroupBox
    Friend WithEvents ctrlBase10 As RadioButton
    Friend WithEvents ctrlBase16 As RadioButton
    Friend WithEvents btnLoopPointToMax As Button
    Friend WithEvents btnLoopPointConvToMax As Button
    Friend WithEvents btnLoopPointReset As Button
    Friend WithEvents btnLoopPointConvReset As Button
End Class
