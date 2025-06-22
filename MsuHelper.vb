Option Compare Binary
Option Explicit On
Option Strict On
Imports System.IO

Namespace Msu
    Public Module MsuHelper
        Public Const IsDevelopmentVersion As Boolean = False

        Public Const BackSlashChar As Char = "\"c
        Public Const DotChar As Char = "."c
        Public Const HyphenChar As Char = "-"c

        Public Const ZeroByte As Byte = 0
        Public Const OneByte As Byte = 1

        <VBFixedString(4)> Public Const JsonL As String = "json"
        <VBFixedString(5)> Public Const JsonExtL As String = DotChar & JsonL
        <VBFixedString(3)> Public Const PcmL As String = "pcm"
        <VBFixedString(4)> Public Const PcmExtL As String = DotChar & PcmL

        Public Function GetHashOfSelf() As String
            Dim oHash = System.Security.Cryptography.SHA1.Create
            Dim sFilePath = System.Reflection.Assembly.GetEntryAssembly().Location ' File of compiled dll

            If String.IsNullOrEmpty(sFilePath) Then
                sFilePath = System.Environment.ProcessPath ' Path of exe
            End If

            Dim fileStream =
                New System.IO.FileStream(
                    path:=sFilePath,
                    access:=System.IO.FileAccess.Read,
                    share:=System.IO.FileShare.ReadWrite Or IO.FileShare.Delete,
                    mode:=System.IO.FileMode.Open)

            Dim iHashBytes = oHash.ComputeHash(fileStream)

            Call fileStream.Close()

            Return Convert.ToBase64String(iHashBytes)
        End Function

        <System.Runtime.InteropServices.DllImport("shell32.dll", CharSet:=System.Runtime.InteropServices.CharSet.Unicode)>
        Private Function FindExecutable _
           (ByVal lpFile As String,
            ByVal lpDirectory As String,
            ByVal lpResult As String) As System.IntPtr
        End Function

        Public Function FindExecutable(ByRef FileSearch As String) As String
            Return FindExecutable(FileSearch, Application.StartupPath())
        End Function

        Public Function FindExecutable(ByRef FileSearch As String, ByRef SearchPath As String) As String
            Const MIN_SUCCESS_LNG As Byte = &H20
            Const MAX_PATH As Integer = &H104
            Dim retPath As String = New String(vbNullChar.Single, MAX_PATH)

            Dim [return] = FindExecutable(
                    lpFile:=FileSearch,
                    lpDirectory:=SearchPath,
                    lpResult:=retPath)

            Dim returnInt = [return].ToInt32

            If returnInt < MIN_SUCCESS_LNG Then
                Return Constants.vbNullString
            End If

            If retPath.Contains(vbNullChar.Single) Then
                Return retPath.Substring(0, retPath.IndexOf(vbNullChar.Single))
            Else
                Return retPath
            End If
        End Function

        ''' <summary>
        ''' Return True if the file is locked to prevent the write access.
        ''' </summary>
        ''' <param name="FilePath">filepath</param>
        ''' <exception cref="System.ArgumentNullException">
        ''' Thrown when <paramref name="FilePath"/><c>is null</c>.
        ''' </exception>
        ''' <exception cref="System.ArgumentException" />
        ''' <exception cref="System.NotSupportedException"/>
        ''' <exception cref="System.IO.FileNotFoundException"/>
        ''' <exception cref="System.Security.SecurityException"/>
        ''' <exception cref="System.IO.DirectoryNotFoundException"/>
        ''' <exception cref="System.UnauthorizedAccessException"/>
        ''' <exception cref="System.IO.PathTooLongException"/>
        ''' <exception cref="System.ArgumentOutOfRangeException"/>
        Public Function FileIsLocked(
        ByRef filePath As String) As Boolean

            FileIsLocked = FileIsLocked(filePath, System.IO.FileAccess.Write)

        End Function

        ''' <summary>
        ''' Return True if the file is locked to prevent the desired access.
        ''' </summary>
        ''' <param name="FilePath">filepath</param>
        ''' <param name="FileAccess">Desired file access</param>
        ''' <exception cref="System.ArgumentNullException">
        ''' Thrown when <paramref name="FilePath"/><c>is null</c>.
        ''' </exception>
        ''' <exception cref="System.ArgumentException" />
        ''' <exception cref="System.NotSupportedException"/>
        ''' <exception cref="System.IO.FileNotFoundException"/>
        ''' <exception cref="System.Security.SecurityException"/>
        ''' <exception cref="System.IO.DirectoryNotFoundException"/>
        ''' <exception cref="System.UnauthorizedAccessException"/>
        ''' <exception cref="System.IO.PathTooLongException"/>
        ''' <exception cref="System.ArgumentOutOfRangeException"/>
        Public Function FileIsLocked(
        ByRef filePath As String,
        ByRef fileAccess As System.IO.FileAccess
        ) As Boolean

            Return FileIsLocked(filePath, fileAccess, System.IO.FileShare.ReadWrite Or System.IO.FileShare.Delete)
        End Function

        ''' <summary>
        ''' Return True if the file is locked to prevent the desired access.
        ''' </summary>
        ''' <param name="FilePath">filepath</param>
        ''' <param name="FileAccess">Desired file access</param>
        ''' <param name="FileShare">Desired file share</param>
        ''' <exception cref="System.ArgumentNullException">
        ''' Thrown when <paramref name="FilePath"/><c>is null</c>.
        ''' </exception>
        ''' <exception cref="System.ArgumentException" />
        ''' <exception cref="System.NotSupportedException"/>
        ''' <exception cref="System.IO.FileNotFoundException"/>
        ''' <exception cref="System.Security.SecurityException"/>
        ''' <exception cref="System.IO.DirectoryNotFoundException"/>
        ''' <exception cref="System.UnauthorizedAccessException"/>
        ''' <exception cref="System.IO.PathTooLongException"/>
        ''' <exception cref="System.ArgumentOutOfRangeException"/>
        Public Function FileIsLocked(
        ByRef filePath As String,
        ByRef fileAccess As System.IO.FileAccess,
        ByRef fileShare As System.IO.FileShare
        ) As Boolean

            Try

                ' Try opening the and closing the file
                Call OpenAndCloseFile(
                filePath:=filePath,
                fileAccess:=fileAccess,
                fileShare:=fileShare)

                ' File is not locked for the specified access mode
                Return False

            Catch ex As System.IO.IOException

                ' File is locked
                Return True

            End Try

        End Function

        ''' <summary>
        ''' Opens and closes a File immediately. Will throw <see cref="System.IO.IOException"/> if the file is locked by another process.
        ''' </summary>
        ''' <param name="FilePath">filepath</param>
        ''' <param name="FileAccess">Desired file access</param>
        ''' <exception cref="System.ArgumentNullException">
        ''' Thrown when <paramref name="FilePath"/><c>is null</c>.
        ''' </exception>
        ''' <exception cref="System.ArgumentException" />
        ''' <exception cref="System.NotSupportedException"/>
        ''' <exception cref="System.IO.FileNotFoundException"/>
        ''' <exception cref="System.IO.IOException"/>
        ''' <exception cref="System.Security.SecurityException"/>
        ''' <exception cref="System.IO.DirectoryNotFoundException"/>
        ''' <exception cref="System.UnauthorizedAccessException"/>
        ''' <exception cref="System.IO.PathTooLongException"/>
        ''' <exception cref="System.ArgumentOutOfRangeException"/>
        Public Sub OpenAndCloseFile(
        ByRef filePath As String
        )
            Call OpenAndCloseFile(
                filePath:=filePath,
                fileAccess:=System.IO.FileAccess.Write,
                fileShare:=System.IO.FileShare.ReadWrite Or System.IO.FileShare.Delete)
        End Sub

        ''' <summary>
        ''' Opens and closes a File immediately. Will throw <see cref="System.IO.IOException"/> if the file is locked by another process.
        ''' </summary>
        ''' <param name="FilePath">filepath</param>
        ''' <param name="FileAccess">Desired file access</param>
        ''' <exception cref="System.ArgumentNullException">
        ''' Thrown when <paramref name="FilePath"/><c>is null</c>.
        ''' </exception>
        ''' <exception cref="System.ArgumentException" />
        ''' <exception cref="System.NotSupportedException"/>
        ''' <exception cref="System.IO.FileNotFoundException"/>
        ''' <exception cref="System.IO.IOException"/>
        ''' <exception cref="System.Security.SecurityException"/>
        ''' <exception cref="System.IO.DirectoryNotFoundException"/>
        ''' <exception cref="System.UnauthorizedAccessException"/>
        ''' <exception cref="System.IO.PathTooLongException"/>
        ''' <exception cref="System.ArgumentOutOfRangeException"/>
        Public Sub OpenAndCloseFile(
        ByRef filePath As String,
        ByRef fileAccess As System.IO.FileAccess
        )
            Call OpenAndCloseFile(
                filePath:=filePath,
                fileAccess:=fileAccess,
                fileShare:=System.IO.FileShare.ReadWrite Or System.IO.FileShare.Delete)
        End Sub

        ''' <summary>
        ''' Opens and closes a File immediately. Will throw <see cref="System.IO.IOException"/> if the file is locked by another process.
        ''' </summary>
        ''' <param name="FilePath">filepath</param>
        ''' <param name="FileAccess">Desired file access</param>
        ''' <param name="FileShare">Desired file share</param>
        ''' <exception cref="System.ArgumentNullException">
        ''' Thrown when <paramref name="FilePath"/><c>is null</c>.
        ''' </exception>
        ''' <exception cref="System.ArgumentException" />
        ''' <exception cref="System.NotSupportedException"/>
        ''' <exception cref="System.IO.FileNotFoundException"/>
        ''' <exception cref="System.IO.IOException"/>
        ''' <exception cref="System.Security.SecurityException"/>
        ''' <exception cref="System.IO.DirectoryNotFoundException"/>
        ''' <exception cref="System.UnauthorizedAccessException"/>
        ''' <exception cref="System.IO.PathTooLongException"/>
        ''' <exception cref="System.ArgumentOutOfRangeException"/>
        Public Sub OpenAndCloseFile(
        ByRef filePath As String,
        ByRef fileAccess As System.IO.FileAccess,
        ByRef fileShare As System.IO.FileShare
        )

            ' Try opening the file with the specified file
            Dim objFs As New _
        System.IO.FileStream(
            path:=filePath,
            access:=fileAccess,
            share:=fileShare,
            mode:=System.IO.FileMode.Open)

            ' Close the opened file immediately
            Call objFs.Close()
        End Sub

