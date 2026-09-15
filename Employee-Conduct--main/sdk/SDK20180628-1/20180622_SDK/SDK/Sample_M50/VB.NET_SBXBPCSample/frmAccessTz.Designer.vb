<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAccessTz
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.label2 = New System.Windows.Forms.Label
        Me.cmbVerifyMode = New System.Windows.Forms.ComboBox
        Me.lstTimeZone = New System.Windows.Forms.ListBox
        Me.dtEnd = New System.Windows.Forms.DateTimePicker
        Me.label1 = New System.Windows.Forms.Label
        Me.dtStart = New System.Windows.Forms.DateTimePicker
        Me.cmdExit = New System.Windows.Forms.Button
        Me.cmdWrite = New System.Windows.Forms.Button
        Me.cmdRead = New System.Windows.Forms.Button
        Me.cmdUpdate = New System.Windows.Forms.Button
        Me.lblEnrollData = New System.Windows.Forms.Label
        Me.lblMessage = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.BackColor = System.Drawing.SystemColors.Control
        Me.label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.label2.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label2.Location = New System.Drawing.Point(201, 70)
        Me.label2.Name = "label2"
        Me.label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label2.Size = New System.Drawing.Size(88, 19)
        Me.label2.TabIndex = 94
        Me.label2.Text = "Verify Mode:"
        '
        'cmbVerifyMode
        '
        Me.cmbVerifyMode.Font = New System.Drawing.Font("Times New Roman", 12.0!)
        Me.cmbVerifyMode.FormattingEnabled = True
        Me.cmbVerifyMode.Items.AddRange(New Object() {"FACE|FP|CD|PWD", "CD&FP", "FP&PWD", "CD&PWD", "FP&CD&PWD", "System Default VM", "FP", "CD", "8-INVALID-DON\'T SELECT ME", "ID&PWD", "10-INVALID-DON\'T SELECT ME", "FACE", "CD&FACE", "FACE&PWD", "FACE&CD&PWD", "FACE&FP"})
        Me.cmbVerifyMode.Location = New System.Drawing.Point(205, 94)
        Me.cmbVerifyMode.Name = "cmbVerifyMode"
        Me.cmbVerifyMode.Size = New System.Drawing.Size(249, 27)
        Me.cmbVerifyMode.TabIndex = 93
        '
        'lstTimeZone
        '
        Me.lstTimeZone.Font = New System.Drawing.Font("Times New Roman", 12.0!)
        Me.lstTimeZone.FormattingEnabled = True
        Me.lstTimeZone.ItemHeight = 19
        Me.lstTimeZone.Location = New System.Drawing.Point(15, 134)
        Me.lstTimeZone.Name = "lstTimeZone"
        Me.lstTimeZone.Size = New System.Drawing.Size(501, 384)
        Me.lstTimeZone.TabIndex = 92
        '
        'dtEnd
        '
        Me.dtEnd.CalendarFont = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.dtEnd.Font = New System.Drawing.Font("Times New Roman", 12.0!)
        Me.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtEnd.Location = New System.Drawing.Point(66, 95)
        Me.dtEnd.Name = "dtEnd"
        Me.dtEnd.Size = New System.Drawing.Size(119, 26)
        Me.dtEnd.TabIndex = 91
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.BackColor = System.Drawing.SystemColors.Control
        Me.label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.label1.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label1.Location = New System.Drawing.Point(11, 95)
        Me.label1.Name = "label1"
        Me.label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label1.Size = New System.Drawing.Size(36, 19)
        Me.label1.TabIndex = 90
        Me.label1.Text = "End:"
        '
        'dtStart
        '
        Me.dtStart.CalendarFont = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.dtStart.Font = New System.Drawing.Font("Times New Roman", 12.0!)
        Me.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtStart.Location = New System.Drawing.Point(66, 66)
        Me.dtStart.Name = "dtStart"
        Me.dtStart.Size = New System.Drawing.Size(119, 26)
        Me.dtStart.TabIndex = 89
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(539, 473)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(104, 46)
        Me.cmdExit.TabIndex = 88
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'cmdWrite
        '
        Me.cmdWrite.BackColor = System.Drawing.SystemColors.Control
        Me.cmdWrite.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdWrite.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdWrite.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdWrite.Location = New System.Drawing.Point(539, 409)
        Me.cmdWrite.Name = "cmdWrite"
        Me.cmdWrite.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdWrite.Size = New System.Drawing.Size(104, 46)
        Me.cmdWrite.TabIndex = 87
        Me.cmdWrite.Text = "Write"
        Me.cmdWrite.UseVisualStyleBackColor = False
        '
        'cmdRead
        '
        Me.cmdRead.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRead.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRead.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRead.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRead.Location = New System.Drawing.Point(539, 344)
        Me.cmdRead.Name = "cmdRead"
        Me.cmdRead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRead.Size = New System.Drawing.Size(104, 46)
        Me.cmdRead.TabIndex = 86
        Me.cmdRead.Text = "Read"
        Me.cmdRead.UseVisualStyleBackColor = False
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUpdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUpdate.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUpdate.Location = New System.Drawing.Point(539, 135)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUpdate.Size = New System.Drawing.Size(104, 46)
        Me.cmdUpdate.TabIndex = 85
        Me.cmdUpdate.Text = "Update"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'lblEnrollData
        '
        Me.lblEnrollData.AutoSize = True
        Me.lblEnrollData.BackColor = System.Drawing.SystemColors.Control
        Me.lblEnrollData.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEnrollData.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEnrollData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEnrollData.Location = New System.Drawing.Point(11, 66)
        Me.lblEnrollData.Name = "lblEnrollData"
        Me.lblEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEnrollData.Size = New System.Drawing.Size(41, 19)
        Me.lblEnrollData.TabIndex = 84
        Me.lblEnrollData.Text = "Start:"
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.SystemColors.Control
        Me.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMessage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMessage.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMessage.Location = New System.Drawing.Point(11, 17)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMessage.Size = New System.Drawing.Size(632, 28)
        Me.lblMessage.TabIndex = 83
        Me.lblMessage.Text = "Message"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmAccessTz
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(657, 531)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.cmbVerifyMode)
        Me.Controls.Add(Me.lstTimeZone)
        Me.Controls.Add(Me.dtEnd)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.dtStart)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdWrite)
        Me.Controls.Add(Me.cmdRead)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Controls.Add(Me.lblEnrollData)
        Me.Controls.Add(Me.lblMessage)
        Me.Name = "frmAccessTz"
        Me.Text = "frmAccessTz"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents cmbVerifyMode As System.Windows.Forms.ComboBox
    Private WithEvents lstTimeZone As System.Windows.Forms.ListBox
    Private WithEvents dtEnd As System.Windows.Forms.DateTimePicker
    Public WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents dtStart As System.Windows.Forms.DateTimePicker
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents cmdWrite As System.Windows.Forms.Button
    Public WithEvents cmdRead As System.Windows.Forms.Button
    Public WithEvents cmdUpdate As System.Windows.Forms.Button
    Public WithEvents lblEnrollData As System.Windows.Forms.Label
    Public WithEvents lblMessage As System.Windows.Forms.Label
End Class
