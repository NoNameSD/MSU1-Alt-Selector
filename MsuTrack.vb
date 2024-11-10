Option Compare Binary
Option Explicit On
Option Strict On
Imports System.Runtime.InteropServices
Imports MsuAltSelect.Msu.Ex

Namespace Msu
    Namespace Tracks
        Partial Public MustInherit Class MsuPcmFile

            Friend Const NormalVersionSuffix As String = "_Normal"

            <Newtonsoft.Json.JsonIgnore>
            Public MustOverride ReadOnly Property MsuTrackConfig As MsuTracks

            ''' <summary>
            ''' MSU1 Track-Id of this track
            ''' </summary>
            <Newtonsoft.Json.JsonProperty("track_number")>
            Public MustOverride Property TrackNumber As Byte

#Disable Warning CA1507 ' Use nameof to express symbol names
            ''' <summary>
            ''' Title / Name of the <see cref="MsuPcmFile"/>
            ''' </summary>
            <Newtonsoft.Json.JsonProperty("title")>
            Public MustOverride Property Title As String
#Enable Warning CA1507 ' Use nameof to express symbol names

            ''' <summary>
            ''' BaseName of the PCM-Track
            ''' </summary>
            <Newtonsoft.Json.JsonIgnore>
            Public MustOverride ReadOnly Property BaseName As String

            ''' <summary>
            ''' FileName of the PCM-Track
            ''' </summary>
            <Newtonsoft.Json.JsonIgnore>
            Public MustOverride ReadOnly Property FileName As String

            ''' <summary>
            ''' Full FilePath of the  PCM-Track
            ''' </summary>
            <Newtonsoft.Json.JsonIgnore>
            Public MustOverride ReadOnly Property FilePath As String

            ''' <summary>
            ''' Parent directory of the .pcm file (Relative to <see cref="MsuTracks.MsuLocation"/>)
            ''' </summary>
            <Newtonsoft.Json.JsonIgnore>
            Public MustOverride Property LocationRelative As String

            Protected Function GetLocationRelative() As String
                Dim currentDirectory As String = Me.MsuTrackConfig.MsuLocation
                Dim locationAbsolute = Me.LocationAbsolute
                If String.IsNullOrWhiteSpace(LocationAbsolute) Then
                    Return Constants.vbNullString
                Else
                    Return System.IO.Path.GetRelativePath(path:=locationAbsolute, relativeTo:=currentDirectory)
                End If
            End Function

            ''' <summary>
            ''' Parent directory of the .pcm file (Absolute Path)
            ''' </summary>
            <Newtonsoft.Json.JsonIgnore>
            Public MustOverride Property LocationAbsolute As String

            ''' <summary>
            ''' Deletes the PCM File associated with <see cref="MsuPcmFile"/>
            ''' </summary>
            Public Overridable Sub Delete()
                Dim locationAbsolute = Me.LocationAbsolute

                Call System.IO.File.Delete(Me.FilePath)

                ' Also try to delete the parent folder, if it is empty after deleting the PCM file
                Try
                    If Msu.MsuHelper.IsDirectoryEmpty(locationAbsolute) Then
                        Call System.IO.Directory.Delete(locationAbsolute, False)
                    End If
                Catch ex As Exception
                    ' Ignore
                End Try
            End Sub

            ''' <returns>
            ''' Checks if the file at <see cref="MsuPcmFile.FilePath"/> exists.
            ''' </returns>
            Public Overridable Function FilePathExists() As Boolean
                Return System.IO.File.Exists(Me.FilePath)
            End Function

            ''' <returns>
            ''' Path to the file that will be used for conversion with MSUPCM++.
            ''' </returns>
            Public Function FilePathForConversion() As String
                Dim filePathNormalSuffix = Me.FilePathWithNormalVersionSuffix

                If System.IO.File.Exists(filePathNormalSuffix) Then
                    ' Tracks are currently converted. Use the file with the suffix "_Normal"
                    Return filePathNormalSuffix
                Else
                    ' Tracks are not currently converted. Use the normal filename.
                    Return Me.FilePath
                End If
            End Function

            ''' <param name="Track"><see cref="MsuTrack"/> to determine the MSU1 Track-Id</param>
            ''' <returns>
            ''' Filename of this <see cref="MsuPcmFile"/>
            ''' </returns>
            Public Shared Function GetFileName(track As MsuTrack) As String
                Return GetFileName(track.Parent.PcmPrefix, track)
            End Function

            ''' <param name="PcmPrefix">Prefix for the .pcm FileName</param>
            ''' <param name="Track"><see cref="MsuTrack"/> to determine the MSU1 Track-Id</param>
            ''' <returns>
            ''' Filename of this <see cref="MsuPcmFile"/>
            ''' </returns>
            Public Shared Function GetFileName(pcmPrefix As String, track As MsuTrack) As String
                Return GetFileName(pcmPrefix, track.TrackNumber)
            End Function

            ''' <param name="PcmPrefix">Prefix for the .pcm FileName</param>
            ''' <param name="TrackNumber">MSU1 Track-Id to use</param>
            ''' <returns>
            ''' Filename of this <see cref="MsuPcmFile"/>
            ''' </returns>
            Public Shared Function GetFileName(pcmPrefix As String, trackNumber As Byte) As String
                Return String.Concat(pcmPrefix, MsuHelper.HyphenChar, trackNumber, MsuHelper.PcmExtL)
            End Function

            ''' <param name="PcmPrefix">Prefix for the .pcm FileName</param>
            ''' <param name="TrackNumber">MSU1 Track-Id to use</param>
            ''' <param name="Suffix">Additional Suffix in the Filename after the <paramref name="TrackNumber"/></param>
            ''' <returns>
            ''' Filename of this <see cref="MsuPcmFile"/>
            ''' </returns>
            Public Shared Function GetFileName(pcmPrefix As String, trackNumber As Byte, ByRef suffix As String) As String
                Return String.Concat(pcmPrefix, MsuHelper.HyphenChar, trackNumber, suffix, MsuHelper.PcmExtL)
            End Function

            ''' <param name="PcmPrefix">Prefix for the .pcm FileName</param>
            ''' <returns>
            ''' Filename of this <see cref="MsuPcmFile"/>
            ''' </returns>
            Public Function GetFileName(pcmPrefix As String) As String
                Return MsuPcmFile.GetFileName(pcmPrefix, Me.TrackNumber)
            End Function

            ''' <param name="PcmPrefix">Prefix for the .pcm FileName</param>
            ''' <param name="Suffix">Additional Suffix in the Filename after the <see cref="TrackNumber"/></param>
            ''' <returns>
            ''' Filename of this <see cref="MsuPcmFile"/>
            ''' </returns>
            Public Function GetFileName(pcmPrefix As String, ByRef suffix As String) As String
                Return MsuPcmFile.GetFileName(pcmPrefix, Me.TrackNumber, suffix)
            End Function

            ''' <param name="Track"><see cref="MsuTrack"/> to determine the MSU1 Track-Id</param>
            ''' <returns>
            ''' Full file path of this <see cref="MsuPcmFile"/>
            ''' </returns>
            Public Function GetFilePath(track As MsuTrack) As String
                Return GetFilePath(track.Parent.PcmPrefix, track)
            End Function

            ''' <param name="PcmPrefix">Prefix for the .pcm FileName</param>
            ''' <param name="Track"><see cref="MsuTrack"/> to determine the MSU1 Track-Id</param>
            ''' <returns>
            ''' Full file path of this <see cref="MsuPcmFile"/>
            ''' </returns>
            Public Function GetFilePath(pcmPrefix As String, track As MsuTrack) As String
                Return GetFilePath(pcmPrefix, track.TrackNumber)
            End Function

            ''' <param name="PcmPrefix">Prefix for the .pcm FileName</param>
            ''' <param name="TrackNumber">MSU1 Track-Id to use</param>
            ''' <returns>
            ''' Full file path of this <see cref="MsuPcmFile"/>
            ''' </returns>
            Public Function GetFilePath(pcmPrefix As String, trackNumber As Byte) As String
                Return GetFilePath(Me.LocationAbsolute, pcmPrefix, trackNumber)
            End Function

            ''' <param name="PcmPrefix">Prefix for the .pcm FileName</param>
            ''' <returns>
            ''' Full file path of this <see cref="MsuPcmFile"/>
            ''' </returns>
            Public Function GetFilePath(pcmPrefix As String) As String
                Return Me.GetFilePath(pcmPrefix:=pcmPrefix, trackNumber:=Me.TrackNumber)
            End Function

            ''' <param name="PcmPrefix">Prefix for the .pcm FileName</param>
            ''' <param name="Suffix">Additional Suffix in the Filename after the <see cref="TrackNumber"/></param>
            ''' <returns>
            ''' Full file path of this <see cref="MsuPcmFile"/>
            ''' </returns>
            Public Function GetFilePath(PcmPrefix As String, Suffix As String) As String
                Return MsuPcmFile.GetFilePath(Me.LocationAbsolute, PcmPrefix, Me.TrackNumber, Suffix)
            End Function

            ''' <param name="Location">Path to parent directory</param>
            ''' <param name="PcmPrefix">Prefix for the .pcm FileName</param>
            ''' <param name="TrackNumber">MSU1 Track-Id to use</param>
            ''' <returns>
            ''' Full file path of this <see cref="MsuPcmFile"/>
            ''' </returns>
            Public Shared Function GetFilePath(location As String, pcmPrefix As String, trackNumber As Byte) As String
                Return System.IO.Path.Join(location, MsuPcmFile.GetFileName(pcmPrefix, trackNumber))
            End Function

            ''' <param name="Location">Path to parent directory</param>
            ''' <param name="PcmPrefix">Prefix for the .pcm FileName</param>
            ''' <param name="TrackNumber">MSU1 Track-Id to use</param>
            ''' <param name="Suffix">Additional Suffix in the Filename after the <paramref name="TrackNumber"/></param>
            ''' <returns>
            ''' Full file path of this <see cref="MsuPcmFile"/>
            ''' </returns>
            Public Shared Function GetFilePath(location As String, pcmPrefix As String, trackNumber As Byte, suffix As String) As String
                Return System.IO.Path.Join(location, MsuPcmFile.GetFileName(pcmPrefix, trackNumber, suffix))
            End Function

            Public Function FilePathWithNormalVersionSuffix() As String
                Return FilePathWithSuffix(NormalVersionSuffix)
            End Function

            Public Function FilePathWithSuffix(ByRef suffix As String) As String
                Return Me.GetFilePath(PcmPrefix:=Me.MsuTrackConfig.PcmPrefix, Suffix:=suffix)
            End Function

            Public Function FilePathWithNormalVersionSuffixExists() As Boolean
                Return FilePathWithSuffixExists(NormalVersionSuffix)
            End Function

            Public Function FilePathWithSuffixExists(ByRef suffix As String) As Boolean
                Return System.IO.File.Exists(FilePathWithSuffix(suffix))
            End Function

            ''' <summary>
            ''' Return if this Track is currently being played (File at <see cref="FilePath"/> is open.)
            ''' </summary>
            Public Overridable Function IsOpen() As Boolean
                Return Msu.MsuHelper.FileIsLocked(Me.FilePath)
            End Function

            ''' <exception cref="System.ArgumentNullException">
            ''' Thrown when <see cref="FilePath"/><c>is null</c>.
            ''' </exception>
            ''' <exception cref="System.ArgumentException" />
            ''' <exception cref="System.NotSupportedException"/>
            ''' <exception cref="System.IO.FileNotFoundException">
            ''' Thrown when <see cref="FilePath"/><c>does not exist</c>.
            ''' </exception>
            ''' <exception cref="System.Security.SecurityException"/>
            ''' <exception cref="System.IO.DirectoryNotFoundException"/>
            ''' <exception cref="System.UnauthorizedAccessException"/>
            ''' <exception cref="System.IO.PathTooLongException"/>
            ''' <exception cref="System.ArgumentOutOfRangeException"/>
            ''' <returns>LoopPoint of the MSU1 PCM File at <see cref="FilePath"/></returns>
            Public Function GetLoopPoint() As UInt32
                Dim FilePathWithNormalVersionSuffix = Me.FilePathWithNormalVersionSuffix

                If System.IO.File.Exists(FilePathWithNormalVersionSuffix) Then
                    Return GetLoopPoint(FilePathWithNormalVersionSuffix)
                Else
                    Return GetLoopPoint(Me.FilePath)
                End If
            End Function

            ''' <exception cref="System.ArgumentNullException">
            ''' Thrown when <paramref name="PcmFilePath"/><c>is null</c>.
            ''' </exception>
            ''' <exception cref="System.ArgumentException" />
            ''' <exception cref="System.NotSupportedException"/>
            ''' <exception cref="System.IO.FileNotFoundException">
            ''' Thrown when <paramref name="PcmFilePath"/><c>does not exist</c>.
            ''' </exception>
            ''' <exception cref="System.Security.SecurityException"/>
            ''' <exception cref="System.IO.DirectoryNotFoundException"/>
            ''' <exception cref="System.UnauthorizedAccessException"/>
            ''' <exception cref="System.IO.PathTooLongException"/>
            ''' <exception cref="System.ArgumentOutOfRangeException"/>
            ''' <returns>LoopPoint of the MSU1 PCM File at <paramref name="PcmFilePath"/></returns>
            Public Shared Function GetLoopPoint(ByRef pcmFilePath As String) As UInt32
                Dim fileStream As System.IO.FileStream

                ' Open the PCM File for reading
                Try

                    fileStream =
                New System.IO.FileStream(
                    path:=pcmFilePath,
                    access:=System.IO.FileAccess.Read,
                    share:=System.IO.FileShare.Read,
                    mode:=System.IO.FileMode.Open)

                Catch ex As System.IO.IOException

                    fileStream =
                New System.IO.FileStream(
                    path:=pcmFilePath,
                    access:=System.IO.FileAccess.Read,
                    share:=System.IO.FileShare.ReadWrite Or IO.FileShare.Delete,
                    mode:=System.IO.FileMode.Open)

                End Try

                ' Skip the 4-Byte "MSU1" Header
                Call fileStream.Seek(offset:=4, origin:=System.IO.SeekOrigin.Begin)

                Dim binaryReader As New _
                    System.IO.BinaryReader(
                        input:=fileStream,
                        encoding:=Text.Encoding.ASCII,
                        leaveOpen:=False)

                ' Read the LoopPoint (Index 4->7 in LittleEndian)
                Dim loopPoint = Buffers.Binary.BinaryPrimitives.ReadUInt32LittleEndian(binaryReader.ReadBytes(4))

                Call binaryReader.Close()

                Return loopPoint
            End Function

            ''' <summary>
            ''' Writes the <paramref name="LoopPoint" /> into the PCM file at <paramref name="PcmFilePath" />
            ''' </summary>
            ''' <exception cref="System.ArgumentNullException">
            ''' Thrown when <paramref name="PcmFilePath"/><c>is null</c>.
            ''' </exception>
            ''' <exception cref="System.ArgumentException" />
            ''' <exception cref="System.NotSupportedException"/>
            ''' <exception cref="System.IO.FileNotFoundException">
            ''' Thrown when <paramref name="PcmFilePath"/><c>does not exist</c>.
            ''' </exception>
            ''' <exception cref="System.Security.SecurityException"/>
            ''' <exception cref="System.IO.DirectoryNotFoundException"/>
            ''' <exception cref="System.UnauthorizedAccessException"/>
            ''' <exception cref="System.IO.PathTooLongException"/>
            ''' <exception cref="System.ArgumentOutOfRangeException"/>
            ''' <param name="PcmFilePath">Path to the PCM file</param>
            ''' <param name="LoopPoint">LoopPoint to write into the PCM file</param>
            Public Shared Sub SetLoopPoint(ByRef pcmFilePath As String, ByRef loopPoint As UInt32)
                Dim fileStream As System.IO.FileStream

                ' Open the PCM File for reading
                Try

                    fileStream =
                New System.IO.FileStream(
                    path:=pcmFilePath,
                    access:=System.IO.FileAccess.Write,
                    share:=System.IO.FileShare.Read,
                    mode:=System.IO.FileMode.Open)

                Catch ex As System.IO.IOException

                    fileStream =
                New System.IO.FileStream(
                    path:=pcmFilePath,
                    access:=System.IO.FileAccess.Write,
                    share:=System.IO.FileShare.ReadWrite Or IO.FileShare.Delete,
                    mode:=System.IO.FileMode.Open)

                End Try

                ' Skip the 4-Byte "MSU1" Header
                Call fileStream.Seek(offset:=4, origin:=System.IO.SeekOrigin.Begin)

                Dim binaryWriter As New _
                    System.IO.BinaryWriter(
                        output:=fileStream,
                        encoding:=Text.Encoding.ASCII,
                        leaveOpen:=False)

                Dim loopPointBytes(3) As Byte

                ' Convert the LoopPoint into Bytes
                Call Buffers.Binary.BinaryPrimitives.WriteUInt32LittleEndian(destination:=LoopPointBytes, value:=loopPoint)

                ' Write the LoopPoint into the PCM file (Index 4->7 in LittleEndian)
                Call binaryWriter.Write(loopPointBytes)

                Call binaryWriter.Close()
            End Sub
        End Class

        Public Class MsuTrack : Inherits MsuPcmFile : Implements IDisposable
            Public Event TrackAltSwitched(ByVal sender As Object, ByVal e As TrackAltSwitchedEventArgs)
            Private disposedValue As Boolean
            Private objTracks As MsuTracks

            Public Class TrackAltSwitchedEventArgs : Inherits System.EventArgs
                Public ReadOnly Property MsuTrack As MsuTrack
                    Get
                        Return Me.MsuTrackAltNew.Parent
                    End Get
                End Property
                Public ReadOnly Property MsuTrackAltOld As MsuTrackAlt
                Public ReadOnly Property MsuTrackAltNew As MsuTrackAlt

                Public Sub New(ByRef msuTrackAltOld As MsuTrackAlt, ByRef msuTrackAltNew As MsuTrackAlt)
                    Me.MsuTrackAltOld = msuTrackAltOld
                    Me.MsuTrackAltNew = msuTrackAltNew
                End Sub
            End Class

            Private Sub MsuTrack_TrackAltSwitched(ByVal sender As Object, ByVal e As TrackAltSwitchedEventArgs) Handles Me.TrackAltSwitched
                Call Me.Parent.RaiseEventTrackAltSwitched(sender, e)
            End Sub

            ''' <summary>
            ''' Reference to the parent object
            ''' </summary>
            <Newtonsoft.Json.JsonIgnore>
            Public Property Parent As MsuTracks
                Get
                    Return objTracks
                End Get
                Friend Set(value As MsuTracks)
                    objTracks = value
                End Set
            End Property

            <Newtonsoft.Json.JsonIgnore>
            Public Overrides ReadOnly Property MsuTrackConfig As MsuTracks
                Get
                    Return Me.Parent
                End Get
            End Property

            Private _TrackNumber As Byte

            Public Overrides Property TrackNumber As Byte
                Get
                    Return _TrackNumber
                End Get
                Set(value As Byte)
                    _TrackNumber = value
                End Set
            End Property

            ''' <summary>
            ''' Title / Name of this Track
            ''' </summary>
            Public Overrides Property Title As String

            ''' <summary>
            ''' <see cref="Array"/> containing all alt. Tracks for this Track.<br/>
            ''' This <see cref="Array"/> is stored inside the property <see cref="MsuTrack.TrackAltDict"/>
            ''' </summary>
            <Newtonsoft.Json.JsonProperty("track_alts")>
            Public Property TrackAltArray As MsuTrackAlt()
                Get
                    If Me.TrackAltDict IsNot Nothing Then
                        Return Me.TrackAltDict.Values.ToArray()
                    End If
                    Return Nothing
                End Get
                Set(trackAltArray As MsuTrackAlt())

                    If trackAltArray Is Nothing Then

                        Me.TrackAltDict = Nothing

                    Else
                        Dim trackAltDict As New SortedDictionary(Of UShort, MsuTrackAlt)

                        For Each trackAlt As MsuTrackAlt In trackAltArray

                            If trackAlt.Parent IsNot Me Then
                                trackAlt.Parent = Me
                            End If

                            If trackAltDict.ContainsKey(trackAlt.AltNumber) Then
                                Call trackAltDict.Add(
                                      key:=Me.GetFirstAvailableAltNum,
                                    value:=trackAlt)
                            Else
                                Call trackAltDict.Add(
                                      key:=trackAlt.AltNumber,
                                    value:=trackAlt)
                            End If
                        Next

                        Me.TrackAltDict = trackAltDict
                    End If
                End Set
            End Property

            ''' <summary>
            ''' Dictionary containing all alt. Tracks for this Track
            ''' </summary>
            <Newtonsoft.Json.JsonIgnore>
            Public Property TrackAltDict() As SortedDictionary(Of System.UInt16, MsuTrackAlt)

            ''' <summary>
            ''' Dictionary of all <see cref=" MsuTrackAlt" />s that will be set as current version automatically if this <see cref=" MsuTrack" /> is open / being played back.
            ''' The <see cref="TKey" /> is the <see cref="MsuTrack.TrackNumber" /> of the <see cref=" MsuTrackAlt.Parent" />.
            ''' </summary>
            ''' <exception cref="MsuAutoSwitchDataInvalidException"/>
            ''' <exception cref="MsuAutoSwitchDataDuplicateException"/>
            <Newtonsoft.Json.JsonIgnore>
            Public ReadOnly Property TrackAltAutoSwitchDict() As SortedDictionary(Of Byte, MsuTrackAlt)
                Get
                    Dim trackDict As SortedDictionary(Of Byte, MsuTrack) = Me.Parent.TrackDict
                    Dim trackAltAutoSwitch As New SortedDictionary(Of Byte, MsuTrackAlt)

                    ' Loop through all MsuTracks
                    For Each keyValuePair As KeyValuePair(Of Byte, MsuTrack) In trackDict

                        Dim msuTrack As MsuTrack = keyValuePair.Value

                        ' Look for alt. Track that has AutoSwitching enabled if this MsuTrack is playing
                        Dim msuTrackAlt As MsuTrackAlt = msuTrack.GetAutoSwitchAltTrackForTrack(Me)

                        If msuTrackAlt Is Nothing Then
                            ' None Found
                            Continue For
                        End If

                        ' Cannot have AutoSwitching for itself
                        If msuTrack Is Me Then
                            Throw New MsuAutoSwitchDataInvalidException(msuTrackAlt, True)
                        End If

                        Call trackAltAutoSwitch.Add(
                          key:=msuTrack.TrackNumber,
                        value:=msuTrackAlt)
                    Next

                    Return trackAltAutoSwitch
                End Get
            End Property

            ''' <returns>
            ''' <see cref="Array"/> of all alt. Tracks, that exist in their swap direcory.<br />
            ''' The only excluded alt. Track should be the one, that is currently used.
            ''' </returns>
            Public Function GetExistingAltTracks() As MsuTrackAlt()
                Dim msuTrackAltList As New List(Of MsuTrackAlt)

                For Each keyValuePair As Generic.KeyValuePair(Of UInt16, MsuTrackAlt) In Me.TrackAltDict

                    Dim msuTrackAlt As MsuTrackAlt = keyValuePair.Value

                    If msuTrackAlt.FilePathExists Then

                        Call msuTrackAltList.Add(msuTrackAlt)
                    End If
                Next
                Return msuTrackAltList.ToArray
            End Function

            ''' <exception cref="MsuAutoSwitchDataDuplicateException"/>
            Public Function GetAutoSwitchAltTrackForTrack(ByRef msuTrack As MsuTrack) As MsuTrackAlt
                Dim dictAsatft As SortedDictionary(Of System.UInt16, MsuTrackAlt)

                dictAsatft = Me.GetAutoSwitchAltTrackForTrackDict(msuTrack)

                If dictAsatft Is Nothing Then Return Nothing

                Select Case dictAsatft.Count
                    Case 0
                        Return Nothing
                    Case 1
                        Return dictAsatft.Values.First
                    Case Else
                        Throw New MsuAutoSwitchDataDuplicateException(dictAsatft.Values.ToArray, msuTrack)
                End Select
            End Function

            Public Function GetAutoSwitchAltTrackForTrackDict(ByRef msuTrack As MsuTrack) As SortedDictionary(Of System.UInt16, MsuTrackAlt)
                Dim dict As New SortedDictionary(Of System.UInt16, MsuTrackAlt)

                ' Loop through all MsuTrackAlts for this MsuTrack
                For Each keyValuePair As KeyValuePair(Of System.UInt16, MsuTrackAlt) In Me.TrackAltDict

                    ' Check if the MsuTrackAlt would switch automatically if the provided MsuTrack is being played back
                    If keyValuePair.Value.DoesAutoSwitchForTrack(msuTrack) Then

                        Call Dict.Add(keyValuePair.Key, keyValuePair.Value)
                    End If
                Next

                Return dict
            End Function

            ''' <summary>
            ''' Checks if any <see cref="MsuTrackAlt"/>s in <see cref="MsuTrack.TrackAltDict"/> have at least one element in the <see cref="MsuTrackAlt.AutoSwitchTrackNumbers"/> Array
            ''' </summary>
            <Newtonsoft.Json.JsonIgnore>
            Public ReadOnly Property HasAltTracksWithAutoSwitch As Boolean
                Get
                    ' Loop through all MsuTrackAlts for this MsuTrack
                    For Each keyValuePair As KeyValuePair(Of System.UInt16, MsuTrackAlt) In Me.TrackAltDict
                        ' Check if AutoSwitchTrackNumbers Array is filled
                        If keyValuePair.Value.AutoSwitchTrackNumbers IsNot Nothing Then
                            Return True
                        End If
                    Next
                    Return False
                End Get
            End Property

            ''' <summary>
            ''' Return if this Track is currently being played (File is open)
            ''' </summary>
            Public Overrides Function IsOpen() As Boolean
                Return MyBase.IsOpen()
            End Function

            ''' <summary>
            ''' Deletes the PCM File associated with the <see cref="MsuTrack"/><br/>
            ''' including all PCM Files of the <see cref="MsuTrackAlt"/>s inside <see cref="MsuTrack.TrackAltDict"/>
            ''' </summary>
            Public Overrides Sub Delete()

                If Me.FilePathExists Then
                    ' Open and close the main track file to make sure it can be deleted
                    Call Msu.MsuHelper.OpenAndCloseFile(Me.FilePath)
                End If

                Dim existingAltTracks = Me.GetExistingAltTracks

                For Each msuTrackAlt As MsuTrackAlt In existingAltTracks
                    ' Open and close the alt. track file to make sure it can be deleted
                    Call Msu.MsuHelper.OpenAndCloseFile(msuTrackAlt.FilePath)
                Next

                ' Delete the PCM files

                Call MyBase.Delete()

                For Each msuTrackAlt As MsuTrackAlt In existingAltTracks
                    Call msuTrackAlt.Delete(False)
                Next
            End Sub

            ''' <summary>
            ''' BaseName of the PCM-Track
            ''' </summary>
            <Newtonsoft.Json.JsonIgnore>
            Public Overrides ReadOnly Property BaseName As String
                Get
                    Return Me.Parent.PcmPrefix & Msu.MsuHelper.HyphenChar & Me.TrackNumber
                End Get
            End Property

            ''' <summary>
            ''' FileName of the PCM-Track
            ''' </summary>
            <Newtonsoft.Json.JsonIgnore>
            Public Overrides ReadOnly Property FileName As String
                Get
                    Return Me.BaseName & MsuHelper.PcmExtL
                End Get
            End Property

            ''' <summary>
            ''' Full FilePath of the current PCM-Track
            ''' </summary>
            <Newtonsoft.Json.JsonIgnore>
            Public Overrides ReadOnly Property FilePath As String
                Get
                    Return System.IO.Path.Join(Me.Parent.MsuLocation, Me.FileName)
                End Get
            End Property

            <Newtonsoft.Json.JsonIgnore>
            Public Overrides Property LocationRelative As String
                Get
                    Return MyBase.GetLocationRelative
                End Get
                Set(strLocation As String)
                    Throw New InvalidOperationException($"Cannot set Location on {NameOf(MsuTrack)} Object.{System.Environment.NewLine}Location is always {NameOf(MsuTracks.MsuLocation)} of the Parent {NameOf(MsuTracks)} Object.")
                End Set
            End Property

            <Newtonsoft.Json.JsonIgnore>
            Public Overrides Property LocationAbsolute As String
                Get
                    Return Me.Parent.MsuLocation
                End Get
                Set(value As String)
                    Throw New InvalidOperationException($"Cannot set Location on {NameOf(MsuTrack)} Object.{System.Environment.NewLine}Location is always {NameOf(MsuTracks.MsuLocation)} of the Parent {NameOf(MsuTracks)} Object.")
                End Set
            End Property

            ''' <exception cref="MsuTrackSwapDirectoryNotFoundException"/>
            ''' <exception cref="MsuAltTracksMultipleNotInSwapDirException"/>
            ''' <exception cref="MsuAltTracksAllInSwapDirException"/>
            Public Sub ValidateMsuTrackPaths()

                Select Case Me.TrackAltDict.Count
                    Case 0
                        ' No alt. Track Object exists.
                        ' At least one alt. Track Object is needed for the version currently in use.
                        Call Me.AddPcmTrack(Me.Parent.GetAltLocationForMainTrackVersion(), Me.Parent.Settings.TrackAltSettings.MsuTrackMainVersionTitle)
                    Case 1
                        ' No check needed for only one alt. Track Object
                        ' No alt. Tracks exist. Only the version currently in use exists.
                        Return
                End Select

                Dim currentMsuTrackAlt As MsuTrackAlt

                Try
                    currentMsuTrackAlt = Me.GetCurrentTrackAlt()
                Catch ex As MsuAltTracksMultipleNotInSwapDirException
                    currentMsuTrackAlt = Nothing
                End Try

                ' Check all swap directories for the alt. Tracks
                For Each keyValuePair As KeyValuePair(Of System.UInt16, MsuTrackAlt) In Me.TrackAltDict

                    Dim msuTrackAlt As MsuTrackAlt = keyValuePair.Value

                    ' Check if the Swap Directory exists
                    If msuTrackAlt.LocationExists() Then
                        Continue For
                    End If

                    If msuTrackAlt.TryFixLocationGoogleDriveZipRename() Then
                        Continue For
                    End If

                    If msuTrackAlt Is currentMsuTrackAlt Then
                        ' Call objTrackAlt.CreateLocation() ' Not needed. Directory will be created when this alt. Track is switched out
                        Continue For
                    End If

                    Throw New MsuTrackSwapDirectoryNotFoundException(msuTrackAlt)
                Next

                ' Check if the current alt. Track can be determined
                ' (Throws error if not)
                Call Me.GetCurrentTrackAlt()
            End Sub

            ''' <exception cref="MsuAltTracksMultipleNotInSwapDirException"/>
            ''' <exception cref="MsuAltTracksAllInSwapDirException"/>
            Public Function GetCurrentTrackAlt() As MsuTrackAlt
                Dim currentMsuTrackAlt As MsuTrackAlt = Nothing

                For Each keyValuePair As KeyValuePair(Of UShort, MsuTrackAlt) In Me.TrackAltDict

                    If keyValuePair.Value.FilePathExists() Then
                        ' Track is in swap directory
                    Else
                        If currentMsuTrackAlt Is Nothing Then

                            currentMsuTrackAlt = keyValuePair.Value
                        Else

                            ' Multiple alt. Tracks doesn't have the file in the specified location
                            ' Current track cannot be determined
                            Throw New MsuAltTracksMultipleNotInSwapDirException(Me, currentMsuTrackAlt, keyValuePair.Value)
                        End If
                    End If
                Next

                If currentMsuTrackAlt Is Nothing Then
                    ' All alt. Tracks have the file in the specified location
                    ' Current track cannot be determined
                    Throw New MsuAltTracksAllInSwapDirException(Me)
                End If

                ' Only one alt. Tracks doesn't have the file in the specified location
                ' Current track found
                Return currentMsuTrackAlt
            End Function

            ''' <exception cref="KeyNotFoundException"/>
            ''' <exception cref="MsuAltTracksMultipleNotInSwapDirException"/>
            ''' <exception cref="MsuTrackFileCannotBeMovedException"/>
            ''' <exception cref="MsuAltTracksAllInSwapDirException"/>
            Public Sub SetCurrentAltTrack(ByRef trackAltSet As UInt16)
                Call Me.SetCurrentAltTrack(Me.TrackAltDict.Item(trackAltSet))
            End Sub

            ''' <exception cref="MsuAltTracksMultipleNotInSwapDirException"/>
            ''' <exception cref="MsuTrackFileCannotBeMovedException"/>
            ''' <exception cref="MsuAltTracksAllInSwapDirException"/>
            Public Sub SetCurrentAltTrack(ByRef trackAltSet As MsuTrackAlt)

                If trackAltSet.Parent IsNot Me Then
                    Throw New System.ArgumentException(
                    message:=$"The parent of the alt. Track that is being switched to is invalid. " & System.Environment.NewLine &
                             $"TrackId of Parent: {trackAltSet.Parent.TrackNumber}" & System.Environment.NewLine &
                             $"Correct TrackId: {Me.TrackNumber}",
                    paramName:=NameOf(trackAltSet))
                    Return
                End If

                ' Get the current alt. Track for this TrackId
                Dim currentMsuTrackAlt As MsuTrackAlt = Me.GetCurrentTrackAlt

                If currentMsuTrackAlt Is trackAltSet Then
                    ' The alt. Track to switch to is already the current alt. track
                    Return
                End If

                Call Me.SetCurrentAltTrack(TrackAltSet:=trackAltSet, TrackAltCurrent:=currentMsuTrackAlt)
            End Sub

            Friend Sub SetCurrentAltTrack(ByRef trackAltSet As MsuTrackAlt, ByRef trackAltCurrent As MsuTrackAlt)

                If trackAltSet.Parent IsNot Me Then
                    Throw New System.ArgumentException(
                    message:=$"The parent of the alt. Track that is being switched to is invalid. " & System.Environment.NewLine &
                             $"TrackId of Parent: {trackAltSet.Parent.TrackNumber}" & System.Environment.NewLine &
                             $"Correct TrackId: {Me.TrackNumber}",
                    paramName:=NameOf(trackAltSet))
                End If

                If trackAltCurrent.Parent IsNot Me Then
                    Throw New System.ArgumentException(
                    message:=$"The parent of the alt. Track that is being switched out is invalid. " & System.Environment.NewLine &
                             $"TrackId of Parent: {trackAltCurrent.Parent.TrackNumber}" & System.Environment.NewLine &
                             $"Correct TrackId: {Me.TrackNumber}",
                    paramName:=NameOf(trackAltCurrent))
                End If

                If Msu.MsuHelper.FileIsLocked(Me.FilePath, System.IO.FileAccess.Read, System.IO.FileShare.None) Then
                    ' Current file cannot be moved. Is in use.
                    Throw New MsuCurrentTrackFileCannotBeMovedException(Me)
                End If

                If Msu.MsuHelper.FileIsLocked(trackAltSet.FilePath, System.IO.FileAccess.Read, System.IO.FileShare.None) Then
                    ' Swap file cannot be moved. Is in use.
                    Throw New MsuSwapTrackFileCannotBeMovedException(trackAltSet)
                End If

                Dim currentTrackNormalVersionExists = Me.FilePathWithNormalVersionSuffixExists
                Dim trackAltSetNormalVersionExists = trackAltSet.FilePathWithNormalVersionSuffixExists

                If currentTrackNormalVersionExists Then
                    If Msu.MsuHelper.FileIsLocked(Me.FilePathWithNormalVersionSuffix, System.IO.FileAccess.Read, System.IO.FileShare.None) Then
                        ' Normal version of current file cannot be moved. Is in use.
                        Throw New MsuCurrentTrackFileCannotBeMovedException(Me)
                    End If
                End If
                If trackAltSetNormalVersionExists Then
                    If Msu.MsuHelper.FileIsLocked(trackAltSet.FilePathWithNormalVersionSuffix, System.IO.FileAccess.Read, System.IO.FileShare.None) Then
                        ' Normal version of swap file cannot be moved. Is in use.
                        Throw New MsuSwapTrackFileCannotBeMovedException(trackAltSet)
                    End If
                End If

                If trackAltCurrent.LocationExists Then
                Else
                    ' Create the dicectory where the alt. Track will be moved to
                    Call trackAltCurrent.CreateLocation()
                End If

                Call System.IO.File.Replace(
                sourceFileName:=trackAltSet.FilePath,
           destinationFileName:=Me.FilePath,
     destinationBackupFileName:=trackAltCurrent.FilePath)

                If currentTrackNormalVersionExists Then
                    If trackAltSetNormalVersionExists Then
                        ' Both have a normal version

                        Call System.IO.File.Replace(
                        sourceFileName:=trackAltSet.FilePathWithNormalVersionSuffix,
                   destinationFileName:=Me.FilePathWithNormalVersionSuffix,
             destinationBackupFileName:=trackAltCurrent.FilePathWithNormalVersionSuffix)

                    Else
                        ' Only the current alt. Track has a normal version

                        Call System.IO.File.Replace(
                        sourceFileName:=Me.FilePathWithNormalVersionSuffix,
                   destinationFileName:=trackAltCurrent.FilePathWithNormalVersionSuffix,
             destinationBackupFileName:=String.Concat(trackAltCurrent.FilePathWithNormalVersionSuffix, "_"c))

                    End If
                Else
                    If trackAltSetNormalVersionExists Then
                        ' Only the alt. Track to set has normal version

                        Call System.IO.File.Replace(
                        sourceFileName:=trackAltSet.FilePathWithNormalVersionSuffix,
                   destinationFileName:=Me.FilePathWithNormalVersionSuffix,
             destinationBackupFileName:=String.Concat(Me.FilePathWithNormalVersionSuffix, "_"c))

                    Else
                        ' Tracks are not converted
                    End If
                End If

                RaiseEvent TrackAltSwitched(Me, (New TrackAltSwitchedEventArgs(msuTrackAltOld:=trackAltCurrent, msuTrackAltNew:=trackAltSet)))
            End Sub

            Public Function TrackFileWithNormalVersionSuffixExists() As Boolean

                If Me.FilePathWithNormalVersionSuffixExists Then
                    Return True
                End If

                For Each msuTrackAlt As MsuTrackAlt In Me.TrackAltDict.Values

                    If msuTrackAlt.FilePathWithNormalVersionSuffixExists Then
                        Return True
                    End If
                Next

                Return False
            End Function

            Public Function LocationExistsAsAltTrack(ByRef locationAbsolute As String) As Boolean
                Return Me.GetAltTrackByLocation(locationAbsolute) IsNot Nothing
            End Function

            Public Function GetAltTrackByLocation(ByRef locationAbsolute As String) As MsuTrackAlt
                Dim locationAbsoluteRemUncPref = Msu.MsuHelper.PathRemoveUncLocalPref(locationAbsolute)

                ' Go through all alt. Tracks for this Id
                For Each keyValuePair As KeyValuePair(Of UShort, MsuTrackAlt) In Me.TrackAltDict

                    Dim locationAbsoluteCompareRemUncPref = Msu.MsuHelper.PathRemoveUncLocalPref(keyValuePair.Value.LocationAbsolute)

                    If locationAbsoluteCompareRemUncPref Is Nothing Then
                        If locationAbsoluteRemUncPref Is Nothing Then
                            Return keyValuePair.Value
                        Else
                            Continue For
                        End If
                    End If

                    ' Return the AltTrack, if the Absolute Location matches
                    If locationAbsoluteCompareRemUncPref.Equals(locationAbsoluteRemUncPref, StringComparison.OrdinalIgnoreCase) Then
                        Return keyValuePair.Value
                    End If
                Next

                Return Nothing
            End Function

            Public Sub AddPcmTrack(ByRef locationAbsolute As String)
                Call AddPcmTrack(locationAbsolute:=locationAbsolute, title:=System.IO.Path.GetFileName(locationAbsolute))
            End Sub

            Public Sub AddPcmTrack(ByRef locationAbsolute As String, ByRef title As String)

                Dim msuTrackAlt As New MsuTrackAlt(
                      Parent:=Me,
                   AltNumber:=Me.GetFirstAvailableAltNum,
                    Location:=Msu.MsuHelper.GetCopyPathPrefix(Me.Parent.MsuLocation, locationAbsolute),
                       Title:=title)

                Call AddPcmTrack(msuTrackAlt)
            End Sub

            Public Sub AddPcmTrack(ByRef msuTrackAlt As MsuTrackAlt)

                If msuTrackAlt.Parent Is Nothing Then
                    msuTrackAlt.Parent = Me
                ElseIf msuTrackAlt.Parent IsNot Me Then
                    Throw New System.ArgumentException(
                    message:=$"The parent of the object does not match the Track where it is being added to. " & System.Environment.NewLine &
                             $"TrackId of Parent: {msuTrackAlt.Parent.TrackNumber}" & System.Environment.NewLine &
                             $"Correct TrackId: {Me.TrackNumber}",
                    paramName:=NameOf(msuTrackAlt))
                End If

                Call Me.TrackAltDict.Add(msuTrackAlt.AltNumber, msuTrackAlt)

                Dim Log As String = $"Added to Track {Me.TrackNumber} "

                If String.IsNullOrWhiteSpace(Me.Title) Then
                Else
                    Log = Log & $"(""{Me.Title}"") "
                End If

                Log = Log & $"the alt. Track {msuTrackAlt.AltNumber} "

                If String.IsNullOrWhiteSpace(msuTrackAlt.Title) Then
                Else
                    Log = Log & $"(""{msuTrackAlt.Title}"") "
                End If

                Log = Log & $"with relative path: ""{msuTrackAlt.LocationRelative}"""

                Call Me.AddToLog(Log)
            End Sub

            Public Function GetFirstAvailableAltNum() As UShort
                GetFirstAvailableAltNum = MsuHelper.ZeroByte

                While Me.TrackAltDict.ContainsKey(GetFirstAvailableAltNum)
                    GetFirstAvailableAltNum += MsuHelper.OneByte
                End While
            End Function

            Public Sub New(ByRef parent As MsuTracks, ByRef trackNumber As Byte)
                Me.Parent = parent
                Me.TrackNumber = trackNumber
                Initialize()
            End Sub
            Private Sub New()
                Initialize()
            End Sub

            Private Sub Initialize()
                Me.TrackAltDict() = New SortedDictionary(Of UShort, MsuTrackAlt)
            End Sub
            Friend Sub SetParentObjectsOfChildren()
                For Each keyValuePair As KeyValuePair(Of UShort, MsuTrackAlt) In Me.TrackAltDict
                    Dim msuTrackAlt As MsuTrackAlt = keyValuePair.Value
                    msuTrackAlt.Parent = Me
                Next
            End Sub
            Friend Sub CalculateAbsoluteLocationForAltTracks()
                For Each keyValuePair As KeyValuePair(Of UShort, MsuTrackAlt) In Me.TrackAltDict
                    Call keyValuePair.Value.CalculateAbsoluteLocation()
                Next
            End Sub

            Friend Sub AddToLog(ByRef text As String)
                If Me.Parent IsNot Nothing Then Call Me.Parent.AddToLog(text)
            End Sub

            Friend Sub AddToLog(ByRef text As String, ByRef entryColor As System.Drawing.Color)
                If Me.Parent IsNot Nothing Then Call Me.Parent.AddToLog(text, entryColor)
            End Sub

            Protected Overridable Sub Dispose(disposing As Boolean)
                If Not disposedValue Then
                    If disposing Then
                        ' dispose managed state (managed objects)

                        If Me.TrackAltDict IsNot Nothing Then

                            ' Dispose of each Alt. Track Object
                            For Each keyValuePair As KeyValuePair(Of UShort, MsuTrackAlt) In Me.TrackAltDict

                                Dim msuTrackAlt As MsuTrackAlt = keyValuePair.Value

                                Call msuTrackAlt.Dispose()
                            Next

                            ' Remove reference to Dictionary conaining all alt. Tracks
                            Me.TrackAltDict = Nothing

                            ' Remove reference to parent object
                            Me.Parent = Nothing
                        End If
                    End If

                    ' free unmanaged resources (unmanaged objects) and override finalizer
                    ' set large fields to null
                    disposedValue = True
                End If
            End Sub

            ' ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
            ' Protected Overrides Sub Finalize()
            '     ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
            '     Dispose(disposing:=False)
            '     MyBase.Finalize()
            ' End Sub

            Public Sub Dispose() Implements IDisposable.Dispose
                ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
                Dispose(disposing:=True)
                GC.SuppressFinalize(Me)
            End Sub
        End Class

        Public Class MsuTrackAlt : Inherits MsuPcmFile : Implements IDisposable
            Private _SavedLocation As String = Constants.vbNullString
            Private disposedValue As Boolean
            Private _MsuTrack As MsuTrack

            ''' <summary>
            ''' Reference to the parent object
            ''' </summary>
            <Newtonsoft.Json.JsonIgnore>
            Public Property Parent As MsuTrack
                Get
                    Return _MsuTrack
                End Get
                Friend Set(value As MsuTrack)
                    _MsuTrack = value
                End Set
            End Property

            <Newtonsoft.Json.JsonIgnore>
            Public Overrides ReadOnly Property MsuTrackConfig As MsuTracks
                Get
                    Return Me.Parent.Parent
                End Get
            End Property

            <Newtonsoft.Json.JsonIgnore>
            Public Overrides Property TrackNumber As Byte
                Get
                    Return Me.Parent.TrackNumber
                End Get
                Set(value As Byte)
                    Throw New InvalidOperationException($"Cannot set {NameOf(Me.TrackNumber)} on {NameOf(MsuTrackAlt)} Object.")
                End Set
            End Property

            ''' <summary>
            ''' Id used to identify the specific alt. Track
            ''' </summary>
            <Newtonsoft.Json.JsonProperty("alt_number")>
            Public Property AltNumber As UShort

            ''' <summary>
            ''' Title / Name of the alt. Track
            ''' </summary>
            Public Overrides Property Title As String

            ''' <summary>
            ''' Property only used for JSON Serialization / Deserialization!
            ''' Makes sure, that the location value stored inside the JSON file is unchanged when saving the file again.
            ''' </summary>
            <Newtonsoft.Json.JsonProperty("location")>
            Public Property SavedLocation As String
                Get
                    If String.IsNullOrEmpty(_SavedLocation) Then
                        Return Me.LocationRelative
                    Else
                        Return _SavedLocation
                    End If
                End Get
                Set(savedLocationNew As String)
                    _SavedLocation = savedLocationNew
                End Set
            End Property

            ''' <summary>
            ''' If the specified location is a sub path of the working directory, returns the location relative to that.
            ''' Returns the absolute path if not.
            ''' </summary>
            <Newtonsoft.Json.JsonIgnore>
            Public Overrides Property LocationRelative As String
                Get
                    Return MyBase.GetLocationRelative
                End Get
                Set(locationNew As String)
                    If locationNew IsNot Nothing AndAlso locationNew.Length <> 0 Then
                        If Me.Parent IsNot Nothing AndAlso Me.Parent.Parent IsNot Nothing AndAlso System.IO.Directory.Exists(Parent.Parent.MsuLocation) Then
                            Me.LocationAbsolute = System.IO.Path.GetFullPath(locationNew, Parent.Parent.MsuLocation)
                        Else
                            _SavedLocation = locationNew
                        End If
                    End If
                End Set
            End Property

            <Newtonsoft.Json.JsonIgnore>
            Public Overrides Property LocationAbsolute As String

            ''' <summary>
            ''' Removes the value inside <see cref="SavedLocation"/>. Meaning <see cref="LocationRelative"/> will be used for saving.
            ''' </summary>
            Public Sub UpdateSavedLocation()
                _SavedLocation = Constants.vbNullString
            End Sub

            Friend Sub CalculateAbsoluteLocation()
                Me.LocationRelative = _SavedLocation
            End Sub

            ''' <returns>
            ''' The directory that contains the alt. Track exists
            ''' </returns>
            Public Function LocationExists() As Boolean
                Return System.IO.Directory.Exists(Me.LocationAbsolute)
            End Function

            ''' <summary>
            ''' When zipped by GoogleDrive some characters get replaced by an "_".<br/>
            ''' Tries to find these invalid folders and renames them to the one loaded from the JSON config
            ''' </summary>
            ''' <remarks>test
            ''' </remarks>
            Friend Function TryFixLocationGoogleDriveZipRename() As Boolean
                Dim dictPathNameParts As Specialized.StringCollection = GetPathPartNamesStringCollection(Me.LocationRelative)
                Dim prevDirInf As New System.IO.DirectoryInfo(Me.Parent.Parent.MsuLocation)

                For i As Integer = dictPathNameParts.Count - 1 To 0 Step -1

                    Dim currentDirInf As New System.IO.DirectoryInfo(System.IO.Path.Join(prevDirInf.FullName, dictPathNameParts(i)))

                    If currentDirInf.Exists() Then
                        GoTo NextPath
                    End If

                    Dim currentNamePlaceholder As String = GetPlaceholderForInvalidGoogleDriveChars(dictPathNameParts(i))

                    Dim subDirsInfo As System.IO.DirectoryInfo() = prevDirInf.GetDirectories

                    For Each subDirInfo As System.IO.DirectoryInfo In subDirsInfo

                        Dim subDirName As String = System.IO.Path.GetFileName(subDirInfo.FullName)

                        If subDirName Like currentNamePlaceholder Then

                            Call System.IO.Directory.Move(
                          sourceDirName:=System.IO.Path.Join(subDirInfo.FullName),
                            destDirName:=System.IO.Path.Join(prevDirInf.FullName, dictPathNameParts(i)))
                            GoTo NextPath
                        End If
                    Next

                    Return False
