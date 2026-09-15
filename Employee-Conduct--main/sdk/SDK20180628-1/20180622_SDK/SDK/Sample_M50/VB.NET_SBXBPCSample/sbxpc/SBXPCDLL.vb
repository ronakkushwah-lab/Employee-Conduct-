
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.IO

Namespace sbxpc
    Class SBXPCDLL
        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Sub _DotNET()
        End Sub
        Public Shared Sub DotNET()
            _DotNET()
        End Sub

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _EnableDevice(dwMachineNumber As Int32, bFlag As Byte) As Byte
        End Function
        Public Shared Function EnableDevice(dwMachineNumber As Int32, bFlag As Byte) As Boolean
            Return _EnableDevice(dwMachineNumber, bFlag) > 0
        End Function

#Region "Not Tested Part 1"
        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _SetMachineType(dwMachineNumber As Int32, ByRef lpszMachineType As IntPtr) As Byte
        End Function

        Public Shared Function _SetMachineType(dwMachineNumber As Int32, lpszMachineType As String) As Boolean
            Dim string_in As IntPtr = Marshal.StringToBSTR(lpszMachineType)
            Try
                Return _SetMachineType(dwMachineNumber, string_in) > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeBSTR(string_in)
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GetEnrollData(dwMachineNumber As Int32, dwEnrollNumber As Int32, dwEMachineNumber As Int32, dwBackupNumber As Int32, dwMachinePrivilege As IntPtr, ByRef dwEnrollData As IntPtr, _
            dwPassWord As IntPtr) As Byte
        End Function

        Public Shared Function GetEnrollData(dwMachineNumber As Int32, dwEnrollNumber As Int32, dwEMachineNumber As Int32, dwBackupNumber As Int32, ByRef dwMachinePrivilege As Int32, dwEnrollData As Byte(), _
            ByRef dwPassWord As Int32) As Boolean
            dwMachinePrivilege = 0
            dwPassWord = 0

            Dim privilege As Byte() = New Byte(3) {}
            Dim password As Byte() = New Byte(3) {}
            Dim gh_privilege As GCHandle = GCHandle.Alloc(privilege, GCHandleType.Pinned)
            Dim gh_password As GCHandle = GCHandle.Alloc(password, GCHandleType.Pinned)
            Dim gh_enroll_data As GCHandle = GCHandle.Alloc(dwEnrollData, GCHandleType.Pinned)

            Try
                Dim addr_privilege As IntPtr = gh_privilege.AddrOfPinnedObject()
                Dim addr_password As IntPtr = gh_password.AddrOfPinnedObject()
                Dim addr_enroll_data As IntPtr = gh_enroll_data.AddrOfPinnedObject()

                Dim ret As Byte = _GetEnrollData(dwMachineNumber, dwEnrollNumber, dwEMachineNumber, dwBackupNumber, addr_privilege, addr_enroll_data, _
                    addr_password)

                dwMachinePrivilege = New BinaryReader(New MemoryStream(privilege)).ReadInt32()
                dwPassWord = New BinaryReader(New MemoryStream(password)).ReadInt32()

                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                gh_privilege.Free()
                gh_password.Free()
                gh_enroll_data.Free()
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GetEnrollData1(dwMachineNumber As Int32, dwEnrollNumber As Int32, dwBackupNumber As Int32, dwMachinePrivilege As IntPtr, ByRef dwEnrollData As IntPtr, dwPassWord As IntPtr) As Byte
        End Function

        Public Shared Function GetEnrollData1(dwMachineNumber As Int32, dwEnrollNumber As Int32, dwBackupNumber As Int32, ByRef dwMachinePrivilege As Int32, dwEnrollData As IntPtr, ByRef dwPassWord As Int32) As Boolean
            dwMachinePrivilege = 0
            dwPassWord = 0

            Dim privilege As Byte() = New Byte(3) {}
            Dim password As Byte() = New Byte(3) {}
            Dim gh_privilege As GCHandle = GCHandle.Alloc(privilege, GCHandleType.Pinned)
            Dim gh_password As GCHandle = GCHandle.Alloc(password, GCHandleType.Pinned)

            Try
                Dim addr_privilege As IntPtr = gh_privilege.AddrOfPinnedObject()
                Dim addr_password As IntPtr = gh_password.AddrOfPinnedObject()

                Dim ret As Byte = _GetEnrollData1(dwMachineNumber, dwEnrollNumber, dwBackupNumber, addr_privilege, dwEnrollData, addr_password)

                dwMachinePrivilege = New BinaryReader(New MemoryStream(privilege)).ReadInt32()
                dwPassWord = New BinaryReader(New MemoryStream(password)).ReadInt32()

                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                gh_privilege.Free()
                gh_password.Free()
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _SetEnrollData(dwMachineNumber As Int32, dwEnrollNumber As Int32, dwEMachineNumber As Int32, dwBackupNumber As Int32, dwMachinePrivilege As Int32, ByRef dwEnrollData As IntPtr, _
            dwPassWord As Int32) As Byte
        End Function

        Public Shared Function SetEnrollData(dwMachineNumber As Int32, dwEnrollNumber As Int32, dwEMachineNumber As Int32, dwBackupNumber As Int32, dwMachinePrivilege As Int32, dwEnrollData As Byte(), _
            dwPassWord As Int32) As Boolean
            Dim gh_enroll_data As GCHandle = GCHandle.Alloc(dwEnrollData, GCHandleType.Pinned)
            Try
                Dim addr As IntPtr = gh_enroll_data.AddrOfPinnedObject()
                Dim ret As Byte = _SetEnrollData(dwMachineNumber, dwEnrollNumber, dwEMachineNumber, dwBackupNumber, dwMachinePrivilege, addr, _
                    dwPassWord)

                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                gh_enroll_data.Free()
            End Try
        End Function
#End Region

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _SetEnrollData1(dwMachineNumber As Int32, dwEnrollNumber As Int32, dwBackupNumber As Int32, dwMachinePrivilege As Int32, ByRef dwEnrollData As IntPtr, dwPassWord As Int32) As Byte
        End Function

        Public Shared Function SetEnrollData1(dwMachineNumber As Int32, dwEnrollNumber As Int32, dwBackupNumber As Int32, dwMachinePrivilege As Int32, dwEnrollData As IntPtr, dwPassWord As Int32) As Boolean
            Try
                Dim ret As Byte = _SetEnrollData1(dwMachineNumber, dwEnrollNumber, dwBackupNumber, dwMachinePrivilege, dwEnrollData, dwPassWord)

                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            End Try
        End Function

