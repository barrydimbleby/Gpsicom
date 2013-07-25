Imports VB6 = Microsoft.VisualBasic
Imports bjdutils
Imports psilog
Imports psiparseinitialise

Module initialisers
     Sub psiloginit()
          g_formlog.SetLogEnablesFile _
          ( _
               bjdutils.StringToBool(GetSetting("MIFSEND", "Addrs", "LOGD"), True), _
               bjdutils.StringToBool(GetSetting("MIFSEND", "Addrs", "LOGI"), True), _
               bjdutils.StringToBool(GetSetting("MIFSEND", "Addrs", "LOGW"), True), _
               bjdutils.StringToBool(GetSetting("MIFSEND", "Addrs", "LOGE"), True), _
               bjdutils.StringToBool(GetSetting("MIFSEND", "Addrs", "LOGF"), True) _
          )
     End Sub

     Function psiparseinit(ByRef p_IsSalesdb As Boolean) As String
          Dim l_log As String = ""

          Try
               Dim ldtTmp As Date = Now
               Dim l_time_and_date As String = bjdutils.date_format(ldtTmp, "dd,MM,yyyy HH;mm", "-", ":")
               Dim l_today As String
               Dim lsttmp As String
               Dim i As Integer
               Dim memsts As MEMORYSTATUS
               Dim fsoTmp As Scripting.FileSystemObject = New Scripting.FileSystemObject
               Dim drvTmp As Scripting.Drive

               drvTmp = fsoTmp.GetDrive(fsoTmp.GetDriveName(gstDrv))
               ldtTmp = DateAdd(Microsoft.VisualBasic.DateInterval.Hour, -8, ldtTmp) ' compensate for midnight anomaly
               l_today = DateString_Renamed(ldtTmp)
               l_log = string_add_line(l_log, " (c) Picsolve International Ltd.")

               l_log = string_add_line(l_log, _
               "+ MIFSend v" & _
               "." & _
               Trim(CStr(My.Application.Info.Version.Major)) & "." & _
               Trim(CStr(My.Application.Info.Version.Minor)) & "." & _
               Trim(CStr(My.Application.Info.Version.Build)) & _
               " @ " & l_time_and_date & _
               " (" & l_today & ")")

               l_log = string_add_line(l_log, "+ Using PSI interface model")

               l_log = string_add_line(l_log, ". Ride ID = " & GetSetting("MIFSEND", "Config", "RIDEID"))
               l_log = string_add_line(l_log, ". INI file = " & GetSetting("MIFSEND", "Addrs", "INIURL"))
               l_log = string_add_line(l_log, ". Local time = GMT + " & (CSng(bjdutils.GetBiasMins()) / 60.0#) & "h")
               l_log = string_add_line(l_log, ". GMT = " & bjdutils.date_format(bjdutils.GMT, "dd,MM,yyyy hh;mm;ss", "-", ":")) ' local time - bias
               l_log = string_add_line(l_log, "+ OS Version: " & GetOSVersion())
               l_log = string_add_line(l_log, ". PSI Version = " & GetPSICommonSetting("Version"))

               ' Show David's day number (days from 2/1/2000 when PSI last ran)
               lsttmp = GetPSICommonSetting("Day number")
               If lsttmp <> "" Then
                    l_log = string_add_line(l_log, ". DAY_NUMBER = " & lsttmp)
               End If

               ' Show PSI's list of its 14 most recent trading days
               lsttmp = GetPSICommonSetting("Trading days")
               If lsttmp <> "" Then
                    l_log = string_add_line(l_log, ". TRADING_DAYS = " & lsttmp)
               End If

               ' Report momory resource
               GlobalMemoryStatus(memsts)
               l_log = string_add_line(l_log, ". Total physical memory = " & VB6.Format((memsts.dwTotalPhys + 1000000) \ 1048576, "#####") & " MBytes")
               l_log = string_add_line(l_log, ". Currently available physical memory = " & VB6.Format((memsts.dwAvailPhys + 1000000) \ 1048576, "#####") & " MBytes")
               l_log = string_add_line(l_log, ". Total virtual memory = " & VB6.Format(memsts.dwTotalVirtual \ 1048576, "#####") & " MBytes")
               l_log = string_add_line(l_log, ". Currently allocated virtual memory = " & VB6.Format((memsts.dwTotalVirtual - memsts.dwAvailVirtual) \ 1048576, "#####") & " MBytes")

               ' Report CPU type
               l_log = string_add_line(l_log, ". CPU-Type = " & GetCPUType())

               ' Report free disk space
               i = GetAvailMB(gstDrv)

               l_log = string_add_line(l_log, "+ Data drive " & gstDrv)
               l_log = string_add_line(l_log, "+  VolumeName: " & drvTmp.VolumeName)
               l_log = string_add_line(l_log, "+  Serial No: " & drvTmp.SerialNumber)

               If i > 99999 Then i = 99999
               If i < 100 Then
                    l_log = string_add_line(l_log, "!!!  Free Space: " & i & " MBytes. <- TOO LOW")
               Else
                    l_log = string_add_line(l_log, "+  Free Space: " & i & " MBytes.")
               End If

               ' Report C: free space
               If gstDrv <> "C:" Then
                    i = GetAvailMB("C:")
                    If i < 32 Then
                         l_log = string_add_line(l_log, "!!! C-Drive free space: " & i & " MBytes. <- TOO LOW")
                    Else
                         l_log = string_add_line(l_log, "+ C-Drive free space: " & i & " MBytes.")
                    End If
               End If

               ' Report last (current) RFID login
               lsttmp = GetPSICommonSetting("Login")
               If lsttmp <> "" Then l_log = string_add_line(l_log, ". RFID-Login = " & lsttmp)

               ' Show the latest database date
               lsttmp = GetSetting("MIFSEND", "Hist", "LastDBToDate")
               If lsttmp <> "" Then
                    l_log = string_add_line(l_log, ". LastDBToDate = " & lsttmp)
               End If
            psiparseinitialise.psiparseinitialise.initialise.Initialise(l_log, p_IsSalesdb, bjdutils.GetRegMifAddrs("ENCODING", "utf-8"))
          Catch l_ex As Exception
               psilog.g_formlog.logE("psicom; initialisers; psiparseinit: " & l_ex.Message)
          End Try

          Return l_log
     End Function
End Module
