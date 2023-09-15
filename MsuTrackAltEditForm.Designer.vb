<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MsuTrackAltEditForm
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
        ucMsuTrack = New MsuTrackAltControl()
        btnOk = New Button()
        btnCancel = New Button()
        btnApply = New Button()
        SuspendLayout()
        ' 
        ' ucMsuTrack
        ' 
        ucMsuTrack.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ucMsuTrack.Dirty = False
        ucMsuTrack.Location = New System.Drawing.Point(0, 0)
        ucMsuTrack.Margin = New Padding(3, 3, 3, 0)
        ucMsuTrack.Name = "ucMsuTrack"
        ucMsuTrack.Size = New System.Drawing.Size(500, 209)
        ucMsuTrack.TabIndex = 0
        ' 
        ' btnOk
        ' 
        btnOk.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnOk.BackgroundImageLayout = ImageLayout.None
        btnOk.DialogResult = DialogResult.TryAgain
        btnOk.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        btnOk.Location = New System.Drawing.Point(322, 212)
        btnOk.Name = "btnOk"
        btnOk.Size = New System.Drawing.Size(54, 24)
        btnOk.TabIndex = 1
        btnOk.Text = "Ok"
        btnOk.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnCancel.DialogResult = DialogResult.TryAgain
        btnCancel.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        btnCancel.Location = New System.Drawing.Point(382, 212)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New System.Drawing.Size(54, 24)
        btnCancel.TabIndex = 2
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' btnApply
        ' 
        btnApply.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnApply.DialogResult = DialogResult.TryAgain
        btnApply.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        btnApply.Location = New System.Drawing.Point(442, 212)
        btnApply.Margin = New Padding(3, 3, 3, 0)
        btnApply.Name = "btnApply"
        btnApply.Size = New System.Drawing.Size(54, 24)
        btnApply.TabIndex = 3
        btnApply.Text = "Apply"
        btnApply.UseVisualStyleBackColor = True
        ' 
        ' frmMsuTrackAltEdit
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(499, 239)
        Controls.Add(btnApply)
        Controls.Add(btnCancel)
        Controls.Add(btnOk)
        Controls.Add(ucMsuTrack)
        FormBorderStyle = FormBorderStyle.SizableToolWindow
        Name = "frmMsuTrackAltEdit"
        Text = "MSU1 Track Edit"
        ResumeLayout(False)
    End Sub

    Private WithEvents ucMsuTrack As MsuTrackAltControl
    Friend WithEvents btnOk As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnApply As Button
End Class
