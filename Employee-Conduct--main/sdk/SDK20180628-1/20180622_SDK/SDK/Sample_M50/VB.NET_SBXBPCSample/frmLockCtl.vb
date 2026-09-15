Option Strict Off
Option Explicit On
Friend Class frmLockCtl
	Inherits System.Windows.Forms.Form
	Dim mMachineNumber As Integer
	
	Private Sub cmdGetDoorStatus_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdGetDoorStatus.Click
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
			If vValue(0) = 1 Then
				lblMessage.Text = "Uncond Door Open State!"
			ElseIf vValue(0) = 2 Then 
				lblMessage.Text = "Uncond Door Close State!"
			ElseIf vValue(0) = 3 Then 
				lblMessage.Text = "Program Door Open State!"
			ElseIf vValue(0) = 4 Then 
				lblMessage.Text = "Auto Recorver State!"
			Else
				lblMessage.Text = "User State !"
			End If
		Else
			sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
			lblMessage.Text = ErrorPrint(vErrorCode)
		End If
		
		'txtSetDevInfo.Text = vValue(0)
		
		sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
		System.Windows.Forms.Application.DoEvents()
		
	End Sub
	
	Private Sub cmdAutoRecover_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAutoRecover.Click
		Dim vErrorCode As Integer
		Dim vRet As Boolean
		
		lblMessage.Text = "Working..."
		System.Windows.Forms.Application.DoEvents()
		
		vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
		If vRet = False Then
			lblMessage.Text = gstrNoDevice
			Exit Sub
		End If
		
		vRet = sbxpc.SBXPCDLL.SetDoorStatus(mMachineNumber, 4)
		If vRet = True Then
			lblMessage.Text = "Auto Recover Success!"
		Else
			sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
			lblMessage.Text = ErrorPrint(vErrorCode)
		End If
		sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
		System.Windows.Forms.Application.DoEvents()
	End Sub
	
	Private Sub cmdDoorOpen_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDoorOpen.Click
		Dim vErrorCode As Integer
		Dim vRet As Boolean
		
		lblMessage.Text = "Working..."
		System.Windows.Forms.Application.DoEvents()
		
		vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
		If vRet = False Then
			lblMessage.Text = gstrNoDevice
			Exit Sub
		End If
		
		vRet = sbxpc.SBXPCDLL.SetDoorStatus(mMachineNumber, 3)
		If vRet = True Then
			lblMessage.Text = "Door Open Success!"
		Else
			sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
			lblMessage.Text = ErrorPrint(vErrorCode)
		End If
		sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
		System.Windows.Forms.Application.DoEvents()
	End Sub
	
	Private Sub cmdRestart_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRestart.Click
		Dim vErrorCode As Integer
		Dim vRet As Boolean
		
		lblMessage.Text = "Working..."
		System.Windows.Forms.Application.DoEvents()
		
		vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
		If vRet = False Then
			lblMessage.Text = gstrNoDevice
			Exit Sub
		End If
		
		vRet = sbxpc.SBXPCDLL.SetDoorStatus(mMachineNumber, 5)
		If vRet = True Then
            lblMessage.Text = "Restart Success!"
		Else
			sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
			lblMessage.Text = ErrorPrint(vErrorCode)
		End If
		sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
		System.Windows.Forms.Application.DoEvents()
	End Sub
	
	Private Sub cmdUncondClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUncondClose.Click
		Dim vErrorCode As Integer
		Dim vRet As Boolean
		
		lblMessage.Text = "Working..."
		System.Windows.Forms.Application.DoEvents()
		
		vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
		If vRet = False Then
			lblMessage.Text = gstrNoDevice
			Exit Sub
		End If
		
		vRet = sbxpc.SBXPCDLL.SetDoorStatus(mMachineNumber, 2)
		If vRet = True Then
			lblMessage.Text = "Uncond Door Close Success!"
		Else
			sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
			lblMessage.Text = ErrorPrint(vErrorCode)
		End If
		sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
		System.Windows.Forms.Application.DoEvents()
	End Sub
	
	Private Sub cmdUncondOpen_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUncondOpen.Click
		Dim vErrorCode As Integer
		Dim vRet As Boolean
		
		lblMessage.Text = "Working..."
		System.Windows.Forms.Application.DoEvents()
		
		vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
		If vRet = False Then
			lblMessage.Text = gstrNoDevice
			Exit Sub
		End If
		
		vRet = sbxpc.SBXPCDLL.SetDoorStatus(mMachineNumber, 1)
		If vRet = True Then
			lblMessage.Text = "Uncond Door Open Success!"
		Else
			sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
			lblMessage.Text = ErrorPrint(vErrorCode)
		End If
		sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
		System.Windows.Forms.Application.DoEvents()
	End Sub
	
	Private Sub cmdWarnCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdWarnCancel.Click
		Dim vErrorCode As Integer
		Dim vRet As Boolean
		
		lblMessage.Text = "Working..."
		System.Windows.Forms.Application.DoEvents()
		
		vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
		If vRet = False Then
			lblMessage.Text = gstrNoDevice
			Exit Sub
		End If
		
		vRet = sbxpc.SBXPCDLL.SetDoorStatus(mMachineNumber, 6)
		If vRet = True Then
            lblMessage.Text = "Warn Cancel Success!"
		Else
			sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
			lblMessage.Text = ErrorPrint(vErrorCode)
		End If
		sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
		System.Windows.Forms.Application.DoEvents()
	End Sub
	
	Private Sub frmLockCtl_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		mMachineNumber = frmMain.gMachineNumber
	End Sub
	Private Sub frmLockCtl_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        frmMain.Visible = True
	End Sub
End Class