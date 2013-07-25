Option Strict On
Option Explicit On
Imports psilegacycut

Friend Class frmBoot
	Inherits System.Windows.Forms.Form
	
    Dim ginCountDown As Integer
	
	Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click
		
		If ginCountDown < 600 Then ginCountDown = ginCountDown + 30
		Label1.Text = "Rebooting in " & ginCountDown & "s"
		
	End Sub
	
	Private Sub Command2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command2.Click
		
		ginCountDown = 1
		Label1.Text = "Rebooting in " & ginCountDown & "s"
		
	End Sub
	
	Private Sub Command3_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command3.Click
		
		Timer1.Enabled = False
		Me.Close()
		
	End Sub
	
	Private Sub frmBoot_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
          psistore.g_store.ApplyForm(Me, 4)

		Timer1.Interval = 1000
		Timer1.Enabled = True
		ginCountDown = 30
		Beep()
		
	End Sub
	
     Private Sub frmBoot_Closed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
          psistore.g_store.SaveForm(Me, 4)
     End Sub

     Private Sub Timer1_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Timer1.Tick

          ginCountDown = ginCountDown - 1
          Label1.Text = "Rebooting in " & ginCountDown & "s"
          If (ginCountDown < 10) Or ((ginCountDown Mod 6) = 5) Then Beep()
          If ginCountDown = 0 Then
               Timer1.Enabled = False
               ReBoot()
               Me.Close()
          End If

     End Sub
End Class