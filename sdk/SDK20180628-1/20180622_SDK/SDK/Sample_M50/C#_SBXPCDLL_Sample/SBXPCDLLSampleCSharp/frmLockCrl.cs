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
    public partial class frmLockCtrl : Form
    {
        public frmLockCtrl()
        {
            InitializeComponent();
        }

        private void cmdGetDoorStatus_Click(object sender, EventArgs e)
        {
            int vValue = 0;
            int vErrorCode = 0;
            Boolean vRet;

            lblMessage.Text = "Working...";
            Application.DoEvents();

            vRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            vRet = sbxpc.SBXPCDLL.GetDoorStatus(Program.gMachineNumber, out vValue);
            if (vRet)
            {
                if (vValue == 1)
                    lblMessage.Text = "Uncond Door Open State!";
                else if (vValue == 2)
                    lblMessage.Text = "Uncond Door Close State!";
                else if (vValue == 3)
                    lblMessage.Text = "Door Open State!";
                else if (vValue == 4)
                    lblMessage.Text = "Auto Recover State!";
                else if (vValue == 5)
                    lblMessage.Text = "Door Close State!";
                else if (vValue == 6)
                    lblMessage.Text = "Watching for Close!";
                else if (vValue == 7)
                    lblMessage.Text = "Illegal open!";
                else
                    lblMessage.Text = "User State !";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
            Application.DoEvents();
        }

        private void cmdDoorOpen_Click(object sender, EventArgs e)
        {
            int vErrorCode = 0;
            Boolean vRet;

            lblMessage.Text = "Working...";
            Application.DoEvents();

            vRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            vRet = sbxpc.SBXPCDLL.SetDoorStatus(Program.gMachineNumber, 3);
            if (vRet)
            {
                lblMessage.Text = "Door Open Success!";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
            Application.DoEvents();
        }

        private void cmdAutoRecover_Click(object sender, EventArgs e)
        {
            int vErrorCode = 0;
            Boolean vRet;

            lblMessage.Text = "Working...";
            Application.DoEvents();

            vRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            vRet = sbxpc.SBXPCDLL.SetDoorStatus(Program.gMachineNumber, 4);
            if (vRet)
            {
                lblMessage.Text = "Auto Recover Success!";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
            Application.DoEvents();
        }

        private void cmdRestart_Click(object sender, EventArgs e)
        {
            int vErrorCode = 0;
            Boolean vRet;

            lblMessage.Text = "Working...";
            Application.DoEvents();

            vRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            vRet = sbxpc.SBXPCDLL.SetDoorStatus(Program.gMachineNumber, 5);
            if (vRet)
            {
                lblMessage.Text = "Reboot Success!";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
            Application.DoEvents();
        }

        private void cmdUncondOpen_Click(object sender, EventArgs e)
        {
            int vErrorCode = 0;
            Boolean vRet;

            lblMessage.Text = "Working...";
            Application.DoEvents();

            vRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            vRet = sbxpc.SBXPCDLL.SetDoorStatus(Program.gMachineNumber, 1);
            if (vRet)
            {
                lblMessage.Text = "Uncond Door Open Success!";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
            Application.DoEvents();
        }

        private void cmdUncondClose_Click(object sender, EventArgs e)
        {
            int vErrorCode = 0;
            Boolean vRet;

            lblMessage.Text = "Working...";
            Application.DoEvents();

            vRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            vRet = sbxpc.SBXPCDLL.SetDoorStatus(Program.gMachineNumber, 2);
            if (vRet)
            {
                lblMessage.Text = "Uncond Door Close Success!";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
            Application.DoEvents();
        }

        private void cmdWarnCancel_Click(object sender, EventArgs e)
        {
            int vErrorCode = 0;
            Boolean vRet;

            lblMessage.Text = "Working...";
            Application.DoEvents();

            vRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            vRet = sbxpc.SBXPCDLL.SetDoorStatus(Program.gMachineNumber, 6);
            if (vRet)
            {
                lblMessage.Text = "Warning cancel Success!";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
            Application.DoEvents();
        }

        private void frmLockCrl_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.OpenForms["frmMain"].Visible = true;
        }

        private void frmLockCrl_Load(object sender, EventArgs e)
        {
        }

        private void btnOpenExt_Click(object sender, EventArgs e)
        {
            int time = Convert.ToInt32(textDoorOpenTime.Text);
            if (time <= 0)
            {
                MessageBox.Show("Please input valid number!");
                return;
            }
            string strXML = null;
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "REQUEST", "OpenDoor");
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "MSGTYPE", "request");
            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "MachineID", Program.gMachineNumber);

            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "OpenTime", time);

            if (sbxpc.SBXPCDLL.GeneralOperationXML(Program.gMachineNumber, ref strXML))
            {
                lblMessage.Text = "Door open OK.";
            }
            else
            {
                lblMessage.Text = "Door open failed.";
            }
        }
    }
}
