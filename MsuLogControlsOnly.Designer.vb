<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MsuLogControlsOnly
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
        Me.grpLogSettings = New GroupBox()
        Me.ctrlLogAutoScroll = New CheckBox()
        Me.nudLogEntries = New NumericUpDown()
        Me.lblLogEntries = New Label()
        Me.scLogSettingButtons = New SplitContainer()
        Me.btnLogClear = New Button()
        Me.btnLogExport = New Button()
        Me.ttpLogControl = New ToolTip(Me.components)
        Me.grpLogSettings.SuspendLayout()
        CType(Me.nudLogEntries, ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.scLogSettingButtons, ComponentModel.ISupportInitialize).BeginInit()
        Me.scLogSettingButtons.Panel1.SuspendLayout()
        Me.scLogSettingButtons.Panel2.SuspendLayout()
        Me.scLogSettingButtons.SuspendLayout()
        Me.SuspendLayout()
        ' 
        ' grpLogSettings
        ' 
        Me.grpLogSettings.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        Me.grpLogSettings.Controls.Add(Me.ctrlLogAutoScroll)
        Me.grpLogSettings.Controls.Add(Me.nudLogEntries)
        Me.grpLogSettings.Controls.Add(Me.lblLogEntries)
        Me.grpLogSettings.Controls.Add(Me.scLogSettingButtons)
        Me.grpLogSettings.Location = New System.Drawing.Point(0, -6)
        Me.grpLogSettings.Margin = New Padding(0)
        Me.grpLogSettings.Name = "grpLogSettings"
        Me.grpLogSettings.Size = New System.Drawing.Size(116, 121)
        Me.grpLogSettings.TabIndex = 1
        Me.grpLogSettings.TabStop = False
        ' 
        ' ctrlLogAutoScroll
        ' 
        Me.ctrlLogAutoScroll.AutoSize = True
        Me.ctrlLogAutoScroll.Checked = True
        Me.ctrlLogAutoScroll.CheckState = CheckState.Checked
        Me.ctrlLogAutoScroll.Location = New System.Drawing.Point(6, 38)
        Me.ctrlLogAutoScroll.Name = "ctrlLogAutoScroll"
        Me.ctrlLogAutoScroll.Size = New System.Drawing.Size(84, 19)
        Me.ctrlLogAutoScroll.TabIndex = 30
        Me.ctrlLogAutoScroll.Text = "Auto Scroll"
        Me.ctrlLogAutoScroll.UseVisualStyleBackColor = True
        ' 
        ' nudLogEntries
        ' 
        Me.nudLogEntries.Location = New System.Drawing.Point(53, 11)
        Me.nudLogEntries.Maximum = New Decimal(New Integer() {-1, 0, 0, 0})
        Me.nudLogEntries.Name = "nudLogEntries"
        Me.nudLogEntries.Size = New System.Drawing.Size(57, 23)
        Me.nudLogEntries.TabIndex = 21
        Me.nudLogEntries.TextAlign = HorizontalAlignment.Right
        Me.nudLogEntries.Value = New Decimal(New Integer() {1000, 0, 0, 0})
        ' 
        ' lblLogEntries
        ' 
        Me.lblLogEntries.AutoSize = True
        Me.lblLogEntries.Location = New System.Drawing.Point(4, 13)
        Me.lblLogEntries.Name = "lblLogEntries"
        Me.lblLogEntries.Size = New System.Drawing.Size(45, 15)
        Me.lblLogEntries.TabIndex = 20
        Me.lblLogEntries.Text = "Entries:"
        ' 
        ' scLogSettingButtons
        ' 
        Me.scLogSettingButtons.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Me.scLogSettingButtons.Location = New System.Drawing.Point(1, 58)
        Me.scLogSettingButtons.Name = "scLogSettingButtons"
        Me.scLogSettingButtons.Orientation = Orientation.Horizontal
        ' 
        ' scLogSettingButtons.Panel1
        ' 
        Me.scLogSettingButtons.Panel1.Controls.Add(Me.btnLogClear)
        ' 
        ' scLogSettingButtons.Panel2
        ' 
        Me.scLogSettingButtons.Panel2.Controls.Add(Me.btnLogExport)
        Me.scLogSettingButtons.Size = New System.Drawing.Size(110, 60)
        Me.scLogSettingButtons.SplitterDistance = 27
        Me.scLogSettingButtons.TabIndex = 33
        ' 
        ' btnLogClear
        ' 
        Me.btnLogClear.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        Me.btnLogClear.BackColor = Drawing.SystemColors.Control
        Me.btnLogClear.ForeColor = Drawing.SystemColors.ControlText
        Me.btnLogClear.Location = New System.Drawing.Point(4, 0)
        Me.btnLogClear.Margin = New Padding(4, 0, 4, 3)
        Me.btnLogClear.Name = "btnLogClear"
        Me.btnLogClear.RightToLeft = RightToLeft.No
        Me.btnLogClear.Size = New System.Drawing.Size(106, 25)
        Me.btnLogClear.TabIndex = 34
        Me.btnLogClear.Text = "Clear"
        Me.btnLogClear.UseVisualStyleBackColor = False
        ' 
        ' btnLogExport
        ' 
        Me.btnLogExport.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        Me.btnLogExport.BackColor = Drawing.SystemColors.Control
        Me.btnLogExport.ForeColor = Drawing.SystemColors.ControlText
        Me.btnLogExport.Location = New System.Drawing.Point(4, 0)
        Me.btnLogExport.Margin = New Padding(4, 0, 4, 3)
        Me.btnLogExport.Name = "btnLogExport"
        Me.btnLogExport.RightToLeft = RightToLeft.No
        Me.btnLogExport.Size = New System.Drawing.Size(106, 25)
        Me.btnLogExport.TabIndex = 35
        Me.btnLogExport.Text = "Export"
        Me.btnLogExport.UseVisualStyleBackColor = False
        ' 
        ' ttpLogControl
        ' 
        Me.ttpLogControl.AutoPopDelay = Integer.MaxValue
        Me.ttpLogControl.InitialDelay = 250
        Me.ttpLogControl.ReshowDelay = 50
        ' 
        ' MsuLogControlsOnly
        ' 
        Me.AutoScaleMode = AutoScaleMode.Inherit
        Me.Controls.Add(Me.grpLogSettings)
        Me.Name = "MsuLogControlsOnly"
        Me.Size = New System.Drawing.Size(116, 115)
        Me.grpLogSettings.ResumeLayout(False)
        Me.grpLogSettings.PerformLayout()
        CType(Me.nudLogEntries, ComponentModel.ISupportInitialize).EndInit()
        Me.scLogSettingButtons.Panel1.ResumeLayout(False)
        Me.scLogSettingButtons.Panel2.ResumeLayout(False)
        CType(Me.scLogSettingButtons, ComponentModel.ISupportInitialize).EndInit()
        Me.scLogSettingButtons.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub
    Private WithEvents grpLogSettings As GroupBox
    Private WithEvents scLogSettingButtons As SplitContainer
    Private WithEvents btnLogClear As Button
    Private WithEvents btnLogExport As Button
    Private WithEvents ctrlLogAutoScroll As CheckBox
    Private WithEvents nudLogEntries As NumericUpDown
    Private WithEvents lblLogEntries As Label
    Friend WithEvents ttpLogControl As ToolTip

End Class
