Option Strict On
Option Explicit On

Imports System.Runtime.InteropServices
Imports VB6 = Microsoft.VisualBasic
Imports System.Threading
Imports psilog
Imports bjdutils
Imports psilegacylink
Imports psiparsejobcontrol.psiparsejobcontrol

Friend Module Module1
    Friend Const G_TASKLIST_ENABLED As Integer = 0
    Friend Const G_TASKLISTLENGTH As Integer = 136

    Friend Const GT_IS_ENABLED As Integer = 0

    Friend Const GT_ENABLES As Integer = 0
    Friend Const GT_CMDPREAMBLE As Integer = 1
    Friend Const GT_CMDINITIAL As Integer = 2
    Friend Const GT_CMDCHECK As Integer = 3
    Friend Const GT_CMDSCAN As Integer = 4
    Friend Const GT_CMDNTP As Integer = 5
    Friend Const GT_CMDCONFIG As Integer = 6
    Friend Const GT_CMDTASKS As Integer = 7
    Friend Const GT_CMDUPGRADER As Integer = 8
    Friend Const GT_CMDWEB As Integer = 9
    Friend Const GT_CMDCOLLECT As Integer = 10
    Friend Const GT_CMDREPORT As Integer = 11
    Friend Const GT_CMDSEND As Integer = 12
    Friend Const GT_CMDUPLOAD As Integer = 13
    Friend Const GT_CMDTIDY As Integer = 14
    Friend Const GT_CMDEXIT As Integer = 15
    Friend Const GT_CMDSMS As Integer = 16

    Friend Const GT_CMDPREAMBLE_DIAL As Integer = 1

    Friend Const GT_CMDINITIAL_CAMERAS As Integer = 1
    Friend Const GT_CMDINITIAL_PRINTERS As Integer = 2
    Friend Const GT_CMDINITIAL_PCS As Integer = 3
    Friend Const GT_CMDINITIAL_MODEMS As Integer = 4
    Friend Const GT_CMDINITIAL_GSMS As Integer = 5

    Friend Const GT_CMDREPORT_APPENDROLLINGLOG As Integer = 1
    Friend Const GT_CMDREPORT_APPENDROLLINGLOG_1 As Integer = 2

    Friend Const RUN_CRITERION_MANUAL As Integer = 1
    Friend Const RUN_CRITERION_CONT As Integer = 2
    Friend Const RUN_CRITERION_REGISTRY_TIME As Integer = 3
    Friend Const RUN_CRITERION_REGISTRY_RUN As Integer = 4
    Friend Const RUN_CRITERION_URGENT_MIF As Integer = 5
    Friend Const RUN_CRITERION_REBOOT_BEFORE_RUN As Integer = 6
    Friend Const RUN_CRITERION_FIRST_EVER_RUN As Integer = 7

    Friend Const EXIT_CRITERION_SCREEN As Integer = 1
    Friend Const EXIT_CRITERION_UDP As Integer = 2
    Friend Const EXIT_CRITERION_WINDOWS As Integer = 3

    Friend Const SHUTDOWNSTATESTART As Integer = 1
    Friend Const SHUTDOWNSTATEWAITING As Integer = 2
    Friend Const SHUTDOWNSTATECLEANUP As Integer = 3
    Friend Const SHUTDOWNSTATEEND As Integer = 4
    Friend Const SHUTDOWNSTATECOMPLETE As Integer = 5

    Friend g_tasklistsched() As Boolean = {True, True, True}
    Friend g_tasklistcont() As Boolean = {False, False, False}
    Friend g_tasklistmanual() As Boolean = Nothing

    Friend g_udp As C_UDP = New C_UDP

    Friend g_frmsched As frmSched

    Friend g_wait As New EventWaitHandle(False, EventResetMode.AutoReset)

    Friend g_PsiArchiver As psiarchiver.psiarchiver = New psiarchiver.psiarchiver()
    Friend g_jc As jobcontrol1 = New jobcontrol1(g_PsiArchiver)

    Friend g_AllowChanges As Boolean ' set true if user changes are allowed in [VIEW SETTINGS]

    Public g_systemShutdown As Boolean = False
    Public g_appexitcalled As Boolean = False
    Public g_systemShutdownState As Integer = SHUTDOWNSTATESTART
    Public g_exit_criterion As Integer = 0

    Friend Const g_one As Integer = 1
    Friend Const g_zero As Integer = 0

    Friend g_intptrone As IntPtr = CType(g_one, IntPtr)
    ' Public g_terminate_delegate As [Delegate]

    '*********************************************************************************************

    ' Exit windows API stuff
    Private Structure LUID
        Dim UsedPart As Integer
        Dim IgnoredForNowHigh32BitPart As Integer
    End Structure

    Private Structure TOKEN_PRIVILEGES
        Dim PrivilegeCount As Integer
        Dim TheLuid As LUID
        Dim Attributes As Integer
    End Structure

    Private Const EWX_FORCE As Integer = 4
    Private Const EWX_LOGOFF As Integer = 0
    Private Const EWX_REBOOT As Integer = 2
    Private Const EWX_SHUTDOWN As Integer = 1

    ' Re-boot
    Private Declare Function ExitWindowsEx Lib "user32" (ByVal uFlags As Integer, ByVal dwReserved As Integer) As Integer

    ' W2000 reboot token extensions
    Private Declare Function GetCurrentProcess Lib "KERNEL32" () As Integer
    Private Declare Function OpenProcessToken Lib "advapi32" (ByVal ProcessHandle As Integer, ByVal DesiredAccess As Integer, ByRef TokenHandle As Integer) As Integer
    'UPGRADE_WARNING: Structure LUID may require marshalling attributes to be passed as an argument in this Declare statement. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="C429C3A5-5D47-4CD9-8F51-74A1616405DC"'
    Private Declare Function LookupPrivilegeValue Lib "advapi32" Alias "LookupPrivilegeValueA" (ByVal lpSystemName As String, ByVal lpName As String, ByRef lpLuid As LUID) As Integer
    'UPGRADE_WARNING: Structure TOKEN_PRIVILEGES may require marshalling attributes to be passed as an argument in this Declare statement. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="C429C3A5-5D47-4CD9-8F51-74A1616405DC"'
    'UPGRADE_WARNING: Structure TOKEN_PRIVILEGES may require marshalling attributes to be passed as an argument in this Declare statement. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="C429C3A5-5D47-4CD9-8F51-74A1616405DC"'
    Private Declare Function AdjustTokenPrivileges Lib "advapi32" (ByVal TokenHandle As Integer, ByVal DisableAllPrivileges As Integer, ByRef NewState As TOKEN_PRIVILEGES, ByVal BufferLength As Integer, ByRef PreviousState As TOKEN_PRIVILEGES, ByRef ReturnLength As Integer) As Integer

    Friend Sub TerminateCleanup()
        psilog.g_formlog.m_systemShutdown = True ' Indicate system shutdown to log form
        g_frmsched.c_form1.Close() ' Close run control form
        psilog.g_formlog.Close() ' Close log form
        ' g_frmsched.Close()
        ' Me.Dispose()
    End Sub

    Friend Sub TerminateStart()
        Dim l_accept As Boolean = True
        Dim l_criterion As Integer = g_exit_criterion

        If l_criterion = EXIT_CRITERION_UDP Then
            psilog.g_formlog.logI("received request to terminate via UDP")
        ElseIf l_criterion = EXIT_CRITERION_SCREEN Then
            psilog.g_formlog.logI("received request to terminate via Psicom terminate button")
        ElseIf l_criterion = EXIT_CRITERION_WINDOWS Then
            psilog.g_formlog.logI("received request to terminate via Windows (logoff/shutdown/restart)")
        Else
            psilog.g_formlog.logI("refusing request to terminate via unknown criterion: " & l_criterion)
            l_accept = False
        End If

        If l_accept Then
            g_systemShutdown = True
            psithreads.g_systemshutdown = True

            ConfigSettings.SaveXMLSettings()
            SaveSetting("MIFSEND", "Hist", "STARTUPINFO", "")

            ' psistore.g_store.SaveForm(g_frmsched, 0)

            ' g_frmsched.c_form1.Close()
        End If
    End Sub

    Friend Sub ReBoot()
        SaveSetting("MIFSEND", "Hist", "REBOOT", bjdutils.date_format(Now, "dd,MM,yyyy HH;mm", "-", ":"))

        AdjustToken()
        ExitWindowsEx(EWX_REBOOT + EWX_FORCE, &HFFFFS)
    End Sub

    Friend Function FindDataDrive(Optional ByRef aboFixIT As Boolean = False) As String
        Dim l_psi_drive As String = GetPSICommonSetting("PC1 data drive")
        l_psi_drive = l_psi_drive.ToUpper & ":\"

        Return l_psi_drive
    End Function

    Private Sub AdjustToken()
        Const TOKEN_ADJUST_PRIVILEGES As Short = &H20S
        Const TOKEN_QUERY As Short = &H8S
        Const SE_PRIVILEGE_ENABLED As Short = &H2S
        Dim hdlProcessHandle As Integer
        Dim hdlTokenHandle As Integer
        Dim tmpLuid As LUID
        Dim tkp As TOKEN_PRIVILEGES
        Dim tkpNewButIgnored As TOKEN_PRIVILEGES
        Dim lBufferNeeded As Integer

        hdlProcessHandle = GetCurrentProcess()
        OpenProcessToken(hdlProcessHandle, TOKEN_ADJUST_PRIVILEGES Or TOKEN_QUERY, hdlTokenHandle)

        ' Get the LUID for shutdown privilege.
        LookupPrivilegeValue("", "SeShutdownPrivilege", tmpLuid)

        tkp.PrivilegeCount = 1 ' One privilege to set
        tkp.TheLuid = tmpLuid
        tkp.Attributes = SE_PRIVILEGE_ENABLED

        ' Enable the shutdown privilege in the access token of this process.
        AdjustTokenPrivileges(hdlTokenHandle, 0, tkp, Len(tkpNewButIgnored), tkpNewButIgnored, lBufferNeeded)
    End Sub
End Module