NextPath:
                    prevDirInf = currentDirInf
                Next

                Return True
            End Function

            Friend Sub CreateLocation()
                Dim dictPathNameParts As Specialized.StringCollection = GetPathPartNamesStringCollection(Me.LocationRelative)
                Dim currentDirInf As New System.IO.DirectoryInfo(Me.Parent.Parent.MsuLocation)

                For i As Integer = DictPathNameParts.Count - 1 To 0 Step -1

                    currentDirInf = New System.IO.DirectoryInfo(System.IO.Path.Join(currentDirInf.FullName, dictPathNameParts(i)))

                    If currentDirInf.Exists() Then
                        Continue For
                    End If

                    Call currentDirInf.Create()
                Next
            End Sub

            ''' <summary>
            ''' Just used to sort and remove duplicates from <see cref="MsuTrackAlt.AutoSwitchTrackNumbers">AutoSwitchTrackNumbers</see>
            ''' </summary>
            Private Property AutoSwitchTrackNumbersDictKey As SortedDictionary(Of Byte, Boolean)

            ''' <summary>
            ''' If (one of) the MSU1 Track-Id(s) is being used (.pcm file is locked), switch to this alt. Track automatically
            ''' </summary>
            <Newtonsoft.Json.JsonIgnore>
            Public Property AutoSwitchTrackNumbers As Byte()
                Get
                    If AutoSwitchTrackNumbersDictKey Is Nothing Then
                        Return Nothing
                    Else
                        Return AutoSwitchTrackNumbersDictKey.Keys.ToArray()
                    End If
                End Get
                Set(value As Byte())
                    If value Is Nothing Then
                        AutoSwitchTrackNumbersDictKey = Nothing
                        Return
                    End If

                    Dim dict As New SortedDictionary(Of Byte, Boolean)

                    For i = LBound(value) To UBound(value)
                        If dict.ContainsKey(value(i)) Then
                            Continue For ' Duplicate
                        End If

                        Call dict.Add(value(i), Nothing)
                    Next

                    Me.AutoSwitchTrackNumbersDictKey = dict
                End Set
            End Property

            ''' <summary>
            ''' Only used for Serialization of <see cref="AutoSwitchTrackNumbers"/>.
            ''' The <see cref="Newtonsoft.Json.JsonSerializer"/> always converts <see cref="Byte"/>Arrays to Base64.
            ''' </summary>
            <Newtonsoft.Json.JsonProperty("auto_switch_track_numbers")>
            Public Property AutoSwitchTrackNumbersInt As System.UInt16()
                Get
                    If Me.AutoSwitchTrackNumbers Is Nothing Then
                        Return Nothing
                    End If
                    Dim bytUbound As Byte = CByte(Information.UBound(Me.AutoSwitchTrackNumbers))

                    Dim intAutoSwitchTrackNumbers(bytUbound) As System.UInt16

                    For i As Byte = 0 To bytUbound
                        intAutoSwitchTrackNumbers(i) = Me.AutoSwitchTrackNumbers(i)
                    Next

                    Return intAutoSwitchTrackNumbers
                End Get
                Set(intAutoSwitchTrackNumbers As System.UInt16())
                    If intAutoSwitchTrackNumbers Is Nothing Then
                        Me.AutoSwitchTrackNumbers = Nothing
                        Return
                    End If
                    Dim bytUbound As Byte = CByte(Information.UBound(intAutoSwitchTrackNumbers))

                    Dim bytAutoSwitchTrackNumbers(bytUbound) As System.Byte

                    For i As Byte = 0 To bytUbound
                        bytAutoSwitchTrackNumbers(i) = CByte(intAutoSwitchTrackNumbers(i))
                    Next
                    Me.AutoSwitchTrackNumbers = bytAutoSwitchTrackNumbers
                End Set
            End Property

            Public Function ShouldSerializeAutoSwitchTrackNumbersInt() As Boolean
                Return Me.AutoSwitchTrackNumbersDictKey IsNot Nothing
            End Function

            ''' <summary>
            ''' Property <see cref="AutoSwitchTrackNumbers"/> as a JSON Array.
            ''' </summary>
            <Newtonsoft.Json.JsonIgnore>
            Public Property AutoSwitchTrackNumbersJson As String
                Get
                    If Me.AutoSwitchTrackNumbers Is Nothing Then
                        Return Constants.vbNullString
                    Else
                        Dim settings As New Newtonsoft.Json.JsonSerializerSettings With {
                    .Formatting = Newtonsoft.Json.Formatting.None
                }

                        Dim text As String =
                  Newtonsoft.Json.JsonConvert.SerializeObject(
                     value:=Me.AutoSwitchTrackNumbersInt,
                  settings:=settings)

                        Return Text
                    End If
                End Get
                Set(value As String)

                    If String.IsNullOrWhiteSpace(value) Then
                        Me.AutoSwitchTrackNumbers = Nothing
                        Return
                    End If

                    Dim valueTrim As String = Strings.LTrim(value)
                    Dim strArr As String ' = Constants.vbNullString

                    Select Case Convert.ToInt16(ValueTrim.Chars(0))
                        Case 91 ' [
                            ' Array already contains the brackets
                            strArr = valueTrim
                        Case Else
                            ' Add brackets
                            strArr = "["c & valueTrim & "]"c
                    End Select

                    Me.AutoSwitchTrackNumbers =
                Newtonsoft.Json.JsonConvert.DeserializeObject(Of Byte())(strArr)
                End Set
            End Property

            ''' <summary>
            ''' Deletes the PCM File associated with the <see cref="MsuTrackAlt"/>
            ''' </summary>
            ''' <param name="UnSetAsCurrentAltTrack">Sets another <see cref="MsuTrackAlt"/> as the current Track, if this  <see cref="MsuTrackAlt"/> is the current Track</param>
            Public Overloads Sub Delete(ByRef unSetAsCurrentAltTrack As Boolean)
                If unSetAsCurrentAltTrack Then
                    Call Me.UnSetAsCurrentAltTrack()
                End If
                Call MyBase.Delete()
            End Sub

            ''' <summary>
            ''' Deletes the PCM File associated with the <see cref="MsuTrackAlt"/><br/>
            ''' Before deleting: Sets another <see cref="MsuTrackAlt"/> as the current Track, if this  <see cref="MsuTrackAlt"/>
            ''' </summary>
            Public Overrides Sub Delete()
                Call Me.Delete(True)
            End Sub

            ''' <summary>
            ''' Sets other alt. Track as the current Track (First available to use)
            ''' </summary>
            Public Sub UnSetAsCurrentAltTrack()
                If Me.FilePathExists Then
                    Return
                End If

                Dim currentAltTrack = Me.Parent.GetCurrentTrackAlt

                If currentAltTrack IsNot Me Then
                    Throw New Exception($"alt. Track {Me.AltNumber} to delete not is swap directory, but also not the currently used alt. Track {currentAltTrack.AltNumber}")
                End If

                ' This alt. Track is set as the current Track
                ' Set other alt. Track as the current Track before deleting

                Dim trackAltKeys = Me.Parent.TrackAltDict.Keys.ToArray
                Dim i As UInt16 = 0

                Do
                    Dim trackAltKey = trackAltKeys(i)
                    Dim trackAlt = Me.Parent.TrackAltDict(trackAltKey)

                    If TrackAlt.FilePathExists Then
                        Call Me.Parent.SetCurrentAltTrack(trackAltSet:=trackAlt, trackAltCurrent:=currentAltTrack)
                        currentAltTrack = Me.Parent.GetCurrentTrackAlt
                    End If

                    i = i + MsuHelper.OneByte
                Loop While currentAltTrack Is Me
            End Sub

            ''' <summary>
            ''' Checks if the provided <see cref="MsuTrack.TrackNumber"/> is inside <see cref="MsuTrackAlt.AutoSwitchTrackNumbers"/> Array.
            ''' </summary>
            ''' <returns>
            ''' Will / will not switch automatically to this <see cref="MsuTrackAlt"/>, if the provided <see cref="MsuTrack.TrackNumber"/> is being played back.
            ''' </returns>
            ''' <param name="MsuTrack"><see cref="MsuTrack"/> to check</param>
            Public Function DoesAutoSwitchForTrack(ByRef msuTrack As MsuTrack) As Boolean
                Return DoesAutoSwitchForTrack(msuTrack.TrackNumber)
            End Function

            ''' <summary>
            ''' Checks if the provided <see cref="MsuTrack.TrackNumber"/> is inside <see cref="MsuTrackAlt.AutoSwitchTrackNumbers"/> Array.
            ''' </summary>
            ''' <returns>
            ''' Will / will not switch automatically to this <see cref="MsuTrackAlt"/>, if the provided <see cref="MsuTrack.TrackNumber"/> is being played back.
            ''' </returns>
            ''' <param name="TrackNumber"><see cref="MsuTrack.TrackNumber"/> to check</param>
            Public Function DoesAutoSwitchForTrack(ByRef trackNumber As Byte) As Boolean

                If Me.AutoSwitchTrackNumbers IsNot Nothing Then
                    For i As Integer = LBound(Me.AutoSwitchTrackNumbers) To UBound(Me.AutoSwitchTrackNumbers)

                        If Me.AutoSwitchTrackNumbers(i) = trackNumber Then
                            Return True
                        End If
                    Next
                End If
                Return False
            End Function

            <Newtonsoft.Json.JsonIgnore>
            Public Overrides ReadOnly Property FilePath As String
                Get
                    Return Me.GetFilePath()
                End Get
            End Property

            <Newtonsoft.Json.JsonIgnore>
            Public Overrides ReadOnly Property BaseName As String
                Get
                    Return Me.Parent.BaseName
                End Get
            End Property

            <Newtonsoft.Json.JsonIgnore>
            Public Overrides ReadOnly Property FileName As String
                Get
                    Return Me.Parent.FileName
                End Get
            End Property

            ''' <returns>
            ''' Full file path of this alt. Track
            ''' </returns>
            Private Overloads Function GetFilePath() As String
                Return MyBase.GetFilePath(Me.Parent)
            End Function

            ''' <summary>
            ''' Checks if the file at <see cref="MsuTrackAlt.FilePath"/> exists.
            ''' </summary>
            ''' <returns>
            ''' True:  Alt. Track is currently inside the specified Location<br/>
            ''' False: Alt. Track is currently used as the main track (not inside specified Location)
            ''' </returns>
            Public Overrides Function FilePathExists() As Boolean
                Return MyBase.FilePathExists()
            End Function

            Public Sub SetAsCurrentAltTrack()
                Call Parent.SetCurrentAltTrack(Me)
            End Sub

            Friend Sub SetAsCurrentAltTrack(ByRef trackAltCurrent As MsuTrackAlt)
                Call Parent.SetCurrentAltTrack(Me, trackAltCurrent)
            End Sub

            Public Sub New(ByRef parent As MsuTrack, ByRef altNumber As UShort, ByRef location As String, ByRef title As String)
                Me.Parent = parent
                Me.AltNumber = altNumber
                Me.LocationRelative = location
                Me.Title = title
            End Sub
            Private Sub New()
            End Sub

            Friend Sub AddToLog(ByRef text As String)
                If Me.Parent IsNot Nothing Then Call Me.Parent.AddToLog(text)
            End Sub

            Friend Sub AddToLog(ByRef text As String, ByRef entryColor As System.Drawing.Color)
                If Me.Parent IsNot Nothing Then Call Me.Parent.AddToLog(text, entryColor)
            End Sub

            Protected Overridable Sub Dispose(disposing As Boolean)
                If Not disposedValue Then
                    If disposing Then
                        ' dispose managed state (managed objects)
                        Parent = Nothing ' Remove reference to parent object
                    End If

                    ' free unmanaged resources (unmanaged objects) and override finalizer
                    ' set large fields to null
                    disposedValue = True
                End If
            End Sub

            ' ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
            ' Protected Overrides Sub Finalize()
            '     ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
            '     Dispose(disposing:=False)
            '     MyBase.Finalize()
            ' End Sub

            Public Sub Dispose() Implements IDisposable.Dispose
                ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
                Dispose(disposing:=True)
                GC.SuppressFinalize(Me)
            End Sub
        End Class

        Public Class MsuPcmBulkConversion

            Public ReadOnly Property MsuPcmFiles As MsuPcmFile()

            Public ReadOnly Property SampleRate As UInt32

            Public ReadOnly Property VolumePercentage As UInt16

            Public ReadOnly Property MsuTracksConfig As MsuTracks

            Public ReadOnly Property ProcessWindowStyle As System.Diagnostics.ProcessWindowStyle

            Public ReadOnly Property KeepProcessesOpen As Boolean

            Private ReadOnly Property Logger As Logger.Logger

            Public ReadOnly Property ProcessCount As Byte

            Public ReadOnly Property ParentHandle As IntPtr

            Protected Class MsuPcmConversionParameters
                Public Property LoopPointOrig As UInt32
                Public Property LoopPointConverted As UInt32
                Public Property FilePathConverted As String
                Public Property ConversionProcessStartInfo As ProcessStartInfo
            End Class

            Public Sub New(ByRef msuTracksConfig As MsuTracks, ByVal msuPcmFiles() As MsuPcmFile, ByRef sampleRate As UInt32, ByRef volumePercentage As UInt16, ByRef processWindowStyle As System.Diagnostics.ProcessWindowStyle, ByRef keepProcessesOpen As Boolean, ByRef parentHandle As IntPtr, ByRef processCount As Byte)
                If msuPcmFiles Is Nothing OrElse msuPcmFiles.Length = 0 Then
                    Throw New ArgumentNullException(NameOf(msuPcmFiles))
                End If
                Call ArgumentNullException.ThrowIfNull(msuTracksConfig, NameOf(msuTracksConfig))
                Call ArgumentNullException.ThrowIfNull(msuTracksConfig.Settings, NameOf(msuTracksConfig.Settings))
                If String.IsNullOrWhiteSpace(msuTracksConfig.Settings.AudioConversionSettings.MsuPcmPath) _
                OrElse (Not System.IO.File.Exists(msuTracksConfig.Settings.AudioConversionSettings.MsuPcmPath)) Then
                    Throw New System.IO.FileNotFoundException("MCUPCM++ not found.", msuTracksConfig.Settings.AudioConversionSettings.MsuPcmPath)
                End If
                If keepProcessesOpen And processWindowStyle = ProcessWindowStyle.Hidden Then
                    Throw New ArgumentException($"The flag {NameOf(keepProcessesOpen)} cannot be set to true while the {NameOf(processWindowStyle)} is set to {processWindowStyle.ToString}")
                End If
                If processCount < 1 OrElse processCount > MAXIMUM_WAIT_OBJECTS Then
                    Throw New ArgumentException($"Amount of processes is invalid. Must be between 1 and {MAXIMUM_WAIT_OBJECTS} ({NameOf(MAXIMUM_WAIT_OBJECTS)})", NameOf(processCount))
                End If

                Me.MsuTracksConfig = msuTracksConfig
                Me.Logger = msuTracksConfig.Logger
                Me.MsuPcmFiles = msuPcmFiles
                Me.SampleRate = sampleRate
                Me.VolumePercentage = volumePercentage
                Me.ProcessWindowStyle = processWindowStyle
                Me.KeepProcessesOpen = keepProcessesOpen
                Me.ParentHandle = parentHandle
                Me.ProcessCount = processCount
                Call ValidateConversionParameters()
            End Sub

            Public Shared Function NewFromExistingPcmTracksInConfig(ByRef msuTracksConfig As MsuTracks, ByRef sampleRate As UInt32, ByRef volumePercentage As UInt16) As MsuPcmBulkConversion
                Return NewFromExistingPcmTracksInConfig(msuTracksConfig:=msuTracksConfig, sampleRate:=sampleRate, VolumePercentage:=volumePercentage, ProcessWindowStyle:=ProcessWindowStyle.Normal)
            End Function

            Public Shared Function NewFromExistingPcmTracksInConfig(ByRef msuTracksConfig As MsuTracks, ByRef sampleRate As UInt32, ByRef volumePercentage As UInt16, ByRef processWindowStyle As System.Diagnostics.ProcessWindowStyle) As MsuPcmBulkConversion
                Return NewFromExistingPcmTracksInConfig(msuTracksConfig:=msuTracksConfig, sampleRate:=sampleRate, volumePercentage:=volumePercentage, ProcessWindowStyle:=processWindowStyle, KeepProcessesOpen:=False)
            End Function

            Public Shared Function NewFromExistingPcmTracksInConfig(ByRef msuTracksConfig As MsuTracks, ByRef sampleRate As UInt32, ByRef volumePercentage As UInt16, ByRef processWindowStyle As System.Diagnostics.ProcessWindowStyle, ByRef keepProcessesOpen As Boolean) As MsuPcmBulkConversion
                Return NewFromExistingPcmTracksInConfig(msuTracksConfig:=msuTracksConfig, sampleRate:=sampleRate, volumePercentage:=volumePercentage, processWindowStyle:=processWindowStyle, keepProcessesOpen:=keepProcessesOpen, ParentHandle:=IntPtr.Zero)
            End Function

            Public Shared Function NewFromExistingPcmTracksInConfig(ByRef msuTracksConfig As MsuTracks, ByRef sampleRate As UInt32, ByRef volumePercentage As UInt16, ByRef processWindowStyle As System.Diagnostics.ProcessWindowStyle, ByRef keepProcessesOpen As Boolean, ByRef parentHandle As IntPtr) As MsuPcmBulkConversion
                Return NewFromExistingPcmTracksInConfig(msuTracksConfig:=msuTracksConfig, sampleRate:=sampleRate, volumePercentage:=volumePercentage, processWindowStyle:=processWindowStyle, keepProcessesOpen:=keepProcessesOpen, ParentHandle:=parentHandle, ProcessCount:=CByte(Math.Ceiling(CDec(System.Environment.ProcessorCount / 2))))
            End Function

            Public Shared Function NewFromExistingPcmTracksInConfig(ByRef msuTracksConfig As MsuTracks, ByRef sampleRate As UInt32, ByRef volumePercentage As UInt16, ByRef processWindowStyle As System.Diagnostics.ProcessWindowStyle, ByRef keepProcessesOpen As Boolean, ByRef parentHandle As IntPtr, ByRef processCount As Byte) As MsuPcmBulkConversion
                ArgumentNullException.ThrowIfNull(msuTracksConfig)
                Return New MsuPcmBulkConversion(
                msuTracksConfig:=msuTracksConfig,
                    msuPcmFiles:=msuTracksConfig.GetExistingPcmTracks,
                     sampleRate:=sampleRate,
               volumePercentage:=volumePercentage,
             processWindowStyle:=processWindowStyle,
              keepProcessesOpen:=keepProcessesOpen,
                   parentHandle:=parentHandle,
                   processCount:=processCount)
            End Function

            Protected Sub ValidateConversionParameters()
                Select Case Me.SampleRate
                    Case < 1000, > 1000000
                        Throw New System.ArgumentOutOfRangeException(NameOf(Me.SampleRate), Me.SampleRate, "The value for sample rate is invalid!" & System.Environment.NewLine & "Only values from 1000 to 1000000 are accepted.")
                    Case 44100
                        If Me.VolumePercentage.Equals(CUShort(100)) Then
                            Throw New ArgumentException($"Sample rate ({Me.SampleRate}) and volume ({Me.VolumePercentage}) are unchanged." & System.Environment.NewLine & "Output files would be identical!")
                        End If
                End Select
            End Sub

            Public Delegate Sub ExecuteCallback()

            Public Sub Execute()
                Dim dictFileParams As Dictionary(Of MsuPcmFile, MsuPcmConversionParameters) = Me.GetDictionaryWithConversionParameters
                Dim processes As Process()
                ReDim processes(Me.ProcessCount - MsuHelper.OneByte)

                Call AddToLog($"Start converting the PCM files via MSUPCM++")

                For Each keyValuePair As KeyValuePair(Of MsuPcmFile, MsuPcmConversionParameters) In dictFileParams

                    Dim processArrayPos = GetAvailableProcessInArray(processes)

                    If processes(ProcessArrayPos) IsNot Nothing Then
                        Call processes(ProcessArrayPos).Dispose()
                    End If

                    Call AddToLog($"Creating converted file ""{System.IO.Path.GetRelativePath(relativeTo:=Me.MsuTracksConfig.MsuLocation, path:=keyValuePair.Value.FilePathConverted)}""")

                    Dim conversionProcessStartInfo = keyValuePair.Value.ConversionProcessStartInfo

                    processes(ProcessArrayPos) = Process.Start(ConversionProcessStartInfo)
                Next

                Call WaitUntilProcessesAreFinished(processes)
                Call AddToLog($"Finished converting the PCM files with MSUPCM++")

                Call WriteLoopPointsToConvertedFiles(dictFileParams)

                Call SwitchToConvertedFiles(dictFileParams)
            End Sub

            Protected Sub SwitchToConvertedFiles(ByRef dictFileParams As Dictionary(Of MsuPcmFile, MsuPcmConversionParameters))
                Call AddToLog($"Switching the current PCM files with the converted PCM files.")
                Dim attempts As UInt16 = UInt16.MinValue

                ' Copy the dictionary
                Dim dictFileParamsCopy = dictFileParams.ToDictionary(Function(entry) entry.Key, Function(entry) entry.Value)

                Dim msuPcmFileListRemove As New Queue(Of MsuPcmFile)

                ' Loop until all files are switched out
                Do Until dictFileParamsCopy.Count = 0

                    ' Reset Attempt counter if MaxValue is reached (Overflow)
                    ' (Would only happen after around 18h)
                    If attempts = UInt16.MaxValue Then
                        attempts = UInt16.MinValue
                    End If
                    attempts += MsuHelper.OneByte

                    Call AddToLog($"Attempt {attempts}", Drawing.Color.DarkGray)

                    For Each keyValuePair As KeyValuePair(Of MsuPcmFile, MsuPcmConversionParameters) In dictFileParamsCopy

                        If System.IO.File.Exists(keyValuePair.Value.FilePathConverted) Then
                        Else
                            ' File was not converted. Skip
                            'Call DictFileParamsCopy.Remove(KeyValuePair.Key)
                            Call msuPcmFileListRemove.Enqueue(keyValuePair.Key)
                            Continue For
                        End If

                        Try

                            If keyValuePair.Key.IsOpen() Then
                                Call AddToLog($"File ""{System.IO.Path.GetRelativePath(relativeTo:=Me.MsuTracksConfig.MsuLocation, path:=keyValuePair.Key.FilePath)}"" is currently open.", Drawing.Color.DarkMagenta)
                                Continue For
                            End If

                            ' Check if there already exists a converted version (Original file with suffix '_Normal' exists)
                            If keyValuePair.Key.FilePathWithNormalVersionSuffixExists Then

                                ' Overwrite the previous converted file
                                Call System.IO.File.Replace(
                                sourceFileName:=keyValuePair.Value.FilePathConverted,
                           destinationFileName:=keyValuePair.Key.FilePath,
                     destinationBackupFileName:=Constants.vbNullString)

                            Else
                                ' Switch out the normal file with the converted file.
                                ' Rename the original file with the suffix '_Normal'
                                Call System.IO.File.Replace(
                                sourceFileName:=keyValuePair.Value.FilePathConverted,
                           destinationFileName:=keyValuePair.Key.FilePath,
                     destinationBackupFileName:=keyValuePair.Key.FilePathWithNormalVersionSuffix)
                            End If

                            Call AddToLog($"Switched ""{System.IO.Path.GetRelativePath(relativeTo:=Me.MsuTracksConfig.MsuLocation, path:=keyValuePair.Key.FilePath)}"" to converted file.")
                            'Call DictFileParamsCopy.Remove(KeyValuePair.Key)
                            Call msuPcmFileListRemove.Enqueue(keyValuePair.Key)

                        Catch ex As System.Exception
                            Call AddToLog(ex.ToString, Drawing.Color.DarkMagenta)
                            Call AddToLog($"Could not switch out file ""{System.IO.Path.GetRelativePath(relativeTo:=Me.MsuTracksConfig.MsuLocation, path:=keyValuePair.Key.FilePath)}"" with converted file.{System.Environment.NewLine}Trying again later.", Drawing.Color.DarkMagenta)
                        End Try
                    Next

                    For Each msuPcmFile In msuPcmFileListRemove
                        If dictFileParamsCopy.Remove(msuPcmFile) Then
                        Else
                            Stop
                        End If
                    Next
                    Call msuPcmFileListRemove.Clear()

                    If dictFileParamsCopy.Count <> 0 Then
                        Call Threading.Thread.Sleep(1000) ' Wait one second and try again
                    End If
                Loop

                Call AddToLog($"Finished switching the current PCM files with the converted PCM files.")
                Call Me.MsuTracksConfig.RaiseEventConvertedFilesSwitched(Me, Nothing)
            End Sub

            Protected Sub WriteLoopPointsToConvertedFiles(ByRef dictFileParams As Dictionary(Of MsuPcmFile, MsuPcmConversionParameters))
                Call AddToLog($"Writing the new LoopPoints into the converted PCM files.")

                ' Copy the dictionary
                Dim dictFileParamsCopy = dictFileParams.ToDictionary(Function(entry) entry.Key, Function(entry) entry.Value)

                Dim loopPointWriteTries As Byte = 0

                Do
                    For Each keyValuePair As KeyValuePair(Of MsuPcmFile, MsuPcmConversionParameters) In dictFileParamsCopy

                        If System.IO.File.Exists(keyValuePair.Value.FilePathConverted) Then
                            Try
                                ' Write the LoopPoint into the file and remove entry from copied Collection

                                MsuPcmFile.SetLoopPoint(keyValuePair.Value.FilePathConverted, keyValuePair.Value.LoopPointConverted)

                                Call AddToLog($"Wrote LoopPoint {keyValuePair.Value.LoopPointConverted} into the file ""{System.IO.Path.GetRelativePath(relativeTo:=Me.MsuTracksConfig.MsuLocation, path:=keyValuePair.Value.FilePathConverted)}""")
                                Call dictFileParamsCopy.Remove(keyValuePair.Key)

                            Catch ex As System.Exception
                                Call AddToLog(ex.ToString, Drawing.Color.DarkMagenta)
                                Call AddToLog($"Could not write LoopPoint {keyValuePair.Value.LoopPointConverted} into the file ""{System.IO.Path.GetRelativePath(relativeTo:=Me.MsuTracksConfig.MsuLocation, path:=keyValuePair.Value.FilePathConverted)}"".{System.Environment.NewLine}Trying again later.", Drawing.Color.DarkMagenta)
                            End Try
                        End If
                    Next

                    Select Case dictFileParamsCopy.Count
                        Case 0
                            Exit Do
                        Case Else
                            Select Case loopPointWriteTries
                                Case >= 50
                                    For Each keyValuePair As KeyValuePair(Of MsuPcmFile, MsuPcmConversionParameters) In dictFileParamsCopy

                                        If System.IO.File.Exists(keyValuePair.Value.FilePathConverted) Then
                                            Call AddToLog($"Failed writing LoopPoint {keyValuePair.Value.LoopPointConverted} into the file ""{System.IO.Path.GetRelativePath(relativeTo:=Me.MsuTracksConfig.MsuLocation, path:=keyValuePair.Value.FilePathConverted)}"".{System.Environment.NewLine}Will have start to end loop by default.", Drawing.Color.Red)
                                        Else
                                            Call AddToLog($"The converted file ""{System.IO.Path.GetRelativePath(relativeTo:=Me.MsuTracksConfig.MsuLocation, path:=keyValuePair.Value.FilePathConverted)}"" does not exist.", Drawing.Color.Red)
                                        End If
                                    Next
                                Case Else
                                    loopPointWriteTries = loopPointWriteTries + MsuHelper.OneByte
                                    Call Threading.Thread.Sleep(100)
                            End Select
                    End Select
                Loop
            End Sub

            Private Shared Function ProcessesToHandles(ByRef processes As Process()) As System.IntPtr()
                Dim arrayPos As Byte
                Dim lowerBound As Byte = CByte(processes.GetLowerBound(0))
                Dim upperBound As Byte = CByte(processes.GetUpperBound(0))
                Dim processesPtr() As System.IntPtr
                ReDim processesPtr(processes.GetUpperBound(0))

                For arrayPos = lowerBound To upperBound

                    If processes(arrayPos) Is Nothing Then
                        Continue For
                    End If

                    processesPtr(arrayPos) = processes(arrayPos).Handle
                Next

                Return ProcessesPtr
            End Function

            Private Shared Function InitializedProcessesToHandles(ByRef processes As Process()) As System.IntPtr()
                Dim arrayPos As Byte
                Dim lowerBound As Byte = CByte(processes.GetLowerBound(0))
                Dim upperBound As Byte = CByte(processes.GetUpperBound(0))
                Dim processesPtrDict As New Dictionary(Of System.IntPtr, Boolean)

                For arrayPos = lowerBound To upperBound

                    If processes(arrayPos) Is Nothing Then
                        Continue For
                    End If

                    Call processesPtrDict.Add(key:=processes(arrayPos).Handle, value:=True)
                Next

                Return processesPtrDict.Keys.ToArray
            End Function

            Private Shared Function GetFirstUndeclaredProcess(ByRef processes As Process()) As Byte
                Dim arrayPos As Byte
                Dim lowerBound As Byte = CByte(processes.GetLowerBound(0))
                Dim upperBound As Byte = CByte(processes.GetUpperBound(0))

                ' Check if a process in the Array is available/finished
                For arrayPos = lowerBound To upperBound

                    If processes(arrayPos) Is Nothing Then
                        Return arrayPos
                    End If
                Next

                Return Byte.MaxValue
            End Function

            '--- for WaitForXxx
            Private Const INFINITE As Int32 = &HFFFFFFFF
            '--- for WaitForXObjects
            Private Const WAIT_FAILED As Int32 = &HFFFFFFFF

            Private Const MAXIMUM_WAIT_OBJECTS As Byte = 64

            <DllImport("kernel32.dll", SetLastError:=True)>
            Private Shared Function WaitForMultipleObjects(ByVal nCount As Int32, ByVal lpHandles As IntPtr(), ByVal fWaitAll As Boolean, ByVal dwMilliseconds As Int32) As Integer : End Function

            Protected Shared Function GetAvailableProcessInArray(ByRef processes As Process()) As Byte
