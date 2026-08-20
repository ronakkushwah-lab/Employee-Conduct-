Option Strict Off
Option Explicit On

Imports System.IO
Imports System.Runtime.InteropServices

Friend Class frmEnroll
    Inherits System.Windows.Forms.Form
    Const DATASIZE As Short = (1404 + 12) / 4 'SmackBio
    Const DATASIZE_FACE As Short = (27668) / 4
    Const NAMESIZE As Short = 54
    Public gGetState As Boolean
    'Dim glngEnrollData As Object
    Dim gTemplngEnrollData(DATASIZE_FACE) As Integer
    Dim glngEnrollPData As Integer
    Dim gbytEnrollData(DATASIZE_FACE * 5) As Byte
    Dim mMachineNumber As Integer
    Dim mDeviceKind As Integer
    Dim mDeviceVer As Integer
    Dim mConvKind As Integer

    Private dsEnrolls As DataSet


    Private Sub chkEnable_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkEnable.CheckStateChanged
        If chkEnable.CheckState = 1 Then
            chkEnable.Text = "Enable User"
        Else
            chkEnable.Text = "Disable User"
        End If
    End Sub

    Private Sub cmdClearData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClearData.Click
        Dim vRet As Boolean
        Dim vErrorCode As Integer

        lblMessage.Text = "Working..."
        System.Windows.Forms.Application.DoEvents()

        vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
        If vRet = False Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        vRet = sbxpc.SBXPCDLL.ClearKeeperData(mMachineNumber)
        If vRet = True Then
            lblMessage.Text = "ClearKeeperData OK"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
    End Sub

    Private Sub cmdDel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDel.Click
        '        If MsgBox("Are you sure to clear backup database?", MsgBoxStyle.YesNo, "Enroll Management") _
        '                  = MsgBoxResult.No Then
        '           Exit Sub
        '        End If

        EnrollData.DataModule.DeleteDB()

        lblMessage.Text = "Deleted Database"
        Label2.Text = "Total : " & 0

    End Sub

    Private Sub cmdDeleteCompany_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDeleteCompany.Click
        Dim vEMachineNumber As Integer
        Dim vRet As Boolean
        Dim vErrorCode As Integer

        Dim vName As String = ""

        lblMessage.Text = "Working..."
        System.Windows.Forms.Application.DoEvents()

        vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
        If vRet = False Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        vEMachineNumber = CInt(cmbEMachineNumber.Text)

        vRet = sbxpc.SBXPCDLL.SetCompanyName1(mMachineNumber, 0, vName)
        If vRet = True Then
            lblMessage.Text = "Delete Company Name OK"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)

    End Sub

    Private Sub cmdDeleteEnrollData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDeleteEnrollData.Click
        Dim vEnrollNumber As Integer
        Dim vEMachineNumber As Integer
        Dim vFingerNumber As Integer
        Dim vRet As Boolean
        Dim vErrorCode As Integer

        lblMessage.Text = "Working..."
        System.Windows.Forms.Application.DoEvents()

        vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
        If vRet = False Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        vEnrollNumber = Val(txtEnrollNumber.Text)
        vEMachineNumber = CInt(cmbEMachineNumber.Text)
        vFingerNumber = CInt(cmbBackupNumber.Text)

        vRet = sbxpc.SBXPCDLL.DeleteEnrollData(mMachineNumber, vEnrollNumber, vEMachineNumber, vFingerNumber)
        If vRet = True Then
            lblMessage.Text = "DeleteEnrollData OK"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
    End Sub

    Private Sub cmdEmptyEnrollData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEmptyEnrollData.Click
        Dim vRet As Boolean
        Dim vErrorCode As Integer

        lblMessage.Text = "Working..."
        System.Windows.Forms.Application.DoEvents()

        vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
        If vRet = False Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        vRet = sbxpc.SBXPCDLL.EmptyEnrollData(mMachineNumber)
        If vRet = True Then
            lblMessage.Text = "Success!"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
    End Sub

    Private Sub cmdEnableUser_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnableUser.Click
        Dim vEnrollNumber As Integer
        Dim vEMachineNumber As Integer
        Dim vFingerNumber As Integer
        Dim vFlag As Boolean
        Dim vRet As Boolean
        Dim vErrorCode As Integer

        lblMessage.Text = "Working..."
        System.Windows.Forms.Application.DoEvents()

        vEMachineNumber = cmbEMachineNumber.SelectedIndex + 1
        vEnrollNumber = Val(txtEnrollNumber.Text)
        vFingerNumber = CInt(cmbBackupNumber.Text)
        vFlag = chkEnable.CheckState

        vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
        If vRet = False Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        vRet = sbxpc.SBXPCDLL.EnableUser(mMachineNumber, vEnrollNumber, vEMachineNumber, vFingerNumber, vFlag)
        If vRet = True Then
            lblMessage.Text = "Success!"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
    End Sub

    Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdGetAllEnrollData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdGetAllEnrollData.Click
        Dim vEnrollNumber As Integer
        Dim vEMachineNumber As Integer
        Dim vFingerNumber As Integer
        Dim vPrivilege As Integer
        Dim vEnable As Integer
        Dim vFlag As Boolean
        Dim vRet As Integer
        Dim vErrorCode As Integer
        Dim vStr As String = ""
        Dim i As Integer
        Dim vTitle As String

        Dim dbEnrollTble As DataTable
        Dim dbRow As DataRow
        Dim dsChange As DataSet

        Dim gh As GCHandle = GCHandle.Alloc(gTemplngEnrollData, GCHandleType.Pinned)
        Dim AddrOfTemplngEnrollData As IntPtr = gh.AddrOfPinnedObject()

        lstEnrollData.Items.Clear()
        vTitle = Me.Text
        Label2.Text = ""
        lblMessage.Text = "Working..."
        System.Windows.Forms.Application.DoEvents()

        vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
        If vRet = False Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        vRet = sbxpc.SBXPCDLL.ReadAllUserID(mMachineNumber)
        If vRet = True Then
            lblMessage.Text = "ReadAllUserID OK"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
            sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
            Exit Sub
        End If

        '---- Get Enroll data and save into database -------------
        Cursor = System.Windows.Forms.Cursors.WaitCursor
        vFlag = False

        dbEnrollTble = dsEnrolls.Tables(0)

        gGetState = True

        Do
            vRet = sbxpc.SBXPCDLL.GetAllUserID(mMachineNumber, vEnrollNumber, vEMachineNumber, vFingerNumber, vPrivilege, vEnable)
            If vRet <> True Then Exit Do
            vFlag = True
