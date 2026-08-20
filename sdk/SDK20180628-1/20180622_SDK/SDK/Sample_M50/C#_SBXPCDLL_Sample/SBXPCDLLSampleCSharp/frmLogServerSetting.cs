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
    public partial class frmLogServerSetting : Form
    {
        public frmLogServerSetting()
        {
            InitializeComponent();
        }

        private void btnGetDnsSettings_Click(object sender, EventArgs e)
        {
            string strXML = null;
            string strValue = "";
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "REQUEST", "GetLogServerSetting");
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "MSGTYPE", "request");
            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "MachineID", Program.gMachineNumber);

            if (sbxpc.SBXPCDLL.GeneralOperationXML(Program.gMachineNumber, ref strXML))
            {
                sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "ManagerPCDomainName", out strValue);
                txtServerDomainName.Text = strValue;

                textBgServerPort.Text = Convert.ToString(sbxpc.SBXPCDLL.XML_ParseLong(ref strXML, "ManagerPCPort"));

                MessageBox.Show("Get DNS Settings OK!");
            }
            else
            {
                MessageBox.Show("Get DNS Settings Failed.");
            }
        }

        private void btnSetDnsSettings_Click(object sender, EventArgs e)
        {

            string strXML = null;
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "REQUEST", "SetLogServerSetting");
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "MSGTYPE", "request");
            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "MachineID", Program.gMachineNumber);

            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "ManagerPCDomainName", txtServerDomainName.Text);
            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "ManagerPCPort", Convert.ToInt32(textBgServerPort.Text));

            if (sbxpc.SBXPCDLL.GeneralOperationXML(Program.gMachineNumber, ref strXML))
            {
                MessageBox.Show("Set DNS Settings OK!");
            }
            else
            {
                string str = "";
                sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "Result", out str);

                MessageBox.Show("Set DNS Settings Failed.\r\nResult:" + str);
            }
        }

        private void frmLogServerSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.OpenForms["frmMain"].Visible = true;
        }
    }
}