#If WINDOWS Then
                Dim arrayPos As Byte = GetFirstUndeclaredProcess(processes)

                If arrayPos <> Byte.MaxValue Then
                    Return arrayPos
                End If

                Dim processesPtr = ProcessesToHandles(processes)

                Dim [return] =
                    WaitForMultipleObjects(
                              nCount:=processesPtr.Length,
                           lpHandles:=processesPtr,
                      dwMilliseconds:=INFINITE,
                            fWaitAll:=False)

                Return CByte([return])
#Else
                Dim arrayPos As Byte
                Dim lowerBound As Byte = CByte(processes.GetLowerBound(0))
                Dim upperBound As Byte = CByte(processes.GetUpperBound(0))
                Do
                    ' Check if a process in the Array is available/finished
                    For arrayPos = lowerBound To upperBound

                        If processes(arrayPos) Is Nothing Then
                            Return arrayPos ' Process at Position not initialized
                        End If

                        If processes(arrayPos).HasExited Then
                            Return arrayPos ' Process at Position has finished
                        End If
                    Next
                    Call Threading.Thread.Sleep(100) ' Wait 100ms and check again
                Loop
#End If
            End Function


            Protected Shared Sub WaitUntilProcessesAreFinished(ByRef processes As Process())
#If WINDOWS Then
                Dim processesPtr = InitializedProcessesToHandles(processes)

                Dim [return] =
                    WaitForMultipleObjects(
                              nCount:=processesPtr.Length,
                           lpHandles:=processesPtr,
                      dwMilliseconds:=INFINITE,
                            fWaitAll:=True)
