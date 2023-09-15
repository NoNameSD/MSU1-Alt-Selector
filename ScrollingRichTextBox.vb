Option Compare Binary
Option Explicit On
Option Strict On

Namespace Logger
    Public Class ScrollingRichTextBox : Inherits System.Windows.Forms.RichTextBox

        <System.Runtime.InteropServices.DllImport("user32.dll", CharSet:=System.Runtime.InteropServices.CharSet.Unicode)>
        Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal LParam As IntPtr) As IntPtr : End Function

        <System.Runtime.InteropServices.DllImport("user32.dll", CharSet:=System.Runtime.InteropServices.CharSet.Unicode)>
        Private Shared Function SendNotifyMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal LParam As IntPtr) As Boolean : End Function

        Private Const _WM_VSCROLL As UInteger = 277
        Private Const _SB_BOTTOM As Integer = 7

        ''' <summary>
        ''' Scrolls to the bottom of the <see cref="System.Windows.Forms.RichTextBox"/>.
        ''' </summary>
        Public Sub ScrollToBottom()
            Call SendNotifyMessage(Me.Handle, _WM_VSCROLL, New IntPtr(_SB_BOTTOM), New IntPtr(0))
        End Sub
    End Class
End Namespace