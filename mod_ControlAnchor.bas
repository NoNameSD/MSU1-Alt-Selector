Attribute VB_Name = "ModuleControlAnchor"
' Code by SeabrookStan:
' https://www.vbforums.com/showthread.php?870133

Option Explicit   'AnchorModule SeabrookStan 2019
'Place the following one line of code in From1 - Sub Form_Resize().
'   Anchor ControlName, AnchorTop, AnchorLeft, AnchorRight, AnchorBottom (as Boolean)

Dim OB() As Control: Dim dL() As Long, dT() As Long, dB() As Long, dR() As Long

Public Sub Anchor(OBJ As Control, _
  Optional AnchorTop As Boolean = False, _
  Optional AnchorLeft As Boolean = False, _
  Optional AnchorRight As Boolean = False, _
  Optional AnchorBottom As Boolean = False)

  Static C As Long: Dim CW As Long: Dim CH As Long:  Dim X As Long
  Dim OBJ_Initiated As Boolean

  If C > 0 Then           'Has any control been Initiated by this Module?
    Do                    'Search through all controls already Initiated
      If OBJ.Name = OB(X).Name Then OBJ_Initiated = True Else X = X + 1
    Loop While OBJ_Initiated = False And X < UBound(OB)
  End If

  If Not OBJ_Initiated Then     'current Control Obj not seen yet,
    ReDim Preserve OB(C)        'so expand the zero based arrays and Initiate
    ReDim Preserve dR(C): ReDim Preserve dB(C):  ReDim Preserve dT(C):  ReDim Preserve dL(C)
    Set OB(C) = OBJ 'Add current control to the array of initiated controls

    'Calculates and saves the original Distance btwn Obj and Side
    If AnchorRight Then dR(C) = OBJ.Container.Width - OBJ.Left - OBJ.Width
    If AnchorBottom Then dB(C) = OBJ.Container.Height - OBJ.Top - OBJ.Height
    If Not AnchorTop Then dT(C) = OBJ.Container.Height - OBJ.Top
    If Not AnchorLeft Then dL(C) = OBJ.Container.Width - OBJ.Left
    X = C: C = C + 1 'Increment count of total controls Initiated
  End If

  CW = OBJ.Container.Width 'Determine Object Container size
  CH = OBJ.Container.Height

  If AnchorRight Then  'Now move the control's sides to follow movement of Form1 sides
    If AnchorLeft Then
      OBJ.Width = IIf(CW - OBJ.Left - dR(X) > 0, CW - OBJ.Left - dR(X), 0)
    Else
      OBJ.Left = OBJ.Container.Width - dL(X)
    End If
  End If
  If AnchorBottom Then
    If AnchorTop Then
      OBJ.Height = IIf(CH - OBJ.Top - dB(X) > 0, CH - OBJ.Top - dB(X), 0)
    Else
      OBJ.Top = OBJ.Container.Height - dT(X)
    End If
  End If
End Sub