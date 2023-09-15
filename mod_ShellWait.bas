Attribute VB_Name = "mod_ShellWait"
Option Explicit

' Source:
' https://stackoverflow.com/questions/2228410/vb6-how-to-run-a-program-from-vb6-and-close-it-once-it-finishes

'--- for WaitForXxx
Private Const INFINITE As Long = &HFFFFFFFF
'--- for WaitForXObjects
Private Const WAIT_FAILED As Long = &HFFFFFFFF
'--- for GetExitCodeProcess
Private Const STILL_ACTIVE As Integer = &H103

Private Const PROCESS_QUERY_INFORMATION As Integer = &H400

Public Const MAXIMUM_WAIT_OBJECTS As Byte = 64

Private Declare Function ShellExecuteExW Lib "shell32.dll" (lpExecInfoW As SHELLEXECUTEINFOW) As Long
Private Declare Function OpenProcess Lib "kernel32" (ByVal dwDesiredAccess As Long, ByVal bInheritHandle As Boolean, ByVal dwProcessId As Long) As Long
Private Declare Function WaitForSingleObject Lib "kernel32" (ByVal hHandle As Long, ByVal dwMilliseconds As Long) As Long
Private Declare Function WaitForMultipleObjects Lib "kernel32" (ByVal nCount As Long, lpHandles As Long, ByVal bWaitAll As Long, ByVal dwMilliseconds As Long) As Long
Private Declare Function GetExitCodeProcess Lib "kernel32" (ByVal hProcess As Long, lpExitCode As Long) As Long
Private Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Long) As Long
Private Declare Function GetLastError Lib "kernel32" () As Long
Public  Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)

' https://docs.microsoft.com/en-us/windows/win32/api/shellapi/ns-shellapi-shellexecuteinfoa
Private Enum fMaskValues
    SEE_MASK_DEFAULT = &H0
    SEE_MASK_CLASSNAME = &H1
    SEE_MASK_CLASSKEY = &H3
    SEE_MASK_IDLIST = &H4
    SEE_MASK_INVOKEIDLIST = &HC
    SEE_MASK_ICON = &H10
    SEE_MASK_HOTKEY = &H20
    SEE_MASK_NOCLOSEPROCESS = &H40
    SEE_MASK_CONNECTNETDRV = &H80
    SEE_MASK_NOASYNC = &H100
    SEE_MASK_FLAG_DDEWAIT = &H100
    SEE_MASK_DOENVSUBST = &H200
    SEE_MASK_FLAG_NO_UI = &H400
    SEE_MASK_UNICODE = &H4000
    SEE_MASK_NO_CONSOLE = &H8000
    SEE_MASK_ASYNCOK = &H100000
    SEE_MASK_NOQUERYCLASSSTORE = &H1000000
    SEE_MASK_HMONITOR = &H200000
    SEE_MASK_NOZONECHECKS = &H800000
    SEE_MASK_WAITFORINPUTIDLE = &H2000000
    SEE_MASK_FLAG_LOG_USAGE = &H4000000
    SEE_MASK_FLAG_HINST_IS_SITE = &H8000000
End Enum

' https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow
Public Enum nCmdShowValues
    SW_HIDE = VbAppWinStyle.vbHide
    SW_SHOWNORMAL = VbAppWinStyle.vbNormalFocus
    SW_SHOWMINIMIZED = VbAppWinStyle.vbMinimizedFocus
    SW_SHOWMAXIMIZED = VbAppWinStyle.vbMaximizedFocus
    SW_SHOWNOACTIVATE = VbAppWinStyle.vbNormalNoFocus
    SW_SHOW = 5
    SW_MINIMIZE = VbAppWinStyle.vbMinimizedNoFocus
    SW_SHOWMINNOACTIVE = 7
    SW_SHOWNA = 8
    SW_RESTORE = 9
    SW_SHOWDEFAULT = 10
    SW_FORCEMINIMIZE = 11
End Enum

Private Type SHELLEXECUTEINFOW
    cbSize As Long
    fMask As Long
    hWnd As Long
    lpVerb As Long       ' String
    lpFile As Long       ' String
    lpParameters As Long ' String
    lpDirectory As Long  ' String
    nShow As Long
    '  Optional fields
    hInstApp As Long
    lpIDList As Long
    lpClass As Long      ' String
    hkeyClass As Long
    dwHotKey As Long
    hIcon As Long
    hProcess As Long
End Type

Private iHandles() As Long

' Anzahl an Threads ausgeben
Public Property Get Threads() As Byte
    If Not Not iHandles Then
        Threads = UBound(iHandles) + 1
    End If
End Property

' Anzahl an Threads setzen
Public Property Let Threads(ByRef iSet As Byte)

    ' Angegebenen Wert auf Gültigkeit überprüfen
    Select Case iSet
        Case 0
            Exit Property
        Case Is > MAXIMUM_WAIT_OBJECTS
            Exit Property
    End Select

    ' Wenn Array für Handles leer
    If (Not Not iHandles) = 0 Then

        ' Arraygröße festlegen
        ReDim iHandles(iSet - 1)

    ' Arraygröße weicht von der angegebenen Threadanzahl ab
    ElseIf UBound(iHandles()) <> (iSet - 1) Then

        ' Arraygröße anpassen
        ReDim Preserve iHandles(iSet - 1)

    End If
End Property