EEE:

            If vFingerNumber >= 50 Then Continue Do

            vRet = sbxpc.SBXPCDLL.GetEnrollData1(mMachineNumber, _
                                                   vEnrollNumber, _
                                                   vFingerNumber, _
                                                   vPrivilege, _
                                                   AddrOfTemplngEnrollData.ToInt32(), _
                                                   glngEnrollPData)

            If vRet <> True Then
                vFlag = False
                vStr = "GetEnrollData"
                sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
                vRet = MsgBox(ErrorPrint(vErrorCode) & ": Continue ?", MsgBoxStyle.YesNoCancel, "GetEnrollData")
                If vRet = MsgBoxResult.Yes Then
                    GoTo EEE
                ElseIf vRet = MsgBoxResult.Cancel Then
                    Cursor = System.Windows.Forms.Cursors.Default
                    sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
                    gGetState = False
                    Exit Sub
                End If
            End If

            ' ----------------------
            For Each dbRow In dbEnrollTble.Rows
                If ((dbRow("EnrollNumber") = vEnrollNumber) And _
                        (dbRow("EMachineNumber") = vEMachineNumber) And _
                        (dbRow("FingerNumber") = vFingerNumber)) Then
                    lblMessage.Text = "Double ID"
                    GoTo FFF
                End If
            Next

            dbRow = dbEnrollTble.NewRow()
            dbRow("EMachineNumber") = vEMachineNumber
            dbRow("EnrollNumber") = vEnrollNumber
            dbRow("FingerNumber") = vFingerNumber
            dbRow("Privilige") = vPrivilege

            If vFingerNumber = 10 Or vFingerNumber = 15 Then
                dbRow("Password1") = glngEnrollPData
            ElseIf vFingerNumber = 11 Then
                dbRow("Password1") = glngEnrollPData
            ElseIf vFingerNumber = 17 Then
                dbRow("Password1") = 0
                For i = 0 To DATASIZE_FACE - 1
                    gbytEnrollData(i * 5) = 1

                    If gTemplngEnrollData(i) = -&H80000000L Then
                        gbytEnrollData(i * 5) = 0
                        gTemplngEnrollData(i) = 0
                    ElseIf gTemplngEnrollData(i) < 0 Then
                        gbytEnrollData(i * 5) = 0
                        gTemplngEnrollData(i) = -gTemplngEnrollData(i)
                    End If

                    gbytEnrollData(i * 5 + 1) = (gTemplngEnrollData(i) \ 256 \ 256 \ 256)
                    gbytEnrollData(i * 5 + 2) = (gTemplngEnrollData(i) \ 256 \ 256) Mod 256
                    gbytEnrollData(i * 5 + 3) = (gTemplngEnrollData(i) \ 256) Mod 256
                    gbytEnrollData(i * 5 + 4) = gTemplngEnrollData(i) Mod 256
                Next

                'dbRow("FPdata") = gbytEnrollData        '<---------- Error

                Dim gbyt(DATASIZE_FACE * 5 - 1) As Byte
                For i = 0 To DATASIZE_FACE * 5 - 1
                    gbyt(i) = gbytEnrollData(i)
                Next
                dbRow("FPdata") = gbyt
            Else
                dbRow("Password1") = 0
                For i = 0 To DATASIZE - 1
                    gbytEnrollData(i * 5) = 1

                    If gTemplngEnrollData(i) = -&H80000000L Then
                        gbytEnrollData(i * 5) = 0
                        gTemplngEnrollData(i) = 0
                    ElseIf gTemplngEnrollData(i) < 0 Then
                        gbytEnrollData(i * 5) = 0
                        gTemplngEnrollData(i) = -gTemplngEnrollData(i)
                    End If

                    gbytEnrollData(i * 5 + 1) = (gTemplngEnrollData(i) \ 256 \ 256 \ 256)
                    gbytEnrollData(i * 5 + 2) = (gTemplngEnrollData(i) \ 256 \ 256) Mod 256
                    gbytEnrollData(i * 5 + 3) = (gTemplngEnrollData(i) \ 256) Mod 256
                    gbytEnrollData(i * 5 + 4) = gTemplngEnrollData(i) Mod 256
                Next

                'dbRow("FPdata") = gbytEnrollData        '<---------- Error

                Dim gbyt(DATASIZE * 5 - 1) As Byte
                For i = 0 To DATASIZE * 5 - 1
                    gbyt(i) = gbytEnrollData(i)
                Next
                dbRow("FPdata") = gbyt

            End If

            dbEnrollTble.Rows.Add(dbRow)
