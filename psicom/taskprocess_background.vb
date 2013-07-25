Option Strict On
Option Explicit On

Imports System.Runtime.InteropServices
Imports VB6 = Microsoft.VisualBasic
Imports System.Net
Imports System.Net.Sockets
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Diagnostics

Imports System.Threading
Imports System.IO
Imports psilog
Imports bjdutils
Imports psiupgrader
Imports psilegacycut
Imports psithreads
Imports psiio
Imports psicommslink
Imports psiarchiver

Module taskprocess_background
    ' Private g_cmdLabel2_delegate As New Form1.cmdLabel2Delegate(AddressOf Me.cmdLabel2Sub)

    Friend g_is_end As Boolean = False
    Friend g_IsActiveWindow As Boolean = False ' Whether within the starttime endtime window. Used by rxt.ini variable EXPENSELEVEL second integer (eg EXPENSELEVEL = 1,1)
    Friend g_run_criteria As Integer = 0
    Private g_is_cont As Boolean = False
    Friend g_is_running As Boolean = False
    Friend g_is_disabled_scheduled_runs As Boolean = False

    Friend c_tasklist() As Boolean

    Private Function GetElement(ByVal p_Idx As Integer) As Integer
        Dim l_int As Integer = 0
        Dim l_str As String = ""

        l_str = GetRegMifAddrs("EXPENSELEVEL", "0")
        Dim l_a_Str() As String
        l_a_Str = bjdutils.StringSplitTerms(l_str, ", ")

        Try
            If l_a_Str.Length >= p_Idx + 1 Then
                l_int = CInt(l_a_Str(p_Idx))
            End If
        Catch l_ex As Exception

        End Try

        Return l_int
    End Function

    Private Function CommsExpense() As Integer
        Return GetElement(0)
    End Function

    Private Function TimeExpense() As Integer
        Return GetElement(1)
    End Function

    Private Function NoHangup() As Integer
        Dim l_int As Integer = 0
        Dim l_str As String = ""

        l_str = GetRegMifAddrs("NOHANGUP", "0")

        Try
            If UCase(l_str) = "TRUE" Then
                l_int = -1
            ElseIf IsStringNumeric(l_str) Then
                l_int = CInt(l_str)
            End If
        Catch l_ex As Exception

        End Try

        Return l_int
    End Function

    Private Sub termcheck()
        If g_udp.Match("KIL*") Then
            ' g_frmsched.g_terminate_delegate(True)
            g_exit_criterion = EXIT_CRITERION_UDP
            g_systemShutdown = True
        End If
    End Sub

    Private Sub processes()
        Dim l_count As Integer

        If g_run_criteria = RUN_CRITERION_MANUAL Then
            c_tasklist = g_tasklistmanual
        ElseIf g_run_criteria = RUN_CRITERION_CONT Then
            c_tasklist = g_tasklistcont
        Else
            c_tasklist = g_tasklistsched
        End If

        If tasklistenabled(c_tasklist) Then
            ' Record run time
            SaveSetting("MIFSEND", "Hist", "LASTSCAN", bjdutils.date_format(Now, "dd,MM,yyyy HH;mm", "-", ":"))

            For l_count = 1 To 16

                Select Case l_count
                    Case 1
                        If g_frmsched.m_firstrun OrElse tasklistbitenabled(c_tasklist, GT_CMDPREAMBLE, GT_IS_ENABLED) Then
                            CmdPreamble()
                        End If
                    Case 2
                        If g_frmsched.m_firstrun OrElse tasklistbitenabled(c_tasklist, GT_CMDINITIAL, GT_IS_ENABLED) Then
                            CmdInitial()
                        End If
                    Case 3
                        If tasklistbitenabled(c_tasklist, GT_CMDCHECK, GT_IS_ENABLED) Then
                            CmdCheck()
                        End If
                    Case 4
                        If tasklistbitenabled(c_tasklist, GT_CMDSCAN, GT_IS_ENABLED) Then
                            ' CmdScan()
                        End If
                    Case 5
                        If tasklistbitenabled(c_tasklist, GT_CMDNTP, GT_IS_ENABLED) Then
                            CmdNtp()
                        End If
                    Case 6
                        If g_frmsched.m_firstrun OrElse tasklistbitenabled(c_tasklist, GT_CMDCONFIG, GT_IS_ENABLED) Then
                            CmdRxtIni()
                        End If
                    Case 7
                        If tasklistbitenabled(c_tasklist, GT_CMDTASKS, GT_IS_ENABLED) Then
                            CmdTasks()
                        End If
                    Case 8
                        If tasklistbitenabled(c_tasklist, GT_CMDUPGRADER, GT_IS_ENABLED) Then
                            CmdUpgrader()
                        End If
                    Case 9
                        If tasklistbitenabled(c_tasklist, GT_CMDWEB, GT_IS_ENABLED) Then
                            CmdWEB()
                        End If
                    Case 10
                        If tasklistbitenabled(c_tasklist, GT_CMDCOLLECT, GT_IS_ENABLED) Then
                            CmdCOLLECT() ' MIF file processing
                        End If
                    Case 11
                        If tasklistbitenabled(c_tasklist, GT_CMDREPORT, GT_IS_ENABLED) Then
                            CmdREPORT() ' Report to live ride status
                        End If
                    Case 12
                        If tasklistbitenabled(c_tasklist, GT_CMDSEND, GT_IS_ENABLED) Then
                            CmdSEND() ' SMTP
                        End If
                    Case 13
                        If tasklistbitenabled(c_tasklist, GT_CMDUPLOAD, GT_IS_ENABLED) Then
                            CmdUPLOAD() ' Cherished files via FTP
                        End If
                    Case 14
                        If tasklistbitenabled(c_tasklist, GT_CMDTIDY, GT_IS_ENABLED) Then
                            CmdTIDY()
                        End If
                    Case 15
                        If tasklistbitenabled(c_tasklist, GT_CMDEXIT, GT_IS_ENABLED) Then
                            CmdEXIT()
                        End If
                    Case 16
                        If tasklistbitenabled(c_tasklist, GT_CMDSMS, GT_IS_ENABLED) Then
                            CmdSMS()
                        End If
                End Select

                termcheck()

                If g_is_end OrElse g_systemShutdown Then Exit For
            Next
        End If
    End Sub

    Private Function SetIcon(ByRef p_icon As String) As Boolean
        Dim l_bool As Boolean = True

        g_frmsched.TrayIcon.Icon = New Icon(g_frmsched.c_form1.GetType(), p_icon)
        g_frmsched.TrayIcon.Text = "Sleeping"

        Return l_bool
    End Function

    Private Function DialinString() As String
        Return GetSetting("MIFSEND", "Addrs", "DIALIN")
    End Function

    Private Function NeedDialup(ByVal p_dp As String) As Boolean
        Dim l_ret As Boolean = False
        If p_dp = "GSM" OrElse p_dp = "ISDN" OrElse p_dp = "PABX" OrElse p_dp = "POTS" Then l_ret = True
        Return l_ret
    End Function

    Private Function NeedDialup() As Boolean
        Dim l_dp As String = DialinString()
        Dim l_ret As Boolean = False
        If l_dp = "GSM" OrElse l_dp = "ISDN" OrElse l_dp = "PABX" OrElse l_dp = "POTS" Then l_ret = True
        Return l_ret
    End Function

    Private Function DialupCheck(ByVal p_comms As psicommslink.comms) As Boolean
        Dim l_ret As Boolean = True
        Dim l_dp As String = UCase(DialinString())
        Dim l_bool As Boolean = False

        If NeedDialup(l_dp) Then ' Dialup required
            Dim l_stat As Integer = 0
            l_bool = p_comms.IsComms(DialinString()) ' Check for Internet connectivity

            ' If Internet connectivity then no need to dial up (again)
            If Not l_bool Then ' No Internet connectivity
                psilog.g_formlog.logD("psicom; taskprocess_background; DialupCheck: Mifdialer dialup proceeding")
                l_ret = p_comms.Dialup(l_stat, "MIFDIALER")  ' Assume that there is a Connectoid called "MIFDIALER"
            Else
                psilog.g_formlog.logD("psicom; taskprocess_background; DialupCheck: Internet connection already exists, no dialup needed")
            End If
        End If

        Return l_ret
    End Function

    Private Function Hangup(ByVal p_comms As psicommslink.comms) As Boolean
        Dim l_ret As Boolean = True
        Dim l_dp As String = UCase(DialinString())

        If NeedDialup(l_dp) Then ' Dialup required
            psilog.g_formlog.logD("Hanging up Mifdialer")
            p_comms.Hangup(True)
        Else
            psilog.g_formlog.logD("DIALIN does not use Mifdialer")
        End If

        Return l_ret
    End Function

    Private Function HangupCheck _
    ( _
         ByVal p_comms As psicommslink.comms, _
         ByRef p_IsConnected As Boolean, _
         ByVal p_nohangup As Integer, _
         ByRef p_DialTime As Date _
    ) As Boolean
        Dim l_ret As Boolean = True
        Dim l_str As String = "psicom; taskprocess_background; HangupCheck: (Mifdialer); "
        Dim l_now As Date = DateNow()
        Dim l_hangup As Boolean = False

        If diffmins(p_DialTime, l_now) >= 3600 Then
            l_str &= "Hourly hangup"
            p_DialTime = l_now
            l_hangup = True
        ElseIf _
        p_nohangup = 0 OrElse _
        ( _
             p_nohangup > 0 AndAlso _
             diffmins(p_DialTime, l_now) >= p_nohangup _
        ) _
        Then
            l_str = l_str & "NOHANGUP = " & p_nohangup
            l_hangup = True
        End If

        ' Drop link if flagged as connected OR 1 hour has elapsed
        If p_IsConnected AndAlso l_hangup Then
            psilog.g_formlog.logD(l_str)
            Hangup(p_comms)
            p_IsConnected = False
        End If

        Return l_ret
    End Function

    ' Set Icon and Comms Status
    Private Function SICStat(ByRef p_firstpass As Boolean, ByRef p_icon As String, ByVal p_IsComms As Boolean, ByVal p_comms As psicommslink.comms) As Boolean
        Dim l_bool As Boolean = False
        Dim l_message As String = Nothing

        p_icon = "Mifsend1.ico"
        If p_firstpass Then SetIcon(p_icon)

        psiio.control.IControl(True)
        l_bool = p_comms.IsComms(DialinString())
        ' l_bool = True
        psiio.control.IControl(False)

        If l_bool Then
            l_message = "Internet connection detected"
        Else
            p_icon = "Dalek1.ico"
            l_message = "No Internet connection detected"
        End If

        SetIcon(p_icon)

        If p_firstpass OrElse l_bool <> p_IsComms Then psilog.g_formlog.logI(l_message)
        p_firstpass = False

        Return l_bool
    End Function

    Friend Sub taskprocess_background(ByVal p_obj As Object)
        Try
            Dim l_comms As psicommslink.comms = New psicommslink.comms
            Dim l_icon As String = "Mifsend1.ico"
            Dim l_firstpass As Boolean = True
            Dim l_ScheduledRun As Boolean = False
            Dim l_IsComms As Boolean = False
            Dim l_IsConnected As Boolean = False
            Dim l_CommsStatus As Integer = 0
            Dim l_CommsExpense As Integer = 0
            Dim l_TimeExpense As Integer = 0
            Dim l_nohangup As Integer = 0
            Dim l_DialTime As Date = DateNow()

            ' l_archiver.m_path = g_PsiArchiver.GetArchivePathjc1()
            SetIcon(l_icon)
            g_frmsched.Invoke(g_frmsched.g_iconise_delegate)
            g_formlog.logI("Comms thread starting")

            ' Background task 1 (BG1) - loop forever
            While Not g_systemShutdown
                Try
                    l_CommsExpense = CommsExpense() ' Read EXPENSELEVEL rxt.ini variable
                    l_nohangup = NoHangup() ' Read NOHANGUP rxt.ini variable

                    g_is_running = False
                    g_frmsched.c_form1.cmdLabel2.Invoke(g_frmsched.c_form1.g_cmdLabel2_delegate, "Ready to Run")

                    ' Update last run time
                    Dim l_string As String = GetSetting("MIFSEND", "Hist", "LASTSCAN")
                    If l_string <> "" Then g_frmsched.lblLast.Invoke(g_frmsched.g_last_delegate, l_string)

                    ' ******** Job check loop when NOT doing rxt.ini download
                    While Not g_systemShutdown
                        Try
                            If l_CommsExpense = 0 Then
                                l_IsComms = SICStat(l_firstpass, l_icon, l_IsComms, l_comms)
                            End If

                            If g_wait.WaitOne(1000, False) = True Then    ' Wait for scheduled run/click of START button
                                g_PsiArchiver.ArchiveCheckHouskeepjc1() ' Check delete old archives
                                Exit While ' Leave jobs loop to run scehduled tasks
                            End If

                            Dim l_IsSalesdb As Boolean = False
                            initialisers.psiparseinit(l_IsSalesdb)

                            If l_IsSalesdb Then
                                ' SaveSetting("MIFSEND", "Hist", "LastDBToDate", Format(Now, "dd/MM/yy"))
                                ' SaveSetting("MIFSEND", "Hist", "LastDBToDate", Now.ToString("dd/MM/yy"))
                                ' SaveSetting("MIFSEND", "Hist", "LastDBToDate", (Now().ToString("dd/MM/yy", New Globalization.CultureInfo("en-US"))))
                                SaveSetting("MIFSEND", "Hist", "LastDBToDate", bjdutils.date_format(Now(), "dd,MM,yy", "/", ":"))
                            End If

                            psithreads.thread_inc()

                            If Not g_systemShutdown AndAlso g_jc.JobExists() Then
                                If (l_TimeExpense <= 0 OrElse g_IsActiveWindow) AndAlso (l_CommsExpense <= 1 OrElse (l_ScheduledRun AndAlso l_CommsExpense > 1)) Then
                                    If l_IsConnected OrElse DialupCheck(l_comms) Then
                                        l_IsConnected = True
                                        l_IsComms = SICStat(l_firstpass, l_icon, l_IsComms, l_comms)

                                        If l_IsComms Then
                                            Dim l_JustOne As Boolean = False
                                            ' Do all pending jobs at scheduled run time, if COMMSEXPENSE > 1 (batch mode to save cost)
                                            If l_CommsExpense <= 0 Then l_JustOne = True

                                            psiio.control.IControl(True)
                                            ' **************************************************************
                                            ' DO JOBS

                                            g_jc.dojobs(l_JustOne) ' .jc1 jobs
                                            l_DialTime = DateNow()
                                            l_ScheduledRun = False
                                        End If
                                        ' **************************************************************
                                        psiio.control.IControl(False)
                                    Else
                                        psilog.g_formlog.logE("psicom; taskprocess_background; dial up failure")
                                    End If
                                End If
                            End If

                            HangupCheck(l_comms, l_IsConnected, l_nohangup, l_DialTime)
                            SetIcon(l_icon) ' Post box icon

                            termcheck()
                        Catch l_ex As Exception
                            psilog.g_formlog.logE("psicom; taskprocess_background; psiparse job check loop: " & l_ex.Message)
                        End Try
                    End While
                    ' ******* END Job check loop

                    ' Scheduled run
                    If Not g_systemShutdown Then
                        If l_IsConnected OrElse DialupCheck(l_comms) Then
                            l_IsConnected = True
                            l_IsComms = SICStat(l_firstpass, l_icon, l_IsComms, l_comms)
                            If l_IsComms Then
                                g_is_running = True
                                g_frmsched.c_form1.cmdLabel2.Invoke(g_frmsched.c_form1.g_cmdLabel2_delegate, "Running")
                                g_frmsched.TrayIcon.Icon = New Icon(g_frmsched.GetType(), "Tardis1.ico")
                                g_frmsched.TrayIcon.Text = "Running"

                                ' psilog.g_formlog.logI(g_formlog.startend_cmd_log(g_formlog.string_prefix_datetime("START OF RUN")))
                                psilog.g_formlog.logI(g_formlog.startend_cmd_log("START OF RUN"))

                                Select Case g_run_criteria
                                    Case RUN_CRITERION_MANUAL
                                        psilog.g_formlog.logI("Manual run")
                                    Case RUN_CRITERION_REGISTRY_RUN
                                        psilog.g_formlog.logI("Ini file RUN command run")
                                    Case RUN_CRITERION_REGISTRY_TIME
                                        psilog.g_formlog.logI("Scheduled run")
                                    Case RUN_CRITERION_URGENT_MIF
                                        psilog.g_formlog.logI("Urgent MIF run")
                                    Case RUN_CRITERION_REBOOT_BEFORE_RUN
                                        psilog.g_formlog.logI("Reboot before run")
                                    Case RUN_CRITERION_CONT
                                        psilog.g_formlog.logI("Continuous run")
                                    Case RUN_CRITERION_FIRST_EVER_RUN
                                        psilog.g_formlog.logI("First ever successful run")
                                    Case Else
                                        psilog.g_formlog.logI("Run - unknown criterion")
                                End Select

                                psiio.control.IControl(True)

                                '******************************************************************************
                                ' DO SCHEDULED RUN (by schedule or manual activation)
                                processes()
                                l_DialTime = DateNow()
                                l_ScheduledRun = True
                                '******************************************************************************

                                psiio.control.IControl(False)

                                ' psilog.g_formlog.logI(g_formlog.startend_cmd_log(g_formlog.string_prefix_datetime("END OF RUN")))
                                psilog.g_formlog.logI(g_formlog.startend_cmd_log("END OF RUN"))

                                If g_is_end Then
                                    g_is_end = False
                                    psilog.g_formlog.logI("Run terminated")
                                End If
                            End If
                        Else
                            psilog.g_formlog.logE("psicom; taskprocess_background; dial up failure")
                        End If
                    End If
                    ' END scheduled run

                Catch l_ex As Exception
                    psilog.g_formlog.logE("Exception; Form1; taskprocess_background; loop: " & l_ex.Message)
                End Try
            End While

            psithreads.thread_mark_terminated()
            While True
                System.Threading.Thread.Sleep(1000)
            End While
        Catch l_ex As Exception
            psilog.g_formlog.logE("Exception; Form1; taskprocess_background: " & l_ex.Message)
        End Try
    End Sub

    Private Sub CmdPreamble()
        cmd_preamble()
    End Sub

    Private Sub CmdInitial()
        cmd_initial()
    End Sub

    Private Sub CmdCheck()
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("Start Check"))
        ' No more legacy support
        ' legacy_Check(g_run_criteria <> RUN_CRITERION_MANUAL)
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("End Check"))
    End Sub

    Private Sub CmdScan()
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("Start cherished file scan"))
        ' No more legacy support
        ' legacy_Scan()
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("End cherished file scan"))
    End Sub

    Private Sub CmdNtp()
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("Start NTP time synchronisation"))
        Try
            Dim l_servers() As String = _
            { _
                 "ntp.cs.strath.ac.uk", _
                 "ntp.ucsd.edu", _
                 "ntp.univ-lyon1.fr", _
                 "ntp.lth.se", _
                 "tick.clara.net", _
                 "chime.utoronto.ca", _
                 "fartein.ifi.uio.no", _
                 "cuckoo.nevada.edu", _
                 "ntp2a.mcc.ac.uk", _
                 "salmon.maths.tcd.ie" _
            }

            UDPHome("STATE=NTP")

            Dim l_NTP As C_NTP = New C_NTP

            If Not l_NTP Is Nothing Then
                If l_NTP.m_OK Then
                    psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("Start NTP time check"))
                    l_NTP.NTP_list(l_servers)
                    psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("End NTP time check"))
                End If
                l_NTP.Dispose()
            End If
        Catch l_ex As Exception
            psilog.g_formlog.logE("Exception; Form1; CmdNtp: " & l_ex.Message)
        End Try
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("End NTP time synchronisation"))
    End Sub

    Private Sub CmdRxtIni()
        rxtini.cmd_RxtIni()
    End Sub

    Private Sub CmdTasks()
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("Start Tasks"))
        ' No more legacy support
        ' legacy_Tasks()
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("End Tasks"))
    End Sub

    Private Sub CmdUpgrader()
        Try
            psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("Start Version Upgrade"))

            If Trim(GetSetting("MIFSend", "Addrs", "DOWNLOADS")) <> "" Then
                psiupgrader.psiupgrader.upgrader(gstDrv)
            Else
                psilog.g_formlog.logI(". Skipping V-DLD task, no downloads requested")
            End If

            psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("End Version Upgrade"))
        Catch l_ex As Exception
            psilog.g_formlog.logE("Exception; Form1; CmdVDLD: " & l_ex.Message)
        End Try
    End Sub

    Private Sub CmdWEB()
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("Start Web"))
        ' No more legacy support
        ' legacy_WEB(g_frmsched.AxLEAD1, g_frmsched.DCrypt1)
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("End Web"))
    End Sub

    Private Sub CmdCOLLECT()
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("Start Collect"))
        ' No more legacy support
        ' legacy_COLLECT()
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("End Collect"))
    End Sub

    Private Sub CmdREPORT()
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("Start Report"))
        ' No more legacy support
        ' legacy_REPORT _
        ' ( _
        '     tasklistbitenabled(c_tasklist, GT_CMDREPORT, GT_CMDREPORT_APPENDROLLINGLOG), _
        '     tasklistbitenabled(c_tasklist, GT_CMDREPORT, GT_CMDREPORT_APPENDROLLINGLOG_1) _
        ' )
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("End Report"))
    End Sub

    Private Sub CmdSEND()
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("Start Send"))
        ' No more legacy support
        ' legacy_SEND(g_frmsched.BWZip1, g_frmsched.g_BWZip1_finished, g_frmsched.g_BWZip1_error)
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("End Send"))
    End Sub

    Private Sub CmdUPLOAD()
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("Start Upload"))
        ' No more legacy support
        ' legacy_UPLOAD(g_frmsched.DCrypt1)
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("End Upload"))
    End Sub

    Private Sub CmdTIDY()
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("Start Tidy"))
        ' No more legacy support
        ' legacy_TIDY()
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("End Tidy"))
    End Sub

    Private Sub CmdEXIT()
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("Start Exit"))
        legacy_EXIT()
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("End Exit"))
    End Sub

    Private Sub CmdSMS()
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("Start SMS"))
        ' No more legacy support
        ' legacy_SMS(g_frmsched.MSComm1, g_frmsched.g_mscomm1_reply)
        psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("End SMS"))
    End Sub
End Module
