Attribute VB_Name = "mod_MAS_Functions"
Option Explicit

Private oFSO       As New Scripting.FileSystemObject

Private sMSUfolder As String ' MSU Folderpath
Private sMSUname   As String ' Name of the MSU Rom (wihout extention)
Private sAltFolder As String ' The Name of the Alt. Subfolder of the MSU1 Directory
Private sCurSelAltTrack As String ' Current Trackname for the currently selected ID, that is selected
Private iTracksWithAlts() As Byte ' All TrackId's where alternative Tracks have been found in the Alt. Folder

Private sAudioProgramPath As String ' Path of MSUPCM++, FFMPEG or SOX (Needed for speed change)
Private Enum eAudioProgram
    apNotSet = 0    ' Not Set  (Will search down this list)
    apMSUPCM = 1    ' MSUPCM++ (msupcm.exe)
    apFFMPEG = 2    ' FFMPEG   (ffmpeg.exe)
    apSoX = 3       ' SoX      (sox.exe)
End Enum
Private eAudioProgramUsed As eAudioProgram ' Which type of program to use when changing the audio insode the .pcm files
Private iSettings As Byte
Private ihWnd As Long

Private Const sChr34 As String * 1 = """"
Private Const sChr32 As String * 1 = " "
Const iBufferMax As Long = 2 ^ 20 ' Set the read Buffer to be 1MiB max at a time (Only when using FFMPEG or SOX) (Theoretical max is somewhere around &H8000000, but it seems to be slower)

Public Property Get hWnd() As Long
    Let hWnd = ihWnd
End Property

Public Property Let hWnd(ByRef ihWndNew As Long)
    Let ihWnd = ihWndNew
End Property

' Is set, when the audio tracks are currently in 'resampled' mode
Public Property Get IsResampled() As Boolean
    Let IsResampled = (iSettings And (2 ^ 0))
End Property

Public Property Let IsResampled(ByRef bValue As Boolean)
    If bValue <> mod_MAS_Functions.IsResampled Then
        Let iSettings = (iSettings Xor (2 ^ 0))
    End If
End Property

' When True, displays the CMD Windows while creating the .pcm fies
Public Property Get CmdShowWindows() As Boolean
    Let CmdShowWindows = (iSettings And (2 ^ 1))
End Property

Public Property Let CmdShowWindows(ByRef bValue As Boolean)
    If bValue <> mod_MAS_Functions.CmdShowWindows Then
        Let iSettings = (iSettings Xor (2 ^ 1))
    End If
End Property

' When True, keeps the CMD Windows opened (For debugging only)
Public Property Get CmdKeepWindowsOpened() As Boolean
    Let CmdKeepWindowsOpened = (iSettings And (2 ^ 2))
End Property

Public Property Let CmdKeepWindowsOpened(ByRef bValue As Boolean)
    If bValue <> mod_MAS_Functions.CmdKeepWindowsOpened Then
        Let iSettings = (iSettings Xor (2 ^ 2))
    End If
End Property


Private Sub Main()
    Dim sMSUpath As String

    ' Checks if there is an .SFC or .SMC rom in the executed directory
    sMSUpath = check_ROM_in_CUR_DIR

    ' If no ROM Found
    If LenB(sMSUpath) = 0 Then

        ' User needs to select the ROM
        sMSUpath = UserSelectROM

    End If

    ' User cancelled the selection
    If LenB(sMSUpath) = 0 Then

        ' Close the Program
        End

    End If

    ' Checks, if the Selection is valid and sets the Variables
    Call InitializeMSU1Folder(sMSUpath)

    If LenB(sAltFolder) <> 0 Then

        Dim sAltPath As String

        sAltPath = oFSO.BuildPath(oFSO.BuildPath(sMSUfolder, sAltFolder), sMSUname & "-")


        ' Special Characters get renamed to "_" when zipped by Google Drive
        ' This reverts that

        If oFSO.FolderExists(oFSO.BuildPath(sAltPath & 5, "Wrinkly_s Save Cave")) Then

            Call RenameAltTrack(5, "Wrinkly_s Save Cave", "Wrinkly's Save Cave")
        End If

        If oFSO.FolderExists(oFSO.BuildPath(sAltPath & 52, "Nuts _ Bolts Win")) Then

            Call RenameAltTrack(52, "Nuts _ Bolts Win", "Nuts & Bolts Win")
        End If

        If oFSO.FolderExists(oFSO.BuildPath(sAltPath & 53, "Nuts _ Bolts Death")) Then

            Call RenameAltTrack(53, "Nuts _ Bolts Death", "Nuts & Bolts Death")
        End If

        If oFSO.FolderExists(oFSO.BuildPath(sAltPath & 50, "Bonus Time_ Lose")) Then

            Call RenameAltTrack(53, "Bonus Time_ Lose", "Bonus Time! Lose")
        End If

        If oFSO.FolderExists(oFSO.BuildPath(sAltPath & 51, "Bonus Time_ Win")) Then

            Call RenameAltTrack(53, "Bonus Time_ Win", "Bonus Time! Win")
        End If

        If oFSO.FolderExists(oFSO.BuildPath(sAltPath & 49, "Bonus Time_")) Then

            Call RenameAltTrack(49, "Bonus Time_", "Bonus Time!")
        End If

    End If

    ' Opens the User Form
    Call frm_MSU1_Alt_Select.Show

End Sub

' Checks, if an .SFC or .SMC ROM is present in the executed Directory
' And returns it, if present
Private Function check_ROM_in_CUR_DIR() As String
    Dim oFile As Scripting.File
    For Each oFile In oFSO.GetFolder(App.Path).Files
        Select Case UCase(oFSO.GetExtensionName(oFile.Name))
        Case "SFC", "SMC"
            check_ROM_in_CUR_DIR = oFile.Path
        End Select
    Next
End Function

Public Function UserSelectROM() As String

    With OFN

        ' size of the OFN structure
        .nStructSize = Len(OFN)

        ' default filename, plus additional padding
        ' for the user's final selection(s). Must be
        'double-null terminated
        .sFile = vbNullChar & Space$(1024) & vbNullChar & vbNullChar

        ' the size of the buffer
        .nMaxFile = Len(.sFile)

        ' Sets the Selection-Filter for SNES ROM's
        .sFilter = "SNES ROM (*.sfc)" & vbNullChar & "*.sfc;*.smc" & vbNullChar & "All Files (*.*)" & vbNullChar & "*.*"

        'the dialog title
        .sDialogTitle = "Select the SNES MSU1 ROM, that the program applies to (e.g. ""dkc3_msu.sfc"")"

    End With

    ' Call the API
    If GetOpenFileName(OFN) Then

        ' Returns the Selected Filepath
        UserSelectROM = OFN.sFile

    End If

End Function

' Checks, if the selected Path is valid and sets the Variables
Public Sub InitializeMSU1Folder(sMSUpath As String)
    On Error Resume Next
    If LenB(sMSUpath) = 0 Then Exit Sub

    Dim sSubFolders() As String
    Dim i As Byte
    Dim oDIR As Scripting.Folder

    ' Sets the Folderpath
    sMSUfolder = oFSO.GetParentFolderName(sMSUpath)

    ' Sets the ROM Prefix
    sMSUname = oFSO.GetBaseName(sMSUpath)

    ' Put all Subdirectorys in an Array
    For Each oDIR In oFSO.GetFolder(sMSUfolder).SubFolders
        ReDim Preserve sSubFolders(i)
        sSubFolders(i) = oDIR.Name
        i = i + 1
    Next

    ' No subdirectories found: Cancel search for Alt.Folder
    If i = 0 Then
        Erase iTracksWithAlts
        GoTo CheckResampled
    End If

    ' Check all SubFolders for valid Alt.Tracks (Keeps the first one it finds)
    For i = 0 To UBound(sSubFolders)

        sAltFolder = sSubFolders(i)
        iTracksWithAlts = readTracksWithAlts

        ' Checks if Subfolder has valid AltTracks (Array is initialized)
        If Not Not iTracksWithAlts Then

            ' Folder has Alt. Tracks with the correct pattern
            ' Sort the Array numerically (Is sorted alphabetically by default)
            Call sortNumArray(iTracksWithAlts)
            GoTo CheckResampled

        Else

            ' Folder is Invalid
            sAltFolder = vbNullString
        End If
    Next

CheckResampled:

    ' Set the flag, if there are any tracks with the 'Normal' Suffix found
    Let mod_MAS_Functions.IsResampled = CBool(Not Not getPathsAllTracks("_Normal"))
End Sub

' Create an Array with all Tracks, that have Alt. Versions
Private Function readTracksWithAlts() As Byte()
    Dim iTracks() As Byte
    Dim iTrackId As Byte
    Dim i As Integer
    Dim oDIR As Scripting.Folder

    ' For Each Subfolder in the Alt. Directory
    For Each oDIR In oFSO.GetFolder(oFSO.BuildPath(sMSUfolder, sAltFolder)).SubFolders

        ' Checks, if the Folder has the correct Prefix
        If Left(oDIR.Name, Len(sMSUname)) = sMSUname Then
            On Error Resume Next

            ' Reads the Tracknumber from the Folder (0 - 255)
            iTrackId = CByte(Right(oDIR.Name, Len(oDIR.Name) - InStrRev(oDIR.Name, "-")))

            ' If no Error occured
            If Err = 0 Then
                ' Add the Track to the Array
                ReDim Preserve iTracks(i)
                iTracks(i) = iTrackId
                i = i + 1
            Else
                Debug.Print Err & vbTab & Err.Description
                Err.Clear
            End If

        End If

    Next

    readTracksWithAlts = iTracks
End Function

' Returns all Alt. Tracks for the Selected TrackId
Public Function getAltTrackList(ByVal iTrackId As Byte, Optional ByRef iSelected As Integer, Optional ByRef sSuffix As String = vbNullString) As String()
    Dim sIDaltPath As String
    Dim sAltTracks() As String
    Dim oDIR As Scripting.Folder
    Dim bFlag As Boolean
    Dim i As Integer

    sIDaltPath = oFSO.BuildPath(oFSO.BuildPath(sMSUfolder, sAltFolder), sMSUname & "-" & iTrackId)

    ' Check Each Alt. Subdirectory for the selected TrackID
    For Each oDIR In oFSO.GetFolder(sIDaltPath).SubFolders

        ' Checks if the Track is in Use (Track with Prefix and ID is not Present in that Folder)
        If IsTrackInUse(oDIR.Path, iTrackId, sSuffix) Then
            ' If there is already a Track in Use foud, skip
            ' (Only add the First one Found)
            If bFlag Then GoTo NextFile
            bFlag = True

            ' Set the Selected Track to the current position
            iSelected = i
        End If

        ' Add the Alt. Track to the Array
        ReDim Preserve sAltTracks(i)
        sAltTracks(i) = oDIR.Name
        i = i + 1
NextFile:
    Next

    ' If No Selected Track Found
    If Not bFlag Then

        ' Create DEFAULT Folder
        Call oFSO.CreateFolder(oFSO.BuildPath(sIDaltPath, "Z_DEFAULT"))

        ' Adds the Default Folder and sets it as the selected Track
        ReDim Preserve sAltTracks(i)
        sAltTracks(i) = "Z_DEFAULT"
        iSelected = i

    End If

    ' Sets the current selected Track String (Needed Later)
    sCurSelAltTrack = sAltTracks(iSelected)

    getAltTrackList = sAltTracks
End Function

' Returns, if the track is currently in use (Track is Missing from the Alt. Folder)
Private Function IsTrackInUse(sPath As String, iTrackId As Byte, Optional ByRef sSuffix As String = vbNullString) As Boolean

    If oFSO.FileExists(oFSO.BuildPath(sPath, sMSUname & "-" & iTrackId & sSuffix & ".pcm")) Then Exit Function

    IsTrackInUse = True
End Function

' Return if this TrackId is currently being played (File is Opened)
Private Function isTrackOpen(iTrackId As Byte) As Boolean
    Dim iFileNum As Byte
    Dim sPath As String

    ' Get The Filepath that gets checked
    sPath = oFSO.BuildPath(sMSUfolder, sMSUname & "-" & iTrackId & ".pcm")

    'Allow all errors to happen
    On Error Resume Next
    iFileNum = FileSystem.FreeFile

    Err.Clear

    ' Try to open and close the file for input.
    ' Errors mean the file is already open
    Open sPath For Input Lock Read As #iFileNum
    Close iFileNum

    If Err = 70 Then
        isTrackOpen = True
    End If

    Err.Clear

End Function

' Removes the first 8 Bytes (MSU1 Header) of the PCM-File
Private Sub removeMSU1Header(sPathIn As String, sPathOut As String)
    Dim iFileNum As Byte
    Dim iFileNumCopy As Byte
    Dim sBuffer As String
    Dim dSize As Double
    Dim bCopied As Boolean
    Dim i As Byte

    If oFSO.FileExists(sPathOut) Then
        Call oFSO.DeleteFile(sPathOut, True)
    End If

    iFileNum = FileSystem.FreeFile

    ' Open the PCM-Audio-File
    Open sPathIn For Binary Access Read Shared As #iFileNum
    ' Ignore first 8 Bytes (Header)
    sBuffer = String$(8, Empty)
    Get #iFileNum, , sBuffer
    dSize = LOF(iFileNum) - 8

    iFileNumCopy = FileSystem.FreeFile

    ' Create Headerless Copy
    Open sPathOut For Binary Access Write As #iFileNumCopy

    ' Insert Audio
    Do

        If dSize > iBufferMax Then
            sBuffer = String$(iBufferMax, Empty)
            dSize = dSize - iBufferMax
        Else
            sBuffer = String$(dSize, Empty)
            bCopied = True
        End If

        Get #iFileNum, , sBuffer
        Put #iFileNumCopy, , sBuffer

    Loop Until bCopied

    Close #iFileNum
    Close #iFileNumCopy
End Sub

' Reads the LoopPoint from a MSU1 PCM file
Public Function getLoopPointFromPCM(sPath As String, Optional sLatestAction As String) As Double
    Dim iFileNum As Byte
    Static sHeader As String * 8
    Dim sHexNum As String
    Dim i As Byte

    sLatestAction = "Reading Header from " & sChr34 & sPath & sChr34

    iFileNum = FileSystem.FreeFile

    ' Open the PCM-Audio-File
    Open sPath For Binary Access Read Shared As #iFileNum
    ' Get the first 8 Bytes from the PCM
    Get #iFileNum, , sHeader
    Close #iFileNum

    i = 8

    sLatestAction = "Decoding LoopPoint from " & sChr34 & sPath & sChr34

    ' Read the LoopPoint (Saved in the PCM in Little-Endian)
    Do Until i = 4

        ' Each Hexnumber has to be a pair
        If Asc(Mid(sHeader, i, 1)) < 16 Then
            sHexNum = sHexNum & "0" & Hex$(Asc(Mid(sHeader, i, 1)))
        Else
            sHexNum = sHexNum & Hex$(Asc(Mid(sHeader, i, 1)))
        End If

        i = i - 1
    Loop

    ' Converts the HEXnum of the LoopPoint to an Integer
    getLoopPointFromPCM = Hex2UInt(sHexNum)

End Function

' Generates the MSU1-Header (First 8 Bytes)
Private Function generateMSU1Header(dLoopPoint As Double) As String
    Static sLoopPointHex As String

    ' If the LoopPoint is bigger or equal to the maximum value
    If dLoopPoint >= 4294967295# Then

        ' Set The LoopPoint to the maximum value
        sLoopPointHex = "FFFFFFFF"

    Else
        ' Convert the Calculated LoopPoint to Hex
        sLoopPointHex = Hex$(dLoopPoint)

        ' Set the HEX-LoopPoint to a fixed 8 Char Length and fill the rest with 0's
        sLoopPointHex = String(8 - Len(sLoopPointHex), "0") & sLoopPointHex
    End If

    ' Generate the Header ("MSU1" + the LoopPoint in Little-Endian)
    generateMSU1Header = "MSU1" & Chr("&h" & Mid$(sLoopPointHex, 7, 2)) & Chr("&h" & Mid$(sLoopPointHex, 5, 2)) & Chr("&h" & Mid$(sLoopPointHex, 3, 2)) & Chr("&h" & Mid$(sLoopPointHex, 1, 2))

End Function

' Edits the Header of a MSU1 PCM File
Private Sub editMSSU1Header(sPathPCM, dLoopPoint As Double)
    Dim iFileNum As Byte
    Dim iErrNum As Long
    Dim iErrCount As Byte

    Let iFileNum = FileSystem.FreeFile

Func_Start:
    On Error Resume Next

    ' Open the PCM
    Open sPathPCM For Binary Access Write As #iFileNum

    Let iErrNum = VBA.Err.Number
    On Error GoTo 0

    Select Case iErrNum
        Case 0
        Case 70
            Let iErrCount = iErrCount + 1
            Debug.Print "ErrCount " & iErrCount & " while editing looppoint of """ & sPathPCM & """"
            GoTo Func_Start
        Case Else
            Call VBA.Err.Raise(iErrNum)
    End Select

    ' Insert New Header
    Put #iFileNum, , generateMSU1Header(dLoopPoint)

    Close #iFileNum

End Sub

' Adds the MSU1 Header to a RAW Audio File
Private Sub addMSU1HeaderToRaw(sPathRaw As String, sPathOut As String, dLoopPoint As Double)
    Dim iFileNum As Byte
    Dim iFileNum2 As Byte
    Dim sBuffer As String
    Dim dSize As Double
    Dim bCopied As Boolean

    ' If the Output-Path already exists, delete it
    If oFSO.FileExists(sPathOut) Then
        Call oFSO.DeleteFile(sPathOut, True)
    End If

    iFileNum = FreeFile

    ' Open the RAW Headerless Audio-File
    Open sPathRaw For Binary Access Read Shared As #iFileNum
    dSize = LOF(iFileNum)

    iFileNum2 = FreeFile

    ' Create the File With The Header
    Open sPathOut For Binary Access Write As #iFileNum2

    ' Insert Header
    Put #iFileNum2, , generateMSU1Header(dLoopPoint)

    ' Insert Audio from the RAW Headerless Audio-File
    Do

        If dSize > iBufferMax Then
            sBuffer = String$(iBufferMax, Empty)
            dSize = dSize - iBufferMax
        Else
            sBuffer = String$(dSize, Empty)
            bCopied = True
        End If

        Get #iFileNum, , sBuffer
        Put #iFileNum2, , sBuffer

    Loop Until bCopied

    Close #iFileNum
    Close #iFileNum2
End Sub

' Generates the Shell-String for Resamling
Public Function GenerateShellAudioResample(sPathIn As String, sPathOut As String, iSampleRate As Long, iVolumePercent As Integer, Optional sLatestAction As String) As String
    Dim dVolumePercent As Single
    Dim sVolumePercent As String
    Dim sVolumePercentShell As String
    Dim sSampleRateSpeed As String
    Dim sSampleRateSpeedShell As String
    Dim iErrCount As Byte
    Dim iErrNum As Long

    sLatestAction = "Converting " & sChr34 & sPathIn & sChr34 & " to " & sChr34 & sPathOut & sChr34

Func_Start:
    On Error Resume Next

    Let dVolumePercent = iVolumePercent / 100

    Let iErrNum = VBA.Err.Number
    On Error GoTo 0

    Select Case iErrNum
        Case 0
        Case 16
            Let iErrCount = iErrCount + 1
            Debug.Print "ErrCount " & iErrCount & " while converting """ & sPathIn & """"
            GoTo Func_Start
        Case Else
            Call VBA.Err.Raise(iErrNum)
    End Select

    Let sVolumePercent = dVolumePercent
    Let sVolumePercent = VBA.Strings.Replace(sVolumePercent, ",", ".")

    If iVolumePercent <> 100 Then
        Let sVolumePercentShell = " -v " & sVolumePercent
    Else
        Let sVolumePercentShell = vbNullString
    End If

    If iSampleRate <> 44100 Then
        Let sSampleRateSpeed = iSampleRate / 44100
        Let sSampleRateSpeed = Replace$(sSampleRateSpeed, ",", ".")
    Else
        Let sSampleRateSpeed = iSampleRate
    End If

    Select Case eAudioProgramUsed

        Case eAudioProgram.apMSUPCM

            Let sLatestAction = sLatestAction & " via MSUPCM++"

            If iSampleRate <> 44100 Then
                Let sSampleRateSpeedShell = " speed " & sSampleRateSpeed
            Else
                Let sSampleRateSpeedShell = vbNullString
            End If

            Let GenerateShellAudioResample = _
            sChr34 & sAudioProgramPath & sChr34 & _
            sChr32 & "-s -V4" & _
            sVolumePercentShell & _
            sChr32 & sChr34 & sPathIn & sChr34 & _
            sChr32 & sChr34 & sPathOut & sChr34 & _
            sSampleRateSpeedShell

        Case eAudioProgram.apFFMPEG

            Let sLatestAction = sLatestAction & " via FFMPEG"

            Let GenerateShellAudioResample = _
            sChr34 & sAudioProgramPath & sChr34 & _
            " -y -sample_rate 44100 -f s16le -ac 2 -i " & sChr34 & sPathIn & sChr34 & _
            " -f s16le -ac 2 " & _
            "-af asetrate=" & iSampleRate & ",aresample=44100" & _
            ",volume=" & sVolumePercent & sChr32 & _
            sChr34 & sPathOut & sChr34

        Case eAudioProgram.apSoX

            Let sLatestAction = sLatestAction & " via SoX"

            Let GenerateShellAudioResample = _
            sChr34 & sAudioProgramPath & sChr34 & _
            sVolumePercentShell & _
            " -V4 -r 44100 -t raw -e signed -b 16 -c 2" & _
            sChr32 & sChr34 & sPathIn & sChr34 & _
            " -t raw " & sChr34 & sPathOut & sChr34 & _
            " speed " & Replace$(iSampleRate / 44100, ",", ".")

    End Select

End Function

' Searches for the Filename in the current directory and all its subdirectories
Public Function checkPathForFile(ByRef sFilenameCheck As String) As String
    Dim colQueue As New Collection
    Dim oDIR     As Scripting.Folder
    Dim oDIRsub  As Scripting.Folder
    Dim oFile    As Scripting.File

    sFilenameCheck = UCase(sFilenameCheck)

    ' Set the first folder to be te parent folder of this program
    Call colQueue.Add(oFSO.GetFolder(App.Path))

    ' Go through all subfolders of the AppPath
    Do Until colQueue.Count = 0

        ' Open current entry in collection
        Set oDIR = colQueue(1)
        ' Remove current entry from collection
        colQueue.Remove 1

        ' Add all subfolders of this entry to the collection
        For Each oDIRsub In oDIR.SubFolders
            Call colQueue.Add(oDIRsub)
        Next oDIRsub

        ' Check if the provided Filename is in that directory
        For Each oFile In oDIR.Files
            ' File is Sox.exe
            If UCase(oFile.Name) = sFilenameCheck Then
                ' Set Sox-Path and exit function
                checkPathForFile = oFile.Path
                Exit Function
            End If
        Next
    Loop
End Function

Private Function checkForAudioChangeProgram() As Boolean
    Dim sCheck As String

    ' Check for MSUPCM++ in the EXE directoy and its subdirectories
    sCheck = checkPathForFile("MSUPCM.EXE")

    If LenB(sCheck) <> 0 Then eAudioProgramUsed = eAudioProgram.apMSUPCM: GoTo SearchSuccess

    ' Check for FFMPEG in the EXE directoy and its subdirectories
    sCheck = checkPathForFile("FFMPEG.EXE")

    If LenB(sCheck) <> 0 Then eAudioProgramUsed = eAudioProgram.apFFMPEG: GoTo SearchSuccess

    ' Check for SoX in the EXE directoy and its subdirectories
    sCheck = checkPathForFile("SOX.EXE")

    If LenB(sCheck) <> 0 Then eAudioProgramUsed = eAudioProgram.apSoX: GoTo SearchSuccess

    ' Check for MSUPCM++ in %PATH%
    Let sCheck = mod_WinApi.FindExecutable("MSUPCM.EXE")

    If LenB(sCheck) <> 0 Then eAudioProgramUsed = eAudioProgram.apMSUPCM: GoTo SearchSuccess

    ' Check for FFMPEG in %PATH%
    Let sCheck = mod_WinApi.FindExecutable("FFMPEG.EXE")

    If LenB(sCheck) <> 0 Then eAudioProgramUsed = eAudioProgram.apFFMPEG: GoTo SearchSuccess

    ' Check for SoX in %PATH%
    Let sCheck = mod_WinApi.FindExecutable("SOX.EXE")

    If LenB(sCheck) <> 0 Then eAudioProgramUsed = eAudioProgram.apSoX: GoTo SearchSuccess

SearchFailure:
    Call MsgBox("No program found for changing audio." & vbNewLine & vbNewLine & "Please insert either MSUPCM++, FFMPEG, or SoX into this directory (or subdirectory)!", vbCritical)
    Exit Function

SearchSuccess:
    sAudioProgramPath = sCheck
    checkForAudioChangeProgram = True
End Function

Private Function EnumAudioProgramToStr(ByRef ePrg As eAudioProgram) As String

    Select Case ePrg
        Case eAudioProgram.apMSUPCM
            Let EnumAudioProgramToStr = "MSUPCM.EXE"
        Case eAudioProgram.apFFMPEG
            Let EnumAudioProgramToStr = "FFMPEG.EXE"
        Case eAudioProgram.apSoX
            Let EnumAudioProgramToStr = "SOX.EXE"
        Case Else
            Let EnumAudioProgramToStr = "INVALID"
    End Select

End Function

' Resamples the Audio Files with FFMPEG or SoX
Public Sub changeSpeed(ByVal iSampleRate As Long, ByVal iVolumePercent As Integer, ByRef sLatestAction As String)
    Dim sPathNormal() As String     ' Path of the Input .pcm File (Normal Version)
    Dim sPathUsed() As String       ' Path of the currently used .pcm (Without any Suffix)(Including Alt.)
    Dim sPathTemp() As String       ' Path of the Headerless copy of the Input .pcm (First 8 Bytes removed)
    Dim sPathTempConv() As String   ' Path of the converted file
    Dim sPathOut() As String        ' Path of the converted file, with the Header added
    Dim sShell As String
    Dim dLoopPoint As Double
    Dim i As Integer

    ' If no Path is set
    If eAudioProgramUsed = eAudioProgram.apNotSet Or LenB(sAudioProgramPath) = 0 Or Not oFSO.FileExists(sAudioProgramPath) Then
        ' Check for them and cancel, if none of them were found
        If Not checkForAudioChangeProgram Then Exit Sub
    End If

    sLatestAction = "Searching for SourceTracks"

    ' Get the paths of all Tracks with "_Normal" Suffix
    sPathNormal = getPathsAllTracks("_Normal")

    ' Tracks found
    If Not Not sPathNormal Then

        ' Generate the currently used paths (No Suffix)

        ReDim sPathUsed(UBound(sPathNormal))

        For i = 0 To UBound(sPathNormal)
            sPathUsed(i) = Left$(sPathNormal(i), Len(sPathNormal(i)) - 11) & ".pcm"
        Next

    Else ' No Tracks with "_Normal" Suffix found (Meaning the normal tracks are currently being used)

        ' Get the currently used tracks
        sPathUsed = getPathsAllTracks

        ' Cancel if no Paths found
        If Not CBool(Not Not sPathUsed) Then
            Call VBA.Err.Raise(Number:=517, Description:="No PCM-Files were found!")
            Exit Sub
        End If

        ' In this case both Arrays are the same
        sPathNormal = sPathUsed
    End If

    ' Only needed, when using other programs than MSUPCM++
    If eAudioProgramUsed <> eAudioProgram.apMSUPCM Then

        sLatestAction = "Generating Temp-Paths for FFMPEG or SoX"

        ReDim sPathTemp(UBound(sPathUsed))
        ReDim sPathTempConv(UBound(sPathUsed))

        ' Generate Temp-Paths for each file
        For i = 0 To UBound(sPathUsed)
            sPathTemp(i) = oFSO.BuildPath(oFSO.GetParentFolderName(sPathUsed(i)), oFSO.GetBaseName(sPathUsed(i)) & "_TEMP_NoHeader.raw")
            sPathTempConv(i) = oFSO.BuildPath(oFSO.GetParentFolderName(sPathUsed(i)), oFSO.GetBaseName(sPathUsed(i)) & "_TEMP_NoHeader_" & iSampleRate & ".raw")
        Next
    End If
    ReDim sPathOut(UBound(sPathUsed))

    sLatestAction = "Generating Out-Paths"

    ' Generate Out-Paths for each file
    For i = 0 To UBound(sPathUsed)
        sPathOut(i) = oFSO.BuildPath(oFSO.GetParentFolderName(sPathUsed(i)), oFSO.GetBaseName(sPathUsed(i)) & "_" & iSampleRate & ".pcm")
    Next

    ' Convert the Files
    If eAudioProgramUsed = eAudioProgram.apMSUPCM Then

        sLatestAction = "Trying to convert the PCM files via MSUPCM++"

        ' Use MSUPCM++
        For i = 0 To UBound(sPathUsed)

            Let sShell = _
                GenerateShellAudioResample( _
                    sPathNormal(i), _
                    sPathOut(i), _
                    iSampleRate, _
                    iVolumePercent, _
                    sLatestAction)

            ' Starts the conversion process of the pcm directly to the output pcm
            Call changeSpeedExec(sShell, oFSO.GetFileName(sPathNormal(i)))

        Next
    Else

        sLatestAction = "Removing MSU1 Header before converting the audio"

        ' Use FFMPEG or SoX
        For i = 0 To UBound(sPathUsed)

            sLatestAction = "Removing MSU1 Header of " & sChr34 & sPathNormal(i) & sChr34 & " and writing it to " & sChr34 & sPathTemp(i) & sChr34

            ' Removes the MSU1 Specific Header
            Call removeMSU1Header( _
            sPathNormal(i), _
            sPathTemp(i))

            Let sShell = GenerateShellAudioResample(sPathTemp(i), sPathTempConv(i), iSampleRate, iVolumePercent, sLatestAction)

            ' Starts the conversion process of the headerless audiofile
            Call changeSpeedExec(sShell, oFSO.GetFileName(sPathNormal(i)))

        Next
    End If

    ' Waits until all batch processes are done
    Call mod_ShellWait.ShellMultiFinish

    If eAudioProgramUsed = eAudioProgram.apMSUPCM Then

        ' Replace header of the converted PCM files (LoopPoint is 0, when created by MSUPCM++)
        For i = 0 To UBound(sPathUsed)

            If oFSO.FileExists(sPathNormal(i)) Then
                ' Calculate New LoopPoint
                dLoopPoint = getLoopPointFromPCM(sPathNormal(i), sLatestAction) * (44100 / iSampleRate)

                sLatestAction = "Editing the MSU1 Header of " & sChr34 & sPathOut(i) & sChr34 & " to the calculated LoopPoint " & dLoopPoint

                Call editMSSU1Header( _
                sPathOut(i), _
                dLoopPoint)
            End If
        Next
    Else
        ' Add the Header to all converted Files
        For i = 0 To UBound(sPathUsed)

            ' Calculate new LoopPoint
            dLoopPoint = getLoopPointFromPCM(sPathNormal(i), sLatestAction) * (44100 / iSampleRate)

            sLatestAction = "Copying RAW-File " & sChr34 & sPathTempConv(i) & sChr34 & " to " & sPathOut(i) & " with adding the MSU1 Header including the LoopPoint " & dLoopPoint

            ' Adds the MSU1 Specific Header
            Call addMSU1HeaderToRaw( _
            sPathTempConv(i), _
            sPathOut(i), _
            dLoopPoint)

        Next

        ' Delete the Temp-Files
        For i = 0 To UBound(sPathUsed)
            sLatestAction = "Deleting temporary File " & sChr34 & sPathTemp(i) & sChr34
            Call oFSO.DeleteFile(sPathTemp(i), True)
            sLatestAction = "Deleting temporary File " & sChr34 & sPathTempConv(i) & sChr34
            Call oFSO.DeleteFile(sPathTempConv(i), True)
        Next
    End If

    sLatestAction = "Switching Converted Files with the Source Files"

    ' Switch to the converted versions
    Call renameSwitchFilesBulk(sPathUsed, sPathOut, "_Normal", , sLatestAction)

    sLatestAction = "Successfully converted All Files (This message should NOT be seen!)"

    Let mod_MAS_Functions.IsResampled = True
End Sub

Private Sub changeSpeedExec(ByRef sShell As String, ByRef sPcmFileName As String)
    Dim sParams As String
    Dim sCmdRunType As String * 2
    Dim eShowOpt As nCmdShowValues

    Let sCmdRunType = "/C"

    If mod_MAS_Functions.CmdShowWindows Then
        Let eShowOpt = nCmdShowValues.SW_SHOWNOACTIVATE

        If mod_MAS_Functions.CmdKeepWindowsOpened Then
            Let sCmdRunType = "/K"
        End If
    Else
        Let eShowOpt = nCmdShowValues.SW_HIDE
    End If

    Let sParams = _
            sCmdRunType & sChr32 & _
            sChr34 & "ECHO Converting """ & sPcmFileName & sChr34 & " via " & EnumAudioProgramToStr(eAudioProgramUsed) & "  && " _
            & sShell & sChr34 & " && " & _
            "ECHO Finished"

    Call mod_ShellWait.ShellMulti( _
            sFile:=mod_MAS_Functions.PathCmdExe, _
            sParams:=sParams, _
            eShowOpt:=eShowOpt, _
            hWnd:=mod_MAS_Functions.hWnd)

