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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        frmEvent frm_event = new frmEvent();

        Boolean mOpenFlag;
        private void cmdOpen_Click(object sender, EventArgs e)
        {
            String lpszIPAddress;
            //		Dim vRet As Boolean
    		
            Program.gMachineNumber = Convert.ToInt32(cmbMachineNumber.Text);
		    if (optNetworkDevice.Checked)
            {
               lpszIPAddress = txtIPAddress.Text;
               String lpszDevId = txtDevId.Text;
               if (lpszDevId.Length >= 6 && lpszDevId.Length <= 16)
                {
                    int nError = 0;
                    Program.gMachineNumber = sbxpc.SBXPCDLL.ConnectP2p(lpszDevId, lpszIPAddress, Convert.ToInt32(txtPortNo.Text), Convert.ToInt32(txtPassword.Text), out nError);
                    if (Program.gMachineNumber != 0)
                    {
                        mOpenFlag = true;
                        cmdOpen.Enabled = false;

                        cmdClose.Enabled = true;
                        cmdEnrollData.Enabled = true;
                        cmdLogData.Enabled = true;
                        cmdSystemInfo.Enabled = true;
                        cmdProuctCode.Enabled = true;
                        cmdBellInfo.Enabled = true;
                        cmdLockCtl.Enabled = true;
                        cmdAccessTz.Enabled = true;
                        cmdLogTz.Enabled = true;
                        cmdDepartment.Enabled = true;
                        cmdProxy.Enabled = true;
                        cmdTrString.Enabled = true;
                        btnLogServerSetting.Enabled = true;
                        
                        if (nError == 4)
                            MessageBox.Show("Relayed Connection!");
                        else if (nError == 5)
                            MessageBox.Show("Direct Local Connection!");
                    }
                    else
                    {
                        if (nError == 1)
                            MessageBox.Show("Cannot Connect To Server!");
                        else if (nError == 2)
                            MessageBox.Show("Device Not Found!");
                        else if (nError == 3)
                            MessageBox.Show("Password Mismatched!");
                        else
                            MessageBox.Show("Unknown Error!");
                    }
                }
                else
                {
                    if (sbxpc.SBXPCDLL.ConnectTcpip(Program.gMachineNumber, lpszIPAddress, Convert.ToInt32(txtPortNo.Text), Convert.ToInt32(txtPassword.Text)))
                    {
                        mOpenFlag = true;
                        cmdOpen.Enabled = false;

                        cmdClose.Enabled = true;
                        cmdEnrollData.Enabled = true;
                        cmdLogData.Enabled = true;
                        cmdSystemInfo.Enabled = true;
                        cmdProuctCode.Enabled = true;
                        cmdBellInfo.Enabled = true;
                        cmdLockCtl.Enabled = true;
                        cmdAccessTz.Enabled = true;
                        cmdLogTz.Enabled = true;
                        cmdDepartment.Enabled = true;
                        cmdProxy.Enabled = true;
                        cmdTrString.Enabled = true;
                        btnLogServerSetting.Enabled = true;
                    }
                }
		    }
		    if (optSerialDevice.Checked)
            {
			    if (sbxpc.SBXPCDLL.ConnectSerial(Program.gMachineNumber, cmbComPort.SelectedIndex + 1, Convert.ToInt32(cmbBaudrate.Text)))
                {
				    mOpenFlag = true;
				    cmdOpen.Enabled = false;
				    cmdClose.Enabled = true;
				    cmdEnrollData.Enabled = true;
				    cmdLogData.Enabled = true;
				    cmdSystemInfo.Enabled = true;
				    cmdProuctCode.Enabled = true;
				    cmdBellInfo.Enabled = true;
				    cmdLockCtl.Enabled = true;
                    cmdAccessTz.Enabled = true;
                    cmdLogTz.Enabled = true;
                    cmdDepartment.Enabled = true;
                    cmdProxy.Enabled = true;
                    cmdTrString.Enabled = true;
                    btnLogServerSetting.Enabled = true;
                }
		    }
            if (optUSBDevice.Checked)
            {
                if (sbxpc.SBXPCDLL.ConnectSerial(Program.gMachineNumber, 0, 0))
                {
                    mOpenFlag = true;
                    cmdOpen.Enabled = false;
                    cmdClose.Enabled = true;
                    cmdEnrollData.Enabled = true;
                    cmdLogData.Enabled = true;
                    cmdSystemInfo.Enabled = true;
                    cmdProuctCode.Enabled = true;
                    cmdBellInfo.Enabled = true;
                    cmdLockCtl.Enabled = true;
                    cmdAccessTz.Enabled = true;
                    cmdLogTz.Enabled = true;
                    cmdDepartment.Enabled = true;
                    cmdProxy.Enabled = true;
                    cmdTrString.Enabled = true;
                    btnLogServerSetting.Enabled = true;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            optSerialDevice.Checked = false;
            lblComPort.Enabled = false;
            cmbComPort.Enabled = false;
            lblBaudrate.Enabled = false;
            cmbBaudrate.Enabled = false;

            optNetworkDevice.Checked = true;
            lblIPAddress.Enabled = true;
            txtIPAddress.Enabled = true;
            lblPortNo.Enabled = true;
            txtPortNo.Enabled = true;
            lblPassword.Enabled = true;
            txtPassword.Enabled = true;

            cmdOpen.Enabled = true;
            cmdClose.Enabled = false;
            cmdEnrollData.Enabled = false;
            cmdLogData.Enabled = false;
            cmdSystemInfo.Enabled = false;
            cmdProuctCode.Enabled = false;
            cmdBellInfo.Enabled = false;
            cmdLockCtl.Enabled = false;
            cmdAccessTz.Enabled = false;
            cmdLogTz.Enabled = false;
            cmdDepartment.Enabled = false;
            cmdProxy.Enabled = false;
            cmdTrString.Enabled = false;
            btnLogServerSetting.Enabled = false;

            mOpenFlag = false;
            cmbMachineNumber.Text = Convert.ToString(1);
            cmbComPort.Text = Convert.ToString(1);
            cmbBaudrate.Text = "115200";

            sbxpc.SBXPCDLL.DotNET();
            sbxpc.SBXPCDLL._DisableTranseiveCallback();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            if (mOpenFlag == true)
            {
                sbxpc.SBXPCDLL.Disconnect(Program.gMachineNumber);
			    mOpenFlag = false;
			    cmdOpen.Enabled = true;
			    cmdClose.Enabled = false;
                cmdEnrollData.Enabled = false;
                cmdLogData.Enabled = false;
                cmdSystemInfo.Enabled = false;
                cmdProuctCode.Enabled = false;
                cmdBellInfo.Enabled = false;
                cmdLockCtl.Enabled = false;
                cmdAccessTz.Enabled = false;
                cmdLogTz.Enabled = false;
                cmdDepartment.Enabled = false;
                cmdProxy.Enabled = false;
                cmdTrString.Enabled = false;
                btnLogServerSetting.Enabled = false;
            }
        }

        private void optSerialDevice_CheckedChanged(object sender, EventArgs e)
        {
            if (optSerialDevice.Checked)
            {
                String lpszIPAddress;
                optSerialDevice.Checked = true;
                optNetworkDevice.Checked = false;
                optUSBDevice.Checked = false;

                if (optSerialDevice.Checked)
                {
                    lblComPort.Enabled = true;
                    cmbComPort.Enabled = true;
                    lblBaudrate.Enabled = true;
                    cmbBaudrate.Enabled = true;
                    lblIPAddress.Enabled = false;
                    txtIPAddress.Enabled = false;
                    lblPortNo.Enabled = false;
                    txtPortNo.Enabled = false;
                    lblPassword.Enabled = false;
                    txtPassword.Enabled = false;
                }
                else if (optNetworkDevice.Checked)
                {
                    lblComPort.Enabled = false;
                    cmbComPort.Enabled = false;
                    lblBaudrate.Enabled = false;
                    cmbBaudrate.Enabled = false;
                    lblIPAddress.Enabled = true;
                    txtIPAddress.Enabled = true;
                    lblPortNo.Enabled = true;
                    txtPortNo.Enabled = true;
                    lblPassword.Enabled = true;
                    txtPassword.Enabled = true;
                    lpszIPAddress = txtIPAddress.Text;
                }
                else
                {
                    lblComPort.Enabled = false;
                    cmbComPort.Enabled = false;
                    lblBaudrate.Enabled = false;
                    cmbBaudrate.Enabled = false;
                    lblIPAddress.Enabled = false;
                    txtIPAddress.Enabled = false;
                    lblPortNo.Enabled = false;
                    txtPortNo.Enabled = false;
                    lblPassword.Enabled = false;
                    txtPassword.Enabled = false;
                }
            }
        }

        private void optNetworkDevice_CheckedChanged(object sender, EventArgs e)
        {
            if (optNetworkDevice.Checked)
            {
                String lpszIPAddress;
                optSerialDevice.Checked = false;
                optNetworkDevice.Checked = true;
                optUSBDevice.Checked = false;

                if (optSerialDevice.Checked)
                {
                    lblComPort.Enabled = true;
                    cmbComPort.Enabled = true;
                    lblBaudrate.Enabled = true;
                    cmbBaudrate.Enabled = true;
                    lblIPAddress.Enabled = false;
                    txtIPAddress.Enabled = false;
                    lblPortNo.Enabled = false;
                    txtPortNo.Enabled = false;
                    lblPassword.Enabled = false;
                    txtPassword.Enabled = false;
                }
                else if (optNetworkDevice.Checked)
                {
                    lblComPort.Enabled = false;
                    cmbComPort.Enabled = false;
                    lblBaudrate.Enabled = false;
                    cmbBaudrate.Enabled = false;
                    lblIPAddress.Enabled = true;
                    txtIPAddress.Enabled = true;
                    lblPortNo.Enabled = true;
                    txtPortNo.Enabled = true;
                    lblPassword.Enabled = true;
                    txtPassword.Enabled = true;
                    lpszIPAddress = txtIPAddress.Text;
                }
                else
                {
                    lblComPort.Enabled = false;
                    cmbComPort.Enabled = false;
                    lblBaudrate.Enabled = false;
                    cmbBaudrate.Enabled = false;
                    lblIPAddress.Enabled = false;
                    txtIPAddress.Enabled = false;
                    lblPortNo.Enabled = false;
                    txtPortNo.Enabled = false;
                    lblPassword.Enabled = false;
                    txtPassword.Enabled = false;
                }
            }
        }

        private void optUSBDevice_CheckedChanged(object sender, EventArgs e)
        {
            if (optUSBDevice.Checked)
            {
                String lpszIPAddress;
                optSerialDevice.Checked = false;
                optNetworkDevice.Checked = false;
                optUSBDevice.Checked = true;

                if (optSerialDevice.Checked)
                {
                    lblComPort.Enabled = true;
                    cmbComPort.Enabled = true;
                    lblBaudrate.Enabled = true;
                    cmbBaudrate.Enabled = true;
                    lblIPAddress.Enabled = false;
                    txtIPAddress.Enabled = false;
                    lblPortNo.Enabled = false;
                    txtPortNo.Enabled = false;
                    lblPassword.Enabled = false;
                    txtPassword.Enabled = false;
                }
                else if (optNetworkDevice.Checked)
                {
                    lblComPort.Enabled = false;
                    cmbComPort.Enabled = false;
                    lblBaudrate.Enabled = false;
                    cmbBaudrate.Enabled = false;
                    lblIPAddress.Enabled = true;
                    txtIPAddress.Enabled = true;
                    lblPortNo.Enabled = true;
                    txtPortNo.Enabled = true;
                    lblPassword.Enabled = true;
                    txtPassword.Enabled = true;
                    lpszIPAddress = txtIPAddress.Text;
                }
                else
                {
                    lblComPort.Enabled = false;
                    cmbComPort.Enabled = false;
                    lblBaudrate.Enabled = false;
                    cmbBaudrate.Enabled = false;
                    lblIPAddress.Enabled = false;
                    txtIPAddress.Enabled = false;
                    lblPortNo.Enabled = false;
                    txtPortNo.Enabled = false;
                    lblPassword.Enabled = false;
                    txtPassword.Enabled = false;
                }
            }
        }

        private void cmdProuctCode_Click(object sender, EventArgs e)
        {
            frmPrtCode frm_prtcode = new frmPrtCode();
            frm_prtcode.Activate();
            frm_prtcode.Visible = true;
            this.Visible = false;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
           if (mOpenFlag) 
               sbxpc.SBXPCDLL.Disconnect(Program.gMachineNumber);
            Close();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void cmdSystemInfo_Click(object sender, EventArgs e)
        {
            frmSystemInfo frm_SystemInfo = new frmSystemInfo();
            frm_SystemInfo.Activate();
            frm_SystemInfo.Visible = true;
            this.Visible = false;
        }

        private void cmdLockCtl_Click(object sender, EventArgs e)
        {
            frmLockCtrl frm_lockctrl = new frmLockCtrl();
            frm_lockctrl.Activate();
            frm_lockctrl.Visible = true;
            this.Visible = false;
        }

        private void cmdBellInfo_Click(object sender, EventArgs e)
        {
            frmBellInfo frm_BellInfo = new frmBellInfo();
            frm_BellInfo.Activate();
            frm_BellInfo.Visible = true;
            this.Visible = false;
        }

        private void cmdLogData_Click(object sender, EventArgs e)
        {
            frmLog frm_log = new frmLog();
            frm_log.Activate();
            frm_log.Visible = true;
            this.Visible = false;
        }

        private void cmdEnrollData_Click(object sender, EventArgs e)
        {
            frmEnroll frm_enroll = new frmEnroll();
            frm_enroll.Activate();
            frm_enroll.Visible = true;
            this.Visible = false;
        }

        private void cmdDepartment_Click(object sender, EventArgs e)
        {
            frmDepartment frm_department = new frmDepartment();
            frm_department.Activate();
            frm_department.Visible = true;
            this.Visible = false;
        }

        private void cmdProxy_Click(object sender, EventArgs e)
        {
            frmDaigong frm_proxy = new frmDaigong();
            frm_proxy.Activate();
            frm_proxy.Visible = true;
            this.Visible = false;
        }

        private void cmdLogTz_Click(object sender, EventArgs e)
        {
            frmTrMode frm_trmode = new frmTrMode();
            frm_trmode.Activate();
            frm_trmode.Visible = true;
            this.Visible = false;
        }

        private void cmdAccessTz_Click(object sender, EventArgs e)
        {
            frmAccessTz frm_accesstz = new frmAccessTz();
            frm_accesstz.Activate();
            frm_accesstz.Visible = true;
            this.Visible = false;
        }

        private void cmdEventMoniter_Click(object sender, EventArgs e)
        {
            frm_event.Activate();
            frm_event.Visible = true;
            this.Visible = false;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            frm_event.Close();
        }

        bool entered_p2p_param = false;
        private void btnEnterP2PParam_Click(object sender, EventArgs e)
        {
            if (entered_p2p_param)
            {
                txtPortNo.Text = "5005";
                txtDevId.Text = "";
                entered_p2p_param = false;
                btnEnterP2PParam.Text = "Enter P2P Connect Param";
            }
            else
            {
                txtPortNo.Text = "4000";
                txtDevId.Text = "1916081282611010";
                entered_p2p_param = true;
                btnEnterP2PParam.Text = "Enter Direct Connect Param";
            }
        }

        private void btnCommParam_Click(object sender, EventArgs e)
        {
            frmCommParam frm_SetCommParam = new frmCommParam();
            frm_SetCommParam.Activate();
            frm_SetCommParam.Visible = true;
            this.Visible = false;
        }

        private void cmdTrString_Click(object sender, EventArgs e)
        {
            frmTrString frm = new frmTrString();
            frm.Activate();
            frm.Visible = true;
            this.Visible = false;
        }

        private void btnLogServerSetting_Click(object sender, EventArgs e)
        {
            frmLogServerSetting frm = new frmLogServerSetting();
            frm.Activate();
            frm.Visible = true;
            this.Visible = false;

        }
    }
}
