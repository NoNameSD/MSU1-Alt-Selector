<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Msu1AltSelectMainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Msu1AltSelectMainForm))
        Me.sfdLogExport = New SaveFileDialog()
        Me.sfdJson = New SaveFileDialog()
        Me.btnSelPathMsu = New Button()
        Me.ofdPathMsu = New OpenFileDialog()
        Me.txtPathMsu = New TextBox()
        Me.ContextMenuStripMsuConfig = New ContextMenuStrip(Me.components)
        Me.OpenMsuLocationToolStripMenuItem = New ToolStripMenuItem()
        Me.lstvTracks = New ListView()
        Me.chTrackNumber = New ColumnHeader()
        Me.chTrackTitle = New ColumnHeader()
        Me.ContextMenuStripTracks = New ContextMenuStrip(Me.components)
        Me.EditTrackToolStripMenuItem = New ToolStripMenuItem()
        Me.DeleteTrackToolStripMenuItem = New ToolStripMenuItem()
        Me.lstvAltTracks = New ListView()
        Me.chAltTrackId = New ColumnHeader()
        Me.chAltTrackTitle = New ColumnHeader()
        Me.ContextMenuStripAltTracks = New ContextMenuStrip(Me.components)
        Me.SetAsCurrentTrackToolStripMenuItem = New ToolStripMenuItem()
        Me.AltTrackToolStripSeparator1 = New ToolStripSeparator()
        Me.OpenAltTrackLocationToolStripMenuItem = New ToolStripMenuItem()
        Me.EditAltTrackToolStripMenuItem = New ToolStripMenuItem()
        Me.DeleteAltTrackToolStripMenuItem = New ToolStripMenuItem()
        Me.AltTrackToolStripSeparator2 = New ToolStripSeparator()
        Me.AddNewAltTrackToolStripMenuItem = New ToolStripMenuItem()
        Me.chAltTrackAutoSwitch = New ColumnHeader()
        Me.chAltTrackLoopPoint = New ColumnHeader()
        Me.chAltTrackLoopPointConverted = New ColumnHeader()
        Me.scTracks = New SplitContainer()
        Me.ucMsuLog = New MsuLogControl()
        Me.grpPcmConvert = New GroupBox()
        Me.ctrlDisplayCmd = New CheckBox()
        Me.nudProcessCount = New NumericUpDown()
        Me.nudPcmResample = New NumericUpDown()
        Me.btnPcmToNormal = New Button()
        Me.lblHz = New Label()
        Me.btnConvertPcm = New Button()
        Me.frameResample = New Panel()
        Me.optPcmNormal = New RadioButton()
        Me.optPcmConverted = New RadioButton()
        Me.nudPcmVolume = New NumericUpDown()
        Me.lblPcmVolume = New Label()
        Me.ctrlKeepCmdOpen = New CheckBox()
        Me.lblProcessCount = New Label()
        Me.btnSaveJson = New Button()
        Me.btnSaveJsonAs = New Button()
        Me.grpMsuTracks = New GroupBox()
        Me.grpAutoSwitch = New GroupBox()
        Me.lblAutoSwitchInterval = New Label()
        Me.lblAutoSwitchIntervalUnit = New Label()
        Me.nudAutoSwitchInterval = New NumericUpDown()
        Me.ctrlEnableAutoSwitch = New CheckBox()
        Me.tmrAutoSwitch = New Timer(Me.components)
        Me.ctrlDisplayOnlyTracksWithAlts = New CheckBox()
        Me.scVerticalHalf = New SplitContainer()
        Me.btnSettings = New Button()
        Me.btnScanMsuDirectory = New Button()
        Me.BackgroundWorkerDelegate = New ComponentModel.BackgroundWorker()
        Me.ttpMsuAltSel = New ToolTip(Me.components)
        Me.ctrlDisplayLoopPoints = New CheckBox()
        Me.ContextMenuStripMsuConfig.SuspendLayout()
        Me.ContextMenuStripTracks.SuspendLayout()
        Me.ContextMenuStripAltTracks.SuspendLayout()
        CType(Me.scTracks, ComponentModel.ISupportInitialize).BeginInit()
        Me.scTracks.Panel1.SuspendLayout()
        Me.scTracks.Panel2.SuspendLayout()
        Me.scTracks.SuspendLayout()
        Me.grpPcmConvert.SuspendLayout()
        CType(Me.nudProcessCount, ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudPcmResample, ComponentModel.ISupportInitialize).BeginInit()
        Me.frameResample.SuspendLayout()
        CType(Me.nudPcmVolume, ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMsuTracks.SuspendLayout()
        Me.grpAutoSwitch.SuspendLayout()
        CType(Me.nudAutoSwitchInterval, ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.scVerticalHalf, ComponentModel.ISupportInitialize).BeginInit()
        Me.scVerticalHalf.Panel1.SuspendLayout()
        Me.scVerticalHalf.Panel2.SuspendLayout()
        Me.scVerticalHalf.SuspendLayout()
        Me.SuspendLayout()
        ' 
        ' sfdLogExport
        ' 
        Me.sfdLogExport.DefaultExt = "rtf"
        Me.sfdLogExport.Filter = "Rich-Text-Format|*.rtf|Plain text|*.txt"
        Me.sfdLogExport.Title = "Export the current log to a file."
        ' 
        ' sfdJson
        ' 
        Me.sfdJson.DefaultExt = "json"
        Me.sfdJson.Filter = "JSON config|*.json"
        Me.sfdJson.Title = "Export the current MSU configuration as JSON"
        ' 
        ' btnSelPathMsu
        ' 
        Me.btnSelPathMsu.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Me.btnSelPathMsu.Location = New System.Drawing.Point(435, 12)
        Me.btnSelPathMsu.Name = "btnSelPathMsu"
        Me.btnSelPathMsu.Size = New System.Drawing.Size(87, 24)
        Me.btnSelPathMsu.TabIndex = 1
        Me.btnSelPathMsu.Text = "Select MSU1"
        Me.btnSelPathMsu.UseVisualStyleBackColor = True
        ' 
        ' ofdPathMsu
        ' 
        Me.ofdPathMsu.Filter = "All Files|*.*|All|*.json;*.msu;*.sfc;*.smc|JSON config|*.json|MSU file|*.msu|SNES ROM|*.sfc;*.smc"
        Me.ofdPathMsu.FilterIndex = 2
        Me.ofdPathMsu.Title = "Select the .MSU file or the MSU1 patched ROM (Or anything that has the same name as the ROM)"
        ' 
        ' txtPathMsu
        ' 
        Me.txtPathMsu.AllowDrop = True
        Me.txtPathMsu.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Me.txtPathMsu.ContextMenuStrip = Me.ContextMenuStripMsuConfig
        Me.txtPathMsu.Location = New System.Drawing.Point(6, 12)
        Me.txtPathMsu.Name = "txtPathMsu"
        Me.txtPathMsu.ReadOnly = True
        Me.txtPathMsu.Size = New System.Drawing.Size(423, 23)
        Me.txtPathMsu.TabIndex = 0
        Me.txtPathMsu.WordWrap = False
        ' 
        ' ContextMenuStripMsuConfig
        ' 
        Me.ContextMenuStripMsuConfig.Items.AddRange(New ToolStripItem() {Me.OpenMsuLocationToolStripMenuItem})
        Me.ContextMenuStripMsuConfig.Name = "ContextMenuStripMsuConfig"
        Me.ContextMenuStripMsuConfig.Size = New System.Drawing.Size(153, 26)
        ' 
        ' OpenMsuLocationToolStripMenuItem
        ' 
        Me.OpenMsuLocationToolStripMenuItem.Name = "OpenMsuLocationToolStripMenuItem"
        Me.OpenMsuLocationToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.OpenMsuLocationToolStripMenuItem.Text = "Open Location"
        ' 
        ' lstvTracks
        ' 
        Me.lstvTracks.AccessibleRole = AccessibleRole.List
        Me.lstvTracks.Alignment = ListViewAlignment.SnapToGrid
        Me.lstvTracks.AllowColumnReorder = True
        Me.lstvTracks.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Me.lstvTracks.BackgroundImageTiled = True
        Me.lstvTracks.Columns.AddRange(New ColumnHeader() {Me.chTrackNumber, Me.chTrackTitle})
        Me.lstvTracks.ContextMenuStrip = Me.ContextMenuStripTracks
        Me.lstvTracks.FullRowSelect = True
        Me.lstvTracks.GridLines = True
        Me.lstvTracks.LabelWrap = False
        Me.lstvTracks.Location = New System.Drawing.Point(3, 0)
        Me.lstvTracks.MultiSelect = False
        Me.lstvTracks.Name = "lstvTracks"
        Me.lstvTracks.ShowItemToolTips = True
        Me.lstvTracks.Size = New System.Drawing.Size(153, 203)
        Me.lstvTracks.TabIndex = 8
        Me.lstvTracks.UseCompatibleStateImageBehavior = False
        Me.lstvTracks.View = View.Details
        ' 
        ' chTrackNumber
        ' 
        Me.chTrackNumber.Text = "Id"
        Me.chTrackNumber.Width = 25
        ' 
        ' chTrackTitle
        ' 
        Me.chTrackTitle.Text = "Track title"
        Me.chTrackTitle.Width = 150
        ' 
        ' ContextMenuStripTracks
        ' 
        Me.ContextMenuStripTracks.Items.AddRange(New ToolStripItem() {Me.EditTrackToolStripMenuItem, Me.DeleteTrackToolStripMenuItem})
        Me.ContextMenuStripTracks.Name = "ContextMenuStripTracks"
        Me.ContextMenuStripTracks.Size = New System.Drawing.Size(136, 48)
        ' 
        ' EditTrackToolStripMenuItem
        ' 
        Me.EditTrackToolStripMenuItem.Name = "EditTrackToolStripMenuItem"
        Me.EditTrackToolStripMenuItem.Size = New System.Drawing.Size(135, 22)
        Me.EditTrackToolStripMenuItem.Text = "Edit"
        ' 
        ' DeleteTrackToolStripMenuItem
        ' 
        Me.DeleteTrackToolStripMenuItem.Name = "DeleteTrackToolStripMenuItem"
        Me.DeleteTrackToolStripMenuItem.ShortcutKeys = Keys.Delete
        Me.DeleteTrackToolStripMenuItem.Size = New System.Drawing.Size(135, 22)
        Me.DeleteTrackToolStripMenuItem.Text = "Delete"
        ' 
        ' lstvAltTracks
        ' 
        Me.lstvAltTracks.AccessibleRole = AccessibleRole.List
        Me.lstvAltTracks.Alignment = ListViewAlignment.SnapToGrid
        Me.lstvAltTracks.AllowColumnReorder = True
        Me.lstvAltTracks.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Me.lstvAltTracks.CheckBoxes = True
        Me.lstvAltTracks.Columns.AddRange(New ColumnHeader() {Me.chAltTrackId, Me.chAltTrackTitle})
        Me.lstvAltTracks.ContextMenuStrip = Me.ContextMenuStripAltTracks
        Me.lstvAltTracks.FullRowSelect = True
        Me.lstvAltTracks.GridLines = True
        Me.lstvAltTracks.Location = New System.Drawing.Point(3, 0)
        Me.lstvAltTracks.MultiSelect = False
        Me.lstvAltTracks.Name = "lstvAltTracks"
        Me.lstvAltTracks.ShowItemToolTips = True
        Me.lstvAltTracks.Size = New System.Drawing.Size(342, 203)
        Me.lstvAltTracks.TabIndex = 9
        Me.lstvAltTracks.UseCompatibleStateImageBehavior = False
        Me.lstvAltTracks.View = View.Details
        ' 
        ' chAltTrackId
        ' 
        Me.chAltTrackId.Text = "Id"
        Me.chAltTrackId.Width = 40
        ' 
        ' chAltTrackTitle
        ' 
        Me.chAltTrackTitle.Text = "alt. Track title"
        Me.chAltTrackTitle.Width = 150
        ' 
        ' ContextMenuStripAltTracks
        ' 
        Me.ContextMenuStripAltTracks.Items.AddRange(New ToolStripItem() {Me.SetAsCurrentTrackToolStripMenuItem, Me.AltTrackToolStripSeparator1, Me.OpenAltTrackLocationToolStripMenuItem, Me.EditAltTrackToolStripMenuItem, Me.DeleteAltTrackToolStripMenuItem, Me.AltTrackToolStripSeparator2, Me.AddNewAltTrackToolStripMenuItem})
        Me.ContextMenuStripAltTracks.Name = "ContextMenuStripAltTracks"
        Me.ContextMenuStripAltTracks.Size = New System.Drawing.Size(177, 126)
        ' 
        ' SetAsCurrentTrackToolStripMenuItem
        ' 
        Me.SetAsCurrentTrackToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9F, Drawing.FontStyle.Bold)
        Me.SetAsCurrentTrackToolStripMenuItem.Name = "SetAsCurrentTrackToolStripMenuItem"
        Me.SetAsCurrentTrackToolStripMenuItem.ShowShortcutKeys = False
        Me.SetAsCurrentTrackToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.SetAsCurrentTrackToolStripMenuItem.Text = "Switch to this Track"
        ' 
        ' AltTrackToolStripSeparator1
        ' 
        Me.AltTrackToolStripSeparator1.Name = "AltTrackToolStripSeparator1"
        Me.AltTrackToolStripSeparator1.Size = New System.Drawing.Size(173, 6)
        ' 
        ' OpenAltTrackLocationToolStripMenuItem
        ' 
        Me.OpenAltTrackLocationToolStripMenuItem.Name = "OpenAltTrackLocationToolStripMenuItem"
        Me.OpenAltTrackLocationToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.OpenAltTrackLocationToolStripMenuItem.Text = "Open Location"
        ' 
        ' EditAltTrackToolStripMenuItem
        ' 
        Me.EditAltTrackToolStripMenuItem.Name = "EditAltTrackToolStripMenuItem"
        Me.EditAltTrackToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.EditAltTrackToolStripMenuItem.Text = "Edit"
        ' 
        ' DeleteAltTrackToolStripMenuItem
        ' 
        Me.DeleteAltTrackToolStripMenuItem.Name = "DeleteAltTrackToolStripMenuItem"
        Me.DeleteAltTrackToolStripMenuItem.ShortcutKeys = Keys.Delete
        Me.DeleteAltTrackToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.DeleteAltTrackToolStripMenuItem.Text = "Delete"
        ' 
        ' AltTrackToolStripSeparator2
        ' 
        Me.AltTrackToolStripSeparator2.Name = "AltTrackToolStripSeparator2"
        Me.AltTrackToolStripSeparator2.Size = New System.Drawing.Size(173, 6)
        ' 
        ' AddNewAltTrackToolStripMenuItem
        ' 
        Me.AddNewAltTrackToolStripMenuItem.Name = "AddNewAltTrackToolStripMenuItem"
        Me.AddNewAltTrackToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.AddNewAltTrackToolStripMenuItem.Text = "Add New"
        ' 
        ' chAltTrackAutoSwitch
        ' 
        Me.chAltTrackAutoSwitch.Text = "Auto Switch"
        Me.chAltTrackAutoSwitch.Width = 80
        ' 
        ' chAltTrackLoopPoint
        ' 
        Me.chAltTrackLoopPoint.Text = "Loop Point"
        Me.chAltTrackLoopPoint.TextAlign = HorizontalAlignment.Right
        Me.chAltTrackLoopPoint.Width = 70
        ' 
        ' chAltTrackLoopPointConverted
        ' 
        Me.chAltTrackLoopPointConverted.Text = "Loop Point (Converted)"
        Me.chAltTrackLoopPointConverted.TextAlign = HorizontalAlignment.Right
        Me.chAltTrackLoopPointConverted.Width = 136
        ' 
        ' scTracks
        ' 
        Me.scTracks.Dock = DockStyle.Fill
        Me.scTracks.Location = New System.Drawing.Point(3, 19)
        Me.scTracks.Name = "scTracks"
        ' 
        ' scTracks.Panel1
        ' 
        Me.scTracks.Panel1.Controls.Add(Me.lstvTracks)
        ' 
        ' scTracks.Panel2
        ' 
        Me.scTracks.Panel2.Controls.Add(Me.lstvAltTracks)
        Me.scTracks.Size = New System.Drawing.Size(512, 203)
        Me.scTracks.SplitterDistance = 158
        Me.scTracks.TabIndex = 0
        ' 
        ' ucMsuLog
        ' 
        Me.ucMsuLog.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Me.ucMsuLog.AutoSizeMode = AutoSizeMode.GrowAndShrink
        Me.ucMsuLog.Location = New System.Drawing.Point(0, 0)
        Me.ucMsuLog.Logger = Nothing
        Me.ucMsuLog.Margin = New Padding(0)
        Me.ucMsuLog.Name = "ucMsuLog"
        Me.ucMsuLog.Size = New System.Drawing.Size(523, 115)
        Me.ucMsuLog.TabIndex = 84
        ' 
        ' grpPcmConvert
        ' 
        Me.grpPcmConvert.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Me.grpPcmConvert.Controls.Add(Me.ctrlDisplayCmd)
        Me.grpPcmConvert.Controls.Add(Me.nudProcessCount)
        Me.grpPcmConvert.Controls.Add(Me.nudPcmResample)
        Me.grpPcmConvert.Controls.Add(Me.btnPcmToNormal)
        Me.grpPcmConvert.Controls.Add(Me.lblHz)
        Me.grpPcmConvert.Controls.Add(Me.btnConvertPcm)
        Me.grpPcmConvert.Controls.Add(Me.frameResample)
        Me.grpPcmConvert.Controls.Add(Me.nudPcmVolume)
        Me.grpPcmConvert.Controls.Add(Me.lblPcmVolume)
        Me.grpPcmConvert.Controls.Add(Me.ctrlKeepCmdOpen)
        Me.grpPcmConvert.Controls.Add(Me.lblProcessCount)
        Me.grpPcmConvert.Location = New System.Drawing.Point(0, 225)
        Me.grpPcmConvert.Name = "grpPcmConvert"
        Me.grpPcmConvert.Size = New System.Drawing.Size(365, 96)
        Me.grpPcmConvert.TabIndex = 10
        Me.grpPcmConvert.TabStop = False
        Me.grpPcmConvert.Text = "Convert PCM files"
        ' 
        ' ctrlDisplayCmd
        ' 
        Me.ctrlDisplayCmd.AutoSize = True
        Me.ctrlDisplayCmd.Font = New System.Drawing.Font("Segoe UI", 8.5F)
        Me.ctrlDisplayCmd.Location = New System.Drawing.Point(6, 71)
        Me.ctrlDisplayCmd.Name = "ctrlDisplayCmd"
        Me.ctrlDisplayCmd.Size = New System.Drawing.Size(137, 19)
        Me.ctrlDisplayCmd.TabIndex = 17
        Me.ctrlDisplayCmd.Text = "Show CMD Windows"
        Me.ctrlDisplayCmd.UseVisualStyleBackColor = True
        ' 
        ' nudProcessCount
        ' 
        Me.nudProcessCount.BackColor = Drawing.SystemColors.Window
        Me.nudProcessCount.CausesValidation = False
        Me.nudProcessCount.Cursor = Cursors.IBeam
        Me.nudProcessCount.ForeColor = Drawing.SystemColors.WindowText
        Me.nudProcessCount.Location = New System.Drawing.Point(91, 19)
        Me.nudProcessCount.Margin = New Padding(4, 0, 4, 3)
        Me.nudProcessCount.Maximum = New Decimal(New Integer() {64, 0, 0, 0})
        Me.nudProcessCount.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudProcessCount.Name = "nudProcessCount"
        Me.nudProcessCount.RightToLeft = RightToLeft.No
        Me.nudProcessCount.Size = New System.Drawing.Size(35, 23)
        Me.nudProcessCount.TabIndex = 13
        Me.nudProcessCount.TextAlign = HorizontalAlignment.Right
        Me.nudProcessCount.Value = New Decimal(New Integer() {64, 0, 0, 0})
        ' 
        ' nudPcmResample
        ' 
        Me.nudPcmResample.BackColor = Drawing.SystemColors.Window
        Me.nudPcmResample.CausesValidation = False
        Me.nudPcmResample.Cursor = Cursors.IBeam
        Me.nudPcmResample.ForeColor = Drawing.SystemColors.WindowText
        Me.nudPcmResample.Location = New System.Drawing.Point(6, 19)
        Me.nudPcmResample.Margin = New Padding(4, 0, 4, 3)
        Me.nudPcmResample.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.nudPcmResample.Minimum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nudPcmResample.Name = "nudPcmResample"
        Me.nudPcmResample.RightToLeft = RightToLeft.No
        Me.nudPcmResample.Size = New System.Drawing.Size(66, 23)
        Me.nudPcmResample.TabIndex = 11
        Me.nudPcmResample.TextAlign = HorizontalAlignment.Right
        Me.nudPcmResample.Value = New Decimal(New Integer() {44100, 0, 0, 0})
        ' 
        ' btnPcmToNormal
        ' 
        Me.btnPcmToNormal.BackColor = Drawing.SystemColors.Control
        Me.btnPcmToNormal.ForeColor = Drawing.SystemColors.ControlText
        Me.btnPcmToNormal.Location = New System.Drawing.Point(194, 19)
        Me.btnPcmToNormal.Margin = New Padding(4, 0, 4, 3)
        Me.btnPcmToNormal.Name = "btnPcmToNormal"
        Me.btnPcmToNormal.RightToLeft = RightToLeft.No
        Me.btnPcmToNormal.Size = New System.Drawing.Size(150, 23)
        Me.btnPcmToNormal.TabIndex = 19
        Me.btnPcmToNormal.Text = "Change Back to Normal"
        Me.btnPcmToNormal.UseVisualStyleBackColor = False
        ' 
        ' lblHz
        ' 
        Me.lblHz.BackColor = Drawing.Color.Transparent
        Me.lblHz.ForeColor = Drawing.SystemColors.ControlText
        Me.lblHz.Location = New System.Drawing.Point(70, 21)
        Me.lblHz.Margin = New Padding(4, 0, 4, 0)
        Me.lblHz.Name = "lblHz"
        Me.lblHz.RightToLeft = RightToLeft.No
        Me.lblHz.Size = New System.Drawing.Size(28, 20)
        Me.lblHz.TabIndex = 12
        Me.lblHz.Text = "Hz"
        ' 
        ' btnConvertPcm
        ' 
        Me.btnConvertPcm.BackColor = Drawing.SystemColors.Control
        Me.btnConvertPcm.ForeColor = Drawing.SystemColors.ControlText
        Me.btnConvertPcm.Location = New System.Drawing.Point(194, 41)
        Me.btnConvertPcm.Margin = New Padding(4, 3, 4, 3)
        Me.btnConvertPcm.Name = "btnConvertPcm"
        Me.btnConvertPcm.RightToLeft = RightToLeft.No
        Me.btnConvertPcm.Size = New System.Drawing.Size(150, 23)
        Me.btnConvertPcm.TabIndex = 20
        Me.btnConvertPcm.Text = "Change Speed/Volume"
        Me.btnConvertPcm.UseVisualStyleBackColor = False
        ' 
        ' frameResample
        ' 
        Me.frameResample.BackColor = Drawing.SystemColors.Control
        Me.frameResample.Controls.Add(Me.optPcmNormal)
        Me.frameResample.Controls.Add(Me.optPcmConverted)
        Me.frameResample.ForeColor = Drawing.SystemColors.ControlText
        Me.frameResample.Location = New System.Drawing.Point(345, 20)
        Me.frameResample.Margin = New Padding(4, 0, 4, 3)
        Me.frameResample.Name = "frameResample"
        Me.frameResample.RightToLeft = RightToLeft.No
        Me.frameResample.Size = New System.Drawing.Size(15, 42)
        Me.frameResample.TabIndex = 21
        ' 
        ' optPcmNormal
        ' 
        Me.optPcmNormal.BackColor = Drawing.SystemColors.Control
        Me.optPcmNormal.Enabled = False
        Me.optPcmNormal.ForeColor = Drawing.SystemColors.ControlText
        Me.optPcmNormal.Location = New System.Drawing.Point(0, 3)
        Me.optPcmNormal.Margin = New Padding(4, 3, 4, 3)
        Me.optPcmNormal.Name = "optPcmNormal"
        Me.optPcmNormal.RightToLeft = RightToLeft.No
        Me.optPcmNormal.Size = New System.Drawing.Size(15, 15)
        Me.optPcmNormal.TabIndex = 22
        Me.optPcmNormal.TabStop = True
        Me.optPcmNormal.UseVisualStyleBackColor = False
        ' 
        ' optPcmConverted
        ' 
        Me.optPcmConverted.BackColor = Drawing.SystemColors.Control
        Me.optPcmConverted.Enabled = False
        Me.optPcmConverted.ForeColor = Drawing.SystemColors.ControlText
        Me.optPcmConverted.Location = New System.Drawing.Point(0, 25)
        Me.optPcmConverted.Margin = New Padding(4, 3, 4, 3)
        Me.optPcmConverted.Name = "optPcmConverted"
        Me.optPcmConverted.RightToLeft = RightToLeft.No
        Me.optPcmConverted.Size = New System.Drawing.Size(15, 15)
        Me.optPcmConverted.TabIndex = 23
        Me.optPcmConverted.TabStop = True
        Me.optPcmConverted.UseVisualStyleBackColor = False
        ' 
        ' nudPcmVolume
        ' 
        Me.nudPcmVolume.BackColor = Drawing.SystemColors.Window
        Me.nudPcmVolume.CausesValidation = False
        Me.nudPcmVolume.Cursor = Cursors.IBeam
        Me.nudPcmVolume.ForeColor = Drawing.SystemColors.WindowText
        Me.nudPcmVolume.Location = New System.Drawing.Point(6, 41)
        Me.nudPcmVolume.Margin = New Padding(4, 3, 4, 3)
        Me.nudPcmVolume.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nudPcmVolume.Name = "nudPcmVolume"
        Me.nudPcmVolume.RightToLeft = RightToLeft.No
        Me.nudPcmVolume.Size = New System.Drawing.Size(66, 23)
        Me.nudPcmVolume.TabIndex = 15
        Me.nudPcmVolume.TextAlign = HorizontalAlignment.Right
        Me.nudPcmVolume.Value = New Decimal(New Integer() {100, 0, 0, 0})
        ' 
        ' lblPcmVolume
        ' 
        Me.lblPcmVolume.BackColor = Drawing.Color.Transparent
        Me.lblPcmVolume.ForeColor = Drawing.SystemColors.ControlText
        Me.lblPcmVolume.Location = New System.Drawing.Point(70, 43)
        Me.lblPcmVolume.Margin = New Padding(4, 0, 4, 0)
        Me.lblPcmVolume.Name = "lblPcmVolume"
        Me.lblPcmVolume.RightToLeft = RightToLeft.No
        Me.lblPcmVolume.Size = New System.Drawing.Size(65, 20)
        Me.lblPcmVolume.TabIndex = 16
        Me.lblPcmVolume.Text = "% Volume"
        ' 
        ' ctrlKeepCmdOpen
        ' 
        Me.ctrlKeepCmdOpen.AutoSize = True
        Me.ctrlKeepCmdOpen.Font = New System.Drawing.Font("Segoe UI", 8.5F)
        Me.ctrlKeepCmdOpen.Location = New System.Drawing.Point(148, 71)
        Me.ctrlKeepCmdOpen.Name = "ctrlKeepCmdOpen"
        Me.ctrlKeepCmdOpen.Size = New System.Drawing.Size(210, 19)
        Me.ctrlKeepCmdOpen.TabIndex = 18
        Me.ctrlKeepCmdOpen.Text = "Keep CMD Windows open (Debug)"
        Me.ctrlKeepCmdOpen.UseVisualStyleBackColor = True
        ' 
        ' lblProcessCount
        ' 
        Me.lblProcessCount.BackColor = Drawing.Color.Transparent
        Me.lblProcessCount.ForeColor = Drawing.SystemColors.ControlText
        Me.lblProcessCount.Location = New System.Drawing.Point(124, 21)
        Me.lblProcessCount.Margin = New Padding(4, 0, 4, 0)
        Me.lblProcessCount.Name = "lblProcessCount"
        Me.lblProcessCount.RightToLeft = RightToLeft.No
        Me.lblProcessCount.Size = New System.Drawing.Size(72, 20)
        Me.lblProcessCount.TabIndex = 14
        Me.lblProcessCount.Text = "Processes"
        ' 
        ' btnSaveJson
        ' 
        Me.btnSaveJson.DialogResult = DialogResult.TryAgain
        Me.btnSaveJson.Image = My.Resources.Resources.Shell32_1965
        Me.btnSaveJson.ImageAlign = Drawing.ContentAlignment.MiddleLeft
        Me.btnSaveJson.Location = New System.Drawing.Point(6, 45)
        Me.btnSaveJson.Name = "btnSaveJson"
        Me.btnSaveJson.Size = New System.Drawing.Size(60, 24)
        Me.btnSaveJson.TabIndex = 2
        Me.btnSaveJson.Text = "Save"
        Me.btnSaveJson.TextAlign = Drawing.ContentAlignment.MiddleRight
        Me.btnSaveJson.UseVisualStyleBackColor = True
        ' 
        ' btnSaveJsonAs
        ' 
        Me.btnSaveJsonAs.DialogResult = DialogResult.TryAgain
        Me.btnSaveJsonAs.Image = My.Resources.Resources.Shell32_1965
        Me.btnSaveJsonAs.ImageAlign = Drawing.ContentAlignment.MiddleLeft
        Me.btnSaveJsonAs.Location = New System.Drawing.Point(73, 45)
        Me.btnSaveJsonAs.Name = "btnSaveJsonAs"
        Me.btnSaveJsonAs.Size = New System.Drawing.Size(76, 24)
        Me.btnSaveJsonAs.TabIndex = 3
        Me.btnSaveJsonAs.Text = "Save As"
        Me.btnSaveJsonAs.TextAlign = Drawing.ContentAlignment.MiddleRight
        Me.btnSaveJsonAs.UseVisualStyleBackColor = True
        ' 
        ' grpMsuTracks
        ' 
        Me.grpMsuTracks.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Me.grpMsuTracks.AutoSizeMode = AutoSizeMode.GrowAndShrink
        Me.grpMsuTracks.Controls.Add(Me.scTracks)
        Me.grpMsuTracks.Location = New System.Drawing.Point(0, 0)
        Me.grpMsuTracks.Name = "grpMsuTracks"
        Me.grpMsuTracks.Size = New System.Drawing.Size(518, 225)
        Me.grpMsuTracks.TabIndex = 7
        Me.grpMsuTracks.TabStop = False
        Me.grpMsuTracks.Text = "Tracks"
        ' 
        ' grpAutoSwitch
        ' 
        Me.grpAutoSwitch.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Me.grpAutoSwitch.Controls.Add(Me.lblAutoSwitchInterval)
        Me.grpAutoSwitch.Controls.Add(Me.lblAutoSwitchIntervalUnit)
        Me.grpAutoSwitch.Controls.Add(Me.nudAutoSwitchInterval)
        Me.grpAutoSwitch.Controls.Add(Me.ctrlEnableAutoSwitch)
        Me.grpAutoSwitch.Location = New System.Drawing.Point(368, 225)
        Me.grpAutoSwitch.Name = "grpAutoSwitch"
        Me.grpAutoSwitch.Size = New System.Drawing.Size(150, 82)
        Me.grpAutoSwitch.TabIndex = 24
        Me.grpAutoSwitch.TabStop = False
        Me.grpAutoSwitch.Text = "AutoSwitch"
        ' 
        ' lblAutoSwitchInterval
        ' 
        Me.lblAutoSwitchInterval.AutoSize = True
        Me.lblAutoSwitchInterval.Location = New System.Drawing.Point(9, 55)
        Me.lblAutoSwitchInterval.Name = "lblAutoSwitchInterval"
        Me.lblAutoSwitchInterval.Size = New System.Drawing.Size(49, 15)
        Me.lblAutoSwitchInterval.TabIndex = 26
        Me.lblAutoSwitchInterval.Text = "Interval:"
        ' 
        ' lblAutoSwitchIntervalUnit
        ' 
        Me.lblAutoSwitchIntervalUnit.AutoSize = True
        Me.lblAutoSwitchIntervalUnit.Location = New System.Drawing.Point(125, 55)
        Me.lblAutoSwitchIntervalUnit.Name = "lblAutoSwitchIntervalUnit"
        Me.lblAutoSwitchIntervalUnit.Size = New System.Drawing.Size(23, 15)
        Me.lblAutoSwitchIntervalUnit.TabIndex = 28
        Me.lblAutoSwitchIntervalUnit.Text = "ms"
        ' 
        ' nudAutoSwitchInterval
        ' 
        Me.nudAutoSwitchInterval.Location = New System.Drawing.Point(64, 53)
        Me.nudAutoSwitchInterval.Maximum = New Decimal(New Integer() {Integer.MaxValue, 0, 0, 0})
        Me.nudAutoSwitchInterval.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudAutoSwitchInterval.Name = "nudAutoSwitchInterval"
        Me.nudAutoSwitchInterval.Size = New System.Drawing.Size(61, 23)
        Me.nudAutoSwitchInterval.TabIndex = 27
        Me.nudAutoSwitchInterval.TextAlign = HorizontalAlignment.Right
        Me.nudAutoSwitchInterval.Value = New Decimal(New Integer() {1000, 0, 0, 0})
        ' 
        ' ctrlEnableAutoSwitch
        ' 
        Me.ctrlEnableAutoSwitch.AutoSize = True
        Me.ctrlEnableAutoSwitch.Location = New System.Drawing.Point(9, 22)
        Me.ctrlEnableAutoSwitch.Name = "ctrlEnableAutoSwitch"
        Me.ctrlEnableAutoSwitch.Size = New System.Drawing.Size(125, 19)
        Me.ctrlEnableAutoSwitch.TabIndex = 25
        Me.ctrlEnableAutoSwitch.Text = "Enable AutoSwitch"
        Me.ctrlEnableAutoSwitch.UseVisualStyleBackColor = True
        ' 
        ' tmrAutoSwitch
        ' 
        Me.tmrAutoSwitch.Interval = 1000
        ' 
        ' ctrlDisplayOnlyTracksWithAlts
        ' 
        Me.ctrlDisplayOnlyTracksWithAlts.AutoSize = True
        Me.ctrlDisplayOnlyTracksWithAlts.Location = New System.Drawing.Point(8, 75)
        Me.ctrlDisplayOnlyTracksWithAlts.Name = "ctrlDisplayOnlyTracksWithAlts"
        Me.ctrlDisplayOnlyTracksWithAlts.Size = New System.Drawing.Size(220, 19)
        Me.ctrlDisplayOnlyTracksWithAlts.TabIndex = 6
        Me.ctrlDisplayOnlyTracksWithAlts.Text = "Hide tracks with no alternative tracks"
        Me.ctrlDisplayOnlyTracksWithAlts.UseVisualStyleBackColor = True
        ' 
        ' scVerticalHalf
        ' 
        Me.scVerticalHalf.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Me.scVerticalHalf.Location = New System.Drawing.Point(5, 95)
        Me.scVerticalHalf.Name = "scVerticalHalf"
        Me.scVerticalHalf.Orientation = Orientation.Horizontal
        ' 
        ' scVerticalHalf.Panel1
        ' 
        Me.scVerticalHalf.Panel1.Controls.Add(Me.grpMsuTracks)
        Me.scVerticalHalf.Panel1.Controls.Add(Me.grpPcmConvert)
        Me.scVerticalHalf.Panel1.Controls.Add(Me.grpAutoSwitch)
        ' 
        ' scVerticalHalf.Panel2
        ' 
        Me.scVerticalHalf.Panel2.Controls.Add(Me.ucMsuLog)
        Me.scVerticalHalf.Size = New System.Drawing.Size(524, 441)
        Me.scVerticalHalf.SplitterDistance = 322
        Me.scVerticalHalf.TabIndex = 82
        ' 
        ' btnSettings
        ' 
        Me.btnSettings.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Me.btnSettings.DialogResult = DialogResult.TryAgain
        Me.btnSettings.Image = My.Resources.Resources.logo_contrast_white_scale_20
        Me.btnSettings.ImageAlign = Drawing.ContentAlignment.MiddleLeft
        Me.btnSettings.Location = New System.Drawing.Point(447, 45)
        Me.btnSettings.Name = "btnSettings"
        Me.btnSettings.Size = New System.Drawing.Size(75, 24)
        Me.btnSettings.TabIndex = 5
        Me.btnSettings.Text = "Settings"
        Me.btnSettings.TextAlign = Drawing.ContentAlignment.MiddleRight
        Me.btnSettings.UseVisualStyleBackColor = True
        ' 
        ' btnScanMsuDirectory
        ' 
        Me.btnScanMsuDirectory.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Me.btnScanMsuDirectory.DialogResult = DialogResult.TryAgain
        Me.btnScanMsuDirectory.Image = My.Resources.Resources.SyncCenter_1223_16px_bw
        Me.btnScanMsuDirectory.ImageAlign = Drawing.ContentAlignment.MiddleLeft
        Me.btnScanMsuDirectory.Location = New System.Drawing.Point(285, 45)
        Me.btnScanMsuDirectory.Name = "btnScanMsuDirectory"
        Me.btnScanMsuDirectory.Size = New System.Drawing.Size(156, 24)
        Me.btnScanMsuDirectory.TabIndex = 4
        Me.btnScanMsuDirectory.Text = "Scan for new PCM Files"
        Me.btnScanMsuDirectory.TextAlign = Drawing.ContentAlignment.MiddleRight
        Me.btnScanMsuDirectory.UseVisualStyleBackColor = True
        ' 
        ' BackgroundWorkerDelegate
        ' 
        ' 
        ' ttpMsuAltSel
        ' 
        Me.ttpMsuAltSel.AutoPopDelay = Integer.MaxValue
        Me.ttpMsuAltSel.InitialDelay = 250
        Me.ttpMsuAltSel.ReshowDelay = 50
        ' 
        ' ctrlDisplayLoopPoints
        ' 
        Me.ctrlDisplayLoopPoints.AutoSize = True
        Me.ctrlDisplayLoopPoints.Location = New System.Drawing.Point(237, 75)
        Me.ctrlDisplayLoopPoints.Name = "ctrlDisplayLoopPoints"
        Me.ctrlDisplayLoopPoints.Size = New System.Drawing.Size(118, 19)
        Me.ctrlDisplayLoopPoints.TabIndex = 7
        Me.ctrlDisplayLoopPoints.Text = "Show loop points"
        Me.ctrlDisplayLoopPoints.UseVisualStyleBackColor = True
        ' 
        ' Msu1AltSelectMainForm
        ' 
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96F, 96F)
        Me.AutoScaleMode = AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(528, 536)
        Me.Controls.Add(Me.btnScanMsuDirectory)
        Me.Controls.Add(Me.btnSettings)
        Me.Controls.Add(Me.txtPathMsu)
        Me.Controls.Add(Me.btnSelPathMsu)
        Me.Controls.Add(Me.ctrlDisplayOnlyTracksWithAlts)
        Me.Controls.Add(Me.ctrlDisplayLoopPoints)
        Me.Controls.Add(Me.btnSaveJsonAs)
        Me.Controls.Add(Me.btnSaveJson)
        Me.Controls.Add(Me.scVerticalHalf)
        Me.Icon = CType(resources.GetObject("$this.Icon"), Drawing.Icon)
        Me.Name = "Msu1AltSelectMainForm"
        Me.Text = "frmMSU1altSel"
        Me.ContextMenuStripMsuConfig.ResumeLayout(False)
        Me.ContextMenuStripTracks.ResumeLayout(False)
        Me.ContextMenuStripAltTracks.ResumeLayout(False)
        Me.scTracks.Panel1.ResumeLayout(False)
        Me.scTracks.Panel2.ResumeLayout(False)
        CType(Me.scTracks, ComponentModel.ISupportInitialize).EndInit()
        Me.scTracks.ResumeLayout(False)
        Me.grpPcmConvert.ResumeLayout(False)
        Me.grpPcmConvert.PerformLayout()
        CType(Me.nudProcessCount, ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudPcmResample, ComponentModel.ISupportInitialize).EndInit()
        Me.frameResample.ResumeLayout(False)
        CType(Me.nudPcmVolume, ComponentModel.ISupportInitialize).EndInit()
        Me.grpMsuTracks.ResumeLayout(False)
        Me.grpAutoSwitch.ResumeLayout(False)
        Me.grpAutoSwitch.PerformLayout()
        CType(Me.nudAutoSwitchInterval, ComponentModel.ISupportInitialize).EndInit()
        Me.scVerticalHalf.Panel1.ResumeLayout(False)
        Me.scVerticalHalf.Panel2.ResumeLayout(False)
        CType(Me.scVerticalHalf, ComponentModel.ISupportInitialize).EndInit()
        Me.scVerticalHalf.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
    Private WithEvents btnSelPathMsu As Button
    Private WithEvents ofdPathMsu As OpenFileDialog
    Private WithEvents txtPathMsu As TextBox
    Private WithEvents lstvTracks As ListView
    Private WithEvents lstvAltTracks As ListView
    Private WithEvents scTracks As SplitContainer
    Private WithEvents sfdLogExport As SaveFileDialog
    Private WithEvents sfdJson As SaveFileDialog
    Private WithEvents btnSaveJson As Button
    Private WithEvents btnSaveJsonAs As Button
    Private WithEvents chAltTrackId As ColumnHeader
    Private WithEvents chAltTrackTitle As ColumnHeader
    Private WithEvents chAltTrackAutoSwitch As ColumnHeader
    Private WithEvents chAltTrackLoopPoint As ColumnHeader
    Private WithEvents chAltTrackLoopPointConverted As ColumnHeader
    Private WithEvents chTrackNumber As ColumnHeader
    Private WithEvents chTrackTitle As ColumnHeader
    Private WithEvents grpMsuTracks As System.Windows.Forms.GroupBox
    Private WithEvents grpAutoSwitch As System.Windows.Forms.GroupBox
    Private WithEvents ctrlDisplayOnlyTracksWithAlts As System.Windows.Forms.CheckBox
    Private WithEvents ctrlEnableAutoSwitch As System.Windows.Forms.CheckBox
    Private WithEvents nudAutoSwitchInterval As NumericUpDown
    Private WithEvents lblAutoSwitchInterval As Label
    Private WithEvents lblAutoSwitchIntervalUnit As Label
    Private WithEvents tmrAutoSwitch As Timer
    Private WithEvents scVerticalHalf As SplitContainer
    Private WithEvents btnSettings As Button
    Private WithEvents objLogger As MsuAltSelect.Logger.Logger
    Private WithEvents btnScanMsuDirectory As Button
    Private WithEvents btnPcmToNormal As Button
    Private WithEvents btnConvertPcm As Button
    Private WithEvents nudPcmResample As NumericUpDown
    Private WithEvents nudPcmVolume As NumericUpDown
    Private WithEvents frameResample As Panel
    Private WithEvents optPcmNormal As RadioButton
    Private WithEvents optPcmConverted As RadioButton
    Private WithEvents lblHz As Label
    Private WithEvents lblPcmVolume As Label
    Private WithEvents nudProcessCount As NumericUpDown
    Private WithEvents lblProcessCount As Label
    Private WithEvents BackgroundWorkerDelegate As System.ComponentModel.BackgroundWorker
    Private WithEvents grpPcmConvert As GroupBox
    Private WithEvents ctrlKeepCmdOpen As CheckBox
    Private WithEvents ctrlDisplayCmd As CheckBox

    Private WithEvents EditTrackToolStripMenuItem As ToolStripMenuItem
    Private WithEvents DeleteTrackToolStripMenuItem As ToolStripMenuItem
    Private WithEvents ContextMenuStripTracks As ContextMenuStrip
    Private WithEvents ContextMenuStripAltTracks As ContextMenuStrip
    Private WithEvents OpenAltTrackLocationToolStripMenuItem As ToolStripMenuItem
    Private WithEvents SetAsCurrentTrackToolStripMenuItem As ToolStripMenuItem
    Private WithEvents AltTrackToolStripSeparator1 As ToolStripSeparator
    Private WithEvents AltTrackToolStripSeparator2 As ToolStripSeparator
    Private WithEvents EditAltTrackToolStripMenuItem As ToolStripMenuItem
    Private WithEvents DeleteAltTrackToolStripMenuItem As ToolStripMenuItem
    Private WithEvents ContextMenuStripMsuConfig As ContextMenuStrip
    Private WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Private WithEvents ToolStripSeparator1 As ToolStripSeparator
    Private WithEvents OpenMsuLocationToolStripMenuItem As ToolStripMenuItem
    Private WithEvents ToolStripMenuItem3 As ToolStripMenuItem
    Private WithEvents ToolStripMenuItem4 As ToolStripMenuItem
    Private WithEvents AddNewAltTrackToolStripMenuItem As ToolStripMenuItem
    Private WithEvents ttpMsuAltSel As ToolTip
    Private WithEvents ctrlDisplayLoopPoints As CheckBox
    Friend WithEvents ucMsuLog As MsuLogControl
End Class
