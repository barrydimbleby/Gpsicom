Imports VB6 = Microsoft.VisualBasic
Imports bjdutils
Imports psilegacycut

Module cmdpreamble

    Function cmd_preamble() As Boolean
        Dim l_bool As Boolean = True
        Try
            Dim lstTmp As String
            Dim l_log As String

            psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("Start Preamble"))

            ' Apply defaults to any non-existent but essential registry entries
            SaveSetting("MIFSEND", "Addrs", "RUNTIMES", GetSetting("MIFSEND", "Addrs", "RUNTIMES", "1:00 *4:00 5:00"))
            SaveSetting("MIFSEND", "Addrs", "EPERSON", GetSetting("MIFSEND", "Addrs", "EPERSON", "webrep@rx-technology.co.uk"))
            SaveSetting("MIFSEND", "Addrs", "EPERSON", GetSetting("MIFSEND", "Addrs", "COPYTO", "eperson@rx-technology.co.uk"))
            SaveSetting("MIFSEND", "Addrs", "SERVER", GetSetting("MIFSEND", "Addrs", "SERVER", "217.158.108.2"))
            SaveSetting("MIFSEND", "Addrs", "DOMAIN", GetSetting("MIFSEND", "Addrs", "DOMAIN", "rxt.co.uk"))
            SaveSetting("MIFSEND", "Addrs", "IDENTITY", GetSetting("MIFSEND", "Addrs", "IDENTITY", GetSetting("MIFSEND", "Config", "RIDEID", "no_identity")))
            SaveSetting("MIFSEND", "Addrs", "WEBSITE", GetSetting("MIFSEND", "Addrs", "WEBSITE", "www.ridephoto.com"))
            SaveSetting("MIFSEND", "Addrs", "INIURL", GetSetting("MIFSEND", "Addrs", "INIURL", "http://www.rx-technology.co.uk/cgi-bin/ini.cgi"))
            SaveSetting("MIFSEND", "Addrs", "CONFIGURL", GetSetting("MIFSEND", "Addrs", "CONFIGURL", "http://www.rx-technology.co.uk/rxt/rxt.ini"))
            SaveSetting("MIFSEND", "Addrs", "DLOADTIMEOUT", GetSetting("MIFSEND", "Addrs", "DLOADTIMEOUT", "181"))
            SaveSetting("MIFSEND", "Addrs", "ISPPWD", GetSetting("MIFSEND", "Addrs", "ISPPWD", "BDF0EF8FF2D1D5"))
            SaveSetting("MIFSEND", "Addrs", "ISPUSER", GetSetting("MIFSEND", "Addrs", "ISPUSER", "rx-technology"))
            SaveSetting("MIFSEND", "Addrs", "DIALIN", GetSetting("MIFSEND", "Addrs", "DIALIN", "DIRECT"))

            gstDEADLINE = "! Dialup cancelled or failed or not-scheduled"

            lstTmp = GetPSICommonSetting("Version")
            If lstTmp = "" Then
                psilog.g_formlog.logI("PSI Version unavailable")
            Else
                psilog.g_formlog.logI("PSI " & lstTmp)
            End If

            l_log = "Psicom v" & g_frmsched.m_currentversion

            psilog.g_formlog.logI(l_log)
            psilog.g_formlog.logI(FileVersionInfoLog("bjdutils.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("log4net.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psilog.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psiarchiver.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psiupgrader.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psithreads.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psistore.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psilegacycut.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psilegacylink.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("gold.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psitimenext.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("mime.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psicommslink.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psiio.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psinetwork.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psismtp.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psiftp.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psintp.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psiudp.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psiweb.dll"))

            psilog.g_formlog.logI(FileVersionInfoLog("psiparsesemantics1.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psiparsesemantics2.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psiparsesemantics.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psiparsesemanticactions.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psiparseinitialise.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psiparsejobcontrol.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psiparseglobals.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psiparseinterpretsourcedata.dll"))
            psilog.g_formlog.logI(FileVersionInfoLog("psiparseplugin.dll"))

            ' Show the command line
            If VB6.Command() <> "" Then psilog.g_formlog.logI("* Command line = " & UCase(VB6.Command()))

            ' List settings
            lstTmp = GetSetting("MIFSEND", "Config", "RXTSETUPDB")
            If lstTmp <> "" Then
                psilog.g_formlog.logI("PSI(Database = " & lstTmp)
                psilog.g_formlog.logI("Run MIFSEND /CONFIG to change this setting.")
                psilog.g_formlog.logI("Database path not required when running with PSI v5xx or higher")
            End If

            psilog.g_formlog.logI(psilog.g_formlog.embelish_cmd_log("End Preamble"))
        Catch l_ex As Exception
            psilog.g_formlog.logE("Exception; Form1; CmdPreamble: " & l_ex.Message)
        End Try

        Return l_bool
    End Function
End Module
