Option Strict Off
Option Explicit On
Friend Class frmSystemInfo
	Inherits System.Windows.Forms.Form
	Dim mMachineNumber As Integer
	
	Private Sub cmdEnableDevice_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnableDevice.Click
		Dim vFlag As Integer
		Dim vRet As Boolean
		Dim vErrorCode As Integer
		
		lblMessage.Text = "Working..."
		System.Windows.Forms.Application.DoEvents()
		
		vFlag = chkEnableDevice.CheckState
		
		vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, vFlag)
		If vRet = True Then
			If vFlag = 1 Then
				lblMessage.Text = "Enable Device Success!"
			Else
				lblMessage.Text = "Disable Device Success!"
			End If
		Else
			sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
			lblMessage.Text = ErrorPrint(vErrorCode)
			Exit Sub
		End If
		
		If chkEnableDevice.CheckState = 1 Then
			chkEnableDevice.CheckState = System.Windows.Forms.CheckState.Unchecked
		Else
			chkEnableDevice.CheckState = System.Windows.Forms.CheckState.Checked
		End If
		
		System.Windows.Forms.Application.DoEvents()
	End Sub
	
	Private Sub cmdGetDeviceInfo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdGetDeviceInfo.Click
		Dim vInfo As Integer
		Dim vValue(0) As Integer
		Dim vRet As Boolean
		Dim vErrorCode As Integer
		
		lblMessage.Text = "Working..."
		System.Windows.Forms.Application.DoEvents()
		
		vInfo = cmbSatus.SelectedIndex + 1
		
		vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
		If vRet = False Then
			lblMessage.Text = gstrNoDevice
			Exit Sub
		End If
		
		vRet = sbxpc.SBXPCDLL.GetDeviceInfo(mMachineNumber, vInfo, vValue(0))
		If vRet = True Then
			Select Case vInfo
				Case 1 : lblMessage.Text = "(1) = ManagerCount = " & vValue(0)
				Case 2 : lblMessage.Text = "(2) = Device ID = " & vValue(0)
				Case 3 : lblMessage.Text = "(3) = Language = " & vValue(0)
				Case 4 : lblMessage.Text = "(4) = PowerOffTime = " & vValue(0) & " min"
				Case 5 : lblMessage.Text = "(5) = Lock Release = " & vValue(0) & " s"
				Case 6 : lblMessage.Text = "(6) = GLogWarning = " & vValue(0)
				Case 7 : lblMessage.Text = "(7) = SLogWarning = " & vValue(0)
				Case 8 : lblMessage.Text = "(8) = ReVerifyTime = " & vValue(0)
				Case 9 : lblMessage.Text = "(9) = Baudrate = " & vValue(0)
				Case 10 : lblMessage.Text = "(10) = Parity (no effect) = " & vValue(0)
				Case 11 : lblMessage.Text = "(11) = Stop (no effect) = " & vValue(0)
				Case 12 : lblMessage.Text = "(12) = DateSeperate = " & vValue(0)
				Case 13 : lblMessage.Text = "(13) = Identification mode = " & vValue(0)
			End Select
		Else
			sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
			lblMessage.Text = ErrorPrint(vErrorCode)
		End If
		
		sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
	End Sub
	
	Private Sub cmdGetDeviceStaus_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdGetDeviceStaus.Click
		Dim vStatus As Integer
		Dim vValue(0) As Integer
		Dim vRet As Boolean
		Dim vErrorCode As Integer
		
		lblMessage.Text = "Working..."
		System.Windows.Forms.Application.DoEvents()
		
		vStatus = cmbSatus.SelectedIndex + 1
		
		vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
		If vRet = False Then
			lblMessage.Text = gstrNoDevice
			Exit Sub
		End If
		
		vRet = sbxpc.SBXPCDLL.GetDeviceStatus(mMachineNumber, vStatus, vValue(0))
		If vRet = True Then
			Select Case vStatus
				Case 1 : lblMessage.Text = "(1) = Manager count = " & vValue(0)
				Case 2 : lblMessage.Text = "(2) = User count = " & vValue(0)
				Case 3 : lblMessage.Text = "(3) = Fp count = " & vValue(0)
				Case 4 : lblMessage.Text = "(4) = Password count = " & vValue(0)
				Case 5 : lblMessage.Text = "(5) = SLog count = " & vValue(0)
				Case 6 : lblMessage.Text = "(6) = GLog count = " & vValue(0)
				Case 7 : lblMessage.Text = "(7) = Card count = " & vValue(0)
                Case 8 : lblMessage.Text = "(8) = Alarm status = " & vValue(0)
                Case 9 : lblMessage.Text = "(9) = Face Count = " & vValue(0)
                Case 10 : lblMessage.Text = "(10) = SLog unread count = " & vValue(0)
                Case 11 : lblMessage.Text = "(11) = GLog unread count = " & vValue(0)

            End Select
		Else
			sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
			lblMessage.Text = ErrorPrint(vErrorCode)
		End If
		
		sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
	End Sub
	
	Private Sub cmdGetDeviceTime_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdGetDeviceTime.Click
		Dim vYear As Integer
		Dim vMonth As Integer
		Dim vDay As Integer
		Dim vHour As Integer
        Dim vMinute As Integer
        Dim vSecond As Integer
		Dim vDayOfWeek As Integer
		Dim strDataTime As String
		Dim vRet As Boolean
		Dim vErrorCode As Integer
		
		lblMessage.Text = "Working..."
		System.Windows.Forms.Application.DoEvents()
		
		vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
		If vRet = False Then
			lblMessage.Text = gstrNoDevice
			Exit Sub
		End If
		
        vRet = sbxpc.SBXPCDLL.GetDeviceTime(mMachineNumber, vYear, vMonth, vDay, vHour, vMinute, vSecond, vDayOfWeek)
		If vRet = True Then
			If vDayOfWeek = 0 Then vDayOfWeek = 7
			strDataTime = "Date = " & CStr(vYear) & "/" & CStr(vMonth) & "/" & CStr(vDay) & " , " & GetWeekDay(vDayOfWeek) & " , Time = " & CStr(vHour) & ":" & VB6.Format(CStr(vMinute), "0#") & VB6.Format(CStr(vSecond), "0#")
			lblMessage.Text = strDataTime
		Else
			sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
			lblMessage.Text = ErrorPrint(vErrorCode)
		End If
		sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
		System.Windows.Forms.Application.DoEvents()
	End Sub
	
	Private Sub cmdGetDoorStatus_Click()
        '		Dim vInfo As Integer
		Dim vValue(0) As Integer
		Dim vErrorCode As Integer
		Dim vRet As Boolean
		
		lblMessage.Text = "Working..."
		System.Windows.Forms.Application.DoEvents()
		
		vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
		If vRet = False Then
			lblMessage.Text = gstrNoDevice
			Exit Sub
		End If
		
		vRet = sbxpc.SBXPCDLL.GetDoorStatus(mMachineNumber, vValue(0))
		If vRet = True Then
			lblMessage.Text = "Success!"
		Else
			sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
			lblMessage.Text = ErrorPrint(vErrorCode)
		End If
		
		txtSetDevInfo.Text = CStr(vValue(0))
		
		sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
		System.Windows.Forms.Application.DoEvents()
		
	End Sub
	
	Private Sub cmdPowerOn_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPowerOn.Click
        sbxpc.SBXPCDLL.PowerOnAllDevice(frmMain.gMachineNumber)
	End Sub
	
	Private Sub cmdSetDeviceInfo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSetDeviceInfo.Click
		Dim vInfo As Integer
		Dim vValue As Integer
		Dim vErrorCode As Integer
		Dim vRet As Boolean
		
		lblMessage.Text = "Working..."
		System.Windows.Forms.Application.DoEvents()
		
		vInfo = cmbSatus.SelectedIndex + 1
		vValue = Val(txtSetDevInfo.Text)
		
		vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
		If vRet = False Then
			lblMessage.Text = gstrNoDevice
			Exit Sub
		End If
		
		vRet = sbxpc.SBXPCDLL.SetDeviceInfo(mMachineNumber, vInfo, vValue)
		If vRet = True Then
			lblMessage.Text = "Success!"
			
			'SmackBio
			If vInfo = 2 Then
				mMachineNumber = vValue
				Sleep(1000)
			End If
		Else
			sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
			lblMessage.Text = ErrorPrint(vErrorCode)
		End If
		sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
		System.Windows.Forms.Application.DoEvents()
	End Sub
	
	Private Sub cmdSetDeviceTime_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSetDeviceTime.Click
		Dim vRet As Boolean
		Dim vErrorCode As Integer
		
		lblMessage.Text = "Working..."
		System.Windows.Forms.Application.DoEvents()
		
		vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
		If vRet = False Then
			lblMessage.Text = gstrNoDevice
			Exit Sub
		End If
		
		vRet = sbxpc.SBXPCDLL.SetDeviceTime(mMachineNumber)
		If vRet = True Then
			lblMessage.Text = "Success!"
		Else
			sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
			lblMessage.Text = ErrorPrint(vErrorCode)
		End If
		
		sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
		System.Windows.Forms.Application.DoEvents()
	End Sub
	
	Private Sub cmdSetDoorStatus_Click()
        '		Dim vInfo As Integer
		Dim vValue As Integer
		Dim vErrorCode As Integer
		Dim vRet As Boolean
		
		lblMessage.Text = "Working..."
		System.Windows.Forms.Application.DoEvents()
		
		vValue = Val(txtSetDevInfo.Text)
		
		vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
		If vRet = False Then
			lblMessage.Text = gstrNoDevice
			Exit Sub
		End If
		
		vRet = sbxpc.SBXPCDLL.SetDoorStatus(mMachineNumber, vValue)
		If vRet = True Then
			lblMessage.Text = "Success!"
		Else
			sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
			lblMessage.Text = ErrorPrint(vErrorCode)
		End If
		sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
		System.Windows.Forms.Application.DoEvents()
	End Sub
	
	Private Sub PowerOffDevice_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles PowerOffDevice.Click
		Dim vRet As Boolean
		Dim vErrorCode As Integer
		
		lblMessage.Text = "Working..."
		System.Windows.Forms.Application.DoEvents()
		
		vRet = sbxpc.SBXPCDLL.PowerOffDevice(mMachineNumber)
		If vRet = True Then
			lblMessage.Text = "Success!"
		Else
			lblMessage.Text = ErrorPrint(vErrorCode)
			sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
		End If
	End Sub
	
	Private Sub frmSystemInfo_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		cmbSatus.SelectedIndex = 0
		mMachineNumber = frmMain.gMachineNumber
	End Sub
	
	Private Sub frmSystemInfo_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        frmMain.Visible = True
	End Sub
	
	Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
		Me.Close()
    End Sub
	
	Private Function GetWeekDay(ByRef anDay As Integer) As String
		Select Case anDay
			Case 1
				GetWeekDay = "Sunday"
			Case 2
				GetWeekDay = "Monday"
			Case 3
				GetWeekDay = "Tuesday"
			Case 4
				GetWeekDay = "Wednesday"
			Case 5
				GetWeekDay = "Thursday"
			Case 6
				GetWeekDay = "Friday"
			Case 7
                GetWeekDay = "Saturday"
            Case Else
                GetWeekDay = ""
        End Select
    End Function
End Class