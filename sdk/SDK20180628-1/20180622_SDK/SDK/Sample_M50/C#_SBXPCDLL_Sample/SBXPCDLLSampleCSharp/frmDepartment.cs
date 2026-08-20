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
    public partial class frmDepartment : Form
    {
        public frmDepartment()
        {
            InitializeComponent();
        }

        const int DEPT_COUNT = 20;
        string[] departmentNameList;

        private void frmDepartment_Load(object sender, EventArgs e)
        {
            departmentNameList = new string[DEPT_COUNT];
            DrawDepartmentList();
        }

        private void DrawDepartmentList()
        {
            lstDepartment.Items.Clear();
            string itemString;
            for (int i = 0; i < DEPT_COUNT; i++)
            {
                itemString = "[No.]" + String.Format("{0:D2}", i) + " ";
                itemString += "[Name]" + departmentNameList[i];
                lstDepartment.Items.Add(itemString);
            }
        }

        private void lstDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDeparmtmentName.Text = departmentNameList[lstDepartment.SelectedIndex];
            txtDeparmtmentName.Focus();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (lstDepartment.SelectedIndex == -1) return;
            departmentNameList[lstDepartment.SelectedIndex] = txtDeparmtmentName.Text;
            DrawDepartmentList();
        }

        private void frmDepartment_FormClosing(object sender, FormClosingEventArgs e)
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
            if(!bRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            for(int i = 0; i < DEPT_COUNT; i ++)
                bRet = sbxpc.SBXPCDLL.GetDepartName(Program.gMachineNumber, i, 0, out departmentNameList[i]) && bRet;
            if(bRet)
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

            for (int i = 0; i < DEPT_COUNT; i++)
                bRet = sbxpc.SBXPCDLL.SetDepartName(Program.gMachineNumber, i, 0, departmentNameList[i]) && bRet;
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
    }
}
