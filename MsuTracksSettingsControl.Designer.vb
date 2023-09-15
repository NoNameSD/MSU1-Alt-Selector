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
        lblMsuLocation = New Label()
        txtMsuLocation = New TextBox()
        ttpMsuSettings = New ToolTip(Me.components)
        lblMsuName = New Label()
        txtMsuName = New TextBox()
        txtPcmPrefix = New TextBox()
        lblPcmPrefix = New Label()
        Me.SuspendLayout()
        ' 
        ' lblMsuLocation
        ' 
        lblMsuLocation.AutoSize = True
        lblMsuLocation.Location = New Drawing.Point(3, 14)
        lblMsuLocation.Name = "lblMsuLocation"
        lblMsuLocation.Size = New Drawing.Size(130, 15)
        lblMsuLocation.TabIndex = 0
        lblMsuLocation.Text = "Location of MSU/ROM:"
        ' 
        ' txtMsuLocation
        ' 
        txtMsuLocation.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtMsuLocation.Location = New Drawing.Point(170, 11)
        txtMsuLocation.Name = "txtMsuLocation"
        txtMsuLocation.Size = New Drawing.Size(160, 23)
        txtMsuLocation.TabIndex = 1
        ' 
        ' ttpMsuSettings
        ' 
        ttpMsuSettings.AutoPopDelay = Integer.MaxValue
        ttpMsuSettings.InitialDelay = 250
        ttpMsuSettings.ReshowDelay = 50
        ' 
        ' lblMsuName
        ' 
        lblMsuName.AutoSize = True
        lblMsuName.Location = New Drawing.Point(3, 43)
        lblMsuName.Name = "lblMsuName"
        lblMsuName.Size = New Drawing.Size(116, 15)
        lblMsuName.TabIndex = 1
        lblMsuName.Text = "Name of MSU/ROM:"
        ' 
        ' txtMsuName
        ' 
        txtMsuName.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtMsuName.Location = New Drawing.Point(170, 40)
        txtMsuName.Name = "txtMsuName"
        txtMsuName.Size = New Drawing.Size(160, 23)
        txtMsuName.TabIndex = 3
        ' 
        ' txtPcmPrefix
        ' 
        txtPcmPrefix.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtPcmPrefix.Location = New Drawing.Point(170, 69)
        txtPcmPrefix.Name = "txtPcmPrefix"
        txtPcmPrefix.Size = New Drawing.Size(160, 23)
        txtPcmPrefix.TabIndex = 5
        ' 
        ' lblPcmPrefix
        ' 
        lblPcmPrefix.AutoSize = True
        lblPcmPrefix.Location = New Drawing.Point(3, 72)
        lblPcmPrefix.Name = "lblPcmPrefix"
        lblPcmPrefix.Size = New Drawing.Size(161, 15)
        lblPcmPrefix.TabIndex = 4
        lblPcmPrefix.Text = "Prefix used for the PCM Files:"
        ' 
        ' MsuTracksSettingsControl
        ' 
        Me.AutoScaleDimensions = New Drawing.SizeF(7F, 15F)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.Controls.Add(txtPcmPrefix)
        Me.Controls.Add(lblPcmPrefix)
        Me.Controls.Add(txtMsuName)
        Me.Controls.Add(lblMsuName)
        Me.Controls.Add(txtMsuLocation)
        Me.Controls.Add(lblMsuLocation)
        Me.Name = "MsuTracksSettingsControl"
        Me.Size = New Drawing.Size(333, 100)
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
