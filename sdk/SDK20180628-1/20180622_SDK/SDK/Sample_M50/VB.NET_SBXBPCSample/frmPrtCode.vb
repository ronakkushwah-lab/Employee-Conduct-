Option Strict Off
Option Explicit On
Friend Class frmSerialNo
	Inherits System.Windows.Forms.Form
	Dim mMachineNumber As Integer
	
	Private Sub cmdGetBackupNumber_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdGetBackupNumber.Click
		Dim vBackupNumber As Integer
		Dim vErrorCode As Integer
		
		lblMessage.Text = "Waiting..."
		txtBackupNo.Text = ""
		System.Windows.Forms.Application.DoEvents()
		
		If sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False) = False Then
			lblMessage.Text = gstrNoDevice
			Exit Sub
		End If
		
		vBackupNumber = sbxpc.SBXPCDLL.GetBackupNumber(mMachineNumber)
		If vBackupNumber <> 0 Then
			txtBackupNo.Text = CStr(vBackupNumber)
			lblMessage.Text = "Success"
		Else
			sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
			lblMessage.Text = ErrorPrint(vErrorCode)
		End If
		
		sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
	End Sub
	
	Private Sub cmdGetProductCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdGetProductCode.Click
        Dim vProductCode As String = ""
		Dim vRet As Integer
		Dim vErrorCode As Integer
		
		lblMessage.Text = "Waiting..."
		txtProductCode.Text = ""
		System.Windows.Forms.Application.DoEvents()
		
		vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
		If vRet = False Then
			lblMessage.Text = gstrNoDevice
			Exit Sub
		End If
		
        vRet = sbxpc.SBXPCDLL.GetProductCode(mMachineNumber, vProductCode)
		If vRet = True Then
			txtProductCode.Text = vProductCode
			lblMessage.Text = "Success"
		Else
			sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
			lblMessage.Text = ErrorPrint(vErrorCode)
		End If
		
		sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
	End Sub
	
	Private Sub cmdGetSerialNumber_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdGetSerialNumber.Click
        Dim vSerialNumber As String = ""
		Dim vRet As Integer
		Dim vErrorCode As Integer
		
		lblMessage.Text = "Waiting..."
		txtSerialNo.Text = ""
		System.Windows.Forms.Application.DoEvents()
		
		vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
		If vRet = False Then
			lblMessage.Text = gstrNoDevice
			Exit Sub
		End If
		
        vRet = sbxpc.SBXPCDLL.GetSerialNumber(mMachineNumber, vSerialNumber)
		If vRet = True Then
			txtSerialNo.Text = vSerialNumber
			lblMessage.Text = "Success"
		Else
			sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
			lblMessage.Text = ErrorPrint(vErrorCode)
		End If
		
		sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
	End Sub
	
	Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
		Me.Close()
    End Sub

    Private Sub frmSerialNo_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        frmMain.Visible = True
    End Sub
	
	Private Sub frmSerialNo_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		mMachineNumber = frmMain.gMachineNumber
	End Sub
	
    '	Private Sub frmSerialNo_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
    '		Me.Close()
    '		frmMain.Visible = True
    '	End Sub
End Class