' Warte, bis ein Thread frei wird
' Starte Thread und füg das Handle zum Array hinzu
Public Sub ShellMulti( _
    ByVal sFile As String, _
    Optional sParams As String = vbNullString, _
    Optional eShowOpt As nCmdShowValues = nCmdShowValues.SW_RESTORE, _
    Optional hWnd As Long = 0)

    Dim iHandle As Long
    Dim iRet As Long

    ' Wenn Array nicht initialisiert
    If (Not Not iHandles) = 0 Then

        ' Standardmäßig auf 4 Threads stellen
        Let mod_ShellWait.Threads = 4
    End If

    ' Warten, bis ein Thread frei wird
    iRet = WaitForMultipleObjects(UBound(iHandles) + 1, iHandles(0), False, INFINITE)

    ' Wenn fehlgeschlagen (Maximale Anzahl der Threads ist noch nicht erreicht)
    If iRet = WAIT_FAILED Then

        ' Erste freie Position raussuchen
        Let iRet = getFirstAvailableHandlePos

    End If

    ' Prozess starten (Handle speichern)
    Let iHandle = StartProcess(sFile, sParams, eShowOpt, hWnd)

    ' Wenn vorheriger Wert bereits ein Handle ist
    If iHandles(iRet) <> 0 Then

        ' Handle schließen, da der Prozess beendet wurde
        Call CloseHandle(iHandles(iRet))
    End If

    ' Handle vom neuen Prozess hinzufügen
    Let iHandles(iRet) = iHandle
End Sub

' Warte bis die Prozesse hinter allen Handles abgeschlossen sind
Public Sub ShellMultiFinish()
    If (Not Not iHandles) = 0 Then Exit Sub
    Dim iRet As Long
    Dim nCount As Byte

    ' Erste freie Position auslesen
    ' Benötigt, wenn nicht komplettes Array mit aktiven Handles befüllt ist
    Let nCount = getFirstAvailableHandlePos

    ' Keine Position frei
    ' Gesamtes Array hat aktive Handles
    If nCount = &HFF Or nCount = 0 Then
        nCount = UBound(iHandles) + 1
    End If

    ' Warten, bis alle Threads fertig sind
    Let iRet = WaitForMultipleObjects(nCount, iHandles(0), True, INFINITE)

    ' Alle restlichen Handles schließen
    For iRet = 0 To nCount - 1
        Call CloseHandle(iHandles(iRet))
        Let iHandles(iRet) = 0
    Next
End Sub

' Gibt ersten Arrayindex aus, der verfügbar ist
Private Function getFirstAvailableHandlePos() As Byte
    Dim i As Byte

    For i = 0 To UBound(iHandles)
        ' Handle ist leer
        If iHandles(i) = 0 Then
            Let getFirstAvailableHandlePos = i
            Exit Function

        ' Wenn nicht mehr aktiv
        ElseIf Not isActive(iHandles(i)) Then
            getFirstAvailableHandlePos = i
            Exit Function
        End If
    Next

    ' Kein Platz frei
    Let getFirstAvailableHandlePos = &HFF
End Function

' Gibt an, ob Prozess hinter Handle aktiv ist
Private Function isActive(ByVal iHandle As Long) As Boolean
    Dim Retval As Long

    Call GetExitCodeProcess(iHandle, Retval)

    isActive = (Retval = STILL_ACTIVE)
End Function

' Führ einen Prozess aus und wartet ggf., bis dieser fertiggestellt ist
Public Function ShellSingle( _
    ByVal sFile As String, _
    Optional sParams As String = vbNullString, _
    Optional eShowOpt As nCmdShowValues = nCmdShowValues.SW_RESTORE, _
    Optional hWnd As Long = 0, _
    Optional bWait As Boolean = True) As Long

    Dim iHandle As Long

    ' Prozess starten
    iHandle = StartProcess(sFile, sParams, eShowOpt, hWnd)

    ' Wenn gewartet werden soll
    If bWait Then

        ' Warten bis Prozess hinter Handle fertiggestellt wurde
        Call WaitForProcess(iHandle)

    Else

        ' Handle vom laufenden Prozess ausgeben
        Let ShellSingle = iHandle
    End If
End Function

' Wartet, bis ein Prozess hinter einem Handle abgeschlossen ist
Public Function WaitForProcess(ByRef iHandle As Long) As Long
    If iHandle = 0 Then Exit Function
    Dim iExitCode As Long

    Call WaitForSingleObject(iHandle, INFINITE)

    If GetExitCodeProcess(iHandle, iExitCode) <> 0 Then
        WaitForProcess = iExitCode
    End If

    Call CloseHandle(iHandle)
End Function

' Startet einen Prozess und gibt das Handle zurück
Private Function StartProcess( _
    ByVal sFile As String, _
    Optional sParams As String = vbNullString, _
    Optional eShowOpt As nCmdShowValues = nCmdShowValues.SW_RESTORE, _
    Optional hWnd As Long = 0) As Long
    Dim iRet As Long

    Dim uShell As SHELLEXECUTEINFOW

    ' Befehl aufbauen
    With uShell
        .hWnd = hWnd
        .fMask = fMaskValues.SEE_MASK_NOCLOSEPROCESS
        .lpFile = VBA.[_HiddenModule].StrPtr(sFile)
        .lpParameters = VBA.[_HiddenModule].StrPtr(sParams)
        .nShow = eShowOpt
        .cbSize = LenB(uShell)
    End With


    ' Befehl ausführen
    Let iRet = ShellExecuteExW(uShell)

    If iRet <> 0 Then

        ' Handle zurückgeben
        StartProcess = uShell.hProcess
    End If
End Function