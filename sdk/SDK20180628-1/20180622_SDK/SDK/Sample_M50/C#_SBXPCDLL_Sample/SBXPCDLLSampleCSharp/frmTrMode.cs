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
    public partial class frmTrMode : Form
    {
        public frmTrMode()
        {
            InitializeComponent();
        }

        const int DB_TRMODE_COUNT = 10;
        const int DB_TRMODE_BYTE_COUNT = 5;
        int[] trModeInfo;

        private void frmTrMode_Load(object sender, EventArgs e)
        {
            trModeInfo = new int[DB_TRMODE_COUNT * DB_TRMODE_BYTE_COUNT];
            InitTrMode();
            DrawTrModeList();
            dtStart.ShowUpDown = true;
            dtEnd.ShowUpDown = true;
        }

        private void InitTrMode()
        {
            for(int i = 0; i < DB_TRMODE_COUNT; i ++)
            {
                trModeInfo[i * DB_TRMODE_BYTE_COUNT] = 0;
                trModeInfo[i * DB_TRMODE_BYTE_COUNT + 1] = 0;
                trModeInfo[i * DB_TRMODE_BYTE_COUNT + 2] = 0;
                trModeInfo[i * DB_TRMODE_BYTE_COUNT + 3] = 0;
                trModeInfo[i * DB_TRMODE_BYTE_COUNT + 4] = 0;
            }
        }

        private void DrawTrModeList()
        {
            string itemString;
            lstTrMode.Items.Clear();
            for(int i = 0; i < DB_TRMODE_COUNT; i ++)
            {
                itemString = "[No.]" + String.Format("{0:D2}", i) + " ";
                itemString += "[S]";
                itemString += String.Format("{0:D2}", trModeInfo[i * DB_TRMODE_BYTE_COUNT + 1]) + ":";
                itemString += String.Format("{0:D2}", trModeInfo[i * DB_TRMODE_BYTE_COUNT + 2]) + " ";
                itemString += "[E]";
                itemString += String.Format("{0:D2}", trModeInfo[i * DB_TRMODE_BYTE_COUNT + 3]) + ":";
                itemString += String.Format("{0:D2}", trModeInfo[i * DB_TRMODE_BYTE_COUNT + 4]) + " ";
                itemString += cmbTrMode.Items[trModeInfo[i * DB_TRMODE_BYTE_COUNT]];
                lstTrMode.Items.Add(itemString);
            }
        }

        private void frmTrMode_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms["frmMain"].Visible =true;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lstTrMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTrMode.SelectedIndex == -1)
                return;

            int index       = lstTrMode.SelectedIndex * DB_TRMODE_BYTE_COUNT;
            int startHour   = trModeInfo[index + 1];
            int startMinute = trModeInfo[index + 2];
            int endHour     = trModeInfo[index + 3];
            int endMinute   = trModeInfo[index + 4];
            int trMode      = trModeInfo[index];

            dtStart.Value = new DateTime(2000, 1, 1, startHour, startMinute, 0);
            dtEnd.Value = new DateTime(2000, 1, 1, endHour, endMinute, 0);
            cmbTrMode.SelectedIndex = trMode;
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (lstTrMode.SelectedIndex == -1)
                return;

            int index = lstTrMode.SelectedIndex * DB_TRMODE_BYTE_COUNT; 
            trModeInfo[index + 1]   = dtStart.Value.Hour;
            trModeInfo[index + 2]   = dtStart.Value.Minute;
            trModeInfo[index + 3]   = dtEnd.Value.Hour;
            trModeInfo[index + 4]   = dtEnd.Value.Minute;
            trModeInfo[index]       = cmbTrMode.SelectedIndex;

            DrawTrModeList();
        }

        private void cmdRead_Click(object sender, EventArgs e)
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

            GCHandle gh = GCHandle.Alloc(trModeInfo, GCHandleType.Pinned);
            IntPtr AddrOftrModeInfo = gh.AddrOfPinnedObject();

            bRet = sbxpc.SBXPCDLL.GetDeviceLongInfo(Program.gMachineNumber, 5, AddrOftrModeInfo);

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

            DrawTrModeList();
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

            GCHandle gh = GCHandle.Alloc(trModeInfo, GCHandleType.Pinned);
            IntPtr AddrOftrModeInfo = gh.AddrOfPinnedObject();

            bRet = sbxpc.SBXPCDLL.SetDeviceLongInfo(Program.gMachineNumber, 5, AddrOftrModeInfo);

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

            DrawTrModeList();
        }

        private void cmdLoadFile_Click(object sender, EventArgs e)
        {
            DialogResult result = OpenFileDlg.ShowDialog();
            if (result == DialogResult.Cancel) return;

            if (!util.LoadDeviceLongInfo(OpenFileDlg.FileName, trModeInfo))
            {
                MessageBox.Show("Failed to open file : " + OpenFileDlg.FileName);
                return;
            }
            DrawTrModeList();
        }

        private void cmdSaveFile_Click(object sender, EventArgs e)
        {
            DialogResult result = OpenSaveDlg.ShowDialog();
            if (result == DialogResult.Cancel) return;

            if (!util.SaveDeviceLongInfo(OpenSaveDlg.FileName, trModeInfo))
            {
                MessageBox.Show("Unexpected Error");
                return;
            }
        }
    }
}