End Sub

' Deletes the Resampled Files and move the 'Normal' Files back to their original position
Public Sub changeSpeedBack()
    Dim sPathNormal() As String
    Dim sPathDest() As String
    Dim i As Integer

    sPathNormal = getPathsAllTracks("_Normal")

    ' Cancel, if no Tracks found with the Normal suffix
    If Not CBool(Not Not sPathNormal) Then Exit Sub

    ReDim sPathDest(UBound(sPathNormal)) As String

    ' Generate Destination-Path
    For i = 0 To UBound(sPathNormal)
        sPathDest(i) = Left$(sPathNormal(i), InStrRev(sPathNormal(i), "_Normal") - 1) & ".pcm"
    Next

    Call renameSwitchFilesBulk(sPathDest, sPathNormal)

    Let mod_MAS_Functions.IsResampled = False
End Sub

' Adds the Suffix to Path1 (If no Suffix is stated, Path1 gets deleted instead)
' Moves Path2 to what has been Path1 before
Private Sub renameSwitchFilesBulk( _
ByRef sPath1() As String, _
ByRef sPath2() As String, _
Optional ByVal sPath1Suffix As String = vbNullString, _
Optional ByVal bOverwrite As Boolean = False, _
Optional ByRef sLatestAction As String)
    On Error Resume Next
    Dim i As Integer
    Dim bNotAllSwitched As Boolean
    Dim bDeletePath1 As Boolean

    ' Delete Path1, if there is no suffix
    If LenB(sPath1Suffix) = 0 Then bDeletePath1 = True