#If WINDOWS Then
        Public Function PathStartsWithLocalDrive(ByRef path As String) As Boolean
            ' Check for:
            ' C:\
            ' 012

            For i As Integer = 0 To path.Length
                Dim Cur As Char = path.Chars(i)

                Select Case i
                    Case 0 ' A B C D E...
                        Select Case Strings.AscW(Cur)
                            Case 65 To 90, 97 To 122 ' A - Z, a - z
                            Case Else
                                Return False
                        End Select
                    Case 1 ' :
                        If Cur.CompareTo(":"c) = 0 Then
                            Return True
                        Else
                            Return False
                        End If
                End Select
            Next
            Return False
        End Function

        Public Function PathRemoveUncLocalPref(ByRef path As String) As String

            If PathHasUncLocalPref(path) Then
                Return PathRemoveUncLocalPrefDirect(path)
            Else
                Return path
            End If

        End Function

        Private Function PathRemoveUncLocalPrefDirect(ByRef path As String) As String
            Return Strings.Right(path, path.Length - 4)
        End Function

        Public Function PathHasUncLocalPref(ByRef path As String) As Boolean
            ' Check for:
            ' \\.\C:\
            ' \\?\C:\
            ' 0123456
            If path Is Nothing Then Return False

            For i As Integer = 0 To path.Length

                Dim Cur As Char = path.Chars(i)

                Select Case i
                    Case 0, 1, 3 ' \
                        If Cur.CompareTo(MsuHelper.BackSlashChar) <> 0 Then
                            Return False
                        End If
                    Case 2 ' . or ?
                        If Cur.CompareTo("."c) = 0 OrElse Cur.CompareTo("?"c) = 0 Then
                        Else
                            Return False
                        End If
                    Case 4 ' A B C D E...
                        Select Case Strings.AscW(Cur)
                            Case 65 To 90, 97 To 122 ' A - Z, a - z
                            Case Else
                                Return False
                        End Select
                    Case 5 ' :
                        If Cur.CompareTo(":"c) = 0 Then
                            Return True
                        Else
                            Return False
                        End If
                End Select
            Next
            Return False
        End Function

        Public Sub SetPathsToSamePrefix(ByRef pathInOut1 As String, ByRef pathInOut2 As String)

            If PathHasUncLocalPref(pathInOut1) Then
                If PathHasUncLocalPref(pathInOut2) Then
                    pathInOut2 = String.Concat(Strings.Left(pathInOut1, 4), PathRemoveUncLocalPrefDirect(pathInOut2))
                Else
                    If PathStartsWithLocalDrive(pathInOut2) Then
                        pathInOut2 = String.Concat(Strings.Left(pathInOut1, 4), pathInOut2)
                    End If
                End If
            Else
                If PathHasUncLocalPref(pathInOut2) Then
                    If PathStartsWithLocalDrive(pathInOut1) Then
                        pathInOut1 = String.Concat(Strings.Left(pathInOut2, 4), pathInOut1)
                    End If
                End If
            End If

        End Sub

        Public Sub CopyPathPrefix(ByVal pathGetPrefix As String, ByRef pathSetPrefix As String)
            If PathHasUncLocalPref(pathGetPrefix) Then
                If PathHasUncLocalPref(pathSetPrefix) Then
                    pathSetPrefix = String.Concat(Strings.Left(pathGetPrefix, 4), PathRemoveUncLocalPrefDirect(pathSetPrefix))
                Else
                    If PathStartsWithLocalDrive(pathSetPrefix) Then
                        pathSetPrefix = String.Concat(Strings.Left(pathGetPrefix, 4), pathSetPrefix)
                    End If
                End If
            Else
                If PathHasUncLocalPref(pathSetPrefix) Then
                    If PathStartsWithLocalDrive(pathGetPrefix) Then
                        pathSetPrefix = PathRemoveUncLocalPrefDirect(pathSetPrefix)
                    End If
                End If
            End If
        End Sub

        Public Function GetCopyPathPrefix(pathGetPrefix As String, pathSetPrefix As String) As String
            Dim pathSetPrefixCopy = pathSetPrefix
            Call CopyPathPrefix(pathGetPrefix, pathSetPrefixCopy)
            Return pathSetPrefixCopy
        End Function