#Else
                Dim arrayPos As Byte
                Dim lowerBound As Byte = CByte(processes.GetLowerBound(0))
                Dim upperBound As Byte = CByte(processes.GetUpperBound(0))
                Dim notAllFinished As Boolean

                Do
                    notAllFinished = False

                    ' Check if a process in the Array is available/finished
                    For arrayPos = lowerBound To upperBound

                        If processes(arrayPos) Is Nothing OrElse processes(arrayPos).HasExited Then
                            Continue For
                        End If

                        notAllFinished = True
                        Call Threading.Thread.Sleep(100) ' Wait 100ms and check again
                    Next
                Loop While notAllFinished = True
#End If
            End Sub

            Protected Function GetDictionaryWithConversionParameters() As Dictionary(Of MsuPcmFile, MsuPcmConversionParameters)
                Dim numberDecimalSeparator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
                Dim dictFileParams As New Dictionary(Of MsuPcmFile, MsuPcmConversionParameters)
                Dim volumeForParamDbl As Double = Me.VolumePercentage / 100
                Dim volumeForParam = Replace(volumeForParamDbl.ToString, numberDecimalSeparator, MsuHelper.DotChar)
                Dim speedForParamDbl = Me.SampleRate / 44100
                Dim speedForParam = Replace(speedForParamDbl.ToString, numberDecimalSeparator, MsuHelper.DotChar)
                Dim cmdPath As String = Constants.vbNullString

                If Me.KeepProcessesOpen Then
                    cmdPath = FindExecutable("CMD")
                End If

                For Each msuPcmFile As MsuPcmFile In Me.MsuPcmFiles

                    Dim params = New MsuPcmConversionParameters

                    Try
                        params.LoopPointOrig = msuPcmFile.GetLoopPoint
                    Catch ex As System.Exception
                        Call AddToLog(ex.ToString, entryColor:=Drawing.Color.Firebrick)
                        Call AddToLog($"Could Not read the LoopPoint from the File {msuPcmFile.FilePath}. Default to start to end loop. (LoopPoint = 0)", entryColor:=Drawing.Color.DarkMagenta)
                        params.LoopPointOrig = 0
                    End Try

                    ' Calculate New LoopPoint
                    Select Case params.LoopPointOrig
                        Case 0
                            params.LoopPointConverted = params.LoopPointOrig
                        Case Else
                            Try
                                params.LoopPointConverted = CUInt(Math.Round(params.LoopPointOrig * (44100 / Me.SampleRate)))
                            Catch ex As System.OverflowException
                                params.LoopPointConverted = 0
                            End Try
                    End Select

                    params.FilePathConverted =
                    System.IO.Path.Join(
                        path1:=msuPcmFile.LocationAbsolute,
                        path2:=String.Concat(msuPcmFile.BaseName, "_"c, Me.SampleRate, "Hz_", Me.VolumePercentage, "PctVol", MsuHelper.PcmExtL)
                    )

                    Dim argsMsuPcm = $"-s -V4 -v {volumeForParam} ""{msuPcmFile.FilePathForConversion}"" ""{params.FilePathConverted}"" speed {speedForParam}"

                    params.ConversionProcessStartInfo = New ProcessStartInfo With {
                    .FileName = Me.MsuTracksConfig.Settings.AudioConversionSettings.MsuPcmPath _
                  , .Arguments = argsMsuPcm _
                  , .WorkingDirectory = Me.MsuTracksConfig.MsuLocation _
                  , .UseShellExecute = False _
                  , .CreateNoWindow = (Me.ProcessWindowStyle = ProcessWindowStyle.Hidden) _
                  , .WindowStyle = Me.ProcessWindowStyle _
                  , .ErrorDialogParentHandle = Me.ParentHandle _
                  , .ErrorDialog = .ErrorDialogParentHandle <> IntPtr.Zero
                }

                    If Me.KeepProcessesOpen Then

                        params.ConversionProcessStartInfo.FileName = cmdPath
                        params.ConversionProcessStartInfo.Arguments =
                        $"/K ""ECHO Converting ""{System.IO.Path.GetRelativePath(relativeTo:=Me.MsuTracksConfig.MsuLocation, path:=params.FilePathConverted)}"" via {System.IO.Path.GetFileName(Me.MsuTracksConfig.Settings.AudioConversionSettings.MsuPcmPath)} && " &
                        $"""{Me.MsuTracksConfig.Settings.AudioConversionSettings.MsuPcmPath}"" {argsMsuPcm}"" && " &
                        $"COLOR A1 && ECHO Finished"

                    End If

#If DEBUG Then
                    Call AddToLog(String.Concat(params.ConversionProcessStartInfo.FileName, " "c, params.ConversionProcessStartInfo.Arguments), Drawing.Color.Blue)
#End If
                    Call dictFileParams.Add(msuPcmFile, Params)
                Next

                Return dictFileParams
            End Function

            Protected Sub AddToLog(ByRef text As String)
                If Me.Logger IsNot Nothing Then Call Me.Logger.AddToLog(text)
            End Sub

            Protected Sub AddToLog(ByRef text As String, ByRef entryColor As System.Drawing.Color)
                If Me.Logger IsNot Nothing Then Call Me.Logger.AddToLog(text, entryColor)
            End Sub

        End Class
    End Namespace
End Namespace