#Region "Not Tested Part 2"
        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _DeleteEnrollData(dwMachineNumber As Int32, dwEnrollNumber As Int32, dwEMachineNumber As Int32, dwBackupNumber As Int32) As Byte
        End Function

        Public Shared Function DeleteEnrollData(dwMachineNumber As Int32, dwEnrollNumber As Int32, dwEMachineNumber As Int32, dwBackupNumber As Int32) As Boolean
            Return _DeleteEnrollData(dwMachineNumber, dwEnrollNumber, dwEMachineNumber, dwBackupNumber) > 0
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _ReadSuperLogData(dwMachineNumber As Int32, bReadMark As Byte) As Byte
        End Function
        Public Shared Function ReadSuperLogData(dwMachineNumber As Int32, bReadMark As Byte) As Boolean
            Return _ReadSuperLogData(dwMachineNumber, bReadMark) > 0
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GetSuperLogData(dwMachineNumber As Int32, dwTMachineNumber As IntPtr, dwSEnrollNumber As IntPtr, dwSMachineNumber As IntPtr, dwGEnrollNumber As IntPtr, dwGMachineNumber As IntPtr, _
            dwManipulation As IntPtr, dwBackupNumber As IntPtr, dwYear As IntPtr, dwMonth As IntPtr, dwDay As IntPtr, dwHour As IntPtr, _
            dwMinute As IntPtr, dwSecond As IntPtr) As Byte
        End Function

        Public Shared Function GetSuperLogData(dwMachineNumber As Int32, ByRef dwTMachineNumber As Int32, ByRef dwSEnrollNumber As Int32, ByRef dwSMachineNumber As Int32, ByRef dwGEnrollNumber As Int32, ByRef dwGMachineNumber As Int32, _
            ByRef dwManipulation As Int32, ByRef dwBackupNumber As Int32, ByRef dwYear As Int32, ByRef dwMonth As Int32, ByRef dwDay As Int32, ByRef dwHour As Int32, _
            ByRef dwMinute As Int32, ByRef dwSecond As Int32) As Boolean
            dwTMachineNumber = 0
            dwSEnrollNumber = 0
            dwSMachineNumber = 0
            dwGEnrollNumber = 0
            dwGMachineNumber = 0
            dwManipulation = 0
            dwBackupNumber = 0
            dwYear = 0
            dwMonth = 0
            dwDay = 0
            dwHour = 0
            dwMinute = 0
            dwSecond = 0

            Dim bin_dwTMachineNumber As Byte() = New Byte(3) {}
            Dim bin_dwSEnrollNumber As Byte() = New Byte(3) {}
            Dim bin_dwSMachineNumber As Byte() = New Byte(3) {}
            Dim bin_dwGEnrollNumber As Byte() = New Byte(3) {}
            Dim bin_dwGMachineNumber As Byte() = New Byte(3) {}
            Dim bin_dwManipulation As Byte() = New Byte(3) {}
            Dim bin_dwBackupNumber As Byte() = New Byte(3) {}
            Dim bin_dwYear As Byte() = New Byte(3) {}
            Dim bin_dwMonth As Byte() = New Byte(3) {}
            Dim bin_dwDay As Byte() = New Byte(3) {}
            Dim bin_dwHour As Byte() = New Byte(3) {}
            Dim bin_dwMinute As Byte() = New Byte(3) {}
            Dim bin_dwSecond As Byte() = New Byte(3) {}

            Dim gh_dwTMachineNumber As GCHandle = GCHandle.Alloc(bin_dwTMachineNumber, GCHandleType.Pinned)
            Dim gh_dwSEnrollNumber As GCHandle = GCHandle.Alloc(bin_dwSEnrollNumber, GCHandleType.Pinned)
            Dim gh_dwSMachineNumber As GCHandle = GCHandle.Alloc(bin_dwSMachineNumber, GCHandleType.Pinned)
            Dim gh_dwGEnrollNumber As GCHandle = GCHandle.Alloc(bin_dwGEnrollNumber, GCHandleType.Pinned)
            Dim gh_dwGMachineNumber As GCHandle = GCHandle.Alloc(bin_dwGMachineNumber, GCHandleType.Pinned)
            Dim gh_dwManipulation As GCHandle = GCHandle.Alloc(bin_dwManipulation, GCHandleType.Pinned)
            Dim gh_dwBackupNumber As GCHandle = GCHandle.Alloc(bin_dwBackupNumber, GCHandleType.Pinned)
            Dim gh_dwYear As GCHandle = GCHandle.Alloc(bin_dwYear, GCHandleType.Pinned)
            Dim gh_dwMonth As GCHandle = GCHandle.Alloc(bin_dwMonth, GCHandleType.Pinned)
            Dim gh_dwDay As GCHandle = GCHandle.Alloc(bin_dwDay, GCHandleType.Pinned)
            Dim gh_dwHour As GCHandle = GCHandle.Alloc(bin_dwHour, GCHandleType.Pinned)
            Dim gh_dwMinute As GCHandle = GCHandle.Alloc(bin_dwMinute, GCHandleType.Pinned)
            Dim gh_dwSecond As GCHandle = GCHandle.Alloc(bin_dwSecond, GCHandleType.Pinned)
            Try
                Dim addr_dwTMachineNumber As IntPtr = gh_dwTMachineNumber.AddrOfPinnedObject()
                Dim addr_dwSEnrollNumber As IntPtr = gh_dwSEnrollNumber.AddrOfPinnedObject()
                Dim addr_dwSMachineNumber As IntPtr = gh_dwSMachineNumber.AddrOfPinnedObject()
                Dim addr_dwGEnrollNumber As IntPtr = gh_dwGEnrollNumber.AddrOfPinnedObject()
                Dim addr_dwGMachineNumber As IntPtr = gh_dwGMachineNumber.AddrOfPinnedObject()
                Dim addr_dwManipulation As IntPtr = gh_dwManipulation.AddrOfPinnedObject()
                Dim addr_dwBackupNumber As IntPtr = gh_dwBackupNumber.AddrOfPinnedObject()
                Dim addr_dwYear As IntPtr = gh_dwYear.AddrOfPinnedObject()
                Dim addr_dwMonth As IntPtr = gh_dwMonth.AddrOfPinnedObject()
                Dim addr_dwDay As IntPtr = gh_dwDay.AddrOfPinnedObject()
                Dim addr_dwHour As IntPtr = gh_dwHour.AddrOfPinnedObject()
                Dim addr_dwMinute As IntPtr = gh_dwMinute.AddrOfPinnedObject()
                Dim addr_dwSecond As IntPtr = gh_dwSecond.AddrOfPinnedObject()

                Dim ret As Byte = _GetSuperLogData(dwMachineNumber, addr_dwTMachineNumber, addr_dwSEnrollNumber, addr_dwSMachineNumber, addr_dwGEnrollNumber, addr_dwGMachineNumber, _
                    addr_dwManipulation, addr_dwBackupNumber, addr_dwYear, addr_dwMonth, addr_dwDay, addr_dwHour, _
                    addr_dwMinute, addr_dwSecond)

                dwTMachineNumber = New BinaryReader(New MemoryStream(bin_dwTMachineNumber)).ReadInt32()
                dwSEnrollNumber = New BinaryReader(New MemoryStream(bin_dwSEnrollNumber)).ReadInt32()
                dwSMachineNumber = New BinaryReader(New MemoryStream(bin_dwSMachineNumber)).ReadInt32()
                dwGEnrollNumber = New BinaryReader(New MemoryStream(bin_dwGEnrollNumber)).ReadInt32()
                dwGMachineNumber = New BinaryReader(New MemoryStream(bin_dwGMachineNumber)).ReadInt32()
                dwManipulation = New BinaryReader(New MemoryStream(bin_dwManipulation)).ReadInt32()
                dwBackupNumber = New BinaryReader(New MemoryStream(bin_dwBackupNumber)).ReadInt32()
                dwYear = New BinaryReader(New MemoryStream(bin_dwYear)).ReadInt32()
                dwMonth = New BinaryReader(New MemoryStream(bin_dwMonth)).ReadInt32()
                dwDay = New BinaryReader(New MemoryStream(bin_dwDay)).ReadInt32()
                dwHour = New BinaryReader(New MemoryStream(bin_dwHour)).ReadInt32()
                dwMinute = New BinaryReader(New MemoryStream(bin_dwMinute)).ReadInt32()
                dwSecond = New BinaryReader(New MemoryStream(bin_dwSecond)).ReadInt32()

                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                gh_dwTMachineNumber.Free()
                gh_dwSEnrollNumber.Free()
                gh_dwSMachineNumber.Free()
                gh_dwGEnrollNumber.Free()
                gh_dwGMachineNumber.Free()
                gh_dwManipulation.Free()
                gh_dwBackupNumber.Free()
                gh_dwYear.Free()
                gh_dwMonth.Free()
                gh_dwDay.Free()
                gh_dwHour.Free()
                gh_dwMinute.Free()
                gh_dwSecond.Free()
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _ReadGeneralLogData(dwMachineNumber As Int32, bReadMark As Byte) As Byte
        End Function

        Public Shared Function ReadGeneralLogData(dwMachineNumber As Int32, bReadMark As Byte) As Boolean
            Return _ReadGeneralLogData(dwMachineNumber, bReadMark) > 0
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GetGeneralLogData(dwMachineNumber As Int32, dwTMachineNumber As IntPtr, dwEnrollNumber As IntPtr, dwEMachineNumber As IntPtr, dwVerifyMode As IntPtr, dwYear As IntPtr, _
            dwMonth As IntPtr, dwDay As IntPtr, dwHour As IntPtr, dwMinute As IntPtr, dwSecond As IntPtr) As Byte
        End Function

        Public Shared Function GetGeneralLogData(dwMachineNumber As Int32, ByRef dwTMachineNumber As Int32, ByRef dwEnrollNumber As Int32, ByRef dwEMachineNumber As Int32, ByRef dwVerifyMode As Int32, ByRef dwYear As Int32, _
            ByRef dwMonth As Int32, ByRef dwDay As Int32, ByRef dwHour As Int32, ByRef dwMinute As Int32, ByRef dwSecond As Int32) As Boolean
            dwTMachineNumber = 0
            dwEnrollNumber = 0
            dwEMachineNumber = 0
            dwVerifyMode = 0
            dwYear = 0
            dwMonth = 0
            dwDay = 0
            dwHour = 0
            dwMinute = 0
            dwSecond = 0

            Dim bin_dwTMachineNumber As Byte() = New Byte(3) {}
            Dim bin_dwEnrollNumber As Byte() = New Byte(3) {}
            Dim bin_dwEMachineNumber As Byte() = New Byte(3) {}
            Dim bin_dwVerifyMode As Byte() = New Byte(3) {}
            Dim bin_dwYear As Byte() = New Byte(3) {}
            Dim bin_dwMonth As Byte() = New Byte(3) {}
            Dim bin_dwDay As Byte() = New Byte(3) {}
            Dim bin_dwHour As Byte() = New Byte(3) {}
            Dim bin_dwMinute As Byte() = New Byte(3) {}
            Dim bin_dwSecond As Byte() = New Byte(3) {}

            Dim gh_dwTMachineNumber As GCHandle = GCHandle.Alloc(bin_dwTMachineNumber, GCHandleType.Pinned)
            Dim gh_dwEnrollNumber As GCHandle = GCHandle.Alloc(bin_dwEnrollNumber, GCHandleType.Pinned)
            Dim gh_dwEMachineNumber As GCHandle = GCHandle.Alloc(bin_dwEMachineNumber, GCHandleType.Pinned)
            Dim gh_dwVerifyMode As GCHandle = GCHandle.Alloc(bin_dwVerifyMode, GCHandleType.Pinned)
            Dim gh_dwYear As GCHandle = GCHandle.Alloc(bin_dwYear, GCHandleType.Pinned)
            Dim gh_dwMonth As GCHandle = GCHandle.Alloc(bin_dwMonth, GCHandleType.Pinned)
            Dim gh_dwDay As GCHandle = GCHandle.Alloc(bin_dwDay, GCHandleType.Pinned)
            Dim gh_dwHour As GCHandle = GCHandle.Alloc(bin_dwHour, GCHandleType.Pinned)
            Dim gh_dwMinute As GCHandle = GCHandle.Alloc(bin_dwMinute, GCHandleType.Pinned)
            Dim gh_dwSecond As GCHandle = GCHandle.Alloc(bin_dwSecond, GCHandleType.Pinned)

            Try
                Dim addr_dwTMachineNumber As IntPtr = gh_dwTMachineNumber.AddrOfPinnedObject()
                Dim addr_dwEnrollNumber As IntPtr = gh_dwEnrollNumber.AddrOfPinnedObject()
                Dim addr_dwEMachineNumber As IntPtr = gh_dwEMachineNumber.AddrOfPinnedObject()
                Dim addr_dwVerifyMode As IntPtr = gh_dwVerifyMode.AddrOfPinnedObject()
                Dim addr_dwYear As IntPtr = gh_dwYear.AddrOfPinnedObject()
                Dim addr_dwMonth As IntPtr = gh_dwMonth.AddrOfPinnedObject()
                Dim addr_dwDay As IntPtr = gh_dwDay.AddrOfPinnedObject()
                Dim addr_dwHour As IntPtr = gh_dwHour.AddrOfPinnedObject()
                Dim addr_dwMinute As IntPtr = gh_dwMinute.AddrOfPinnedObject()
                Dim addr_dwSecond As IntPtr = gh_dwSecond.AddrOfPinnedObject()

                Dim ret As Byte = _GetGeneralLogData(dwMachineNumber, addr_dwTMachineNumber, addr_dwEnrollNumber, addr_dwEMachineNumber, addr_dwVerifyMode, addr_dwYear, _
                    addr_dwMonth, addr_dwDay, addr_dwHour, addr_dwMinute, addr_dwSecond)

                dwTMachineNumber = New BinaryReader(New MemoryStream(bin_dwTMachineNumber)).ReadInt32()
                dwEnrollNumber = New BinaryReader(New MemoryStream(bin_dwEnrollNumber)).ReadInt32()
                dwEMachineNumber = New BinaryReader(New MemoryStream(bin_dwEMachineNumber)).ReadInt32()
                dwVerifyMode = New BinaryReader(New MemoryStream(bin_dwVerifyMode)).ReadInt32()
                dwYear = New BinaryReader(New MemoryStream(bin_dwYear)).ReadInt32()
                dwMonth = New BinaryReader(New MemoryStream(bin_dwMonth)).ReadInt32()
                dwDay = New BinaryReader(New MemoryStream(bin_dwDay)).ReadInt32()
                dwHour = New BinaryReader(New MemoryStream(bin_dwHour)).ReadInt32()
                dwMinute = New BinaryReader(New MemoryStream(bin_dwMinute)).ReadInt32()
                dwSecond = New BinaryReader(New MemoryStream(bin_dwSecond)).ReadInt32()

                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                gh_dwTMachineNumber.Free()
                gh_dwEnrollNumber.Free()
                gh_dwEMachineNumber.Free()
                gh_dwVerifyMode.Free()
                gh_dwYear.Free()
                gh_dwMonth.Free()
                gh_dwDay.Free()
                gh_dwHour.Free()
                gh_dwMinute.Free()
                gh_dwSecond.Free()
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _ReadAllSLogData(dwMachineNumber As Int32) As Byte
        End Function

        Public Shared Function ReadAllSLogData(dwMachineNumber As Int32) As Boolean
            Return _ReadAllSLogData(dwMachineNumber) > 0
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GetAllSLogData(dwMachineNumber As Int32, dwTMachineNumber As IntPtr, dwSEnrollNumber As IntPtr, dwSMachineNumber As IntPtr, dwGEnrollNumber As IntPtr, dwGMachineNumber As IntPtr, _
            dwManipulation As IntPtr, dwBackupNumber As IntPtr, dwYear As IntPtr, dwMonth As IntPtr, dwDay As IntPtr, dwHour As IntPtr, _
            dwMinute As IntPtr, dwSecond As IntPtr) As Byte
        End Function

        Public Shared Function GetAllSLogData(dwMachineNumber As Int32, ByRef dwTMachineNumber As Int32, ByRef dwSEnrollNumber As Int32, ByRef dwSMachineNumber As Int32, ByRef dwGEnrollNumber As Int32, ByRef dwGMachineNumber As Int32, _
            ByRef dwManipulation As Int32, ByRef dwBackupNumber As Int32, ByRef dwYear As Int32, ByRef dwMonth As Int32, ByRef dwDay As Int32, ByRef dwHour As Int32, _
            ByRef dwMinute As Int32, ByRef dwSecond As Int32) As Boolean
            dwTMachineNumber = 0
            dwSEnrollNumber = 0
            dwSMachineNumber = 0
            dwGEnrollNumber = 0
            dwGMachineNumber = 0
            dwManipulation = 0
            dwBackupNumber = 0
            dwYear = 0
            dwMonth = 0
            dwDay = 0
            dwHour = 0
            dwMinute = 0
            dwSecond = 0

            Dim bin_dwTMachineNumber As Byte() = New Byte(3) {}
            Dim bin_dwSEnrollNumber As Byte() = New Byte(3) {}
            Dim bin_dwSMachineNumber As Byte() = New Byte(3) {}
            Dim bin_dwGEnrollNumber As Byte() = New Byte(3) {}
            Dim bin_dwGMachineNumber As Byte() = New Byte(3) {}
            Dim bin_dwManipulation As Byte() = New Byte(3) {}
            Dim bin_dwBackupNumber As Byte() = New Byte(3) {}
            Dim bin_dwYear As Byte() = New Byte(3) {}
            Dim bin_dwMonth As Byte() = New Byte(3) {}
            Dim bin_dwDay As Byte() = New Byte(3) {}
            Dim bin_dwHour As Byte() = New Byte(3) {}
            Dim bin_dwMinute As Byte() = New Byte(3) {}
            Dim bin_dwSecond As Byte() = New Byte(3) {}

            Dim gh_dwTMachineNumber As GCHandle = GCHandle.Alloc(bin_dwTMachineNumber, GCHandleType.Pinned)
            Dim gh_dwSEnrollNumber As GCHandle = GCHandle.Alloc(bin_dwSEnrollNumber, GCHandleType.Pinned)
            Dim gh_dwSMachineNumber As GCHandle = GCHandle.Alloc(bin_dwSMachineNumber, GCHandleType.Pinned)
            Dim gh_dwGEnrollNumber As GCHandle = GCHandle.Alloc(bin_dwGEnrollNumber, GCHandleType.Pinned)
            Dim gh_dwGMachineNumber As GCHandle = GCHandle.Alloc(bin_dwGMachineNumber, GCHandleType.Pinned)
            Dim gh_dwManipulation As GCHandle = GCHandle.Alloc(bin_dwManipulation, GCHandleType.Pinned)
            Dim gh_dwBackupNumber As GCHandle = GCHandle.Alloc(bin_dwBackupNumber, GCHandleType.Pinned)
            Dim gh_dwYear As GCHandle = GCHandle.Alloc(bin_dwYear, GCHandleType.Pinned)
            Dim gh_dwMonth As GCHandle = GCHandle.Alloc(bin_dwMonth, GCHandleType.Pinned)
            Dim gh_dwDay As GCHandle = GCHandle.Alloc(bin_dwDay, GCHandleType.Pinned)
            Dim gh_dwHour As GCHandle = GCHandle.Alloc(bin_dwHour, GCHandleType.Pinned)
            Dim gh_dwMinute As GCHandle = GCHandle.Alloc(bin_dwMinute, GCHandleType.Pinned)
            Dim gh_dwSecond As GCHandle = GCHandle.Alloc(bin_dwSecond, GCHandleType.Pinned)

            Try
                Dim addr_dwTMachineNumber As IntPtr = gh_dwTMachineNumber.AddrOfPinnedObject()
                Dim addr_dwSEnrollNumber As IntPtr = gh_dwSEnrollNumber.AddrOfPinnedObject()
                Dim addr_dwSMachineNumber As IntPtr = gh_dwSMachineNumber.AddrOfPinnedObject()
                Dim addr_dwGEnrollNumber As IntPtr = gh_dwGEnrollNumber.AddrOfPinnedObject()
                Dim addr_dwGMachineNumber As IntPtr = gh_dwGMachineNumber.AddrOfPinnedObject()
                Dim addr_dwManipulation As IntPtr = gh_dwManipulation.AddrOfPinnedObject()
                Dim addr_dwBackupNumber As IntPtr = gh_dwBackupNumber.AddrOfPinnedObject()
                Dim addr_dwYear As IntPtr = gh_dwYear.AddrOfPinnedObject()
                Dim addr_dwMonth As IntPtr = gh_dwMonth.AddrOfPinnedObject()
                Dim addr_dwDay As IntPtr = gh_dwDay.AddrOfPinnedObject()
                Dim addr_dwHour As IntPtr = gh_dwHour.AddrOfPinnedObject()
                Dim addr_dwMinute As IntPtr = gh_dwMinute.AddrOfPinnedObject()
                Dim addr_dwSecond As IntPtr = gh_dwSecond.AddrOfPinnedObject()

                Dim ret As Byte = _GetAllSLogData(dwMachineNumber, addr_dwTMachineNumber, addr_dwSEnrollNumber, addr_dwSMachineNumber, addr_dwGEnrollNumber, addr_dwGMachineNumber, _
                    addr_dwManipulation, addr_dwBackupNumber, addr_dwYear, addr_dwMonth, addr_dwDay, addr_dwHour, _
                    addr_dwMinute, addr_dwSecond)

                dwTMachineNumber = New BinaryReader(New MemoryStream(bin_dwTMachineNumber)).ReadInt32()
                dwSEnrollNumber = New BinaryReader(New MemoryStream(bin_dwSEnrollNumber)).ReadInt32()
                dwSMachineNumber = New BinaryReader(New MemoryStream(bin_dwSMachineNumber)).ReadInt32()
                dwGEnrollNumber = New BinaryReader(New MemoryStream(bin_dwGEnrollNumber)).ReadInt32()
                dwGMachineNumber = New BinaryReader(New MemoryStream(bin_dwGMachineNumber)).ReadInt32()
                dwManipulation = New BinaryReader(New MemoryStream(bin_dwManipulation)).ReadInt32()
                dwBackupNumber = New BinaryReader(New MemoryStream(bin_dwBackupNumber)).ReadInt32()
                dwYear = New BinaryReader(New MemoryStream(bin_dwYear)).ReadInt32()
                dwMonth = New BinaryReader(New MemoryStream(bin_dwMonth)).ReadInt32()
                dwDay = New BinaryReader(New MemoryStream(bin_dwDay)).ReadInt32()
                dwHour = New BinaryReader(New MemoryStream(bin_dwHour)).ReadInt32()
                dwMinute = New BinaryReader(New MemoryStream(bin_dwMinute)).ReadInt32()
                dwSecond = New BinaryReader(New MemoryStream(bin_dwSecond)).ReadInt32()

                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                gh_dwTMachineNumber.Free()
                gh_dwSEnrollNumber.Free()
                gh_dwSMachineNumber.Free()
                gh_dwGEnrollNumber.Free()
                gh_dwGMachineNumber.Free()
                gh_dwManipulation.Free()
                gh_dwBackupNumber.Free()
                gh_dwYear.Free()
                gh_dwMonth.Free()
                gh_dwDay.Free()
                gh_dwHour.Free()
                gh_dwMinute.Free()
                gh_dwSecond.Free()
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _ReadAllGLogData(dwMachineNumber As Int32) As Byte
        End Function

        Public Shared Function ReadAllGLogData(dwMachineNumber As Int32) As Boolean
            Return _ReadAllGLogData(dwMachineNumber) > 0
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GetAllGLogData(dwMachineNumber As Int32, dwTMachineNumber As IntPtr, dwEnrollNumber As IntPtr, dwEMachineNumber As IntPtr, dwVerifyMode As IntPtr, dwYear As IntPtr, _
            dwMonth As IntPtr, dwDay As IntPtr, dwHour As IntPtr, dwMinute As IntPtr, dwSecond As IntPtr) As Byte
        End Function

        Public Shared Function GetAllGLogData(dwMachineNumber As Int32, ByRef dwTMachineNumber As Int32, ByRef dwEnrollNumber As Int32, ByRef dwEMachineNumber As Int32, ByRef dwVerifyMode As Int32, ByRef dwYear As Int32, _
            ByRef dwMonth As Int32, ByRef dwDay As Int32, ByRef dwHour As Int32, ByRef dwMinute As Int32, ByRef dwSecond As Int32) As Boolean
            dwTMachineNumber = 0
            dwEnrollNumber = 0
            dwEMachineNumber = 0
            dwVerifyMode = 0
            dwYear = 0
            dwMonth = 0
            dwDay = 0
            dwHour = 0
            dwMinute = 0
            dwSecond = 0

            Dim bin_dwTMachineNumber As Byte() = New Byte(3) {}
            Dim bin_dwEnrollNumber As Byte() = New Byte(3) {}
            Dim bin_dwEMachineNumber As Byte() = New Byte(3) {}
            Dim bin_dwVerifyMode As Byte() = New Byte(3) {}
            Dim bin_dwYear As Byte() = New Byte(3) {}
            Dim bin_dwMonth As Byte() = New Byte(3) {}
            Dim bin_dwDay As Byte() = New Byte(3) {}
            Dim bin_dwHour As Byte() = New Byte(3) {}
            Dim bin_dwMinute As Byte() = New Byte(3) {}
            Dim bin_dwSecond As Byte() = New Byte(3) {}

            Dim gh_dwTMachineNumber As GCHandle = GCHandle.Alloc(bin_dwTMachineNumber, GCHandleType.Pinned)
            Dim gh_dwEnrollNumber As GCHandle = GCHandle.Alloc(bin_dwEnrollNumber, GCHandleType.Pinned)
            Dim gh_dwEMachineNumber As GCHandle = GCHandle.Alloc(bin_dwEMachineNumber, GCHandleType.Pinned)
            Dim gh_dwVerifyMode As GCHandle = GCHandle.Alloc(bin_dwVerifyMode, GCHandleType.Pinned)
            Dim gh_dwYear As GCHandle = GCHandle.Alloc(bin_dwYear, GCHandleType.Pinned)
            Dim gh_dwMonth As GCHandle = GCHandle.Alloc(bin_dwMonth, GCHandleType.Pinned)
            Dim gh_dwDay As GCHandle = GCHandle.Alloc(bin_dwDay, GCHandleType.Pinned)
            Dim gh_dwHour As GCHandle = GCHandle.Alloc(bin_dwHour, GCHandleType.Pinned)
            Dim gh_dwMinute As GCHandle = GCHandle.Alloc(bin_dwMinute, GCHandleType.Pinned)
            Dim gh_dwSecond As GCHandle = GCHandle.Alloc(bin_dwSecond, GCHandleType.Pinned)

            Try
                Dim addr_dwTMachineNumber As IntPtr = gh_dwTMachineNumber.AddrOfPinnedObject()
                Dim addr_dwEnrollNumber As IntPtr = gh_dwEnrollNumber.AddrOfPinnedObject()
                Dim addr_dwEMachineNumber As IntPtr = gh_dwEMachineNumber.AddrOfPinnedObject()
                Dim addr_dwVerifyMode As IntPtr = gh_dwVerifyMode.AddrOfPinnedObject()
                Dim addr_dwYear As IntPtr = gh_dwYear.AddrOfPinnedObject()
                Dim addr_dwMonth As IntPtr = gh_dwMonth.AddrOfPinnedObject()
                Dim addr_dwDay As IntPtr = gh_dwDay.AddrOfPinnedObject()
                Dim addr_dwHour As IntPtr = gh_dwHour.AddrOfPinnedObject()
                Dim addr_dwMinute As IntPtr = gh_dwMinute.AddrOfPinnedObject()
                Dim addr_dwSecond As IntPtr = gh_dwSecond.AddrOfPinnedObject()

                Dim ret As Byte = _GetAllGLogData(dwMachineNumber, addr_dwTMachineNumber, addr_dwEnrollNumber, addr_dwEMachineNumber, addr_dwVerifyMode, addr_dwYear, _
                    addr_dwMonth, addr_dwDay, addr_dwHour, addr_dwMinute, addr_dwSecond)

                dwTMachineNumber = New BinaryReader(New MemoryStream(bin_dwTMachineNumber)).ReadInt32()
                dwEnrollNumber = New BinaryReader(New MemoryStream(bin_dwEnrollNumber)).ReadInt32()
                dwEMachineNumber = New BinaryReader(New MemoryStream(bin_dwEMachineNumber)).ReadInt32()
                dwVerifyMode = New BinaryReader(New MemoryStream(bin_dwVerifyMode)).ReadInt32()
                dwYear = New BinaryReader(New MemoryStream(bin_dwYear)).ReadInt32()
                dwMonth = New BinaryReader(New MemoryStream(bin_dwMonth)).ReadInt32()
                dwDay = New BinaryReader(New MemoryStream(bin_dwDay)).ReadInt32()
                dwHour = New BinaryReader(New MemoryStream(bin_dwHour)).ReadInt32()
                dwMinute = New BinaryReader(New MemoryStream(bin_dwMinute)).ReadInt32()
                dwSecond = New BinaryReader(New MemoryStream(bin_dwSecond)).ReadInt32()

                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                gh_dwTMachineNumber.Free()
                gh_dwEnrollNumber.Free()
                gh_dwEMachineNumber.Free()
                gh_dwVerifyMode.Free()
                gh_dwYear.Free()
                gh_dwMonth.Free()
                gh_dwDay.Free()
                gh_dwHour.Free()
                gh_dwMinute.Free()
                gh_dwSecond.Free()
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GetDeviceStatus(dwMachineNumber As Int32, dwStatus As Int32, dwValue As IntPtr) As Byte
        End Function

        Public Shared Function GetDeviceStatus(dwMachineNumber As Int32, dwStatus As Int32, ByRef dwValue As UInt32) As Boolean
            dwValue = 0

            Dim bin_dwValue As Byte() = New Byte(3) {}

            Dim gh_dwValue As GCHandle = GCHandle.Alloc(bin_dwValue, GCHandleType.Pinned)

            Try
                Dim addr_dwValue As IntPtr = gh_dwValue.AddrOfPinnedObject()

                Dim ret As Byte = _GetDeviceStatus(dwMachineNumber, dwStatus, addr_dwValue)

                dwValue = New BinaryReader(New MemoryStream(bin_dwValue)).ReadUInt32()

                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                gh_dwValue.Free()
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GetDeviceInfo(dwMachineNumber As Int32, dwInfo As Int32, dwValue As IntPtr) As Byte
        End Function

        Public Shared Function GetDeviceInfo(dwMachineNumber As Int32, dwInfo As Int32, ByRef dwValue As UInt32) As Boolean
            dwValue = 0

            Dim bin_dwValue As Byte() = New Byte(3) {}

            Dim gh_dwValue As GCHandle = GCHandle.Alloc(bin_dwValue, GCHandleType.Pinned)
            Try
                Dim addr_dwValue As IntPtr = gh_dwValue.AddrOfPinnedObject()

                Dim ret As Byte = _GetDeviceInfo(dwMachineNumber, dwInfo, addr_dwValue)

                dwValue = New BinaryReader(New MemoryStream(bin_dwValue)).ReadUInt32()

                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                gh_dwValue.Free()
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _SetDeviceInfo(dwMachineNumber As Int32, dwInfo As Int32, dwValue As Int32) As Byte
        End Function

        Public Shared Function SetDeviceInfo(dwMachineNumber As Int32, dwInfo As Int32, dwValue As Int32) As Boolean
            Return _SetDeviceInfo(dwMachineNumber, dwInfo, dwValue) > 0
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GetDeviceTime(dwMachineNumber As Int32, dwYear As IntPtr, dwMonth As IntPtr, dwDay As IntPtr, dwHour As IntPtr, dwMinute As IntPtr, _
            dwSecond As IntPtr, dwDayOfWeek As IntPtr) As Byte
        End Function

        Public Shared Function GetDeviceTime(dwMachineNumber As Int32, ByRef dwYear As Int32, ByRef dwMonth As Int32, ByRef dwDay As Int32, ByRef dwHour As Int32, ByRef dwMinute As Int32, _
            ByRef dwSecond As Int32, ByRef dwDayOfWeek As Int32) As Boolean
            dwYear = 0
            dwMonth = 0
            dwDay = 0
            dwHour = 0
            dwMinute = 0
            dwSecond = 0
            dwDayOfWeek = 0

            Dim bin_dwYear As Byte() = New Byte(3) {}
            Dim bin_dwMonth As Byte() = New Byte(3) {}
            Dim bin_dwDay As Byte() = New Byte(3) {}
            Dim bin_dwHour As Byte() = New Byte(3) {}
            Dim bin_dwMinute As Byte() = New Byte(3) {}
            Dim bin_dwSecond As Byte() = New Byte(3) {}
            Dim bin_dwDayOfWeek As Byte() = New Byte(3) {}

            Dim gh_dwYear As GCHandle = GCHandle.Alloc(bin_dwYear, GCHandleType.Pinned)
            Dim gh_dwMonth As GCHandle = GCHandle.Alloc(bin_dwMonth, GCHandleType.Pinned)
            Dim gh_dwDay As GCHandle = GCHandle.Alloc(bin_dwDay, GCHandleType.Pinned)
            Dim gh_dwHour As GCHandle = GCHandle.Alloc(bin_dwHour, GCHandleType.Pinned)
            Dim gh_dwMinute As GCHandle = GCHandle.Alloc(bin_dwMinute, GCHandleType.Pinned)
            Dim gh_dwSecond As GCHandle = GCHandle.Alloc(bin_dwSecond, GCHandleType.Pinned)
            Dim gh_dwDayOfWeek As GCHandle = GCHandle.Alloc(bin_dwDayOfWeek, GCHandleType.Pinned)

            Try
                Dim addr_dwYear As IntPtr = gh_dwYear.AddrOfPinnedObject()
                Dim addr_dwMonth As IntPtr = gh_dwMonth.AddrOfPinnedObject()
                Dim addr_dwDay As IntPtr = gh_dwDay.AddrOfPinnedObject()
                Dim addr_dwHour As IntPtr = gh_dwHour.AddrOfPinnedObject()
                Dim addr_dwMinute As IntPtr = gh_dwMinute.AddrOfPinnedObject()
                Dim addr_dwSecond As IntPtr = gh_dwSecond.AddrOfPinnedObject()
                Dim addr_dwDayOfWeek As IntPtr = gh_dwDayOfWeek.AddrOfPinnedObject()

                Dim ret As Byte = _GetDeviceTime(dwMachineNumber, addr_dwYear, addr_dwMonth, addr_dwDay, addr_dwHour, addr_dwMinute, _
                    addr_dwSecond, addr_dwDayOfWeek)

                dwYear = New BinaryReader(New MemoryStream(bin_dwYear)).ReadInt32()
                dwMonth = New BinaryReader(New MemoryStream(bin_dwMonth)).ReadInt32()
                dwDay = New BinaryReader(New MemoryStream(bin_dwDay)).ReadInt32()
                dwHour = New BinaryReader(New MemoryStream(bin_dwHour)).ReadInt32()
                dwMinute = New BinaryReader(New MemoryStream(bin_dwMinute)).ReadInt32()
                dwSecond = New BinaryReader(New MemoryStream(bin_dwSecond)).ReadInt32()
                dwDayOfWeek = New BinaryReader(New MemoryStream(bin_dwDayOfWeek)).ReadInt32()

                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                gh_dwYear.Free()
                gh_dwMonth.Free()
                gh_dwDay.Free()
                gh_dwHour.Free()
                gh_dwMinute.Free()
                gh_dwSecond.Free()
                gh_dwDayOfWeek.Free()
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _SetDeviceTime(dwMachineNumber As Int32) As Byte
        End Function

        Public Shared Function SetDeviceTime(dwMachineNumber As Int32) As Boolean
            Return _SetDeviceTime(dwMachineNumber) > 0
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _SetDeviceTime1(dwMachineNumber As Int32, dwYear As Int32, dwMonth As Int32, dwDay As Int32, dwHour As Int32, dwMinute As Int32, _
            dwSecond As Int32) As Byte
        End Function

        Public Shared Function SetDeviceTime1(dwMachineNumber As Int32, dwYear As Int32, dwMonth As Int32, dwDay As Int32, dwHour As Int32, dwMinute As Int32, _
            dwSecond As Int32) As Boolean
            Return _SetDeviceTime1(dwMachineNumber, dwYear, dwMonth, dwDay, dwHour, dwMinute, _
                dwSecond) > 0
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _EnableUser(dwMachineNumber As Int32, dwEnrollNumber As Int32, dwEMachineNumber As Int32, dwBackupNumber As Int32, bFlag As Byte) As Byte
        End Function

        Public Shared Function EnableUser(dwMachineNumber As Int32, dwEnrollNumber As Int32, dwEMachineNumber As Int32, dwBackupNumber As Int32, bFlag As Byte) As Boolean
            Return _EnableUser(dwMachineNumber, dwEnrollNumber, dwEMachineNumber, dwBackupNumber, bFlag) > 0
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _PowerOnAllDevice(dwMachineNumber As Int32) As Byte
        End Function

        Public Shared Function PowerOnAllDevice(dwMachineNumber As Int32) As Boolean
            Return _PowerOnAllDevice(dwMachineNumber) > 0
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _PowerOffDevice(dwMachineNumber As Int32) As Byte
        End Function

        Public Shared Function PowerOffDevice(dwMachineNumber As Int32) As Boolean
            Return _PowerOffDevice(dwMachineNumber) > 0
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _ModifyPrivilege(dwMachineNumber As Int32, dwEnrollNumber As Int32, dwEMachineNumber As Int32, dwBackupNumber As Int32, dwMachinePrivilege As Int32) As Byte
        End Function

        Public Shared Function ModifyPrivilege(dwMachineNumber As Int32, dwEnrollNumber As Int32, dwEMachineNumber As Int32, dwBackupNumber As Int32, dwMachinePrivilege As Int32) As Boolean
            Return _ModifyPrivilege(dwMachineNumber, dwEnrollNumber, dwEMachineNumber, dwBackupNumber, dwMachinePrivilege) > 0
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _ReadAllUserID(dwMachineNumber As Int32) As Byte
        End Function

        Public Shared Function ReadAllUserID(dwMachineNumber As Int32) As Boolean
            Return _ReadAllUserID(dwMachineNumber) > 0
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GetAllUserID(dwMachineNumber As Int32, dwEnrollNumber As IntPtr, dwEMachineNumber As IntPtr, dwBackupNumber As IntPtr, dwMachinePrivilege As IntPtr, dwEnable As IntPtr) As Byte
        End Function

        Public Shared Function GetAllUserID(dwMachineNumber As Int32, ByRef dwEnrollNumber As Int32, ByRef dwEMachineNumber As Int32, ByRef dwBackupNumber As Int32, ByRef dwMachinePrivilege As Int32, ByRef dwEnable As Int32) As Boolean
            dwEnrollNumber = 0
            dwEMachineNumber = 0
            dwBackupNumber = 0
            dwMachinePrivilege = 0
            dwEnable = 0

            Dim bin_dwEnrollNumber As Byte() = New Byte(3) {}
            Dim bin_dwEMachineNumber As Byte() = New Byte(3) {}
            Dim bin_dwBackupNumber As Byte() = New Byte(3) {}
            Dim bin_dwMachinePrivilege As Byte() = New Byte(3) {}
            Dim bin_dwEnable As Byte() = New Byte(3) {}

            Dim gh_dwEnrollNumber As GCHandle = GCHandle.Alloc(bin_dwEnrollNumber, GCHandleType.Pinned)
            Dim gh_dwEMachineNumber As GCHandle = GCHandle.Alloc(bin_dwEMachineNumber, GCHandleType.Pinned)
            Dim gh_dwBackupNumber As GCHandle = GCHandle.Alloc(bin_dwBackupNumber, GCHandleType.Pinned)
            Dim gh_dwMachinePrivilege As GCHandle = GCHandle.Alloc(bin_dwMachinePrivilege, GCHandleType.Pinned)
            Dim gh_dwEnable As GCHandle = GCHandle.Alloc(bin_dwEnable, GCHandleType.Pinned)

            Try
                Dim addr_dwEnrollNumber As IntPtr = gh_dwEnrollNumber.AddrOfPinnedObject()
                Dim addr_dwEMachineNumber As IntPtr = gh_dwEMachineNumber.AddrOfPinnedObject()
                Dim addr_dwBackupNumber As IntPtr = gh_dwBackupNumber.AddrOfPinnedObject()
                Dim addr_dwMachinePrivilege As IntPtr = gh_dwMachinePrivilege.AddrOfPinnedObject()
                Dim addr_dwEnable As IntPtr = gh_dwEnable.AddrOfPinnedObject()

                Dim ret As Byte = _GetAllUserID(dwMachineNumber, addr_dwEnrollNumber, addr_dwEMachineNumber, addr_dwBackupNumber, addr_dwMachinePrivilege, addr_dwEnable)

                dwEnrollNumber = New BinaryReader(New MemoryStream(bin_dwEnrollNumber)).ReadInt32()
                dwEMachineNumber = New BinaryReader(New MemoryStream(bin_dwEMachineNumber)).ReadInt32()
                dwBackupNumber = New BinaryReader(New MemoryStream(bin_dwBackupNumber)).ReadInt32()
                dwMachinePrivilege = New BinaryReader(New MemoryStream(bin_dwMachinePrivilege)).ReadInt32()
                dwEnable = New BinaryReader(New MemoryStream(bin_dwEnable)).ReadInt32()

                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                gh_dwEnrollNumber.Free()
                gh_dwEMachineNumber.Free()
                gh_dwBackupNumber.Free()
                gh_dwMachinePrivilege.Free()
                gh_dwEnable.Free()
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GetSerialNumber(dwMachineNumber As Int32, ByRef lpszSerialNumber As IntPtr) As Byte
        End Function

        Public Shared Function GetSerialNumber(dwMachineNumber As Int32, ByRef lpszSerialNumber As String) As Boolean
            lpszSerialNumber = ""
            Dim string_in As IntPtr = Marshal.StringToBSTR(lpszSerialNumber)
            Try
                Dim ret As Byte = _GetSerialNumber(dwMachineNumber, string_in)
                lpszSerialNumber = Marshal.PtrToStringBSTR(string_in)
                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeBSTR(string_in)
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GetBackupNumber(dwMachineNumber As Int32) As Int32
        End Function
        Public Shared Function GetBackupNumber(dwMachineNumber As Int32) As Int32
            Return _GetBackupNumber(dwMachineNumber)
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GetProductCode(dwMachineNumber As Int32, ByRef lpszProductCode As IntPtr) As Byte
        End Function

        Public Shared Function GetProductCode(dwMachineNumber As Int32, ByRef lpszProductCode As String) As Boolean
            lpszProductCode = ""
            Dim addr_product_code As IntPtr = Marshal.StringToBSTR(lpszProductCode)

            Try
                Dim ret As Byte = _GetProductCode(dwMachineNumber, addr_product_code)
                lpszProductCode = Marshal.PtrToStringBSTR(addr_product_code)
                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeBSTR(addr_product_code)
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _ClearKeeperData(dwMachineNumber As Int32) As Byte
        End Function

        Public Shared Function ClearKeeperData(dwMachineNumber As Int32) As Boolean
            Return _ClearKeeperData(dwMachineNumber) > 0
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _EmptyEnrollData(dwMachineNumber As Int32) As Byte
        End Function

        Public Shared Function EmptyEnrollData(dwMachineNumber As Int32) As Boolean
            Return _EmptyEnrollData(dwMachineNumber) > 0
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _EmptyGeneralLogData(dwMachineNumber As Int32) As Byte
        End Function

        Public Shared Function EmptyGeneralLogData(dwMachineNumber As Int32) As Boolean
            Return _EmptyGeneralLogData(dwMachineNumber) > 0
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _EmptySuperLogData(dwMachineNumber As Int32) As Byte
        End Function

        Public Shared Function EmptySuperLogData(dwMachineNumber As Int32) As Boolean
            Return _EmptySuperLogData(dwMachineNumber) > 0
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GetUserName(DeviceKind As Int32, dwMachineNumber As Int32, dwEnrollNumber As Int32, dwEMachineNumber As Int32, ByRef lpszUserName As IntPtr) As Byte
        End Function

        Public Shared Function GetUserName(DeviceKind As Int32, dwMachineNumber As Int32, dwEnrollNumber As Int32, dwEMachineNumber As Int32, ByRef lpszUserName As String) As Boolean
            Dim user_name As Byte() = New Byte(255) {}
            Dim userName As IntPtr
            lpszUserName = ""

            Try
                Dim ret As Byte = _GetUserName(DeviceKind, dwMachineNumber, dwEnrollNumber, dwEMachineNumber, userName)
                lpszUserName = Marshal.PtrToStringUni(userName)

                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GetUserName1(dwMachineNumber As Int32, dwEnrollNumber As Int32, ByRef lpszUserName As IntPtr) As Byte
        End Function

        Public Shared Function GetUserName1(dwMachineNumber As Int32, dwEnrollNumber As Int32, ByRef lpszUserName As String) As Boolean
            lpszUserName = ""
            Dim userName As IntPtr
            Try
                Dim ret As Byte = _GetUserName1(dwMachineNumber, dwEnrollNumber, userName)
                lpszUserName = Marshal.PtrToStringUni(userName)

                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _SetUserName(DeviceKind As Int32, dwMachineNumber As Int32, dwEnrollNumber As Int32, dwEMachineNumber As Int32, ByRef lpszUserName As IntPtr) As Byte
        End Function

        Public Shared Function SetUserName(DeviceKind As Int32, dwMachineNumber As Int32, dwEnrollNumber As Int32, dwEMachineNumber As Int32, lpszUserName As String) As Boolean
            If lpszUserName Is Nothing Then
                Return False
            End If

            Dim addr_user_name As IntPtr = Marshal.StringToHGlobalUni(lpszUserName)

            Try
                Return _SetUserName(DeviceKind, dwMachineNumber, dwEnrollNumber, dwEMachineNumber, addr_user_name) > 0
            Catch generatedExceptionName As Exception
                Return False
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _SetUserName1(dwMachineNumber As Int32, dwEnrollNumber As Int32, ByRef lpszUserName As IntPtr) As Byte
        End Function

        Public Shared Function SetUserName1(dwMachineNumber As Int32, dwEnrollNumber As Int32, lpszUserName As String) As Boolean
            If lpszUserName Is Nothing Then
                Return False
            End If

            Dim addr_user_name As IntPtr = Marshal.StringToHGlobalUni(lpszUserName)

            Try
                Return _SetUserName1(dwMachineNumber, dwEnrollNumber, addr_user_name) > 0
            Catch generatedExceptionName As Exception
                Return False
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GetCompanyName(dwMachineNumber As Int32, ByRef dwCompanyName As IntPtr) As Byte
        End Function

        Public Shared Function GetCompanyName(dwMachineNumber As Int32, ByRef dwCompanyName As String) As Boolean
            Dim company_name As Byte() = New Byte(255) {}
            Dim gh_company_name As GCHandle = GCHandle.Alloc(company_name, GCHandleType.Pinned)
            Dim addr_company_name As IntPtr = gh_company_name.AddrOfPinnedObject()
            dwCompanyName = ""

            Try
                Dim ret As Byte = _GetCompanyName(dwMachineNumber, addr_company_name)
                Dim index As Integer = Array.FindIndex(company_name, Function(c) c = 0)
                dwCompanyName = Encoding.Unicode.GetString(company_name, 0, index)

                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                gh_company_name.Free()
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GetCompanyName1(dwMachineNumber As Int32, ByRef dwCompanyName As IntPtr) As Byte
        End Function

        Public Shared Function GetCompanyName1(dwMachineNumber As Int32, ByRef dwCompanyName As String) As Boolean
            Dim company_name As Byte() = New Byte(255) {}
            Dim gh_company_name As GCHandle = GCHandle.Alloc(company_name, GCHandleType.Pinned)
            Dim addr_company_name As IntPtr = gh_company_name.AddrOfPinnedObject()
            dwCompanyName = ""

            Try
                Dim ret As Byte = _GetCompanyName1(dwMachineNumber, addr_company_name)
                Dim index As Integer = Array.FindIndex(company_name, Function(c) c = 0)
                dwCompanyName = Encoding.Unicode.GetString(company_name, 0, index)

                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                gh_company_name.Free()
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _SetCompanyName(dwMachineNumber As Int32, vKind As Int32, ByRef CompanyName As IntPtr) As Byte
        End Function

        Public Shared Function SetCompanyName(dwMachineNumber As Int32, vKind As Int32, CompanyName As String) As Boolean
            If CompanyName Is Nothing Then
                Return False
            End If

            Dim addr_name As IntPtr = Marshal.StringToHGlobalUni(CompanyName)

            Try
                Return _SetCompanyName(dwMachineNumber, vKind, addr_name) > 0
            Catch generatedExceptionName As Exception
                Return False
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _SetCompanyName1(dwMachineNumber As Int32, vKind As Int32, ByRef dwCompanyName As IntPtr) As Byte
        End Function

        Public Shared Function SetCompanyName1(dwMachineNumber As Int32, vKind As Int32, CompanyName As String) As Boolean
            If CompanyName Is Nothing Then
                Return False
            End If

            Dim temp_name As Byte() = Encoding.Unicode.GetBytes(CompanyName)
            Dim name As Byte() = New Byte(temp_name.Length) {}
            Array.Copy(temp_name, name, temp_name.Length)

            Dim gh_name As GCHandle = GCHandle.Alloc(name, GCHandleType.Pinned)
            Dim addr_name As IntPtr = gh_name.AddrOfPinnedObject()

            Try
                Return _SetCompanyName1(dwMachineNumber, vKind, addr_name) > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                gh_name.Free()
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GetDoorStatus(dwMachineNumber As Int32, dwValue As IntPtr) As Byte
        End Function

        Public Shared Function GetDoorStatus(dwMachineNumber As Int32, ByRef dwValue As Int32) As Boolean
            dwValue = 0

            Dim bin_dwValue As Byte() = New Byte(3) {}

            Dim gh_dwValue As GCHandle = GCHandle.Alloc(bin_dwValue, GCHandleType.Pinned)
            Try
                Dim addr_dwValue As IntPtr = gh_dwValue.AddrOfPinnedObject()

                Dim ret As Byte = _GetDoorStatus(dwMachineNumber, addr_dwValue)

                dwValue = New BinaryReader(New MemoryStream(bin_dwValue)).ReadInt32()

                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                gh_dwValue.Free()
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _SetDoorStatus(dwMachineNumber As Int32, dwValue As Int32) As Byte
        End Function
        Public Shared Function SetDoorStatus(dwMachineNumber As Int32, dwValue As Int32) As Boolean
            Try
                Return _SetDoorStatus(dwMachineNumber, dwValue) > 0
            Catch generatedExceptionName As Exception
                Return False
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GetBellTime(dwMachineNumber As Int32, dwValue As IntPtr, ByRef dwBellInfo As IntPtr) As Byte
        End Function

        Public Shared Function GetBellTime(dwMachineNumber As Int32, ByRef dwValue As Int32, dwBellInfo As IntPtr) As Boolean
            dwValue = 0

            Dim bin_dwValue As Byte() = New Byte(3) {}

            Dim gh_dwValue As GCHandle = GCHandle.Alloc(bin_dwValue, GCHandleType.Pinned)

            Dim addr_dwValue As IntPtr = gh_dwValue.AddrOfPinnedObject()

            Try
                Dim ret As Byte = _GetBellTime(dwMachineNumber, addr_dwValue, dwBellInfo)

                dwValue = New BinaryReader(New MemoryStream(bin_dwValue)).ReadInt32()

                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                gh_dwValue.Free()
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _SetBellTime(dwMachineNumber As Int32, dwValue As Int32, ByRef dwBellInfo As IntPtr) As Byte
        End Function

        Public Shared Function SetBellTime(dwMachineNumber As Int32, dwValue As Int32, dwBellInfo As IntPtr) As Boolean
            Try
                Return _SetBellTime(dwMachineNumber, dwValue, dwBellInfo) > 0
            Catch generatedExceptionName As Exception
                Return False
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _ConnectSerial(dwMachineNumber As Int32, dwCommPort As Int32, dwBaudRate As Int32) As Byte
        End Function
        Public Shared Function ConnectSerial(dwMachineNumber As Int32, dwCommPort As Int32, dwBaudRate As Int32) As Boolean
            Try
                Return _ConnectSerial(dwMachineNumber, dwCommPort, dwBaudRate) > 0
            Catch generatedExceptionName As System.Exception
                Return False
            End Try
        End Function
