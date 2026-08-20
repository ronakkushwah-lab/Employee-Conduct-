using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;
using System.IO;

namespace SBXPCDLLSampleCSharp
{
    static class util
    {

//        Public Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Integer)
//        Public Declare Function GetSystemDefaultLangID Lib "kernel32" () As Short
        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
        public static extern void Sleep(int dwMilliseconds);

        public static string gstrNoDevice = "No Device";
        public static string gTempPhotoFile = "C:\\TempPhoto.jpg";
        public static int gCompressPhotoSize = 8192; // 8K

        public static short FK_Company = 1;
        public static short FK_328 = 101;
        public static short FK_338 = 102;
        public static short FK_528 = 201;
        public static short FK_526 = 202;

        public static string ErrorPrint(int aErrorCode)
        {
            switch (aErrorCode)
            {
                case 0:
                    return "SUCCESS";
                case 1:
                    return "ERR_COMPORT_ERROR";
                case 2:
                    return "ERR_WRITE_FAIL";
                case 3:
                    return "ERR_READ_FAIL";
                case 4:
                    return "ERR_INVALID_PARAM";
                case 5:
                    return "ERR_NON_CARRYOUT";
                case 6:
                    return "ERR_LOG_END";
                case 7:
                    return "ERR_MEMORY";
                case 8:
                    return "ERR_MULTIUSER";
                default:
                    return "NO_ERROR";
            }
            
        }
        public static int pubIPAddrToLong(string txtIP)
        {

            long dwIP;
            int k;
            string szTmp;
            k = txtIP.IndexOf('.');
            if (k == 0) 
                return 0;
            szTmp = txtIP.Substring(0, k);
            txtIP = txtIP.Remove(0, k + 1);
            dwIP = Convert.ToInt32(szTmp);

            k = txtIP.IndexOf('.');
            if (k == 0)
                return 0;
            szTmp = txtIP.Substring(0, k);
            txtIP = txtIP.Remove(0, k + 1);
            dwIP = dwIP * 256 + Convert.ToInt32(szTmp);
            k = txtIP.IndexOf('.');
            if (k == 0)
            {
                return 0;
            }
            szTmp = txtIP.Substring(0, k);
            txtIP = txtIP.Remove(0, k + 1);
            dwIP = dwIP * 256 + Convert.ToInt32(szTmp);
            dwIP = dwIP * 256 + Convert.ToInt32(txtIP);
            if (dwIP > 2147483647L) dwIP = dwIP - 2147483647L;
            return (int)dwIP;
        }
        public static string pubLongToIPAddr(int vValue)
        {
            string txtIP;
            int remain;

            remain = vValue % 256;
            if (remain < 0) remain = 256 + remain;
            txtIP = Convert.ToString(remain);
            vValue = (vValue - remain) / 256;
            remain = vValue % 256;
            if (remain < 0)  remain = 256 + remain;
            txtIP = Convert.ToString(remain) + "." + txtIP; 
            vValue = (vValue - remain) / 256;
            remain = vValue % 256;
            if (remain < 0)  remain = 256 + remain;
            txtIP = Convert.ToString(remain) + "." + txtIP;
            vValue = (vValue - remain) / 256;
            remain = vValue % 256;
            if (remain < 0)  remain = 256 + remain;
            txtIP = Convert.ToString(remain) + "." + txtIP;

            return txtIP;
        }

        public static bool MakeXMLRequestHeader(ref string strXML, string strRequest)
        {
            bool ret;
            ret = sbxpc.SBXPCDLL.XML_AddString(ref strXML, "REQUEST", strRequest);
            ret = sbxpc.SBXPCDLL.XML_AddString(ref strXML, "MSGTYPE", "request") && ret;
            ret = sbxpc.SBXPCDLL.XML_AddInt(ref strXML, "MachineID", Program.gMachineNumber) && ret;
            return ret;
        }

        public static bool SaveDeviceLongInfo(string fileName, int[] data)
        {
            FileStream FS = null;
            try
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FS = File.Create(fileName);

                Byte[] buf = new Byte[4 * data.Length];
                GCHandle gh = GCHandle.Alloc(data, GCHandleType.Pinned);
                IntPtr AddrOfdata = gh.AddrOfPinnedObject();

                Marshal.Copy(AddrOfdata, buf, 0, 4 * data.Length);

                FS.Write(buf, 0, buf.Length);
                return true;
            }
            catch (Exception)
            {
                return false;
            }finally
            {
                if(FS != null)
                {
                    FS.Close();
                    FS.Dispose();
                }
            }
        }

        public static bool LoadDeviceLongInfo(string fileName, int[] data)
        {
            FileStream FS = null;
            try
            {
                if (!File.Exists(fileName))
                    return false;
                FS = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                if (FS.Length != data.Length * 4)
                    return false;

                Byte[] buf = new Byte[data.Length * 4];
                FS.Read(buf, 0, buf.Length);

                GCHandle gh = GCHandle.Alloc(buf, GCHandleType.Pinned);
                IntPtr AddrOfdata = gh.AddrOfPinnedObject();

                Marshal.Copy(AddrOfdata, data, 0, data.Length);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (FS != null)
                {
                    FS.Close();
                    FS.Dispose();
                }
            }
        }
    }
}