SwitchFiles:

    ' Switch out the Normal Files and the Resampled Files
    For i = 0 To UBound(sPath1)

        ' If file is not already switched
        If Not sPath2(i) = vbNullString Then

            ' Go to next file, if the to be switched file does not exist
            If Not oFSO.FileExists(sPath2(i)) Then
                sPath2(i) = vbNullString
                GoTo SwitchNextFile
            End If

            ' If File deletion flag is set
            If bDeletePath1 Then
                sLatestAction = "Deleting File " & sChr34 & sPath1(i) & sChr34

                ' Delete the File
                Call oFSO.DeleteFile(sPath1(i))

                ' Skip, if File is Opened
                If Err = 70 Then Err.Clear: GoTo SkipFile
            Else

                sLatestAction = "Renaming File " & sChr34 & oFSO.GetFileName(sPath1(i)) & sChr34 & " to " & (oFSO.GetBaseName(sPath1(i)) & sPath1Suffix & ".pcm") & vbNewLine & "(Inside Folder " & sChr34 & oFSO.GetParentFolderName(sPath1(i)) & sChr34 & ")"

                ' Rename the SourceFile
                Call oFSO.MoveFile(sPath1(i), oFSO.BuildPath(oFSO.GetParentFolderName(sPath1(i)), oFSO.GetBaseName(sPath1(i)) & sPath1Suffix & ".pcm"))

                ' Skip, if File is Opened
                If Err = 70 Then Err.Clear: GoTo SkipFile

                ' If version with Suffix already exists
                If Err = 58 Then
                    Err.Clear

                    If bOverwrite Then
                        sLatestAction = "Deleting File " & sChr34 & oFSO.BuildPath(oFSO.GetParentFolderName(sPath1(i)), oFSO.GetBaseName(sPath1(i)) & sPath1Suffix & ".pcm") & sChr34

                        ' Delete the file, that already has the suffix
                        Call oFSO.DeleteFile(oFSO.BuildPath(oFSO.GetParentFolderName(sPath1(i)), oFSO.GetBaseName(sPath1(i)) & sPath1Suffix & ".pcm"))
                        GoTo SkipFile
                    Else
                        sLatestAction = "Deleting File " & sChr34 & sPath1(i) & sChr34

                        ' Delete the 'to be switched' File
                        Call oFSO.DeleteFile(sPath1(i))
                    End If

                    ' Skip, if File is Opened
                    If Err = 70 Then Err.Clear: GoTo SkipFile
                End If
            End If

            sLatestAction = "Renaming File " & sChr34 & oFSO.GetFileName(sPath2(i)) & sChr34 & " to " & oFSO.GetFileName(sPath1(i)) & vbNewLine & "(Inside Folder " & sChr34 & oFSO.GetParentFolderName(sPath1(i)) & sChr34 & ")"

            ' Rename the Converted File
            Call oFSO.MoveFile(sPath2(i), sPath1(i))

            ' Skip, if File is Opened
            If Err = 70 Then Err.Clear: GoTo SkipFile

            ' Mark the Output-File as already switched
            sPath2(i) = vbNullString

        End If
