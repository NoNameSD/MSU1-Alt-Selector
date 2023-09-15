VERSION 5.00
Begin VB.Form frm_MSU1_Alt_Select
   ClientHeight    =   5400
   ClientLeft      =   120
   ClientTop       =   465
   ClientWidth     =   3735
   Icon            =   "frm_MSU1_Alt_Select.frx":0000
   LinkTopic       =   "frm_MSU1_Alt_Select"
   ScaleHeight     =   5400
   ScaleWidth      =   3735
   WhatsThisButton =   -1  'True
   WhatsThisHelp   =   -1  'True
   Begin VB.CommandButton cmdBackToNormal
      Caption         =   "Change Back to Normal"
      Height          =   255
      Left            =   1370
      TabIndex        =   6
      Top             =   3800
      Width           =   2000
   End
   Begin VB.CommandButton cmdResample
      Caption         =   "Change Speed/Volume"
      Height          =   255
      Left            =   1370
      TabIndex        =   7
      Top             =   4100
      Width           =   2000
   End
   Begin VB.TextBox txtResample
      Alignment       =   1  'Right Justify
      CausesValidation=   0   'False
      Height          =   246
      Left            =   120
      TabIndex        =   4
      Text            =   "44100"
      Top             =   3799
      Width           =   855
   End
   Begin VB.TextBox txtVolume
      Alignment       =   1  'Right Justify
      CausesValidation=   0   'False
      Height          =   246
      Left            =   120
      TabIndex        =   5
      Text            =   "100"
      Top             =   4080
      Width           =   375
   End
   Begin VB.Timer tmrDKC3autoSwitch
      Enabled         =   0   'False
      Interval        =   1000
      Left            =   3120
      Top             =   3240
   End
   Begin VB.OptionButton optDKC3autoSwitch
      Caption         =   "Automatic Switch (DKC3) (Tracks 49-53)"
      Height          =   255
      Left            =   120
      TabIndex        =   10
      Top             =   5100
      Width           =   3600
   End
   Begin VB.ListBox lstMSUalts
      Height          =   3180
      Left            =   720
      TabIndex        =   3
      Top             =   600
      Width           =   2895
   End
   Begin VB.ListBox lstMSUwithAlt
      BeginProperty DataFormat
         Type            =   1
         Format          =   "000"
         HaveTrueFalseNull=   0
         FirstDayOfWeek  =   0
         FirstWeekOfYear =   0
         LCID            =   1031
         SubFormatType   =   0
      EndProperty
      Height          =   3180
      ItemData        =   "frm_MSU1_Alt_Select.frx":09AA
      Left            =   120
      List            =   "frm_MSU1_Alt_Select.frx":09AC
      TabIndex        =   2
      Top             =   600
      Width           =   495
   End
   Begin VB.CommandButton cmdSelectROM
      Caption         =   "Select MSU1 ROM"
      Height          =   375
      Left            =   2040
      TabIndex        =   1
      Top             =   120
      Width           =   1575
   End
   Begin VB.TextBox txtMSU1rom
      CausesValidation=   0   'False
      Height          =   375
      Left            =   120
      Locked          =   -1  'True
      TabIndex        =   0
      Top             =   120
      Width           =   1815
   End
   Begin VB.Frame frameResample
      BorderStyle     =   0  'None
      Height          =   615
      Left            =   3400
      TabIndex        =   12
      Top             =   3805
      Width           =   255
      Begin VB.OptionButton optNormal
         Enabled         =   0   'False
         Height          =   255
         Left            =   0
         TabIndex        =   13
         Top             =   0
         Width           =   255
      End
      Begin VB.OptionButton optResampled
         Enabled         =   0   'False
         Height          =   255
         Left            =   0
         TabIndex        =   14
         Top             =   300
         Width           =   255
      End
   End
   Begin VB.Frame frmExtProgramSettings
      Height          =   735
      Left            =   120
      TabIndex        =   16
      Top             =   4340
      Width           =   3495
      Begin VB.CheckBox optKeepCmdOpen
         Caption         =   "Keep CMD Windows opened (Debug)"
         Height          =   255
         Left            =   270
         TabIndex        =   18
         Top             =   450
         Width           =   3135
      End
      Begin VB.CheckBox optShowCmd
         Caption         =   "Show CMD Windows"
         Height          =   195
         Left            =   1440
         TabIndex        =   9
         Top             =   210
         Width           =   1815
      End
      Begin VB.TextBox txtThreads
         Alignment       =   1  'Right Justify
         CausesValidation=   0   'False
         Height          =   246
         Left            =   150
         TabIndex        =   8
         Text            =   "8"
         Top             =   165
         Width           =   375
      End
      Begin VB.Label lblThreads
         BackStyle       =   0  'Transparent
         Caption         =   "Threads"
         Height          =   240
         Left            =   540
         TabIndex        =   17
         Top             =   210
         Width           =   840
      End
   End
   Begin VB.Label lblHz
      BackStyle       =   0  'Transparent
      Caption         =   "Hz"
      Height          =   246
      Left            =   990
      TabIndex        =   11
      Top             =   3833
      Width           =   240
   End
   Begin VB.Label lblVolume
      BackStyle       =   0  'Transparent
      Caption         =   "% Volume"
      Height          =   246
      Left            =   510
      TabIndex        =   15
      Top             =   4119
      Width           =   840
   End
