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
        Me.lblMsuTitle = New Label()
        Me.txtMsuTitle = New TextBox()
        Me.lblMsuTrackId = New Label()
        Me.txtMsuFileName = New TextBox()
        Me.nudMsuTrackId = New NumericUpDown()
        CType(Me.nudMsuTrackId, ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        ' 
        ' lblMsuTitle
        ' 
        Me.lblMsuTitle.AutoSize = True
        Me.lblMsuTitle.Location = New System.Drawing.Point(3, 14)
        Me.lblMsuTitle.Name = "lblMsuTitle"
        Me.lblMsuTitle.Size = New System.Drawing.Size(32, 15)
        Me.lblMsuTitle.TabIndex = 0
        Me.lblMsuTitle.Text = "Title:"
        ' 
        ' txtMsuTitle
        ' 
        Me.txtMsuTitle.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Me.txtMsuTitle.Location = New System.Drawing.Point(41, 11)
        Me.txtMsuTitle.Name = "txtMsuTitle"
        Me.txtMsuTitle.Size = New System.Drawing.Size(136, 23)
        Me.txtMsuTitle.TabIndex = 1
        ' 
        ' lblMsuTrackId
        ' 
        Me.lblMsuTrackId.AutoSize = True
        Me.lblMsuTrackId.Location = New System.Drawing.Point(3, 46)
        Me.lblMsuTrackId.Name = "lblMsuTrackId"
        Me.lblMsuTrackId.Size = New System.Drawing.Size(82, 15)
        Me.lblMsuTrackId.TabIndex = 2
        Me.lblMsuTrackId.Text = "Track number:"
        ' 
        ' txtMsuFileName
        ' 
        Me.txtMsuFileName.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Me.txtMsuFileName.Location = New System.Drawing.Point(137, 43)
        Me.txtMsuFileName.MaxLength = 3
        Me.txtMsuFileName.Name = "txtMsuFileName"
        Me.txtMsuFileName.ReadOnly = True
        Me.txtMsuFileName.Size = New System.Drawing.Size(40, 23)
        Me.txtMsuFileName.TabIndex = 3
        ' 
        ' nudMsuTrackId
        ' 
        Me.nudMsuTrackId.Location = New System.Drawing.Point(90, 43)
        Me.nudMsuTrackId.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nudMsuTrackId.Name = "nudMsuTrackId"
        Me.nudMsuTrackId.Size = New System.Drawing.Size(41, 23)
        Me.nudMsuTrackId.TabIndex = 2
        ' 
        ' MsuTrackControl
        ' 
        Me.AutoScaleMode = AutoScaleMode.Inherit
        Me.Controls.Add(Me.nudMsuTrackId)
        Me.Controls.Add(Me.txtMsuFileName)
        Me.Controls.Add(Me.lblMsuTrackId)
        Me.Controls.Add(Me.txtMsuTitle)
        Me.Controls.Add(Me.lblMsuTitle)
        Me.Name = "MsuTrackControl"
        Me.Size = New System.Drawing.Size(188, 75)
        CType(Me.nudMsuTrackId, ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents lblMsuTitle As Label
    Friend WithEvents txtMsuTitle As TextBox
    Friend WithEvents lblMsuTrackId As Label
    Friend WithEvents txtMsuFileName As TextBox
    Friend WithEvents nudMsuTrackId As NumericUpDown
End Class