SwitchNextFile:

    Next

    ' If at least one file could not be switched
    If bNotAllSwitched Then
        ' Wait one second before trying again
        Call mod_ShellWait.Sleep(1000)
        bNotAllSwitched = False
        GoTo SwitchFiles
    End If


    Exit Sub

SkipFile:
    ' Sets Flag to Try Again
    bNotAllSwitched = True
    GoTo SwitchNextFile
End Sub

' Returns the Paths of all existing valid trackpaths
Private Function getPathsAllTracks(Optional ByRef sSuffix As String = vbNullString) As String()
    On Error Resume Next
    Dim iTrackId As Byte
    Dim sCheck As String
    Dim sPath() As String
    Dim i As Integer

    ' Check all Tracks
    Do
        ' Generate Path of Current ID
        sCheck = oFSO.BuildPath(sMSUfolder, sMSUname & "-" & iTrackId & sSuffix & ".pcm")

        ' Add the Path to the Array, if it exists
        If oFSO.FileExists(sCheck) Then
            ReDim Preserve sPath(i)
            sPath(i) = sCheck
            i = i + 1
        End If

        ' Next ID
        If iTrackId <> 255 Then iTrackId = iTrackId + 1 Else Exit Do
    Loop

    ' Alt. Tracks exist
    If Not Not iTracksWithAlts Then

        Dim iSelected As Integer
        Dim j As Byte
        Dim k As Integer
        Dim sAlts() As String

        ' Check all Alt. Tracks
        Do
            sAlts = mod_MAS_Functions.getAltTrackList(iTracksWithAlts(j), iSelected, sSuffix)

            ' Alt. Tracks for that ID found
            If Not Not sAlts Then

                ' Go through all Alt. Tracks of the current ID
                For k = 0 To UBound(sAlts)

                    ' If Track is not the currently used Track for that ID
                    If k <> iSelected Then

                        ' Add the Filepath to the Array
                        ReDim Preserve sPath(i)
                        sPath(i) = oFSO.BuildPath(oFSO.BuildPath(oFSO.BuildPath(oFSO.BuildPath(sMSUfolder, sAltFolder), sMSUname & "-" & iTracksWithAlts(j)), sAlts(k)), sMSUname & "-" & iTracksWithAlts(j) & sSuffix & ".pcm")
                        i = i + 1

                    End If
                Next
            End If

            If j <> UBound(iTracksWithAlts) Then j = j + 1 Else Exit Do
        Loop

        Erase sAlts
    End If

    getPathsAllTracks = sPath
