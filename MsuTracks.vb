Option Compare Binary
Option Explicit On
Option Strict On
Imports Newtonsoft.Json
Imports MsuAltSelect.Msu.Ex

Namespace Msu
    Namespace Tracks
        Public Class MsuTracks : Implements IDisposable
            Public Event TrackAltSwitched(ByVal sender As Object, ByVal e As MsuAltSelect.Msu.Tracks.MsuTrack.TrackAltSwitchedEventArgs)
            Public Event ConvertedFilesSwitched(ByVal sender As Object, ByVal e As EventArgs)
            Private disposedValue As Boolean

            Private WithEvents _Logger As Logger.Logger

            <Newtonsoft.Json.JsonIgnore>
            Public Property Settings As Msu.Settings.Settings

            <Newtonsoft.Json.JsonIgnore>
            Public Property Logger As Logger.Logger
                Get
                    Return _Logger
                End Get
                Set(value As Logger.Logger)
                    _Logger = value
                End Set
            End Property

            Private Sub MsuTrack_TrackAltSwitched(ByVal sender As Object, ByVal e As MsuAltSelect.Msu.Tracks.MsuTrack.TrackAltSwitchedEventArgs) Handles Me.TrackAltSwitched

                Dim log As String = $"Switched Track { e.MsuTrack.TrackNumber } "

                If String.IsNullOrEmpty(e.MsuTrack.Title) Then
                Else
                    log = String.Concat(log, $"(""{ e.MsuTrack.Title }"") ")
                End If

                log = String.Concat(log, $"from { e.MsuTrackAltOld.AltNumber } ")

                If String.IsNullOrEmpty(e.MsuTrackAltOld.Title) Then
                Else
                    log = String.Concat(log, $"(""{ e.MsuTrackAltOld.Title }"") ")
                End If

                log = String.Concat(log, $"to { e.MsuTrackAltNew.AltNumber } ")

                If String.IsNullOrEmpty(e.MsuTrackAltNew.Title) Then
                Else
                    log = String.Concat(log, $"(""{ e.MsuTrackAltNew.Title }"") ")
                End If

                log = String.Concat(log, "successfully.")

                Call AddToLog(log)
            End Sub

            Friend Sub RaiseEventTrackAltSwitched(ByVal sender As Object, ByVal e As MsuTrack.TrackAltSwitchedEventArgs)
                RaiseEvent TrackAltSwitched(sender, e)
            End Sub

            Friend Sub RaiseEventConvertedFilesSwitched(ByVal sender As Object, ByVal e As EventArgs)
                RaiseEvent ConvertedFilesSwitched(sender, e)
            End Sub


            <Newtonsoft.Json.JsonIgnore>
            Private _MsuLocation As String
            ''' <summary>
            ''' Folderpath containing the .msu file selected by the user<br/>
            ''' including the ROM and all .PCM files<br/>
            ''' </summary>
            <Newtonsoft.Json.JsonProperty("msu_location")>
            Public Property MsuLocation As String
                Get
                    Return _MsuLocation
                End Get
                Set(value As String)
                    _MsuLocation = value
                    Call Me.SetCurrentDirectoryToMsuLocation()
                End Set
            End Property

            <Newtonsoft.Json.JsonIgnore>
            Private Property ShouldSerializeMsuLocationValue As Boolean

            Public Function ShouldSerializeMsuLocation() As Boolean
                Return Me.ShouldSerializeMsuLocationValue
            End Function

            ''' <summary>
            ''' Basename of the selected .msu file
            ''' </summary>
            <Newtonsoft.Json.JsonProperty("msu_name")>
            Public Property MsuName As String

            Private _PcmPrefix As String

            ''' <summary>
            ''' Prefix used for the .pcm audio tracks<br/>
            ''' Normally the same as the BaseName of the .msu file + '-'<br/>
            ''' Some playback methods for MSU1 may use another prefix<br/>
            ''' </summary>
            <Newtonsoft.Json.JsonProperty("pcm_prefix")>
            Public Property PcmPrefix As String
                Get
                    Return _PcmPrefix
                End Get
                Set(pcmPrefixNew As String)
                    _PcmPrefix = pcmPrefixNew
                End Set
            End Property

            ''' <summary>
            ''' Attribute used in JSON configuration for creating PCM files using MSUPCM++<br/>
            ''' </summary>
#Disable Warning IDE0051 ' Remove unused private members
            <Newtonsoft.Json.JsonProperty("output_prefix")>
            Private WriteOnly Property OutputPrefixMSUPCM As String
#Enable Warning IDE0051 ' Remove unused private members
                Set(outputPref As String)
                    If String.IsNullOrWhiteSpace(_PcmPrefix) Then
                        _PcmPrefix = outputPref
                    End If
                    If String.IsNullOrWhiteSpace(Me.MsuName) Then
                        Me.MsuName = outputPref
                    End If
                End Set
            End Property

            ''' <summary>
            ''' Array containing all Tracks
            ''' This Array is stored inside the Property <see cref="MsuTracks.TrackDict"/>
            ''' </summary>
            <Newtonsoft.Json.JsonProperty("tracks")>
            Public Property TrackArray As MsuTrack()
                Get
                    If Me.TrackDict IsNot Nothing Then
                        Return Me.TrackDict.Values.ToArray()
                    End If
                    Return Nothing
                End Get
                Set(trackArray As MsuTrack())

                    If trackArray Is Nothing Then
                        Me.TrackDict = Nothing
                    Else
                        Dim trackDict As New SortedDictionary(Of Byte, MsuTrack)

                        For Each msuTrack As MsuTrack In trackArray

                            If msuTrack.Parent IsNot Me Then
                                msuTrack.Parent = Me
                            End If

                            Call trackDict.Add(
                                    key:=msuTrack.TrackNumber,
                                  value:=msuTrack)
                        Next

                        Me.TrackDict = trackDict
                    End If
                End Set
            End Property

            ''' <summary>
            ''' Dictionary containing all Tracks
            ''' </summary>
            <Newtonsoft.Json.JsonIgnore>
            Public Property TrackDict() As SortedDictionary(Of Byte, MsuTrack)

            ''' <summary>
            ''' Checks if any child <see cref="MsuTrackAlt"/>s have at least one element in the <see cref="MsuTrackAlt.AutoSwitchTrackNumbers"/> Array
            ''' </summary>
            <Newtonsoft.Json.JsonIgnore>
            Public ReadOnly Property HasAltTracksWithAutoSwitch As Boolean
                Get
                    ' Loop and check through all MsuTracks
                    For Each objDictItem As KeyValuePair(Of Byte, MsuTrack) In Me.TrackDict
                        If objDictItem.Value.HasAltTracksWithAutoSwitch Then
                            Return True
                        End If
                    Next
                    Return False
                End Get
            End Property

            <Newtonsoft.Json.JsonIgnore>
            Public Property MsuFilePath As String
                Get
                    If Me.MsuLocation IsNot Nothing _
               AndAlso Me.MsuName IsNot Nothing _
               AndAlso Me.MsuLocation.Length <> 0 _
               AndAlso Me.MsuName.Length <> 0 Then

                        ' Assemble the filepath
                        Return System.IO.Path.Join(Me.MsuLocation, String.Concat(Me.MsuName, MsuHelper.DotChar, "msu"))
                    Else
                        Return Nothing
                    End If
                End Get
                Set(value As String)

                    ' Split the filepath into multiple Strings
                    Me.MsuLocation = System.IO.Path.GetDirectoryName(value)

                    Dim fileExt = System.IO.Path.GetExtension(value)

                    Dim dataSet = False

                    If fileExt.Equals(MsuHelper.PcmExtL, StringComparison.OrdinalIgnoreCase) Then
                        ' Selected file is PCM file

                        Dim pcmPrefix = MsuTracks.GetPcmPrefix(value)

                        If pcmPrefix IsNot Nothing Then

                            Me.PcmPrefix = pcmPrefix
                            Me.MsuName = Me.PcmPrefix

                            dataSet = True
                        End If
                    End If

                    Call SetCurrentDirectoryToMsuLocation()

                    If Not dataSet Then
                        Me.MsuName = System.IO.Path.GetFileNameWithoutExtension(value)

                        Call Me.FindPcmPrefix()
                    End If
                End Set
            End Property

            Public Sub New()
                Call Me.Initialize(vbNullString, Nothing, Nothing)
            End Sub

            Public Sub New(ByRef msuFilePath As String, ByRef settings As Msu.Settings.Settings)
                Call Me.Initialize(msuFilePath, Nothing, settings)
            End Sub

            Public Sub New(ByRef msuFilePath As String, ByRef logger As Logger.Logger, ByRef settings As Msu.Settings.Settings)
                Call Me.Initialize(msuFilePath, logger, settings)
            End Sub

            Public Sub New(ByRef logger As Logger.Logger, ByRef settings As Msu.Settings.Settings)
                Call Me.Initialize(vbNullString, logger, settings)
            End Sub

            Private Sub Initialize(ByRef msuFilePath As String, ByRef logger As Logger.Logger, ByRef settings As Msu.Settings.Settings)
                Me.TrackDict() = New SortedDictionary(Of Byte, MsuTrack)
                Me.Logger = logger
                If String.IsNullOrWhiteSpace(msuFilePath) Then Else Me.MsuFilePath = msuFilePath
                Me.Settings = settings
            End Sub

            ' Set the working directory to the directory of the .msu file
            Public Sub SetCurrentDirectoryToMsuLocation()
                If String.IsNullOrWhiteSpace(Me.MsuLocation) Then Return
                Dim curLoc As String = System.IO.Directory.GetCurrentDirectory()

                If curLoc.Equals(Me.MsuLocation, StringComparison.Ordinal) Then
                    Return ' MsuLocation is already the current Directory
                End If

                Call AddToLog($"Setting the location of the MSU as the current directory. (""{Me.MsuLocation}"")")

                Try
                    Call System.IO.Directory.SetCurrentDirectory(Me.MsuLocation)
                    Call Me.CalculateAbsoluteLocationForAltTracks()
                Catch ex As System.IO.DirectoryNotFoundException
                    Call AddToLog($"Could not set current directory. Saved MsuLocation does not exist. (""{Me.MsuLocation}"")", Drawing.Color.DarkMagenta)
                End Try

            End Sub

            Friend Sub CalculateAbsoluteLocationForAltTracks()
                For Each keyValuePair As KeyValuePair(Of Byte, MsuTrack) In Me.TrackDict
                    Call keyValuePair.Value.CalculateAbsoluteLocationForAltTracks()
                Next
            End Sub

            Public Sub ValidateMsuTrackPaths()
                For Each keyValuePair As KeyValuePair(Of Byte, MsuTrack) In Me.TrackDict
                    Call keyValuePair.Value.ValidateMsuTrackPaths()
                Next
            End Sub

            Public Sub FindPcmPrefix()
                Dim bSearchForPcmPrefix As Boolean = False

                Me.PcmPrefix = Constants.vbNullString

                If String.IsNullOrEmpty(Me.MsuName) Then
                    ' No MsuName was found
                    Call AddToLog($"No MSU name found. (*.msu file missing?)")
                    bSearchForPcmPrefix = True
                Else
                    Call AddToLog($"Checking if the PCM files are using the prefix ""{Me.MsuName}"" from the MSU.")

                    ' Look for PCM-Files with the ROM-Name as the prefix
                    Dim files() As String = System.IO.Directory.GetFiles(
                             path:=Me.MsuLocation,
                    searchPattern:=Me.MsuName & "-*" & MsuHelper.PcmExtL,
                     searchOption:=System.IO.SearchOption.TopDirectoryOnly)

                    If files.Length = 0 Then
                        Call AddToLog($"No PCM file using the prefix ""{Me.MsuName}"" found inside ""{Me.MsuLocation}"".")
                        ' No PCM-Files with the ROM-Name as the prefix found
                        ' Uses another name (higan uses track-#.pcm)
                        bSearchForPcmPrefix = True
                    End If
                End If

                If bSearchForPcmPrefix Then
                    Me.PcmPrefix = ReadFirstPcmPrefix()
                Else
                    Call AddToLog($"The prefix from the MSU and from the PCM files are the same.")
                End If

                If String.IsNullOrEmpty(Me.PcmPrefix) Then
                    ' Normally the pcm files have same prefix as the ROM-/MSU-Name
                    Me.PcmPrefix = Me.MsuName
                ElseIf String.IsNullOrEmpty(Me.MsuName) Then
                    ' Apply the found PcmPrefix to the MsuName as a fallback
                    Me.MsuName = Me.PcmPrefix
                End If
            End Sub

            Private Function ReadFirstPcmPrefix() As String
                Call AddToLog("Searching for the first PCM File to get the prefix.")

                Dim files() As String = System.IO.Directory.GetFiles(
                         path:=Me.MsuLocation,
                searchPattern:=System.String.Concat("*"c, MsuHelper.PcmExtL),
                 searchOption:=System.IO.SearchOption.TopDirectoryOnly)

                If files.Length = MsuHelper.ZeroByte Then
                    Call AddToLog($"No PCM File found inside ""{Me.MsuLocation}"".")
                    Return vbNullString
                End If

                ' Look for the .pcm file in the MSU Directory with a valid name
                For i As UInt16 = CUShort(LBound(files)) To CUShort(UBound(files))

                    Dim prefix = GetPcmPrefix(files(i))

                    If prefix Is Nothing Then
                        Continue For ' PCM-File has no Hyphen -> Invalid
                    End If

                    Dim fileName As String = System.IO.Path.GetFileName(files(i))

                    Call AddToLog($"Found prefix ""{prefix}"" from file ""{fileName}"".")

                    ' Return the prefix of the first valid PCM-File
                    Return prefix
                Next

                Call AddToLog($"No PCM File with a valid filename found inside ""{Me.MsuLocation}"".")
                Return vbNullString
            End Function

            Public Shared Function GetPcmPrefix(ByRef pcmFile As String) As String
                Dim fileName As String = System.IO.Path.GetFileName(pcmFile)

                Dim hyphenIndx As Integer = fileName.LastIndexOf(Msu.MsuHelper.HyphenChar)

                Select Case hyphenIndx
                    Case MsuHelper.ZeroByte
                        Return Constants.vbNullString ' PCM-File has no Hyphen -> Invalid

                    Case Else

                        Dim prefix As String = fileName.Substring(0, hyphenIndx)

                        ' Return the prefix of the first valid PCM-File
                        Return prefix
                End Select
            End Function

            Private Function ReadFirstMsuName() As String
                Dim files() As String = System.IO.Directory.GetFiles(Me.MsuLocation, "*.msu", System.IO.SearchOption.TopDirectoryOnly)

                If files.Length = MsuHelper.ZeroByte Then
                    Return vbNullString
                End If

                Return System.IO.Path.GetFileNameWithoutExtension(files.First)
            End Function

            ''' <summary>
            ''' Renames all files with <see cref="MsuTracks.MsuName"/> as their BaseName (Normally MSU file and the ROM (.smc / .sfc) file) and sets the <see cref="MsuTracks.MsuName"/> property to <paramref name="MsuNameNew"/> if successful.
            ''' </summary>
            ''' <param name="MsuNameNew">New Name for the MSU file and the ROM file</param>
            ''' <exception cref="System.IO.IOException"/>
            ''' <exception cref="System.IO.FileNotFoundException"/>
            ''' <exception cref="System.ArgumentException"/>
            ''' <exception cref="System.ArgumentNullException"/>
            ''' <exception cref="System.UnauthorizedAccessException"/>
            ''' <exception cref="System.IO.PathTooLongException"/>
            ''' <exception cref="System.IO.DirectoryNotFoundException"/>
            ''' <exception cref="System.NotSupportedException"/>
            Public Sub RenameMsu(ByRef msuNameNew As String)
                If Me.MsuName.Equals(msuNameNew, StringComparison.OrdinalIgnoreCase) Then Return
                Call RenameMsu(msuNameNew, Me.GetExistingMsuFiles())
            End Sub

            ''' <summary>
            ''' Renames the MSU file and the ROM (.smc / .sfc) file and sets the <see cref="MsuTracks.MsuName"/> property tp <paramref name="MsuNameNew"/> if successful.
            ''' </summary>
            ''' <param name="MsuNameNew">New Name for the MSU file and the ROM file</param>
            ''' <exception cref="System.IO.IOException"/>
            ''' <exception cref="System.IO.FileNotFoundException"/>
            ''' <exception cref="System.ArgumentException"/>
            ''' <exception cref="System.ArgumentNullException"/>
            ''' <exception cref="System.UnauthorizedAccessException"/>
            ''' <exception cref="System.IO.PathTooLongException"/>
            ''' <exception cref="System.IO.DirectoryNotFoundException"/>
            ''' <exception cref="System.NotSupportedException"/>
            Friend Sub RenameMsu(ByRef msuNameNew As String, filesToRename As String())
                If Me.MsuName.Equals(msuNameNew, StringComparison.OrdinalIgnoreCase) Then Return

                For Each fileNameOld As String In filesToRename
                    Dim fileExt = System.IO.Path.GetExtension(fileNameOld).Replace("."c, Constants.vbNullString, StringComparison.Ordinal)
                    Dim parentDir = System.IO.Path.GetDirectoryName(fileNameOld)

                    Dim fileNameNew = JoinFilePath(parentDir, msuNameNew, fileExt)

                    Try
                        Call System.IO.File.Move(sourceFileName:=fileNameOld, destFileName:=fileNameNew)

                    Catch ex As System.Exception

                        If fileExt.Equals("msu", StringComparison.OrdinalIgnoreCase) _
                OrElse fileExt.Equals("smc", StringComparison.OrdinalIgnoreCase) _
                OrElse fileExt.Equals("sfc", StringComparison.OrdinalIgnoreCase) Then

                            Call AddToLog(ex.ToString, Drawing.Color.Red)
                            Throw
                        Else
                            Call AddToLog(ex.ToString, Drawing.Color.DarkMagenta)
                        End If
                    End Try
                Next

                Me.MsuName = msuNameNew
            End Sub

            ''' <summary>
            ''' Rename all existing PcmFiles and set property <see cref="MsuTracks.PcmPrefix"/> to <paramref name="PcmPrefixNew"/> if successful.
            ''' </summary>
            ''' <exception cref="System.ArgumentNullException" />
            ''' <exception cref="System.ArgumentException" />
            ''' <exception cref="System.NotSupportedException"/>
            ''' <exception cref="System.IO.FileNotFoundException"/>
            ''' <exception cref="System.IO.IOException"/>
            ''' <exception cref="System.Security.SecurityException"/>
            ''' <exception cref="System.IO.DirectoryNotFoundException"/>
            ''' <exception cref="System.UnauthorizedAccessException"/>
            ''' <exception cref="System.IO.PathTooLongException"/>
            ''' <exception cref="System.ArgumentOutOfRangeException"/>
            Public Sub RenamePcmFiles(ByRef pcmPrefixNew As String)
                If Me.PcmPrefix.Equals(pcmPrefixNew, StringComparison.OrdinalIgnoreCase) Then Return
                Call RenamePcmFiles(pcmPrefixNew, GetPcmFileRenameDict(pcmPrefixNew))
            End Sub

            ''' <summary>
            ''' Rename all existing PcmFiles and set property <see cref="MsuTracks.PcmPrefix"/> to <paramref name="PcmPrefixNew"/> if successful.
            ''' </summary>
            ''' <exception cref="System.ArgumentNullException" />
            ''' <exception cref="System.ArgumentException" />
            ''' <exception cref="System.NotSupportedException"/>
            ''' <exception cref="System.IO.FileNotFoundException"/>
            ''' <exception cref="System.IO.IOException"/>
            ''' <exception cref="System.Security.SecurityException"/>
            ''' <exception cref="System.IO.DirectoryNotFoundException"/>
            ''' <exception cref="System.UnauthorizedAccessException"/>
            ''' <exception cref="System.IO.PathTooLongException"/>
            ''' <exception cref="System.ArgumentOutOfRangeException"/>
            Private Sub RenamePcmFiles(ByRef pcmPrefixNew As String, ByRef pcmFileRenameDict As Dictionary(Of MsuPcmFile, PcmFileRenameParams))
                If Me.PcmPrefix.Equals(pcmPrefixNew, StringComparison.OrdinalIgnoreCase) Then Return

                If pcmFileRenameDict IsNot Nothing Then

                    ' Rename all existing PcmFiles to the new Filename
                    For Each keyValuePair As KeyValuePair(Of MsuPcmFile, PcmFileRenameParams) In pcmFileRenameDict

                        If keyValuePair.Value.FilePathWithNormalVersionSuffixExists Then
                            Call System.IO.File.Move(
                            sourceFileName:=keyValuePair.Value.FilePathWithNormalVersionSuffix,
                              destFileName:=keyValuePair.Value.FilePathWithNormalVersionSuffixNew,
                                 overwrite:=False)
                        End If

                        Call System.IO.File.Move(
                            sourceFileName:=keyValuePair.Key.FilePath,
                              destFileName:=keyValuePair.Value.FilePathNew,
                                 overwrite:=False)
                    Next
                End If

                Me.PcmPrefix = pcmPrefixNew
            End Sub

            ''' <exception cref="System.ArgumentNullException" />
            ''' <exception cref="System.ArgumentException" />
            ''' <exception cref="System.NotSupportedException"/>
            ''' <exception cref="System.IO.FileNotFoundException"/>
            ''' <exception cref="System.IO.IOException"/>
            ''' <exception cref="System.Security.SecurityException"/>
            ''' <exception cref="System.IO.DirectoryNotFoundException"/>
            ''' <exception cref="System.UnauthorizedAccessException"/>
            ''' <exception cref="System.IO.PathTooLongException"/>
            ''' <exception cref="System.ArgumentOutOfRangeException"/>
            Private Function GetPcmFileRenameDict(ByRef pcmPrefixNew As String) As Dictionary(Of MsuPcmFile, PcmFileRenameParams)
                Dim pcmFiles = Me.GetExistingPcmTracks
                If pcmFiles.Length = 0 Then Return Nothing
                Dim pcmFileRenameDict As New Dictionary(Of MsuPcmFile, PcmFileRenameParams)

                ' Open and close all PCM files, that will be renamed
                ' Will throw an exception, if any file is locked by another process
                ' Also throw an exception, if one of the new filenames already exist
                ' Prevents partial renaming of the PCM file
                For Each pcmFile As MsuPcmFile In pcmFiles

                    Dim pcmFileRenameParams As New PcmFileRenameParams With {
                    .FilePathWithNormalVersionSuffix = pcmFile.FilePathWithNormalVersionSuffix
                }

                    If pcmFileRenameParams.FilePathWithNormalVersionSuffixExists Then
                        Call Msu.MsuHelper.OpenAndCloseFile(pcmFileRenameParams.FilePathWithNormalVersionSuffix)

                        pcmFileRenameParams.FilePathWithNormalVersionSuffixNew = pcmFile.GetFilePath(pcmPrefixNew, MsuPcmFile.NormalVersionSuffix)

                        If System.IO.File.Exists(pcmFileRenameParams.FilePathWithNormalVersionSuffixNew) Then
                            Throw New System.IO.IOException($"The new filename {System.IO.Path.GetFileName(pcmFileRenameParams.FilePathWithNormalVersionSuffixNew)} for file {System.IO.Path.GetRelativePath(relativeTo:=Me.MsuLocation, path:=pcmFileRenameParams.FilePathWithNormalVersionSuffix)}")
                        End If
                    End If

                    Call Msu.MsuHelper.OpenAndCloseFile(pcmFile.FilePath)

                    PcmFileRenameParams.FilePathNew = pcmFile.GetFilePath(pcmPrefixNew)

                    If System.IO.File.Exists(PcmFileRenameParams.FilePathNew) Then
                        Throw New System.IO.IOException($"The new filename {System.IO.Path.GetFileName(PcmFileRenameParams.FilePathNew)} for file {System.IO.Path.GetRelativePath(relativeTo:=Me.MsuLocation, path:=pcmFile.FilePath)}")
                    End If

                    Call pcmFileRenameDict.Add(pcmFile, PcmFileRenameParams)
                Next

                Return PcmFileRenameDict
            End Function

            Private Structure PcmFileRenameParams
                Private _FilePathWithNormalVersionSuffix As String
                Public Property FilePathWithNormalVersionSuffix As String
                    Get
                        Return _FilePathWithNormalVersionSuffix
                    End Get
                    Set(value As String)
                        _FilePathWithNormalVersionSuffix = value
                        Me.FilePathWithNormalVersionSuffixExists = System.IO.File.Exists(_FilePathWithNormalVersionSuffix)
                    End Set
                End Property
                Private _FilePathWithNormalVersionSuffixExists As Boolean
                Public Property FilePathWithNormalVersionSuffixExists As Boolean
                    Get
                        Return _FilePathWithNormalVersionSuffixExists
                    End Get
                    Private Set(value As Boolean)
                        _FilePathWithNormalVersionSuffixExists = value
                    End Set
                End Property
                Public Property FilePathWithNormalVersionSuffixNew As String
                Public Property FilePathNew As String
            End Structure

            'Private Function JoinFilePathMsuLocation(ByRef BaseName As String, ByRef FileExt As String) As String
            '    Return MsuTracks.JoinFilePath(Me.MsuLocation, BaseName, FileExt)
            'End Function

            Private Shared Function JoinFilePath(ByRef location As String, ByRef baseName As String, ByRef fileExt As String) As String

                If String.IsNullOrWhiteSpace(fileExt) Then
                    Return System.IO.Path.Join(location, baseName)
                Else
                    Return System.IO.Path.Join(location, String.Concat(baseName, ".", fileExt))
                End If

            End Function

            Public Sub ScanMsuDirectoryForTracks()

                Call Me.AddToLog($"Scanning For (New) PCM Files inside ""{Me.MsuLocation}""")

                ' Read all PCM-Files in the main directory
                Call Me.ScanMainMsuDirectoryForTracks()

                Dim subFolders() As String = System.IO.Directory.GetDirectories(Me.MsuLocation)

                ' Read all PCM-Files in the sub directories, but not the main directory itself
                If subFolders.Length <> 0 Then
                    For i As Byte = CByte(LBound(subFolders)) To CByte(UBound(subFolders))

                        Call Me.ScanDirectoryForAltTracks(subFolders(i), False, System.IO.SearchOption.AllDirectories)
                    Next
                End If

                Call Me.SetParentObjectsOfChildren()
            End Sub

            Public Sub ScanMainMsuDirectoryForTracks()
                Call ScanDirectoryForAltTracks(DirPath:=Me.MsuLocation, IsMainDir:=True, searchOption:=System.IO.SearchOption.TopDirectoryOnly)
            End Sub

            Public Sub ScanDirectoryForAltTracks(ByRef dirPath As String, ByRef searchOption As System.IO.SearchOption)
                Call ScanDirectoryForAltTracks(DirPath:=dirPath, IsMainDir:=Me.MsuLocation.Equals(dirPath, StringComparison.OrdinalIgnoreCase), searchOption:=searchOption)
            End Sub

            Public Sub ScanDirectoryForAltTracks(ByRef dirPath As String, ByRef isMainDir As Boolean, ByRef searchOption As System.IO.SearchOption)

                ' Search for all PCM-Files in the specified folder subfolders
                Dim PcmFiles() As String =
            System.IO.Directory.GetFiles(
                path:=dirPath,
                searchPattern:=System.String.Concat(Me.PcmPrefix, "-*", MsuHelper.PcmExtL),
                searchOption:=searchOption)

                ' Check all Files and add them to the list, if they are missing
                For i As Integer = LBound(PcmFiles) To UBound(PcmFiles)
                    Call AddPcmTrackIfMissing(PcmFiles(i), isMainDir)
                Next
            End Sub

            Public Sub AddPcmTrackIfMissing(ByRef pcmFilePath As String, ByRef isMainDir As Boolean)
                Dim trackNumberN As Nullable(Of Byte) = GetTrackNumberForPcm(pcmFilePath)

                ' Ignore Tracks without a valid Id
                If trackNumberN Is Nothing Then
                    Return
                End If

                Dim trackNumber As Byte = CByte(trackNumberN)

                If isMainDir Then
                    Call AddPcmTrackIfMissing(GetAltLocationForMainTrackVersion(), TrackNumber, isMainDir)
                Else
                    Call AddPcmTrackIfMissing(System.IO.Path.GetDirectoryName(pcmFilePath), trackNumber, isMainDir)
                End If
            End Sub

            Public Sub AddPcmTrackIfMissing(ByRef locationAbsolute As String, ByRef trackNumber As Byte, ByRef isMainDir As Boolean)
                Dim value As MsuTrack = Nothing

                ' If the Track Object for this Id exists
                If Me.TrackDict.TryGetValue(trackNumber, value) Then

                    If isMainDir Then
                        Try
                            Call value.GetCurrentTrackAlt()
                            ' Main Version already added
                            Return
                        Catch ex As MsuAltTracksAllInSwapDirException
                            ' Main Version not added
                        End Try
                    End If

                    ' Cancel, if location already exists as an alt. track
                    If value.LocationExistsAsAltTrack(locationAbsolute) Then
                        Return
                    End If
                End If

                If isMainDir Then
                    Call AddPcmTrack(locationAbsolute, trackNumber, Me.Settings.TrackAltSettings.MsuTrackMainVersionTitle)
                Else
                    Call AddPcmTrack(locationAbsolute, trackNumber)
                End If
            End Sub

            Public Sub AddPcmTrack(ByRef locationAbsolute As String, ByRef trackNumber As Byte)
                Dim msuTrack As MsuTrack = GetOrCreateMsuTrack(trackNumber)
                Call msuTrack.AddPcmTrack(locationAbsolute)
            End Sub

            Public Sub AddPcmTrack(ByRef locationAbsolute As String, ByRef trackNumber As Byte, ByRef title As String)
                Dim msuTrack As MsuTrack = GetOrCreateMsuTrack(trackNumber)
                Call msuTrack.AddPcmTrack(locationAbsolute, title)
            End Sub

            ''' <summary>
            ''' Returns the entry in <see cref="MsuTracks.TrackDict"/> with the Key.
            ''' Creates a new entry if the key does not exist.
            ''' </summary>
            ''' <param name="TrackNumber">Key of <see cref="MsuTracks.TrackDict"/></param>
            Private Function GetOrCreateMsuTrack(ByRef trackNumber As Byte) As MsuTrack
                Dim value As MsuTrack = Nothing

                If Me.TrackDict.TryGetValue(trackNumber, value) Then
                    Return value
                Else
                    Dim msuTrack = New MsuTrack(Me, trackNumber)
                    Call Me.TrackDict.Add(trackNumber, msuTrack)
                    Return msuTrack
                End If
            End Function

            Public Shared Function GetTrackNumberForPcm(ByRef pcmFile As String) As Nullable(Of Byte)
                Dim hyphenPos = GetLastHyphenPos(pcmFile)
                Dim dotPos = GetLastDotPos(pcmFile)

                Dim trackNumber As String = Mid(pcmFile, hyphenPos + 1, dotPos - 1 - hyphenPos)

                Try
                    Return CByte(trackNumber)
                Catch
                    Return Nothing
                End Try
            End Function

            Private Sub SetParentObjectsOfChildren()
                For Each keyValuePair As KeyValuePair(Of Byte, MsuTrack) In Me.TrackDict
                    Dim msuTrack As MsuTrack = keyValuePair.Value
                    msuTrack.Parent = Me
                    Call msuTrack.SetParentObjectsOfChildren()
                Next
            End Sub

            Public Function GetAltLocationForMainTrackVersion() As String
                Dim altFolderPath As String = System.IO.Path.GetFullPath(path:=Me.Settings.TrackAltSettings.MsuTrackMainVersionLocation, basePath:=Me.MsuLocation)

                If CheckIfFolderPathAvailableForPcm(altFolderPath) Then
                    Return altFolderPath
                Else
                    Return GetAltLocationForMainTrackVersionFallback()
                End If
            End Function

            Protected Function GetAltLocationForMainTrackVersionFallback() As String
                Dim altFolderPath As String

                For i = Byte.MinValue To Byte.MaxValue

                    altFolderPath = System.IO.Path.GetFullPath(path:=Me.Settings.TrackAltSettings.MsuTrackMainVersionLocation & "_" & i, basePath:=Me.MsuLocation)

                    If CheckAndCreateIfFolderPathAvailableForPcm(altFolderPath) Then

                        Return altFolderPath
                    End If
                Next
                Return vbNullString
            End Function

            ''' <summary>
            ''' Checks for PCM files in the <paramref name="FilesCheck"/> <see cref="Array"/>, that fit the naming scheme of the current <see cref="MsuTracks"/>.
            ''' </summary>
            ''' <param name="FilesCheck"></param>
            ''' <returns>All valid PCM files</returns>
            Public Function CheckForValidPcmFiles(ByRef filesCheck As String()) As String()
                Return Me.CheckForValidPcmFiles(filesCheck, Nothing)
            End Function


            ''' <summary>
            ''' Checks for PCM files in the <paramref name="FilesCheck"/> <see cref="Array"/>, that fit the naming scheme of the current <see cref="MsuTracks"/>.
            ''' </summary>
            ''' <param name="filesCheck"><see cref="Array"/> of filepaths to check.</param>
            ''' <param name="trackNumberCheck">Only allow this track number. (Null for any track number)</param>
            ''' <returns>All valid PCM files</returns>
            Public Function CheckForValidPcmFiles(ByRef filesCheck As String(), trackNumberCheck As Nullable(Of Byte)) As String()
                Dim validFiles As New Specialized.StringDictionary

                For Each fileCheck In filesCheck
                    If System.IO.Path.GetExtension(fileCheck).Equals(MsuHelper.PcmExtL, StringComparison.OrdinalIgnoreCase) Then
                    Else
                        Continue For ' Invalid file extension
                    End If

                    Dim trackNumberN As Nullable(Of Byte) = GetTrackNumberForPcm(fileCheck)

                    If trackNumberN Is Nothing Then
                        Continue For ' No TrackId in the filename
                    ElseIf trackNumberCheck IsNot Nothing Then
                        If trackNumberCheck <> trackNumberN Then
                            Continue For ' Current TrackNumber different to the one to check
                        End If
                    End If

                    Dim fileNameCheck = System.IO.Path.GetFileName(fileCheck)

                    ' Assemble the Filename for the current config
                    Dim fileNameAssemled =
                    MsuPcmFile.GetFileName(
                        pcmPrefix:=Me.PcmPrefix,
                        trackNumber:=trackNumberN.Value)

                    If fileNameCheck.Equals(fileNameAssemled, StringComparison.OrdinalIgnoreCase) Then
                    Else
                        Continue For ' Different file name
                    End If

                    If validFiles.ContainsKey(fileCheck) Then
                        ' Already in output Dictionary. Ignore
                    Else
                        Call validFiles.Add(fileCheck, fileCheck)
                    End If
                Next

                Return validFiles.Values.Cast(Of String)().ToArray
            End Function

            Protected Function CheckAndCreateIfFolderPathAvailableForPcm(ByRef folderPath As String) As Boolean

                If CheckIfFolderPathAvailableForPcm(folderPath) Then
                Else
                    Return False
                End If

                ' Check if this directory already exists
                If System.IO.Directory.Exists(folderPath) Then

                    ' Search for all PCM-Files in this folder
                    Dim pcmFiles() As String =
                System.IO.Directory.GetFiles(
                    path:=folderPath,
                    searchPattern:=Me.PcmPrefix & "-*" & MsuHelper.PcmExtL,
                    searchOption:=System.IO.SearchOption.TopDirectoryOnly)

                    ' Folder can be used to store the PCM-Files, if there are none in there currently
                    Return pcmFiles.Length = 0
                Else
                    Try ' Create this directory
                        Call System.IO.Directory.CreateDirectory(folderPath)
                        Return True
                    Catch
                        Return False
                    End Try
                End If

            End Function

            Protected Shared Function CheckIfFolderPathAvailableForPcm(ByRef folderPath As String) As Boolean

                ' Cancel if a file exists with this path (Very unlikely, but possible)
                If System.IO.File.Exists(folderPath) Then
                    Return False
                Else
                    Return True
                End If

            End Function

            Public Function TrackFileWithNormalVersionSuffixExists() As Boolean

                For Each msuTrack As MsuTrack In Me.TrackDict.Values

                    If msuTrack.TrackFileWithNormalVersionSuffixExists Then
                        Return True
                    End If
                Next

                Return False
            End Function

            Public Delegate Sub SwitchConvertedFilesToNormalVersionCallback()

            Public Sub SwitchConvertedFilesToNormalVersion()
                Call AddToLog($"Switching the converted PCM files back to the normal PCM files.")
                Dim attempts = UInt16.MinValue

                Dim msuPcmFileList As New List(Of MsuPcmFile)
                Dim msuPcmFileListRemove As New Queue(Of MsuPcmFile)

                Call msuPcmFileList.AddRange(Me.GetExistingPcmTracks)

                ' Loop until all files are switched back
                Do Until msuPcmFileList.Count = 0

                    ' Reset Attempt counter if MaxValue is reached (Overflow)
                    ' (Would only happen after around 18h)
                    If attempts = UInt16.MaxValue Then
                        attempts = UInt16.MinValue
                    End If
                    attempts += MsuHelper.OneByte

                    Call AddToLog($"Attempt {attempts}", Drawing.Color.DarkGray)

                    For Each msuPcmFile As MsuPcmFile In msuPcmFileList

                        If msuPcmFile.FilePathWithNormalVersionSuffixExists Then
                        Else
                            ' File is already the normal version
                            Call msuPcmFileListRemove.Enqueue(msuPcmFile)
                            Continue For
                        End If

                        Try

                            If msuPcmFile.IsOpen() Then
                                Call AddToLog($"File {System.IO.Path.GetRelativePath(relativeTo:=Me.MsuLocation, path:=msuPcmFile.FilePath)} is currently open.", Drawing.Color.DarkMagenta)
                                Continue For
                            End If

                            ' Switch out the converted file with the normal file.
                            ' Converted file will be deleted
                            Call System.IO.File.Replace(
                                sourceFileName:=msuPcmFile.FilePathWithNormalVersionSuffix,
                           destinationFileName:=msuPcmFile.FilePath,
                     destinationBackupFileName:=Constants.vbNullString)


                            Call AddToLog($"Switched ""{System.IO.Path.GetRelativePath(relativeTo:=Me.MsuLocation, path:=msuPcmFile.FilePath)}"" to normal file.")

                            Call msuPcmFileListRemove.Enqueue(msuPcmFile)

                        Catch ex As System.Exception
                            Call AddToLog(ex.ToString, Drawing.Color.DarkMagenta)
                            Call AddToLog($"Could not switch file {System.IO.Path.GetRelativePath(relativeTo:=Me.MsuLocation, path:=msuPcmFile.FilePath)} to normal file.{System.Environment.NewLine}Trying again later.", Drawing.Color.DarkMagenta)
                        End Try
                    Next

                    For Each msuPcmFile In msuPcmFileListRemove
                        Call msuPcmFileList.Remove(msuPcmFile)
                    Next
                    Call msuPcmFileListRemove.Clear()

                    If msuPcmFileList.Count <> 0 Then
                        Call Threading.Thread.Sleep(1000) ' Wait one second and try again
                    End If
                Loop

                Call AddToLog($"Finished switching the converted PCM files back to the normal PCM files.")
                RaiseEvent ConvertedFilesSwitched(Me, Nothing)
            End Sub

            ''' <summary>
            ''' Saves this instance as a .JSON configuration 
            ''' </summary>
            ''' <param name="strJsonFilePath">A absolute path for the .JSON file that the <see cref="Newtonsoft.Json.JsonSerializer" /> will serialize to.</param>
            ''' <exception cref="T:System.ArgumentNullException"/>
            ''' <exception cref="T:System.ArgumentException"/>
            ''' <exception cref="System.NotSupportedException"/>
            ''' <exception cref="T:System.IO.FileNotFoundException"/>
            ''' <exception cref="T:System.IO.IOException"/>
            ''' <exception cref="T:System.Security.SecurityException" />
            ''' <exception cref="T:System.IO.DirectoryNotFoundException" />
            ''' <exception cref="T:System.UnauthorizedAccessException" />
            ''' <exception cref="T:System.IO.PathTooLongException" />
            Public Sub SaveToJson(ByRef jsonFilePath As String)
                Dim jsonPathOld As String = String.Concat(jsonFilePath, "_old")
                Dim jsonPathTmp As String = String.Concat(jsonFilePath, "_tmp")
                Dim i As Byte = MsuHelper.ZeroByte

                While System.IO.File.Exists(jsonPathTmp) OrElse System.IO.Directory.Exists(jsonPathTmp)
                    jsonPathTmp = jsonFilePath & "_tmp" & i
                    i += MsuHelper.OneByte
                End While

                Select Case Me.Settings.TrackAltSettings.SaveMsuLocation
                    Case CheckState.Checked
                        Me.ShouldSerializeMsuLocationValue = True
                    Case CheckState.Unchecked
                        Me.ShouldSerializeMsuLocationValue = False
                    Case Else
