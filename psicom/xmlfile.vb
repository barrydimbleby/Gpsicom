Imports System
Imports System.Xml
Imports System.Configuration
Imports System.Reflection
Imports System.io
Imports System.text
Imports psilog
Imports bjdutils

Public Class ConfigSettings
     Const XMLFILE As String = ".\psicom.config"

     Public Shared Function ReadSetting(ByVal key As String) As String
          ' Return ConfigurationSettings.AppSettings(key)
          Return System.Configuration.ConfigurationManager.AppSettings(key)
     End Function

     Private Shared Sub WriteSetting(ByVal Key As String, ByVal value As String)
          ' load config document for current assembly
          Dim doc As XmlDocument = loadConfigDocument()

          ' retrieve appSettings node
          Dim node As XmlNode = doc.SelectSingleNode("//applicationSettings")

          If (node Is Nothing) Then Throw New InvalidOperationException("appSettings section not found in config file.")

          Try
               ' select the 'add' element that contains the key
               Dim elem As XmlElement = CType(node.SelectSingleNode(String.Format("//setting[@name='{0}']", Key)), XmlElement)

               If (Not elem Is Nothing) Then
                    ' add value for key
                    elem.SetAttribute("value", value)
               Else
                    ' key was not found so create the 'add' element 
                    ' and set it's key/value attributes 
                    elem = doc.CreateElement("setting")
                    elem.SetAttribute("name", Key)
                    elem.SetAttribute("value", value)
                    node.AppendChild(elem)
               End If
               doc.Save(getConfigFilePath())
          Catch l_ex As Exception
               psilog.g_formlog.logE("Exception; xmlfile; WriteSetting: " & l_ex.Message)
          End Try
     End Sub

     Public Shared Sub RemoveSetting(ByVal key As String)
          ' load config document for current assembly
          Dim doc As XmlDocument = loadConfigDocument()

          ' retrieve appSettings node
          Dim node As XmlNode = doc.SelectSingleNode("//applicationSettings")

          Try
               If (node Is Nothing) Then
                    Throw New InvalidOperationException("appSettings section not found in config file.")
               Else
                    ' remove 'add' element with coresponding key
                    node.RemoveChild(node.SelectSingleNode(String.Format("//setting[@name='{0}']", key)))
                    doc.Save(getConfigFilePath())
               End If
          Catch l_ex As NullReferenceException
               psilog.g_formlog.logE("The key {0} does not exist: " & key & " " & l_ex.Message)
          End Try
     End Sub

      Private Shared Function loadConfigDocument() As XmlDocument
          Dim doc As XmlDocument = Nothing
          Try
               doc = New XmlDocument()
               doc.Load(getConfigFilePath())
          Catch l_ex As System.IO.FileNotFoundException
               psilog.g_formlog.logE("xmlfile; loadConfigDocument; No configuration file found. " & l_ex.Message)
          End Try

          Return doc
     End Function

     Shared Function GetXmlWriter(ByVal ws As XmlWriterSettings, ByVal outStream As Stream) As XmlWriter
          Dim xw As XmlWriter = Nothing
          Try
               xw = XmlWriter.Create(outStream, ws)
               Return xw
          Catch l_ex As Exception
               Try
                    xw.Close()
               Catch l_ex2 As Exception
                    psilog.g_formlog.logE("xmlfile; GetXmlWriter; inner: " & l_ex2.Message)
               End Try

               psilog.g_formlog.logE("xmlfile; GetXmlWriter; outer: " & l_ex.Message)
               Return Nothing
          End Try
     End Function

     Private Shared Sub WriteXML(ByVal p_name As String, ByVal p_val As String, ByVal p_xw As XmlWriter)
          p_xw.WriteStartElement("setting")
          p_xw.WriteAttributeString("name", p_name)
          p_xw.WriteAttributeString("SerialiseAs", "String")
          p_xw.WriteStartElement("value")
          p_xw.WriteString(p_val)
          p_xw.WriteEndElement()
          p_xw.WriteEndElement()
     End Sub

     Private Shared Function CreateXMLDocument(ByVal p_xmlfile As String) As XmlDocument
          Dim doc As XmlDocument = Nothing
          Dim l_s As Stream = New FileStream(p_xmlfile, FileMode.OpenOrCreate)
          Dim settings As New XmlWriterSettings()

          settings.CheckCharacters = True
          settings.CloseOutput = True
          settings.Indent = True
          settings.Encoding = Encoding.ASCII

          ' write an XML document to the ASP.NET Response

          If Not l_s Is Nothing Then

               Dim l_xw As XmlWriter = GetXmlWriter(settings, l_s)

               If Not (l_xw Is Nothing) Then

                    ' ... create the XML document here ...
                    l_xw.WriteStartDocument(True)
                    l_xw.WriteComment("Created at " & DateTime.Now.ToString("hh:mm:ss"))

                    l_xw.WriteStartElement("configuration")
                    l_xw.WriteStartElement("log4net")

                    WriteXML("logd", CStr(g_formlog.m_DebugScreen), l_xw)
                    WriteXML("logi", CStr(g_formlog.m_InfoScreen), l_xw)
                    WriteXML("logw", CStr(g_formlog.m_WarnScreen), l_xw)
                    WriteXML("loge", CStr(g_formlog.m_ErrorScreen), l_xw)
                    WriteXML("logf", CStr(g_formlog.m_FatalScreen), l_xw)

                    l_xw.WriteEndElement()
                    l_xw.WriteEndElement()

                    l_xw.WriteEndDocument()

                    l_xw.Close()

               End If

               l_s.Close()
          End If

          Return doc
     End Function

     Private Shared Function CheckCreateXMLDocument() As XmlDocument
          Dim doc As XmlDocument = Nothing

          If Not bjdutils.FileExists(XMLFILE) Then
               CreateXMLDocument(XMLFILE)
          End If

          Try
               doc = New XmlDocument()
               doc.Load(XMLFILE)
          Catch l_ex As System.IO.FileNotFoundException
               psilog.g_formlog.logE("xmlfile; CheckCreateXMLDocument; No configuration file found: " & l_ex.Message)
          End Try

          Return doc
     End Function

     Public Shared Sub SaveXMLSettings()
          Try
               CreateXMLDocument(XMLFILE)
          Catch l_ex As Exception
               g_formlog.logE("xmlfile; SaveXMLSettings: " & l_ex.Message)
          End Try
     End Sub

     Public Shared Sub InitSettings()
          Try
               Dim l_doc As XmlDocument = CheckCreateXMLDocument()
               Dim l_c As Integer

               ' Dim l_ns As XmlNodeList = l_doc.GetElementsByTagName("log4net")
               ' Dim l_e As XmlNode = l_ns(0)
               ' Dim l_nsa As XmlNode = l_e.NextSiblin

               Dim l_ns1 As XmlNodeList = l_doc.GetElementsByTagName("setting")

               For l_c = 0 To l_ns1.Count - 1
                    Dim l_n1 As XmlNode = l_ns1(l_c)
                    Dim l_a1 As XmlAttributeCollection = l_n1.Attributes
                    Dim l_ni As XmlNode = l_a1.GetNamedItem("name")
                    Dim l_n2 As XmlNode = l_n1.FirstChild

                    Select Case l_ni.InnerText
                         Case "logd"
                              g_formlog.m_DebugScreen = CBool(l_n2.InnerText)
                         Case "logi"
                              g_formlog.m_InfoScreen = CBool(l_n2.InnerText)
                         Case "logw"
                              g_formlog.m_WarnScreen = CBool(l_n2.InnerText)
                         Case "loge"
                              g_formlog.m_ErrorScreen = CBool(l_n2.InnerText)
                         Case "logf"
                              g_formlog.m_FatalScreen = CBool(l_n2.InnerText)
                    End Select
               Next
          Catch l_ex As Exception
               g_formlog.logE("xmlfile; InitSettings: " & l_ex.Message)
          End Try
     End Sub

     Private Shared Function getConfigFilePath() As String
          Return [Assembly].GetExecutingAssembly().Location + ".config"
     End Function
End Class