FFF:

            lblMessage.Text = VB6.Format(vEMachineNumber, "00#") & "-" & VB6.Format(vEnrollNumber, "0000#") & "-" & vFingerNumber
            Me.Text = VB6.Format(vEnrollNumber, "0000#")
            txtEnrollNumber.Text = CStr(vEnrollNumber)
            cmbBackupNumber.Text = CStr(vFingerNumber)
            cmbEMachineNumber.Text = CStr(vEMachineNumber)
            cmbPrivilege.Text = CStr(vPrivilege)
            System.Windows.Forms.Application.DoEvents()
        Loop

        Label2.Text = "Total : " & dsEnrolls.Tables.Item("tblEnroll").Rows.Count
        dsChange = dsEnrolls.GetChanges
        EnrollData.DataModule.SaveEnrolls(dsEnrolls)

        gh.Free()

        gGetState = False

        vTitle = Me.Text
        Cursor = System.Windows.Forms.Cursors.Default

        If vFlag = True Then
            lblMessage.Text = "GetAllUserID OK"
        Else
            lblMessage.Text = vStr & ":" & ErrorPrint(vErrorCode)
        End If

        System.Windows.Forms.Application.DoEvents()
        sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
    End Sub

    Private Sub cmdGetEnrollData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdGetEnrollData.Click
        Dim vEnrollNumber As Integer
        Dim vEMachineNumber As Integer
        Dim vFingerNumber As Integer
        Dim vPrivilege As Integer
        Dim vRet As Boolean
        Dim vErrorCode As Integer
        Dim i As Integer

        lstEnrollData.Items.Clear()
        Label2.Text = ""
        lstEnrollData.Items.Clear()
        lblMessage.Text = "Working..."
        System.Windows.Forms.Application.DoEvents()

        vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
        If vRet = False Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        vEnrollNumber = Val(txtEnrollNumber.Text)
        vFingerNumber = CInt(cmbBackupNumber.Text)
        vEMachineNumber = CInt(cmbEMachineNumber.Text)
        If vFingerNumber = 10 Then vFingerNumber = 15

        Dim gh As GCHandle = GCHandle.Alloc(gTemplngEnrollData, GCHandleType.Pinned)
        Dim AddrOfTemplngEnrollData As IntPtr = gh.AddrOfPinnedObject()

        vRet = sbxpc.SBXPCDLL.GetEnrollData1(mMachineNumber, _
                                                vEnrollNumber, _
                                                vFingerNumber, _
                                                vPrivilege, _
                                                AddrOfTemplngEnrollData.ToInt32(), _
                                                glngEnrollPData)

        gh.Free()


        If vRet = True Then
            cmbPrivilege.SelectedIndex = vPrivilege
            lblMessage.Text = "GetEnrollData OK"
            If vFingerNumber = 11 Then
                txtCardNumber.Text = Convert.ToString(glngEnrollPData, 16).ToUpper()
            ElseIf vFingerNumber = 14 Then
                txtUserTz1.Text = glngEnrollPData Mod 64 : glngEnrollPData = glngEnrollPData \ 64
                txtUserTz2.Text = glngEnrollPData Mod 64 : glngEnrollPData = glngEnrollPData \ 64
                txtUserTz3.Text = glngEnrollPData Mod 64 : glngEnrollPData = glngEnrollPData \ 64
                txtUserTz4.Text = glngEnrollPData Mod 64 : glngEnrollPData = glngEnrollPData \ 64
                txtUserTz5.Text = glngEnrollPData Mod 64 : glngEnrollPData = glngEnrollPData \ 64
            ElseIf vFingerNumber = 15 Then
                txtCardNumber.Text = ""
                While glngEnrollPData > 0
                    i = glngEnrollPData Mod 16 - 1
                    txtCardNumber.Text = txtCardNumber.Text & Convert.ToString(i)
                    glngEnrollPData = glngEnrollPData \ 16
                End While
            ElseIf vFingerNumber = 16 Then
                txtDepart.Text = Convert.ToString(glngEnrollPData)
            ElseIf vFingerNumber = 17 Then
                For i = 0 To DATASIZE_FACE - 1
                    lstEnrollData.Items.Add((CStr(gTemplngEnrollData(i))))
                Next
            Else
                For i = 0 To DATASIZE - 1
                    lstEnrollData.Items.Add((CStr(gTemplngEnrollData(i))))
                Next
            End If
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
    End Sub

    Private Sub cmdGetEnrollInfo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdGetEnrollInfo.Click
        Dim vEMachineNumber As Integer
        Dim vEnrollNumber As Integer
        Dim vFingerNumber As Integer
        Dim vPrivilege As Integer
        Dim vEnable As Integer
        Dim vRet As Integer
        Dim vFlag As Boolean
        Dim vErrorCode As Integer
        Dim i As Integer

        lblEnrollData.Text = "User IDs"
        lstEnrollData.Items.Clear()
        lblMessage.Text = "Working..."
        System.Windows.Forms.Application.DoEvents()

        vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
        If vRet = False Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        vRet = sbxpc.SBXPCDLL.ReadAllUserID(mMachineNumber)
        If vRet = True Then
            lblMessage.Text = "ReadAllUserID OK"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
            sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
            Exit Sub
        End If

        '------ Show all enroll information ----------
        vFlag = False
        i = 0
        lstEnrollData.Items.Add(("No.     EnNo    EMNo    Fp      Priv  Enable"))
        Do
            vRet = sbxpc.SBXPCDLL.GetAllUserID(mMachineNumber, vEnrollNumber, vEMachineNumber, vFingerNumber, vPrivilege, vEnable)
            If vRet <> True Then Exit Do
            vFlag = True
            lstEnrollData.Items.Add((VB6.Format(i, "00#") & "    " & VB6.Format(vEnrollNumber, "0000#") & "     " & VB6.Format(vEMachineNumber, "00#") & "       " & VB6.Format(vFingerNumber, "0#") & "       " & CStr(vPrivilege) & "        " & CStr(vEnable Mod 256)))

            i = i + 1
            Label2.Text = "Total : " & i
        Loop

        If vFlag = True Then
            lblMessage.Text = "GetAllUserID OK"
        Else
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
    End Sub

    Private Sub cmdGetName_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdGetName.Click
        Dim vEnrollNumber As Integer
        Dim vEMachineNumber As Integer
        Dim vRet As Boolean
        Dim vErrorCode As Integer
        Dim vName As String

        vName = "            "

        lblMessage.Text = "Working..."
        System.Windows.Forms.Application.DoEvents()

        vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
        If vRet = False Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        vEnrollNumber = Val(txtEnrollNumber.Text)
        vEMachineNumber = CInt(cmbEMachineNumber.Text)

        vRet = sbxpc.SBXPCDLL.GetUserName1(mMachineNumber, _
                                             vEnrollNumber, _
                                             vName)

        If vRet = True Then
            txtName.Text = vName    'vName 'SmackBio
            lblMessage.Text = "GetUserName OK"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
    End Sub

    Private Sub cmdModifyPrivilege_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModifyPrivilege.Click
        Dim vEnrollNumber As Integer
        Dim vFingerNumber As Integer
        Dim vEMachineNumber As Integer
        Dim vMachinePrivilege As Integer
        Dim vRet As Boolean
        Dim vErrorCode As Integer

        lblMessage.Text = "Working..."
        System.Windows.Forms.Application.DoEvents()

        vEnrollNumber = Val(txtEnrollNumber.Text)
        vEMachineNumber = cmbEMachineNumber.SelectedIndex + 1
        vFingerNumber = CInt(cmbBackupNumber.Text)
        vMachinePrivilege = cmbPrivilege.SelectedIndex

        vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
        If vRet = False Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        vRet = sbxpc.SBXPCDLL.ModifyPrivilege(mMachineNumber, vEnrollNumber, vEMachineNumber, vFingerNumber, vMachinePrivilege)
        If vRet = True Then
            lblMessage.Text = "Success!"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
    End Sub

    Private Sub cmdSendAllEnrollData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSendAllEnrollData.Click

    End Sub

    Private Sub cmdSetAllEnrollData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSetAllEnrollData.Click
        Dim vEnrollNumber As Integer
        Dim vEMachineNumber As Integer
        Dim vFingerNumber As Integer
        Dim vPrivilege As Integer
        Dim vFlag As Boolean
        Dim vRet As Integer
        Dim vErrorCode As Integer
        Dim vStr As String
        Dim vByte() As Byte
        Dim i As Integer
        Dim vTitle As String

        Dim dbEnrollTble As DataTable
        Dim dbRow As DataRow

        Dim gh As GCHandle

        Dim num As Integer


        lstEnrollData.Items.Clear()
        vTitle = Me.Text
        lblMessage.Text = "Working..."
        System.Windows.Forms.Application.DoEvents()

        vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
        If vRet = False Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        vFlag = False
        gGetState = True
        Cursor = System.Windows.Forms.Cursors.WaitCursor

        dbEnrollTble = dsEnrolls.Tables(0)
        num = 0

        If dbEnrollTble.Rows.Count = 0 Then GoTo EEE

        For Each dbRow In dbEnrollTble.Rows

            vEMachineNumber = dbRow("EMachineNumber")
            vEnrollNumber = dbRow("EnrollNumber")
            vFingerNumber = dbRow("FingerNumber")
            vPrivilege = dbRow("Privilige")
            glngEnrollPData = dbRow("Password1")

            num = num + 1

            If vFingerNumber = 17 Then
                vByte = dbRow("FPData")

                For i = 0 To DATASIZE_FACE - 1
                    gTemplngEnrollData(i) = vByte(i * 5 + 1)
                    gTemplngEnrollData(i) = gTemplngEnrollData(i) * 256 + vByte(i * 5 + 2)
                    gTemplngEnrollData(i) = gTemplngEnrollData(i) * 256 + vByte(i * 5 + 3)
                    gTemplngEnrollData(i) = gTemplngEnrollData(i) * 256 + vByte(i * 5 + 4)
                    If vByte(i * 5) = 0 Then
                        If gTemplngEnrollData(i) = 0 Then
                            gTemplngEnrollData(i) = -&H80000000L
                        Else
                            gTemplngEnrollData(i) = -gTemplngEnrollData(i)
                        End If
                    End If
                Next
            ElseIf vFingerNumber < 10 Then
                vByte = dbRow("FPData")

                For i = 0 To DATASIZE - 1
                    gTemplngEnrollData(i) = vByte(i * 5 + 1)
                    gTemplngEnrollData(i) = gTemplngEnrollData(i) * 256 + vByte(i * 5 + 2)
                    gTemplngEnrollData(i) = gTemplngEnrollData(i) * 256 + vByte(i * 5 + 3)
                    gTemplngEnrollData(i) = gTemplngEnrollData(i) * 256 + vByte(i * 5 + 4)
                    If vByte(i * 5) = 0 Then
                        If gTemplngEnrollData(i) = 0 Then
                            gTemplngEnrollData(i) = -&H80000000L
                        Else
                            gTemplngEnrollData(i) = -gTemplngEnrollData(i)
                        End If
                    End If
                Next
            End If