#End Region

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _ConnectTcpip(dwMachineNumber As Int32, ByRef lpszIPAddress As IntPtr, dwPortNumber As Int32, dwPassWord As Int32) As Byte
        End Function

        Public Shared Function ConnectTcpip(dwMachineNumber As Int32, lpszIPAddress As String, dwPortNumber As Int32, dwPassWord As Int32) As Boolean
            If lpszIPAddress Is Nothing Then
                Return False
            End If

            Dim string_in As IntPtr = Marshal.StringToBSTR(lpszIPAddress)
            Try
                Dim ret As Byte = _ConnectTcpip(dwMachineNumber, string_in, dwPortNumber, dwPassWord)
                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeBSTR(string_in)
            End Try
        End Function

#Region "Not Tested Part 3"
        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _ConnectP2p(ByRef lpszMachineID As IntPtr, ByRef lpszServerIPAddress As IntPtr, dwServerPortNumber As Int32, dwPassWord As Int32, pnError As IntPtr) As Int32
        End Function

        Public Shared Function ConnectP2p(lpszMachineID As String, lpszServerIPAddress As String, dwServerPortNumber As Int32, dwPassWord As Int32, ByRef pnError As Int32) As Int32
            pnError = 0

            If lpszServerIPAddress Is Nothing OrElse lpszMachineID Is Nothing Then
                Return 0
            End If

            Dim str_server_ip As IntPtr = Marshal.StringToHGlobalUni(lpszServerIPAddress)
            Dim str_machine_id As IntPtr = Marshal.StringToHGlobalUni(lpszMachineID)

            Dim [error] As Byte() = New Byte(3) {}
            Dim gh_error As GCHandle = GCHandle.Alloc([error], GCHandleType.Pinned)
            Dim ptr_error As IntPtr = gh_error.AddrOfPinnedObject()

            Try
                Dim ret As Int32 = _ConnectP2p(str_machine_id, str_server_ip, dwServerPortNumber, dwPassWord, ptr_error)

                pnError = New BinaryReader(New MemoryStream([error])).ReadInt32()

                Return ret
            Catch generatedExceptionName As Exception
                Return 0
            Finally
                Marshal.FreeHGlobal(str_server_ip)
                Marshal.FreeHGlobal(str_machine_id)
                gh_error.Free()
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _PrepareP2p(ByRef lpszMachineID As IntPtr, ByRef lpszServerIPAddress As IntPtr, dwServerPortNumber As Int32, pnStartYear As IntPtr, pnStartMonth As IntPtr, pnStartDay As IntPtr, _
            pnEndYear As IntPtr, pnEndMonth As IntPtr, pnEndDay As IntPtr, pnError As IntPtr) As Byte
        End Function

        Public Shared Function _PrepareP2p(lpszMachineID As String, lpszServerIPAddress As String, dwServerPortNumber As Int32, ByRef pnStartYear As Int32, ByRef pnStartMonth As Int32, ByRef pnStartDay As Int32, _
            ByRef pnEndYear As Int32, ByRef pnEndMonth As Int32, ByRef pnEndDay As Int32, ByRef pnError As Int32) As Boolean
            pnStartYear = 0
            pnStartMonth = 0
            pnStartDay = 0
            pnEndYear = 0
            pnEndMonth = 0
            pnEndDay = 0
            pnError = 0

            If lpszServerIPAddress Is Nothing OrElse lpszMachineID Is Nothing Then
                Return False
            End If


            Dim str_server_ip As IntPtr = Marshal.StringToHGlobalUni(lpszServerIPAddress)
            Dim str_machine_id As IntPtr = Marshal.StringToHGlobalUni(lpszMachineID)

            Dim bin_StartYear As Byte() = New Byte(3) {}
            Dim bin_StartMonth As Byte() = New Byte(3) {}
            Dim bin_StartDay As Byte() = New Byte(3) {}
            Dim bin_EndYear As Byte() = New Byte(3) {}
            Dim bin_EndMonth As Byte() = New Byte(3) {}
            Dim bin_EndDay As Byte() = New Byte(3) {}
            Dim bin_Error As Byte() = New Byte(3) {}

            Dim gh_StartYear As GCHandle = GCHandle.Alloc(bin_StartYear, GCHandleType.Pinned)
            Dim gh_StartMonth As GCHandle = GCHandle.Alloc(bin_StartMonth, GCHandleType.Pinned)
            Dim gh_StartDay As GCHandle = GCHandle.Alloc(bin_StartDay, GCHandleType.Pinned)
            Dim gh_EndYear As GCHandle = GCHandle.Alloc(bin_EndYear, GCHandleType.Pinned)
            Dim gh_EndMonth As GCHandle = GCHandle.Alloc(bin_EndMonth, GCHandleType.Pinned)
            Dim gh_EndDay As GCHandle = GCHandle.Alloc(bin_EndDay, GCHandleType.Pinned)
            Dim gh_Error As GCHandle = GCHandle.Alloc(bin_Error, GCHandleType.Pinned)

            Dim ptr_StartYear As IntPtr = gh_StartYear.AddrOfPinnedObject()
            Dim ptr_StartMonth As IntPtr = gh_StartMonth.AddrOfPinnedObject()
            Dim ptr_StartDay As IntPtr = gh_StartDay.AddrOfPinnedObject()
            Dim ptr_EndYear As IntPtr = gh_EndYear.AddrOfPinnedObject()
            Dim ptr_EndMonth As IntPtr = gh_EndMonth.AddrOfPinnedObject()
            Dim ptr_EndDay As IntPtr = gh_EndDay.AddrOfPinnedObject()
            Dim ptr_Error As IntPtr = gh_Error.AddrOfPinnedObject()

            Try
                Dim ret As Byte = _PrepareP2p(str_machine_id, str_server_ip, dwServerPortNumber, ptr_StartYear, ptr_StartMonth, ptr_StartDay, _
                    ptr_EndYear, ptr_EndMonth, ptr_EndDay, ptr_Error)

                pnStartYear = New BinaryReader(New MemoryStream(bin_StartYear)).ReadInt32()
                pnStartMonth = New BinaryReader(New MemoryStream(bin_StartMonth)).ReadInt32()
                pnStartDay = New BinaryReader(New MemoryStream(bin_StartDay)).ReadInt32()
                pnEndYear = New BinaryReader(New MemoryStream(bin_EndYear)).ReadInt32()
                pnEndMonth = New BinaryReader(New MemoryStream(bin_EndMonth)).ReadInt32()
                pnEndDay = New BinaryReader(New MemoryStream(bin_EndDay)).ReadInt32()
                pnError = New BinaryReader(New MemoryStream(bin_Error)).ReadInt32()

                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeHGlobal(str_server_ip)
                Marshal.FreeHGlobal(str_machine_id)

                gh_StartYear.Free()
                gh_StartMonth.Free()
                gh_StartDay.Free()
                gh_EndYear.Free()
                gh_EndMonth.Free()
                gh_EndDay.Free()
                gh_Error.Free()
            End Try
        End Function
