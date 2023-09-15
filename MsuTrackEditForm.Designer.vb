<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MsuTrackEditForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        ucMsuTrack = New MsuTrackControl()
        btnOk = New Button()
        btnCancel = New Button()
        btnApply = New Button()
        SuspendLayout()
        ' 
        ' ucMsuTrack
        ' 
        ucMsuTrack.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ucMsuTrack.AutoSizeMode = AutoSizeMode.GrowAndShrink
        ucMsuTrack.Location = New System.Drawing.Point(0, 0)
        ucMsuTrack.Margin = New Padding(3, 3, 3, 0)
        ucMsuTrack.Name = "ucMsuTrack"
        ucMsuTrack.Size = New System.Drawing.Size(264, 76)
        ucMsuTrack.TabIndex = 0
        ' 
        ' btnOk
        ' 
        btnOk.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnOk.BackgroundImageLayout = ImageLayout.None
        btnOk.DialogResult = DialogResult.TryAgain
        btnOk.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        btnOk.Location = New System.Drawing.Point(90, 79)
        btnOk.Name = "btnOk"
        btnOk.Size = New System.Drawing.Size(54, 24)
        btnOk.TabIndex = 6
        btnOk.Text = "Ok"
        btnOk.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnCancel.DialogResult = DialogResult.TryAgain
        btnCancel.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        btnCancel.Location = New System.Drawing.Point(150, 79)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New System.Drawing.Size(54, 24)
        btnCancel.TabIndex = 7
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' btnApply
        ' 
        btnApply.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnApply.DialogResult = DialogResult.TryAgain
        btnApply.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        btnApply.Location = New System.Drawing.Point(210, 79)
        btnApply.Name = "btnApply"
        btnApply.Size = New System.Drawing.Size(54, 24)
        btnApply.TabIndex = 8
        btnApply.Text = "Apply"
        btnApply.UseVisualStyleBackColor = True
        ' 
        ' frmMsuTrackEdit
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(268, 107)
        Controls.Add(btnApply)
        Controls.Add(btnCancel)
        Controls.Add(btnOk)
        Controls.Add(ucMsuTrack)
        FormBorderStyle = FormBorderStyle.SizableToolWindow
        Name = "frmMsuTrackEdit"
        Text = "MSU1 Track Edit"
        ResumeLayout(False)
    End Sub

    Private WithEvents ucMsuTrack As MsuTrackControl
    Friend WithEvents btnOk As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnApply As Button
End Class
