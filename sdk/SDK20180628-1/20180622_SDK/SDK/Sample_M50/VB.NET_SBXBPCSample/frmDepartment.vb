Option Strict Off
Option Explicit On
Imports System.Runtime.InteropServices

Public Class frmDepartment
    Const DEPT_COUNT As Integer = 20
    Dim departmentNameList(DEPT_COUNT - 1) As String
    Private Sub frmDepartment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DepartmentNameInit()
        DrawDepartmentNameList()
    End Sub

    Private Sub DepartmentNameInit()
        Dim i As Integer
        For i = 0 To DEPT_COUNT - 1
            departmentNameList(i) = ""
        Next
    End Sub

    Private Sub DrawDepartmentNameList()
        Dim i As Integer
        lstDepartment.Items.Clear()
        For i = 0 To DEPT_COUNT - 1
            lstDepartment.Items.Add("[No.] " & VB6.Format(CStr(i), "0# ") & _
                                "[Name] " & departmentNameList(i))
        Next
    End Sub

    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        If lstDepartment.SelectedIndex = -1 Then
            Exit Sub
        End If

        departmentNameList(lstDepartment.SelectedIndex) = txtDepartmentName.Text
        DrawDepartmentNameList()
    End Sub

    Private Sub lstDepartment_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstDepartment.SelectedIndexChanged
        If lstDepartment.SelectedIndex = -1 Then
            Exit Sub
        End If

        txtDepartmentName.Text = departmentNameList(lstDepartment.SelectedIndex)
        txtDepartmentName.Focus()
    End Sub

    Private Sub frmDepartment_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        frmMain.Visible = True
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Close()
    End Sub

    Private Sub cmdRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRead.Click
        Dim bRet As Boolean
        Dim vErrorCode As Integer
        Dim i As Integer
        Dim departmentName As String = ""

        lblMessage.Text = "Working..."
        Application.DoEvents()

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, False)
        If Not bRet Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        For i = 0 To DEPT_COUNT - 1

            bRet = sbxpc.SBXPCDLL.GetDepartName(frmMain.gMachineNumber, i, 0, departmentName)

            If bRet Then
                lblMessage.Text = "Success"
                departmentNameList(i) = departmentName
            Else
                sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
                lblMessage.Text = ErrorPrint(vErrorCode)
                departmentNameList(i) = ""
            End If
        Next

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, True)

        DrawDepartmentNameList()
    End Sub

    Private Sub cmdWrite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWrite.Click
        Dim bRet As Boolean
        Dim vErrorCode As Integer
        Dim i As Integer

        lblMessage.Text = "Working..."
        Application.DoEvents()

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, False)
        If Not bRet Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        For i = 0 To DEPT_COUNT - 1

            bRet = sbxpc.SBXPCDLL.SetDepartName(frmMain.gMachineNumber, i, 0, departmentNameList(i))

            If bRet Then
                lblMessage.Text = "Success"
            Else
                sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
                lblMessage.Text = ErrorPrint(vErrorCode)
            End If
        Next

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, True)
    End Sub
End Class