End
Attribute VB_Name = "frm_MSU1_Alt_Select"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private bDKC3autoSwitch As Boolean
Private Const iMinWidth As Integer = 3855
Private Const iMinHeight As Integer = 3045
Private Const iMinHeightNoAlt As Integer = iMinHeight - 300

Private Sub Form_Load()
    ' Fixes issue of not Scaling to Fullscreen properly, before resizing
    Call Form_Resize

    ' Display Version in Window Caption
    Me.Caption = "MSU1 Helper" & " (Version " & App.Major & "." & App.Minor & "." & App.Revision & ")"

    Call reloadMSU1folder

    ' Put form to its minimum size, when no tracks with alts were found
    If Me.lstMSUwithAlt.ListCount = 0 Then Me.Height = 0: Me.Width = 0

    Call optShowCmd_Click
    Call optKeepCmdOpen_Click
    Call txtThreads_Change

    Let mod_MAS_Functions.hWnd = Me.hWnd
End Sub

Private Sub cmdSelectROM_Click()
    Call mod_MAS_Functions.InitializeMSU1Folder(mod_MAS_Functions.UserSelectROM)
    Call reloadMSU1folder
End Sub

Private Sub reloadMSU1folder()
    Me.txtMSU1rom.Text = mod_MAS_Functions.getMSUFolder

    Call lstMSUalts.Clear

    Call loadLstMSUwithAlt

    ' Activate Automatic Win & Lose Switch for DKC3 (ROM-Name begins with 'DKC3' and Tracks with alts were found)
    bDKC3autoSwitch = (UCase(Left$(mod_MAS_Functions.getMSUname, 4)) = "DKC3") And CBool(Me.lstMSUwithAlt.ListCount)

    ' Set the Audio Switch radio Button and Timer
    Me.optDKC3autoSwitch.Value = bDKC3autoSwitch
    Me.tmrDKC3autoSwitch.Enabled = bDKC3autoSwitch

    Call setResampleRadioButtons
End Sub

Private Sub optKeepCmdOpen_Click()

    Const sMsg As String = _
        "By enabling this, all CMD windows for converting the .pcm audio files won't be automatically closed, when the process is finished." & vbNewLine & _
        "This can be useful for troubleshooting, but you will have to close all windows manually." & vbNewLine & _
        "Do you want to continue?"

    If Me.optKeepCmdOpen.Value Then
        If vbYes <> MsgBox(sMsg, vbQuestion Or vbYesNo) Then
            Let Me.optKeepCmdOpen.Value = CByte(Abs(mod_MAS_Functions.CmdKeepWindowsOpened))
            Exit Sub
        End If
    End If

    Let mod_MAS_Functions.CmdKeepWindowsOpened = Me.optKeepCmdOpen.Value
End Sub

Private Sub optShowCmd_Click()
    Let mod_MAS_Functions.CmdShowWindows = Me.optShowCmd
    Let Me.optKeepCmdOpen.Enabled = mod_MAS_Functions.CmdShowWindows
End Sub

