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
        lblLoopPoint = New Label()
        lblLoopPointConv = New Label()
        nudLoopPoint = New NumericUpDown()
        nudLoopPointConv = New NumericUpDown()
        grpLoopPointBase = New GroupBox()
        ctrlBase16 = New RadioButton()
        ctrlBase10 = New RadioButton()
        btnLoopPointToMax = New Button()
        btnLoopPointConvToMax = New Button()
        btnLoopPointReset = New Button()
        btnLoopPointConvReset = New Button()
        CType(nudMsuTrackAltId, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudLoopPoint, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudLoopPointConv, ComponentModel.ISupportInitialize).BeginInit()
        grpLoopPointBase.SuspendLayout()
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
        btnSelPathPcm.Location = New Drawing.Point(3, 243)
        btnSelPathPcm.Name = "btnSelPathPcm"
        btnSelPathPcm.Size = New Drawing.Size(145, 25)
        btnSelPathPcm.TabIndex = 13
        btnSelPathPcm.Text = "Select alt. Track"
        btnSelPathPcm.UseVisualStyleBackColor = True
        ' 
        ' btnSelLocationPcm
        ' 
        btnSelLocationPcm.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnSelLocationPcm.Location = New Drawing.Point(152, 243)
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
        ' lblLoopPoint
        ' 
        lblLoopPoint.AutoSize = True
        lblLoopPoint.Location = New System.Drawing.Point(3, 188)
        lblLoopPoint.Name = "lblLoopPoint"
        lblLoopPoint.Size = New System.Drawing.Size(68, 15)
        lblLoopPoint.TabIndex = 15
        lblLoopPoint.Text = "Loop point:"
        ' 
        ' lblLoopPointConv
        ' 
        lblLoopPointConv.AutoSize = True
        lblLoopPointConv.Location = New System.Drawing.Point(3, 217)
        lblLoopPointConv.Name = "lblLoopPointConv"
        lblLoopPointConv.Size = New System.Drawing.Size(134, 15)
        lblLoopPointConv.TabIndex = 17
        lblLoopPointConv.Text = "Loop point (Converted):"
        ' 
        ' nudLoopPoint
        ' 
        nudLoopPoint.CausesValidation = False
        nudLoopPoint.Hexadecimal = True
        nudLoopPoint.Location = New System.Drawing.Point(143, 185)
        nudLoopPoint.Maximum = New Decimal(New Integer() {-1, 0, 0, 0})
        nudLoopPoint.Name = "nudLoopPoint"
        nudLoopPoint.Size = New System.Drawing.Size(80, 23)
        nudLoopPoint.TabIndex = 18
        nudLoopPoint.Value = New Decimal(New Integer() {-1, 0, 0, 0})
        ' 
        ' nudLoopPointConv
        ' 
        nudLoopPointConv.Location = New System.Drawing.Point(143, 214)
        nudLoopPointConv.Maximum = New Decimal(New Integer() {-1, 0, 0, 0})
        nudLoopPointConv.Name = "nudLoopPointConv"
        nudLoopPointConv.Size = New System.Drawing.Size(80, 23)
        nudLoopPointConv.TabIndex = 21
        nudLoopPointConv.Value = New Decimal(New Integer() {-1, 0, 0, 0})
        ' 
        ' grpLoopPointBase
        ' 
        grpLoopPointBase.Controls.Add(Me.ctrlBase16)
        grpLoopPointBase.Controls.Add(Me.ctrlBase10)
        grpLoopPointBase.Location = New System.Drawing.Point(318, 178)
        grpLoopPointBase.Name = "grpLoopPointBase"
        grpLoopPointBase.Size = New System.Drawing.Size(50, 59)
        grpLoopPointBase.TabIndex = 25
        grpLoopPointBase.TabStop = False
        grpLoopPointBase.Text = "Base"
        ' 
        ' ctrlBase16
        ' 
        ctrlBase16.AutoSize = True
        ctrlBase16.Location = New System.Drawing.Point(6, 34)
        ctrlBase16.Name = "ctrlBase16"
        ctrlBase16.Size = New System.Drawing.Size(37, 19)
        ctrlBase16.TabIndex = 1
        ctrlBase16.TabStop = True
        ctrlBase16.Text = "16"
        ctrlBase16.UseVisualStyleBackColor = True
        ' 
        ' ctrlBase10
        ' 
        ctrlBase10.AutoSize = True
        ctrlBase10.Location = New System.Drawing.Point(6, 13)
        ctrlBase10.Name = "ctrlBase10"
        ctrlBase10.Size = New System.Drawing.Size(37, 19)
        ctrlBase10.TabIndex = 0
        ctrlBase10.TabStop = True
        ctrlBase10.Text = "10"
        ctrlBase10.UseVisualStyleBackColor = True
        ' 
        ' btnLoopPointToMax
        ' 
        btnLoopPointToMax.Location = New System.Drawing.Point(229, 184)
        btnLoopPointToMax.Name = "btnLoopPointToMax"
        btnLoopPointToMax.Size = New System.Drawing.Size(38, 25)
        btnLoopPointToMax.TabIndex = 19
        btnLoopPointToMax.Text = "Max"
        btnLoopPointToMax.UseVisualStyleBackColor = True
        ' 
        ' btnLoopPointConvToMax
        ' 
        btnLoopPointConvToMax.Location = New System.Drawing.Point(229, 213)
        btnLoopPointConvToMax.Name = "btnLoopPointConvToMax"
        btnLoopPointConvToMax.Size = New System.Drawing.Size(38, 25)
        btnLoopPointConvToMax.TabIndex = 22
        btnLoopPointConvToMax.Text = "Max"
        btnLoopPointConvToMax.UseVisualStyleBackColor = True
        ' 
        ' btnLoopPointReset
        ' 
        btnLoopPointReset.Location = New System.Drawing.Point(270, 184)
        btnLoopPointReset.Name = "btnLoopPointReset"
        btnLoopPointReset.Size = New System.Drawing.Size(43, 25)
        btnLoopPointReset.TabIndex = 20
        btnLoopPointReset.Text = "Reset"
        btnLoopPointReset.UseVisualStyleBackColor = True
        ' 
        ' btnLoopPointConvReset
        ' 
        btnLoopPointConvReset.Location = New System.Drawing.Point(270, 213)
        btnLoopPointConvReset.Name = "btnLoopPointConvReset"
        btnLoopPointConvReset.Size = New System.Drawing.Size(43, 25)
        btnLoopPointConvReset.TabIndex = 23
        btnLoopPointConvReset.Text = "Reset"
        btnLoopPointConvReset.UseVisualStyleBackColor = True
        ' 
        ' MsuTrackAltControl
        ' 
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New Drawing.SizeF(7F, 15F)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.Controls.Add(btnLoopPointConvReset)
        Me.Controls.Add(btnLoopPointReset)
        Me.Controls.Add(btnLoopPointConvToMax)
        Me.Controls.Add(btnLoopPointToMax)
        Me.Controls.Add(txtAutoSwitch)
        Me.Controls.Add(grpLoopPointBase)
        Me.Controls.Add(nudLoopPointConv)
        Me.Controls.Add(nudLoopPoint)
        Me.Controls.Add(lblLoopPointConv)
        Me.Controls.Add(lblLoopPoint)
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
        Me.Size = New Drawing.Size(300, 272)
        CType(nudMsuTrackAltId, ComponentModel.ISupportInitialize).EndInit()
        CType(nudLoopPoint, ComponentModel.ISupportInitialize).EndInit()
        CType(nudLoopPointConv, ComponentModel.ISupportInitialize).EndInit()
        grpLoopPointBase.ResumeLayout(False)
        grpLoopPointBase.PerformLayout()
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