#End If

        '''' <summary>
        '''' Returns the parent folder for the specified filepath
        '''' </summary>
        '''' <param name="strFilePath">filepath</param>
        'Public Function GetParentFolderPath(ByRef strFilePath As String) As String
        '    Dim intLastBsPos As Integer = GetLastBackslashPos(strFilePath)

        '    Select Case intLastBsPos
        '        Case 0

        '            ' No backslash in string -> no parent folder
        '            Return vbNullString

        '        Case Else

        '            ' Return the path of the parent folder
        '            Return Left(strFilePath, intLastBsPos - 1)

        '    End Select
        'End Function

        '''' <summary>
        '''' Returns the parent folder name for the specified filepath
        '''' </summary>
        '''' <param name="strFilePath">filepath</param>
        'Public Function GetParentFolderName(ByRef strFilePath As String) As String
        '    Dim intLastBsPos As Integer = GetLastBackslashPos(strFilePath)

        '    Dim int2ndLastBsPos As Integer = InStrRev(strFilePath, chrBslash, intLastBsPos - 1)

        '    Select Case intLastBsPos
        '        Case 0

        '            ' No backslash in string -> no parent folder
        '            Return vbNullString

        '        Case Else

        '            ' Return the path of the parent folder
        '            Return Mid(strFilePath, int2ndLastBsPos + 1, intLastBsPos - 1 - int2ndLastBsPos)

        '    End Select
        'End Function

        '''' <summary>
        '''' Returns the filename for the specified filepath
        '''' </summary>
        '''' <param name="strFilePath">filepath</param>
        'Public Function GetFileName(ByRef strFilePath As String) As String
        '    Dim intLastBsPos As Integer = GetLastBackslashPos(strFilePath)

        '    Select Case intLastBsPos
        '        Case 0

        '            ' No backslash in string -> no filename
        '            Return vbNullString

        '        Case Else

        '            ' Return the path of the parent folder
        '            Return Right(strFilePath, strFilePath.Length - intLastBsPos)

        '    End Select
        'End Function

        '''' <summary>
        '''' Returns the basename for the specified filepath / filename
        '''' </summary>
        '''' <param name="strFilePath">filepath / filename</param>
        'Public Function GetBaseName(ByRef sFilePath As String) As String
        '    Dim intLastBsPos As Integer = GetLastBackslashPos(sFilePath)
        '    Dim intDotPos As Integer = GetLastDotPos(sFilePath)

        '    ' If the last dot is in the filename (path has file extension)
        '    If intDotPos > intLastBsPos Then

        '        ' Return the basename
        '        Select Case intLastBsPos
        '            Case 0

        '                ' No backslash (Filename was specified)
        '                Return Left(sFilePath, intDotPos - 1)

        '            Case Else

        '                ' Has backslash (Filepath was specified)
        '                Return Mid(sFilePath, intLastBsPos + 1, intDotPos - (intLastBsPos + 1))

        '        End Select
        '    End If

        '    Return vbNullString
        'End Function

        '''' <summary>
        '''' Returns the file extension for the specified filepath / filename
        '''' </summary>
        '''' <param name="strFilePath">filepath / filename</param>
        'Public Function GetFileExt(ByRef strFilePath As String) As String
        '    Dim intDotPos As UInt16

        '    ' Get position for the last dot
        '    intDotPos = GetLastDotPos(strFilePath)

        '    Select Case intDotPos
        '        Case 0

        '            ' No dot in string -> no file extension
        '            Return vbNullString

        '        Case Else

        '            ' Return the found file extension
        '            Return Right(strFilePath, Len(strFilePath) - intDotPos)

        '    End Select
        'End Function

        ''' <summary>
        ''' Returns the position of the seperator for the file extension
        ''' </summary>
        ''' <param name="filePath">filepath / filename</param>
        Public Function GetLastDotPos(ByRef filePath As String) As Integer
            ' Return strFilePath.LastIndexOf(modGeneralStuff.chrDot)
            Return InStrRev(
            StringCheck:=filePath,
            StringMatch:=MsuHelper.DotChar,
            Compare:=vbBinaryCompare)
        End Function

        '''' <summary>
        '''' Returns the position of the seperator for the filename
        '''' </summary>
        '''' <param name="strFilePath">filepath / filename</param>
        'Public Function GetLastBackslashPos(ByRef strFilePath As String) As Integer
        '    Return InStrRev(
        '        StringCheck:=strFilePath,
        '        StringMatch:=chrBslash,
        '        Compare:=vbBinaryCompare)
        'End Function

        ''' <summary>
        ''' Returns the position of the last hyphen for the filename
        ''' </summary>
        ''' <param name="filePath">filepath / filename</param>
        Public Function GetLastHyphenPos(ByRef filePath As String) As Integer
            Return InStrRev(
            StringCheck:=filePath,
            StringMatch:=MsuHelper.HyphenChar,
            Compare:=vbBinaryCompare)
        End Function

        Public Function GetPathPartsStringCollection(ByRef strPath As String) As Specialized.StringCollection
            Dim objDictPathParts As New Specialized.StringCollection
            Dim i As Integer = objDictPathParts.Add(System.IO.Path.TrimEndingDirectorySeparator(strPath))

            Do
                Dim strParentDir As String = System.IO.Path.GetDirectoryName(objDictPathParts(i))

                If String.IsNullOrWhiteSpace(strParentDir) Then
                    Return objDictPathParts
                End If

                i = objDictPathParts.Add(strParentDir)
            Loop
        End Function

        Public Function GetPathPartNamesStringCollection(ByRef strPath As String) As Specialized.StringCollection
            Dim objDictPathParts As Specialized.StringCollection = GetPathPartsStringCollection(strPath)

            For i = 0 To objDictPathParts.Count() - 1
                objDictPathParts(i) = System.IO.Path.GetFileName(objDictPathParts(i))
            Next

            Return objDictPathParts
        End Function

        ''' <summary>
        ''' Replace all chars that get converted to "_" by GoogleDrive zipping with the Placeholder "?"
        ''' </summary>
        ''' <remarks>
        ''' Result after putting all 127 Ascii Chars into Google Drive and zipping:<br />
        ''' _ _ _ !_#$_()_+,-.-0123456789_=_@ABCDEFGHIJKLMNOPQRSTUVWXYZ[_]^_`abcdefghijklmnopqrstuvwxyz{_}~
        ''' </remarks>
        ''' <param name="fileName">Name of a File or Folder</param>
        ''' <returns>String with replaced placeholder chars</returns>
        Public Function GetPlaceholderForInvalidGoogleDriveChars(ByRef fileName As String) As String
            If String.IsNullOrWhiteSpace(fileName) Then Return Constants.vbNullString
            Dim OutChars = fileName.ToCharArray

            For i As Integer = 0 To OutChars.Length - 1

                ' Get Unicode Id of current char
                Dim CurCharId As UInt32 = Convert.ToUInt32(OutChars(i))

                Select Case CurCharId
                    Case 65 To 90, 97 To 122 ' A - Z, a - z
                    Case 48 To 57 ' 0 - 9
                    Case 32 ' 
                    Case 33 ' !
                    Case 35 ' #
                    Case 36 ' $
                    Case 40 To 41 ' ()
                    Case 43 ' +
                    Case 44 ' ,
                    Case 61 ' =
                    Case 64 ' @
                    Case 91, 93 ' []
                    Case 94 ' ^
                    Case 123, 125 ' { }
                    Case 126 ' ~
                    Case Else
                        ' Replace with Placeholder
                        OutChars(i) = "?"c
                End Select
            Next

            Return OutChars
        End Function

        ' https://stackoverflow.com/a/954837
        Public Function IsDirectoryEmpty(ByRef path As String) As Boolean
            Return Not Directory.EnumerateFileSystemEntries(path).Any()
        End Function
    End Module
End Namespace