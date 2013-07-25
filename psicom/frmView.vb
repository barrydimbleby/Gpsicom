Option Strict On
Option Explicit On
Imports VB6 = Microsoft.VisualBasic.Compatibility.VB6
Imports psilegacycut

Friend Class frmView
     Inherits System.Windows.Forms.Form

     Private Sub frmView_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
          psistore.g_store.ApplyForm(Me, 3)

          Call RefreshSettings()
          If g_AllowChanges Then
               Me.Height = CInt(VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Label3.Top) + VB6.PixelsToTwipsY(Label3.Height) + 500))
               Label1.Text = "Select a setting"
          Else
               Label1.Text = ""
               Me.Height = CInt(VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Label2.Top) + VB6.PixelsToTwipsY(Label2.Height) + 500))
          End If
          Command2.Enabled = False
          Text1.Text = ""

          Label4.Text = "Last INI Download: " & GetSetting("MIFSEND", "Hist", "LASTINI", "Not known")

     End Sub

     Protected Overrides Sub OnClosing(ByVal e As System.ComponentModel.CancelEventArgs)
          psistore.g_store.SaveForm(Me, 3)

          e.Cancel = False
     End Sub

     Private Sub frmView_Closed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
     End Sub

     Private Sub frmView_Detroyed(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Me.HandleDestroyed
          psistore.g_store.SaveForm(Me, 3)
     End Sub

     Private Sub frmView_LocationChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Me.LocationChanged
          psistore.g_store.SaveForm(Me, 3)
     End Sub

     Private Sub Command2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command2.Click ' SAVE
          Dim lstConfig As String
          Dim lstAddrs As String
          Dim lboAddrs As Boolean
          Dim lboConfig As Boolean

          lstConfig = GetSetting("MIFSEND", "Config", Label1.Text, "1")
          lboConfig = (lstConfig = GetSetting("MIFSEND", "Config", Label1.Text, "2"))

          lstAddrs = GetSetting("MIFSEND", "Addrs", Label1.Text, "1")
          lboAddrs = (lstAddrs = GetSetting("MIFSEND", "Addrs", Label1.Text, "2"))

          If lboAddrs Then
               SaveSetting("MIFSEND", "Addrs", Label1.Text, Text1.Text)
          Else
               If lboConfig Then SaveSetting("MIFSEND", "Config", Label1.Text, Text1.Text)
          End If

          Call RefreshSettings()

     End Sub

     'UPGRADE_WARNING: Event List1.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
     Private Sub List1_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles List1.SelectedIndexChanged

          If g_AllowChanges Then
               Command2.Enabled = True
               Label1.Text = Trim(Mid(VB6.GetItemString(List1, List1.SelectedIndex), 1, 17))
               Text1.Text = Trim(Mid(VB6.GetItemString(List1, List1.SelectedIndex), 18))
          End If

     End Sub

     'UPGRADE_WARNING: Event Text1.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
     Private Sub Text1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Text1.TextChanged

     End Sub

     Private Sub Text1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Text1.Click

     End Sub

     Private Sub RefreshSettings()
          Dim i As Integer

          i = List1.SelectedIndex
          List1.Items.Clear()
          AddKeyToList("Addrs", "INIREF")
          AddKeyToList("Config", "RXTSETUPDB")
          AddKeyToList("Config", "RIDEID")
          AddKeyToList("Addrs", "FULLNAME")
          AddKeyToList("Addrs", "IDENTITY")
          AddKeyToList("Addrs", "WEBSALES")
          AddKeyToList("Addrs", "ONDEMAND")
          AddKeyToList("Addrs", "INIURL")
          AddKeyToList("Addrs", "CONFIGURL")
          AddKeyToList("Addrs", "RUNTIMES")
          AddKeyToList("Addrs", "WEBSITE")
          AddKeyToList("Addrs", "DOWNLOADS")
          AddKeyToList("Addrs", "DOWNLOAD_MIFSEND")
          AddKeyToList("Addrs", "DLOADTIMEOUT")
          AddKeyToList("Addrs", "DOMAIN")
          AddKeyToList("Addrs", "SERVER")
          AddKeyToList("Addrs", "FILEWARN")
          AddKeyToList("Addrs", "COPYTO")
          AddKeyToList("Addrs", "ENGINEERS")
          AddKeyToList("Addrs", "EPERSON")
          AddKeyToList("Addrs", "LOGS")
          AddKeyToList("Addrs", "QAPERSON")
          AddKeyToList("Addrs", "REPORTS")
          AddKeyToList("Addrs", "SMSPERSON")
          AddKeyToList("Addrs", "DIALIN")
          List1.SelectedIndex = i

     End Sub

     Private Sub AddKeyToList(ByRef astSect As String, ByRef astKey As String)
          Dim lstTmp1, lstTmp2 As String

          lstTmp1 = GetSetting("MIFSEND", astSect, astKey, "")
          lstTmp2 = GetSetting("MIFSEND", astSect, astKey, " ")
          If lstTmp1 = lstTmp2 Then List1.Items.Add(VB6.Format(astKey, "!@@@@@@@@@@@@@@@@@") & lstTmp1)

     End Sub
End Class