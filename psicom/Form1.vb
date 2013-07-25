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

Public Class Form1
     Inherits System.Windows.Forms.Form

     Friend g_cmdLabel2_delegate As New Form1.cmdLabel2Delegate(AddressOf Me.cmdLabel2Sub)

     Friend Shared c_tasklist() As Boolean

     Private Sub cmdEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEnd.Click
          taskprocess_background.g_is_end = True
     End Sub

     Friend Delegate Sub cmdLabel2Delegate(ByRef p_string As String)

     Private Sub cmdLabel2Sub(ByRef p_string As String)
          Me.cmdLabel2.Text = p_string
     End Sub

     Private Sub form1_Move(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Move
          ' g_frmsched.SaveSettings()
     End Sub

     Private Sub form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
          Me.CheckBox_LogDebug.Checked = g_formlog.m_DebugScreen
          Me.CheckBox_LogInfo.Checked = g_formlog.m_InfoScreen
          Me.CheckBox_LogWarn.Checked = g_formlog.m_WarnScreen
          Me.CheckBox_LogError.Checked = g_formlog.m_ErrorScreen
          Me.CheckBox_LogFatal.Checked = g_formlog.m_FatalScreen
          psistore.g_store.ApplyForm(Me, 2)
     End Sub

     Protected Overrides Sub OnClosing(ByVal e As CancelEventArgs)
          psistore.g_store.SaveForm(Me, 2)

          If g_frmsched Is Nothing OrElse g_systemShutdown Then
               e.Cancel = False
          Else
               e.Cancel = True
               Me.Hide()
          End If
     End Sub

     Private Sub form1_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
          '          psistore.g_store.SaveForm(Me, 2)
          '          Me.Hide()
     End Sub

     Private Sub cmdPause_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPause.Click
          psithreads.thread_suspend(g_frmsched.m_threadidx1)
          cmdLabel1.Text = "Suspend State"
     End Sub

     Private Sub cmdresume_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdresume.Click
          Try
               psithreads.thread_resume(g_frmsched.m_threadidx1)
               cmdLabel1.Text = "Resume State"
          Catch l_ex As Exception
               psilog.g_formlog.logE("Exception; Form1; cmdresume_Click: " & l_ex.Message)
          End Try
     End Sub

     Private Sub cmdStart_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdStart.Click
          '  If Me.tbxInfo.IsHandleCreated Then
          If psithreads.thread_value(g_frmsched.m_threadidx1).thread.IsAlive Then
               If Not g_is_running Then
                    g_run_criteria = RUN_CRITERION_MANUAL
                    g_wait.Set()
               End If
          End If
          ' End If
     End Sub

     Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles disabled_runs.CheckedChanged
          g_is_disabled_scheduled_runs = Me.disabled_runs.Checked
     End Sub

     Protected Overrides Sub Finalize()
          MyBase.Finalize()
     End Sub

     Private Sub Check_LogDebug(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_LogDebug.CheckedChanged
          g_formlog.m_DebugScreen = CheckBox_LogDebug.Checked
     End Sub

     Private Sub Check_LogInfo(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_LogInfo.CheckedChanged
          g_formlog.m_InfoScreen = CheckBox_LogInfo.Checked
     End Sub

     Private Sub Check_LogWarn(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_LogWarn.CheckedChanged
          g_formlog.m_WarnScreen = CheckBox_LogWarn.Checked
     End Sub

     Private Sub Check_LogError(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_LogError.CheckedChanged
          g_formlog.m_ErrorScreen = CheckBox_LogError.Checked
     End Sub

     Private Sub Check_LogFatal(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_LogFatal.CheckedChanged
          g_formlog.m_FatalScreen = CheckBox_LogFatal.Checked
     End Sub
End Class