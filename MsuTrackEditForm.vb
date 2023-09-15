Option Compare Binary
Option Explicit On
Option Strict On

Public Class MsuTrackEditForm
    Public ReadOnly Property MsuTrackControl As MsuTrackControl
        Get
            Return Me.ucMsuTrack
        End Get
    End Property

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        If Me.ApplyChanges() Then
            Me.DialogResult = DialogResult.OK
        Else
            Me.DialogResult = DialogResult.None
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        Call Me.ApplyChanges()
        Me.DialogResult = DialogResult.None
    End Sub

    Private Function ApplyChanges() As Boolean
        Try
            Me.MsuTrackControl.ApplyChanges()
            Return True
        Catch ex As Exception
            Call Msu.Ex.MsuExceptionDisplay.DisplayExceptionAsMessageBox(owner:=Me, ex:=ex)
            Return False
        End Try
    End Function
End Class