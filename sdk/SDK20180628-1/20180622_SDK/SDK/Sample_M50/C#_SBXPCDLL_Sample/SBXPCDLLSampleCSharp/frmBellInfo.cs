using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace SBXPCDLLSampleCSharp
{
    public partial class frmBellInfo : Form
    {
        public frmBellInfo()
        {
            InitializeComponent();
        }

        int mBellCount;
        const int MAX_BELLCOUNT_DAY = 24;
        const int dataLen = MAX_BELLCOUNT_DAY * 3;
        CheckBox[] chkValid;
        TextBox[] txtHour;
        TextBox[] txtMinute;
        Byte[] dbBellInfoList;

        private void BellInfoInit()
        {
            txtHour = new TextBox[MAX_BELLCOUNT_DAY];
            txtMinute = new TextBox[MAX_BELLCOUNT_DAY];
            chkValid = new CheckBox[MAX_BELLCOUNT_DAY];
            dbBellInfoList = new Byte[dataLen];

            txtHour[0] = _txtHour_0;
            txtHour[1] = _txtHour_1;
            txtHour[2] = _txtHour_2;
            txtHour[3] = _txtHour_3;
            txtHour[4] = _txtHour_4;
            txtHour[5] = _txtHour_5;
            txtHour[6] = _txtHour_6;
            txtHour[7] = _txtHour_7;
            txtHour[8] = _txtHour_8;
            txtHour[9] = _txtHour_9;
            txtHour[10] = _txtHour_10;
            txtHour[11] = _txtHour_11;
            txtHour[12] = _txtHour_12;
            txtHour[13] = _txtHour_13;
            txtHour[14] = _txtHour_14;
            txtHour[15] = _txtHour_15;
            txtHour[16] = _txtHour_16;
            txtHour[17] = _txtHour_17;
            txtHour[18] = _txtHour_18;
            txtHour[19] = _txtHour_19;
            txtHour[20] = _txtHour_20;
            txtHour[21] = _txtHour_21;
            txtHour[22] = _txtHour_22;
            txtHour[23] = _txtHour_23;

            txtMinute[0] = _txtMinute_0;
            txtMinute[1] = _txtMinute_1;
            txtMinute[2] = _txtMinute_2;
            txtMinute[3] = _txtMinute_3;
            txtMinute[4] = _txtMinute_4;
            txtMinute[5] = _txtMinute_5;
            txtMinute[6] = _txtMinute_6;
            txtMinute[7] = _txtMinute_7;
            txtMinute[8] = _txtMinute_8;
            txtMinute[9] = _txtMinute_9;
            txtMinute[10] = _txtMinute_10;
            txtMinute[11] = _txtMinute_11;
            txtMinute[12] = _txtMinute_12;
            txtMinute[13] = _txtMinute_13;
            txtMinute[14] = _txtMinute_14;
            txtMinute[15] = _txtMinute_15;
            txtMinute[16] = _txtMinute_16;
            txtMinute[17] = _txtMinute_17;
            txtMinute[18] = _txtMinute_18;
            txtMinute[19] = _txtMinute_19;
            txtMinute[20] = _txtMinute_20;
            txtMinute[21] = _txtMinute_21;
            txtMinute[22] = _txtMinute_22;
            txtMinute[23] = _txtMinute_23;

            chkValid[0] = _chkValid_0;
            chkValid[1] = _chkValid_1;
            chkValid[2] = _chkValid_2;
            chkValid[3] = _chkValid_3;
            chkValid[4] = _chkValid_4;
            chkValid[5] = _chkValid_5;
            chkValid[6] = _chkValid_6;
            chkValid[7] = _chkValid_7;
            chkValid[8] = _chkValid_8;
            chkValid[9] = _chkValid_9;
            chkValid[10] = _chkValid_10;
            chkValid[11] = _chkValid_11;
            chkValid[12] = _chkValid_12;
            chkValid[13] = _chkValid_13;
            chkValid[14] = _chkValid_14;
            chkValid[15] = _chkValid_15;
            chkValid[16] = _chkValid_16;
            chkValid[17] = _chkValid_17;
            chkValid[18] = _chkValid_18;
            chkValid[19] = _chkValid_19;
            chkValid[20] = _chkValid_20;
            chkValid[21] = _chkValid_21;
            chkValid[22] = _chkValid_22;
            chkValid[23] = _chkValid_23;
        }

        private void ShowValue()
        {
            int i;

            for (i = 0; i < MAX_BELLCOUNT_DAY; i++)
            {
                if (dbBellInfoList[i] > 1)
                    dbBellInfoList[i] = 0;
                chkValid[i].Checked = dbBellInfoList[i] == 1;
                txtHour[i].Text = Convert.ToString(dbBellInfoList[i + MAX_BELLCOUNT_DAY]);
                txtMinute[i].Text = Convert.ToString(dbBellInfoList[i + MAX_BELLCOUNT_DAY * 2]);
            }
            txtBellCount.Text = Convert.ToString(mBellCount);
        }

        private Boolean GetValue()
        {
            int i;

            for (i = 0; i < MAX_BELLCOUNT_DAY; i++)
            {
                dbBellInfoList[i] = chkValid[i].Checked ? (byte)1 : (byte)0;
                if (Convert.ToInt32(txtHour[i].Text == "" ? "0" : txtHour[i].Text) < 0 ||
                    Convert.ToInt32(txtHour[i].Text == "" ? "0" : txtHour[i].Text) > 23)
                {
                    return false;
                }
                dbBellInfoList[i + MAX_BELLCOUNT_DAY] = Convert.ToByte(txtHour[i].Text == "" ? "0" : txtHour[i].Text);

                if (Convert.ToInt32(txtMinute[i].Text == "" ? "0" : txtMinute[i].Text) < 0 ||
                    Convert.ToInt32(txtMinute[i].Text == "" ? "0" : txtMinute[i].Text) > 59)
                {
                    return false;
                }
                dbBellInfoList[i + MAX_BELLCOUNT_DAY * 2] = Convert.ToByte(txtMinute[i].Text == "" ? "0" : txtMinute[i].Text);

            }
            mBellCount = Convert.ToInt32(txtBellCount.Text == "" ? "0" : txtBellCount.Text);

            return true;
        }

        private void cmdRead_Click(object sender, EventArgs e)
        {
            Boolean vRet = true;
            int vErrorCode = 0;

            lblMessage.Text = "Waiting...";
            Application.DoEvents();

            if (!sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0)) // 0 : false
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            GCHandle gh = GCHandle.Alloc(dbBellInfoList, GCHandleType.Pinned);
            IntPtr AddrOfmlngBellInfo = gh.AddrOfPinnedObject();

            vRet = sbxpc.SBXPCDLL.GetBellTime(Program.gMachineNumber, out mBellCount, AddrOfmlngBellInfo);

            if (vRet)
            {
                ShowValue();
                lblMessage.Text = "Success!";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            gh.Free();
            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
        }

        private void cmdWrite_Click(object sender, EventArgs e)
        {
            Boolean vRet;
            int vErrorCode = 0;

            lblMessage.Text = "Waiting...";
            Application.DoEvents();

            if (!sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0)) // 0 : false
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            if (!GetValue())
            {
                util.MessageBox(new IntPtr(0), "Invalid Value.", "Bell info", 1);
            }
            else
            {
                GCHandle gh = GCHandle.Alloc(dbBellInfoList, GCHandleType.Pinned);
                IntPtr AddrOfmlngBellInfo = gh.AddrOfPinnedObject();

                vRet = sbxpc.SBXPCDLL.SetBellTime(Program.gMachineNumber, mBellCount, AddrOfmlngBellInfo);
                gh.Free();

                if (vRet)
                {
                    ShowValue();
                    lblMessage.Text = "Success!";
                }
                else
                {
                    sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                    lblMessage.Text = util.ErrorPrint(vErrorCode);
                }
            }

            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
        }

        private void frmBellInfo_Load(object sender, EventArgs e)
        {
            BellInfoInit();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
            Application.OpenForms["frmMain"].Visible = true;
        }
    }
}
