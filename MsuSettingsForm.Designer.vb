<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MsuSettingsForm : Inherits System.Windows.Forms.Form

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
        Dim TreeNode1 As TreeNode = New TreeNode("Application")
        Dim TreeNode2 As TreeNode = New TreeNode("Current Config")
        btnOk = New Button()
        btnCancel = New Button()
        btnApply = New Button()
        tvSettings = New TreeView()
        scSettings = New SplitContainer()
        SettingsControl = New UserControl()
        CType(scSettings, ComponentModel.ISupportInitialize).BeginInit()
        scSettings.Panel1.SuspendLayout()
        scSettings.Panel2.SuspendLayout()
        scSettings.SuspendLayout()
        Me.SuspendLayout()
        ' 
        ' btnOk
        ' 
        btnOk.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnOk.BackgroundImageLayout = ImageLayout.None
        btnOk.DialogResult = DialogResult.TryAgain
        btnOk.ImageAlign = Drawing.ContentAlignment.TopLeft
        btnOk.Location = New Drawing.Point(280, 216)
        btnOk.Name = "btnOk"
        btnOk.Size = New Drawing.Size(54, 24)
        btnOk.TabIndex = 2
        btnOk.Text = "Ok"
        btnOk.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnCancel.DialogResult = DialogResult.TryAgain
        btnCancel.ImageAlign = Drawing.ContentAlignment.TopLeft
        btnCancel.Location = New Drawing.Point(340, 216)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Drawing.Size(54, 24)
        btnCancel.TabIndex = 3
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' btnApply
        ' 
        btnApply.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnApply.DialogResult = DialogResult.TryAgain
        btnApply.ImageAlign = Drawing.ContentAlignment.TopLeft
        btnApply.Location = New Drawing.Point(400, 216)
        btnApply.Margin = New Padding(3, 3, 3, 0)
        btnApply.Name = "btnApply"
        btnApply.Size = New Drawing.Size(54, 24)
        btnApply.TabIndex = 4
        btnApply.Text = "Apply"
        btnApply.UseVisualStyleBackColor = True
        ' 
        ' tvSettings
        ' 
        tvSettings.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tvSettings.Location = New Drawing.Point(0, 0)
        tvSettings.Margin = New Padding(0)
        tvSettings.Name = "tvSettings"
        TreeNode1.Name = "Application"
        TreeNode1.Text = "Application"
        TreeNode2.Checked = True
        TreeNode2.Name = "CurrentConfig"
        TreeNode2.Text = "Current Config"
        tvSettings.Nodes.AddRange(New TreeNode() {TreeNode1, TreeNode2})
        tvSettings.Size = New Drawing.Size(137, 243)
        tvSettings.TabIndex = 0
        ' 
        ' scSettings
        ' 
        scSettings.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        scSettings.Location = New Drawing.Point(0, 0)
        scSettings.Name = "scSettings"
        ' 
        ' scSettings.Panel1
        ' 
        scSettings.Panel1.Controls.Add(tvSettings)
        ' 
        ' scSettings.Panel2
        ' 
        scSettings.Panel2.Controls.Add(btnApply)
        scSettings.Panel2.Controls.Add(btnOk)
        scSettings.Panel2.Controls.Add(btnCancel)
        scSettings.Panel2.Controls.Add(SettingsControl)
        scSettings.Size = New Drawing.Size(598, 243)
        scSettings.SplitterDistance = 137
        scSettings.TabIndex = 7
        ' 
        ' SettingsControl
        ' 
        SettingsControl.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        SettingsControl.Location = New Drawing.Point(0, 0)
        SettingsControl.Name = "SettingsControl"
        SettingsControl.Size = New Drawing.Size(454, 240)
        SettingsControl.TabIndex = 1
        ' 
        ' MsuSettingsForm
        ' 
        Me.AutoScaleDimensions = New Drawing.SizeF(7F, 15F)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New Drawing.Size(599, 243)
        Me.Controls.Add(scSettings)
        Me.FormBorderStyle = FormBorderStyle.SizableToolWindow
        Me.Name = "MsuSettingsForm"
        Me.Text = "MSU1 Settings"
        scSettings.Panel1.ResumeLayout(False)
        scSettings.Panel2.ResumeLayout(False)
        CType(scSettings, ComponentModel.ISupportInitialize).EndInit()
        scSettings.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub
    Friend WithEvents btnOk As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnApply As Button
    Friend WithEvents tvSettings As TreeView
    Friend WithEvents scSettings As SplitContainer
    Friend WithEvents SettingsControl As UserControl
End Class
