Option Strict On
Option Explicit On
Imports System.ComponentModel
Imports System.Threading
Imports System.Configuration
Imports System.Configuration.ApplicationSettingsBase
Imports System.net
Imports System.Net.Sockets
Imports System.Net.IPEndPoint
Imports VB6 = Microsoft.VisualBasic
Imports VB6C = Microsoft.VisualBasic.Compatibility.VB6
Imports Microsoft.Win32
Imports System.IO
Imports bjdutils
Imports psilog
Imports psithreads
Imports psiudp
Imports psilegacycut
Imports psistore

Public Class frmSched
     Inherits System.Windows.Forms.Form
     Public m_threadidx1 As Integer
     Public m_threadidx2 As Integer
     Public m_threadWatchdog As Integer

     Public g_mscomm1_reply As String
     Public c_form1 As Form1 = New Form1

     Public m_currentversion As String = Nothing
     Public m_firstrun As Boolean = True

     Public WithEvents TrayIcon As NotifyIcon = New NotifyIcon

     Public g_parkride_delegate As New frmSched.cmdlblparkrideDelegate(AddressOf Me.cmdlblparkride)
     Public g_time_delegate As New frmSched.cmdlbltimeDelegate(AddressOf Me.cmdlbltime)
     Public g_last_delegate As New frmSched.cmdlbllastDelegate(AddressOf Me.cmdlbllast)
     Public g_next_delegate As New frmSched.cmdlblnextDelegate(AddressOf Me.cmdlblnext)
     Public g_iconise_delegate As New frmSched.iconiseDelegate(AddressOf Me.Iconise)
     ' Public Shared Event g_SessionEndingEvent As Microsoft.Win32.SessionEndingEventHandler
     ' Public g_sessionending As New SessionEndedEvHandler(AddressOf shutdown)

     Public Delegate Sub cmdlblparkrideDelegate(ByRef p_string As String)
     Public Delegate Sub cmdlbltimeDelegate(ByRef p_string As String)
     Public Delegate Sub cmdlbllastDelegate(ByRef p_string As String)
     Public Delegate Sub cmdlblnextDelegate(ByRef p_string As String)
     Public Delegate Sub iconiseDelegate()
     ' Public Delegate Sub SessionEndedEvHandler(ByVal sender As Object, ByVal e As Microsoft.Win32.SessionEndedEventArgs)

     ' Public Delegate Sub TerminateDelegate(ByVal p_criterion As Integer)

     ' Dim m_handler As Microsoft.Win32.SessionEndingEventHandler

     Private m_frmview As frmView
     Friend m_frmboot As frmBoot = New frmBoot()

     Private _settings As My.MySettings

     Private Shared WM_QUERYENDSESSION As Integer = &H11
     Private Const WM_QUIT As Integer = &H12
     Private Shared WM_ENDSESSION As Integer = &H16
     Private Const WM_CANCELMODE As Integer = &H1F

     Private m_TimerCount As Integer = 0

     Protected Overrides Sub WndProc(ByRef p_msg As System.Windows.Forms.Message)
          Dim l_reply As Boolean = True

          If p_msg.Msg = WM_QUERYENDSESSION Then
               psilog.g_formlog.m_systemShutdown = True
               g_exit_criterion = EXIT_CRITERION_WINDOWS
               g_systemShutdown = True
               psilog.g_formlog.logD("WM_QUERYENDSESSION")

               ' Stop Windows end session
               ' Dim MyMsg As New Message
               ' MyMsg.Msg = WM_CANCELMODE
               ' MyBase.WndProc(MyMsg)
               ' l_reply = False
               ' End stop windows end session
          ElseIf p_msg.Msg = WM_ENDSESSION Then
               ' g_systemShutdown = True
               ' psilog.g_formlog.m_systemShutdown = True
               ' g_exit_criterion = EXIT_CRITERION_WINDOWS
               psilog.g_formlog.logD("WM_ENDSESSION")
          ElseIf g_systemShutdown Then
               psilog.g_formlog.logD("message: " & p_msg.Msg)
          End If

          If l_reply Then
               MyBase.WndProc(p_msg)
          End If
     End Sub 'WndProc 

     ' Closure step 1
     Protected Overrides Sub OnClosing(ByVal e As CancelEventArgs)
          ' ToolTip1.SetToolTip(Me, "Iconise")
          psilog.g_formlog.logD("OnClosing")
          psistore.g_store.SaveForm(Me, 0)

          If g_systemShutdown Then
               psilog.g_formlog.logD("OnClosing g_systemshutdown")
               e.Cancel = False
          Else
               e.Cancel = True
               Iconise()
          End If
     End Sub

     ' Closure step 2. Called immediately after step 1
     '   Protected Overrides Sub OnFormClosing(ByVal e As System.Windows.Forms.FormClosingEventArgs)
     '        psilog.g_formlog.logD("Closure reason: " & e.CloseReason.ToString)
     '        MyBase.OnFormClosing(e)
     '   End Sub

     ' Not called for some reason
     Private Sub frmsched_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
          psilog.g_formlog.logD("frmsched_Closing")
          If g_systemShutdown Then
               ' Reset the variable because the user might cancel the shutdown.
               psilog.g_formlog.logD("frmsched_Closing g_systemshutdown")
               e.Cancel = False
          Else
               e.Cancel = True
               Iconise()
          End If
     End Sub

     Public Sub shutdown(ByVal p_sender As Object, ByVal e As Microsoft.Win32.SessionEndingEventArgs)
          ' Friend Sub shutdown(ByVal p_sender As Object, ByVal e As System.EventArgs)
          psilog.g_formlog.logD("shutdown - Session ending")
          ' ' g_frmsched.Invoke(g_terminate_delegate, True)
     End Sub

     Friend Sub cmdlblparkride(ByRef p_string As String)
          Me.lblID.Text = p_string
     End Sub

     Friend Sub cmdlbltime(ByRef p_string As String)
          Me.lblTime.Text = p_string
     End Sub

     Friend Sub cmdlbllast(ByRef p_string As String)
          Me.lblLast.Text = p_string
     End Sub

     Friend Sub cmdlblnext(ByRef p_string As String)
          Me.lblNext.Text = p_string
     End Sub

     Private Sub OKpopbox(ByVal p_msg As String, ByVal p_terminate As Boolean)
          Dim l_ret As DialogResult = New DialogResult

          l_ret = MessageBox.Show(p_msg, "Error", MessageBoxButtons.OK)

          If l_ret = DialogResult.OK Then
          End If

          If p_terminate Then
               Application.Exit()
               Me.Dispose()
          End If
     End Sub

     ' Private Sub MenuOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrayIcon.DoubleClick
     Private Sub MenuOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrayIcon.Click ' v.9.4.4 Single click only
          DeIconise()
     End Sub

     Private Sub cmdRUN_MouseUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles cmdRUN.MouseUp
          If CBool(Windows.Forms.MouseButtons.Left) Then

               ' Now do the run...
               c_form1.Show()
               lblID.Text = GetSetting("MIFSEND", "Config", "RIDEID")
          End If
     End Sub

     Private Sub CmdLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdLog.Click
          psilog.g_formlog.Show()
     End Sub

     Private Sub cmdConf_MouseUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles cmdConf.MouseUp
          If CBool(Windows.Forms.MouseButtons.Left) Then
               g_AllowChanges = (My.Computer.Keyboard.ShiftKeyDown <> False)
               m_frmview = New frmView
               m_frmview.Show()
          End If
     End Sub

     Private Sub cmdReboot_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdReboot.Click
          m_frmboot.ShowDialog()
     End Sub

     Private Sub cmdTerminate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdTerminate.Click
          Dim l_ret As DialogResult = New DialogResult

          l_ret = MessageBox.Show("Are you sure?", "Terminate psicom", MessageBoxButtons.YesNo)

          If l_ret = DialogResult.Yes Then
               g_exit_criterion = EXIT_CRITERION_SCREEN
               g_systemShutdown = True
               ' psistore.g_store.WriteMstr()
          End If
     End Sub

     ' Private Sub OnClosing_MouseUp(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Me.MouseHover
     '      ToolTip1.SetToolTip(Me, "Iconise")
     ' End Sub

     Private Sub Iconise()
          ' Me.Visible = False
          ' Me.Hide()
          Me.Opacity = 0
          Me.ShowInTaskbar = False
          g_formlog.Hide()
          c_form1.Hide()
          With Me.TrayIcon
               .Visible = True
               .Text = "Sleeping"
          End With
     End Sub

     Private Sub DeIconise()
          Me.Opacity = 100
          Me.Show()
          Me.ShowInTaskbar = True ' v.3.4.4
          Me.WindowState = FormWindowState.Normal
          TrayIcon.Visible = False
     End Sub

     ' ******** ENTRY POINT ********
     Public Sub frmSched_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
          psistore.g_store.ReadMstr()
          psistore.g_store.ApplyForm(Me, 0)

          Try
               ' AddHandler SystemEvents.SessionEnding, AddressOf shutdown

               ' g_systemShutdown = False
               ' g_terminate_delegate = New frmSched.TerminateDelegate(AddressOf Terminate)
               Dim lstTmp As String
               Dim l_ipendpoint As IPEndPoint = Nothing
               Dim l_udpclient As UdpClient = Nothing

               Try
                    l_ipendpoint = New IPEndPoint(IPAddress.Any, 0)
                    l_udpclient = New UdpClient(4502)
               Catch l_ex As Exception
                    psilog.g_formlog.logE("Exception frmSched_Load; UDP setup")
               End Try

               g_frmsched = Me

               ' Instantiate the log system
               psilog.g_formlog.Show()
               psiloginit()

               ' Asynchronous request terminate message from Psispy
               g_udp.asyncUDPrx(l_ipendpoint, l_udpclient)

            ' Log start parameters if there are greater than 1. There will always be at least one start parameter - the start context
               Dim l_a_params As String() = System.Environment.GetCommandLineArgs()
               Dim l_params As String = ""
               For Each l_param As String In l_a_params
                    l_params = bjdutils.string_add(l_params, l_param, " ")
               Next

               If l_a_params.Length > 1 Then psilog.g_formlog.logI("Start parameters: " & l_params) ' First parameter is the start context
               ' END Log start parameters

               If GetPSICommonSetting("") Is Nothing Then
                    OKpopbox _
                    ( _
                         "Looks like PSI is not installed" & ControlChars.CrLf _
                         & "PSI Registry settings: ""HLM\Picsolve\Common"" not found" & ControlChars.CrLf _
                         & "Cannot continue", _
                         True _
                    )
               Else
                    Dim l_ridecode As String = GetPSICommonSetting("Ride code")
                    Dim l_is_OK_for_background_tasks As Boolean = False

                    If l_ridecode Is Nothing Then
                         OKpopbox _
                         ( _
                              "No ridecode registry area found: ""HLM\Picsolve\Common\Ride code"" " & ControlChars.CrLf _
                          & "Cannot continue", _
                          True _
                         )
                    Else

                         If l_ridecode = "" OrElse l_ridecode.ToUpper = "PKRD" OrElse l_ridecode.Length <> 4 Then
                              OKpopbox _
                              ( _
                                   "Invalid ride code" & ControlChars.CrLf _
                               & "Cannot become operational", _
                               False _
                              )
                         Else
                              SaveSetting("MIFSEND", "Config", "RIDEID", l_ridecode)
                              SaveSetting("MIFSEND", "Config", "RXTSETUPDB", "")

                              ' ******** Find the 'data' drive
                              lstTmp = FindDataDrive()

                              ' last two chars are our result
                              gstDrv = VB6.Left(lstTmp, 2)

                              ' Check the result, nul = no data drive
                              If gstDrv = "" Then

                                   ' Couldn't find data drive
                                   OKpopbox _
                                   ( _
                                        "This PC is not set-up for emailing" & vbCrLf _
                                        & " The \EMAIL directory does not exist on any local fixed drive." & ControlChars.CrLf _
                                        & "Cannot become operational", _
                                        False _
                                   )
                              Else
                                   l_is_OK_for_background_tasks = True

                                   m_currentversion = "." & _
                                   Trim(CStr(My.Application.Info.Version.Major)) & "." & _
                                   Trim(CStr(My.Application.Info.Version.Minor)) & "." & _
                                   Trim(CStr(My.Application.Info.Version.Build)) '& _
                                   ' Trim(CStr(My.Application.Info.Version.Revision))

                                   Me.Text = "PSICOM v" & m_currentversion

                                   Dim l_tickcount As Integer
                                   l_tickcount = GetTickCount()
                                   If l_tickcount < 0 Then l_tickcount = &H7FFFFFFF
                                   l_tickcount = l_tickcount \ 1000
                                   SaveSetting("MIFSEND", "Hist", "STARTUPINFO", bjdutils.date_format(Now, "dd,MM,yyyy HH;mm", "-", ":") & " , " & CStr(System.Threading.Thread.CurrentThread.ManagedThreadId) & " , " & CStr(l_tickcount))

                                   ' Recalc last reboot.
                                   Dim l_date As Date = DateAdd(Microsoft.VisualBasic.DateInterval.Second, -1 * l_tickcount, Now)
                                   SaveSetting("MIFSEND", "Hist", "REBOOT", bjdutils.date_format(l_date, "dd,MM,yyyy HH;mm", "-", ":"))

                                   ' Enhanced multi-run protection...
                                   ' Write my app path to the registry on startup

                                   SaveSetting("MIFSEND", "Hist", "MyAppPath", My.Application.Info.DirectoryPath & "\" & My.Application.Info.AssemblyName & ".EXE;" & CStr(System.Threading.Thread.CurrentThread.ManagedThreadId))

                                   ' We'll check the MyAppPath setting periodically to monitor multi-runs...

                                   ' Check for first ever run
                                   If GetSetting("MIFSEND", "Addrs", "FIRSTRUN", "0") <> GetSetting("MIFSEND", "Addrs", "FIRSTRUN", "1") Then
                                        SaveSetting("MIFSEND", "Addrs", "FIRSTRUN", "1")
                                        m_firstrun = True
                                   ElseIf GetSetting("MIFSEND", "Addrs", "FIRSTRUN", "1") = "1" Then
                                        m_firstrun = True
                                   Else
                                        m_firstrun = False
                                   End If

                                   ' If psicom.exe is read-only pop-up a warning box
                                   Dim l_attribute As Integer = GetAttr(My.Application.Info.DirectoryPath & "\" & My.Application.Info.AssemblyName & ".EXE")
                                   If (Err.Number = 0) And ((l_attribute And FileAttribute.ReadOnly) <> 0) Then
                                        psilog.g_formlog.logW("WARNING:" & vbCrLf & "PSICOM.EXE is set to read-only - this is a bad thing. Please exit PSICOM, set it to r/w then re-run.")
                                   End If

                                   ' Give the main UI thread the name "UI"
                                   Thread.CurrentThread.Name = "UI"

                                   ConfigSettings.InitSettings()
                                   c_form1.Show()

                                   ' Set up the background threads
                                   If l_is_OK_for_background_tasks AndAlso Me.IsHandleCreated Then
                                        processtasklists()
                                        m_threadidx1 = psithreads.thread_run(AddressOf taskprocess_background.taskprocess_background, True, "BG0")
                                        m_threadidx2 = psithreads.thread_run(AddressOf scheduler.scheduler_background, True, "BG1")
                                        m_threadWatchdog = psithreads.thread_run(AddressOf psithreads.watchdog, True, "BGW")

                                        Timer1.Interval = 100
                                        Timer1.Enabled = True
                                   Else
                                        While True
                                             System.Threading.Thread.Sleep(10000)
                                             psilog.g_formlog.logW("Cannot run until valid park ride code set in PSI")
                                        End While
                                   End If
                              End If
                         End If
                    End If
               End If
          Catch l_ex As Exception
               psilog.g_formlog.logE("Exception; frmSched; frmSched_Load: " & l_ex.Message)
          End Try
     End Sub

     Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
          While True
               If g_systemShutdown Then
                    If g_systemShutdownState = SHUTDOWNSTATESTART Then
                         ' DeIconise()
                         psilog.g_formlog.logD("SHUTDOWNSTATESTART")
                         psiudp.UDPsend("127.0.0.1", 835, "PCW 0*") ' I am alive and closing
                         TerminateStart()
                         g_systemShutdownState = SHUTDOWNSTATEWAITING
                    ElseIf g_systemShutdownState = SHUTDOWNSTATEWAITING Then
                         ' psilog.g_formlog.logD("SHUTDOWNSTATEWAITING")
                         If psithreads.is_terminated() Then
                              psilog.g_formlog.logI("all threads terminated")
                              g_systemShutdownState = SHUTDOWNSTATECLEANUP
                         End If
                    ElseIf g_systemShutdownState = SHUTDOWNSTATECLEANUP Then
                         psilog.g_formlog.logD("SHUTDOWNSTATECLEANUP")
                         TerminateCleanup()
                         g_systemShutdownState = SHUTDOWNSTATEEND
                    ElseIf g_systemShutdownState = SHUTDOWNSTATEEND Then
                         psilog.g_formlog.logD("SHUTDOWNSTATEEND")
                         g_systemShutdownState = SHUTDOWNSTATECOMPLETE
                    ElseIf g_systemShutdownState = SHUTDOWNSTATECOMPLETE Then
                         psilog.g_formlog.logD("SHUTDOWNSTATECOMPLETE")
                         g_frmsched.Close()
                         psistore.g_store.WriteMstr()
                         Application.Exit()
                         ' Me.Close()
                         Exit While
                    End If
               Else ' Not closing down

                    If m_TimerCount = 0 Then
                         psiudp.UDPsend("127.0.0.1", 835, "PCW 1*") ' I am alive and running
                         m_TimerCount += 1
                    ElseIf m_TimerCount >= 300 Then ' 300 x 100mS = 30 seconds
                         m_TimerCount = 0
                    Else
                         m_TimerCount += 1
                    End If

                    Exit While
               End If

               Exit While ' Do not in fact loop
          End While

     End Sub
End Class
