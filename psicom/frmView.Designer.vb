<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmView
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
    Public WithEvents Command2 As System.Windows.Forms.Button
	Public WithEvents Text1 As System.Windows.Forms.TextBox
    Public WithEvents List1 As System.Windows.Forms.ListBox
    Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
          Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmView))
          Me.Command2 = New System.Windows.Forms.Button
          Me.Text1 = New System.Windows.Forms.TextBox
          Me.List1 = New System.Windows.Forms.ListBox
          Me.Label4 = New System.Windows.Forms.Label
          Me.Label3 = New System.Windows.Forms.Label
          Me.Label2 = New System.Windows.Forms.Label
          Me.Label1 = New System.Windows.Forms.Label
          Me.SuspendLayout()
          '
          'Command2
          '
          Me.Command2.BackColor = System.Drawing.SystemColors.Control
          Me.Command2.Cursor = System.Windows.Forms.Cursors.Default
          Me.Command2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.Command2.ForeColor = System.Drawing.SystemColors.ControlText
          Me.Command2.Location = New System.Drawing.Point(436, 226)
          Me.Command2.Name = "Command2"
          Me.Command2.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.Command2.Size = New System.Drawing.Size(73, 19)
          Me.Command2.TabIndex = 4
          Me.Command2.Text = "SAVE"
          Me.Command2.UseVisualStyleBackColor = False
          '
          'Text1
          '
          Me.Text1.AcceptsReturn = True
          Me.Text1.BackColor = System.Drawing.SystemColors.Window
          Me.Text1.Cursor = System.Windows.Forms.Cursors.IBeam
          Me.Text1.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.Text1.ForeColor = System.Drawing.SystemColors.WindowText
          Me.Text1.Location = New System.Drawing.Point(8, 226)
          Me.Text1.MaxLength = 0
          Me.Text1.Name = "Text1"
          Me.Text1.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.Text1.Size = New System.Drawing.Size(413, 21)
          Me.Text1.TabIndex = 3
          Me.Text1.Text = "Text1"
          '
          'List1
          '
          Me.List1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                      Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
          Me.List1.BackColor = System.Drawing.SystemColors.Window
          Me.List1.Cursor = System.Windows.Forms.Cursors.Default
          Me.List1.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.List1.ForeColor = System.Drawing.SystemColors.WindowText
          Me.List1.HorizontalScrollbar = True
          Me.List1.ItemHeight = 15
          Me.List1.Location = New System.Drawing.Point(8, 16)
          Me.List1.Name = "List1"
          Me.List1.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.List1.Size = New System.Drawing.Size(506, 154)
          Me.List1.TabIndex = 1
          '
          'Label4
          '
          Me.Label4.BackColor = System.Drawing.SystemColors.Control
          Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
          Me.Label4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
          Me.Label4.Location = New System.Drawing.Point(8, 0)
          Me.Label4.Name = "Label4"
          Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.Label4.Size = New System.Drawing.Size(441, 17)
          Me.Label4.TabIndex = 7
          Me.Label4.Text = "Last INI"
          Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
          '
          'Label3
          '
          Me.Label3.BackColor = System.Drawing.SystemColors.Control
          Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
          Me.Label3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
          Me.Label3.Location = New System.Drawing.Point(35, 269)
          Me.Label3.Name = "Label3"
          Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.Label3.Size = New System.Drawing.Size(441, 57)
          Me.Label3.TabIndex = 6
          Me.Label3.Text = resources.GetString("Label3.Text")
          '
          'Label2
          '
          Me.Label2.BackColor = System.Drawing.SystemColors.Control
          Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
          Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
          Me.Label2.Location = New System.Drawing.Point(416, 192)
          Me.Label2.Name = "Label2"
          Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.Label2.Size = New System.Drawing.Size(25, 17)
          Me.Label2.TabIndex = 5
          Me.Label2.Text = "Label2"
          Me.Label2.Visible = False
          '
          'Label1
          '
          Me.Label1.BackColor = System.Drawing.SystemColors.Control
          Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
          Me.Label1.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
          Me.Label1.Location = New System.Drawing.Point(143, 206)
          Me.Label1.Name = "Label1"
          Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.Label1.Size = New System.Drawing.Size(137, 17)
          Me.Label1.TabIndex = 2
          Me.Label1.Text = "Select a setting"
          '
          'frmView
          '
          Me.AccessibleRole = System.Windows.Forms.AccessibleRole.Grip
          Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
          Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
          Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
          Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
          Me.BackColor = System.Drawing.SystemColors.Control
          Me.ClientSize = New System.Drawing.Size(521, 346)
          Me.Controls.Add(Me.Command2)
          Me.Controls.Add(Me.Text1)
          Me.Controls.Add(Me.List1)
          Me.Controls.Add(Me.Label4)
          Me.Controls.Add(Me.Label3)
          Me.Controls.Add(Me.Label2)
          Me.Controls.Add(Me.Label1)
          Me.Cursor = System.Windows.Forms.Cursors.Default
          Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.ImeMode = System.Windows.Forms.ImeMode.Off
          Me.MinimizeBox = False
          Me.Name = "frmView"
          Me.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.ShowIcon = False
          Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
          Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
          Me.Text = "PSICOM Settings"
          Me.ResumeLayout(False)
          Me.PerformLayout()

     End Sub
#End Region 
End Class