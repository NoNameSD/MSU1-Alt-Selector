Public Class MsuLogControl

    Public Event ClickedLogExport(sender As Object, e As EventArgs)

    Public Property Logger As MsuAltSelect.Logger.Logger
        Get
            Return objLogger
        End Get
        Set(_LoggerSet As MsuAltSelect.Logger.Logger)
            objLogger = _LoggerSet
        End Set
    End Property

    Public ReadOnly Property MaxLogEntriesNUD As NumericUpDown
        Get
            Return Me.ucControls.MaxLogEntriesNUD
        End Get
    End Property

    Private Sub RefreshRichTextLogHandler(sender As Object, e As EventArgs) Handles objLogger.LogEntriesUpdated
        Call RefreshRichTextLog()
    End Sub

    Private Delegate Sub RefreshRichTextLogCallback()

    Private Sub RefreshRichTextLog()
        If Me.rtbLog Is Nothing Then Return
        ' See https://stackoverflow.com/a/10775421
        If Me.rtbLog.InvokeRequired Then
            Dim RefreshRichTextLogCallback As RefreshRichTextLogCallback = New RefreshRichTextLogCallback(AddressOf RefreshRichTextLog)
            Me.Invoke(RefreshRichTextLogCallback)
        Else
            Me.rtbLog.Rtf = Me.Logger.GetLogAsRichText(False, True)
            If Me.ucControls.LogAutoScroll Then Call Me.rtbLog.ScrollToBottom()
        End If
    End Sub

    Private Sub rtbLog_Resize(sender As Object, e As EventArgs) Handles rtbLog.Resize
        If Me.ucControls.LogAutoScroll Then Call Me.rtbLog.ScrollToBottom()
    End Sub

    Private Sub btnLogClear_Click(sender As Object, e As EventArgs) Handles ucControls.ClickedLogClear
        Call Me.Logger.Clear()
    End Sub

    Private Sub nudLogEntries_ValueChanged(sender As Object, e As System.EventArgs) Handles ucControls.MaxLogEntriesValueChanged
        If Me.Logger Is Nothing Then Return
        Dim MaxEntries = CUInt(ucControls.MaxLogEntriesNUD.Value)
        If Me.Logger.MaxEntries <> MaxEntries Then
            Me.Logger.MaxEntries = MaxEntries
        End If
    End Sub

    Private Sub btnLogExport_Click(sender As Object, e As EventArgs) Handles ucControls.ClickedLogExport
        RaiseEvent ClickedLogExport(sender, e)
    End Sub
End Class
