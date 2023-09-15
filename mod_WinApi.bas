Attribute VB_Name = "mod_WinApi"
Option Compare Binary
Option Explicit

Public Declare Function FindExecutableW Lib "shell32.dll" _
   (ByVal lpFile As Long, _
    ByVal lpDirectory As Long, _
    ByVal lpResult As Long) As Long

' https://docs.microsoft.com/de-de/windows/win32/DevNotes/RtlMoveMemory
'Public Declare Sub RtlMoveMemory Lib "kernel32" ( _
  ByVal dest As Long _
, ByVal Source As Long _
, ByVal bytes As Long)

Private Const MIN_SUCCESS_LNG       As Byte = &H20
Private Const MAX_PATH              As Long = &H104

' Use WindowsAPI for search
' Searches in the provided SearchPath and the Windows %PATH% Environment Variable
Public Function FindExecutable(ByRef sFileSearch As String, Optional ByRef sSearchPath As String = vbNullString) As String
    If LenB(sFileSearch) = 0 Then Exit Function
    Dim sRetPath As String
    Dim iReturn As Long

    Let sRetPath = String$(MAX_PATH, vbNullChar)

    Let iReturn = FindExecutableW( _
        lpFile:=VBA.StrPtr(sFileSearch), _
        lpDirectory:=VBA.StrPtr(sSearchPath), _
        lpResult:=VBA.StrPtr(sRetPath))

    If iReturn >= MIN_SUCCESS_LNG Then
        If InStr(1, sRetPath, vbNullChar) Then
            FindExecutable = Left$(sRetPath, InStr(1, sRetPath, vbNullChar) - 1)
        Else
            FindExecutable = sRetPath
        End If
    End If
End Function