End Function

Private Function RenameAltTrack(ByVal iTrackId As Byte, ByVal sTrackNameOld As String, ByVal sTrackNameNew As String)
    On Error Resume Next
    Dim sIDaltPath As String
    Dim bErr As Boolean

    ' Alt. Path for the TrackId
    sIDaltPath = oFSO.BuildPath(oFSO.BuildPath(sMSUfolder, sAltFolder), sMSUname & "-" & iTrackId)

RenameFolder:
    ' Renames the Folder for the Alt. Track with this TrackId
    Call oFSO.MoveFolder(oFSO.BuildPath(sIDaltPath, sTrackNameOld), oFSO.BuildPath(sIDaltPath, sTrackNameNew))

    ' If Folder already exists on first try
    If Err = 58 And Not bErr Then
        ' Delete that Folder
        Call oFSO.DeleteFolder(oFSO.BuildPath(sIDaltPath, sTrackNameNew), True)

        bErr = True

        ' Try Again
        GoTo RenameFolder
    End If

End Function

' Automatic Switching of Track 52 (Win) and 53 (Lose / Death) for DKC3
' Looks for opened tracks and switches accordingly
Public Function DKC3autoSwitch()

    Select Case True

        ' Big Boss Blues
        Case isTrackOpen(26), isTrackOpen(39):
            Call switchTracksAuto(53, "Big Boss Blues Death")
            Call switchTracksAuto(52, "Big Boss Blues Win")

        ' Boss Boogie
        Case isTrackOpen(12), isTrackOpen(31), isTrackOpen(40):
            Call switchTracksAuto(53, "Boss Boogie Death")
            Call switchTracksAuto(50, "Boss Boogie Death")
            Call switchTracksAuto(49, "Boss Boogie Win")

        ' Cascade Capers
        Case isTrackOpen(10):
            Call switchTracksAuto(53, "Cascade Capers Death")
            Call switchTracksAuto(52, "Cascade Capers Win")

        ' Cavern Caprice
        Case isTrackOpen(30), isTrackOpen(41):
            Call switchTracksAuto(53, "Cavern Caprice Death")
            Call switchTracksAuto(52, "Cavern Caprice Win")

        ' Enchanted Riverbank
        Case isTrackOpen(16):
            Call switchTracksAuto(53, "Enchanted Riverbank Death")
            Call switchTracksAuto(52, "Enchanted Riverbank Win")

        ' Frosty Frolics
        Case isTrackOpen(23):
            Call switchTracksAuto(53, "Frosty Frolics Death")
            Call switchTracksAuto(52, "Frosty Frolics Win")

        ' Hot Pursuit
        Case isTrackOpen(7):
            Call switchTracksAuto(53, "Hot Pursuit Death")
            Call switchTracksAuto(52, "Hot Pursuit Win")

        ' Bonus Time!
        Case isTrackOpen(1), isTrackOpen(2), isTrackOpen(3):
            Call switchTracksAuto(50, "Bonus Time! Lose")
            Call switchTracksAuto(51, "Bonus Time! Win")
            Call switchTracksAuto(49, "Bonus Time!")

        ' Jangle Bells
        Case isTrackOpen(13), isTrackOpen(43), isTrackOpen(44), isTrackOpen(45):
            Call switchTracksAuto(50, "Jangle Bells Lose")
            Call switchTracksAuto(51, "Jangle Bells Win")
            Call switchTracksAuto(49, "Jangle Bells")

        ' Jungle Jitter
        Case isTrackOpen(29):
            Call switchTracksAuto(53, "Jungle Jitter Death")
            Call switchTracksAuto(52, "Jungle Jitter Win")

        ' Mill Fever
        Case isTrackOpen(14):
            Call switchTracksAuto(53, "Mill Fever Death")
            Call switchTracksAuto(52, "Mill Fever Win")

        ' Nuts & Bolts
        Case isTrackOpen(8), isTrackOpen(38):
            Call switchTracksAuto(53, "Nuts & Bolts Death")
            Call switchTracksAuto(52, "Nuts & Bolts Win")

        ' Pokey Pipes
        Case isTrackOpen(25):
            Call switchTracksAuto(53, "Pokey Pipes Death")
            Call switchTracksAuto(52, "Pokey Pipes Win")

        ' Rocket Run
        Case isTrackOpen(28):
            Call switchTracksAuto(53, "Rocket Run Death")
            Call switchTracksAuto(52, "Rocket Run Win")

        ' Rockface Rumble
        Case isTrackOpen(33):
            Call switchTracksAuto(53, "Rockface Rumble Death")
            Call switchTracksAuto(52, "Rockface Rumble Win")

        ' Stilt Village
        Case isTrackOpen(15):
            Call switchTracksAuto(53, "Stilt Village Death")
            Call switchTracksAuto(52, "Stilt Village Win")

        ' Treetop Tumble
        Case isTrackOpen(11):
            Call switchTracksAuto(53, "Treetop Tumble Death")
            Call switchTracksAuto(52, "Treetop Tumble Win")

        ' Water World
        Case isTrackOpen(6):
            Call switchTracksAuto(53, "Water World Death")
            Call switchTracksAuto(52, "Water World Win")
    End Select
