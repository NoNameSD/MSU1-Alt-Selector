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
        Me.btnOk = New Button()
        Me.btnCancel = New Button()
        Me.btnApply = New Button()
        Me.tvSettings = New TreeView()
        Me.scSettings = New SplitContainer()
        Me.SettingsControl = New UserControl()
        CType(Me.scSettings, ComponentModel.ISupportInitialize).BeginInit()
        Me.scSettings.Panel1.SuspendLayout()
        Me.scSettings.Panel2.SuspendLayout()
        Me.scSettings.SuspendLayout()
        Me.SuspendLayout()
        ' 
        ' btnOk
        ' 
        Me.btnOk.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Me.btnOk.BackgroundImageLayout = ImageLayout.None
        Me.btnOk.DialogResult = DialogResult.TryAgain
        Me.btnOk.ImageAlign = Drawing.ContentAlignment.TopLeft
        Me.btnOk.Location = New System.Drawing.Point(280, 245)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(54, 24)
        Me.btnOk.TabIndex = 2
        Me.btnOk.Text = "Ok"
        Me.btnOk.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        Me.btnCancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Me.btnCancel.DialogResult = DialogResult.TryAgain
        Me.btnCancel.ImageAlign = Drawing.ContentAlignment.TopLeft
        Me.btnCancel.Location = New System.Drawing.Point(340, 245)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(54, 24)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        ' 
        ' btnApply
        ' 
        Me.btnApply.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Me.btnApply.DialogResult = DialogResult.TryAgain
        Me.btnApply.ImageAlign = Drawing.ContentAlignment.TopLeft
        Me.btnApply.Location = New System.Drawing.Point(400, 245)
        Me.btnApply.Margin = New Padding(3, 3, 3, 0)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(54, 24)
        Me.btnApply.TabIndex = 4
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        ' 
        ' tvSettings
        ' 
        Me.tvSettings.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Me.tvSettings.Location = New System.Drawing.Point(0, 0)
        Me.tvSettings.Margin = New Padding(0)
        Me.tvSettings.Name = "tvSettings"
        TreeNode1.Name = "Application"
        TreeNode1.Text = "Application"
        TreeNode2.Checked = True
        TreeNode2.Name = "CurrentConfig"
        TreeNode2.Text = "Current Config"
        Me.tvSettings.Nodes.AddRange(New TreeNode() {TreeNode1, TreeNode2})
        Me.tvSettings.Size = New System.Drawing.Size(137, 272)
        Me.tvSettings.TabIndex = 0
        ' 
        ' scSettings
        ' 
        Me.scSettings.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Me.scSettings.Location = New System.Drawing.Point(0, 0)
        Me.scSettings.Name = "scSettings"
        ' 
        ' scSettings.Panel1
        ' 
        Me.scSettings.Panel1.Controls.Add(Me.tvSettings)
        ' 
        ' scSettings.Panel2
        ' 
        Me.scSettings.Panel2.Controls.Add(Me.btnApply)
        Me.scSettings.Panel2.Controls.Add(Me.btnOk)
        Me.scSettings.Panel2.Controls.Add(Me.btnCancel)
        Me.scSettings.Panel2.Controls.Add(Me.SettingsControl)
        Me.scSettings.Size = New System.Drawing.Size(598, 272)
        Me.scSettings.SplitterDistance = 137
        Me.scSettings.TabIndex = 7
        ' 
        ' SettingsControl
        ' 
        Me.SettingsControl.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Me.SettingsControl.Location = New System.Drawing.Point(0, 0)
        Me.SettingsControl.Name = "SettingsControl"
        Me.SettingsControl.Size = New System.Drawing.Size(454, 269)
        Me.SettingsControl.TabIndex = 1
        ' 
        ' MsuSettingsForm
        ' 
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7F, 15F)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(599, 272)
        Me.Controls.Add(Me.scSettings)
        Me.FormBorderStyle = FormBorderStyle.SizableToolWindow
        Me.Name = "MsuSettingsForm"
        Me.Text = "MSU1 Settings"
        Me.scSettings.Panel1.ResumeLayout(False)
        Me.scSettings.Panel2.ResumeLayout(False)
        CType(Me.scSettings, ComponentModel.ISupportInitialize).EndInit()
        Me.scSettings.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub
    Friend WithEvents btnOk As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnApply As Button
    Friend WithEvents tvSettings As TreeView
    Friend WithEvents scSettings As SplitContainer
    Friend WithEvents SettingsControl As UserControl
End Class
