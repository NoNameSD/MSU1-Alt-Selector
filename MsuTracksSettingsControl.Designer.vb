<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MsuTracksSettingsControl
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
        Me.components = New ComponentModel.Container()
        Me.lblMsuLocation = New Label()
        Me.txtMsuLocation = New TextBox()
        Me.ttpMsuSettings = New ToolTip(Me.components)
        Me.lblMsuName = New Label()
        Me.txtMsuName = New TextBox()
        Me.txtPcmPrefix = New TextBox()
        Me.lblPcmPrefix = New Label()
        Me.SuspendLayout()
        ' 
        ' lblMsuLocation
        ' 
        Me.lblMsuLocation.AutoSize = True
        Me.lblMsuLocation.Location = New System.Drawing.Point(3, 14)
        Me.lblMsuLocation.Name = "lblMsuLocation"
        Me.lblMsuLocation.Size = New System.Drawing.Size(130, 15)
        Me.lblMsuLocation.TabIndex = 0
        Me.lblMsuLocation.Text = "Location of MSU/ROM:"
        ' 
        ' txtMsuLocation
        ' 
        Me.txtMsuLocation.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Me.txtMsuLocation.Location = New System.Drawing.Point(170, 11)
        Me.txtMsuLocation.Name = "txtMsuLocation"
        Me.txtMsuLocation.Size = New System.Drawing.Size(160, 23)
        Me.txtMsuLocation.TabIndex = 1
        ' 
        ' ttpMsuSettings
        ' 
        Me.ttpMsuSettings.AutoPopDelay = Integer.MaxValue
        Me.ttpMsuSettings.InitialDelay = 250
        Me.ttpMsuSettings.ReshowDelay = 50
        ' 
        ' lblMsuName
        ' 
        Me.lblMsuName.AutoSize = True
        Me.lblMsuName.Location = New System.Drawing.Point(3, 43)
        Me.lblMsuName.Name = "lblMsuName"
        Me.lblMsuName.Size = New System.Drawing.Size(116, 15)
        Me.lblMsuName.TabIndex = 1
        Me.lblMsuName.Text = "Name of MSU/ROM:"
        ' 
        ' txtMsuName
        ' 
        Me.txtMsuName.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Me.txtMsuName.Location = New System.Drawing.Point(170, 40)
        Me.txtMsuName.Name = "txtMsuName"
        Me.txtMsuName.Size = New System.Drawing.Size(160, 23)
        Me.txtMsuName.TabIndex = 3
        ' 
        ' txtPcmPrefix
        ' 
        Me.txtPcmPrefix.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Me.txtPcmPrefix.Location = New System.Drawing.Point(170, 69)
        Me.txtPcmPrefix.Name = "txtPcmPrefix"
        Me.txtPcmPrefix.Size = New System.Drawing.Size(160, 23)
        Me.txtPcmPrefix.TabIndex = 5
        ' 
        ' lblPcmPrefix
        ' 
        Me.lblPcmPrefix.AutoSize = True
        Me.lblPcmPrefix.Location = New System.Drawing.Point(3, 72)
        Me.lblPcmPrefix.Name = "lblPcmPrefix"
        Me.lblPcmPrefix.Size = New System.Drawing.Size(161, 15)
        Me.lblPcmPrefix.TabIndex = 4
        Me.lblPcmPrefix.Text = "Prefix used for the PCM Files:"
        ' 
        ' MsuTracksSettingsControl
        ' 
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7F, 15F)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.Controls.Add(Me.txtPcmPrefix)
        Me.Controls.Add(Me.lblPcmPrefix)
        Me.Controls.Add(Me.txtMsuName)
        Me.Controls.Add(Me.lblMsuName)
        Me.Controls.Add(Me.txtMsuLocation)
        Me.Controls.Add(Me.lblMsuLocation)
        Me.Name = "MsuTracksSettingsControl"
        Me.Size = New System.Drawing.Size(333, 100)
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    'Friend WithEvents btnSelPathMsu As Button
    Friend WithEvents lblMsuLocation As Label
    Friend WithEvents txtMsuLocation As TextBox
    Friend WithEvents ttpMsuSettings As ToolTip
    Friend WithEvents lblMsuName As Label
    Friend WithEvents txtMsuName As TextBox
    Friend WithEvents txtPcmPrefix As TextBox
    Friend WithEvents lblPcmPrefix As Label
End Class
