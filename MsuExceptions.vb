Option Compare Binary
Option Explicit On
Option Strict On
Imports MsuAltSelect.Msu.Tracks

Namespace Msu
    Namespace Ex
        Public Class MsuTrackAlreadyExistsException : Inherits System.Exception
            Private _MsuTrackExisting As MsuTrack
            Public Property MsuTrackExisting As MsuTrack
                Get
                    Return _MsuTrackExisting
                End Get
                Protected Set(value As MsuTrack)
                    _MsuTrackExisting = value
                End Set
            End Property

            Public Overrides ReadOnly Property Message As String
                Get
                    Dim messageOut As String =
                    "A Track with the Id " & Me.MsuTrackExisting.TrackNumber & " already exists."

                    If String.IsNullOrWhiteSpace(Me.MsuTrackExisting.Title) Then
                    Else
                        messageOut = String.Concat(messageOut, System.Environment.NewLine,
                   $"Title: ""{Me.MsuTrackExisting.Title}""")
                    End If

                    Return messageOut
                End Get
            End Property

            Public Sub New(ByRef trackExisting As MsuTrack)
                Me.MsuTrackExisting = trackExisting
            End Sub
        End Class

        Public Class MsuTrackAltAlreadyExistsException : Inherits MsuTrackAlreadyExistsException

            Public Enum ExceptionType
                DuplicateId
                DuplicatePath
            End Enum

            Private _MsuTrackAltExisting As MsuTrackAlt
            Public Property MsuTrackAltExisting As MsuTrackAlt
                Get
                    Return _MsuTrackAltExisting
                End Get
                Protected Set(value As MsuTrackAlt)
                    _MsuTrackAltExisting = value
                End Set
            End Property

            Private _MsuTrackAltAlreadyExistsExceptionType As ExceptionType
            Public Property AlreadyExistsExceptionType As ExceptionType
                Get
                    Return _MsuTrackAltAlreadyExistsExceptionType
                End Get
                Protected Set(value As ExceptionType)
                    _MsuTrackAltAlreadyExistsExceptionType = value
                End Set
            End Property

            Public Overrides ReadOnly Property Message As String
                Get
                    Dim messageOut As String =
                    "A Track with the "

                    Select Case Me.AlreadyExistsExceptionType
                        Case ExceptionType.DuplicateId

                            messageOut = String.Concat(messageOut,
                    "AltId ", Me.MsuTrackAltExisting.AltNumber, " "c)

                        Case Else

                            messageOut = String.Concat(messageOut,
                    "Location """, Me.MsuTrackAltExisting.LocationRelative, """ ")

                    End Select

                    messageOut = String.Concat(messageOut,
                    "already exists for the TrackId ", Me.MsuTrackExisting.TrackNumber, "."c)

                    Dim messageOutSuff As String = Constants.vbNullString

                    If Me.AlreadyExistsExceptionType <> ExceptionType.DuplicateId Then
                        messageOutSuff = String.Concat(System.Environment.NewLine,
                   $"AltId: ""{Me.MsuTrackAltExisting.AltNumber}""")
                    End If

                    If String.IsNullOrWhiteSpace(Me.MsuTrackAltExisting.Title) Then
                    Else
                        messageOutSuff = String.Concat(messageOutSuff, System.Environment.NewLine,
                   $"Title: ""{Me.MsuTrackAltExisting.Title}""")
                    End If

                    If messageOutSuff IsNot Nothing Then
                        messageOut = String.Concat(messageOut, System.Environment.NewLine, messageOutSuff)
                    End If

                    Return messageOut
                End Get
            End Property

            Public Sub New(ByRef trackExisting As MsuTrackAlt, ByRef alreadyExistsExceptionType As ExceptionType)
                Call MyBase.New(trackExisting.Parent)
                Me.MsuTrackAltExisting = trackExisting
                Me.AlreadyExistsExceptionType = alreadyExistsExceptionType
            End Sub
        End Class

        Public Class MsuAutoSwitchDataInvalidException : Inherits System.Exception
            Const _MessageNoParam As String = "The data for AutoSwitching is invalid."
            Private _Message As String

            Public Overrides ReadOnly Property Message As String
                Get
                    If String.IsNullOrWhiteSpace(_Message) Then
                        Return MyBase.Message
                    Else
                        Return _Message
                    End If
                End Get
            End Property

            Public Sub New()
                MyBase.New(_MessageNoParam)
            End Sub

            Public Sub New(innerException As Exception)
                MyBase.New(_MessageNoParam, innerException)
            End Sub

            Public Sub New(message As String)
                MyBase.New(message)
            End Sub

            Public Sub New(message As String, innerException As Exception)
                MyBase.New(message, innerException)
            End Sub

            Public Sub New(ByRef msuTrackAlt As MsuTrackAlt, ByRef referencesOwnTrackNumber As Boolean)
                MyBase.New(_MessageNoParam)
                Dim msuTrack As MsuTrack = msuTrackAlt.Parent

                If referencesOwnTrackNumber Then
                    _Message =
                "Tracks cannot have an alt. track with AutoSwitch for its own TrackId."
                Else
                    _Message = _MessageNoParam
                End If

                _Message = String.Concat(_Message, System.Environment.NewLine, System.Environment.NewLine,
                "Track ", msuTrack.TrackNumber)

                If String.IsNullOrWhiteSpace(msuTrack.Title) Then
                Else
                    _Message = _Message &
               $": ""{msuTrack.Title}"""
                End If

                If String.IsNullOrWhiteSpace(msuTrackAlt.Title) Then
                    _Message = _Message & System.Environment.NewLine &
               $"AltId: {msuTrackAlt.AltNumber}"
                Else
                    _Message = _Message & System.Environment.NewLine &
               $"AltTrack {msuTrackAlt.AltNumber}: ""{msuTrackAlt.Title}"""
                End If
            End Sub
        End Class

        Public Class MsuAutoSwitchDataDuplicateException : Inherits MsuAutoSwitchDataInvalidException
            Const _MessageNoParam As String =
        "The Data for AutoSwitching is invalid." & vbCrLf &
        "Duplicates found."
            Private _MsuTrackAltSwapDup() As MsuTrackAlt
            Private _MsuTrackWithDuplicates As MsuTrack
            Private _MsuTrackDuplicate As MsuTrack
            Private _Message As String

            Public Property MsuTrackAltSwapDuplicates As MsuTrackAlt()
                Get
                    Return _MsuTrackAltSwapDup
                End Get
                Protected Set(value As MsuTrackAlt())
                    _MsuTrackAltSwapDup = value

                    If _MsuTrackAltSwapDup Is Nothing Then Return
                    If _MsuTrackAltSwapDup.Length = 0 Then Return

                    _MsuTrackWithDuplicates = _MsuTrackAltSwapDup.First().Parent
                End Set
            End Property

            Public ReadOnly Property MsuTrackWithDuplicates As MsuTrack
                Get
                    Return _MsuTrackWithDuplicates
                End Get
            End Property

            Public Property MsuTrackDuplicate As MsuTrack
                Get
                    Return _MsuTrackDuplicate
                End Get
                Protected Set(value As MsuTrack)
                    _MsuTrackDuplicate = value
                End Set
            End Property

            Public Overrides ReadOnly Property Message As String
                Get
                    If String.IsNullOrWhiteSpace(_Message) Then
                        Return MyBase.Message
                    Else
                        Return _Message
                    End If
                End Get
            End Property

            Private Sub SetMessage()
                _Message =
            "The Data for AutoSwitching is invalid." & System.Environment.NewLine &
            "Duplicate alt. Tracks with AutoSwitching found"

                If Me.MsuTrackWithDuplicates Is Nothing Then
                    _Message = _Message &
            "."c
                Else
                    _Message = _Message &
            " for TrackId " & Me.MsuTrackWithDuplicates.TrackNumber & "."
                End If

                If Me.MsuTrackDuplicate IsNot Nothing Then

                    If String.IsNullOrWhiteSpace(Me.MsuTrackDuplicate.Title) Then
                        _Message = _Message & System.Environment.NewLine & System.Environment.NewLine &
            "TrackId: " & Me.MsuTrackDuplicate.TrackNumber
                    Else
                        _Message = _Message & System.Environment.NewLine & System.Environment.NewLine &
            "Track " & Me.MsuTrackDuplicate.TrackNumber & ": """ & Me.MsuTrackDuplicate.Title & """"
                    End If

                End If

                If _MsuTrackAltSwapDup Is Nothing OrElse _MsuTrackAltSwapDup.Length = 0 Then Return

                _Message = _Message & System.Environment.NewLine & System.Environment.NewLine &
            "Duplicates:"

                For i As Integer = LBound(Me.MsuTrackAltSwapDuplicates) To UBound(Me.MsuTrackAltSwapDuplicates)

                    If String.IsNullOrWhiteSpace(Me.MsuTrackAltSwapDuplicates(i).Title) Then
                        _Message = _Message & System.Environment.NewLine &
            "AltId: " & Me.MsuTrackAltSwapDuplicates(i).AltNumber
                    Else
                        _Message = _Message & System.Environment.NewLine &
            "AltTrack " & Me.MsuTrackAltSwapDuplicates(i).AltNumber & ": """ & Me.MsuTrackAltSwapDuplicates(i).Title & """"
                    End If
                Next
            End Sub

            Public Sub New(msuTrackAltSwapDuplicates As MsuTrackAlt())
                Call MyBase.New(_MessageNoParam)
                Me.MsuTrackAltSwapDuplicates = msuTrackAltSwapDuplicates
                Call SetMessage()
            End Sub

            Public Sub New(msuTrackAltSwapDuplicates As MsuTrackAlt(), innerException As Exception)
                Call MyBase.New(_MessageNoParam, innerException)
                Me.MsuTrackAltSwapDuplicates = msuTrackAltSwapDuplicates
                Call SetMessage()
            End Sub

            Public Sub New(msuTrackAltSwapDuplicates As MsuTrackAlt(), msuTrackDuplicate As MsuTrack)
                Call MyBase.New(_MessageNoParam)
                Me.MsuTrackDuplicate = msuTrackDuplicate
                Me.MsuTrackAltSwapDuplicates = msuTrackAltSwapDuplicates
                Call SetMessage()
            End Sub

            Public Sub New(msuTrackAltSwapDuplicates As MsuTrackAlt(), msuTrackDuplicate As MsuTrack, innerException As Exception)
                Call MyBase.New(_MessageNoParam, innerException)
                Me.MsuTrackDuplicate = msuTrackDuplicate
                Me.MsuTrackAltSwapDuplicates = msuTrackAltSwapDuplicates
                Call SetMessage()
            End Sub

            Public Sub New()
                MyBase.New(_MessageNoParam)
            End Sub

            Public Sub New(innerException As Exception)
                MyBase.New(_MessageNoParam, innerException)
            End Sub

            Public Sub New(message As String)
                MyBase.New(message)
            End Sub

            Public Sub New(message As String, innerException As Exception)
                MyBase.New(message, innerException)
            End Sub

        End Class

        Public Class MsuTrackNumberChangeException : Inherits System.Exception
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "The Track Id of an existing track cannot be changed."
                End Get
            End Property
        End Class

        Public Class MsuCurrentTrackAltCantBeDeterminedException : Inherits System.Exception
            Private _MsuTrack As MsuTrack

            Public Property MsuTrack As MsuTrack
                Get
                    Return _MsuTrack
                End Get
                Protected Set(value As MsuTrack)
                    _MsuTrack = value
                End Set
            End Property

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "The currently selected alt. Track Version for the TrackId " & Me.MsuTrack.TrackNumber & " could not be determined."
                End Get
            End Property

            Public Sub New(ByRef msuTrack As MsuTrack)
                Me.MsuTrack = msuTrack
            End Sub
        End Class

        Public Class MsuAltTracksAllInSwapDirException : Inherits MsuCurrentTrackAltCantBeDeterminedException
            Public Sub New(ByRef msuTrack As MsuTrack)
                Call MyBase.New(msuTrack)
            End Sub
        End Class

        Public Class MsuAltTracksMultipleNotInSwapDirException : Inherits MsuCurrentTrackAltCantBeDeterminedException
            Private _TrackAltsNotInSwapDir As MsuTrackAlt()

            Public Property TrackAltsNotInSwapDir As MsuTrackAlt()
                Get
                    Return _TrackAltsNotInSwapDir
                End Get
                Protected Set(value As MsuTrackAlt())
                    _TrackAltsNotInSwapDir = value
                End Set
            End Property

            Public Overrides ReadOnly Property Message As String
                Get
                    Dim messageOut As String = MyBase.Message & System.Environment.NewLine & System.Environment.NewLine &
                "Multiple alt. tracks not in their swap directory:"

                    For Each objTrackAlt In Me.TrackAltsNotInSwapDir
                        messageOut = messageOut & System.Environment.NewLine & System.Environment.NewLine &
                    "alt. Track with Id " & objTrackAlt.AltNumber & " not found in '" & objTrackAlt.FilePath & "'"
                    Next

                    Return messageOut
                End Get
            End Property

            Public Sub New(ByRef track As MsuTrack, ByRef alt1 As MsuTrackAlt, ByRef alt2 As MsuTrackAlt)
                Call MyBase.New(track)
                Dim objTrackAltsNotInSwapDir As MsuTrackAlt()

                ReDim objTrackAltsNotInSwapDir(1)

                objTrackAltsNotInSwapDir(0) = alt1
                objTrackAltsNotInSwapDir(1) = alt2

                Me.TrackAltsNotInSwapDir = objTrackAltsNotInSwapDir
            End Sub

            Public Sub New(ByRef track As MsuTrack, trackAltsNotInSwapDir As MsuTrackAlt())
                Call MyBase.New(track)
                Me.TrackAltsNotInSwapDir = trackAltsNotInSwapDir
            End Sub
        End Class

        Public Class MsuTrackSwapDirectoryNotFoundException : Inherits System.IO.DirectoryNotFoundException
            Private _Message As String

            Public Overrides ReadOnly Property Message As String
                Get
                    If String.IsNullOrWhiteSpace(_Message) Then
                        Return MyBase.Message
                    Else
                        Return _Message
                    End If
                End Get
            End Property

            Public Sub New(ByRef msuTrackAlt As MsuTrackAlt)
                MyBase.New()

                _Message =
            "The directory for alt. Track " & msuTrackAlt.AltNumber

                If String.IsNullOrWhiteSpace(msuTrackAlt.Title) Then
                Else
                    _Message = _Message &
            " (" & msuTrackAlt.Title & ")"
                End If


                If String.IsNullOrWhiteSpace(msuTrackAlt.Parent.Title) Then
                    _Message = _Message &
            " (Track " & msuTrackAlt.Parent.TrackNumber & ": " & msuTrackAlt.Parent.Title & ")"
                Else
                    _Message = _Message &
            " (TrackId " & msuTrackAlt.Parent.TrackNumber & ")"
                End If

                _Message = _Message & System.Environment.NewLine &
            System.Environment.NewLine &
            "Missing Direcory:" & System.Environment.NewLine &
            msuTrackAlt.LocationAbsolute
            End Sub

        End Class

        Public Class MsuTrackFileCannotBeMovedException : Inherits System.IO.IOException
            Private _MsuTrack As MsuTrack
            Private _MsuTrackAlt As MsuTrackAlt

            Public Property MsuTrack As MsuTrack
                Get
                    Return _MsuTrack
                End Get
                Protected Set(value As MsuTrack)
                    _MsuTrack = value
                End Set
            End Property
            Public Property MsuTrackAlt As MsuTrackAlt
                Get
                    Return _MsuTrackAlt
                End Get
                Protected Set(value As MsuTrackAlt)
                    _MsuTrackAlt = value
                End Set
            End Property

            Public Overrides ReadOnly Property Message As String
                Get
                    Dim strFilePath As String = Me.PcmFilePath

                    Dim messageOut As String

                    If TypeOf Me Is MsuCurrentTrackFileCannotBeMovedException Then
                        messageOut = "The Currently used Msu Track in the path """ & strFilePath & """ cannot be moved to its swap directory."
                    ElseIf TypeOf Me Is MsuSwapTrackFileCannotBeMovedException Then
                        messageOut = "The alt. Msu Track with the path """ & strFilePath & """ cannot be moved to the base path."
                    Else
                        messageOut = "The Msu Track with the path """ & strFilePath & """ cannot be moved."
                    End If

                    If Me.PcmFileExists Then
                        Return messageOut & " File is in use."
                    ElseIf Me.PcmFilePathIsDirecotry Then
                        Return messageOut & " A Directory with that path already exists."
                    Else
                        Return messageOut & " File does not exist."
                    End If
                End Get
            End Property

            Public ReadOnly Property PcmFilePath As String
                Get
                    If Me.MsuTrackAlt Is Nothing Then
                        Return Me.MsuTrackAlt.FilePath
                    Else
                        Return Me.MsuTrack.FilePath
                    End If
                End Get
            End Property

            Public ReadOnly Property PcmFileExists As Boolean
                Get
                    Return System.IO.File.Exists(Me.PcmFilePath)
                End Get
            End Property

            Public ReadOnly Property PcmFilePathIsDirecotry As Boolean
                Get
                    Return System.IO.Directory.Exists(Me.PcmFilePath)
                End Get
            End Property

            Public Sub New(ByRef track As MsuTrack)
                MsuTrack = track
            End Sub

            Public Sub New(ByRef trackAlt As MsuTrackAlt)
                MsuTrackAlt = trackAlt
                MsuTrack = MsuTrackAlt.Parent
            End Sub
        End Class

        Public Class MsuCurrentTrackFileCannotBeMovedException : Inherits MsuTrackFileCannotBeMovedException

            Public Sub New(ByRef msuTrack As MsuTrack)

                MyBase.New(msuTrack)

                Try
                    ' Get the current alt. Track for this TrackId
                    MyBase.MsuTrackAlt = msuTrack.GetCurrentTrackAlt
                Catch ex As Exception
                    ' Ignore
                End Try
            End Sub
        End Class

        Public Class MsuSwapTrackFileCannotBeMovedException : Inherits MsuTrackFileCannotBeMovedException

            Public Sub New(ByRef msuTrackAlt As MsuTrackAlt)
                MyBase.New(msuTrackAlt)
            End Sub
        End Class


        Public Class MsuExceptionDisplay
            Public Shared Function DisplayExceptionAsMessageBox(owner As IWin32Window, ex As System.Exception) As System.Windows.Forms.DialogResult
                Return DisplayExceptionAsMessageBox(owner, ex, MessageBoxIcon.Error)
            End Function
            Public Shared Function DisplayExceptionAsMessageBox(owner As IWin32Window, ex As System.Exception, icon As MessageBoxIcon) As System.Windows.Forms.DialogResult
                Return System.Windows.Forms.MessageBox.Show(
                    owner:=owner,
                    text:=ex.Message,
                    caption:=ex.GetType.ToString,
                    buttons:=MessageBoxButtons.OK,
                    icon:=icon
                )
            End Function
        End Class
    End Namespace
End Namespace