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
    public partial class frmTrString : Form
    {
        public frmTrString()
        {
            InitializeComponent();
        }

        private void frmTrString_Load(object sender, EventArgs e)
        {
        }

        private void cmdRead_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "Working...";
            Application.DoEvents();

            if (txtTrNo.Text.Length == 0)
            {
                lblMessage.Text = "Please input TrNo";
                return;
            }

            string strXML = null;
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "REQUEST", "GetTrString");
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "MSGTYPE", "request");
            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "MachineID", Program.gMachineNumber);
            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "TrNo", Convert.ToInt32(txtTrNo.Text));

            bool bRet = sbxpc.SBXPCDLL.GeneralOperationXML(Program.gMachineNumber, ref strXML);

            if (bRet)
            {
                lblMessage.Text = "Tr[" + txtTrNo.Text + "]: ";

                string base64_name;
                if (!sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "TrName", out base64_name))
                {
                    string error_info;
                    sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "Error", out error_info);
                    lblMessage.Text = "Error: " + error_info;
                }
                else
                {
                    if (base64_name != null)
                    {
                        try
                        {
                            byte[] name_binary = Convert.FromBase64String(base64_name);
                            int index = 0;
                            for (int i = 0; i < name_binary.Length - 1; i += 2)
                            {
                                if (name_binary[i] == 0 && name_binary[i + 1] == 0)
                                {
                                    index = i;
                                    break;
                                }
                            }

                            txtTrString.Text = Encoding.Unicode.GetString(name_binary, 0, index);
                            lblMessage.Text += txtTrString.Text;
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            else
            {
                int vErrorCode;
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }
        }

        private void cmdWrite_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "Working...";
            Application.DoEvents();

            if (txtTrNo.Text.Length == 0)
            {
                lblMessage.Text = "Please input TrNo";
                return;
            }

            string strXML = null;
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "REQUEST", "SetTrString");
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "MSGTYPE", "request");
            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "MachineID", Program.gMachineNumber);
            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "TrNo", Convert.ToInt32(txtTrNo.Text));
            {
                byte[] name_binary = Encoding.Unicode.GetBytes(txtTrString.Text);
                sbxpc.SBXPCDLL.XML_AddString(ref strXML, "TrName", Convert.ToBase64String(name_binary));
            }
            bool bRet = sbxpc.SBXPCDLL.GeneralOperationXML(Program.gMachineNumber, ref strXML);

            if (bRet)
            {
                string error_info;
                sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "Error", out error_info);
                if (error_info == "Success")
                    lblMessage.Text = "Set OK. Tr[" + txtTrNo.Text + "]: " + txtTrString.Text;
                else
                    lblMessage.Text = "Error: " + error_info;
            }
            else
            {
                int vErrorCode;
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }
        }

        private void frmTrString_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms["frmMain"].Visible = true;
        }
    }
}
