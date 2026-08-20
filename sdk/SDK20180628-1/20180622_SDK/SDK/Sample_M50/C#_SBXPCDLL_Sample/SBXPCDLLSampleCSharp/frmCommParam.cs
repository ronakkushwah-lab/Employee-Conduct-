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
    public partial class frmCommParam : Form
    {
        public frmCommParam()
        {
            InitializeComponent();
        }

        private void frmCommParam_Load(object sender, EventArgs e)
        {
            txtMachineID.Text = Convert.ToString(((frmMain)Application.OpenForms["frmMain"]).cmbMachineNumber.SelectedIndex + 1);
            txtIP.Text = ((frmMain)Application.OpenForms["frmMain"]).txtIPAddress.Text;
            txtPort.Text = ((frmMain)Application.OpenForms["frmMain"]).txtPortNo.Text;

            String[] event_types = {"NO", "TCP/IP", "RS485"};
            foreach (String t in event_types)
                cmbEventOutType.Items.Add(t);
            cmbEventOutType.SelectedIndex = 0;
        }

        private void frmCommParam_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.OpenForms["frmMain"].Visible = true;
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            string strXML = null;
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "REQUEST", "GetCommParam");
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "MSGTYPE", "request");
            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "MachineID", Program.gMachineNumber);

            if (sbxpc.SBXPCDLL.GeneralOperationXML(Program.gMachineNumber, ref strXML))
            {
                txtMachineID.Text = Convert.ToString(sbxpc.SBXPCDLL.XML_ParseLong(ref strXML, "MachineID"));
                chkUseDHCP.Checked = (sbxpc.SBXPCDLL.XML_ParseLong(ref strXML, "UseDHCP") == 1) ? true : false;
                txtIP.Text = IP_to_String(sbxpc.SBXPCDLL.XML_ParseLong(ref strXML, "IP"));
                txtPort.Text = Convert.ToString(sbxpc.SBXPCDLL.XML_ParseLong(ref strXML, "Port"));
                txtSubnetMask.Text = IP_to_String(sbxpc.SBXPCDLL.XML_ParseLong(ref strXML, "SubnetMask"));
                txtGateway.Text = IP_to_String(sbxpc.SBXPCDLL.XML_ParseLong(ref strXML, "Gateway"));
                cmbEventOutType.SelectedIndex = sbxpc.SBXPCDLL.XML_ParseLong(ref strXML, "EventOutType");
                txtServerIP.Text = IP_to_String(sbxpc.SBXPCDLL.XML_ParseLong(ref strXML, "ServerIP"));
                txtServerPort.Text = Convert.ToString(sbxpc.SBXPCDLL.XML_ParseLong(ref strXML, "ServerPort"));

                MessageBox.Show("Get Communication Parameters OK");
            }
            else
            {
                MessageBox.Show("Get Communication Parameters Failed.");
            }
        }
        private void btnSet_Click(object sender, EventArgs e)
        {
            if (IP_to_String(String_to_IP(txtIP.Text)) != txtIP.Text)
            {
                MessageBox.Show("Invalid IP.");
                return;
            }
            if (IP_to_String(String_to_IP(txtSubnetMask.Text)) != txtSubnetMask.Text)
            {
                MessageBox.Show("Invalid SubnetMask.");
                return;
            }
            if (IP_to_String(String_to_IP(txtGateway.Text)) != txtGateway.Text)
            {
                MessageBox.Show("Invalid Gateway.");
                return;
            }
            if (IP_to_String(String_to_IP(txtServerIP.Text)) != txtServerIP.Text)
            {
                MessageBox.Show("Invalid ServerIP.");
                return;
            }

            string strXML = null;
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "REQUEST", "SetCommParam");
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "MSGTYPE", "request");
            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "MachineID", Program.gMachineNumber);

            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "MachineID_New", Convert.ToInt32(txtMachineID.Text));
            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "UseDHCP", chkUseDHCP.Checked ? 1 : 0);
            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "IP", String_to_IP(txtIP.Text));
            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "Port", Convert.ToInt32(txtPort.Text));
            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "SubnetMask", String_to_IP(txtSubnetMask.Text));
            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "Gateway", String_to_IP(txtGateway.Text));
            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "EventOutType", cmbEventOutType.SelectedIndex);
            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "ServerIP", String_to_IP(txtServerIP.Text));
            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "ServerPort", Convert.ToInt32(txtServerPort.Text));

            if (sbxpc.SBXPCDLL.GeneralOperationXML(Program.gMachineNumber, ref strXML))
            {
//                 string str = "";
//                 sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "Result", out str);

                MessageBox.Show("Set Communication Parameters OK");
            }
            else
            {
                string str = "";
                sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "Result", out str);

                MessageBox.Show("Set Communication Parameters Failed.\r\nResult:" + str);
            }
        }

        private int String_to_IP(string IP_str)
        {
            string[] t = IP_str.Split('.');

            if (t.Length != 4)
                return 0;

            int ip = Convert.ToInt32(t[0]) << 24 | Convert.ToInt32(t[1]) << 16 | Convert.ToInt32(t[2]) << 8 | Convert.ToInt32(t[3]) << 0;
            return ip;
        }

        private string IP_to_String(int ip)
        {
            return string.Format("{0}.{1}.{2}.{3}", (ip >> 24) & 0xFF, (ip >> 16) & 0xFF, (ip >> 8) & 0xFF, ip & 0xFF);
        }
    }
}
