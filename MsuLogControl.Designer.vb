<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MsuLogControl
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
        Me.rtbLog = New Logger.ScrollingRichTextBox()
        Me.ucControls = New MsuLogControlsOnly()
        Me.SuspendLayout()
        ' 
        ' rtbLog
        ' 
        Me.rtbLog.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Me.rtbLog.Location = New System.Drawing.Point(0, 0)
        Me.rtbLog.Margin = New Padding(0)
        Me.rtbLog.Name = "rtbLog"
        Me.rtbLog.Size = New System.Drawing.Size(78, 115)
        Me.rtbLog.TabIndex = 30
        Me.rtbLog.Text = ""
        ' 
        ' ucControls
        ' 
        Me.ucControls.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Me.ucControls.Location = New System.Drawing.Point(84, 0)
        Me.ucControls.MinimumSize = New System.Drawing.Size(116, 115)
        Me.ucControls.Name = "ucControls"
        Me.ucControls.Size = New System.Drawing.Size(116, 115)
        Me.ucControls.TabIndex = 31
        ' 
        ' MsuLogControl
        ' 
        Me.AutoScaleMode = AutoScaleMode.Inherit
        Me.AutoSizeMode = AutoSizeMode.GrowAndShrink
        Me.Controls.Add(Me.rtbLog)
        Me.Controls.Add(Me.ucControls)
        Me.Name = "MsuLogControl"
        Me.Size = New System.Drawing.Size(200, 115)
        Me.ResumeLayout(False)
    End Sub
    Private WithEvents rtbLog As Logger.ScrollingRichTextBox
    Friend WithEvents ucControls As MsuLogControlsOnly
    Private WithEvents objLogger As MsuAltSelect.Logger.Logger

End Class
