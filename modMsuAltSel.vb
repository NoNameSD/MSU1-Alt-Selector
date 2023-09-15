Option Explicit On
Option Compare Binary
Option Strict On
Module modMsuAltSel
    '<VBFixedString(12)> Const strMainVerAltFldDefault As String = "Main Version"

    Public Property MsuTracks As MsuTracks

    Private strJsonFilePath As String
    Public Property JsonFilePath As String
        Get
            Return strJsonFilePath
        End Get
        Set(value As String)
            strJsonFilePath = value
        End Set
    End Property

    Public Sub Load()
        Call modMsuAltSel.Load(modMsuAltSel.JsonFilePath)
    End Sub

    Public Sub Load(ByRef strMsuFilePath As String)

        ' Dispose of previous data
        If modMsuAltSel.MsuTracks IsNot Nothing Then

            Call modMsuAltSel.MsuTracks.Dispose()
            modMsuAltSel.MsuTracks = Nothing

        End If

        modMsuAltSel.JsonFilePath =
               System.IO.Path.Join(
                    System.IO.Path.GetDirectoryName(strMsuFilePath),
                    System.IO.Path.GetFileNameWithoutExtension(strMsuFilePath) &
                    ".json")

        If System.IO.File.Exists(modMsuAltSel.JsonFilePath) Then

            Call System.IO.Directory.SetCurrentDirectory(System.IO.Path.GetDirectoryName(strMsuFilePath))

            ' Load .pcm Tracks with alts from saved JSON
            modMsuAltSel.MsuTracks = MsuTracks.NewFromJson(strMsuFilePath)

            If modMsuAltSel.MsuTracks.MsuLocation Is Nothing OrElse modMsuAltSel.MsuTracks.MsuLocation.Length = 0 Then
                modMsuAltSel.MsuTracks.MsuLocation = System.IO.Path.GetDirectoryName(strMsuFilePath)
            End If

            If modMsuAltSel.MsuTracks.MsuName Is Nothing OrElse modMsuAltSel.MsuTracks.MsuName.Length = 0 Then
                modMsuAltSel.MsuTracks.MsuName = System.IO.Path.GetFileNameWithoutExtension(strMsuFilePath)
            End If
        Else

            If modMsuAltSel.MsuTracks Is Nothing Then
                modMsuAltSel.MsuTracks = New MsuTracks
            End If

            modMsuAltSel.MsuTracks.MsuFilePath = strMsuFilePath

            ' Read all .pcm Tracks with alts in current folder (incl. sub folders)
            'Call modMsuAltSel.ReadMsuTracks()
            modMsuAltSel.MsuTracks.ScanMsuDirectoryForTracks()

            ' Save this data inside a JSON file
            Call modMsuAltSel.MsuTracks.SaveToJson(modMsuAltSel.JsonFilePath)

        End If

        Call modMsuAltSel.MsuTracks.SetParentObjectsOfChildren()
    End Sub

    'Friend Sub LoadJson()

    '    Dim objStream As System.IO.FileStream

    '    ' Open the JsonFile
    '    Try

    '        objStream =
    '            New System.IO.FileStream(
    '                path:=modMsuAltSel.JsonFilePath,
    '                access:=System.IO.FileAccess.Read,
    '                share:=System.IO.FileShare.Read,
    '                mode:=System.IO.FileMode.Open)

    '    Catch ex As System.IO.IOException

    '        objStream =
    '            New System.IO.FileStream(
    '                path:=modMsuAltSel.JsonFilePath,
    '                access:=System.IO.FileAccess.Read,
    '                share:=System.IO.FileShare.ReadWrite Or IO.FileShare.Delete,
    '                mode:=System.IO.FileMode.Open)

    '    End Try

    '    ' Create StreamReader for opened JsonFile
    '    Dim objStreamReader As _
    '        New System.IO.StreamReader(
    '            stream:=objStream,
    '            encoding:=System.Text.Encoding.Default)

    '    ' Deserialize JSON into MsuTracks (load data)
    '    Using objJsonReader As _
    '        New Newtonsoft.Json.JsonTextReader(
    '           reader:=objStreamReader)

    '        Dim objSerialize As New Newtonsoft.Json.JsonSerializer()

    '        modMsuAltSel.MsuTracks = objSerialize.Deserialize(Of MsuTracks)(objJsonReader)

    '    End Using

    '    Call objStreamReader.Close()
    'End Sub

    'Friend Sub SaveJson()

    '    Dim strJsonPath As String = modMsuAltSel.JsonFilePath
    '    Dim strJsonPathOld As String = strJsonPath & "_old"
    '    Dim strJsonPathTmp As String = strJsonPath & "_tmp"
    '    Dim i As Byte = 0

    '    While System.IO.File.Exists(strJsonPathTmp)
    '        strJsonPathTmp = strJsonPath & "_tmp" & i
    '        i += modGeneralStuff.bytOne
    '    End While

    '    Dim objStream As _
    '        New System.IO.FileStream(
    '            path:=strJsonPathTmp,
    '            access:=IO.FileAccess.Write,
    '            share:=IO.FileShare.Read,
    '            mode:=IO.FileMode.CreateNew)

    '    Dim objStreamWriter As _
    '        New System.IO.StreamWriter(
    '            stream:=objStream,
    '            encoding:=System.Text.Encoding.Default)

    '    Using objJsonWriter As _
    '        New Newtonsoft.Json.JsonTextWriter(
    '           textWriter:=objStreamWriter)

    '        objJsonWriter.IndentChar = CChar(vbTab)
    '        objJsonWriter.Indentation = 1
    '        objJsonWriter.Formatting = Newtonsoft.Json.Formatting.Indented

    '        Dim objSerialize As New Newtonsoft.Json.JsonSerializer()

    '        Call objSerialize.Serialize(objJsonWriter, modMsuAltSel.MsuTracks)

    '        Call objJsonWriter.Flush()
    '    End Using

    '    Call objStream.Close()

    '    If System.IO.File.Exists(strJsonPath) Then

    '        Call System.IO.File.Replace(strJsonPathTmp, strJsonPath, strJsonPathOld)

    '    Else

    '        Call System.IO.File.Move(strJsonPathTmp, strJsonPath)

    '    End If
    'End Sub

    'Friend Sub ReadMsuTracks()
    '    Dim objMsuTrackSortDict As New SortedDictionary(Of Byte, MsuTrack)
    '    Dim strSubFolders() As String = System.IO.Directory.GetDirectories(modMsuAltSel.MsuTracks.MsuLocation)

    '    Call AddPcmTracksInMainDirToDict(objMsuTrackSortDict)

    '    ' Read all PCM-Files in the sub directories, but not the main directory itself
    '    If strSubFolders.Length <> 0 Then
    '        For i As Byte = LBound(strSubFolders) To UBound(strSubFolders)

    '            Call AddPcmTracksInDirToDict(objMsuTrackSortDict, strSubFolders(i))
    '        Next
    '    End If

    '    modMsuAltSel.MsuTracks = New MsuTracks With {
    '        .TrackDict = objMsuTrackSortDict
    '    }
    'End Sub

    'Private Sub AddPcmTracksInMainDirToDict(
    '    ByRef objMsuTrackDict As SortedDictionary(Of Byte, MsuTrack))

    '    ' Search for all PCM-Files in the MSU-Folder (not including subfolders)
    '    Dim strPcmFiles() As String =
    '        System.IO.Directory.GetFiles(
    '            path:=modMsuAltSel.MsuTracks.MsuLocation,
    '            searchPattern:=modMsuAltSel.MsuTracks.PcmPrefix & "*.pcm",
    '            searchOption:=System.IO.SearchOption.TopDirectoryOnly)

    '    If strPcmFiles.Length = 0 Then
    '        Return
    '    End If

    '    Dim strLocation = GetAltLocationForMainTrackVersion()

    '    For i = LBound(strPcmFiles) To UBound(strPcmFiles)

    '        Call AddPcmMainTrackToDict(objMsuTrackDict, strPcmFiles(i), strLocation)

    '    Next
    'End Sub

    'Private Function GetAltLocationForMainTrackVersion() As String
    '    Dim sAltFldPath As String = System.IO.Path.GetFullPath(path:=strMainVerAltFldDefault, basePath:=modMsuAltSel.MsuTracks.MsuLocation)

    '    If CheckAndCreateIfFolderPathAvailableForPcm(sAltFldPath) Then
    '        Return sAltFldPath
    '    Else
    '        Return GetAltLocationForMainTrackVersionFallback()
    '    End If

    'End Function

    'Private Function GetAltLocationForMainTrackVersionFallback() As String
    '    Dim sAltFldPath As String

    '    For i = Byte.MinValue To Byte.MaxValue

    '        sAltFldPath = System.IO.Path.GetFullPath(path:=strMainVerAltFldDefault & "_" & i, basePath:=modMsuAltSel.MsuTracks.MsuLocation)

    '        If CheckAndCreateIfFolderPathAvailableForPcm(sAltFldPath) Then

    '            Return sAltFldPath
    '        End If
    '    Next
    '    Return vbNullString
    'End Function

    'Private Function CheckAndCreateIfFolderPathAvailableForPcm(ByRef strFolderPath As String) As Boolean

    '    ' Cancel if a file exists with this path (Very unlikely, but possible)
    '    If System.IO.File.Exists(strFolderPath) Then
    '        Return False
    '    End If

    '    ' Check if this directory already exists
    '    If System.IO.Directory.Exists(strFolderPath) Then

    '        ' Search for all PCM-Files in this folder
    '        Dim strPcmFiles() As String =
    '            System.IO.Directory.GetFiles(
    '                path:=strFolderPath,
    '                searchPattern:=modMsuAltSel.MsuTracks.PcmPrefix & "*.pcm",
    '                searchOption:=System.IO.SearchOption.TopDirectoryOnly)

    '        ' Folder can be used to store the PCM-Files, if there are none in there currently
    '        Return strPcmFiles.Length = 0
    '    Else
    '        Try ' Create this directory
    '            Call System.IO.Directory.CreateDirectory(strFolderPath)
    '            Return True
    '        Catch
    '            Return False
    '        End Try
    '    End If

    'End Function

    'Private Sub AddPcmTracksInDirToDict(
    '    ByRef objMsuTrackDict As SortedDictionary(Of Byte, MsuTrack),
    '    ByRef strFolderPath As String)

    '    ' Search for all PCM-Files in the specified folder including subfolders
    '    Dim strPcmFiles() As String =
    '        System.IO.Directory.GetFiles(
    '            path:=strFolderPath,
    '            searchPattern:=modMsuAltSel.MsuTracks.PcmPrefix & "*.pcm",
    '            searchOption:=System.IO.SearchOption.AllDirectories)

    '    If strPcmFiles.Length = 0 Then
    '        Exit Sub
    '    End If

    '    ' Read all found files
    '    For i As UShort = LBound(strPcmFiles) To UBound(strPcmFiles)
    '        AddPcmTrackToDict(objMsuTrackDict, strPcmFiles(i))
    '    Next
    'End Sub

    'Private Function GetTrackByNumberAndAddToDictIfMissing(
    '    ByRef objMsuTrackDict As SortedDictionary(Of Byte, MsuTrack),
    '    ByRef bytTrackNumber As Byte) As MsuTrack

    '    ' Checks if there is already an entry for this TrackNumber
    '    If objMsuTrackDict.ContainsKey(bytTrackNumber) Then

    '        ' Get the object for this TrackNumber
    '        Return objMsuTrackDict.Item(bytTrackNumber)

    '    Else
    '        Dim objMsuTrack As MsuTrack

    '        ' Create the track for this TrackNumber
    '        objMsuTrack = New MsuTrack(bytTrackNumber)

    '        ' Add the track to the array
    '        Call objMsuTrackDict.Add(bytTrackNumber, objMsuTrack)

    '        Return objMsuTrack
    '    End If

    'End Function

    'Private Sub AddPcmTrackToDict(
    '    ByRef objMsuTrackDict As SortedDictionary(Of Byte, MsuTrack),
    '    ByRef strPcmFile As String)

    '    Dim bytTrackNumber As Byte = MsuTracks.GetTrackNumberForPcm(strPcmFile)

    '    Dim objMsuTrack As MsuTrack
    '    Dim objMsuTrackAltArray() As MsuTrackAlt

    '    Dim intAltNumber As UShort

    '    objMsuTrack = GetTrackByNumberAndAddToDictIfMissing(objMsuTrackDict, bytTrackNumber)

    '    objMsuTrackAltArray = objMsuTrack.TrackAltArray()

    '    ' Checks if there are no alt tracks for this track
    '    If objMsuTrack.TrackAltArray Is Nothing _
    'OrElse objMsuTrack.TrackAltArray.Length = 0 Then

    '        ' Don't allow adding an alt. Track without Main Version
    '        Exit Sub

    '    Else

    '        ' Set the AltNumber of the alt. Track to the highest number +1
    '        intAltNumber = GetHighestAltNumForTrack(objMsuTrack) + 1

    '        ' Increase the alt. Track array
    '        ReDim Preserve objMsuTrackAltArray(UBound(objMsuTrackAltArray) + 1)

    '    End If

    '    ' Get Location from PCM-Path (Relative path to parent folder)
    '    Dim strLocation As String = System.IO.Path.GetDirectoryName(strPcmFile)

    '    ' Get Title from the name of the parent folder
    '    Dim strAltTitle As String = System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(strPcmFile))

    '    Dim objMsuTrackAlt As _
    '            New MsuTrackAlt(
    '                intAltNumber:=intAltNumber,
    '                strLocation:=strLocation,
    '                strTitle:=strAltTitle)

    '    ' Add this alt. Track to the Track
    '    objMsuTrackAltArray(UBound(objMsuTrackAltArray)) = objMsuTrackAlt

    '    objMsuTrack.TrackAltArray = objMsuTrackAltArray
    'End Sub

    'Private Sub AddPcmMainTrackToDict(
    '    ByRef objMsuTrackDict As SortedDictionary(Of Byte, MsuTrack),
    '    ByRef strPcmFile As String,
    '    ByRef strLocation As String)

    '    Dim bytTrackNumber As Byte = MsuTracks.GetTrackNumberForPcm(strPcmFile)
    '    Dim intAltNumber As UShort

    '    Dim objMsuTrack As MsuTrack
    '    Dim objMsuTrackAltArray() As MsuTrackAlt

    '    objMsuTrack = GetTrackByNumberAndAddToDictIfMissing(objMsuTrackDict, bytTrackNumber)

    '    objMsuTrackAltArray = objMsuTrack.TrackAltArray

    '    ' Checks if there are no alt tracks for this track
    '    If objMsuTrackAltArray Is Nothing _
    'OrElse objMsuTrackAltArray.Length = 0 Then

    '        bytTrackNumber = 0

    '        ' Add the first alt track for this TrackNumber
    '        ReDim objMsuTrackAltArray(0)

    '    Else

    '        Dim bAltIdZeroAvailable As Boolean = True

    '        ' Check if an entry for the Main Version already exists
    '        For Each objMsuTrackAltCheck As MsuTrackAlt In objMsuTrackAltArray

    '            ' Check if the AltNumber 0 is already taken
    '            If objMsuTrackAltCheck.AltNumber = 0 Then
    '                bAltIdZeroAvailable = False
    '            End If

    '            If StrComp(
    '                String1:=objMsuTrackAltCheck.LocationAbsolute,
    '                String2:=strLocation,
    '                Compare:=vbTextCompare) Then

    '                ' Entry for Main Ver. exists. Don't add again
    '                Return
    '            End If
    '        Next

    '        If bAltIdZeroAvailable Then
    '            intAltNumber = 0
    '        Else
    '            ' Set the AltNumber of the alt. Track to the highest number + 1 
    '            intAltNumber = GetHighestAltNumForTrack(objMsuTrack) + 1
    '        End If

    '        ' Increase the Array for the alt. Tracks
    '        ReDim Preserve objMsuTrackAltArray(UBound(objMsuTrackAltArray) + 1)

    '    End If

    '    Dim objMsuTrackAlt As _
    '            New MsuTrackAlt(
    '                intAltNumber:=intAltNumber,
    '                strLocation:=strLocation,
    '                strTitle:=strMainVerAltFldDefault)

    '    Dim intArrPos As Integer = UBound(objMsuTrackAltArray)

    '    ' Add the main alt. Track to the Track
    '    objMsuTrackAltArray(intArrPos) = objMsuTrackAlt
    '    objMsuTrack.TrackAltArray = objMsuTrackAltArray

    'End Sub

    'Private Function GetHighestAltNumForTrack(ByRef objMsuTrack As MsuTrack) As UShort
    '    GetHighestAltNumForTrack = 0

    '    For Each objAlt In objMsuTrack.TrackAltArray

    '        If objAlt.AltNumber > GetHighestAltNumForTrack Then

    '            GetHighestAltNumForTrack = objAlt.AltNumber
    '        End If
    '    Next
    'End Function



End Module