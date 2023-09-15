Option Compare Binary
Option Explicit On
Option Strict On

Namespace Logger

    ''' <summary>
    ''' A circular buffer style logging class which stores N items for display in a Rich Text Box.
    ''' See: https://stackoverflow.com/a/55540909
    ''' </summary>
    Public Class Logger
        Public Event LogEntryAdded(ByVal sender As Object, ByVal e As EventArgs)
        Public Event LogEntriesCleared(ByVal sender As Object, ByVal e As EventArgs)
        Public Event LogEntriesUpdated(ByVal sender As Object, ByVal e As EventArgs)

        Private Sub RaiseLogEntriesUpdated(ByVal sender As Object, ByVal e As EventArgs) Handles Me.LogEntryAdded, Me.LogEntriesCleared
            RaiseEvent LogEntriesUpdated(sender, e)
        End Sub

        Private ReadOnly _log As Queue(Of LogEntry)
        Private _entryNumber As System.UInt32
        Private _maxEntries As System.UInt32
        Private ReadOnly _logLock As Object = New Object()
        Private ReadOnly _defaultColor As System.Drawing.Color = System.Drawing.Color.Black

        Public Property MaxEntries As System.UInt32
            Get
                Return _maxEntries
            End Get
            Set(value As System.UInt32)
                SyncLock _logLock
                    _maxEntries = value

                    If _log.Count > _maxEntries Then
                        Call DequeueOldEntries()
                        RaiseEvent LogEntriesUpdated(Me, Nothing)
                    End If
                End SyncLock
            End Set
        End Property

        Public ReadOnly Property EntryNumber As System.UInt32
            Get
                Return _entryNumber
            End Get
        End Property

        Private Class LogEntry
            Public EntryId As UInteger
            Public EntryTimeStamp As DateTime
            Public EntryText As String
            Public EntryColor As System.Drawing.Color
        End Class

        Private Structure ColorTableItem
            Public Index As UInteger
            Public RichColor As String
        End Structure

        ''' <summary>
        ''' Create an instance of the Logger class which stores <paramref name="maximumEntries"/> log entries.
        ''' </summary>
        Public Sub New(ByRef maximumEntries As System.UInt32)
            _log = New Queue(Of LogEntry)()
            _maxEntries = maximumEntries
        End Sub

        ''' <summary>
        ''' Retrieve the contents of the log as rich text, suitable for populating a <see cref="System.Windows.Forms.RichTextBox.Rtf"/> property.
        ''' </summary>
        ''' <param name="includeEntryNumbers">Option to prepend line numbers to each entry.</param>
        Public Function GetLogAsRichText(ByVal includeEntryNumbers As Boolean, ByVal includeTimeStamp As Boolean) As String
            Const FontTbl = "{\fonttbl{\f0\fnil Segoe UI;}}"

            SyncLock _logLock
                Dim sb = New System.Text.StringBuilder()
                Dim uniqueColors = Me.BuildRichTextColorTable()
                Call sb.AppendLine($"{{\rtf1\ansi\ansicpg1252\deff0\nouicompat\noxlattoyen{{\colortbl ;{String.Concat(uniqueColors.[Select](Function(d) d.Value.RichColor))}}}{FontTbl}\f0\fs18")

                For Each entry In _log

                    If includeTimeStamp OrElse includeEntryNumbers Then
                        Call sb.Append($"\cf1 ")

                        If includeEntryNumbers Then sb.Append($"{entry.EntryId}. ")

                        If includeTimeStamp Then
                            Call sb.Append($"[{entry.EntryTimeStamp.ToString("yyyy-MM-ddTHH:mm:sszzz")}] ")
                        End If
                    End If

                    Dim richColor = $"\cf{uniqueColors(entry.EntryColor).Index + 1}"

                    Call sb.Append($"{richColor} {entry.EntryText.Replace("\", "\'5c", StringComparison.Ordinal)}\par").AppendLine()
                Next

                Call sb.Append($"}}").AppendLine()
                Return sb.ToString()
            End SyncLock
        End Function

        Public Function GetLogAsPlainText(ByVal includeEntryNumbers As Boolean, ByVal includeTimeStamp As Boolean) As String
            SyncLock _logLock
                Dim sb = New System.Text.StringBuilder()

                For Each entry In _log
                    If includeEntryNumbers Then sb.Append($"{entry.EntryId}. ")
                    'If includeTimeStamp Then sb.Append($"{entry.EntryTimeStamp.ToShortDateString()} {entry.EntryTimeStamp.ToShortTimeString()}: ")
                    If includeTimeStamp Then sb.Append($"[{entry.EntryTimeStamp.ToString("yyyy-MM-ddTHH:mm:sszzz")}] ")
                    sb.Append(entry.EntryText).AppendLine()
                Next

                Return sb.ToString()
            End SyncLock
        End Function

        ''' <summary>
        ''' Adds <paramref name="text"/> as a log entry.
        ''' </summary>
        Public Sub AddToLog(ByRef text As String)
            AddToLog(text, _defaultColor)
        End Sub

        ''' <summary>
        ''' Adds <paramref name="text"/> as a log entry, And specifies a color to display it in.
        ''' </summary>
        Public Sub AddToLog(ByRef text As String, ByRef entryColor As System.Drawing.Color)
            SyncLock _logLock
                If _entryNumber >= System.UInt32.MaxValue Then _entryNumber = 0
                _entryNumber += Msu.MsuHelper.OneByte
                Dim logEntry = New LogEntry With {
                    .EntryId = _entryNumber,
                    .EntryTimeStamp = DateTime.Now,
                    .EntryText = text,
                    .EntryColor = entryColor
                }
                _log.Enqueue(logEntry)

                Call DequeueOldEntries()
            End SyncLock

            RaiseEvent LogEntryAdded(Me, Nothing)
        End Sub

        Private Sub DequeueOldEntries()
            While _log.Count > _maxEntries
                _log.Dequeue()
            End While
        End Sub

        ''' <summary>
        ''' Clears the entire log.
        ''' </summary>
        Public Sub Clear()
            SyncLock _logLock
                _log.Clear()
            End SyncLock
            RaiseEvent LogEntriesCleared(Me, Nothing)
        End Sub

        Private Function BuildRichTextColorTable() As Dictionary(Of System.Drawing.Color, ColorTableItem)
            Dim uniqueColors = New Dictionary(Of System.Drawing.Color, ColorTableItem)()
            Dim index = 0UI
            uniqueColors.Add(_defaultColor, New ColorTableItem() With {
                .Index = Math.Min(System.Threading.Interlocked.Increment(index), index - Msu.MsuHelper.OneByte),
                .RichColor = ColorToRichColorString(_defaultColor)
            })

            For Each c In _log.[Select](Function(l) l.EntryColor).Distinct()
                If c = _defaultColor OrElse uniqueColors.ContainsKey(c) Then Continue For
                uniqueColors.Add(c, New ColorTableItem() With {
                    .Index = Math.Min(System.Threading.Interlocked.Increment(index), index - Msu.MsuHelper.OneByte),
                    .RichColor = ColorToRichColorString(c)
                })
            Next

            Return uniqueColors
        End Function

        Private Shared Function ColorToRichColorString(ByVal c As System.Drawing.Color) As String
            Return $"\red{c.R}\green{c.G}\blue{c.B};"
        End Function
    End Class
End Namespace