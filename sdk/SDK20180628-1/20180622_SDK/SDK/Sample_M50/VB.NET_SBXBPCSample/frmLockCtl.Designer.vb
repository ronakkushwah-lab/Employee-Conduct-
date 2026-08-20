<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmLockCtl
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
	Public WithEvents cmdWarnCancel As System.Windows.Forms.Button
	Public WithEvents cmdRestart As System.Windows.Forms.Button
	Public WithEvents cmdAutoRecover As System.Windows.Forms.Button
	Public WithEvents cmdUncondClose As System.Windows.Forms.Button
	Public WithEvents cmdUncondOpen As System.Windows.Forms.Button
	Public WithEvents cmdGetDoorStatus As System.Windows.Forms.Button
	Public WithEvents cmdDoorOpen As System.Windows.Forms.Button
	Public WithEvents lblMessage As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdWarnCancel = New System.Windows.Forms.Button
        Me.cmdRestart = New System.Windows.Forms.Button
        Me.cmdAutoRecover = New System.Windows.Forms.Button
        Me.cmdUncondClose = New System.Windows.Forms.Button
        Me.cmdUncondOpen = New System.Windows.Forms.Button
        Me.cmdGetDoorStatus = New System.Windows.Forms.Button
        Me.cmdDoorOpen = New System.Windows.Forms.Button
        Me.lblMessage = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'cmdWarnCancel
        '
        Me.cmdWarnCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdWarnCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdWarnCancel.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdWarnCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdWarnCancel.Location = New System.Drawing.Point(296, 136)
        Me.cmdWarnCancel.Name = "cmdWarnCancel"
        Me.cmdWarnCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdWarnCancel.Size = New System.Drawing.Size(125, 33)
        Me.cmdWarnCancel.TabIndex = 7
        Me.cmdWarnCancel.Text = "Warn Cancel"
        Me.cmdWarnCancel.UseVisualStyleBackColor = False
        '
        'cmdRestart
        '
        Me.cmdRestart.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRestart.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRestart.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRestart.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRestart.Location = New System.Drawing.Point(156, 137)
        Me.cmdRestart.Name = "cmdRestart"
        Me.cmdRestart.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRestart.Size = New System.Drawing.Size(124, 32)
        Me.cmdRestart.TabIndex = 6
        Me.cmdRestart.Text = "Reboot"
        Me.cmdRestart.UseVisualStyleBackColor = False
        '
        'cmdAutoRecover
        '
        Me.cmdAutoRecover.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAutoRecover.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAutoRecover.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAutoRecover.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAutoRecover.Location = New System.Drawing.Point(156, 98)
        Me.cmdAutoRecover.Name = "cmdAutoRecover"
        Me.cmdAutoRecover.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAutoRecover.Size = New System.Drawing.Size(124, 34)
        Me.cmdAutoRecover.TabIndex = 5
        Me.cmdAutoRecover.Text = "Auto Recover"
        Me.cmdAutoRecover.UseVisualStyleBackColor = False
        '
        'cmdUncondClose
        '
        Me.cmdUncondClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUncondClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUncondClose.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUncondClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUncondClose.Location = New System.Drawing.Point(296, 97)
        Me.cmdUncondClose.Name = "cmdUncondClose"
        Me.cmdUncondClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUncondClose.Size = New System.Drawing.Size(126, 33)
        Me.cmdUncondClose.TabIndex = 4
        Me.cmdUncondClose.Text = "Uncond Close"
        Me.cmdUncondClose.UseVisualStyleBackColor = False
        '
        'cmdUncondOpen
        '
        Me.cmdUncondOpen.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUncondOpen.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUncondOpen.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUncondOpen.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUncondOpen.Location = New System.Drawing.Point(297, 60)
        Me.cmdUncondOpen.Name = "cmdUncondOpen"
        Me.cmdUncondOpen.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUncondOpen.Size = New System.Drawing.Size(126, 33)
        Me.cmdUncondOpen.TabIndex = 3
        Me.cmdUncondOpen.Text = "Uncond Open"
        Me.cmdUncondOpen.UseVisualStyleBackColor = False
        '
        'cmdGetDoorStatus
        '
        Me.cmdGetDoorStatus.BackColor = System.Drawing.SystemColors.Control
        Me.cmdGetDoorStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGetDoorStatus.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetDoorStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGetDoorStatus.Location = New System.Drawing.Point(20, 62)
        Me.cmdGetDoorStatus.Name = "cmdGetDoorStatus"
        Me.cmdGetDoorStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGetDoorStatus.Size = New System.Drawing.Size(122, 33)
        Me.cmdGetDoorStatus.TabIndex = 2
        Me.cmdGetDoorStatus.Text = "Get DoorStatus"
        Me.cmdGetDoorStatus.UseVisualStyleBackColor = False
        '
        'cmdDoorOpen
        '
        Me.cmdDoorOpen.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDoorOpen.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDoorOpen.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDoorOpen.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDoorOpen.Location = New System.Drawing.Point(156, 60)
        Me.cmdDoorOpen.Name = "cmdDoorOpen"
        Me.cmdDoorOpen.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDoorOpen.Size = New System.Drawing.Size(125, 33)
        Me.cmdDoorOpen.TabIndex = 1
        Me.cmdDoorOpen.Text = "Door Open"
        Me.cmdDoorOpen.UseVisualStyleBackColor = False
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.SystemColors.Control
        Me.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMessage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMessage.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMessage.Location = New System.Drawing.Point(20, 17)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMessage.Size = New System.Drawing.Size(401, 28)
        Me.lblMessage.TabIndex = 0
        Me.lblMessage.Text = "Message"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmLockCtl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(438, 178)
        Me.Controls.Add(Me.cmdWarnCancel)
        Me.Controls.Add(Me.cmdRestart)
        Me.Controls.Add(Me.cmdAutoRecover)
        Me.Controls.Add(Me.cmdUncondClose)
        Me.Controls.Add(Me.cmdUncondOpen)
        Me.Controls.Add(Me.cmdGetDoorStatus)
        Me.Controls.Add(Me.cmdDoorOpen)
        Me.Controls.Add(Me.lblMessage)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Location = New System.Drawing.Point(3, 29)
        Me.Name = "frmLockCtl"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Door Open Control"
        Me.ResumeLayout(False)

    End Sub
#End Region 
End Class