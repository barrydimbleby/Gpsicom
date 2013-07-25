Option Strict On
Option Explicit On
Imports VB6 = Microsoft.VisualBasic
Imports System
Imports System.Net
Imports psilog
Imports bjdutils

Friend Class rxtini
     Private Shared Function GetRxtIni() As Boolean
          Dim l_stat As Boolean = False

          Try
               Dim l_str As String = Nothing
               Dim l_a_str() As String = Nothing
               Dim lstTmp As String

               Dim lstURL As String
               Dim lstKEY, lstArg As String
               Dim linPosEq As Integer

               Dim l_webclient As psiweb.psiwebrequest = New psiweb.psiwebrequest

               Dim l_relevant As Boolean = True
               Dim l_keyarg As Boolean = True
               Dim l_foundpark As Boolean = False
               Dim l_foundparkride As Boolean = False
               Dim l_string As String = ""

               Dim l_reg_pk As String = ""
               Dim l_reg_rd As String = ""
               Dim l_ini_id As String = ""
               Dim l_reg_id As String = ""

               ' Get the file
               lstURL = GetSetting("MIFSEND", "Addrs", "INIURL") & "?" & GetSetting("MIFSEND", "Config", "RIDEID") & "&" & DateString_Renamed(Now)

               If l_webclient.downloadstring(lstURL, l_str) Then
                    If Not l_str Is Nothing AndAlso l_str.Length > 0 Then
                         l_a_str = StringSplitLineEnd(l_str) ' Split string on any line end characters. Do not include any line end characters

                         If l_a_str(l_a_str.Length - 1) = "#! END" Then
                              Dim l_c As Integer

                              psilog.g_formlog.logD("Source URL: " & lstURL)

                              l_reg_id = UCase(GetSetting("MIFSEND", "Config", "RIDEID"))
                              l_reg_pk = l_reg_id.Substring(0, 2)
                              l_reg_rd = l_reg_id.Substring(2, 2)

                              For l_c = 0 To l_a_str.Length - 1
                                   l_relevant = False
                                   l_keyarg = False

                                   ' Show each line
                                   lstTmp = Trim(l_a_str(l_c))

                                   ' Parse the line
                                   If Len(lstTmp) > 3 Then
                                        lstArg = ""
                                        lstKEY = ""

                                        ' Look for an = delimiter
                                        linPosEq = InStr(1, lstTmp, "=")
                                        If linPosEq > 2 Then
                                             lstKEY = UCase(Trim(VB6.Left(lstTmp, linPosEq - 1)))
                                             lstArg = Trim(Mid(lstTmp, linPosEq + 1))

                                             If lstKEY.Length > 0 Then
                                                  l_relevant = True
                                                  l_keyarg = True
                                             End If
                                        End If

                                        ' If there's no = but there is a [????] section header then is it for our section...
                                        If (linPosEq = 0) And lstTmp.Substring(0, 1) = "[" And lstTmp.Substring(lstTmp.Length - 1, 1) = "]" Then
                                             l_ini_id = lstTmp.Substring(1, lstTmp.Length - 2)

                                             If l_ini_id.Length = 4 Then
                                                  ' [PKRD] section
                                                  If l_reg_id = l_ini_id Then
                                                       l_foundparkride = True
                                                       l_stat = True
                                                       l_relevant = True
                                                  Else
                                                       psilog.g_formlog.logE("[PKRD] missmatch; Registry: " & l_reg_id & " rxt.ini: " & l_ini_id)
                                                  End If
                                             ElseIf l_ini_id.Length = 2 Then
                                                  ' [PK] section
                                                  If l_reg_id.Substring(0, 2) = l_ini_id Then
                                                       l_foundpark = True
                                                       l_relevant = True
                                                  Else
                                                       psilog.g_formlog.logE("[PK] missmatch; Registry: " & l_reg_id & " rxt.ini: " & l_ini_id)
                                                  End If
                                             Else
                                                  psilog.g_formlog.logE("Strange rxt.ini [entry] " & l_ini_id)
                                             End If
                                        End If

                                        ' If its the default section or our section then use it
                                        If l_relevant And l_keyarg Then
                                             l_string = "From rxt.ini To Registry "
                                             SaveSetting("MIFSEND", "Addrs", lstKEY, lstArg)

                                             If l_foundparkride Or l_foundpark Then

                                                  ' Site-specific settings
                                                  l_string = l_string & "[" & UCase(l_ini_id) & "] "

                                             Else
                                                  l_string = l_string & "Common "
                                             End If

                                             l_string = l_string & lstKEY & " = " & lstArg
                                             psilog.g_formlog.logD(l_string)
                                        ElseIf Not l_relevant Then
                                             If lstTmp = "#! END" Then
                                                  psilog.g_formlog.logD("End of rxt.ini marker found: " & lstTmp)
                                             Else
                                                  psilog.g_formlog.logD("Not relevant " & lstTmp)
                                             End If
                                        End If
                                   End If

                                   ' Move on to next line
                              Next
                         Else
                              psilog.g_formlog.logE("rxt.ini end string not found: #! END")
                         End If
                    Else
                         psilog.g_formlog.logE("No rxt.ini")
                    End If

                    ' Record the INI download time & date
                    l_string = bjdutils.date_format(Now, "dd,MM,yyyy HH;mm", "-", ":")
                    SaveSetting("MIFSEND", "Hist", "LASTINI", l_string)
                    psilog.g_formlog.logD("To Registry Hist,LASTINI " & l_string)

                    ' Report if we did not find our relevant section of the INI file
                    If Not l_foundparkride Then
                         psilog.g_formlog.logE("!  Did not find [" & GetSetting("MIFSEND", "Config", "RIDEID") & "] section in INI File, or INI file name incorrectly configured." & vbCrLf & "+  Using identity: " & GetSetting("MIFSEND", "Addrs", "IDENTITY"))
                    End If

                    processtasklists()
               Else
                    psilog.g_formlog.logE("? Could not download the INI File from " & GetSetting("MIFSEND", "Addrs", "INIURL", "<!EMPTY!>"))
               End If
          Catch l_ex As Exception
               psilog.g_formlog.logE("Exception; frmcfg; frmCFG_download: " & l_ex.Message)
          End Try

          Return l_stat
     End Function

     Friend Shared Function cmd_RxtIni() As Boolean
          Dim l_bool As Boolean = True

          psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("Start Config"))
          Try
               Dim l_count As Integer
               Dim l_stat As Boolean

               ' Launch the HTTP client to get the latest INI file from the web site,
               ' parse it & save the new settings to the registry
               gstDEADLINE = ""

               l_stat = rxtini.GetRxtIni()

               If Not l_stat Then
                    psilog.g_formlog.logW("Ini file: rxt.ini - download or processing problem")
               Else
                    SaveSetting("MIFSEND", "Addrs", "FIRSTRUN", "0")
                    g_frmsched.m_firstrun = False
                    g_formlog.logI("Ini file: rxt.ini - successfully downloaded and processed")
               End If

               ' Refresh globals from new reg settings
               ' &&& any others ?
               l_count = CInt(Val(GetSetting("MIFSEND", "Addrs", "WEBSALES", "0")))
               psiloginit() ' Psilog 

               If ginWebSalesMode <> l_count Then
                    ginWebSalesMode = l_count
                    psilog.g_formlog.logI("! WARNING: WebSalesMode changed to " & l_count)
               End If

               If ginWebSalesMode = 0 Then gstDEADLINE = "! WebSales mode disabled by INI file"
          Catch l_e As Exception
               psilog.g_formlog.logE("Exception; Form1; CmdConfig: " & l_e.Message)
          End Try
          psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("End Config"))

          Return l_bool
     End Function

End Class