<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEvent
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
        Me.txtPortNumber = New System.Windows.Forms.TextBox
        Me.cmdClear = New System.Windows.Forms.Button
        Me.cmdEndMoniter = New System.Windows.Forms.Button
        Me.cmdStartMoniter = New System.Windows.Forms.Button
        Me.groupBox2 = New System.Windows.Forms.GroupBox
        Me.txtSourceIP = New System.Windows.Forms.TextBox
        Me.label2 = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        Me.radioNetwork = New System.Windows.Forms.RadioButton
        Me.groupBox1 = New System.Windows.Forms.GroupBox
        Me.cmbBaudrate = New System.Windows.Forms.ComboBox
        Me.cmbComPort = New System.Windows.Forms.ComboBox
        Me.label1 = New System.Windows.Forms.Label
        Me.lblEnrollData = New System.Windows.Forms.Label
        Me.radioSerial = New System.Windows.Forms.RadioButton
        Me.lstEvents = New System.Windows.Forms.ListBox
        Me.groupBox2.SuspendLayout()
        Me.groupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtPortNumber
        '
        Me.txtPortNumber.Location = New System.Drawing.Point(132, 71)
        Me.txtPortNumber.Name = "txtPortNumber"
        Me.txtPortNumber.Size = New System.Drawing.Size(134, 26)
        Me.txtPortNumber.TabIndex = 76
        Me.txtPortNumber.Text = "5005"
        '
        'cmdClear
        '
        Me.cmdClear.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClear.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClear.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClear.Location = New System.Drawing.Point(505, 150)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClear.Size = New System.Drawing.Size(149, 30)
        Me.cmdClear.TabIndex = 81
        Me.cmdClear.Text = "Clear"
        Me.cmdClear.UseVisualStyleBackColor = False
        '
        'cmdEndMoniter
        '
        Me.cmdEndMoniter.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEndMoniter.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEndMoniter.Enabled = False
        Me.cmdEndMoniter.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEndMoniter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEndMoniter.Location = New System.Drawing.Point(331, 150)
        Me.cmdEndMoniter.Name = "cmdEndMoniter"
        Me.cmdEndMoniter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEndMoniter.Size = New System.Drawing.Size(149, 30)
        Me.cmdEndMoniter.TabIndex = 80
        Me.cmdEndMoniter.Text = "End Moniter"
        Me.cmdEndMoniter.UseVisualStyleBackColor = False
        '
        'cmdStartMoniter
        '
        Me.cmdStartMoniter.BackColor = System.Drawing.SystemColors.Control
        Me.cmdStartMoniter.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdStartMoniter.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdStartMoniter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdStartMoniter.Location = New System.Drawing.Point(159, 150)
        Me.cmdStartMoniter.Name = "cmdStartMoniter"
        Me.cmdStartMoniter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdStartMoniter.Size = New System.Drawing.Size(149, 30)
        Me.cmdStartMoniter.TabIndex = 79
        Me.cmdStartMoniter.Text = "Start Moniter"
        Me.cmdStartMoniter.UseVisualStyleBackColor = False
        '
        'groupBox2
        '
        Me.groupBox2.Controls.Add(Me.txtPortNumber)
        Me.groupBox2.Controls.Add(Me.txtSourceIP)
        Me.groupBox2.Controls.Add(Me.label2)
        Me.groupBox2.Controls.Add(Me.label3)
        Me.groupBox2.Controls.Add(Me.radioNetwork)
        Me.groupBox2.Font = New System.Drawing.Font("Times New Roman", 12.0!)
        Me.groupBox2.ForeColor = System.Drawing.Color.Black
        Me.groupBox2.Location = New System.Drawing.Point(442, 12)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(289, 112)
        Me.groupBox2.TabIndex = 78
        Me.groupBox2.TabStop = False
        '
        'txtSourceIP
        '
        Me.txtSourceIP.Location = New System.Drawing.Point(132, 39)
        Me.txtSourceIP.Name = "txtSourceIP"
        Me.txtSourceIP.Size = New System.Drawing.Size(134, 26)
        Me.txtSourceIP.TabIndex = 75
        Me.txtSourceIP.Text = "0.0.0.0"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.BackColor = System.Drawing.SystemColors.Control
        Me.label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.label2.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label2.Location = New System.Drawing.Point(30, 73)
        Me.label2.Name = "label2"
        Me.label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label2.Size = New System.Drawing.Size(92, 19)
        Me.label2.TabIndex = 74
        Me.label2.Text = "Port Number:"
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.BackColor = System.Drawing.SystemColors.Control
        Me.label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.label3.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label3.Location = New System.Drawing.Point(30, 39)
        Me.label3.Name = "label3"
        Me.label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label3.Size = New System.Drawing.Size(73, 19)
        Me.label3.TabIndex = 73
        Me.label3.Text = "Source IP:"
        '
        'radioNetwork
        '
        Me.radioNetwork.AutoSize = True
        Me.radioNetwork.Checked = True
        Me.radioNetwork.Location = New System.Drawing.Point(35, 0)
        Me.radioNetwork.Name = "radioNetwork"
        Me.radioNetwork.Size = New System.Drawing.Size(128, 23)
        Me.radioNetwork.TabIndex = 1
        Me.radioNetwork.TabStop = True
        Me.radioNetwork.Text = "Network Device"
        Me.radioNetwork.UseVisualStyleBackColor = True
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.cmbBaudrate)
        Me.groupBox1.Controls.Add(Me.cmbComPort)
        Me.groupBox1.Controls.Add(Me.label1)
        Me.groupBox1.Controls.Add(Me.lblEnrollData)
        Me.groupBox1.Controls.Add(Me.radioSerial)
        Me.groupBox1.Font = New System.Drawing.Font("Times New Roman", 12.0!)
        Me.groupBox1.ForeColor = System.Drawing.Color.Black
        Me.groupBox1.Location = New System.Drawing.Point(82, 12)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(289, 112)
        Me.groupBox1.TabIndex = 77
        Me.groupBox1.TabStop = False
        '
        'cmbBaudrate
        '
        Me.cmbBaudrate.FormattingEnabled = True
        Me.cmbBaudrate.Items.AddRange(New Object() {"9600", "19200", "38400", "57600", "115200"})
        Me.cmbBaudrate.Location = New System.Drawing.Point(115, 70)
        Me.cmbBaudrate.Name = "cmbBaudrate"
        Me.cmbBaudrate.Size = New System.Drawing.Size(132, 27)
        Me.cmbBaudrate.TabIndex = 74
        '
        'cmbComPort
        '
        Me.cmbComPort.FormattingEnabled = True
        Me.cmbComPort.Items.AddRange(New Object() {"COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8"})
        Me.cmbComPort.Location = New System.Drawing.Point(115, 37)
        Me.cmbComPort.Name = "cmbComPort"
        Me.cmbComPort.Size = New System.Drawing.Size(132, 27)
        Me.cmbComPort.TabIndex = 73
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.BackColor = System.Drawing.SystemColors.Control
        Me.label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.label1.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label1.Location = New System.Drawing.Point(29, 74)
        Me.label1.Name = "label1"
        Me.label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label1.Size = New System.Drawing.Size(67, 19)
        Me.label1.TabIndex = 72
        Me.label1.Text = "Baudrate:"
        '
        'lblEnrollData
        '
        Me.lblEnrollData.AutoSize = True
        Me.lblEnrollData.BackColor = System.Drawing.SystemColors.Control
        Me.lblEnrollData.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEnrollData.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEnrollData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEnrollData.Location = New System.Drawing.Point(29, 40)
        Me.lblEnrollData.Name = "lblEnrollData"
        Me.lblEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEnrollData.Size = New System.Drawing.Size(68, 19)
        Me.lblEnrollData.TabIndex = 71
        Me.lblEnrollData.Text = "ComPort:"
        '
        'radioSerial
        '
        Me.radioSerial.AutoSize = True
        Me.radioSerial.Location = New System.Drawing.Point(35, 0)
        Me.radioSerial.Name = "radioSerial"
        Me.radioSerial.Size = New System.Drawing.Size(107, 23)
        Me.radioSerial.TabIndex = 0
        Me.radioSerial.Text = "Serial Device"
        Me.radioSerial.UseVisualStyleBackColor = True
        '
        'lstEvents
        '
        Me.lstEvents.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstEvents.FormattingEnabled = True
        Me.lstEvents.ItemHeight = 15
        Me.lstEvents.Location = New System.Drawing.Point(15, 203)
        Me.lstEvents.Name = "lstEvents"
        Me.lstEvents.Size = New System.Drawing.Size(786, 259)
        Me.lstEvents.TabIndex = 82
        '
        'frmEvent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(813, 491)
        Me.Controls.Add(Me.cmdClear)
        Me.Controls.Add(Me.cmdEndMoniter)
        Me.Controls.Add(Me.cmdStartMoniter)
        Me.Controls.Add(Me.groupBox2)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.lstEvents)
        Me.Name = "frmEvent"
        Me.Text = "frmEvent"
        Me.groupBox2.ResumeLayout(False)
        Me.groupBox2.PerformLayout()
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents txtPortNumber As System.Windows.Forms.TextBox
    Public WithEvents cmdClear As System.Windows.Forms.Button
    Public WithEvents cmdEndMoniter As System.Windows.Forms.Button
    Public WithEvents cmdStartMoniter As System.Windows.Forms.Button
    Private WithEvents groupBox2 As System.Windows.Forms.GroupBox
    Private WithEvents txtSourceIP As System.Windows.Forms.TextBox
    Public WithEvents label2 As System.Windows.Forms.Label
    Public WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents radioNetwork As System.Windows.Forms.RadioButton
    Private WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents cmbBaudrate As System.Windows.Forms.ComboBox
    Private WithEvents cmbComPort As System.Windows.Forms.ComboBox
    Public WithEvents label1 As System.Windows.Forms.Label
    Public WithEvents lblEnrollData As System.Windows.Forms.Label
    Private WithEvents radioSerial As System.Windows.Forms.RadioButton
    Private WithEvents lstEvents As System.Windows.Forms.ListBox
End Class