#End Region

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Sub _Disconnect(dwMachineNumber As Int32)
        End Sub
        Public Shared Sub Disconnect(dwMachineNumber As Int32)
            _Disconnect(dwMachineNumber)
        End Sub

#Region "Not Tested Part 4"
        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GetLastError(dwMachineNumber As Int32, dwErrorCode As IntPtr) As Byte
        End Function

        Public Shared Function GetLastError(dwMachineNumber As Int32, ByRef dwErrorCode As Int32) As Boolean
            dwErrorCode = 0

            Dim error_code As Byte() = New Byte(3) {}
            Dim gh As GCHandle = GCHandle.Alloc(error_code, GCHandleType.Pinned)
            Dim addr As IntPtr = gh.AddrOfPinnedObject()
            Try
                Dim ret As Byte = _GetLastError(dwMachineNumber, addr)
                dwErrorCode = New BinaryReader(New MemoryStream(error_code)).ReadInt32()

                Return ret > 0
            Catch generatedExceptionName As System.Exception
                Return False
            Finally
                gh.Free()
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GetDeviceLongInfo(dwMachineNumber As Int32, dwInfo As Int32, ByRef dwValue As IntPtr) As Byte
        End Function

        Public Shared Function GetDeviceLongInfo(dwMachineNumber As Int32, dwInfo As Int32, dwValue As IntPtr) As Boolean
            Try
                Return _GetDeviceLongInfo(dwMachineNumber, dwInfo, dwValue) > 0
            Catch generatedExceptionName As Exception
                Return False
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _SetDeviceLongInfo(dwMachineNumber As Int32, dwInfo As Int32, ByRef dwValue As IntPtr) As Byte
        End Function

        Public Shared Function SetDeviceLongInfo(dwMachineNumber As Int32, dwInfo As Int32, dwValue As IntPtr) As Boolean
            Try
                Return _SetDeviceLongInfo(dwMachineNumber, dwInfo, dwValue) > 0
            Catch generatedExceptionName As Exception
                Return False
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _ModifyDuressFP(dwMachineNumber As Int32, dwEnrollNumber As Int32, dwBackupNumber As Int32, dwDuressSetting As Int32) As Byte
        End Function
        Public Shared Function ModifyDuressFP(dwMachineNumber As Int32, dwEnrollNumber As Int32, dwBackupNumber As Int32, dwDuressSetting As Int32) As Boolean
            Try
                Return _ModifyDuressFP(dwMachineNumber, dwEnrollNumber, dwBackupNumber, dwDuressSetting) > 0
            Catch generatedExceptionName As Exception
                Return False
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GetMachineIP(lpszProduct As IntPtr, dwMachineNumber As Int32, lpszIPBuf As IntPtr) As Byte
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GetDepartName(dwMachineNumber As Int32, dwDepartNumber As Int32, dwDepartOrDaigong As Int32, ByRef lpszDepartName As IntPtr) As Byte
        End Function
        Public Shared Function GetDepartName(dwMachineNumber As Int32, dwDepartNumber As Int32, dwDepartOrDaigong As Int32, ByRef lpszDepartName As String) As Boolean
            lpszDepartName = ""
            Dim addr_depart_name As IntPtr = Marshal.StringToBSTR(lpszDepartName)

            Try
                Dim ret As Byte = _GetDepartName(dwMachineNumber, dwDepartNumber, dwDepartOrDaigong, addr_depart_name)
                lpszDepartName = Marshal.PtrToStringBSTR(addr_depart_name)
                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeBSTR(addr_depart_name)
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _SetDepartName(dwMachineNumber As Int32, dwDepartNumber As Int32, dwDepartOrDaigong As Int32, ByRef lpszDepartName As IntPtr) As Byte
        End Function

        Public Shared Function SetDepartName(dwMachineNumber As Int32, dwDepartNumber As Int32, dwDepartOrDaigong As Int32, lpszDepartName As String) As Boolean
            If lpszDepartName Is Nothing Then
                Return False
            End If

            Dim temp_name As Byte() = Encoding.Unicode.GetBytes(lpszDepartName)
            Dim name As Byte() = New Byte(temp_name.Length) {}
            Array.Copy(temp_name, name, temp_name.Length)

            Dim gh_name As GCHandle = GCHandle.Alloc(name, GCHandleType.Pinned)
            Dim addr_name As IntPtr = gh_name.AddrOfPinnedObject()

            Try
                Return _SetDepartName(dwMachineNumber, dwDepartNumber, dwDepartOrDaigong, addr_name) > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                gh_name.Free()
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _GeneralOperationXML(dwMachineNumber As Int32, ByRef lpszReqNResXML As IntPtr) As Byte
        End Function

        Public Shared Function GeneralOperationXML(dwMachineNumber As Int32, ByRef lpszReqNResXML As String) As Boolean
            Dim string_in As IntPtr = Marshal.StringToBSTR(lpszReqNResXML)

            Try
                Dim ret As Byte = _GeneralOperationXML(dwMachineNumber, string_in)
                lpszReqNResXML = Marshal.PtrToStringBSTR(string_in)
                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeBSTR(string_in)
            End Try
        End Function

        'xml helper interfaces
        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _XML_ParseInt(ByRef lpszXML As IntPtr, lpszTagname As IntPtr) As Int32
        End Function
        Public Shared Function XML_ParseInt(ByRef lpszXML As String, lpszTagname As String) As Int32
            ' TODO KGH
            Dim string_in As IntPtr = Marshal.StringToBSTR(lpszXML)
            Dim tag As IntPtr = Marshal.StringToBSTR(lpszTagname)

            Try
                Return _XML_ParseInt(string_in, tag)
            Catch generatedExceptionName As Exception
                Return -1
            Finally
                Marshal.FreeBSTR(string_in)
                Marshal.FreeBSTR(tag)
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _XML_ParseLong(ByRef lpszXML As IntPtr, lpszTagname As IntPtr) As Int32
        End Function
        Public Shared Function XML_ParseLong(ByRef lpszXML As String, lpszTagname As String) As Int32
            ' TODO KGH
            Dim string_in As IntPtr = Marshal.StringToBSTR(lpszXML)
            Dim tag As IntPtr = Marshal.StringToBSTR(lpszTagname)

            Try
                Return _XML_ParseLong(string_in, tag)
            Catch generatedExceptionName As Exception
                Return -1
            Finally
                Marshal.FreeBSTR(string_in)
                Marshal.FreeBSTR(tag)
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _XML_ParseBoolean(ByRef lpszXML As IntPtr, lpszTagname As IntPtr) As Byte
        End Function
        Public Shared Function _XML_ParseBoolean(ByRef lpszXML As String, lpszTagname As String) As Boolean
            ' TODO KGH
            Dim string_in As IntPtr = Marshal.StringToBSTR(lpszXML)
            Dim tag As IntPtr = Marshal.StringToBSTR(lpszTagname)

            Try
                Return _XML_ParseBoolean(string_in, tag) > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeBSTR(string_in)
                Marshal.FreeBSTR(tag)
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _XML_ParseString(ByRef lpszXML As IntPtr, lpszTagname As IntPtr, ByRef lpszValue As IntPtr) As Byte
        End Function
        Public Shared Function XML_ParseString(ByRef lpszXML As String, lpszTagname As String, ByRef lpszValue As String) As Boolean
            ' TODO KGH
            Dim string_in As IntPtr = Marshal.StringToBSTR(lpszXML)
            Dim tag As IntPtr = Marshal.StringToBSTR(lpszTagname)
            lpszValue = ""
            Dim val As IntPtr = Marshal.StringToBSTR(lpszValue)

            Try
                Dim ret As Byte = _XML_ParseString(string_in, tag, val)
                lpszValue = Marshal.PtrToStringBSTR(val)
                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeBSTR(string_in)
                Marshal.FreeBSTR(tag)
                Marshal.FreeBSTR(val)
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _XML_ParseBinaryByte(ByRef lpszXML As IntPtr, lpszTagname As IntPtr, ByRef pData As IntPtr, dwLen As Int32) As Byte
        End Function
        Public Shared Function XML_ParseBinaryByte(ByRef lpszXML As String, lpszTagname As String, pData As IntPtr, dwLen As Int32) As Boolean
            ' TODO KGH
            Dim string_in As IntPtr = Marshal.StringToBSTR(lpszXML)
            Dim tag As IntPtr = Marshal.StringToBSTR(lpszTagname)

            Try
                Return _XML_ParseBinaryByte(string_in, tag, pData, dwLen) > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeBSTR(string_in)
                Marshal.FreeBSTR(tag)
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _XML_ParseBinaryWord(ByRef lpszXML As IntPtr, lpszTagname As IntPtr, ByRef pData As IntPtr, dwLen As Int32) As Byte
        End Function
        Public Shared Function XML_ParseBinaryWord(ByRef lpszXML As String, lpszTagname As String, pData As IntPtr, dwLen As Int32) As Boolean
            ' TODO KGH
            Dim string_in As IntPtr = Marshal.StringToBSTR(lpszXML)
            Dim tag As IntPtr = Marshal.StringToBSTR(lpszTagname)

            Try
                Return _XML_ParseBinaryWord(string_in, tag, pData, dwLen) > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeBSTR(string_in)
                Marshal.FreeBSTR(tag)
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _XML_ParseBinaryLong(ByRef lpszXML As IntPtr, lpszTagname As IntPtr, ByRef pData As IntPtr, dwLen As Int32) As Byte
        End Function
        Public Shared Function XML_ParseBinaryLong(ByRef lpszXML As String, lpszTagname As String, pData As IntPtr, dwLen As Int32) As Boolean
            ' TODO KGH
            Dim string_in As IntPtr = Marshal.StringToBSTR(lpszXML)
            Dim tag As IntPtr = Marshal.StringToBSTR(lpszTagname)

            Try
                Return _XML_ParseBinaryLong(string_in, tag, pData, dwLen) > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeBSTR(string_in)
                Marshal.FreeBSTR(tag)
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _XML_ParseBinaryUnicode(ByRef lpszXML As IntPtr, lpszTagname As IntPtr, ByRef pData As IntPtr, dwLen As Int32) As Byte
        End Function
        Public Shared Function XML_ParseBinaryUnicode(ByRef lpszXML As String, lpszTagname As String, ByRef pData As String, dwLen As Int32) As Boolean
            ' TODO KGH
            Dim string_in As IntPtr = Marshal.StringToBSTR(lpszXML)
            Dim tag As IntPtr = Marshal.StringToBSTR(lpszTagname)
            pData = ""
            Dim val As IntPtr = Marshal.StringToBSTR(pData)
            Try
                Dim ret As Byte = _XML_ParseBinaryUnicode(string_in, tag, val, dwLen)
                pData = Marshal.PtrToStringBSTR(val)
                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeBSTR(string_in)
                Marshal.FreeBSTR(tag)
                Marshal.FreeBSTR(val)
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _XML_ParseMultiUnicode(ByRef lpszXML As IntPtr, lpszTagname As IntPtr, ByRef pData As IntPtr, dwLen As Int32) As Byte
        End Function
        Public Shared Function XML_ParseMultiUnicode(ByRef lpszXML As String, lpszTagname As String, ByRef pData As String, dwLen As Int32) As Boolean
            ' TODO KGH
            Dim string_in As IntPtr = Marshal.StringToBSTR(lpszXML)
            Dim tag As IntPtr = Marshal.StringToBSTR(lpszTagname)
            pData = ""
            Dim val As IntPtr = Marshal.StringToBSTR(pData)
            Try
                Dim ret As Byte = _XML_ParseMultiUnicode(string_in, tag, val, dwLen)
                pData = Marshal.PtrToStringBSTR(val)
                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeBSTR(string_in)
                Marshal.FreeBSTR(tag)
                Marshal.FreeBSTR(val)
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _XML_AddInt(ByRef lpszXML As IntPtr, lpszTagname As IntPtr, nValue As Integer) As Byte
        End Function
        Public Shared Function XML_AddInt(ByRef lpszXML As String, lpszTagname As String, nValue As Integer) As Boolean
            ' TODO KGH
            Dim string_in As IntPtr = Marshal.StringToBSTR(lpszXML)
            Dim tag As IntPtr = Marshal.StringToBSTR(lpszTagname)
            Try
                Dim ret As Byte = _XML_AddInt(string_in, tag, nValue)
                lpszXML = Marshal.PtrToStringBSTR(string_in)
                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeBSTR(string_in)
                Marshal.FreeBSTR(tag)
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _XML_AddLong(ByRef lpszXML As IntPtr, lpszTagname As IntPtr, dwValue As Int32) As Byte
        End Function
        Public Shared Function XML_AddLong(ByRef lpszXML As String, lpszTagname As String, dwValue As Int32) As Boolean
            ' TODO KGH
            Dim string_in As IntPtr = Marshal.StringToBSTR(lpszXML)
            Dim tag As IntPtr = Marshal.StringToBSTR(lpszTagname)
            Try
                Dim ret As Byte = _XML_AddLong(string_in, tag, dwValue)
                lpszXML = Marshal.PtrToStringBSTR(string_in)
                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeBSTR(string_in)
                Marshal.FreeBSTR(tag)
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _XML_AddBoolean(ByRef lpszXML As IntPtr, lpszTagname As IntPtr, bValue As Byte) As Byte
        End Function
        Public Shared Function XML_AddBoolean(ByRef lpszXML As String, lpszTagname As String, bValue As Boolean) As Boolean
            ' TODO KGH
            Dim string_in As IntPtr = Marshal.StringToBSTR(lpszXML)
            Dim tag As IntPtr = Marshal.StringToBSTR(lpszTagname)
            Try
                Dim ret As Byte = _XML_AddBoolean(string_in, tag, If(bValue, CByte(1), CByte(0)))
                lpszXML = Marshal.PtrToStringBSTR(string_in)
                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeBSTR(string_in)
                Marshal.FreeBSTR(tag)
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _XML_AddString(ByRef lpszXML As IntPtr, lpszTagname As IntPtr, lpszValue As IntPtr) As Byte
        End Function
        Public Shared Function XML_AddString(ByRef lpszXML As String, lpszTagname As String, lpszValue As String) As Boolean
            ' TODO KGH
            Dim string_in As IntPtr = Marshal.StringToBSTR(lpszXML)
            Dim tag As IntPtr = Marshal.StringToBSTR(lpszTagname)
            Dim value As IntPtr = Marshal.StringToHGlobalUni(lpszValue)
            Try
                Dim ret As Byte = _XML_AddString(string_in, tag, value)
                lpszXML = Marshal.PtrToStringBSTR(string_in)
                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeBSTR(string_in)
                Marshal.FreeBSTR(tag)
                Marshal.FreeHGlobal(value)
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _XML_AddBinaryByte(ByRef lpszXML As IntPtr, lpszTagname As IntPtr, ByRef dwData As IntPtr, dwLen As Int32) As Byte
        End Function
        Public Shared Function XML_AddBinaryByte(ByRef lpszXML As String, lpszTagname As String, dwData As IntPtr, dwLen As Int32) As Boolean
            ' TODO KGH
            Dim string_in As IntPtr = Marshal.StringToBSTR(lpszXML)
            Dim tag As IntPtr = Marshal.StringToBSTR(lpszTagname)
            Try
                Dim ret As Byte = _XML_AddBinaryByte(string_in, tag, dwData, dwLen)
                lpszXML = Marshal.PtrToStringBSTR(string_in)
                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeBSTR(string_in)
                Marshal.FreeBSTR(tag)
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _XML_AddBinaryWord(ByRef lpszXML As IntPtr, lpszTagname As IntPtr, ByRef dwData As IntPtr, dwLen As Int32) As Byte
        End Function
        Public Shared Function XML_AddBinaryWord(ByRef lpszXML As String, lpszTagname As String, dwData As IntPtr, dwLen As Int32) As Boolean
            ' TODO KGH
            Dim string_in As IntPtr = Marshal.StringToBSTR(lpszXML)
            Dim tag As IntPtr = Marshal.StringToBSTR(lpszTagname)
            Try
                Dim ret As Byte = _XML_AddBinaryWord(string_in, tag, dwData, dwLen)
                lpszXML = Marshal.PtrToStringBSTR(string_in)
                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeBSTR(string_in)
                Marshal.FreeBSTR(tag)
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _XML_AddBinaryLong(ByRef lpszXML As IntPtr, lpszTagname As IntPtr, ByRef dwData As IntPtr, dwLen As Int32) As Byte
        End Function
        Public Shared Function XML_AddBinaryLong(ByRef lpszXML As String, lpszTagname As String, dwData As IntPtr, dwLen As Int32) As Boolean
            ' TODO KGH
            Dim string_in As IntPtr = Marshal.StringToBSTR(lpszXML)
            Dim tag As IntPtr = Marshal.StringToBSTR(lpszTagname)
            Try
                Dim ret As Byte = _XML_AddBinaryLong(string_in, tag, dwData, dwLen)
                lpszXML = Marshal.PtrToStringBSTR(string_in)
                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeBSTR(string_in)
                Marshal.FreeBSTR(tag)
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _XML_AddBinaryUnicode(ByRef lpszXML As IntPtr, lpszTagname As IntPtr, lpszData As IntPtr) As Byte
        End Function
        Public Shared Function XML_AddBinaryUnicode(ByRef lpszXML As String, lpszTagname As String, lpszData As String) As Boolean
            ' TODO KGH
            Dim string_in As IntPtr = Marshal.StringToBSTR(lpszXML)
            Dim tag As IntPtr = Marshal.StringToBSTR(lpszTagname)
            Dim val As IntPtr = Marshal.StringToHGlobalUni(lpszData)
            Try
                Dim ret As Byte = _XML_AddBinaryUnicode(string_in, tag, val)
                lpszXML = Marshal.PtrToStringBSTR(string_in)
                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeBSTR(string_in)
                Marshal.FreeBSTR(tag)
                Marshal.FreeHGlobal(val)
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _XML_AddBinaryGlyph(ByRef lpszXML As IntPtr, lpszMessage As IntPtr, width As Int32, height As Int32, lpszFontDescriptor As IntPtr) As Byte
        End Function
        Public Shared Function XML_AddBinaryGlyph(ByRef lpszXML As String, lpszMessage As String, width As Int32, height As Int32, lpszFontDescriptor As String) As Boolean
            ' TODO KGH
            Dim string_in As IntPtr = Marshal.StringToBSTR(lpszXML)
            Dim msg As IntPtr = Marshal.StringToBSTR(lpszMessage)
            Dim font As IntPtr = Marshal.StringToHGlobalUni(lpszFontDescriptor)
            Try
                Dim ret As Byte = _XML_AddBinaryGlyph(string_in, msg, width, height, font)
                lpszXML = Marshal.PtrToStringBSTR(string_in)
                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeBSTR(string_in)
                Marshal.FreeBSTR(msg)
                Marshal.FreeHGlobal(font)
            End Try
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _XML_AddBinaryNameGlyph(dwMachineNumber As Int32, ByRef lpszXML As IntPtr, lpszName As IntPtr) As Byte
        End Function
        Public Shared Function XML_AddBinaryNameGlyph(dwMachineNumber As Int32, ByRef lpszXML As String, lpszName As String) As Boolean
            ' TODO KGH
            Dim string_in As IntPtr = Marshal.StringToBSTR(lpszXML)
            Dim name As IntPtr = Marshal.StringToBSTR(lpszName)
            Try
                Dim ret As Byte = _XML_AddBinaryNameGlyph(dwMachineNumber, string_in, name)
                lpszXML = Marshal.PtrToStringBSTR(string_in)
                Return ret > 0
            Catch generatedExceptionName As Exception
                Return False
            Finally
                Marshal.FreeBSTR(string_in)
                Marshal.FreeBSTR(name)
            End Try
        End Function
