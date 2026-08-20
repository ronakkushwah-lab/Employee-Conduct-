Option Strict Off
Option Explicit On

Imports System.IO
Imports System.Runtime.InteropServices

Friend Class frmLog
    Inherits System.Windows.Forms.Form

    Const gMaxLow As Short = 30000
    Dim mMachineNumber As Integer
    Dim gGlogSearched As Boolean = False
    Dim prevGlogIndex As Integer = -1
    Public gstrLogItem As Object

    Private Sub ShowGlogItem(ByVal vTMachineNumber As Integer, ByVal vSEnrollNumber As Integer, ByVal vSMachineNumber As Integer, ByVal vVerifyMode As Integer, ByVal vYear As Integer, ByVal vMonth As Integer, ByVal vDay As Integer, ByVal vHour As Integer, ByVal vMinute As Integer, ByVal vSecond As Integer, ByVal vIndex As Integer, ByVal vMaxLogCnt As Integer, ByVal gridGlogData As AxMSFlexGridLib.AxMSFlexGrid)
        Dim vAttStatus As Integer, vAntipass As Integer
        Dim stAttStatus As String = "", stAntipass As String = ""
        Dim vDiv As Integer = 65536

        vAntipass = vVerifyMode / vDiv
        vVerifyMode = vVerifyMode Mod vDiv
        vAttStatus = vVerifyMode / 256
        vVerifyMode = vVerifyMode Mod 256

        If vAttStatus = 0 Then
            stAttStatus = "_DutyOn"
        ElseIf vAttStatus = 1 Then
            stAttStatus = "_DutyOff"
        ElseIf vAttStatus = 2 Then
            stAttStatus = "_OverOn"
        ElseIf vAttStatus = 3 Then
            stAttStatus = "_OverOff"
        ElseIf vAttStatus = 4 Then
            stAttStatus = "_GoIn"
        ElseIf vAttStatus = 5 Then
            stAttStatus = "_GoOut"
        End If

        If vAntipass = 1 Then
            stAntipass = "(AP_In)"
        ElseIf vAntipass = 3 Then
            stAntipass = "(AP_Out)"
        End If

        With gridGlogData
            .Row = vIndex - vMaxLogCnt
            .Col = 0
            .Text = vIndex
            .Col = 1
            If vTMachineNumber = -1 Then
                .Text = "No Photo"
            Else
                .Text = CStr(vTMachineNumber)
            End If
            .Col = 2
            .Text = CStr(vSEnrollNumber)
            .Col = 3
            .Text = CStr(vSMachineNumber)
            .Col = 4
            vVerifyMode = vVerifyMode Mod 256
            If vVerifyMode = 1 Then
                .Text = "Fp"
            ElseIf vVerifyMode = 2 Then
                .Text = "Password"
            ElseIf vVerifyMode = 3 Then
                .Text = "Card"
            ElseIf vVerifyMode = 10 Then
                .Text = "Hand Lock"
            ElseIf vVerifyMode = 11 Then
                .Text = "Prog Lock"
            ElseIf vVerifyMode = 12 Then
                .Text = "Prog Open"
            ElseIf vVerifyMode = 13 Then
                .Text = "Prog Close"
            ElseIf vVerifyMode = 14 Then
                .Text = "Auto Recover"
            ElseIf vVerifyMode = 20 Then
                .Text = "Lock Over"
            ElseIf vVerifyMode = 21 Then
                .Text = "Illegal Open"
            ElseIf vVerifyMode = 22 Then
                .Text = "Duress alarm"
            ElseIf vVerifyMode = 23 Then
                .Text = "Tamper detect"
            ElseIf vVerifyMode = 30 Then
                .Text = "FACE"
            ElseIf vVerifyMode = 31 Then
                .Text = "FACE+CARD"
            ElseIf vVerifyMode = 32 Then
                .Text = "FACE+PWD"
            ElseIf vVerifyMode = 33 Then
                .Text = "FACE+CARD+PWD"
            ElseIf vVerifyMode = 34 Then
                .Text = "FACE+FP"
            ElseIf vVerifyMode = 51 Then
                .Text = "Fp"
            ElseIf vVerifyMode = 52 Then
                .Text = "Password"
            ElseIf vVerifyMode = 53 Then
                .Text = "Card"
            ElseIf vVerifyMode = 101 Then
                .Text = "Fp"
            ElseIf vVerifyMode = 102 Then
                .Text = "Password"
            ElseIf vVerifyMode = 103 Then
                .Text = "Card"
            ElseIf vVerifyMode = 151 Then
                .Text = "Fp"
            ElseIf vVerifyMode = 152 Then
                .Text = "Password"
            ElseIf vVerifyMode = 153 Then
                .Text = "Card"
            Else
                .Text = "--"
            End If

            If 1 <= vVerifyMode And vVerifyMode <= 7 Then
                .Text = .Text & stAttStatus
            ElseIf 31 <= vVerifyMode And vVerifyMode <= 34 Then
                .Text = .Text + stAttStatus
            ElseIf 51 <= vVerifyMode And vVerifyMode <= 53 Then
                .Text = .Text + stAttStatus
            ElseIf 101 <= vVerifyMode And vVerifyMode <= 103 Then
                .Text = .Text + stAttStatus
            ElseIf 151 <= vVerifyMode And vVerifyMode <= 153 Then
                .Text = .Text + stAttStatus
            End If

            gridGlogData.Text = gridGlogData.Text + stAntipass

            .Col = 5
            .Text = CStr(vYear) & "/" & VB6.Format(vMonth, "0#") & "/" & VB6.Format(vDay, "0#") & " " & VB6.Format(vHour, "0#") & ":" & VB6.Format(vMinute, "0#")
        End With
    End Sub

    Private Sub cmdAllGLogData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAllGLogData.Click
        gGlogSearched = True

        Dim vTMachineNumber As Integer
        Dim vSMachineNumber As Integer
        Dim vSEnrollNumber As Integer
        Dim vVerifyMode As Integer
        Dim vYear As Integer
        Dim vMonth As Integer
        Dim vDay As Integer
        Dim vHour As Integer
        Dim vMinute As Integer
        Dim vSecond As Integer
        Dim vErrorCode As Integer
        Dim vRet As Boolean
        Dim i As Integer
        Dim n As Integer
        Dim vMaxLogCnt As Integer

        vMaxLogCnt = gMaxLow

        lblMessage.Text = "Waiting..."
        LabelTotal.Text = "Total : "
        System.Windows.Forms.Application.DoEvents()

        gridSLogData.Height = VB6.TwipsToPixelsY(4800)
        gridSLogData.Redraw = False
        gridSLogData.Clear()
        gridSLogData1.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(gridSLogData.Top) + VB6.PixelsToTwipsY(gridSLogData.Height))
        gridSLogData1.Height = 0
        gridSLogData1.Redraw = False
        gridSLogData1.Clear()
        gridSLogData2.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(gridSLogData.Top) + VB6.PixelsToTwipsY(gridSLogData.Height))
        gridSLogData2.Height = 0
        gridSLogData2.Redraw = False
        gridSLogData2.Clear()

        gstrLogItem = New Object() {"", "Photo No", "EnrollNo", "EMachineNo", "VeriMode", "DateTime"}
        With gridSLogData
            .Row = 0
            .set_ColWidth(0, 600)
            For i = 1 To 5
                .Col = i
                .Text = gstrLogItem(i)
                .set_ColAlignment(i, 3)
                .set_ColWidth(i, 1200)
            Next i
            .set_ColWidth(5, 2000)
            .set_ColWidth(6, 700)
            .set_ColWidth(7, 700)
            .set_ColWidth(8, 700)
            n = .Rows
            If n > 2 Then
                Do
                    If n = 2 Then Exit Do
                    .RemoveItem((n))
                    n = n - 1
                Loop
            End If
            .Redraw = True
        End With
        With gridSLogData1
            .Row = 0
            .set_ColWidth(0, 600)
            For i = 1 To 5
                .Col = i
                .set_ColAlignment(i, 3)
                .set_ColWidth(i, 1200)
            Next i
            .Col = 5
            .set_ColWidth(5, 2000)
            .set_ColWidth(6, 700)
            .set_ColWidth(7, 700)
            .set_ColWidth(8, 700)
            n = .Rows
            If n > 2 Then
                Do
                    If n = 2 Then Exit Do
                    .RemoveItem((n))
                    n = n - 1
                Loop
            End If
            .Redraw = True
        End With
        With gridSLogData2
            .Row = 0
            .set_ColWidth(0, 600)
            For i = 1 To 5
                .Col = i
                .set_ColAlignment(i, 3)
                .set_ColWidth(i, 1200)
            Next i
            .Col = 5
            .set_ColWidth(5, 2000)
            .set_ColWidth(6, 700)
            .set_ColWidth(7, 700)
            .set_ColWidth(8, 700)
            n = .Rows
            If n > 2 Then
                Do
                    If n = 2 Then Exit Do
                    .RemoveItem((n))
                    n = n - 1
                Loop
            End If
            .Redraw = True
        End With

        Cursor = System.Windows.Forms.Cursors.WaitCursor
        vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
        If vRet = False Then
            lblMessage.Text = gstrNoDevice
            Cursor = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        vRet = sbxpc.SBXPCDLL.ReadAllGLogData(mMachineNumber)
        If vRet = False Then
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        Else
            If chkAndDelete.CheckState = 1 Then
                sbxpc.SBXPCDLL.EmptyGeneralLogData(mMachineNumber)
            End If
        End If

        If vRet = True Then
            lblMessage.Text = "Getting..."
            Cursor = System.Windows.Forms.Cursors.WaitCursor
            System.Windows.Forms.Application.DoEvents()
            gridSLogData.Redraw = False
            gridSLogData1.Redraw = False
            gridSLogData2.Redraw = False
            With gridSLogData
                i = 1
                Do

                    vRet = sbxpc.SBXPCDLL.GetAllGLogData(mMachineNumber, vTMachineNumber, vSEnrollNumber, vSMachineNumber, vVerifyMode, vYear, vMonth, vDay, vHour, vMinute, vSecond)
                    If vRet = False Then Exit Do
                    If vRet = True And i <> 1 Then
                        .AddItem(CStr(1))
                    End If

                    ShowGlogItem(vTMachineNumber, vSEnrollNumber, vSMachineNumber, vVerifyMode, vYear, vMonth, vDay, vHour, vMinute, vSecond, i, 0, gridSLogData)

                    LabelTotal.Text = "Total : " & i
                    System.Windows.Forms.Application.DoEvents()
                    i = i + 1
                    If i > vMaxLogCnt Then Exit Do
                Loop
            End With

            If i > vMaxLogCnt Then
                gridSLogData.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(gridSLogData.Height) / 2)
                gridSLogData1.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(gridSLogData.Top) + VB6.PixelsToTwipsY(gridSLogData.Height))
                gridSLogData1.Height = gridSLogData.Height
                With gridSLogData1
                    Do
                        vRet = sbxpc.SBXPCDLL.GetGeneralLogData(mMachineNumber, vTMachineNumber, vSEnrollNumber, vSMachineNumber, vVerifyMode, vYear, vMonth, vDay, vHour, vMinute, vSecond)
                        If vRet = False Then Exit Do
                        If vRet = True And i <> 1 Then
                            If i - vMaxLogCnt > 1 Then .AddItem(CStr(1))
                        End If

                        ShowGlogItem(vTMachineNumber, vSEnrollNumber, vSMachineNumber, vVerifyMode, vYear, vMonth, vDay, vHour, vMinute, vSecond, i, vMaxLogCnt, gridSLogData1)

                        LabelTotal.Text = "Total : " & i
                        System.Windows.Forms.Application.DoEvents()
                        i = i + 1
                        If i > vMaxLogCnt * 2 Then Exit Do
                    Loop
                End With
            End If
            vMaxLogCnt = vMaxLogCnt * 2
            If i > vMaxLogCnt Then
                gridSLogData.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(gridSLogData.Height) * 2 / 3)
                gridSLogData1.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(gridSLogData.Top) + VB6.PixelsToTwipsY(gridSLogData.Height))
                gridSLogData1.Height = gridSLogData.Height
                gridSLogData2.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(gridSLogData.Top) + VB6.PixelsToTwipsY(gridSLogData.Height) * 2)
                gridSLogData2.Height = gridSLogData.Height
                With gridSLogData2
                    Do
                        vRet = sbxpc.SBXPCDLL.GetGeneralLogData(mMachineNumber, vTMachineNumber, vSEnrollNumber, vSMachineNumber, vVerifyMode, vYear, vMonth, vDay, vHour, vMinute, vSecond)
                        If vRet = False Then Exit Do
                        If vRet = True And i <> 1 Then
                            If i - vMaxLogCnt > 1 Then .AddItem(CStr(1))
                        End If

                        ShowGlogItem(vTMachineNumber, vSEnrollNumber, vSMachineNumber, vVerifyMode, vYear, vMonth, vDay, vHour, vMinute, vSecond, i, vMaxLogCnt, gridSLogData2)

                        LabelTotal.Text = "Total : " & i
                        System.Windows.Forms.Application.DoEvents()
                        i = i + 1
                    Loop
                End With
            End If
            gridSLogData.Redraw = True
            gridSLogData1.Redraw = True
            gridSLogData2.Redraw = True

            lblMessage.Text = "ReadAllGLogData OK"
        End If

        sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
        Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdAllSLogData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAllSLogData.Click
        gGlogSearched = False

        Dim vTMachineNumber As Integer
        Dim vSMachineNumber As Integer
        Dim vSEnrollNumber As Integer
        Dim vGEnrollNumber As Integer
        Dim vGMachineNumber As Integer
        Dim vManipulation As Integer
        Dim vFingerNumber As Integer
        Dim vYear As Integer
        Dim vMonth As Integer
        Dim vDay As Integer
        Dim vHour As Integer
        Dim vMinute As Integer
        Dim vSecond As Integer
        Dim vRet As Boolean
        Dim vErrorCode As Integer
        Dim i As Integer
        Dim n As Integer

        gridSLogData.Height = VB6.TwipsToPixelsY(4800)
        gridSLogData.Redraw = False
        gridSLogData.Clear()
        gridSLogData1.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(gridSLogData.Top) + VB6.PixelsToTwipsY(gridSLogData.Height))
        gridSLogData1.Height = 0
        gridSLogData1.Redraw = False
        gridSLogData1.Clear()
        gridSLogData2.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(gridSLogData.Top) + VB6.PixelsToTwipsY(gridSLogData.Height))
        gridSLogData2.Height = 0
        gridSLogData2.Redraw = False
        gridSLogData2.Clear()

        lblMessage.Text = "Waiting..."
        LabelTotal.Text = "Total : "
        System.Windows.Forms.Application.DoEvents()
        gridSLogData.Redraw = False
        gridSLogData.Clear()
        gstrLogItem = New Object() {"", "TMNo", "SEnlNo", "SMNo", "GEnlNo", "GMNo", "Manipulation", "FpNo", "DateTime"}

        With gridSLogData
            .Row = 0
            .set_ColWidth(0, 600)
            For i = 1 To 8
                .Col = i
                .Text = gstrLogItem(i)
                .set_ColWidth(i, 900)
                .set_ColAlignment(i, 3)
            Next i
            .set_ColWidth(6, 2000)
            .set_ColAlignment(6, 2)
            .set_ColWidth(7, 800)
            .Col = 8
            .Text = gstrLogItem(8)
            .set_ColWidth(8, 2000)
            n = .Rows
            If n > 2 Then
                Do
                    If n = 2 Then Exit Do
                    .RemoveItem((n))
                    n = n - 1
                Loop
            End If
            .Redraw = True
        End With

        Cursor = System.Windows.Forms.Cursors.WaitCursor
        vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
        If vRet = False Then
            lblMessage.Text = gstrNoDevice
            Cursor = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        vRet = sbxpc.SBXPCDLL.ReadAllSLogData(mMachineNumber)
        If vRet = False Then
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        Else
            If chkAndDelete.CheckState = 1 Then
                sbxpc.SBXPCDLL.EmptySuperLogData(mMachineNumber)
            End If
        End If

        If vRet = True Then
            lblMessage.Text = "Getting..."
            Cursor = System.Windows.Forms.Cursors.WaitCursor
            System.Windows.Forms.Application.DoEvents()
            With gridSLogData
                .Redraw = False
                i = 1
                Do
                    vRet = sbxpc.SBXPCDLL.GetAllSLogData(mMachineNumber, vTMachineNumber, vSEnrollNumber, vSMachineNumber, vGEnrollNumber, vGMachineNumber, vManipulation, vFingerNumber, vYear, vMonth, vDay, vHour, vMinute, vSecond)
                    If vRet = False Then Exit Do
                    If vRet = True And i <> 1 Then
                        .AddItem(CStr(1))
                    End If

                    .Row = i
                    .Col = 0
                    .Text = i
                    .Col = 1
                    .Text = CStr(vTMachineNumber)
                    .Col = 2
                    .Text = CStr(vSEnrollNumber)
                    .Col = 3
                    .Text = CStr(vSMachineNumber)
                    .Col = 4
                    .Text = CStr(vGEnrollNumber)
                    .Col = 5
                    .Text = CStr(vGMachineNumber)
                    .Col = 6
                    Select Case vManipulation
                        Case 1
                        Case 2
                        Case 3
                            .Text = vManipulation & "--" & "Enroll User"
                        Case 4
                            .Text = vManipulation & "--" & "Enroll Manager"
                        Case 5
                            .Text = vManipulation & "--" & "Delete Fp Data"
                        Case 6
                            .Text = vManipulation & "--" & "Delete Password"
                        Case 7
                            .Text = vManipulation & "--" & "Delete Card Data"
                        Case 8
                            .Text = vManipulation & "--" & "Delete All LogData"
                        Case 9
                            .Text = vManipulation & "--" & "Modify System Info"
                        Case 10
                            .Text = vManipulation & "--" & "Modify System Time"
                        Case 11
                            .Text = vManipulation & "--" & "Modify Log Setting"
                        Case 12
                            .Text = vManipulation & "--" & "Modify Comm Setting"
                        Case 13
                            .Text = vManipulation & "--" & "Modify Timezone Setting"
                        Case 14
                            .Text = vManipulation & "--" & "Delete Face"
                    End Select

                    .Col = 7
                    If vFingerNumber < 10 Then
                        .Text = CStr(vFingerNumber)
                    ElseIf vFingerNumber = 10 Then
                        .Text = "Password"
                    ElseIf vFingerNumber = 14 Then
                        .Text = "Face"
                    Else
                        .Text = "Card"
                    End If
                    .Col = 8
                    .Text = CStr(vYear) & "/" & VB6.Format(vMonth, "0#") & "/" & VB6.Format(vDay, "0#") & " " & VB6.Format(vHour, "0#") & ":" & VB6.Format(vMinute, "0#")

                    LabelTotal.Text = "Total : " & i
                    System.Windows.Forms.Application.DoEvents()
                    i = i + 1
                Loop
                .Redraw = True
            End With
            lblMessage.Text = "ReadAllSLogData OK"
        End If

        Cursor = System.Windows.Forms.Cursors.Default
        sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
    End Sub

    Private Sub cmdEmptyGLog_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEmptyGLog.Click
        Dim vRet As Boolean
        Dim vErrorCode As Integer

        lblMessage.Text = "Working..."
        System.Windows.Forms.Application.DoEvents()

        vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
        If vRet = False Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        vRet = sbxpc.SBXPCDLL.EmptyGeneralLogData(mMachineNumber)
        If vRet = True Then
            lblMessage.Text = "EmptyGeneralLogData OK"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
    End Sub

    Private Sub cmdEmptySLog_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEmptySLog.Click
        Dim vRet As Boolean
        Dim vErrorCode As Integer

        lblMessage.Text = "Working..."
        System.Windows.Forms.Application.DoEvents()

        vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
        If vRet = False Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        vRet = sbxpc.SBXPCDLL.EmptySuperLogData(mMachineNumber)
        If vRet = True Then
            lblMessage.Text = "EmptySuperLogData OK"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
    End Sub

    Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdGlogData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdGlogData.Click
        gGlogSearched = True

        Dim vTMachineNumber As Integer
        Dim vSMachineNumber As Integer
        Dim vSEnrollNumber As Integer
        '		Dim vInOutMode As Integer
        Dim vVerifyMode As Integer
        Dim vYear As Integer
        Dim vMonth As Integer
        Dim vDay As Integer
        Dim vHour As Integer
        Dim vMinute As Integer
        Dim vSecond As Integer
        Dim vRet As Boolean
        Dim vErrorCode As Integer
        Dim i As Integer
        Dim n As Integer
        Dim vMaxLogCnt As Integer

        vMaxLogCnt = gMaxLow

        lblMessage.Text = "Waiting..."
        LabelTotal.Text = "Total : "
        System.Windows.Forms.Application.DoEvents()

        gridSLogData.Height = VB6.TwipsToPixelsY(4800)
        gridSLogData.Redraw = False
        gridSLogData.Clear()
        gridSLogData1.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(gridSLogData.Top) + VB6.PixelsToTwipsY(gridSLogData.Height))
        gridSLogData1.Height = 0
        gridSLogData1.Redraw = False
        gridSLogData1.Clear()
        gridSLogData2.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(gridSLogData.Top) + VB6.PixelsToTwipsY(gridSLogData.Height))
        gridSLogData2.Height = 0
        gridSLogData2.Redraw = False
        gridSLogData2.Clear()

        gstrLogItem = New Object() {"", "Photo No", "EnrollNo", "EMachineNo", "VeriMode", "DateTime"}
        With gridSLogData
            .Row = 0
            .set_ColWidth(0, 600)
            For i = 1 To 5
                .Col = i
                .Text = gstrLogItem(i)
                .set_ColAlignment(i, 3)
                .set_ColWidth(i, 1200)
            Next i
            .Col = 5
            .set_ColWidth(5, 2000)
            .set_ColWidth(6, 700)
            .set_ColWidth(7, 700)
            .set_ColWidth(8, 700)
            n = .Rows
            If n > 2 Then
                Do
                    If n = 2 Then Exit Do
                    .RemoveItem((n))
                    n = n - 1
                Loop
            End If
            .Redraw = True
        End With
        With gridSLogData1
            .Row = 0
            .set_ColWidth(0, 600)
            For i = 1 To 5
                .Col = i
                .set_ColAlignment(i, 3)
                .set_ColWidth(i, 1200)
            Next i
            .Col = 5
            .set_ColWidth(5, 2000)
            .set_ColWidth(6, 700)
            .set_ColWidth(7, 700)
            .set_ColWidth(8, 700)
            n = .Rows
            If n > 2 Then
                Do
                    If n = 2 Then Exit Do
                    .RemoveItem((n))
                    n = n - 1
                Loop
            End If
            .Redraw = True
        End With
        With gridSLogData2
            .Row = 0
            .set_ColWidth(0, 600)
            For i = 1 To 5
                .Col = i
                .set_ColAlignment(i, 3)
                .set_ColWidth(i, 1200)
            Next i
            .Col = 5
            .set_ColWidth(5, 2000)
            .set_ColWidth(6, 700)
            .set_ColWidth(7, 700)
            .set_ColWidth(8, 700)
            n = .Rows
            If n > 2 Then
                Do
                    If n = 2 Then Exit Do
                    .RemoveItem((n))
                    n = n - 1
                Loop
            End If
            .Redraw = True
        End With

        Cursor = System.Windows.Forms.Cursors.WaitCursor
        vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
        If vRet = False Then
            lblMessage.Text = gstrNoDevice
            Cursor = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        vRet = sbxpc.SBXPCDLL.ReadGeneralLogData(mMachineNumber, chkReadMark.Checked)
        If vRet = False Then
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        Else
            If chkAndDelete.CheckState = 1 Then
                sbxpc.SBXPCDLL.EmptyGeneralLogData(mMachineNumber)
            End If
        End If

        If vRet = True Then
            Cursor = System.Windows.Forms.Cursors.WaitCursor
            lblMessage.Text = "Getting ..."
            System.Windows.Forms.Application.DoEvents()
            gridSLogData.Redraw = False
            gridSLogData1.Redraw = False
            gridSLogData2.Redraw = False
            With gridSLogData
                i = 1

                Do
                    vRet = sbxpc.SBXPCDLL.GetGeneralLogData(mMachineNumber, vTMachineNumber, vSEnrollNumber, vSMachineNumber, vVerifyMode, vYear, vMonth, vDay, vHour, vMinute, vSecond)
                    If vRet = False Then Exit Do
                    If vRet = True And i <> 1 Then
                        .AddItem(CStr(1))
                    End If
                    .Row = i
                    .Col = 0
                    .Text = i
                    .Col = 1
                    If vTMachineNumber = -1 Then
                        .Text = "No Photo"
                    Else
                        .Text = CStr(vTMachineNumber)
                    End If
                    .Col = 2
                    .Text = CStr(vSEnrollNumber)
                    .Col = 3
                    .Text = CStr(vSMachineNumber)
                    .Col = 4
                    vVerifyMode = vVerifyMode Mod 256
                    If vVerifyMode = 1 Then
                        .Text = "Fp"
                    ElseIf vVerifyMode = 2 Then
                        .Text = "Password"
                    ElseIf vVerifyMode = 3 Then
                        .Text = "Card"
                    ElseIf vVerifyMode = 10 Then
                        .Text = "Hand Lock"
                    ElseIf vVerifyMode = 11 Then
                        .Text = "Prog Lock"
                    ElseIf vVerifyMode = 12 Then
                        .Text = "Prog Open"
                    ElseIf vVerifyMode = 13 Then
                        .Text = "Prog Close"
                    ElseIf vVerifyMode = 14 Then
                        .Text = "Auto Recover"
                    ElseIf vVerifyMode = 20 Then
                        .Text = "Lock Over"
                    ElseIf vVerifyMode = 21 Then
                        .Text = "Illegal Open"
                    ElseIf vVerifyMode = 22 Then
                        .Text = "Duress alarm"
                    ElseIf vVerifyMode = 23 Then
                        .Text = "Tamper detect"
                    ElseIf vVerifyMode = 30 Then
                        .Text = "FACE"
                    ElseIf vVerifyMode = 31 Then
                        .Text = "FACE+CARD"
                    ElseIf vVerifyMode = 32 Then
                        .Text = "FACE+PWD"
                    ElseIf vVerifyMode = 33 Then
                        .Text = "FACE+CARD+PWD"
                    ElseIf vVerifyMode = 34 Then
                        .Text = "FACE+FP"
                    ElseIf vVerifyMode = 51 Then
                        .Text = "Fp_IN"
                    ElseIf vVerifyMode = 52 Then
                        .Text = "Password_IN"
                    ElseIf vVerifyMode = 53 Then
                        .Text = "Card_IN"
                    ElseIf vVerifyMode = 101 Then
                        .Text = "Fp_OUT"
                    ElseIf vVerifyMode = 102 Then
                        .Text = "Password_OUT"
                    ElseIf vVerifyMode = 103 Then
                        .Text = "Card_OUT"
                    ElseIf vVerifyMode = 151 Then
                        .Text = "Fp_OT"
                    ElseIf vVerifyMode = 152 Then
                        .Text = "Password_OT"
                    ElseIf vVerifyMode = 153 Then
                        .Text = "Card_OT"
                    Else
                        .Text = "--"
                    End If
                    .Col = 5
                    .Text = CStr(vYear) & "/" & VB6.Format(vMonth, "0#") & "/" & VB6.Format(vDay, "0#") & " " & VB6.Format(vHour, "0#") & ":" & VB6.Format(vMinute, "0#")

                    LabelTotal.Text = "Total : " & i
                    System.Windows.Forms.Application.DoEvents()
                    i = i + 1
                    If i > vMaxLogCnt Then Exit Do
                Loop
            End With

            If i > vMaxLogCnt Then
                gridSLogData.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(gridSLogData.Height) / 2)
                gridSLogData1.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(gridSLogData.Top) + VB6.PixelsToTwipsY(gridSLogData.Height))
                gridSLogData1.Height = gridSLogData.Height
                With gridSLogData1
                    Do
                        vRet = sbxpc.SBXPCDLL.GetGeneralLogData(mMachineNumber, vTMachineNumber, vSEnrollNumber, vSMachineNumber, vVerifyMode, vYear, vMonth, vDay, vHour, vMinute, vSecond)
                        If vRet = False Then Exit Do
                        If vRet = True And i <> 1 Then
                            If i - vMaxLogCnt > 1 Then .AddItem(CStr(1))
                        End If
                        .Row = i - vMaxLogCnt
                        .Col = 0
                        .Text = i
                        .Col = 1
                        .Text = CStr(vTMachineNumber)
                        .Col = 2
                        .Text = CStr(vSEnrollNumber)
                        .Col = 3
                        .Text = CStr(vSMachineNumber)
                        .Col = 4
                        vVerifyMode = vVerifyMode Mod 256
                        If vVerifyMode = 1 Then
                            .Text = "Fp"
                        ElseIf vVerifyMode = 2 Then
                            .Text = "Password"
                        ElseIf vVerifyMode = 3 Then
                            .Text = "Card"
                        ElseIf vVerifyMode = 10 Then
                            .Text = "Hand Lock"
                        ElseIf vVerifyMode = 11 Then
                            .Text = "Prog Lock"
                        ElseIf vVerifyMode = 12 Then
                            .Text = "Prog Open"
                        ElseIf vVerifyMode = 13 Then
                            .Text = "Prog Close"
                        ElseIf vVerifyMode = 14 Then
                            .Text = "Auto Recover"
                        ElseIf vVerifyMode = 20 Then
                            .Text = "Lock Over"
                        ElseIf vVerifyMode = 21 Then
                            .Text = "Illegal Open"
                        ElseIf vVerifyMode = 22 Then
                            .Text = "Duress alarm"
                        ElseIf vVerifyMode = 23 Then
                            .Text = "Tamper detect"
                        ElseIf vVerifyMode = 30 Then
                            .Text = "FACE"
                        ElseIf vVerifyMode = 31 Then
                            .Text = "FACE+CARD"
                        ElseIf vVerifyMode = 32 Then
                            .Text = "FACE+PWD"
                        ElseIf vVerifyMode = 33 Then
                            .Text = "FACE+CARD+PWD"
                        ElseIf vVerifyMode = 34 Then
                            .Text = "FACE+FP"
                        ElseIf vVerifyMode = 51 Then
                            .Text = "Fp_IN"
                        ElseIf vVerifyMode = 52 Then
                            .Text = "Password_IN"
                        ElseIf vVerifyMode = 53 Then
                            .Text = "Card_IN"
                        ElseIf vVerifyMode = 101 Then
                            .Text = "Fp_OUT"
                        ElseIf vVerifyMode = 102 Then
                            .Text = "Password_OUT"
                        ElseIf vVerifyMode = 103 Then
                            .Text = "Card_OUT"
                        ElseIf vVerifyMode = 151 Then
                            .Text = "Fp_OT"
                        ElseIf vVerifyMode = 152 Then
                            .Text = "Password_OT"
                        ElseIf vVerifyMode = 153 Then
                            .Text = "Card_OT"
                        Else
                            .Text = "--"
                        End If
                        .Col = 5
                        .Text = CStr(vYear) & "/" & VB6.Format(vMonth, "0#") & "/" & VB6.Format(vDay, "0#") & " " & VB6.Format(vHour, "0#") & ":" & VB6.Format(vMinute, "0#")

                        LabelTotal.Text = "Total : " & i
                        System.Windows.Forms.Application.DoEvents()
                        i = i + 1
                        If i > vMaxLogCnt * 2 Then Exit Do
                    Loop
                End With
            End If
            vMaxLogCnt = vMaxLogCnt * 2
            If i > vMaxLogCnt Then
                gridSLogData.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(gridSLogData.Height) * 2 / 3)
                gridSLogData1.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(gridSLogData.Top) + VB6.PixelsToTwipsY(gridSLogData.Height))
                gridSLogData1.Height = gridSLogData.Height
                gridSLogData2.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(gridSLogData.Top) + VB6.PixelsToTwipsY(gridSLogData.Height) * 2)
                gridSLogData2.Height = gridSLogData.Height
                With gridSLogData2
                    Do
                        vRet = sbxpc.SBXPCDLL.GetGeneralLogData(mMachineNumber, vTMachineNumber, vSEnrollNumber, vSMachineNumber, vVerifyMode, vYear, vMonth, vDay, vHour, vMinute, vSecond)
                        If vRet = False Then Exit Do
                        If vRet = True And i <> 1 Then
                            If i - vMaxLogCnt > 1 Then .AddItem(CStr(1))
                        End If
                        .Row = i - vMaxLogCnt
                        .Col = 0
                        .Text = i
                        .Col = 1
                        .Text = CStr(vTMachineNumber)
                        .Col = 2
                        .Text = CStr(vSEnrollNumber)
                        .Col = 3
                        .Text = CStr(vSMachineNumber)
                        .Col = 4
                        vVerifyMode = vVerifyMode Mod 256
                        If vVerifyMode = 1 Then
                            .Text = "Fp"
                        ElseIf vVerifyMode = 2 Then
                            .Text = "Password"
                        ElseIf vVerifyMode = 3 Then
                            .Text = "Card"
                        ElseIf vVerifyMode = 10 Then
                            .Text = "Hand Lock"
                        ElseIf vVerifyMode = 11 Then
                            .Text = "Prog Lock"
                        ElseIf vVerifyMode = 12 Then
                            .Text = "Prog Open"
                        ElseIf vVerifyMode = 13 Then
                            .Text = "Prog Close"
                        ElseIf vVerifyMode = 14 Then
                            .Text = "Auto Recover"
                        ElseIf vVerifyMode = 20 Then
                            .Text = "Lock Over"
                        ElseIf vVerifyMode = 21 Then
                            .Text = "Illegal Open"
                        ElseIf vVerifyMode = 22 Then
                            .Text = "Duress alarm"
                        ElseIf vVerifyMode = 23 Then
                            .Text = "Tamper detect"
                        ElseIf vVerifyMode = 30 Then
                            .Text = "FACE"
                        ElseIf vVerifyMode = 31 Then
                            .Text = "FACE+CARD"
                        ElseIf vVerifyMode = 32 Then
                            .Text = "FACE+PWD"
                        ElseIf vVerifyMode = 33 Then
                            .Text = "FACE+CARD+PWD"
                        ElseIf vVerifyMode = 34 Then
                            .Text = "FACE+FP"
                        ElseIf vVerifyMode = 51 Then
                            .Text = "Fp_IN"
                        ElseIf vVerifyMode = 52 Then
                            .Text = "Password_IN"
                        ElseIf vVerifyMode = 53 Then
                            .Text = "Card_IN"
                        ElseIf vVerifyMode = 101 Then
                            .Text = "Fp_OUT"
                        ElseIf vVerifyMode = 102 Then
                            .Text = "Password_OUT"
                        ElseIf vVerifyMode = 103 Then
                            .Text = "Card_OUT"
                        ElseIf vVerifyMode = 151 Then
                            .Text = "Fp_OT"
                        ElseIf vVerifyMode = 152 Then
                            .Text = "Password_OT"
                        ElseIf vVerifyMode = 153 Then
                            .Text = "Card_OT"
                        Else
                            .Text = "--"
                        End If
                        .Col = 5
                        .Text = CStr(vYear) & "/" & VB6.Format(vMonth, "0#") & "/" & VB6.Format(vDay, "0#") & " " & VB6.Format(vHour, "0#") & ":" & VB6.Format(vMinute, "0#")

                        LabelTotal.Text = "Total : " & i
                        System.Windows.Forms.Application.DoEvents()
                        i = i + 1
                    Loop
                End With
            End If
            gridSLogData.Redraw = True
            gridSLogData1.Redraw = True
            gridSLogData2.Redraw = True

            lblMessage.Text = "ReadGeneralLogData OK"
        End If

        Cursor = System.Windows.Forms.Cursors.Default
        sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
    End Sub

    Private Sub cmdSLogData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSLogData.Click
        gGlogSearched = False

        Dim vTMachineNumber As Integer
        Dim vSMachineNumber As Integer
        Dim vSEnrollNumber As Integer
        Dim vGEnrollNumber As Integer
        Dim vGMachineNumber As Integer
        Dim vManipulation As Integer
        Dim vFingerNumber As Integer
        Dim vYear As Integer
        Dim vMonth As Integer
        Dim vDay As Integer
        Dim vHour As Integer
        Dim vMinute As Integer
        Dim vSecond As Integer
        Dim vRet As Boolean
        Dim vErrorCode As Integer
        Dim i As Integer
        Dim n As Integer

        gridSLogData.Height = VB6.TwipsToPixelsY(4800)
        gridSLogData.Redraw = False
        gridSLogData.Clear()
        gridSLogData1.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(gridSLogData.Top) + VB6.PixelsToTwipsY(gridSLogData.Height))
        gridSLogData1.Height = 0
        gridSLogData1.Redraw = False
        gridSLogData1.Clear()
        gridSLogData2.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(gridSLogData.Top) + VB6.PixelsToTwipsY(gridSLogData.Height))
        gridSLogData2.Height = 0
        gridSLogData2.Redraw = False
        gridSLogData2.Clear()

        lblMessage.Text = "Waiting..."
        LabelTotal.Text = "Total : "
        System.Windows.Forms.Application.DoEvents()

        gridSLogData.Redraw = False
        gridSLogData.Clear()

        gstrLogItem = New Object() {"", "TMNo", "SEnlNo", "SMNo", "GEnlNo", "GMNo", "Manipulation", "FpNo", "DateTime"}
        With gridSLogData
            .Row = 0
            .set_ColWidth(0, 600)
            For i = 1 To 8
                .Col = i
                .Text = gstrLogItem(i)
                .set_ColAlignment(i, 3)
                .set_ColWidth(i, 900)
            Next i
            .Col = 6
            .set_ColWidth(6, 2000)
            .set_ColAlignment(6, 2)
            .set_ColWidth(7, 800)
            .Col = 8
            .Text = gstrLogItem(8)
            .set_ColWidth(8, 2000)
            n = .Rows
            If n > 2 Then
                Do
                    If n = 2 Then Exit Do
                    .RemoveItem((n))
                    n = n - 1
                Loop
            End If
            .Redraw = True
        End With

        Cursor = System.Windows.Forms.Cursors.WaitCursor
        vRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)
        If vRet = False Then
            lblMessage.Text = gstrNoDevice
            Cursor = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        vRet = sbxpc.SBXPCDLL.ReadSuperLogData(mMachineNumber, chkReadMark.Checked)
        If vRet = False Then
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        Else
            If chkAndDelete.CheckState = 1 Then
                sbxpc.SBXPCDLL.EmptySuperLogData(mMachineNumber)
            End If
        End If

        If vRet = True Then
            Cursor = System.Windows.Forms.Cursors.WaitCursor
            lblMessage.Text = "Getting ..."
            System.Windows.Forms.Application.DoEvents()
            With gridSLogData
                .Redraw = False
                i = 1
                Do
                    vRet = sbxpc.SBXPCDLL.GetSuperLogData(mMachineNumber, vTMachineNumber, vSEnrollNumber, vSMachineNumber, vGEnrollNumber, vGMachineNumber, vManipulation, vFingerNumber, vYear, vMonth, vDay, vHour, vMinute, vSecond)
                    If vRet = False Then Exit Do
                    If vRet = True And i <> 1 Then
                        .AddItem(CStr(1))
                    End If

                    .Row = i
                    .Col = 0
                    .Text = i
                    .Col = 1
                    .Text = CStr(vTMachineNumber)
                    .Col = 2
                    .Text = CStr(vSEnrollNumber)
                    .Col = 3
                    .Text = CStr(vSMachineNumber)
                    .Col = 4
                    .Text = CStr(vGEnrollNumber)
                    .Col = 5
                    .Text = CStr(vGMachineNumber)
                    .Col = 6
                    Select Case vManipulation
                        Case 1
                        Case 2
                        Case 3
                            .Text = vManipulation & "--" & "Enroll User"
                        Case 4
                            .Text = vManipulation & "--" & "Enroll Manager"
                        Case 5
                            .Text = vManipulation & "--" & "Delete Fp Data"
                        Case 6
                            .Text = vManipulation & "--" & "Delete Password"
                        Case 7
                            .Text = vManipulation & "--" & "Delete All LogData"
                        Case 7
                            .Text = vManipulation & "--" & "Delete Card Data"
                        Case 9
                            .Text = vManipulation & "--" & "Modify System Info"
                        Case 10
                            .Text = vManipulation & "--" & "Modify System Time"
                        Case 11
                            .Text = vManipulation & "--" & "Modify Log Setting"
                        Case 12
                            .Text = vManipulation & "--" & "Modify Comm Setting"
                        Case 13
                            .Text = vManipulation & "--" & "Modify Timezone Setting"
                        Case 14
                            .Text = vManipulation & "--" & "Delete Face"
                    End Select
                    .Col = 7
                    If vFingerNumber < 10 Then
                        .Text = CStr(vFingerNumber)
                    ElseIf vFingerNumber = 10 Then
                        .Text = "Password"
                    ElseIf vFingerNumber = 14 Then
                        .Text = "Face"
                    Else
                        .Text = "Card"
                    End If
                    .Col = 8
                    .Text = CStr(vYear) & "/" & VB6.Format(vMonth, "0#") & "/" & VB6.Format(vDay, "0#") & " " & VB6.Format(vHour, "0#") & ":" & VB6.Format(vMinute, "0#")

                    LabelTotal.Text = "Total : " & i
                    System.Windows.Forms.Application.DoEvents()
                    i = i + 1
                Loop
                .Redraw = True
            End With
            lblMessage.Text = "ReadSuperLogData OK"
        End If

        Cursor = System.Windows.Forms.Cursors.Default
        sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
    End Sub

    Private Sub frmLog_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        mMachineNumber = frmMain.gMachineNumber
        chkReadMark.CheckState = System.Windows.Forms.CheckState.Checked
    End Sub

    Private Sub frmLog_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        '	Me.Visible = False
        frmMain.Visible = True
        ClearPhoto()
    End Sub

    Private Sub cmdSetRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetRange.Click
        Dim bRet As Boolean
        Dim vErrorCode As Integer
        Dim strXML As String

        lblMessage.Text = "Working..."
        Application.DoEvents()

        bRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)

        If Not bRet Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        strXML = MakeXMLCommandHeader("SetGLogSearchRange")
        sbxpc.SBXPCDLL.XML_AddBoolean(strXML, "UseSearchRange", chkUseSearchRange.Checked)

        If chkUseSearchRange.Checked Then
            sbxpc.SBXPCDLL.XML_AddLong(strXML, "StartYear", dtStart.Value.Year)
            sbxpc.SBXPCDLL.XML_AddLong(strXML, "StartMonth", dtStart.Value.Month)
            sbxpc.SBXPCDLL.XML_AddLong(strXML, "StartDate", dtStart.Value.Day)
            sbxpc.SBXPCDLL.XML_AddLong(strXML, "EndYear", dtEnd.Value.Year)
            sbxpc.SBXPCDLL.XML_AddLong(strXML, "EndMonth", dtEnd.Value.Month)
            sbxpc.SBXPCDLL.XML_AddLong(strXML, "EndDate", dtEnd.Value.Day)
        End If

        bRet = sbxpc.SBXPCDLL.GeneralOperationXML(frmMain.gMachineNumber, strXML)

        If bRet Then
            lblMessage.Text = "SetGLogSearchRange OK"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        bRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
    End Sub

    Private Sub ClearPhoto()
        On Error Resume Next
        picGLogPhoto.Image.Dispose()
        picGLogPhoto.Image = Nothing
        picGLogPhoto.ImageLocation = ""
    End Sub

    Private Sub gridSLogData_ClickEvent(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gridSLogData.ClickEvent
        If Not chkShowGLogPhoto.Checked Then Exit Sub
        If Not gGlogSearched Then Exit Sub
        If prevGlogIndex = gridSLogData.Row Then Exit Sub
        prevGlogIndex = gridSLogData.Row
        ClearPhoto()
        If gridSLogData.get_TextMatrix(gridSLogData.Row, 1) = "No Photo" Then
            Exit Sub
        End If

        Dim bRet As Boolean
        Dim vErrorCode As Integer
        Dim strXML As String

        lblMessage.Text = "Working..."
        Application.DoEvents()

        bRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False)

        If Not bRet Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        strXML = MakeXMLCommandHeader("GetGLogPhotoData")
        sbxpc.SBXPCDLL.XML_AddLong(strXML, "PhotoPos", Val(gridSLogData.get_TextMatrix(gridSLogData.Row, 1)))

        bRet = sbxpc.SBXPCDLL.GeneralOperationXML(frmMain.gMachineNumber, strXML)

        If Not bRet Then
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
            GoTo _lExit
        End If

        lblMessage.Text = "SetGLogSearchRange OK"

        Dim photoData(gCompressPhotoSize - 1) As Byte
        Dim gh As GCHandle = GCHandle.Alloc(photoData, GCHandleType.Pinned)
        Dim AddrOfphotoData As IntPtr = gh.AddrOfPinnedObject()
        sbxpc.SBXPCDLL.XML_ParseBinaryLong(strXML, "PhotoData", AddrOfphotoData.ToInt32, gCompressPhotoSize)

        Dim FS As IO.FileStream = IO.File.Open(gTempPhotoFile, IO.FileMode.Open, IO.FileAccess.Write, IO.FileShare.None)
        FS.Write(photoData, 0, photoData.Length)
        FS.Close()
        FS.Dispose()
        FS = Nothing

        picGLogPhoto.Image = Image.FromFile(gTempPhotoFile)

_lExit:
        bRet = sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
    End Sub
End Class