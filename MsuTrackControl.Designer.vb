<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MsuTrackControl
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
        lblMsuTitle = New Label()
        txtMsuTitle = New TextBox()
        lblMsuTrackId = New Label()
        txtMsuFileName = New TextBox()
        nudMsuTrackId = New NumericUpDown()
        CType(nudMsuTrackId, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lblMsuTitle
        ' 
        lblMsuTitle.AutoSize = True
        lblMsuTitle.Location = New System.Drawing.Point(3, 14)
        lblMsuTitle.Name = "lblMsuTitle"
        lblMsuTitle.Size = New System.Drawing.Size(32, 15)
        lblMsuTitle.TabIndex = 0
        lblMsuTitle.Text = "Title:"
        ' 
        ' txtMsuTitle
        ' 
        txtMsuTitle.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtMsuTitle.Location = New System.Drawing.Point(41, 11)
        txtMsuTitle.Name = "txtMsuTitle"
        txtMsuTitle.Size = New System.Drawing.Size(136, 23)
        txtMsuTitle.TabIndex = 1
        ' 
        ' lblMsuTrackId
        ' 
        lblMsuTrackId.AutoSize = True
        lblMsuTrackId.Location = New System.Drawing.Point(3, 46)
        lblMsuTrackId.Name = "lblMsuTrackId"
        lblMsuTrackId.Size = New System.Drawing.Size(82, 15)
        lblMsuTrackId.TabIndex = 2
        lblMsuTrackId.Text = "Track number:"
        ' 
        ' txtMsuFileName
        ' 
        txtMsuFileName.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtMsuFileName.Location = New System.Drawing.Point(137, 43)
        txtMsuFileName.MaxLength = 3
        txtMsuFileName.Name = "txtMsuFileName"
        txtMsuFileName.ReadOnly = True
        txtMsuFileName.Size = New System.Drawing.Size(40, 23)
        txtMsuFileName.TabIndex = 3
        ' 
        ' nudMsuTrackId
        ' 
        nudMsuTrackId.Location = New System.Drawing.Point(90, 43)
        nudMsuTrackId.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        nudMsuTrackId.Name = "nudMsuTrackId"
        nudMsuTrackId.Size = New System.Drawing.Size(41, 23)
        nudMsuTrackId.TabIndex = 2
        ' 
        ' MsuTrackControl
        ' 
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0F, 15.0F)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.Controls.Add(nudMsuTrackId)
        Me.Controls.Add(txtMsuFileName)
        Me.Controls.Add(lblMsuTrackId)
        Me.Controls.Add(txtMsuTitle)
        Me.Controls.Add(lblMsuTitle)
        Me.Name = "MsuTrackControl"
        Me.Size = New System.Drawing.Size(188, 75)
        CType(nudMsuTrackId, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblMsuTitle As Label
    Friend WithEvents txtMsuTitle As TextBox
    Friend WithEvents lblMsuTrackId As Label
    Friend WithEvents txtMsuFileName As TextBox
    Friend WithEvents nudMsuTrackId As NumericUpDown
End Class
