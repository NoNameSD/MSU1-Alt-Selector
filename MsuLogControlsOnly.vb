Public Class MsuLogControlsOnly

    Public Event ClickedLogClear(sender As Object, e As EventArgs)
    Public Event ClickedLogExport(sender As Object, e As EventArgs)
    Public Event MaxLogEntriesValueChanged(sender As Object, e As EventArgs)

    Public ReadOnly Property LogAutoScroll As Boolean
        Get
            Return Me.ctrlLogAutoScroll.Checked
        End Get
    End Property

    Public ReadOnly Property MaxLogEntriesNUD As NumericUpDown
        Get
            Return Me.nudLogEntries
        End Get
    End Property

    Private Sub btnLogClear_Click(sender As Object, e As EventArgs) Handles btnLogClear.Click
        RaiseEvent ClickedLogClear(sender, e)
    End Sub

    Private Sub btnLogExport_Click(sender As Object, e As EventArgs) Handles btnLogExport.Click
        RaiseEvent ClickedLogExport(sender, e)
    End Sub

    Private Sub nudLogEntries_ValueChanged(sender As Object, e As System.EventArgs) Handles nudLogEntries.ValueChanged
        RaiseEvent MaxLogEntriesValueChanged(sender, e)
    End Sub

    Private Sub SetToolTips()
        With Me.ttpLogControl

            .SetToolTip(Me.nudLogEntries,
                       $"Maximum number of Log-Entries that will be kept.{Environment.NewLine}" &
                        "Too many Log-Entries will decrease performance.")
            .SetToolTip(Me.lblLogEntries, .GetToolTip(Me.nudLogEntries))

            .SetToolTip(Me.ctrlLogAutoScroll,
                       $"Automatically scroll to the last Log-Entry.")

            .SetToolTip(Me.btnLogClear,
                       $"Clear log.")

            .SetToolTip(Me.btnLogExport,
                       $"Export the log as a text file.")
        End With
    End Sub

    Private Sub MsuLogControlsOnly_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call SetToolTips()
    End Sub
End Class
