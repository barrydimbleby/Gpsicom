<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSched
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
    Public WithEvents cmdTerminate As System.Windows.Forms.Button
    Public WithEvents cmdConf As System.Windows.Forms.Button
    Public WithEvents cmdReboot As System.Windows.Forms.Button
    Public WithEvents cmdRUN As System.Windows.Forms.Button
    Public WithEvents label_ride_id As System.Windows.Forms.Label
    Public WithEvents lblID As System.Windows.Forms.Label
    Public WithEvents lblNext As System.Windows.Forms.Label
    Public WithEvents lblTime As System.Windows.Forms.Label
    Public WithEvents label_next_run As System.Windows.Forms.Label
    Public WithEvents Label_time As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
          Me.components = New System.ComponentModel.Container
          Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSched))
          Me.cmdTerminate = New System.Windows.Forms.Button
          Me.cmdConf = New System.Windows.Forms.Button
          Me.cmdReboot = New System.Windows.Forms.Button
          Me.cmdRUN = New System.Windows.Forms.Button
          Me.label_ride_id = New System.Windows.Forms.Label
          Me.lblID = New System.Windows.Forms.Label
          Me.lblNext = New System.Windows.Forms.Label
          Me.lblTime = New System.Windows.Forms.Label
          Me.label_next_run = New System.Windows.Forms.Label
          Me.Label_time = New System.Windows.Forms.Label
          Me.label_last_run = New System.Windows.Forms.Label
          Me.lblLast = New System.Windows.Forms.Label
          Me.CmdLog = New System.Windows.Forms.Button
          Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
          Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
          Me.SuspendLayout()
          '
          'cmdTerminate
          '
          Me.cmdTerminate.BackColor = System.Drawing.Color.WhiteSmoke
          Me.cmdTerminate.Cursor = System.Windows.Forms.Cursors.Default
          Me.cmdTerminate.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
          Me.cmdTerminate.FlatAppearance.BorderSize = 10
          Me.cmdTerminate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.cmdTerminate.ForeColor = System.Drawing.SystemColors.ControlText
          Me.cmdTerminate.Location = New System.Drawing.Point(8, 213)
          Me.cmdTerminate.Name = "cmdTerminate"
          Me.cmdTerminate.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.cmdTerminate.Size = New System.Drawing.Size(129, 30)
          Me.cmdTerminate.TabIndex = 1
          Me.cmdTerminate.Text = "Terminate"
          Me.cmdTerminate.UseVisualStyleBackColor = False
          '
          'cmdConf
          '
          Me.cmdConf.BackColor = System.Drawing.SystemColors.Control
          Me.cmdConf.Cursor = System.Windows.Forms.Cursors.Default
          Me.cmdConf.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.cmdConf.ForeColor = System.Drawing.SystemColors.ControlText
          Me.cmdConf.Location = New System.Drawing.Point(8, 151)
          Me.cmdConf.Name = "cmdConf"
          Me.cmdConf.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.cmdConf.Size = New System.Drawing.Size(129, 25)
          Me.cmdConf.TabIndex = 4
          Me.cmdConf.Text = "VIEW SETTINGS"
          Me.cmdConf.UseVisualStyleBackColor = False
          '
          'cmdReboot
          '
          Me.cmdReboot.BackColor = System.Drawing.SystemColors.Control
          Me.cmdReboot.Cursor = System.Windows.Forms.Cursors.Default
          Me.cmdReboot.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.cmdReboot.ForeColor = System.Drawing.SystemColors.ControlText
          Me.cmdReboot.Location = New System.Drawing.Point(8, 182)
          Me.cmdReboot.Name = "cmdReboot"
          Me.cmdReboot.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.cmdReboot.Size = New System.Drawing.Size(129, 25)
          Me.cmdReboot.TabIndex = 2
          Me.cmdReboot.Text = "REBOOT"
          Me.cmdReboot.UseVisualStyleBackColor = False
          '
          'cmdRUN
          '
          Me.cmdRUN.BackColor = System.Drawing.SystemColors.Control
          Me.cmdRUN.Cursor = System.Windows.Forms.Cursors.Default
          Me.cmdRUN.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.cmdRUN.ForeColor = System.Drawing.SystemColors.ControlText
          Me.cmdRUN.Location = New System.Drawing.Point(8, 89)
          Me.cmdRUN.Name = "cmdRUN"
          Me.cmdRUN.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.cmdRUN.Size = New System.Drawing.Size(129, 25)
          Me.cmdRUN.TabIndex = 3
          Me.cmdRUN.Text = "RUN CONTROL"
          Me.cmdRUN.UseVisualStyleBackColor = False
          '
          'label_ride_id
          '
          Me.label_ride_id.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
          Me.label_ride_id.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
          Me.label_ride_id.Cursor = System.Windows.Forms.Cursors.Default
          Me.label_ride_id.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.label_ride_id.ForeColor = System.Drawing.SystemColors.WindowText
          Me.label_ride_id.Location = New System.Drawing.Point(8, 9)
          Me.label_ride_id.Name = "label_ride_id"
          Me.label_ride_id.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.label_ride_id.Size = New System.Drawing.Size(65, 17)
          Me.label_ride_id.TabIndex = 9
          Me.label_ride_id.Text = " Ride ID"
          '
          'lblID
          '
          Me.lblID.BackColor = System.Drawing.SystemColors.Window
          Me.lblID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
          Me.lblID.Cursor = System.Windows.Forms.Cursors.Default
          Me.lblID.ForeColor = System.Drawing.SystemColors.WindowText
          Me.lblID.Location = New System.Drawing.Point(72, 9)
          Me.lblID.Name = "lblID"
          Me.lblID.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.lblID.Size = New System.Drawing.Size(65, 17)
          Me.lblID.TabIndex = 8
          Me.lblID.Text = "????"
          Me.lblID.TextAlign = System.Drawing.ContentAlignment.TopCenter
          '
          'lblNext
          '
          Me.lblNext.BackColor = System.Drawing.SystemColors.Window
          Me.lblNext.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
          Me.lblNext.Cursor = System.Windows.Forms.Cursors.Default
          Me.lblNext.ForeColor = System.Drawing.SystemColors.WindowText
          Me.lblNext.Location = New System.Drawing.Point(72, 60)
          Me.lblNext.Name = "lblNext"
          Me.lblNext.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.lblNext.Size = New System.Drawing.Size(65, 17)
          Me.lblNext.TabIndex = 7
          Me.lblNext.Text = "??:??"
          Me.lblNext.TextAlign = System.Drawing.ContentAlignment.TopCenter
          '
          'lblTime
          '
          Me.lblTime.BackColor = System.Drawing.SystemColors.Window
          Me.lblTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
          Me.lblTime.Cursor = System.Windows.Forms.Cursors.Default
          Me.lblTime.ForeColor = System.Drawing.SystemColors.WindowText
          Me.lblTime.Location = New System.Drawing.Point(72, 26)
          Me.lblTime.Name = "lblTime"
          Me.lblTime.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.lblTime.Size = New System.Drawing.Size(65, 17)
          Me.lblTime.TabIndex = 6
          Me.lblTime.Text = "??:??"
          Me.lblTime.TextAlign = System.Drawing.ContentAlignment.TopCenter
          '
          'label_next_run
          '
          Me.label_next_run.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
          Me.label_next_run.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
          Me.label_next_run.Cursor = System.Windows.Forms.Cursors.Default
          Me.label_next_run.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.label_next_run.ForeColor = System.Drawing.SystemColors.WindowText
          Me.label_next_run.Location = New System.Drawing.Point(8, 60)
          Me.label_next_run.Name = "label_next_run"
          Me.label_next_run.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.label_next_run.Size = New System.Drawing.Size(65, 17)
          Me.label_next_run.TabIndex = 5
          Me.label_next_run.Text = " Next Run"
          '
          'Label_time
          '
          Me.Label_time.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
          Me.Label_time.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
          Me.Label_time.Cursor = System.Windows.Forms.Cursors.Default
          Me.Label_time.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.Label_time.ForeColor = System.Drawing.SystemColors.WindowText
          Me.Label_time.Location = New System.Drawing.Point(8, 26)
          Me.Label_time.Name = "Label_time"
          Me.Label_time.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.Label_time.Size = New System.Drawing.Size(65, 17)
          Me.Label_time.TabIndex = 0
          Me.Label_time.Text = " Time"
          '
          'label_last_run
          '
          Me.label_last_run.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
          Me.label_last_run.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
          Me.label_last_run.Cursor = System.Windows.Forms.Cursors.Default
          Me.label_last_run.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.label_last_run.ForeColor = System.Drawing.SystemColors.WindowText
          Me.label_last_run.Location = New System.Drawing.Point(8, 43)
          Me.label_last_run.Name = "label_last_run"
          Me.label_last_run.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.label_last_run.Size = New System.Drawing.Size(65, 17)
          Me.label_last_run.TabIndex = 10
          Me.label_last_run.Text = " Last Run"
          '
          'lblLast
          '
          Me.lblLast.BackColor = System.Drawing.SystemColors.Window
          Me.lblLast.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
          Me.lblLast.Cursor = System.Windows.Forms.Cursors.Default
          Me.lblLast.ForeColor = System.Drawing.SystemColors.WindowText
          Me.lblLast.Location = New System.Drawing.Point(72, 43)
          Me.lblLast.Name = "lblLast"
          Me.lblLast.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.lblLast.Size = New System.Drawing.Size(168, 17)
          Me.lblLast.TabIndex = 11
          Me.lblLast.Text = "??:??"
          Me.lblLast.TextAlign = System.Drawing.ContentAlignment.TopCenter
          '
          'CmdLog
          '
          Me.CmdLog.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.CmdLog.Location = New System.Drawing.Point(8, 120)
          Me.CmdLog.Name = "CmdLog"
          Me.CmdLog.Size = New System.Drawing.Size(129, 25)
          Me.CmdLog.TabIndex = 20
          Me.CmdLog.Text = "VIEW LOG"
          Me.CmdLog.UseVisualStyleBackColor = True
          '
          'Timer1
          '
          '
          'frmSched
          '
          Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
          Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
          Me.BackColor = System.Drawing.SystemColors.Control
          Me.ClientSize = New System.Drawing.Size(244, 246)
          Me.Controls.Add(Me.CmdLog)
          Me.Controls.Add(Me.lblLast)
          Me.Controls.Add(Me.label_last_run)
          Me.Controls.Add(Me.cmdTerminate)
          Me.Controls.Add(Me.cmdConf)
          Me.Controls.Add(Me.cmdReboot)
          Me.Controls.Add(Me.cmdRUN)
          Me.Controls.Add(Me.label_ride_id)
          Me.Controls.Add(Me.lblID)
          Me.Controls.Add(Me.lblNext)
          Me.Controls.Add(Me.lblTime)
          Me.Controls.Add(Me.label_next_run)
          Me.Controls.Add(Me.Label_time)
          Me.Cursor = System.Windows.Forms.Cursors.Default
          Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
          Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
          Me.Location = New System.Drawing.Point(20, 10)
          Me.MaximizeBox = False
          Me.MinimizeBox = False
          Me.MinimumSize = New System.Drawing.Size(6, 24)
          Me.Name = "frmSched"
          Me.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
          Me.Text = "PSICOM"
          Me.ResumeLayout(False)

     End Sub
    Public WithEvents label_last_run As System.Windows.Forms.Label
    Public WithEvents lblLast As System.Windows.Forms.Label
     Friend WithEvents CmdLog As System.Windows.Forms.Button
     Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
     Friend WithEvents Timer1 As System.Windows.Forms.Timer
#End Region
End Class