FFF:
            gh = GCHandle.Alloc(gTemplngEnrollData, GCHandleType.Pinned)
            Dim AddrOfTemplngEnrollData As IntPtr = gh.AddrOfPinnedObject()

            vRet = sbxpc.SBXPCDLL.SetEnrollData1(mMachineNumber, _
                                                    vEnrollNumber, _
                                                    vFingerNumber, _
                                                    vPrivilege, _
                                                    AddrOfTemplngEnrollData.ToInt32(), _
                                                    glngEnrollPData)

            If vRet <> True Then
                vFlag = False
                vStr = "SetEnrollData"
                sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
                vRet = MsgBox(ErrorPrint(vErrorCode) & ": Continue ?", MsgBoxStyle.YesNoCancel, "SetEnrollData")
                If vRet = MsgBoxResult.Yes Then GoTo FFF
                If vRet = MsgBoxResult.Cancel Then GoTo EEE
            End If

LLL:
            lblMessage.Text = "EMachine = " & VB6.Format(vEMachineNumber, "00#") & ", ID = " & VB6.Format(vEnrollNumber, "000#") & ", FpNo = " & vFingerNumber & ", Count = " & num

            Me.Text = CStr(num)
            System.Windows.Forms.Application.DoEvents()
        Next

EEE:
        vTitle = Me.Text
        Cursor = System.Windows.Forms.Cursors.Default
        gGetState = False

        lblMessage.Text = "SetAllUserData OK"
        System.Windows.Forms.Application.DoEvents()

        sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
    End Sub

    Private Sub cmdSetCompany_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSetCompany.Click
        Dim vEMachineNumber As Integer
        Dim vRet As Boolean
        Dim vErrorCode As Integer
        Dim vName As String

        lblMessage.Text = "Working..."
        System.Windows.Forms.Application.DoEvents()

        vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
        If vRet = False Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        vEMachineNumber = CInt(cmbEMachineNumber.Text)

        vName = txtName.Text 'SmackBio

        vRet = sbxpc.SBXPCDLL.SetCompanyName1(mMachineNumber, 1, vName)
        If vRet = True Then
            lblMessage.Text = "Set Company Name OK"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)

    End Sub

    Private Sub cmdSetEnrollData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSetEnrollData.Click
        Dim vEnrollNumber As Integer
        Dim vCardNumber As Integer
        Dim vEMachineNumber As Integer
        Dim vFingerNumber As Integer
        Dim vPrivilege As Integer
        Dim vRet As Boolean
        Dim vErrorCode As Integer

        lblMessage.Text = "Working..."
        System.Windows.Forms.Application.DoEvents()

        vEnrollNumber = Val(txtEnrollNumber.Text)
        vCardNumber = Convert.ToInt32(txtCardNumber.Text, 16)
        vFingerNumber = CInt(cmbBackupNumber.Text)
        vPrivilege = CInt(cmbPrivilege.Text)
        vEMachineNumber = CInt(cmbEMachineNumber.Text)

        If vFingerNumber = 10 Then vFingerNumber = 15
        ' Card Number valid
        If vFingerNumber = 11 Then
            If vCardNumber <> 0 Then
                glngEnrollPData = vCardNumber
            End If
        ElseIf vFingerNumber = 14 Then
            glngEnrollPData = Convert.ToInt32(txtUserTz5.Text)
            glngEnrollPData = glngEnrollPData * 64 + Convert.ToInt32(txtUserTz4.Text)
            glngEnrollPData = glngEnrollPData * 64 + Convert.ToInt32(txtUserTz3.Text)
            glngEnrollPData = glngEnrollPData * 64 + Convert.ToInt32(txtUserTz2.Text)
            glngEnrollPData = glngEnrollPData * 64 + Convert.ToInt32(txtUserTz1.Text)
        ElseIf vFingerNumber = 15 Then
            Dim i As Integer = txtCardNumber.Text.Length
            If i > 6 Then i = 6
            glngEnrollPData = 0
            While i > 0
                glngEnrollPData = glngEnrollPData * 16 + Convert.ToInt16(Mid(txtCardNumber.Text, i, 1)) + 1
                i = i - 1
            End While
        ElseIf vFingerNumber = 16 Then
            glngEnrollPData = Convert.ToInt32(txtDepart.Text)
        End If



        vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
        If vRet = False Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        Dim gh As GCHandle = GCHandle.Alloc(gTemplngEnrollData, GCHandleType.Pinned)
        Dim AddrOfTemplngEnrollData As IntPtr = gh.AddrOfPinnedObject()

        vRet = sbxpc.SBXPCDLL.SetEnrollData1(mMachineNumber, _
                                               vEnrollNumber, _
                                               vFingerNumber, _
                                               vPrivilege, _
                                               AddrOfTemplngEnrollData.ToInt32(), _
                                               glngEnrollPData)
        gh.Free()

        If vRet = True Then
            lblMessage.Text = "SetEnrollData OK"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
    End Sub

    Private Sub cmdSetName_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSetName.Click
        Dim vEnrollNumber As Integer
        Dim vEMachineNumber As Integer
        Dim vRet As Boolean
        Dim vErrorCode As Integer

        Dim vName As String

        lblMessage.Text = "Working..."
        System.Windows.Forms.Application.DoEvents()

        vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
        If vRet = False Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        vEnrollNumber = Val(txtEnrollNumber.Text)
        vEMachineNumber = CInt(cmbEMachineNumber.Text)

        'UPGRADE_WARNING: Couldn't resolve default property of object glngUserName. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        vName = txtName.Text 'SmackBio
        'vRet = FontForFK1.GetTextBitmap(mDeviceKind, txtName.Text, glngUserName) 'SmackBio
        vRet = sbxpc.SBXPCDLL.SetUserName1(mMachineNumber, vEnrollNumber, vName)
        If vRet = True Then
            lblMessage.Text = "SetUserName OK"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
    End Sub

    Private Sub frmEnroll_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        frmMain.Visible = True
        ClearUserPhoto()
    End Sub

    Private Sub frmEnroll_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim nLangId As Short
        nLangId = GetSystemDefaultLangID()
        If nLangId = &H41E Then
            txtName.Font = VB6.FontChangeName(txtName.Font, "Cordia New")
            txtName.Font = VB6.FontChangeGdiCharSet(txtName.Font, 222)
        End If
        cmbBackupNumber.SelectedIndex = 0
        cmbEMachineNumber.SelectedIndex = 0
        txtEnrollNumber.Text = CStr(1)
        txtCardNumber.Text = CStr(0)
        cmbPrivilege.Text = CStr(0)
        gGetState = False


        mMachineNumber = frmMain.gMachineNumber
        mConvKind = 0

        Dim sPath As String = Application.ExecutablePath
        sPath = System.IO.Path.GetDirectoryName(sPath)

        If sPath.EndsWith("\bin") Then
            sPath = sPath.Substring(0, Len(sPath) - 4)
        End If

        EnrollData.DataModule = New EnrollData(sPath)
        dsEnrolls = EnrollData.DataModule.GetEnrollDatas()

        Label2.Text = "Total : " & dsEnrolls.Tables(0).Rows.Count
    End Sub

    Private Sub cmdModifyDuressFP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdModifyDuressFP.Click
        Dim vEnrollNumber As Integer
        Dim vEMachineNumber As Integer
        Dim vFingerNumber As Integer
        Dim vRet As Boolean
        Dim vErrorCode As Integer

        Dim vDuressSetting As Integer

        lblMessage.Text = "Working..."
        System.Windows.Forms.Application.DoEvents()

        vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
        If vRet = False Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        vEnrollNumber = Val(txtEnrollNumber.Text)
        vEMachineNumber = CInt(cmbEMachineNumber.Text)
        vFingerNumber = Val(cmbBackupNumber.Text)
        vDuressSetting = cmbDuressSetting.SelectedIndex 'SmackBio
        vRet = sbxpc.SBXPCDLL.ModifyDuressFP(mMachineNumber, vEnrollNumber, vFingerNumber, vDuressSetting)

        If vRet = True Then
            lblMessage.Text = "Modify Duress FP OK"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
    End Sub

    Private Sub cmdGetUserPhoto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetUserPhoto.Click
        Dim vEnrollNumber As Integer
        Dim bRet As Boolean
        Dim strXML As String = ""
        Dim vErrorCode As Integer = 0

        txtUserPhotoFile.Text = ""

        lblMessage.Text = "Working..."
        Application.DoEvents()

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, 0)

        If Not bRet Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        vEnrollNumber = Val(txtEnrollNumber.Text)
        strXML = MakeXMLCommandHeader("GetUserPhotoData")
        sbxpc.SBXPCDLL.XML_AddLong(strXML, "UserID", vEnrollNumber)

        bRet = sbxpc.SBXPCDLL.GeneralOperationXML(frmMain.gMachineNumber, strXML)

        If Not bRet Then
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
            ClearUserPhoto()
            GoTo _lexit
        End If

        lblMessage.Text = "GetUserPhotoData OK"
        Dim photoData(gCompressPhotoSize - 1) As Byte
        Dim gh As GCHandle = GCHandle.Alloc(photoData, GCHandleType.Pinned)
        Dim AddrOfPhotoData As IntPtr = gh.AddrOfPinnedObject()

        bRet = sbxpc.SBXPCDLL.XML_ParseBinaryLong(strXML, "PhotoData", AddrOfPhotoData.ToInt32(), gCompressPhotoSize)

        ClearUserPhoto()

        If Not bRet Then
            lblMessage.Text = "GetGlogPhotoData - XML Parse Error"
            GoTo _lexit
        End If

        If File.Exists(gTempPhotoFile) Then
            File.Delete(gTempPhotoFile)
        End If
        Dim FS As FileStream = File.Create(gTempPhotoFile)
        FS.Write(photoData, 0, gCompressPhotoSize)
        FS.Close()
        FS.Dispose()
        FS = Nothing

        picUserPhoto.Image = Image.FromFile(gTempPhotoFile)
        txtUserPhotoFile.Text = gTempPhotoFile