Private Sub Form_Resize()
    Select Case Me.WindowState
    Case vbMinimized ' Don't do any resizing when window is minimized (Program will crash otherwise (See Run-time error 384))
        Exit Sub
    Case vbNormal ' Only when Form is in windowed mode

        ' Put Form to minimum size, if it is set lower that that

        If Me.lstMSUalts.Visible Then

            If Me.Height <= iMinHeight Then
                Let Me.Height = iMinHeight
            End If
        Else
            If Me.Height <= iMinHeightNoAlt Then
                Let Me.Height = iMinHeightNoAlt
            End If
        End If

        If Me.Width <= iMinWidth Then
            Let Me.Width = iMinWidth
        End If
    End Select
    Call ModuleControlAnchor.Anchor(Me.optDKC3autoSwitch, , True, , True)
    Call ModuleControlAnchor.Anchor(Me.txtMSU1rom, True, True, True)
    Call ModuleControlAnchor.Anchor(Me.cmdSelectROM, , , True)
    Call ModuleControlAnchor.Anchor(Me.lstMSUwithAlt, True, True, , True)
    Call ModuleControlAnchor.Anchor(Me.lstMSUalts, True, True, True, True)
    Call ModuleControlAnchor.Anchor(Me.txtResample, , True, , True)
    Call ModuleControlAnchor.Anchor(Me.txtVolume, , True, , True)
    Call ModuleControlAnchor.Anchor(Me.lblVolume, , True, , True)
    Call ModuleControlAnchor.Anchor(Me.lblHz, , True, , True)
    Call ModuleControlAnchor.Anchor(Me.cmdResample, , True, True, True)
    Call ModuleControlAnchor.Anchor(Me.cmdBackToNormal, , True, True, True)
    Call ModuleControlAnchor.Anchor(Me.frameResample, , , True, True)
    Call ModuleControlAnchor.Anchor(Me.frmExtProgramSettings, , True, True, True)
    Call ModuleControlAnchor.Anchor(Me.optShowCmd, , , True, True)
    Call ModuleControlAnchor.Anchor(Me.optKeepCmdOpen, , , True, True)
End Sub

' Loads all the Tracks, that have Alt. Tracks
Private Sub loadLstMSUwithAlt()
    Dim i As Byte
    Dim iList() As Byte

    iList = mod_MAS_Functions.getTracksWithAlts()

    Call lstMSUwithAlt.Clear

    If (Not Not iList) = 0 Then
        Let Me.lstMSUalts.Visible = False
        Let Me.lstMSUwithAlt.Visible = False
        Exit Sub
    End If

    Let Me.lstMSUalts.Visible = True
    Let Me.lstMSUwithAlt.Visible = True

    Call Form_Resize

    For i = 0 To UBound(iList)

        Call lstMSUwithAlt.AddItem(iList(i))
    Next
End Sub

Private Sub lstMSUalts_Click()

    ' Leave Sub, if the old Track is the same as the new Track
    If mod_MAS_Functions.getCurSelAltTrack = lstMSUalts.List(lstMSUalts.ListIndex) Then Exit Sub

    ' Switch both Tracks
    If Not mod_MAS_Functions.switchTracks( _
        iTrackId:=lstMSUwithAlt.List(lstMSUwithAlt.ListIndex), _
        sTrackNameOld:=mod_MAS_Functions.getCurSelAltTrack, _
        sTrackNameNew:=lstMSUalts.List(lstMSUalts.ListIndex)) Then

        ' Something went wrong
        Call MsgBox("Tracks could not be switched", vbInformation)

    End If

    ' Refill the List of the Alt. Tracks
    Call lstMSUwithAlt_Click

End Sub

Private Sub lstMSUwithAlt_Click()
    Call reloadAltTracks
End Sub

' Refresh the selection of the Tracks with Alt. Tracks
Private Sub reloadAltTracks()

    ' Do Nothing, while nothing is selected
    If lstMSUwithAlt.ListIndex = -1 Then Exit Sub

    Dim iSelected As Integer
    Dim i As Integer
    Dim sAltTracks() As String

    ' Get Array of all Alt. Track of the selected MSU-ID + The Index of the selected Track
    sAltTracks = mod_MAS_Functions.getAltTrackList(lstMSUwithAlt.List(lstMSUwithAlt.ListIndex), iSelected)

    On Error Resume Next

    Call lstMSUalts.Clear

    ' Add the entire Array to the List
    For i = 0 To UBound(sAltTracks)

        Call lstMSUalts.AddItem(sAltTracks(i))

    Next

    ' Set the Current selected Track in the List
    lstMSUalts.ListIndex = iSelected

    Err.Clear
