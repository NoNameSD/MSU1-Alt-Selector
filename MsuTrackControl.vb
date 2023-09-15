Option Compare Binary
Option Explicit On
Option Strict On
Imports MsuAltSelect.Msu.Tracks
Imports MsuAltSelect.Msu.Ex

Public Class MsuTrackControl
    'Inherits MsuControlBase

    Public Event AppliedChanges(sender As Object, e As EventArgs)
    Public Event UserChangedValue(sender As Object, e As EventArgs)
    Private Property MsuTrackToEdit As MsuTrack
    Private Property MsuTrackTmp As MsuTrack
    Private Property MsuTracks As MsuTracks

    Public Property MsuTrackNumberChangeAllowed As Boolean
        Get
            Return Me.nudMsuTrackId.Enabled
        End Get
        Protected Set(value As Boolean)
            Me.nudMsuTrackId.Enabled = value
        End Set
    End Property

    Public Sub LoadMsuTrackToEdit(ByRef msuTrackToEdit As MsuTrack)
        Me.MsuTrackToEdit = msuTrackToEdit

        If Me.MsuTrackTmp IsNot Nothing Then
            Call Me.MsuTrackTmp.Dispose()
        End If

        ' Create temporary copy of the MsuTrack Object to edit
        Me.MsuTrackTmp = New MsuTrack(msuTrackToEdit.Parent, msuTrackToEdit.TrackNumber) With {
            .Title = msuTrackToEdit.Title
        }

        Me.MsuTracks = Me.MsuTrackToEdit.Parent

        Me.MsuTrackNumberChangeAllowed = False

        RaiseEvent UserChangedValue(Me.MsuTracks, Nothing)
    End Sub

    Protected Sub RefreshTextFields(sender As Object, e As EventArgs) Handles Me.UserChangedValue

        If Me.txtMsuTitle.Text.Equals(Me.MsuTrackTmp.Title, StringComparison.Ordinal) Then
        Else
            Me.txtMsuTitle.Text = Me.MsuTrackTmp.Title
        End If

        Me.nudMsuTrackId.Value = Me.MsuTrackTmp.TrackNumber
        Me.txtMsuFileName.Text = Me.MsuTrackTmp.FileName

    End Sub

    Public Sub ApplyChanges()

        ' Check for existing Track with this Id
        If Me.MsuTracks.TrackDict.ContainsKey(Me.MsuTrackTmp.TrackNumber) Then
            Dim objMsuTrackExisting As MsuTrack = Me.MsuTracks.TrackDict.Item(Me.MsuTrackTmp.TrackNumber)

            If objMsuTrackExisting IsNot Me.MsuTrackToEdit Then
                Throw New MsuTrackAlreadyExistsException(objMsuTrackExisting)
            End If
        End If

        If Me.MsuTrackToEdit Is Nothing Then

            ' Add new track

            Call Me.MsuTracks.TrackDict.Add(Me.MsuTrackTmp.TrackNumber, Me.MsuTrackTmp)
            Me.MsuTrackNumberChangeAllowed = False
        Else

            ' Update existing track

            If Me.MsuTrackToEdit.TrackNumber <> Me.MsuTrackTmp.TrackNumber Then
                Throw New MsuTrackNumberChangeException
            End If

            Me.MsuTrackToEdit.Title = Me.MsuTrackTmp.Title
        End If

        RaiseEvent AppliedChanges(Me, Nothing)
    End Sub

    Private Sub nudMsuTrackId_ValueChanged(sender As Object, e As EventArgs) Handles nudMsuTrackId.ValueChanged
        Try
            Me.MsuTrackTmp.TrackNumber = CByte(Me.nudMsuTrackId.Value)
            RaiseEvent UserChangedValue(sender, e)
        Catch ex As System.OverflowException
            Me.nudMsuTrackId.Value = Me.MsuTrackTmp.TrackNumber
        Catch ex As System.InvalidCastException
            Me.nudMsuTrackId.Value = Me.MsuTrackTmp.TrackNumber
        End Try
    End Sub

    Private Sub txtMsuTitle_TextChanged(sender As Object, e As EventArgs) Handles txtMsuTitle.TextChanged
        If String.IsNullOrWhiteSpace(txtMsuTitle.Text) Then
            Me.MsuTrackTmp.Title = Nothing
        Else
            Me.MsuTrackTmp.Title = txtMsuTitle.Text()
        End If
        RaiseEvent UserChangedValue(sender, e)
    End Sub

    Protected Overrides Sub Finalize()
        If Me.MsuTrackTmp IsNot Nothing Then
            Call Me.MsuTrackTmp.Dispose()
        End If
        MyBase.Finalize()
    End Sub
End Class