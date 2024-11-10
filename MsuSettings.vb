
Namespace Msu
    Namespace Settings
        Public Class Settings : Inherits ClassWithPropertyReset
            Public Sub New()
                Me.AudioConversionSettings = New AudioConversionSettings
                Me.TrackAltSettings = New TrackAltSettings
                Me.LoggerSettings = New LoggerSettings
            End Sub

            Public Sub New(audioConversionSettings As AudioConversionSettings, trackAltSettings As TrackAltSettings, loggerSettings As LoggerSettings)
                If audioConversionSettings Is Nothing Then
                    Me.AudioConversionSettings = New AudioConversionSettings
                Else
                    Me.AudioConversionSettings = audioConversionSettings
                End If
                If trackAltSettings Is Nothing Then
                    Me.TrackAltSettings = New TrackAltSettings
                Else
                    Me.TrackAltSettings = trackAltSettings
                End If
                If loggerSettings Is Nothing Then
                    Me.LoggerSettings = New LoggerSettings
                Else
                    Me.LoggerSettings = loggerSettings
                End If
            End Sub

            <Newtonsoft.Json.JsonIgnore>
            Public Property Logger As Logger.Logger

            <Newtonsoft.Json.JsonProperty("audio_conversion_settings")>
            Public Property AudioConversionSettings As AudioConversionSettings

            <Newtonsoft.Json.JsonProperty("track_alt_settings")>
            Public Property TrackAltSettings As TrackAltSettings

            <Newtonsoft.Json.JsonProperty("logger_settings")>
            Public Property LoggerSettings As LoggerSettings

            Public Shared Function LoadOrCreateNew(ByRef logger As Logger.Logger) As Msu.Settings.Settings
                Dim JsonFilePath = Msu.Settings.Settings.GetDefaultSavePath()
                Dim Settings As Msu.Settings.Settings = Nothing

                If logger IsNot Nothing Then Call logger.AddToLog($"Checking if the settings file ""{JsonFilePath}"" exists.")

                If System.IO.File.Exists(JsonFilePath) Then
                    Try
                        Settings = LoadFromJson(JsonFilePath, logger)
                    Catch ex As System.Exception
                        If logger IsNot Nothing Then Call logger.AddToLog(ex.ToString, Drawing.Color.Red)
                        If logger IsNot Nothing Then Call logger.AddToLog("File containing the settings could not be read.", Drawing.Color.Firebrick)
                    End Try
                Else
                    If logger IsNot Nothing Then Call logger.AddToLog($"File ""{JsonFilePath}"" does not exist")
                End If

                If Settings Is Nothing Then
                    If logger IsNot Nothing Then Call logger.AddToLog("Loading default settings.")
                    Settings = New Msu.Settings.Settings With {
                    .Logger = logger
                }

                    Call Settings.ResetProperties()
                End If

                If Settings.LoggerSettings.MaxEntries < 1 Then
                    Call Settings.LoggerSettings.ResetProperty(NameOf(Settings.LoggerSettings.MaxEntries))
                End If

                Return Settings
            End Function

            Public Shared Function GetDefaultSavePath() As String
                Dim exePath = Application.ExecutablePath

                Return System.IO.Path.ChangeExtension(exePath, MsuHelper.JsonL)
            End Function

            ''' <summary>
            ''' Creates a new instance from a saved .JSON configuration 
            ''' </summary>
            ''' <param name="strJsonFilePath">A absolute path for the .JSON file that the <see cref="Newtonsoft.Json.JsonSerializer" /> will deserialize to an instance of <see cref="MsuTracks" />.</param>
            ''' <exception cref="System.ArgumentNullException"/>
            ''' <exception cref="System.ArgumentException"/>
            ''' <exception cref="System.NotSupportedException"/>
            ''' <exception cref="System.IO.FileNotFoundException"/>
            ''' <exception cref="System.IO.IOException"/>
            ''' <exception cref="System.Security.SecurityException" />
            ''' <exception cref="System.IO.DirectoryNotFoundException" />
            ''' <exception cref="System.UnauthorizedAccessException" />
            ''' <exception cref="System.IO.PathTooLongException" />
            ''' <exception cref="Newtonsoft.Json.JsonException" />
            Public Shared Function LoadFromJson(ByRef jsonFilePath As String, ByRef logger As Logger.Logger) As Msu.Settings.Settings
                Dim fileStream As System.IO.FileStream

                If logger IsNot Nothing Then Call logger.AddToLog($"Loading settings from JSON file: ""{jsonFilePath}""")
                Try
                    ' Open the JsonFile
                    Try

                        FileStream =
                New System.IO.FileStream(
                    path:=jsonFilePath,
                    access:=System.IO.FileAccess.Read,
                    share:=System.IO.FileShare.Read,
                    mode:=System.IO.FileMode.Open)

                    Catch ex As System.IO.IOException

                        FileStream =
                New System.IO.FileStream(
                    path:=jsonFilePath,
                    access:=System.IO.FileAccess.Read,
                    share:=System.IO.FileShare.ReadWrite Or IO.FileShare.Delete,
                    mode:=System.IO.FileMode.Open)

                    End Try

                    ' Create StreamReader for opened JsonFile
                    Dim StreamReader As _
            New System.IO.StreamReader(
                stream:=FileStream,
                encoding:=System.Text.Encoding.Default)

                    ' Deserialize JSON into Msu.Settings.Settings (load data)
                    Using JsonTextReader As _
            New Newtonsoft.Json.JsonTextReader(
               reader:=StreamReader)

                        Dim jsonSerializer As New Newtonsoft.Json.JsonSerializer()

                        LoadFromJson = JsonSerializer.Deserialize(Of Msu.Settings.Settings)(JsonTextReader)
                        LoadFromJson.Logger = logger

                    End Using
                Catch ex As System.Exception
                    If logger IsNot Nothing Then Call logger.AddToLog(ex.ToString, Drawing.Color.Red)
                    Throw
                End Try

            End Function

            ''' <summary>
            ''' Saves this instance as a .JSON configuration 
            ''' </summary>
            ''' <exception cref="T:System.ArgumentNullException"/>
            ''' <exception cref="T:System.ArgumentException"/>
            ''' <exception cref="T:System.NotSupportedException"/>
            ''' <exception cref="T:System.IO.FileNotFoundException"/>
            ''' <exception cref="T:System.IO.IOException"/>
            ''' <exception cref="T:System.Security.SecurityException" />
            ''' <exception cref="T:System.IO.DirectoryNotFoundException" />
            ''' <exception cref="T:System.UnauthorizedAccessException" />
            ''' <exception cref="T:System.IO.PathTooLongException" />
            Public Sub SaveToJson()
                Call SaveToJson(Msu.Settings.Settings.GetDefaultSavePath)
            End Sub

            ''' <summary>
            ''' Saves this instance as a .JSON configuration 
            ''' </summary>
            ''' <param name="jsonFilePath">A absolute path for the .JSON file that the <see cref="Newtonsoft.Json.JsonSerializer" /> will serialize to.</param>
            ''' <exception cref="T:System.ArgumentNullException"/>
            ''' <exception cref="T:System.ArgumentException"/>
            ''' <exception cref="T:System.NotSupportedException"/>
            ''' <exception cref="T:System.IO.FileNotFoundException"/>
            ''' <exception cref="T:System.IO.IOException"/>
            ''' <exception cref="T:System.Security.SecurityException" />
            ''' <exception cref="T:System.IO.DirectoryNotFoundException" />
            ''' <exception cref="T:System.UnauthorizedAccessException" />
            ''' <exception cref="T:System.IO.PathTooLongException" />
            Public Sub SaveToJson(ByRef jsonFilePath As String)
                If Me.Logger IsNot Nothing Then Call Me.Logger.AddToLog($"Saving settings to JSON file: ""{jsonFilePath}""")
                Dim jsonPathOld As String = String.Concat(jsonFilePath, "_old")
                Dim jsonPathTmp As String = String.Concat(jsonFilePath, "_tmp")
                Dim i As Byte = MsuHelper.ZeroByte

                While System.IO.File.Exists(JsonPathTmp) OrElse System.IO.Directory.Exists(JsonPathTmp)
                    JsonPathTmp = jsonFilePath & "_tmp" & i
                    i += MsuHelper.OneByte
                End While

                ' Create a new temporary file
                Dim stream As _
            New System.IO.FileStream(
                path:=jsonPathTmp,
                access:=IO.FileAccess.Write,
                share:=IO.FileShare.Read,
                mode:=IO.FileMode.CreateNew)

                Dim streamWriter As _
            New System.IO.StreamWriter(
                stream:=stream,
                encoding:=System.Text.Encoding.Default)

                ' Serialize this object as .JSON to the opened file
                Using jsonTextWriter As _
            New Newtonsoft.Json.JsonTextWriter(
               textWriter:=streamWriter)

                    jsonTextWriter.IndentChar = vbTab.Single
                    jsonTextWriter.Indentation = MsuHelper.OneByte
                    jsonTextWriter.Formatting = Newtonsoft.Json.Formatting.Indented

                    Dim jsonSerializer As New Newtonsoft.Json.JsonSerializer()

                    Call JsonSerializer.Serialize(jsonTextWriter, Me)

                    Call jsonTextWriter.Flush()
                End Using

                Call stream.Close()

                ' If a previous .JSON configuration exists
                If System.IO.File.Exists(jsonFilePath) Then

                    ' Replace existing .JSON with serialized temp .JSON
                    Call System.IO.File.Replace(JsonPathTmp, jsonFilePath, JsonPathOld)

                Else

                    ' Move serialized temp .JSON to destination
                    Call System.IO.File.Move(JsonPathTmp, jsonFilePath)

                End If
            End Sub
        End Class
        Public Class AudioConversionSettings : Inherits ClassWithPropertyReset

            ''' <summary>
            ''' Path of the file "<see href="https://github.com/qwertymodo/msupcmplusplus/releases">msupcm.exe</see>".
            ''' </summary>
            <Newtonsoft.Json.JsonProperty("msupcm_path")>
            Public Property MsuPcmPath As String

            Public Sub TryLocateMsuPcmPath()
                Dim files() As String = System.IO.Directory.GetFiles(
                         path:=Application.StartupPath(),
                searchPattern:="msupcm.exe",
                 searchOption:=System.IO.SearchOption.AllDirectories)

                Select Case files.Length
                    Case 0
                        Call Me.TryLocateMsuPcmPathWithFindExecutable()
                    Case Else
                        Me.MsuPcmPath = files.First
                End Select

            End Sub

            Private Sub TryLocateMsuPcmPathWithFindExecutable()
                Dim RetPath = FindExecutable("msupcm.exe", Application.StartupPath())

                If String.IsNullOrWhiteSpace(RetPath) Then
                    Return
                End If

                Me.MsuPcmPath = RetPath
            End Sub
        End Class

        Public Class TrackAltSettings : Inherits ClassWithPropertyReset
            <VBFixedString(12)> Const _MainVerAltFldDefault As String = "Main Version"

            <Newtonsoft.Json.JsonProperty("auto_set_auto_switch")>
            <System.ComponentModel.DefaultValue(True)>
            Public Property AutoSetAutoSwitch As Boolean

            <Newtonsoft.Json.JsonProperty("auto_set_display_only_tracks_with_alts")>
            <System.ComponentModel.DefaultValue(True)>
            Public Property AutoSetDisplayOnlyTracksWithAlts As Boolean

            <Newtonsoft.Json.JsonProperty("msu_track_main_version_title")>
            <System.ComponentModel.DefaultValue(_MainVerAltFldDefault)>
            Public Property MsuTrackMainVersionTitle As String

            <Newtonsoft.Json.JsonProperty("msu_track_main_version_location")>
            <System.ComponentModel.DefaultValue(_MainVerAltFldDefault)>
            Public Property MsuTrackMainVersionLocation As String

            <Newtonsoft.Json.JsonProperty("save_msu_location")>
            <System.ComponentModel.DefaultValue(True)>
            Public Property SaveMsuLocation As Boolean

            <Newtonsoft.Json.JsonProperty("display_loop_point_hexadecimal")>
            <System.ComponentModel.DefaultValue(False)>
            Public Property DisplayLoopPointInHexadecimal As Boolean
        End Class

        Public Class LoggerSettings : Inherits ClassWithPropertyReset

            <Newtonsoft.Json.JsonProperty("max_entries")>
            <System.ComponentModel.DefaultValue(Byte.MaxValue)>
            Public Property MaxEntries As System.UInt32
        End Class

        Partial Public MustInherit Class ClassWithPropertyReset
            ''' <summary>
            ''' Resets all Property Values to default. See <see href="https://www.codeproject.com/Articles/66073/DefaultValue-Attribute-Based-Approach-to-Property"/>
            ''' </summary>
            Public Sub ResetProperties()
                For Each [property] As System.ComponentModel.PropertyDescriptor In System.ComponentModel.TypeDescriptor.GetProperties(Me)
                    Call ResetProperty([property])
                Next
            End Sub

            Public Sub ResetProperty(ByRef propertyName As String)
                For Each [property] As System.ComponentModel.PropertyDescriptor In System.ComponentModel.TypeDescriptor.GetProperties(Me)
                    If [property].Name = propertyName Then
                        [property].ResetValue(Me)
                    End If
                Next
            End Sub

            Protected Sub ResetProperty([property] As System.ComponentModel.PropertyDescriptor)
                [property].ResetValue(Me)
                Dim propertyValue = [property].GetValue(Me)

                If TypeOf propertyValue Is ClassWithPropertyReset Then
                    Dim subClassWithPropertyReset = DirectCast(propertyValue, ClassWithPropertyReset)
                    Call subClassWithPropertyReset.ResetProperties()
                End If
            End Sub

            ''' <summary>
            ''' Copies all properties to another instance of this class.
            ''' </summary>
            ''' <param name="ClassWithPropertyResetCopy">Another instance of the same class.</param>
            Public Sub CopyProperties(ByVal classWithPropertyResetCopy As ClassWithPropertyReset)
                For Each [property] As System.ComponentModel.PropertyDescriptor In System.ComponentModel.TypeDescriptor.GetProperties(Me)
                    Dim propertyValue = [property].GetValue(Me)

                    If TypeOf propertyValue Is ClassWithPropertyReset Then
                        Dim subClassWithPropertyReset = DirectCast(propertyValue, ClassWithPropertyReset)
                        Dim subClassWithPropertyResetCopy = DirectCast([property].GetValue(classWithPropertyResetCopy), ClassWithPropertyReset)

                        Call subClassWithPropertyReset.CopyProperties(subClassWithPropertyResetCopy)
                    Else
                        [property].SetValue(classWithPropertyResetCopy, propertyValue)
                    End If
                Next
            End Sub
        End Class
    End Namespace
End Namespace