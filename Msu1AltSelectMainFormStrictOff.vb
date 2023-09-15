Option Compare Binary
Option Explicit On
Option Strict Off
Partial Class Msu1AltSelectMainForm
    Private Sub BackgroundWorkerDelegate_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerDelegate.DoWork
        Call Me.DisableControls()

#If False Then
        Dim [delegate] = DirectCast(e.Argument, System.Delegate)

        Dim [object] As Object = [Delegate]

        Call [object].Invoke()
#Else
        e.Argument.Invoke()
#End If
    End Sub
End Class