<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmBoot
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
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents Timer1 As System.Windows.Forms.Timer
	Public WithEvents Command3 As System.Windows.Forms.Button
	Public WithEvents Command2 As System.Windows.Forms.Button
	Public WithEvents Command1 As System.Windows.Forms.Button
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
          Me.components = New System.ComponentModel.Container
          Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
          Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
          Me.Command3 = New System.Windows.Forms.Button
          Me.Command2 = New System.Windows.Forms.Button
          Me.Command1 = New System.Windows.Forms.Button
          Me.Label2 = New System.Windows.Forms.Label
          Me.Label1 = New System.Windows.Forms.Label
          Me.SuspendLayout()
          '
          'Timer1
          '
          Me.Timer1.Interval = 1
          '
          'Command3
          '
          Me.Command3.BackColor = System.Drawing.SystemColors.Control
          Me.Command3.Cursor = System.Windows.Forms.Cursors.Default
          Me.Command3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.Command3.ForeColor = System.Drawing.SystemColors.ControlText
          Me.Command3.Location = New System.Drawing.Point(8, 184)
          Me.Command3.Name = "Command3"
          Me.Command3.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.Command3.Size = New System.Drawing.Size(193, 33)
          Me.Command3.TabIndex = 4
          Me.Command3.Text = "CANCEL"
          Me.Command3.UseVisualStyleBackColor = False
          '
          'Command2
          '
          Me.Command2.BackColor = System.Drawing.SystemColors.Control
          Me.Command2.Cursor = System.Windows.Forms.Cursors.Default
          Me.Command2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.Command2.ForeColor = System.Drawing.SystemColors.ControlText
          Me.Command2.Location = New System.Drawing.Point(8, 144)
          Me.Command2.Name = "Command2"
          Me.Command2.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.Command2.Size = New System.Drawing.Size(193, 33)
          Me.Command2.TabIndex = 3
          Me.Command2.Text = "REBOOT NOW"
          Me.Command2.UseVisualStyleBackColor = False
          '
          'Command1
          '
          Me.Command1.BackColor = System.Drawing.SystemColors.Control
          Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
          Me.Command1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
          Me.Command1.Location = New System.Drawing.Point(8, 104)
          Me.Command1.Name = "Command1"
          Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.Command1.Size = New System.Drawing.Size(193, 33)
          Me.Command1.TabIndex = 2
          Me.Command1.Text = "GIVE ME MORE TIME"
          Me.Command1.UseVisualStyleBackColor = False
          '
          'Label2
          '
          Me.Label2.BackColor = System.Drawing.SystemColors.Window
          Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
          Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
          Me.Label2.ForeColor = System.Drawing.SystemColors.WindowText
          Me.Label2.Location = New System.Drawing.Point(8, 32)
          Me.Label2.Name = "Label2"
          Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.Label2.Size = New System.Drawing.Size(193, 65)
          Me.Label2.TabIndex = 1
          Me.Label2.Text = "Make sure you have closed all your programs and saved all your data."
          Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
          '
          'Label1
          '
          Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
          Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
          Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
          Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.Label1.ForeColor = System.Drawing.SystemColors.WindowText
          Me.Label1.Location = New System.Drawing.Point(8, 8)
          Me.Label1.Name = "Label1"
          Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.Label1.Size = New System.Drawing.Size(193, 17)
          Me.Label1.TabIndex = 0
          Me.Label1.Text = "Countdown"
          Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
          '
          'frmBoot
          '
          Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
          Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
          Me.BackColor = System.Drawing.SystemColors.Control
          Me.ClientSize = New System.Drawing.Size(210, 225)
          Me.ControlBox = False
          Me.Controls.Add(Me.Command3)
          Me.Controls.Add(Me.Command2)
          Me.Controls.Add(Me.Command1)
          Me.Controls.Add(Me.Label2)
          Me.Controls.Add(Me.Label1)
          Me.Cursor = System.Windows.Forms.Cursors.Default
          Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
          Me.MaximizeBox = False
          Me.MinimizeBox = False
          Me.Name = "frmBoot"
          Me.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.ShowIcon = False
          Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
          Me.Text = "PSICOM Re-booter"
          Me.ResumeLayout(False)

     End Sub
#End Region 
End Class