#If WINDOWS Then
                        Dim locationAbsoluteRemLocUnc As String = Msu.MsuHelper.PathRemoveUncLocalPref(System.IO.Path.GetDirectoryName(jsonFilePath))
                        Dim msuTracksLocationRemLocUnc As String = Msu.MsuHelper.PathRemoveUncLocalPref(Me.MsuLocation)
#Else
                        Dim locationAbsoluteRemLocUnc As String = System.IO.Path.GetDirectoryName(jsonFilePath)
                        Dim msuTracksLocationRemLocUnc As String = Me.MsuLocation
#End If
                        If locationAbsoluteRemLocUnc.Equals(msuTracksLocationRemLocUnc, StringComparison.OrdinalIgnoreCase) Then
                            Me.ShouldSerializeMsuLocationValue = False ' Folder path of JSON is the same as the MSU
                        Else
                            Me.ShouldSerializeMsuLocationValue = True  ' JSON is saved in a different location than the MSU
                        End If
                End Select

                ' Create a new temporary file
                Dim stream As _
            New System.IO.FileStream(
                path:=jsonPathTmp,
                access:=IO.FileAccess.Write,
                share:=IO.FileShare.Read,
                mode:=IO.FileMode.CreateNew)

                Dim streamWriter As _
            New System.IO.StreamWriter(
                stream:=stream,
                encoding:=System.Text.Encoding.Default)

                ' Serialize this object as .JSON to the opened file
                Using jsonTextWriter As _
            New Newtonsoft.Json.JsonTextWriter(
               textWriter:=streamWriter)

                    jsonTextWriter.IndentChar = vbTab.Single
                    jsonTextWriter.Indentation = MsuHelper.OneByte
                    jsonTextWriter.Formatting = Newtonsoft.Json.Formatting.Indented

                    Dim jsonSerializer As New Newtonsoft.Json.JsonSerializer()

                    Call jsonSerializer.Serialize(jsonTextWriter, Me)

                    Call jsonTextWriter.Flush()
                End Using

                Call stream.Close()

                ' If a previous .JSON configuration exists
                If System.IO.File.Exists(jsonFilePath) Then

                    ' Replace existing .JSON with serialized temp .JSON
                    Call System.IO.File.Replace(jsonPathTmp, jsonFilePath, jsonPathOld)

                Else

                    ' Move serialized temp .JSON to destination
                    Call System.IO.File.Move(jsonPathTmp, jsonFilePath)

                End If
            End Sub

            ''' <summary>
            ''' Creates a new instance from a saved .JSON configuration 
            ''' </summary>
            ''' <param name="strJsonFilePath">A absolute path for the .JSON file that the <see cref="Newtonsoft.Json.JsonSerializer" /> will deserialize to an instance of <see cref="MsuTracks" />.</param>
            ''' <exception cref="System.ArgumentNullException"/>
            ''' <exception cref="System.ArgumentException"/>
            ''' <exception cref="System.NotSupportedException"/>
            ''' <exception cref="System.IO.FileNotFoundException"/>
            ''' <exception cref="System.IO.IOException"/>
            ''' <exception cref="System.Security.SecurityException" />
            ''' <exception cref="System.IO.DirectoryNotFoundException" />
            ''' <exception cref="System.UnauthorizedAccessException" />
            ''' <exception cref="System.IO.PathTooLongException" />
            ''' <exception cref="Newtonsoft.Json.JsonException" />
            Public Shared Function LoadFromJson(ByRef jsonFilePath As String, ByRef logger As Logger.Logger, ByRef settings As Msu.Settings.Settings) As MsuTracks
                Dim fileStream As System.IO.FileStream

                If logger IsNot Nothing Then Call logger.AddToLog($"Loading from JSON configuration: ""{jsonFilePath}""")
                Try
                    ' Open the JsonFile
                    Try

                        fileStream =
                New System.IO.FileStream(
                    path:=jsonFilePath,
                    access:=System.IO.FileAccess.Read,
                    share:=System.IO.FileShare.Read,
                    mode:=System.IO.FileMode.Open)

                    Catch ex As System.IO.IOException

                        fileStream =
                New System.IO.FileStream(
                    path:=jsonFilePath,
                    access:=System.IO.FileAccess.Read,
                    share:=System.IO.FileShare.ReadWrite Or IO.FileShare.Delete,
                    mode:=System.IO.FileMode.Open)

                    End Try

                    ' Create StreamReader for opened JsonFile
                    Dim streamReader As _
            New System.IO.StreamReader(
                stream:=fileStream,
                encoding:=System.Text.Encoding.Default)

                    ' Deserialize JSON into MsuTracks (load data)
                    Using jsonTextReader As _
            New Newtonsoft.Json.JsonTextReader(
               reader:=streamReader)

                        Dim jsonSerializer As New Newtonsoft.Json.JsonSerializer()

                        LoadFromJson = jsonSerializer.Deserialize(Of MsuTracks)(jsonTextReader)

                        LoadFromJson.Logger = logger
                        LoadFromJson.Settings = settings
                        LoadFromJson.SetParentObjectsOfChildren()
                        LoadFromJson.CheckMsuLocation(jsonFilePath)
                        LoadFromJson.CalculateAbsoluteLocationForAltTracks()
                        LoadFromJson.CheckMsuName()
                    End Using
                Catch ex As System.Exception
                    If logger IsNot Nothing Then Call logger.AddToLog(ex.ToString, Drawing.Color.Red)
                    Throw
                End Try

            End Function

            Protected Sub CheckMsuLocation(ByRef jsonFilePath As String)
                If Me.MsuLocation Is Nothing _
        OrElse Me.MsuLocation.Length = MsuHelper.ZeroByte _
        OrElse Not System.IO.Directory.Exists(Me.MsuLocation) Then
                    Me.MsuLocation = System.IO.Path.GetDirectoryName(jsonFilePath)
                End If
            End Sub

            'Protected Sub CheckPcmPrefix()
            '    If Me.PcmPrefix Is Nothing _
            '    OrElse Me.PcmPrefix.Length = bytZero Then
            '        Me.PcmPrefix = Me.ReadFirstPcmPrefix
            '    End If
            'End Sub

            Protected Sub CheckMsuName()

                If Me.MsuName Is Nothing _
    OrElse Me.MsuName.Length = MsuHelper.ZeroByte _
    OrElse Not System.IO.File.Exists(Me.MsuFilePath) Then
                    Dim strMsuName As String = Me.ReadFirstMsuName
                    If strMsuName IsNot Nothing Then
                        Me.MsuName = strMsuName
                    End If
                End If

                Call FindPcmPrefix()

            End Sub

            ''' <exception cref="MsuAutoSwitchDataInvalidException"/>
            ''' <exception cref="MsuAutoSwitchDataDuplicateException"/>
            Public Sub AutoSwitchValidate()

                Call AddToLog("Checking if the data for AutoSwitching is valid")

                Try
                    For Each keyValuePair As KeyValuePair(Of Byte, MsuTrack) In Me.TrackDict

                        Dim autoSwitchDict As SortedDictionary(Of Byte, MsuTrackAlt) = keyValuePair.Value.TrackAltAutoSwitchDict()
                    Next
                Catch ex As System.Exception
                    Call AddToLog(ex.ToString, Drawing.Color.Red)
                    Throw
                End Try

            End Sub

            Public Sub AutoSwitch()

                Call AddToLog("Start AutoSwitch", Drawing.Color.DarkGray)

                Try
                    For Each keyValuePair As KeyValuePair(Of Byte, MsuTrack) In Me.TrackDict

                        Dim msuTrack As MsuTrack = keyValuePair.Value

                        Dim autoSwitchDict As SortedDictionary(Of Byte, MsuTrackAlt) = msuTrack.TrackAltAutoSwitchDict()

                        If autoSwitchDict.Count = MsuHelper.ZeroByte Then
                            Continue For
                        End If

                        Dim allCurrentAlt As Boolean = True

                        ' Check if any alt. Tracks in the Dictionary are not the current Track
                        For Each dictAltItem In autoSwitchDict

                            Dim msuTrackAlt As MsuTrackAlt = dictAltItem.Value

                            If msuTrackAlt IsNot msuTrackAlt.Parent.GetCurrentTrackAlt Then
                                allCurrentAlt = False
                                Exit For
                            End If
                        Next

                        If allCurrentAlt Then
                            ' No need to switch.
                            ' All alt. Tracks in the List are already the current Track
                            Continue For
                        End If

                        ' Check if this Tack is currently beng played back
                        If msuTrack.IsOpen Then

                            ' Set all alt. Tracks that have AutoSwitch for this Track as the current Track
                            For Each dictAltItem In autoSwitchDict

                                Dim msuTrackAlt As MsuTrackAlt = dictAltItem.Value

                                Try
                                    Dim msuTrackAltCurrent As MsuTrackAlt = msuTrackAlt.Parent.GetCurrentTrackAlt

                                    If msuTrackAlt IsNot msuTrackAltCurrent Then
                                        Call msuTrackAlt.SetAsCurrentAltTrack(msuTrackAltCurrent)
                                    End If
                                Catch ex As System.Exception
                                    Call AddToLog(ex.ToString, Drawing.Color.Firebrick)
                                    ' Ignore if Track could not be switched
                                End Try
                            Next
                        End If
                    Next
                Catch ex As System.Exception
                    Call AddToLog(ex.ToString, Drawing.Color.Red)
                    Throw
                End Try
                Call AddToLog("End AutoSwitch", Drawing.Color.DarkGray)
            End Sub

            ''' <summary>
            ''' Gets files, that have the <see cref="MsuTracks.MsuName"/> as their BaseFileName. (Search only main <see cref="MsuTracks.MsuLocation"/>)
            ''' </summary>
            ''' <returns></returns>
            Public Function GetExistingMsuFiles() As String()
                Dim fileList As Specialized.StringCollection
                ' Get all files that start with the MsuName
                Dim files() As String =
                System.IO.Directory.GetFiles(
                           path:=Me.MsuLocation,
                  searchPattern:=System.String.Concat(Me.MsuName, "*"),
                   searchOption:=IO.SearchOption.TopDirectoryOnly)

                If files Is Nothing OrElse files.Length = 0 Then Return Nothing

                fileList = New Specialized.StringCollection
                Call fileList.AddRange(files)

                For i = 0 To fileList.Count - 1

                    If i > fileList.Count - 1 Then Exit For

                    If Me.MsuName.Equals(System.IO.Path.GetFileNameWithoutExtension(fileList.Item(i))) Then
                        ' BaseName of file is MsuName
                    Else
                        ' BaseName of file is not the MsuName (Filename only begins with MsuName)
                        Call fileList.RemoveAt(i)
                        i = i - 1
                    End If
                Next

                Return fileList.Cast(Of String)().ToArray
            End Function

            Public Function GetExistingPcmTracks() As MsuPcmFile()
                Dim msuPcmFileList As New List(Of MsuPcmFile)

                For Each keyValuePair As KeyValuePair(Of Byte, MsuTrack) In Me.TrackDict

                    Dim msuTrack As MsuTrack = keyValuePair.Value

                    ' The currently used Pcm Track should always exist
                    ' Ignore this TrackId completely if not
                    If msuTrack.FilePathExists Then
                    Else
                        Continue For
                    End If

                    ' Add the currently used PCM Track to the list
                    Call msuPcmFileList.Add(msuTrack)

                    ' Get all alt tracks for the current TrackId that exist in their swap directory
                    Dim msuTrackAltsExisting() As MsuTrackAlt = msuTrack.GetExistingAltTracks

                    If msuTrackAltsExisting.Length = 0 Then
                        Continue For
                    End If

                    ' Add all existing alt. Tracks to the List
                    Call msuPcmFileList.AddRange(msuTrackAltsExisting)
                Next

                Return msuPcmFileList.ToArray()
            End Function

            Friend Sub AddToLog(ByRef text As String)
                If Me.Logger IsNot Nothing Then Call Me.Logger.AddToLog(text)
            End Sub

            Friend Sub AddToLog(ByRef text As String, ByRef entryColor As System.Drawing.Color)
                If Me.Logger IsNot Nothing Then Call Me.Logger.AddToLog(text, entryColor)
            End Sub

            Protected Overridable Sub Dispose(disposing As Boolean)
                If Not disposedValue Then
                    If disposing Then
                        ' Verwalteten Zustand (verwaltete Objekte) bereinigen

                        If Me.TrackDict IsNot Nothing Then

                            ' Dispose of each Alt. Track Object
                            For Each objDictItem As KeyValuePair(Of Byte, MsuTrack) In Me.TrackDict

                                Dim objMsuTrack As MsuTrack = objDictItem.Value

                                Call objMsuTrack.Dispose()
                            Next

                            ' Remove reference to Dictionary conaining all Tracks
                            Me.TrackDict = Nothing
                        End If
                    End If

                    ' TODO: Nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer überschreiben
                    ' TODO: Große Felder auf NULL setzen
                    disposedValue = True
                End If
            End Sub

            ' ' TODO: Finalizer nur überschreiben, wenn "Dispose(disposing As Boolean)" Code für die Freigabe nicht verwalteter Ressourcen enthält
            ' Protected Overrides Sub Finalize()
            '     ' Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in der Methode "Dispose(disposing As Boolean)" ein.
            '     Dispose(disposing:=False)
            '     MyBase.Finalize()
            ' End Sub

            Public Sub Dispose() Implements IDisposable.Dispose
                ' Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in der Methode "Dispose(disposing As Boolean)" ein.
                Dispose(disposing:=True)
                GC.SuppressFinalize(Me)
            End Sub
        End Class
    End Namespace
End Namespace