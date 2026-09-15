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
    public partial class frmAccessTz : Form
    {
        public frmAccessTz()
        {
            InitializeComponent();
        }

        const int DBTIMEZONE_COUNT = 50;
        const int DBVMTIMEZONE_COUNT = 10;
        const int DBTIMESECTION_COUNT = 8;
        const int DBALLCOUNT = DBTIMEZONE_COUNT * DBTIMESECTION_COUNT;
        const int DBSYSTEMDEFAULT_VM = 5;

        int[] timeZoneInfo;
        private void frmAccessTz_Load(object sender, EventArgs e)
        {
            timeZoneInfo = new int[DBALLCOUNT * 5];
            dtStart.ShowUpDown = true;
            dtEnd.ShowUpDown = true;
            TimeZoneInit();
            DrawTimeZone();
        }

        private void TimeZoneInit()
        {
            for(int i = 0; i < DBTIMEZONE_COUNT; i ++)
            {
                for(int j = 0; j < DBTIMESECTION_COUNT; j ++)
                {
                    timeZoneInfo[(i * DBTIMESECTION_COUNT + j) * 5] = 0;
                    timeZoneInfo[(i * DBTIMESECTION_COUNT + j) * 5 + 1] = 0;
                    timeZoneInfo[(i * DBTIMESECTION_COUNT + j) * 5 + 2] = 23;
                    timeZoneInfo[(i * DBTIMESECTION_COUNT + j) * 5 + 3] = 59;
                    timeZoneInfo[(i * DBTIMESECTION_COUNT + j) * 5 + 4] = DBSYSTEMDEFAULT_VM;
                }

            }
        }

        private void DrawTimeZone()
        {
            string itemString = "";
            lstTimeZone.Items.Clear();
            int startHour, startMinute, endHour, endMinute, verifyMode;
            for (int i = 0; i < DBTIMEZONE_COUNT; i ++ )
            {
                for (int j = 0; j < DBTIMESECTION_COUNT; j ++ )
                {
                    startHour   = timeZoneInfo[(i * DBTIMESECTION_COUNT + j) * 5];
                    startMinute = timeZoneInfo[(i * DBTIMESECTION_COUNT + j) * 5 + 1];
                    endHour     = timeZoneInfo[(i * DBTIMESECTION_COUNT + j) * 5 + 2];
                    endMinute   = timeZoneInfo[(i * DBTIMESECTION_COUNT + j) * 5 + 3];
                    if (i < DBVMTIMEZONE_COUNT)
                        verifyMode = timeZoneInfo[(i * DBTIMESECTION_COUNT + j) * 5 + 4];
                    else
                        verifyMode = DBSYSTEMDEFAULT_VM;

                    itemString  = "[Tz.]" + String.Format("{0:D2}-{1:D1} ", i, j);
                    itemString += "[S]" + String.Format("{0:D2}:{1:D2} ", startHour, startMinute);
                    itemString += "[E]" + String.Format("{0:D2}:{1:D2}", endHour, endMinute) + " ";
                    itemString += "[VM]" + cmbVerifyMode.Items[verifyMode];
                    lstTimeZone.Items.Add(itemString);
                }
            }
        }

        private void cmdRead_Click(object sender, EventArgs e)
        {
            bool bRet;
            int vErrorCode = 0;

            lblMessage.Text = "Working...";
            Application.DoEvents();

            bRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : disable

            if(!bRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            GCHandle gh = GCHandle.Alloc(timeZoneInfo, GCHandleType.Pinned);
            IntPtr AddrOftimeZoneInfo = gh.AddrOfPinnedObject();

            bRet = sbxpc.SBXPCDLL.GetDeviceLongInfo(Program.gMachineNumber, 3, AddrOftimeZoneInfo);

            if(bRet)
            {
                lblMessage.Text = "Success!";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            bRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : enable
            DrawTimeZone();
        }

        private void frmAccessTz_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms["frmMain"].Visible = true;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdWrite_Click(object sender, EventArgs e)
        {
            bool bRet;
            int vErrorCode = 0;

            lblMessage.Text = "Working...";
            Application.DoEvents();

            bRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : disable

            if (!bRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            GCHandle gh = GCHandle.Alloc(timeZoneInfo, GCHandleType.Pinned);
            IntPtr AddrOftimeZoneInfo = gh.AddrOfPinnedObject();

            bRet = sbxpc.SBXPCDLL.SetDeviceLongInfo(Program.gMachineNumber, 3, AddrOftimeZoneInfo);

            if (bRet)
            {
                lblMessage.Text = "Success!";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            bRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : enable
            DrawTimeZone();
        }

        private void lstTimeZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTimeZone.SelectedIndex == -1) 
                return;

            int index = lstTimeZone.SelectedIndex;

            dtStart.Value = new DateTime(   2000, 1, 1,         // Don't care year/month/date
                                            timeZoneInfo[index * 5], 
                                            timeZoneInfo[index * 5 + 1], 
                                            0
                                        );
            dtEnd.Value = new DateTime(     2000, 1, 1,         // Don't care year/month/date
                                            timeZoneInfo[index * 5 + 2], 
                                            timeZoneInfo[index * 5 + 3], 
                                            0
                                      );
            if (index < DBVMTIMEZONE_COUNT * DBTIMESECTION_COUNT)
                cmbVerifyMode.SelectedIndex = timeZoneInfo[index * 5 + 4];
            else
                cmbVerifyMode.SelectedIndex = DBSYSTEMDEFAULT_VM;
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (lstTimeZone.SelectedIndex == -1)
                return;

            int index = lstTimeZone.SelectedIndex;

            timeZoneInfo[index * 5] = dtStart.Value.Hour;
            timeZoneInfo[index * 5 + 1] = dtStart.Value.Minute;
            timeZoneInfo[index * 5 + 2] = dtEnd.Value.Hour;
            timeZoneInfo[index * 5 + 3] = dtEnd.Value.Minute;
            if (index < DBVMTIMEZONE_COUNT * DBTIMESECTION_COUNT)
                timeZoneInfo[index * 5 + 4] = cmbVerifyMode.SelectedIndex;
            DrawTimeZone();
        }
    }
}
