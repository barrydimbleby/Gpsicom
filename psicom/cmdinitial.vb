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

Imports psiparseglobals.psiparseglobals

Module cmdinitial
     Dim l_bool As Boolean = True
    Function cmd_initial() As Boolean
        Dim l_Bool As Boolean = True
        Try
            Dim l_date As Date
            Dim lstTmp As String
            Dim i As Integer
            Dim n As Integer
            Dim memsts As MEMORYSTATUS
            Dim l_log As String
            Dim fsoTmp As Scripting.FileSystemObject = New Scripting.FileSystemObject
            Dim drvTmp As Scripting.Drive

            ' Point the drive object at our valid drive
            drvTmp = fsoTmp.GetDrive(fsoTmp.GetDriveName(gstDrv))

            psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("Start Initial Processing"))

            psilog.g_formlog.logI("Operating system version: " & GetOSVersion())
            psilog.g_formlog.logI("Operating system drive: " & UCase(psiparseglobals.lookup1.lookup("os")))
            psilog.g_formlog.logI("Program drive: " & UCase(psiparseglobals.lookup1.lookup("program")))
            psilog.g_formlog.logI("Data drive: " & UCase(psiparseglobals.lookup1.lookup("data")))

            psilog.g_formlog.logI("Psicom run location: " & UCase(My.Application.Info.DirectoryPath & "\" & My.Application.Info.AssemblyName) & ".EXE")

            ' **************** TIMES ****************
            psilog.g_formlog.logI("Current date & time; displayed using ""Regional Settings"": " & Now().ToString)
            ' Set up time and date strings
            l_date = Now
            l_date = DateAdd(Microsoft.VisualBasic.DateInterval.Hour, -8, l_date) ' compensate for midnight anomaly
            gstToday = DateString_Renamed(l_date)

            psilog.g_formlog.logI("Local time: GMT + " & (CSng(bjdutils.GetBiasMins()) / 60.0#) & "h")
            psilog.g_formlog.logI("GMT: " & bjdutils.date_format(bjdutils.GMT, "dd,MM,yyyy HH;mm;ss", "-", ":")) ' local time - bias

            ' Report misc data
            lstTmp = GetSetting("MIFSEND", "HIST", "REBOOT")
            If Trim(lstTmp) <> "" Then
                n = GetTickCount()
                If n < 0 Then n = &H7FFFFFFF
                n = n \ 1000
                If n > 86499 Then
                    psilog.g_formlog.logI("? Last reboot estimated at: " & lstTmp)
                Else
                    psilog.g_formlog.logI("Last reboot estimated at: " & lstTmp)
                End If
            End If

            ' Show current deadline
            If ginWebSalesMode > 0 Then
                lstTmp = GetSetting("MIFSEND", "HIST", "DEADLINE")
                If VB6.Left(lstTmp, 1) = "!" Then
                    psilog.g_formlog.logI("? DEADLINE = " & lstTmp & " <- PSI is currently declining email sales")
                Else
                    If IsDate(lstTmp) Then
                        If CDate(lstTmp) < Now Then psilog.g_formlog.logI("? DEADLINE = " & lstTmp & " <- PSI is currently declining email sales")
                    End If
                End If
            End If

            psilog.g_formlog.logI("Date of last sales database transmission: " & GetSetting("MIFSEND", "HIST", "lastdbtodate"))

            ' Show David's day number (days from 2/1/2000 when PSI last ran)
            lstTmp = GetPSICommonSetting("Day number")
            If lstTmp <> "" Then
                psilog.g_formlog.logI("PSI last ran on day number: " & lstTmp)
            End If

            ' Show PSI's list of its 14 most recent trading days
            lstTmp = GetPSICommonSetting("Trading days")
            If lstTmp <> "" Then
                psilog.g_formlog.logI("14 most recent trading days: " & lstTmp)
            End If

            ' Determine web sales mode
            ginWebSalesMode = CInt(Val(GetSetting("MIFSEND", "Addrs", "WEBSALES", "0")))
            psilog.g_formlog.logI("Web sales mode: " & ginWebSalesMode)

            ' Report registry auto-runs
            lstTmp = RegEnumLOCALMACHINEValues("SOFTWARE\Microsoft\Windows\CurrentVersion\Run")
            If lstTmp <> "" Then
                psilog.g_formlog.logI("+ Current registry autoruns:")
                psilog.g_formlog.logI(lstTmp)
            End If

            ' Report memory resource
            GlobalMemoryStatus(memsts)
            psilog.g_formlog.logI(". Total physical memory = " & VB6.Format((memsts.dwTotalPhys + 1000000) \ 1048576, "#####") & " MBytes")
            psilog.g_formlog.logI(". Currently available physical memory = " & VB6.Format((memsts.dwAvailPhys + 1000000) \ 1048576, "#####") & " MBytes")
            psilog.g_formlog.logI(". Total virtual memory = " & VB6.Format(memsts.dwTotalVirtual \ 1048576, "#####") & " MBytes")
            psilog.g_formlog.logI(". Currently allocated virtual memory = " & VB6.Format((memsts.dwTotalVirtual - memsts.dwAvailVirtual) \ 1048576, "#####") & " MBytes")

            ' Report CPU type
            psilog.g_formlog.logI(". CPU-Type = " & GetCPUType())

            ' Report free disk space
            i = GetAvailMB(gstDrv)

            l_log = "+  VolumeName: " & drvTmp.VolumeName
            psilog.g_formlog.logI(l_log)
            l_log = "+  Serial No: " & drvTmp.SerialNumber
            psilog.g_formlog.logI(l_log)

            If i > 99999 Then i = 99999
            If i < 100 Then
                psilog.g_formlog.logI("!!!  Free Space: " & i & " MBytes. <- TOO LOW")
            Else
                l_log = "+  Free Space: " & i & " MBytes."
                psilog.g_formlog.logI(l_log)
            End If

            ' Report C: free space
            If gstDrv <> "C:" Then
                i = GetAvailMB("C:")
                If i < 32 Then
                    psilog.g_formlog.logI("!!! C-Drive free space: " & i & " MBytes. <- TOO LOW")
                Else
                    psilog.g_formlog.logI("+ C-Drive free space: " & i & " MBytes.")
                End If
            End If

            ' Report last (current) RFID login
            lstTmp = GetPSICommonSetting("Login")
            If lstTmp <> "" Then psilog.g_formlog.logI(". RFID-Login = " & lstTmp)

            psilog.g_formlog.logI("Ride ID: " & GetSetting("MIFSEND", "Config", "RIDEID"))
            psilog.g_formlog.logI("DIALIN: " & UCase(GetSetting("MIFSEND", "Addrs", "DIALIN")))

            psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("End Initial Processing"))
        Catch l_ex As Exception
            psilog.g_formlog.logE("Exception; Form1; CmdInitial: " & l_ex.Message)
        End Try

        Return l_Bool
    End Function
End Module