_lexit:
        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, 1)
    End Sub

    Private Sub ClearUserPhoto()
        On Error Resume Next
        If Not picUserPhoto.Image Is Nothing Then
            picUserPhoto.Image.Dispose()
            picUserPhoto.Image = Nothing
        End If
    End Sub

    Private Sub cmdSetUserPhoto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetUserPhoto.Click
        Dim bRet As Boolean
        Dim vErrorCode As Integer = 0
        Dim strXML As String = ""
        Dim vEnrollNumber As Integer

        Dim photoFileName As String = txtUserPhotoFile.Text
        If Not File.Exists(photoFileName) Then
            lblMessage.Text = "Can not find the photo file."
            Exit Sub
        End If

        lblMessage.Text = "Working..."
        Application.DoEvents()

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, 0)

        If Not bRet Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        ClearUserPhoto()

        Dim FS As FileStream = File.Open(photoFileName, FileMode.Open, FileAccess.Read, FileShare.None)
        If FS.Length <> gCompressPhotoSize Then
            lblMessage.Text = "Photo file size is not" + Str(gCompressPhotoSize) + "Byte"
            Exit Sub
        End If
        Dim photoData(gCompressPhotoSize - 1) As Byte
        FS.Read(photoData, 0, gCompressPhotoSize)
        FS.Close()
        FS.Dispose()
        FS = Nothing

        picUserPhoto.Image = Image.FromFile(photoFileName)

        Dim gh As GCHandle = GCHandle.Alloc(photoData, GCHandleType.Pinned)
        Dim AddrOfPhotoData As IntPtr = gh.AddrOfPinnedObject()

        vEnrollNumber = Convert.ToInt32(txtEnrollNumber.Text)

        strXML = MakeXMLCommandHeader("SetUserPhotoData")
        sbxpc.SBXPCDLL.XML_AddLong(strXML, "UserID", vEnrollNumber)
        sbxpc.SBXPCDLL.XML_AddBinaryLong(strXML, "PhotoData", AddrOfPhotoData.ToInt32(), gCompressPhotoSize)

        bRet = sbxpc.SBXPCDLL.GeneralOperationXML(frmMain.gMachineNumber, strXML)

        If bRet Then
            lblMessage.Text = "SetUserPhotoData OK"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, 1)
    End Sub

    Private Sub cmdDeleteUserPhoto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteUserPhoto.Click
        Dim bRet As Boolean
        Dim vErrorCode As Integer = 0
        Dim strXML As String = ""
        Dim vEnrollNumber As Integer

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, 0)

        If Not bRet Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        vEnrollNumber = Convert.ToInt32(txtEnrollNumber.Text)

        strXML = MakeXMLCommandHeader("SetUserPhotoData")
        sbxpc.SBXPCDLL.XML_AddLong(strXML, "UserID", vEnrollNumber)
        ' Don't make "PhotoData" tag to delete user photo

        bRet = sbxpc.SBXPCDLL.GeneralOperationXML(frmMain.gMachineNumber, strXML)

        If bRet Then
            lblMessage.Text = "DeleteUserPhotoData OK"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, 1)
    End Sub

    Private Sub cmdUserPhotoBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUserPhotoBrowse.Click
        OpenFileDlg.ShowDialog()
        txtUserPhotoFile.Text = OpenFileDlg.FileName

        ClearUserPhoto()

        If Not File.Exists(txtUserPhotoFile.Text) Then
            Exit Sub
        End If

        picUserPhoto.Image = Image.FromFile(txtUserPhotoFile.Text)
    End Sub

    Private Sub cmdRemoteEnroll_Click(sender As Object, e As EventArgs) Handles cmdRemoteEnroll.Click
        lblMessage.Text = "Working..."
        Application.DoEvents()

        Dim vEnrollNumber As Int32 = Convert.ToInt32(txtEnrollNumber.Text)
        Dim vFingerNumber As Int32 = Convert.ToInt32(cmbBackupNumber.Text)

        Dim strXML As String = Nothing
        sbxpc.SBXPCDLL.XML_AddString(strXML, "REQUEST", "RemoteEnroll")
        sbxpc.SBXPCDLL.XML_AddString(strXML, "MSGTYPE", "request")
        sbxpc.SBXPCDLL.XML_AddLong(strXML, "MachineID", frmMain.gMachineNumber)
        sbxpc.SBXPCDLL.XML_AddLong(strXML, "EnrollNumber", vEnrollNumber)
        Dim bRet As Boolean = sbxpc.SBXPCDLL.GeneralOperationXML(frmMain.gMachineNumber, strXML)

        If bRet Then
            Dim strResultCode As String = Nothing
            sbxpc.SBXPCDLL.XML_ParseString(strXML, "ResultCode", strResultCode)
            If strResultCode = "MenuProcessing" Then
                lblMessage.Text = "Machine is now processing menu."
            ElseIf strResultCode = "EnrollNumberError" Then
                lblMessage.Text = "Invalid Enroll Number"
            ElseIf strResultCode = "AllEnrolled" Then
                lblMessage.Text = "All fingerprints are enrolled for this user"
            ElseIf strResultCode = "DatabaseFull" Then
                lblMessage.Text = "Fingerprint database is full."
            ElseIf strResultCode = "Success" Then
                lblMessage.Text = "Remote Enroll Started."
            Else
                lblMessage.Text = "Unknown Error"
            End If
        Else
            Dim vErrorCode As Int32
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If
    End Sub
End Class