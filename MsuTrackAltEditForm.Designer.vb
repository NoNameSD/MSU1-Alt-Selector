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
        Me.ucMsuTrack = New MsuTrackAltControl()
        Me.btnOk = New Button()
        Me.btnCancel = New Button()
        Me.btnApply = New Button()
        Me.SuspendLayout()
        ' 
        ' ucMsuTrack
        ' 
        Me.ucMsuTrack.AllowDrop = True
        Me.ucMsuTrack.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Me.ucMsuTrack.Dirty = False
        Me.ucMsuTrack.Location = New System.Drawing.Point(0, 0)
        Me.ucMsuTrack.Margin = New Padding(3, 3, 3, 0)
        Me.ucMsuTrack.Name = "ucMsuTrack"
        Me.ucMsuTrack.Size = New System.Drawing.Size(500, 267)
        Me.ucMsuTrack.TabIndex = 0
        ' 
        ' btnOk
        ' 
        Me.btnOk.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Me.btnOk.BackgroundImageLayout = ImageLayout.None
        Me.btnOk.DialogResult = DialogResult.TryAgain
        Me.btnOk.ImageAlign = Drawing.ContentAlignment.TopLeft
        Me.btnOk.Location = New System.Drawing.Point(322, 270)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(54, 24)
        Me.btnOk.TabIndex = 1
        Me.btnOk.Text = "Ok"
        Me.btnOk.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        Me.btnCancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Me.btnCancel.DialogResult = DialogResult.TryAgain
        Me.btnCancel.ImageAlign = Drawing.ContentAlignment.TopLeft
        Me.btnCancel.Location = New System.Drawing.Point(382, 270)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(54, 24)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        ' 
        ' btnApply
        ' 
        Me.btnApply.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Me.btnApply.DialogResult = DialogResult.TryAgain
        Me.btnApply.ImageAlign = Drawing.ContentAlignment.TopLeft
        Me.btnApply.Location = New System.Drawing.Point(442, 270)
        Me.btnApply.Margin = New Padding(3, 3, 3, 0)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(54, 24)
        Me.btnApply.TabIndex = 3
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        ' 
        ' MsuTrackAltEditForm
        ' 
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7F, 15F)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(499, 297)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.ucMsuTrack)
        Me.FormBorderStyle = FormBorderStyle.SizableToolWindow
        Me.Name = "MsuTrackAltEditForm"
        Me.Text = "MSU1 Track Edit"
        Me.ResumeLayout(False)
    End Sub

    Private WithEvents ucMsuTrack As MsuTrackAltControl
    Friend WithEvents btnOk As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnApply As Button
End Class