#End Region

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Sub _DisconnectAll()
        End Sub
        Public Shared Sub DisconnectAll()
            _DisconnectAll()
        End Sub

        Public Delegate Function TransceiveCallback(dwCurrent As Int32, dwTotal As Int32) As Byte
        Shared transceive_call_back As TransceiveCallback = AddressOf DisabledTransceiveCallback

        Private Shared Function DisabledTransceiveCallback(dwCurrent As Int32, dwTotal As Int32) As Byte
            Return 1
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Public Shared Sub _SetTranseiveCallback(callback As TransceiveCallback)
        End Sub

        Public Shared Sub _DisableTranseiveCallback()
            Try
                _SetTranseiveCallback(transceive_call_back)

            Catch generatedExceptionName As Exception
            Finally
            End Try
        End Sub

        Public Delegate Sub EventCallback_Core(context As IntPtr, xml As IntPtr)

        Private Shared Sub OnEvent(context As IntPtr, xml As IntPtr)
            If event_callback Is Nothing Then
                Return
            End If

            event_callback(Marshal.PtrToStringAnsi(xml))
        End Sub

        Private Shared core_event_callback As EventCallback_Core = AddressOf OnEvent

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _StartEventCapture(dwCommType As Int32, dwParam1 As Int32, dwParam2 As Int32, cb As EventCallback_Core, context As IntPtr) As Byte
        End Function

        Public Delegate Sub EventCallback(eventXML As String)
        Private Shared event_callback As EventCallback = Nothing
        Public Shared Function StartEventCapture(dwCommType As Int32, dwParam1 As Int32, dwParam2 As Int32, cb As EventCallback, context As IntPtr) As Boolean
            event_callback = cb
            Return _StartEventCapture(dwCommType, dwParam1, dwParam2, core_event_callback, context) > 0
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Sub _StopEventCapture()
        End Sub
        Public Shared Sub StopEventCapture()
            _StopEventCapture()
        End Sub

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _ReadGLogWithPos(dwMachineNumber As Int32, dwStartPos As Int32, dwEndPos As Int32) As Boolean
        End Function
        Public Shared Function ReadGLogWithPos(dwMachineNumber As Integer, dwStartPos As Integer, dwEndPos As Integer) As Boolean
            Return _ReadGLogWithPos(dwMachineNumber, dwStartPos, dwEndPos)
        End Function

        <DllImport("SBXPCDLL.dll", CallingConvention:=CallingConvention.Winapi)> _
        Private Shared Function _ReadSLogWithPos(dwMachineNumber As Int32, dwStartPos As Int32, dwEndPos As Int32) As Boolean
        End Function
        Public Shared Function ReadSLogWithPos(dwMachineNumber As Integer, dwStartPos As Integer, dwEndPos As Integer) As Boolean
            Return _ReadSLogWithPos(dwMachineNumber, dwStartPos, dwEndPos)
        End Function
    End Class
End Namespace
