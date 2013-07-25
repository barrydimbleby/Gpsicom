Option Strict On
Option Explicit On
Imports System.ComponentModel
Imports System.Threading
Imports VB6 = Microsoft.VisualBasic
Imports VB6C = Microsoft.VisualBasic.Compatibility.VB6
Imports System.IO
Imports psilog
Imports bjdutils
Imports psithreads
Imports psitimenext

Public Module scheduler
     Public Function Str2Long(ByRef astCode As String) As Integer
          Dim l As Integer
          Dim i As Integer
          Dim ch As Integer

          l = 0
          For i = 1 To Len(astCode)
               ch = Asc(Mid(UCase(astCode), i, 1)) And 31
               l = l * 32
               l = l + ch
          Next i

          Return l
     End Function

     Private Function SubstXX(ByRef astRuns As String) As String
          Dim lstTmp As String
          Dim x As Integer

          ' Scans the input string for ":XX" and substitutes ":NN" for such occurrences
          ' NN is a 2 digit number in the range 00..59 derived from the park-ride code.

          ' First calculate x the 0..59 number.
          x = Str2Long(GetSetting("MIFSEND", "Config", "RIDEID", "XXXX"))
          lstTmp = ":" & VB6.Right("0" & Trim(CStr(x Mod 60)), 2)

          Return Replace(UCase(astRuns), ":XX", lstTmp)
     End Function

     Private Function FormString(ByRef p_strout As String, ByVal p_strref As String) As Boolean
          Dim l_bool As Boolean = False
          Dim l_str As String = Nothing

          If p_strref = "RUNTIMES" Then
               l_str = SubstXX(GetSetting("MIFSEND", "Addrs", "RUNTIMES"))
          Else
               l_str = (GetSetting("MIFSEND", "Addrs", p_strref))
          End If

          If Not l_str Is Nothing AndAlso l_str.Length > 0 Then
               l_str = p_strref & " = " & l_str
               p_strout = LCase(l_str)
               l_bool = True
          End If

          Return l_bool
     End Function

     Private Function CalcNext() As String
          Dim l_straccum As String = ""
          Dim l_str As String = Nothing
          Dim l_strtimenext As String = Nothing

          If FormString(l_str, "RUNTIMES") Then l_straccum &= l_str & ControlChars.NewLine
          If FormString(l_str, "STARTTIME") Then l_straccum &= l_str & ControlChars.NewLine
          If FormString(l_str, "ENDTIME") Then l_straccum &= l_str & ControlChars.NewLine
          If FormString(l_str, "OFFSET") Then l_straccum &= l_str & ControlChars.NewLine
          If FormString(l_str, "INTERVAL") Then l_straccum &= l_str & ControlChars.NewLine
          If FormString(l_str, "NUMBER") Then l_straccum &= l_str & ControlChars.NewLine
          If FormString(l_str, "MINUTES") Then l_straccum &= l_str & ControlChars.NewLine

          If Not l_straccum = Nothing AndAlso l_straccum.Length > 0 Then
               Dim l_timenext As psitimenext.psitimenext.timenext = New psitimenext.psitimenext.timenext
               Dim l_diff As Integer = -1
               Dim l_now As Date = CDate(bjdutils.date_format(Now, "HH;mm", "-", ":"))

            l_timenext.timenext(g_IsActiveWindow, l_diff, l_strtimenext, l_straccum, l_now)
          End If

          If l_straccum Is Nothing OrElse l_straccum.Length <= 0 Then l_strtimenext = "03:00" ' Emergency default should never be needed.

          Return l_strtimenext
     End Function

     Private Sub scheduler_background_legacy()
          Try
               Dim l_LastTime As Date
               Dim lstNow As String
               Dim lstTmp As String
               Dim l_run_criteria As Integer = 0
               Dim l_reboot_before_run As Boolean = False
               Dim l_reboot_after_run As Boolean = False

               Try
                    l_LastTime = Now()

                    ' Check schedule
                    lstNow = bjdutils.date_format(Now(), "HH;mm", "/", ":")

                    System.Threading.Thread.Sleep(900)

                    If g_frmsched.lblTime.Text <> lstNow Then
                         ' A minute has passed...

                         If Not g_systemShutdown Then
                              ' Refresh parkride ID field
                              g_frmsched.lblID.Invoke(g_frmsched.g_parkride_delegate, GetSetting("MIFSEND", "Config", "RIDEID"))

                              ' Refresh the time field
                              g_frmsched.lblTime.Invoke(g_frmsched.g_time_delegate, lstNow)
                         End If

                         l_run_criteria = 0
                         l_reboot_before_run = False
                         l_reboot_after_run = False

                         ' Check for run criteria before checking for next run time
                         If InStr(1, g_frmsched.lblNext.Text, g_frmsched.lblTime.Text) <> 0 Then
                              ' Scheduled run - next run time matches time now
                              l_run_criteria = RUN_CRITERION_REGISTRY_TIME
                              l_reboot_after_run = (VB6.Right(g_frmsched.lblNext.Text, 1) = "*")
                              l_reboot_before_run = (VB6.Left(g_frmsched.lblNext.Text, 1) = "*")
                         ElseIf (GetArgEx(GetSetting("MIFSEND", "Requests", "CMD"), 1, ",") = "RUN") Then
                              ' Clear the "RUN" registry entry
                              SaveSetting("MIFSEND", "Requests", "CMD", GetArgEx(GetSetting("MIFSEND", "Requests", "CMD"), 2, ",", True))
                              l_run_criteria = RUN_CRITERION_REGISTRY_RUN
                         ElseIf (GetArgEx(GetSetting("MIFSEND", "Requests", "CMD"), 1, ",") = "REBOOTRUN") Then
                              ' Clear the "REBOOTRUN" registry entry
                              SaveSetting("MIFSEND", "Requests", "CMD", GetArgEx(GetSetting("MIFSEND", "Requests", "CMD"), 2, ",", True))
                              l_run_criteria = RUN_CRITERION_REBOOT_BEFORE_RUN
                         ElseIf g_tasklistcont(G_TASKLIST_ENABLED) Then
                              'Continuous run
                              l_run_criteria = RUN_CRITERION_CONT
                         ElseIf g_frmsched.m_firstrun Then
                              l_run_criteria = RUN_CRITERION_FIRST_EVER_RUN
                         End If

                         If Not g_systemShutdown Then
                              ' Refresh the next run time field
                              g_frmsched.lblNext.Invoke(g_frmsched.g_next_delegate, CalcNext())
                         End If

                         ' If we have a new RFID login then save its details to a repappend file
                         lstTmp = GetPSICommonSetting("Login")
                         If lstTmp <> GetSetting("MIFSend", "Hist", "Login") Then
                              SaveSetting("MIFSend", "Hist", "Login", lstTmp)
                         End If

                         If l_run_criteria > 0 Then
                              ' Run requested

                              If Not g_is_disabled_scheduled_runs Then
                                   ' Pre-run reboot request?
                                   If l_reboot_before_run Then
                                        ' Remember to run after reboot
                                        SaveSetting("MIFSEND", "Requests", "CMD", "REBOOTRUN")
                                        ' Reboot now
                                        g_frmsched.m_frmboot.ShowDialog()
                                   Else

                                        ' Wait for active run completion
                                        While g_is_running
                                             System.Threading.Thread.Sleep(900)
                                        End While

                                        ' Indicate run criterion for reporting purposes
                                        g_run_criteria = l_run_criteria

                                        '******************************************************
                                        ' Run
                                        g_wait.Set() ' Trigger background task: BG1
                                        '******************************************************

                                        ' Post-run reboot request?
                                        If l_reboot_after_run Then g_frmsched.m_frmboot.ShowDialog()
                                   End If
                              End If
                         End If
                    End If
               Catch l_ex As Exception
                    psilog.g_formlog.logE("Exception; scheduler_background_legacy; loop: " & l_ex.Message)
               End Try
          Catch l_ex As Exception
               psilog.g_formlog.logE("Exception; scheduler; scheduler_background_legacy: " & l_ex.Message)
          End Try
     End Sub

     Friend Sub scheduler_background(ByVal p_obj As Object)
          g_formlog.logI("Scheduler thread starting")

          While Not g_systemShutdown
               Try
                    scheduler_background_legacy()
                    g_jc.m_jobtimes.scheduler_background()
               Catch l_ex As Exception
                    g_formlog.logE("Exception; schedular_background; loop: " & l_ex.Message)
               End Try

               thread_inc() ' Watchdog
          End While

          psithreads.thread_mark_terminated()
          While True
               System.Threading.Thread.Sleep(1000)
          End While
     End Sub
End Module
