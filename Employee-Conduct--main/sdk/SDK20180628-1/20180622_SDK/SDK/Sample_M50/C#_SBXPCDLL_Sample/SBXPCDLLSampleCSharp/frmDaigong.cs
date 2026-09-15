using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SBXPCDLLSampleCSharp
{
    public partial class frmDaigong : Form
    {
        public frmDaigong()
        {
            InitializeComponent();
        }

        const int PROXY_COUNT = 7;
        string[] proxyNameList;

        private void DrawDepartmentList()
        {
            lstProxy.Items.Clear();
            string itemString;
            for (int i = 0; i < PROXY_COUNT; i++)
            {
                itemString = "[No.]" + String.Format("{0:D2}", i) + " ";
                itemString += "[Name]" + proxyNameList[i];
                lstProxy.Items.Add(itemString);
            }
        }

        private void lstDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtProxy.Text = proxyNameList[lstProxy.SelectedIndex];
            txtProxy.Focus();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            proxyNameList[lstProxy.SelectedIndex] = txtProxy.Text;
            DrawDepartmentList();
        }

        private void frmDaigong_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms["frmMain"].Visible = true;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdRead_Click(object sender, EventArgs e)
        {
            bool bRet;
            int vErrorCode = 0;

            lblMessage.Text = "Working...";
            Application.DoEvents();

            bRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0);
            if (!bRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            for (int i = 0; i < PROXY_COUNT; i++)
                bRet = sbxpc.SBXPCDLL.GetDepartName(Program.gMachineNumber, i + 1, 1, out proxyNameList[i]) && bRet;
            if (bRet)
                lblMessage.Text = "Success";
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            bRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1);

            DrawDepartmentList();
        }

        private void cmdWrite_Click(object sender, EventArgs e)
        {
            bool bRet;
            int vErrorCode = 0;

            lblMessage.Text = "Working...";
            Application.DoEvents();

            bRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0);
            if (!bRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            for (int i = 0; i < PROXY_COUNT; i++)
                bRet = sbxpc.SBXPCDLL.SetDepartName(Program.gMachineNumber, i + 1, 1, proxyNameList[i]) && bRet;
            if (bRet)
                lblMessage.Text = "Success";
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            bRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1);

            DrawDepartmentList();
        }

        private void frmDaigong_Load(object sender, EventArgs e)
        {
            proxyNameList = new string[PROXY_COUNT];
            DrawDepartmentList();
        }
    }
}
