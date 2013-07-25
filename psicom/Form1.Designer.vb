<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class Form1
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
    Public WithEvents cmdPause As System.Windows.Forms.Button
    Public WithEvents cmdStart As System.Windows.Forms.Button
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
          Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
          Me.cmdPause = New System.Windows.Forms.Button
          Me.cmdStart = New System.Windows.Forms.Button
          Me.cmdresume = New System.Windows.Forms.Button
          Me.cmdLabel1 = New System.Windows.Forms.Label
          Me.cmdEnd = New System.Windows.Forms.Button
          Me.cmdLabel2 = New System.Windows.Forms.Label
          Me.disabled_runs = New System.Windows.Forms.CheckBox
          Me.CheckBox_LogDebug = New System.Windows.Forms.CheckBox
          Me.CheckBox_LogInfo = New System.Windows.Forms.CheckBox
          Me.CheckBox_LogWarn = New System.Windows.Forms.CheckBox
          Me.CheckBox_LogError = New System.Windows.Forms.CheckBox
          Me.CheckBox_LogFatal = New System.Windows.Forms.CheckBox
          Me.SuspendLayout()
          '
          'cmdPause
          '
          Me.cmdPause.BackColor = System.Drawing.SystemColors.Control
          Me.cmdPause.Cursor = System.Windows.Forms.Cursors.Default
          Me.cmdPause.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.cmdPause.ForeColor = System.Drawing.SystemColors.ControlText
          Me.cmdPause.Location = New System.Drawing.Point(83, 26)
          Me.cmdPause.Name = "cmdPause"
          Me.cmdPause.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.cmdPause.Size = New System.Drawing.Size(75, 25)
          Me.cmdPause.TabIndex = 5
          Me.cmdPause.Text = "PAUSE"
          Me.cmdPause.UseVisualStyleBackColor = False
          '
          'cmdStart
          '
          Me.cmdStart.BackColor = System.Drawing.SystemColors.Control
          Me.cmdStart.Cursor = System.Windows.Forms.Cursors.Default
          Me.cmdStart.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.cmdStart.ForeColor = System.Drawing.SystemColors.ControlText
          Me.cmdStart.Location = New System.Drawing.Point(174, 26)
          Me.cmdStart.Name = "cmdStart"
          Me.cmdStart.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.cmdStart.Size = New System.Drawing.Size(75, 25)
          Me.cmdStart.TabIndex = 1
          Me.cmdStart.Text = "START"
          Me.cmdStart.UseVisualStyleBackColor = False
          '
          'cmdresume
          '
          Me.cmdresume.Location = New System.Drawing.Point(2, 26)
          Me.cmdresume.Name = "cmdresume"
          Me.cmdresume.Size = New System.Drawing.Size(75, 25)
          Me.cmdresume.TabIndex = 9
          Me.cmdresume.Text = "RESUME"
          Me.cmdresume.UseVisualStyleBackColor = True
          '
          'cmdLabel1
          '
          Me.cmdLabel1.AutoSize = True
          Me.cmdLabel1.Location = New System.Drawing.Point(39, -1)
          Me.cmdLabel1.Name = "cmdLabel1"
          Me.cmdLabel1.Size = New System.Drawing.Size(74, 14)
          Me.cmdLabel1.TabIndex = 10
          Me.cmdLabel1.Text = "Resume State"
          '
          'cmdEnd
          '
          Me.cmdEnd.Location = New System.Drawing.Point(255, 26)
          Me.cmdEnd.Name = "cmdEnd"
          Me.cmdEnd.Size = New System.Drawing.Size(75, 25)
          Me.cmdEnd.TabIndex = 12
          Me.cmdEnd.Text = "END RUN"
          Me.cmdEnd.UseVisualStyleBackColor = True
          '
          'cmdLabel2
          '
          Me.cmdLabel2.AutoSize = True
          Me.cmdLabel2.Location = New System.Drawing.Point(230, -1)
          Me.cmdLabel2.Name = "cmdLabel2"
          Me.cmdLabel2.Size = New System.Drawing.Size(72, 14)
          Me.cmdLabel2.TabIndex = 13
          Me.cmdLabel2.Text = "Ready to Run"
          '
          'disabled_runs
          '
          Me.disabled_runs.AutoSize = True
          Me.disabled_runs.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.disabled_runs.Location = New System.Drawing.Point(42, 57)
          Me.disabled_runs.Name = "disabled_runs"
          Me.disabled_runs.Size = New System.Drawing.Size(169, 20)
          Me.disabled_runs.TabIndex = 14
          Me.disabled_runs.Text = "Disable Scheduled Runs"
          Me.disabled_runs.UseVisualStyleBackColor = True
          '
          'CheckBox_LogDebug
          '
          Me.CheckBox_LogDebug.AutoSize = True
          Me.CheckBox_LogDebug.Location = New System.Drawing.Point(408, 13)
          Me.CheckBox_LogDebug.Name = "CheckBox_LogDebug"
          Me.CheckBox_LogDebug.Size = New System.Drawing.Size(78, 18)
          Me.CheckBox_LogDebug.TabIndex = 15
          Me.CheckBox_LogDebug.Text = "Log Debug"
          Me.CheckBox_LogDebug.UseVisualStyleBackColor = True
          '
          'CheckBox_LogInfo
          '
          Me.CheckBox_LogInfo.AutoSize = True
          Me.CheckBox_LogInfo.Location = New System.Drawing.Point(408, 38)
          Me.CheckBox_LogInfo.Name = "CheckBox_LogInfo"
          Me.CheckBox_LogInfo.Size = New System.Drawing.Size(65, 18)
          Me.CheckBox_LogInfo.TabIndex = 16
          Me.CheckBox_LogInfo.Text = "Log Info"
          Me.CheckBox_LogInfo.UseVisualStyleBackColor = True
          '
          'CheckBox_LogWarn
          '
          Me.CheckBox_LogWarn.AutoSize = True
          Me.CheckBox_LogWarn.Location = New System.Drawing.Point(408, 63)
          Me.CheckBox_LogWarn.Name = "CheckBox_LogWarn"
          Me.CheckBox_LogWarn.Size = New System.Drawing.Size(73, 18)
          Me.CheckBox_LogWarn.TabIndex = 17
          Me.CheckBox_LogWarn.Text = "Log Warn"
          Me.CheckBox_LogWarn.UseVisualStyleBackColor = True
          '
          'CheckBox_LogError
          '
          Me.CheckBox_LogError.AutoSize = True
          Me.CheckBox_LogError.Location = New System.Drawing.Point(408, 88)
          Me.CheckBox_LogError.Name = "CheckBox_LogError"
          Me.CheckBox_LogError.Size = New System.Drawing.Size(71, 18)
          Me.CheckBox_LogError.TabIndex = 18
          Me.CheckBox_LogError.Text = "Log Error"
          Me.CheckBox_LogError.UseVisualStyleBackColor = True
          '
          'CheckBox_LogFatal
          '
          Me.CheckBox_LogFatal.AutoSize = True
          Me.CheckBox_LogFatal.Location = New System.Drawing.Point(408, 113)
          Me.CheckBox_LogFatal.Name = "CheckBox_LogFatal"
          Me.CheckBox_LogFatal.Size = New System.Drawing.Size(70, 18)
          Me.CheckBox_LogFatal.TabIndex = 19
          Me.CheckBox_LogFatal.Text = "Log Fatal"
          Me.CheckBox_LogFatal.UseVisualStyleBackColor = True
          '
          'Form1
          '
          Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
          Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
          Me.AutoScroll = True
          Me.BackColor = System.Drawing.SystemColors.Control
          Me.ClientSize = New System.Drawing.Size(560, 152)
          Me.Controls.Add(Me.CheckBox_LogFatal)
          Me.Controls.Add(Me.CheckBox_LogError)
          Me.Controls.Add(Me.CheckBox_LogWarn)
          Me.Controls.Add(Me.CheckBox_LogInfo)
          Me.Controls.Add(Me.CheckBox_LogDebug)
          Me.Controls.Add(Me.disabled_runs)
          Me.Controls.Add(Me.cmdLabel2)
          Me.Controls.Add(Me.cmdEnd)
          Me.Controls.Add(Me.cmdLabel1)
          Me.Controls.Add(Me.cmdresume)
          Me.Controls.Add(Me.cmdPause)
          Me.Controls.Add(Me.cmdStart)
          Me.Cursor = System.Windows.Forms.Cursors.Default
          Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
          Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
          Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
          Me.MinimizeBox = False
          Me.Name = "Form1"
          Me.RightToLeft = System.Windows.Forms.RightToLeft.No
          Me.ShowIcon = False
          Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
          Me.Text = "PSICOM Run Control"
          Me.ResumeLayout(False)
          Me.PerformLayout()

     End Sub
     Friend WithEvents cmdresume As System.Windows.Forms.Button
     Friend WithEvents cmdLabel1 As System.Windows.Forms.Label
     Friend WithEvents cmdEnd As System.Windows.Forms.Button
     Friend WithEvents cmdLabel2 As System.Windows.Forms.Label
     Friend WithEvents disabled_runs As System.Windows.Forms.CheckBox
     Friend WithEvents CheckBox_LogDebug As System.Windows.Forms.CheckBox
     Friend WithEvents CheckBox_LogInfo As System.Windows.Forms.CheckBox
     Friend WithEvents CheckBox_LogWarn As System.Windows.Forms.CheckBox
     Friend WithEvents CheckBox_LogError As System.Windows.Forms.CheckBox
     Friend WithEvents CheckBox_LogFatal As System.Windows.Forms.CheckBox
#End Region
End Class