End Sub

Private Sub optDKC3autoSwitch_Mouseup(Button As Integer, Shift As Integer, X As Single, Y As Single)

    ' Switch the Option Selection
    bDKC3autoSwitch = Not bDKC3autoSwitch
    Me.optDKC3autoSwitch = bDKC3autoSwitch

    ' Enable / Disable the Timer
    Me.tmrDKC3autoSwitch.Enabled = bDKC3autoSwitch

End Sub

Private Sub tmrDKC3autoSwitch_Timer()
    Call DKC3autoSwitch

    Call reloadAltTracks

    'Debug.Print "Timer Loaded: "& Time
End Sub

Private Sub cmdResample_Click()
    On Error GoTo ShowErrorMessageOther
    Dim iSampleRate As Long
    Dim iVolumePercent As Integer
    Dim sLatestAction As String

    iSampleRate = CLng(Val(Me.txtResample.Text))

    ' Cancel, if the value is invalid
    If iSampleRate < 1000 Or iSampleRate > 1000000 Then Call Err.Raise(Number:=513, Description:="The Value for Sample Rate is invalid!" & vbNewLine & "Only Values from 1000 to 1000000 are accepted.")

    Select Case Val(Me.txtVolume)
        Case Is > 1000
            Call Err.Raise(Number:=514, Description:="The volume level is too large!" & vbNewLine & "Maximum Value is 1000")
        Case Is < 0
            Call Err.Raise(Number:=515, Description:="Volume level cannot be a negative number!")
        Case Else
            iVolumePercent = Val(Me.txtVolume)
    End Select

    If iSampleRate = 44100 And Val(Me.txtVolume) = 100 Then Call Err.Raise(Number:=516, Description:="Sample Rate and Volume is unchanged." & vbNewLine & "OutputFiles would be identical!")

    ' Create and Switch to Resampled Versions
    Call mod_MAS_Functions.changeSpeed(iSampleRate, CInt(Val(Me.txtVolume)), sLatestAction)

    Call setResampleRadioButtons

    Exit Sub

ShowErrorMessageOther:
    Call MsgBox("An Error occurred (" & IIf(Err <= 512, "VB Error", "Error") & Chr(32) & Err & ")" & vbNewLine & vbNewLine & Error & IIf(LenB(sLatestAction) <> 0, (vbNewLine & vbNewLine & "Latest Performed Action:" & vbNewLine & sLatestAction), Empty), vbCritical): Exit Sub
End Sub

Private Sub cmdBackToNormal_Click()
    Call mod_MAS_Functions.changeSpeedBack
    Call setResampleRadioButtons
End Sub

Private Sub setResampleRadioButtons()
    Select Case mod_MAS_Functions.IsResampled
    Case True
        Me.optResampled = True
    Case Else
        Me.optNormal = True
    End Select
End Sub

' Sometimes the Program still runs in the Background
' This keeps it from doing this
Private Sub Form_Unload(Cancel As Integer)
    End
End Sub

Private Sub txtThreads_Change()
    On Error GoTo Err_InvalidByteVal
    Dim iThreads As Byte

    Let iThreads = CByte(Me.txtThreads.Text)

    On Error GoTo Err_InvalidNumber

    If iThreads = 0 Then GoTo Err_InvalidByteVal

    If iThreads > MAXIMUM_WAIT_OBJECTS Then
        Let iThreads = MAXIMUM_WAIT_OBJECTS
    End If

    Let mod_ShellWait.Threads = iThreads

    GoTo WriteBackToTxt
Err_InvalidByteVal:
    Let mod_ShellWait.Threads = 8
    GoTo WriteBackToTxt
Err_InvalidNumber:
WriteBackToTxt:
    Let Me.txtThreads.Text = mod_ShellWait.Threads

    Select Case mod_ShellWait.Threads
        Case 1
            Let Me.lblThreads.Caption = "Thread"
        Case Else
            Let Me.lblThreads.Caption = "Threads"
    End Select
End Sub
