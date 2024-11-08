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
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(Msu1AltSelectMainForm))
        sfdLogExport = New SaveFileDialog()
        sfdJson = New SaveFileDialog()
        btnSelPathMsu = New Button()
        ofdPathMsu = New OpenFileDialog()
        txtPathMsu = New TextBox()
        ContextMenuStripMsuConfig = New ContextMenuStrip(Me.components)
        OpenMsuLocationToolStripMenuItem = New ToolStripMenuItem()
        lstvTracks = New ListView()
        chTrackNumber = New ColumnHeader()
        chTrackTitle = New ColumnHeader()
        ContextMenuStripTracks = New ContextMenuStrip(Me.components)
        EditTrackToolStripMenuItem = New ToolStripMenuItem()
        DeleteTrackToolStripMenuItem = New ToolStripMenuItem()
        lstvAltTracks = New ListView()
        chAltTrackId = New ColumnHeader()
        chAltTrackTitle = New ColumnHeader()
        ContextMenuStripAltTracks = New ContextMenuStrip(Me.components)
        SetAsCurrentTrackToolStripMenuItem = New ToolStripMenuItem()
        AltTrackToolStripSeparator1 = New ToolStripSeparator()
        OpenAltTrackLocationToolStripMenuItem = New ToolStripMenuItem()
        EditAltTrackToolStripMenuItem = New ToolStripMenuItem()
        DeleteAltTrackToolStripMenuItem = New ToolStripMenuItem()
        AltTrackToolStripSeparator2 = New ToolStripSeparator()
        AddNewAltTrackToolStripMenuItem = New ToolStripMenuItem()
        chAltTrackAutoSwitch = New ColumnHeader()
        scTracks = New SplitContainer()
        grpPcmConvert = New GroupBox()
        ctrlDisplayCmd = New CheckBox()
        nudProcessCount = New NumericUpDown()
        nudPcmResample = New NumericUpDown()
        lblProcessCount = New Label()
        btnPcmToNormal = New Button()
        lblHz = New Label()
        btnConvertPcm = New Button()
        frameResample = New Panel()
        optPcmNormal = New RadioButton()
        optPcmConverted = New RadioButton()
        nudPcmVolume = New NumericUpDown()
        lblPcmVolume = New Label()
        ctrlKeepCmdOpen = New CheckBox()
        btnSaveJson = New Button()
        btnSaveJsonAs = New Button()
        grpMsuTracks = New GroupBox()
        grpAutoSwitch = New GroupBox()
        lblAutoSwitchInterval = New Label()
        lblAutoSwitchIntervalUnit = New Label()
        nudAutoSwitchInterval = New NumericUpDown()
        ctrlEnableAutoSwitch = New CheckBox()
        tmrAutoSwitch = New Timer(Me.components)
        ctrlDisplayOnlyTracksWithAlts = New CheckBox()
        scVerticalHalf = New SplitContainer()
        Me.rtbLog = New Logger.ScrollingRichTextBox()
        grpLogSettings = New GroupBox()
        ctrlLogAutoScroll = New CheckBox()
        scLogSettingButtons = New SplitContainer()
        btnLogClear = New Button()
        btnLogExport = New Button()
        lblLogEntries = New Label()
        nudLogEntries = New NumericUpDown()
        btnSettings = New Button()
        btnScanMsuDirectory = New Button()
        Me.BackgroundWorkerDelegate = New ComponentModel.BackgroundWorker()
        ttpMsuAltSel = New ToolTip(Me.components)
        ContextMenuStripMsuConfig.SuspendLayout()
        ContextMenuStripTracks.SuspendLayout()
        ContextMenuStripAltTracks.SuspendLayout()
        CType(scTracks, ComponentModel.ISupportInitialize).BeginInit()
        scTracks.Panel1.SuspendLayout()
        scTracks.Panel2.SuspendLayout()
        scTracks.SuspendLayout()
        grpPcmConvert.SuspendLayout()
        CType(nudProcessCount, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudPcmResample, ComponentModel.ISupportInitialize).BeginInit()
        frameResample.SuspendLayout()
        CType(nudPcmVolume, ComponentModel.ISupportInitialize).BeginInit()
        grpMsuTracks.SuspendLayout()
        grpAutoSwitch.SuspendLayout()
        CType(nudAutoSwitchInterval, ComponentModel.ISupportInitialize).BeginInit()
        CType(scVerticalHalf, ComponentModel.ISupportInitialize).BeginInit()
        scVerticalHalf.Panel1.SuspendLayout()
        scVerticalHalf.Panel2.SuspendLayout()
        scVerticalHalf.SuspendLayout()
        grpLogSettings.SuspendLayout()
        CType(scLogSettingButtons, ComponentModel.ISupportInitialize).BeginInit()
        scLogSettingButtons.Panel1.SuspendLayout()
        scLogSettingButtons.Panel2.SuspendLayout()
        scLogSettingButtons.SuspendLayout()
        CType(nudLogEntries, ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        ' 
        ' sfdLogExport
        ' 
        sfdLogExport.DefaultExt = "rtf"
        sfdLogExport.Filter = "Rich-Text-Format|*.rtf|Plain text|*.txt"
        sfdLogExport.Title = "Export the current log to a file."
        ' 
        ' sfdJson
        ' 
        sfdJson.DefaultExt = "json"
        sfdJson.Filter = "JSON config|*.json"
        sfdJson.Title = "Export the current MSU configuration as JSON"
        ' 
        ' btnSelPathMsu
        ' 
        btnSelPathMsu.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnSelPathMsu.Location = New Drawing.Point(435, 12)
        btnSelPathMsu.Name = "btnSelPathMsu"
        btnSelPathMsu.Size = New Drawing.Size(87, 24)
        btnSelPathMsu.TabIndex = 1
        btnSelPathMsu.Text = "Select MSU1"
        btnSelPathMsu.UseVisualStyleBackColor = True
        ' 
        ' ofdPathMsu
        ' 
        ofdPathMsu.Filter = "All Files|*.*|All|*.json;*.msu;*.sfc;*.smc|JSON config|*.json|MSU file|*.msu|SNES ROM|*.sfc;*.smc"
        ofdPathMsu.FilterIndex = 2
        ofdPathMsu.Title = "Select the .MSU file or the MSU1 patched ROM (Or anything that has the same name as the ROM)"
        ' 
        ' txtPathMsu
        ' 
        txtPathMsu.AllowDrop = True
        txtPathMsu.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtPathMsu.ContextMenuStrip = ContextMenuStripMsuConfig
        txtPathMsu.Location = New Drawing.Point(6, 12)
        txtPathMsu.Name = "txtPathMsu"
        txtPathMsu.ReadOnly = True
        txtPathMsu.Size = New Drawing.Size(423, 23)
        txtPathMsu.TabIndex = 0
        txtPathMsu.WordWrap = False
        ' 
        ' ContextMenuStripMsuConfig
        ' 
        ContextMenuStripMsuConfig.Items.AddRange(New ToolStripItem() {OpenMsuLocationToolStripMenuItem})
        ContextMenuStripMsuConfig.Name = "ContextMenuStripMsuConfig"
        ContextMenuStripMsuConfig.Size = New Drawing.Size(153, 26)
        ' 
        ' OpenMsuLocationToolStripMenuItem
        ' 
        OpenMsuLocationToolStripMenuItem.Name = "OpenMsuLocationToolStripMenuItem"
        OpenMsuLocationToolStripMenuItem.Size = New Drawing.Size(152, 22)
        OpenMsuLocationToolStripMenuItem.Text = "Open Location"
        ' 
        ' lstvTracks
        ' 
        lstvTracks.AccessibleRole = AccessibleRole.List
        lstvTracks.Alignment = ListViewAlignment.SnapToGrid
        lstvTracks.AllowColumnReorder = True
        lstvTracks.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        lstvTracks.BackgroundImageTiled = True
        lstvTracks.Columns.AddRange(New ColumnHeader() {chTrackNumber, chTrackTitle})
        lstvTracks.ContextMenuStrip = ContextMenuStripTracks
        lstvTracks.FullRowSelect = True
        lstvTracks.GridLines = True
        lstvTracks.LabelWrap = False
        lstvTracks.Location = New Drawing.Point(3, 0)
        lstvTracks.MultiSelect = False
        lstvTracks.Name = "lstvTracks"
        lstvTracks.ShowItemToolTips = True
        lstvTracks.Size = New Drawing.Size(153, 203)
        lstvTracks.TabIndex = 8
        lstvTracks.UseCompatibleStateImageBehavior = False
        lstvTracks.View = View.Details
        ' 
        ' chTrackNumber
        ' 
        chTrackNumber.Text = "Id"
        chTrackNumber.Width = 25
        ' 
        ' chTrackTitle
        ' 
        chTrackTitle.Text = "Track title"
        chTrackTitle.Width = 150
        ' 
        ' ContextMenuStripTracks
        ' 
        ContextMenuStripTracks.Items.AddRange(New ToolStripItem() {EditTrackToolStripMenuItem, DeleteTrackToolStripMenuItem})
        ContextMenuStripTracks.Name = "ContextMenuStripTracks"
        ContextMenuStripTracks.Size = New Drawing.Size(132, 48)
        ' 
        ' EditTrackToolStripMenuItem
        ' 
        EditTrackToolStripMenuItem.Name = "EditTrackToolStripMenuItem"
        EditTrackToolStripMenuItem.Size = New Drawing.Size(131, 22)
        EditTrackToolStripMenuItem.Text = "Edit"
        ' 
        ' DeleteTrackToolStripMenuItem
        ' 
        DeleteTrackToolStripMenuItem.Name = "DeleteTrackToolStripMenuItem"
        DeleteTrackToolStripMenuItem.ShortcutKeys = Keys.Delete
        DeleteTrackToolStripMenuItem.Size = New Drawing.Size(131, 22)
        DeleteTrackToolStripMenuItem.Text = "Delete"
        ' 
        ' lstvAltTracks
        ' 
        lstvAltTracks.AccessibleRole = AccessibleRole.List
        lstvAltTracks.Alignment = ListViewAlignment.SnapToGrid
        lstvAltTracks.AllowColumnReorder = True
        lstvAltTracks.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        lstvAltTracks.CheckBoxes = True
        lstvAltTracks.Columns.AddRange(New ColumnHeader() {chAltTrackId, chAltTrackTitle})
        lstvAltTracks.ContextMenuStrip = ContextMenuStripAltTracks
        lstvAltTracks.FullRowSelect = True
        lstvAltTracks.GridLines = True
        lstvAltTracks.Location = New Drawing.Point(3, 0)
        lstvAltTracks.MultiSelect = False
        lstvAltTracks.Name = "lstvAltTracks"
        lstvAltTracks.ShowItemToolTips = True
        lstvAltTracks.Size = New Drawing.Size(342, 203)
        lstvAltTracks.TabIndex = 9
        lstvAltTracks.UseCompatibleStateImageBehavior = False
        lstvAltTracks.View = View.Details
        ' 
        ' chAltTrackId
        ' 
        chAltTrackId.Text = "Id"
        chAltTrackId.Width = 40
        ' 
        ' chAltTrackTitle
        ' 
        chAltTrackTitle.Text = "alt. Track title"
        chAltTrackTitle.Width = 150
        ' 
        ' ContextMenuStripAltTracks
        ' 
        ContextMenuStripAltTracks.Items.AddRange(New ToolStripItem() {SetAsCurrentTrackToolStripMenuItem, AltTrackToolStripSeparator1, OpenAltTrackLocationToolStripMenuItem, EditAltTrackToolStripMenuItem, DeleteAltTrackToolStripMenuItem, AltTrackToolStripSeparator2, AddNewAltTrackToolStripMenuItem})
        ContextMenuStripAltTracks.Name = "ContextMenuStripAltTracks"
        ContextMenuStripAltTracks.Size = New Drawing.Size(177, 126)
        ' 
        ' SetAsCurrentTrackToolStripMenuItem
        ' 
        SetAsCurrentTrackToolStripMenuItem.Font = New Drawing.Font("Segoe UI", 9F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point)
        SetAsCurrentTrackToolStripMenuItem.Name = "SetAsCurrentTrackToolStripMenuItem"
        SetAsCurrentTrackToolStripMenuItem.ShowShortcutKeys = False
        SetAsCurrentTrackToolStripMenuItem.Size = New Drawing.Size(176, 22)
        SetAsCurrentTrackToolStripMenuItem.Text = "Switch to this Track"
        ' 
        ' AltTrackToolStripSeparator1
        ' 
        AltTrackToolStripSeparator1.Name = "AltTrackToolStripSeparator1"
        AltTrackToolStripSeparator1.Size = New Drawing.Size(173, 6)
        ' 
        ' OpenAltTrackLocationToolStripMenuItem
        ' 
        OpenAltTrackLocationToolStripMenuItem.Name = "OpenAltTrackLocationToolStripMenuItem"
        OpenAltTrackLocationToolStripMenuItem.Size = New Drawing.Size(176, 22)
        OpenAltTrackLocationToolStripMenuItem.Text = "Open Location"
        ' 
        ' EditAltTrackToolStripMenuItem
        ' 
        EditAltTrackToolStripMenuItem.Name = "EditAltTrackToolStripMenuItem"
        EditAltTrackToolStripMenuItem.Size = New Drawing.Size(176, 22)
        EditAltTrackToolStripMenuItem.Text = "Edit"
        ' 
        ' DeleteAltTrackToolStripMenuItem
        ' 
        DeleteAltTrackToolStripMenuItem.Name = "DeleteAltTrackToolStripMenuItem"
        DeleteAltTrackToolStripMenuItem.ShortcutKeys = Keys.Delete
        DeleteAltTrackToolStripMenuItem.Size = New Drawing.Size(176, 22)
        DeleteAltTrackToolStripMenuItem.Text = "Delete"
        ' 
        ' AltTrackToolStripSeparator2
        ' 
        AltTrackToolStripSeparator2.Name = "AltTrackToolStripSeparator2"
        AltTrackToolStripSeparator2.Size = New Drawing.Size(173, 6)
        ' 
        ' AddNewAltTrackToolStripMenuItem
        ' 
        AddNewAltTrackToolStripMenuItem.Name = "AddNewAltTrackToolStripMenuItem"
        AddNewAltTrackToolStripMenuItem.Size = New Drawing.Size(176, 22)
        AddNewAltTrackToolStripMenuItem.Text = "Add New"
        ' 
        ' chAltTrackAutoSwitch
        ' 
        chAltTrackAutoSwitch.Text = "Auto Switch"
        chAltTrackAutoSwitch.Width = 80
        ' 
        ' scTracks
        ' 
        scTracks.Dock = DockStyle.Fill
        scTracks.Location = New Drawing.Point(3, 19)
        scTracks.Name = "scTracks"
        ' 
        ' scTracks.Panel1
        ' 
        scTracks.Panel1.Controls.Add(lstvTracks)
        ' 
        ' scTracks.Panel2
        ' 
        scTracks.Panel2.Controls.Add(lstvAltTracks)
        scTracks.Size = New Drawing.Size(512, 203)
        scTracks.SplitterDistance = 158
        scTracks.TabIndex = 0
        ' 
        ' grpPcmConvert
        ' 
        grpPcmConvert.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        grpPcmConvert.Controls.Add(ctrlDisplayCmd)
        grpPcmConvert.Controls.Add(nudProcessCount)
        grpPcmConvert.Controls.Add(nudPcmResample)
        grpPcmConvert.Controls.Add(lblProcessCount)
        grpPcmConvert.Controls.Add(btnPcmToNormal)
        grpPcmConvert.Controls.Add(lblHz)
        grpPcmConvert.Controls.Add(btnConvertPcm)
        grpPcmConvert.Controls.Add(frameResample)
        grpPcmConvert.Controls.Add(nudPcmVolume)
        grpPcmConvert.Controls.Add(lblPcmVolume)
        grpPcmConvert.Controls.Add(ctrlKeepCmdOpen)
        grpPcmConvert.Location = New Drawing.Point(0, 225)
        grpPcmConvert.Name = "grpPcmConvert"
        grpPcmConvert.Size = New Drawing.Size(365, 96)
        grpPcmConvert.TabIndex = 10
        grpPcmConvert.TabStop = False
        grpPcmConvert.Text = "Convert PCM files"
        ' 
        ' ctrlDisplayCmd
        ' 
        ctrlDisplayCmd.AutoSize = True
        ctrlDisplayCmd.Location = New Drawing.Point(6, 71)
        ctrlDisplayCmd.Name = "ctrlDisplayCmd"
        ctrlDisplayCmd.Size = New Drawing.Size(137, 19)
        ctrlDisplayCmd.TabIndex = 17
        ctrlDisplayCmd.Text = "Show CMD Windows"
        ctrlDisplayCmd.UseVisualStyleBackColor = True
        ' 
        ' nudProcessCount
        ' 
        nudProcessCount.BackColor = Drawing.SystemColors.Window
        nudProcessCount.CausesValidation = False
        nudProcessCount.Cursor = Cursors.IBeam
        nudProcessCount.ForeColor = Drawing.SystemColors.WindowText
        nudProcessCount.Location = New Drawing.Point(91, 19)
        nudProcessCount.Margin = New Padding(4, 0, 4, 3)
        nudProcessCount.Maximum = New Decimal(New Integer() {64, 0, 0, 0})
        nudProcessCount.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nudProcessCount.Name = "nudProcessCount"
        nudProcessCount.RightToLeft = RightToLeft.No
        nudProcessCount.Size = New Drawing.Size(35, 23)
        nudProcessCount.TabIndex = 13
        nudProcessCount.TextAlign = HorizontalAlignment.Right
        nudProcessCount.Value = New Decimal(New Integer() {64, 0, 0, 0})
        ' 
        ' nudPcmResample
        ' 
        nudPcmResample.BackColor = Drawing.SystemColors.Window
        nudPcmResample.CausesValidation = False
        nudPcmResample.Cursor = Cursors.IBeam
        nudPcmResample.ForeColor = Drawing.SystemColors.WindowText
        nudPcmResample.Location = New Drawing.Point(6, 19)
        nudPcmResample.Margin = New Padding(4, 0, 4, 3)
        nudPcmResample.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        nudPcmResample.Minimum = New Decimal(New Integer() {1000, 0, 0, 0})
        nudPcmResample.Name = "nudPcmResample"
        nudPcmResample.RightToLeft = RightToLeft.No
        nudPcmResample.Size = New Drawing.Size(66, 23)
        nudPcmResample.TabIndex = 11
        nudPcmResample.TextAlign = HorizontalAlignment.Right
        nudPcmResample.Value = New Decimal(New Integer() {44100, 0, 0, 0})
        ' 
        ' lblProcessCount
        ' 
        lblProcessCount.BackColor = Drawing.Color.Transparent
        lblProcessCount.ForeColor = Drawing.SystemColors.ControlText
        lblProcessCount.Location = New Drawing.Point(124, 21)
        lblProcessCount.Margin = New Padding(4, 0, 4, 0)
        lblProcessCount.Name = "lblProcessCount"
        lblProcessCount.RightToLeft = RightToLeft.No
        lblProcessCount.Size = New Drawing.Size(58, 20)
        lblProcessCount.TabIndex = 14
        lblProcessCount.Text = "Processes"
        ' 
        ' btnPcmToNormal
        ' 
        btnPcmToNormal.BackColor = Drawing.SystemColors.Control
        btnPcmToNormal.ForeColor = Drawing.SystemColors.ControlText
        btnPcmToNormal.Location = New Drawing.Point(194, 19)
        btnPcmToNormal.Margin = New Padding(4, 0, 4, 3)
        btnPcmToNormal.Name = "btnPcmToNormal"
        btnPcmToNormal.RightToLeft = RightToLeft.No
        btnPcmToNormal.Size = New Drawing.Size(150, 23)
        btnPcmToNormal.TabIndex = 19
        btnPcmToNormal.Text = "Change Back to Normal"
        btnPcmToNormal.UseVisualStyleBackColor = False
        ' 
        ' lblHz
        ' 
        lblHz.BackColor = Drawing.Color.Transparent
        lblHz.ForeColor = Drawing.SystemColors.ControlText
        lblHz.Location = New Drawing.Point(70, 21)
        lblHz.Margin = New Padding(4, 0, 4, 0)
        lblHz.Name = "lblHz"
        lblHz.RightToLeft = RightToLeft.No
        lblHz.Size = New Drawing.Size(28, 20)
        lblHz.TabIndex = 12
        lblHz.Text = "Hz"
        ' 
        ' btnConvertPcm
        ' 
        btnConvertPcm.BackColor = Drawing.SystemColors.Control
        btnConvertPcm.ForeColor = Drawing.SystemColors.ControlText
        btnConvertPcm.Location = New Drawing.Point(194, 41)
        btnConvertPcm.Margin = New Padding(4, 3, 4, 3)
        btnConvertPcm.Name = "btnConvertPcm"
        btnConvertPcm.RightToLeft = RightToLeft.No
        btnConvertPcm.Size = New Drawing.Size(150, 23)
        btnConvertPcm.TabIndex = 20
        btnConvertPcm.Text = "Change Speed/Volume"
        btnConvertPcm.UseVisualStyleBackColor = False
        ' 
        ' frameResample
        ' 
        frameResample.BackColor = Drawing.SystemColors.Control
        frameResample.Controls.Add(optPcmNormal)
        frameResample.Controls.Add(optPcmConverted)
        frameResample.ForeColor = Drawing.SystemColors.ControlText
        frameResample.Location = New Drawing.Point(345, 20)
        frameResample.Margin = New Padding(4, 0, 4, 3)
        frameResample.Name = "frameResample"
        frameResample.RightToLeft = RightToLeft.No
        frameResample.Size = New Drawing.Size(15, 42)
        frameResample.TabIndex = 21
        ' 
        ' optPcmNormal
        ' 
        optPcmNormal.BackColor = Drawing.SystemColors.Control
        optPcmNormal.Enabled = False
        optPcmNormal.ForeColor = Drawing.SystemColors.ControlText
        optPcmNormal.Location = New Drawing.Point(0, 3)
        optPcmNormal.Margin = New Padding(4, 3, 4, 3)
        optPcmNormal.Name = "optPcmNormal"
        optPcmNormal.RightToLeft = RightToLeft.No
        optPcmNormal.Size = New Drawing.Size(15, 15)
        optPcmNormal.TabIndex = 22
        optPcmNormal.TabStop = True
        optPcmNormal.UseVisualStyleBackColor = False
        ' 
        ' optPcmConverted
        ' 
        optPcmConverted.BackColor = Drawing.SystemColors.Control
        optPcmConverted.Enabled = False
        optPcmConverted.ForeColor = Drawing.SystemColors.ControlText
        optPcmConverted.Location = New Drawing.Point(0, 25)
        optPcmConverted.Margin = New Padding(4, 3, 4, 3)
        optPcmConverted.Name = "optPcmConverted"
        optPcmConverted.RightToLeft = RightToLeft.No
        optPcmConverted.Size = New Drawing.Size(15, 15)
        optPcmConverted.TabIndex = 23
        optPcmConverted.TabStop = True
        optPcmConverted.UseVisualStyleBackColor = False
        ' 
        ' nudPcmVolume
        ' 
        nudPcmVolume.BackColor = Drawing.SystemColors.Window
        nudPcmVolume.CausesValidation = False
        nudPcmVolume.Cursor = Cursors.IBeam
        nudPcmVolume.ForeColor = Drawing.SystemColors.WindowText
        nudPcmVolume.Location = New Drawing.Point(6, 41)
        nudPcmVolume.Margin = New Padding(4, 3, 4, 3)
        nudPcmVolume.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        nudPcmVolume.Name = "nudPcmVolume"
        nudPcmVolume.RightToLeft = RightToLeft.No
        nudPcmVolume.Size = New Drawing.Size(66, 23)
        nudPcmVolume.TabIndex = 15
        nudPcmVolume.TextAlign = HorizontalAlignment.Right
        nudPcmVolume.Value = New Decimal(New Integer() {100, 0, 0, 0})
        ' 
        ' lblPcmVolume
        ' 
        lblPcmVolume.BackColor = Drawing.Color.Transparent
        lblPcmVolume.ForeColor = Drawing.SystemColors.ControlText
        lblPcmVolume.Location = New Drawing.Point(70, 43)
        lblPcmVolume.Margin = New Padding(4, 0, 4, 0)
        lblPcmVolume.Name = "lblPcmVolume"
        lblPcmVolume.RightToLeft = RightToLeft.No
        lblPcmVolume.Size = New Drawing.Size(65, 20)
        lblPcmVolume.TabIndex = 16
        lblPcmVolume.Text = "% Volume"
        ' 
        ' ctrlKeepCmdOpen
        ' 
        ctrlKeepCmdOpen.AutoSize = True
        ctrlKeepCmdOpen.Location = New Drawing.Point(148, 71)
        ctrlKeepCmdOpen.Name = "ctrlKeepCmdOpen"
        ctrlKeepCmdOpen.Size = New Drawing.Size(210, 19)
        ctrlKeepCmdOpen.TabIndex = 18
        ctrlKeepCmdOpen.Text = "Keep CMD Windows open (Debug)"
        ctrlKeepCmdOpen.UseVisualStyleBackColor = True
        ' 
        ' btnSaveJson
        ' 
        btnSaveJson.DialogResult = DialogResult.TryAgain
        btnSaveJson.Image = My.Resources.Resources.Shell32_1965
        btnSaveJson.ImageAlign = Drawing.ContentAlignment.TopLeft
        btnSaveJson.Location = New Drawing.Point(6, 45)
        btnSaveJson.Name = "btnSaveJson"
        btnSaveJson.Size = New Drawing.Size(60, 24)
        btnSaveJson.TabIndex = 2
        btnSaveJson.Text = "Save"
        btnSaveJson.TextAlign = Drawing.ContentAlignment.MiddleRight
        btnSaveJson.UseVisualStyleBackColor = True
        ' 
        ' btnSaveJsonAs
        ' 
        btnSaveJsonAs.DialogResult = DialogResult.TryAgain
        btnSaveJsonAs.Image = My.Resources.Resources.Shell32_1965
        btnSaveJsonAs.ImageAlign = Drawing.ContentAlignment.TopLeft
        btnSaveJsonAs.Location = New Drawing.Point(73, 45)
        btnSaveJsonAs.Name = "btnSaveJsonAs"
        btnSaveJsonAs.Size = New Drawing.Size(76, 24)
        btnSaveJsonAs.TabIndex = 3
        btnSaveJsonAs.Text = "Save As"
        btnSaveJsonAs.TextAlign = Drawing.ContentAlignment.MiddleRight
        btnSaveJsonAs.UseVisualStyleBackColor = True
        ' 
        ' grpMsuTracks
        ' 
        grpMsuTracks.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        grpMsuTracks.AutoSizeMode = AutoSizeMode.GrowAndShrink
        grpMsuTracks.Controls.Add(scTracks)
        grpMsuTracks.Location = New Drawing.Point(0, 0)
        grpMsuTracks.Name = "grpMsuTracks"
        grpMsuTracks.Size = New Drawing.Size(518, 225)
        grpMsuTracks.TabIndex = 7
        grpMsuTracks.TabStop = False
        grpMsuTracks.Text = "Tracks"
        ' 
        ' grpAutoSwitch
        ' 
        grpAutoSwitch.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        grpAutoSwitch.Controls.Add(lblAutoSwitchInterval)
        grpAutoSwitch.Controls.Add(lblAutoSwitchIntervalUnit)
        grpAutoSwitch.Controls.Add(nudAutoSwitchInterval)
        grpAutoSwitch.Controls.Add(ctrlEnableAutoSwitch)
        grpAutoSwitch.Location = New Drawing.Point(368, 225)
        grpAutoSwitch.Name = "grpAutoSwitch"
        grpAutoSwitch.Size = New Drawing.Size(150, 82)
        grpAutoSwitch.TabIndex = 24
        grpAutoSwitch.TabStop = False
        grpAutoSwitch.Text = "AutoSwitch"
        ' 
        ' lblAutoSwitchInterval
        ' 
        lblAutoSwitchInterval.AutoSize = True
        lblAutoSwitchInterval.Location = New Drawing.Point(9, 55)
        lblAutoSwitchInterval.Name = "lblAutoSwitchInterval"
        lblAutoSwitchInterval.Size = New Drawing.Size(49, 15)
        lblAutoSwitchInterval.TabIndex = 26
        lblAutoSwitchInterval.Text = "Interval:"
        ' 
        ' lblAutoSwitchIntervalUnit
        ' 
        lblAutoSwitchIntervalUnit.AutoSize = True
        lblAutoSwitchIntervalUnit.Location = New Drawing.Point(125, 55)
        lblAutoSwitchIntervalUnit.Name = "lblAutoSwitchIntervalUnit"
        lblAutoSwitchIntervalUnit.Size = New Drawing.Size(23, 15)
        lblAutoSwitchIntervalUnit.TabIndex = 28
        lblAutoSwitchIntervalUnit.Text = "ms"
        ' 
        ' nudAutoSwitchInterval
        ' 
        nudAutoSwitchInterval.Location = New Drawing.Point(64, 53)
        nudAutoSwitchInterval.Maximum = New Decimal(New Integer() {Integer.MaxValue, 0, 0, 0})
        nudAutoSwitchInterval.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nudAutoSwitchInterval.Name = "nudAutoSwitchInterval"
        nudAutoSwitchInterval.Size = New Drawing.Size(61, 23)
        nudAutoSwitchInterval.TabIndex = 27
        nudAutoSwitchInterval.TextAlign = HorizontalAlignment.Right
        nudAutoSwitchInterval.Value = New Decimal(New Integer() {1000, 0, 0, 0})
        ' 
        ' ctrlEnableAutoSwitch
        ' 
        ctrlEnableAutoSwitch.AutoSize = True
        ctrlEnableAutoSwitch.Location = New Drawing.Point(9, 22)
        ctrlEnableAutoSwitch.Name = "ctrlEnableAutoSwitch"
        ctrlEnableAutoSwitch.Size = New Drawing.Size(125, 19)
        ctrlEnableAutoSwitch.TabIndex = 25
        ctrlEnableAutoSwitch.Text = "Enable AutoSwitch"
        ctrlEnableAutoSwitch.UseVisualStyleBackColor = True
        ' 
        ' tmrAutoSwitch
        ' 
        tmrAutoSwitch.Interval = 1000
        ' 
        ' ctrlDisplayOnlyTracksWithAlts
        ' 
        ctrlDisplayOnlyTracksWithAlts.AutoSize = True
        ctrlDisplayOnlyTracksWithAlts.Location = New Drawing.Point(8, 75)
        ctrlDisplayOnlyTracksWithAlts.Name = "ctrlDisplayOnlyTracksWithAlts"
        ctrlDisplayOnlyTracksWithAlts.Size = New Drawing.Size(220, 19)
        ctrlDisplayOnlyTracksWithAlts.TabIndex = 6
        ctrlDisplayOnlyTracksWithAlts.Text = "Hide tracks with no alternative tracks"
        ctrlDisplayOnlyTracksWithAlts.UseVisualStyleBackColor = True
        ' 
        ' scVerticalHalf
        ' 
        scVerticalHalf.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        scVerticalHalf.Location = New Drawing.Point(5, 95)
        scVerticalHalf.Name = "scVerticalHalf"
        scVerticalHalf.Orientation = Orientation.Horizontal
        ' 
        ' scVerticalHalf.Panel1
        ' 
        scVerticalHalf.Panel1.Controls.Add(grpMsuTracks)
        scVerticalHalf.Panel1.Controls.Add(grpPcmConvert)
        scVerticalHalf.Panel1.Controls.Add(grpAutoSwitch)
        ' 
        ' scVerticalHalf.Panel2
        ' 
        scVerticalHalf.Panel2.Controls.Add(Me.rtbLog)
        scVerticalHalf.Panel2.Controls.Add(grpLogSettings)
        scVerticalHalf.Size = New Drawing.Size(524, 441)
        scVerticalHalf.SplitterDistance = 322
        scVerticalHalf.TabIndex = 82
        ' 
        ' rtbLog
        ' 
        Me.rtbLog.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Me.rtbLog.Location = New Drawing.Point(0, 0)
        Me.rtbLog.Margin = New Padding(0)
        Me.rtbLog.Name = "rtbLog"
        Me.rtbLog.Size = New Drawing.Size(400, 113)
        Me.rtbLog.TabIndex = 29
        Me.rtbLog.Text = ""
        ' 
        ' grpLogSettings
        ' 
        grpLogSettings.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        grpLogSettings.Controls.Add(ctrlLogAutoScroll)
        grpLogSettings.Controls.Add(scLogSettingButtons)
        grpLogSettings.Controls.Add(lblLogEntries)
        grpLogSettings.Controls.Add(nudLogEntries)
        grpLogSettings.Location = New Drawing.Point(402, -8)
        grpLogSettings.Margin = New Padding(3, 0, 3, 3)
        grpLogSettings.Name = "grpLogSettings"
        grpLogSettings.Size = New Drawing.Size(116, 121)
        grpLogSettings.TabIndex = 1
        grpLogSettings.TabStop = False
        ' 
        ' ctrlLogAutoScroll
        ' 
        ctrlLogAutoScroll.AutoSize = True
        ctrlLogAutoScroll.Checked = True
        ctrlLogAutoScroll.CheckState = CheckState.Checked
        ctrlLogAutoScroll.Location = New Drawing.Point(7, 42)
        ctrlLogAutoScroll.Name = "ctrlLogAutoScroll"
        ctrlLogAutoScroll.Size = New Drawing.Size(84, 19)
        ctrlLogAutoScroll.TabIndex = 32
        ctrlLogAutoScroll.Text = "Auto Scroll"
        ctrlLogAutoScroll.UseVisualStyleBackColor = True
        ' 
        ' scLogSettingButtons
        ' 
        scLogSettingButtons.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        scLogSettingButtons.Location = New Drawing.Point(1, 63)
        scLogSettingButtons.Name = "scLogSettingButtons"
        scLogSettingButtons.Orientation = Orientation.Horizontal
        ' 
        ' scLogSettingButtons.Panel1
        ' 
        scLogSettingButtons.Panel1.Controls.Add(btnLogClear)
        ' 
        ' scLogSettingButtons.Panel2
        ' 
        scLogSettingButtons.Panel2.Controls.Add(btnLogExport)
        scLogSettingButtons.Size = New Drawing.Size(114, 54)
        scLogSettingButtons.SplitterDistance = 25
        scLogSettingButtons.TabIndex = 33
        ' 
        ' btnLogClear
        ' 
        btnLogClear.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        btnLogClear.BackColor = Drawing.SystemColors.Control
        btnLogClear.ForeColor = Drawing.SystemColors.ControlText
        btnLogClear.Location = New Drawing.Point(4, 0)
        btnLogClear.Margin = New Padding(4, 0, 4, 3)
        btnLogClear.Name = "btnLogClear"
        btnLogClear.RightToLeft = RightToLeft.No
        btnLogClear.Size = New Drawing.Size(106, 25)
        btnLogClear.TabIndex = 34
        btnLogClear.Text = "Clear"
        btnLogClear.UseVisualStyleBackColor = False
        ' 
        ' btnLogExport
        ' 
        btnLogExport.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        btnLogExport.BackColor = Drawing.SystemColors.Control
        btnLogExport.ForeColor = Drawing.SystemColors.ControlText
        btnLogExport.Location = New Drawing.Point(4, 0)
        btnLogExport.Margin = New Padding(4, 0, 4, 3)
        btnLogExport.Name = "btnLogExport"
        btnLogExport.RightToLeft = RightToLeft.No
        btnLogExport.Size = New Drawing.Size(106, 25)
        btnLogExport.TabIndex = 35
        btnLogExport.Text = "Export"
        btnLogExport.UseVisualStyleBackColor = False
        ' 
        ' lblLogEntries
        ' 
        lblLogEntries.AutoSize = True
        lblLogEntries.Location = New Drawing.Point(5, 19)
        lblLogEntries.Name = "lblLogEntries"
        lblLogEntries.Size = New Drawing.Size(45, 15)
        lblLogEntries.TabIndex = 30
        lblLogEntries.Text = "Entries:"
        ' 
        ' nudLogEntries
        ' 
        nudLogEntries.Location = New Drawing.Point(51, 17)
        nudLogEntries.Maximum = New Decimal(New Integer() {-1, 0, 0, 0})
        nudLogEntries.Name = "nudLogEntries"
        nudLogEntries.Size = New Drawing.Size(57, 23)
        nudLogEntries.TabIndex = 31
        nudLogEntries.TextAlign = HorizontalAlignment.Right
        nudLogEntries.Value = New Decimal(New Integer() {1000, 0, 0, 0})
        ' 
        ' btnSettings
        ' 
        btnSettings.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnSettings.DialogResult = DialogResult.TryAgain
        btnSettings.Image = My.Resources.Resources.logo_contrast_white_scale_20
        btnSettings.ImageAlign = Drawing.ContentAlignment.MiddleLeft
        btnSettings.Location = New Drawing.Point(447, 45)
        btnSettings.Name = "btnSettings"
        btnSettings.Size = New Drawing.Size(75, 24)
        btnSettings.TabIndex = 5
        btnSettings.Text = "Settings"
        btnSettings.TextAlign = Drawing.ContentAlignment.MiddleRight
        btnSettings.UseVisualStyleBackColor = True
        ' 
        ' btnScanMsuDirectory
        ' 
        btnScanMsuDirectory.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnScanMsuDirectory.DialogResult = DialogResult.TryAgain
        btnScanMsuDirectory.Image = My.Resources.Resources.SyncCenter_1223_16px_bw
        btnScanMsuDirectory.ImageAlign = Drawing.ContentAlignment.MiddleLeft
        btnScanMsuDirectory.Location = New Drawing.Point(285, 45)
        btnScanMsuDirectory.Name = "btnScanMsuDirectory"
        btnScanMsuDirectory.Size = New Drawing.Size(156, 24)
        btnScanMsuDirectory.TabIndex = 4
        btnScanMsuDirectory.Text = "Scan for new PCM Files"
        btnScanMsuDirectory.TextAlign = Drawing.ContentAlignment.MiddleRight
        btnScanMsuDirectory.UseVisualStyleBackColor = True
        ' 
        ' BackgroundWorkerDelegate
        ' 
        ' 
        ' ttpMsuAltSel
        ' 
        ttpMsuAltSel.AutoPopDelay = Integer.MaxValue
        ttpMsuAltSel.InitialDelay = 250
        ttpMsuAltSel.ReshowDelay = 50
        ' 
        ' Msu1AltSelectMainForm
        ' 
        Me.AutoScaleDimensions = New Drawing.SizeF(7F, 15F)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New Drawing.Size(528, 536)
        Me.Controls.Add(btnScanMsuDirectory)
        Me.Controls.Add(btnSettings)
        Me.Controls.Add(txtPathMsu)
        Me.Controls.Add(btnSelPathMsu)
        Me.Controls.Add(ctrlDisplayOnlyTracksWithAlts)
        Me.Controls.Add(btnSaveJsonAs)
        Me.Controls.Add(btnSaveJson)
        Me.Controls.Add(scVerticalHalf)
        Me.Icon = CType(resources.GetObject("$this.Icon"), Drawing.Icon)
        Me.Name = "Msu1AltSelectMainForm"
        Me.Text = "frmMSU1altSel"
        ContextMenuStripMsuConfig.ResumeLayout(False)
        ContextMenuStripTracks.ResumeLayout(False)
        ContextMenuStripAltTracks.ResumeLayout(False)
        scTracks.Panel1.ResumeLayout(False)
        scTracks.Panel2.ResumeLayout(False)
        CType(scTracks, ComponentModel.ISupportInitialize).EndInit()
        scTracks.ResumeLayout(False)
        grpPcmConvert.ResumeLayout(False)
        grpPcmConvert.PerformLayout()
        CType(nudProcessCount, ComponentModel.ISupportInitialize).EndInit()
        CType(nudPcmResample, ComponentModel.ISupportInitialize).EndInit()
        frameResample.ResumeLayout(False)
        CType(nudPcmVolume, ComponentModel.ISupportInitialize).EndInit()
        grpMsuTracks.ResumeLayout(False)
        grpAutoSwitch.ResumeLayout(False)
        grpAutoSwitch.PerformLayout()
        CType(nudAutoSwitchInterval, ComponentModel.ISupportInitialize).EndInit()
        scVerticalHalf.Panel1.ResumeLayout(False)
        scVerticalHalf.Panel2.ResumeLayout(False)
        CType(scVerticalHalf, ComponentModel.ISupportInitialize).EndInit()
        scVerticalHalf.ResumeLayout(False)
        grpLogSettings.ResumeLayout(False)
        grpLogSettings.PerformLayout()
        scLogSettingButtons.Panel1.ResumeLayout(False)
        scLogSettingButtons.Panel2.ResumeLayout(False)
        CType(scLogSettingButtons, ComponentModel.ISupportInitialize).EndInit()
        scLogSettingButtons.ResumeLayout(False)
        CType(nudLogEntries, ComponentModel.ISupportInitialize).EndInit()
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
    Private WithEvents grpLogSettings As GroupBox
    Private WithEvents lblLogEntries As Label
    Private WithEvents nudLogEntries As NumericUpDown
    Private WithEvents btnLogExport As Button
    Private WithEvents btnLogClear As Button
    Private WithEvents scLogSettingButtons As SplitContainer

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
    Private WithEvents rtbLog As Logger.ScrollingRichTextBox
    Private WithEvents ttpMsuAltSel As ToolTip
    Private WithEvents ctrlLogAutoScroll As CheckBox
End Class