End Function

' Like switchTracks, but the old track is unknown and no errors get displayed
Public Sub switchTracksAuto(ByVal iTrackId As Byte, ByVal sTrackNameNew As String)

    ' Don't Switch, if Track is already in Use
    If IsTrackInUse(oFSO.BuildPath(oFSO.BuildPath(oFSO.BuildPath(sMSUfolder, sAltFolder), sMSUname & "-" & iTrackId), sTrackNameNew), iTrackId) Then
        Exit Sub
    End If

    Dim iSelected As Integer
    Dim sTracks() As String

    ' Gets the Tracklist of the selected TrackId
    sTracks = getAltTrackList(iTrackId, iSelected)

    Call switchTracks(iTrackId, sTracks(iSelected), sTrackNameNew, False)

    ' Track 52 gets copied to 46 (DKC3 specific)
    If iTrackId = 52 Then
        Call mod_MAS_Functions.copyTrack(52, 46)
    End If

End Sub

' Moves the Old / Current File to the Alt. Folder
' And the New File to the MSU1-Path
Public Function switchTracks(ByVal iTrackId As Byte, ByVal sTrackNameOld As String, ByVal sTrackNameNew As String, Optional ByVal bShowError As Boolean = True) As Boolean
    On Error Resume Next

    ' Leave Function, if the old Track is the same as the new Track
    ' Or if either of the Strings are empty
    If sTrackNameOld = sTrackNameNew Or sTrackNameOld = Empty Or sTrackNameNew = Empty Then Exit Function

    Dim sTrackBaseName As String ' TrackId with Prefix
    Dim sTrackFileName As String ' Filename of Track

    Dim sIDaltPath As String     ' Folderpath, that contains all Alt. Tracks for the current ID

    Dim sCurTrackPath As String  ' Path of the currently selected Track for the current ID
    Dim sCurTrackAltFolderPath As String ' Alt. Folderpath of the current currently selected Track for the current ID
    Dim sNewTrackAltFolderPath As String ' Alt. Folderpath of the to be selected Track for the current ID

    ' Fill all the Strings with the above described contents
    sTrackBaseName = sMSUname & "-" & iTrackId
    sTrackFileName = sTrackBaseName & ".pcm"

    sIDaltPath = oFSO.BuildPath(oFSO.BuildPath(sMSUfolder, sAltFolder), sTrackBaseName)

    sCurTrackPath = oFSO.BuildPath(sMSUfolder, sTrackFileName)

    sCurTrackAltFolderPath = oFSO.BuildPath(sIDaltPath, sTrackNameOld)
    sNewTrackAltFolderPath = oFSO.BuildPath(sIDaltPath, sTrackNameNew)

    ' Checking if all of these Paths exist
    ' Throw error message and leave sub, if one of them does not exist
    ' Or create the path, if possible

    If Not oFSO.FileExists(sCurTrackPath) Then

        If bShowError Then Call MsgBox("The File """ & sTrackFileName & """ is NOT in the MSU1-Folder!" & vbNewLine & vbNewLine & "(" & sCurTrackPath & ")", vbCritical)
        Exit Function

    End If

    If Not createFolderPathIfMissing(sCurTrackAltFolderPath) Then

        If bShowError Then Call MsgBox("The Alt. Folder for """ & sTrackNameOld & """ could not be created!" & vbNewLine & vbNewLine & "(" & sCurTrackAltFolderPath & ")" & vbNewLine & vbNewLine & "VB Error " & Err.Number & ":" & vbNewLine & Err.Description, vbCritical)
        Exit Function

    End If

    If Not oFSO.FolderExists(sNewTrackAltFolderPath) Then

        If bShowError Then Call MsgBox("The Alt. Folder for """ & sTrackNameNew & """ does not exist!" & vbNewLine & vbNewLine & "(" & sNewTrackAltFolderPath & ")", vbCritical)
        Exit Function

    End If

    If Not oFSO.FileExists(oFSO.BuildPath(sNewTrackAltFolderPath, sTrackFileName)) Then

        If bShowError Then Call MsgBox("The File """ & sTrackFileName & """ for Track """ & sTrackNameNew & """ is missing in the Alt. Folder!" & vbNewLine & vbNewLine & "(" & oFSO.BuildPath(sNewTrackAltFolderPath, sTrackFileName) & ")", vbCritical)
        Exit Function

    End If

    Err.Clear

    ' Put the Current / Old Track to it's Alt. Folder
    Call oFSO.MoveFile(sCurTrackPath, oFSO.BuildPath(sCurTrackAltFolderPath, sTrackFileName))

    ' If tracks are resampled
    If mod_MAS_Functions.IsResampled Then
        ' And the 'normal' version exists
        If oFSO.FileExists(oFSO.BuildPath(oFSO.GetParentFolderName(sCurTrackPath), oFSO.GetBaseName(sTrackFileName) & "_Normal.pcm")) Then
             ' Do the Same for the 'Normal' version
            Call oFSO.MoveFile(oFSO.BuildPath(oFSO.GetParentFolderName(sCurTrackPath), oFSO.GetBaseName(sTrackFileName) & "_Normal.pcm"), oFSO.BuildPath(sCurTrackAltFolderPath, oFSO.GetBaseName(sTrackFileName) & "_Normal.pcm"))
        End If
    End If

    ' Handle Error
    If Err <> 0 Then

        If Err = 70 Then

            If bShowError Then Call MsgBox("The Current Track """ & sTrackNameOld & """ is in Use!", vbExclamation)

        Else

            If bShowError Then Call MsgBox("Error while moving the old Track """ & sTrackNameOld & """ to the Alt. Folder" & vbNewLine & vbNewLine & "VB Error " & Err.Number & ":" & vbNewLine & Err.Description, vbCritical)

        End If
        Exit Function
    End If

    ' Put the new Track to the Current Folder
    Call oFSO.MoveFile(oFSO.BuildPath(sNewTrackAltFolderPath, sTrackFileName), sCurTrackPath)

    ' If tracks are resampled
    If mod_MAS_Functions.IsResampled Then
        ' And the 'normal' version exists
        If oFSO.FileExists(oFSO.BuildPath(sNewTrackAltFolderPath, oFSO.GetBaseName(sTrackFileName) & "_Normal.pcm")) Then
             ' Do the Same for the 'Normal' version
            Call oFSO.MoveFile(oFSO.BuildPath(sNewTrackAltFolderPath, oFSO.GetBaseName(sTrackFileName) & "_Normal.pcm"), oFSO.BuildPath(oFSO.GetParentFolderName(sCurTrackPath), oFSO.GetBaseName(sTrackFileName) & "_Normal.pcm"))
        End If
    End If

    ' Handle Error
    If Err <> 0 Then

        ' Put the Current / Old Track back
        Call oFSO.MoveFile(oFSO.BuildPath(sCurTrackAltFolderPath, sTrackFileName), sCurTrackPath)

        ' If tracks are resampled
        If mod_MAS_Functions.IsResampled Then
            ' And the 'normal' version exists
            If oFSO.FileExists(oFSO.BuildPath(sCurTrackAltFolderPath, oFSO.GetBaseName(sTrackFileName) & "_Normal.pcm")) Then
                 ' Do the Same for the 'Normal' version
                Call oFSO.MoveFile(oFSO.BuildPath(sCurTrackAltFolderPath, oFSO.GetBaseName(sTrackFileName) & "_Normal.pcm"), oFSO.BuildPath(oFSO.GetParentFolderName(sCurTrackPath), oFSO.GetBaseName(sTrackFileName) & "_Normal.pcm"))
            End If
        End If

        If bShowError Then Call MsgBox("Error while moving the new Track """ & sTrackNameNew & """ from the Alt. Folder" & vbNewLine & vbNewLine & "VB Error " & Err.Number & ":" & vbNewLine & Err.Description, vbCritical)

        Exit Function

    End If

    ' Move the File CurrentlyUsed.txt to the Alt. Folder of the new Track
    If oFSO.FileExists(oFSO.BuildPath(sCurTrackAltFolderPath, "CurrentlyUsed.txt")) Then

        Call oFSO.MoveFile(oFSO.BuildPath(sCurTrackAltFolderPath, "CurrentlyUsed.txt"), oFSO.BuildPath(sNewTrackAltFolderPath, "CurrentlyUsed.txt"))

        If Err Then
            Call oFSO.CreateTextFile(oFSO.BuildPath(sNewTrackAltFolderPath, "CurrentlyUsed.txt"))
            Err.Clear
        End If
    Else

        Call oFSO.CreateTextFile(oFSO.BuildPath(sNewTrackAltFolderPath, "CurrentlyUsed.txt"))

    End If

    ' Switching Successful
    switchTracks = True

    Debug.Print "Switched Track " & iTrackId & " from """ & sTrackNameOld & """ to """ & sTrackNameNew & """ succesfully"

End Function

' Copies a Track to Another Track
Private Sub copyTrack(ByVal iTrackIdFrom As Byte, ByVal iTrackIdTo As Byte)
    On Error Resume Next
    Call oFSO.CopyFile(Source:=oFSO.BuildPath(sMSUfolder, sMSUname & "-" & iTrackIdFrom & ".pcm"), _
                  Destination:=oFSO.BuildPath(sMSUfolder, sMSUname & "-" & iTrackIdTo & ".pcm"), _
                  OverWriteFiles:=True)
    Debug.Print "Copied Track " & iTrackIdFrom & " to " & iTrackIdTo
End Sub

' Simple Bubblesort
Private Sub sortNumArray(ByRef iNum() As Byte)
    On Error Resume Next
    Dim i As Byte
    Dim iSave As Byte
    Dim bFlag As Boolean

    ' Cancel sorting, if there is only one value in the array
    If UBound(iNum) = 0 Then Exit Sub

    Do
        bFlag = False
        For i = 0 To UBound(iNum) - 1

            If Err Then Exit Sub

            If iNum(i) > iNum(i + 1) Then
                bFlag = True
                iSave = iNum(i)
                iNum(i) = iNum(i + 1)
                iNum(i + 1) = iSave
            End If

        Next
    Loop While bFlag

End Sub

' Checks if the Folderpath exists
' And creates it, if not
Private Function createFolderPathIfMissing(ByVal sPath As String) As Boolean
    On Error Resume Next
    Dim sPaths() As String ' Array that contains sPath and all parent paths of it
    Dim i As Integer       ' Current position in the Array sPaths()
    Dim strTmp As String   ' Current selected Path of the Array sPaths()

    If sPath = Empty Then Exit Function

    Err.Clear

    ' If the Folderpath of sPath does not exist
    If Not oFSO.FolderExists(sPath) Then

        ' Put sPath in the first Array value
        ReDim sPaths(0)
        sPaths(0) = sPath

        ' Read first parent path of sPath
        strTmp = oFSO.GetParentFolderName(sPath)

        ' Save all parent paths in the array
        Do Until strTmp = Empty
            i = i + 1
            ReDim Preserve sPaths(i)
            sPaths(i) = strTmp
            strTmp = oFSO.GetParentFolderName(strTmp)
        Loop

        ' Go through the array
        For i = i To 0 Step -1

            ' If the current Folder in the array does not exist
            If Not oFSO.FolderExists(sPaths(i)) Then

                ' Create the Folder
                Call oFSO.CreateFolder(sPaths(i))

            End If
        Next
    End If

    ' Return true, if no error occurred
    If Not Err Then
        createFolderPathIfMissing = True
    End If
End Function

' Converts a Hex Number to an unsigned Integer
' Has to use Double since Long only goes Up To 7FFFFFFF (2147483647) and the theoretical maximum is FFFFFFFF
Private Function Hex2UInt(h As String) As Double
    ' https://stackoverflow.com/questions/40213758/convert-hex-string-to-unsigned-int-vba

    ' Convert Hex to Double
    Hex2UInt = CDbl("&h" & h)

    ' If Number is negative
    If Hex2UInt < 0 Then
        ' Convert signed negative to unsigned positive
        Hex2UInt = CDbl("&h1" & h) - 4294967296#
    End If
End Function

Public Function setAltFolder(ByVal sFolderName As String)
    sAltFolder = sFolderName
End Function

Public Function getAltFolder(ByVal sFolderName As String)
    getAltFolder = sAltFolder
End Function

Public Function getMSUFolder() As String
    getMSUFolder = sMSUfolder
End Function

Public Function getMSUname() As String
    getMSUname = sMSUname
End Function

Public Function getCurSelAltTrack() As String
    getCurSelAltTrack = sCurSelAltTrack
End Function

Public Function getTracksWithAlts() As Byte()
    getTracksWithAlts = iTracksWithAlts
End Function

Public Property Get PathCmdExe() As String
    Static sPathCmd As String

    Select Case LenB(sPathCmd)
        Case 0
            Let sPathCmd = FindExecutable("CMD")
    End Select

    Let PathCmdExe = sPathCmd
End Property


' Puts the whole array into the Debug-Window
Public Sub PrintStringArray(ByRef sArray() As String)
    Dim i As Integer

    For i = 0 To UBound(sArray)
        Debug.Print Format(i, "000") & ":" & vbTab & sArray(i)
    Next
End Sub

Public Sub PrintIntArray(ByRef sArray() As Integer)
    Dim i As Integer

    For i = 0 To UBound(sArray)
        Debug.Print Format(i, "000") & ":" & vbTab & sArray(i)
    Next
End Sub

Public Sub PrintByteArray(ByRef sArray() As Byte)
    Dim i As Integer

    For i = 0 To UBound(sArray)
        Debug.Print Format(i, "000") & ":" & vbTab & sArray(i)
    Next
End Sub
