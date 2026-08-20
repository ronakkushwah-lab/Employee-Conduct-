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
    public partial class frmEvent : Form
    {
        public frmEvent()
        {
            InitializeComponent();
        }

        private void frmEvent_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
            Application.OpenForms["frmMain"].Visible = true;
        }

        private void frmEvent_Load(object sender, EventArgs e)
        {
            cmbComPort.SelectedIndex = 0;
            cmbBaudrate.SelectedIndex = 4;
            SwitchMode(true);
        }

        private void SwitchMode(bool bComport)
        {
            cmbComPort.Enabled = !bComport;
            cmbBaudrate.Enabled = !bComport;
            txtSourceIP.Enabled = bComport;
            txtPortNumber.Enabled = bComport;
        }

        public void ReceiveEvent(string eventXML)
        {
            int year, month, day, hour, minute, second, weekday;
            string strXML, eventItemString;
            string strMachineType = "", strEventType = "";
            int machinId;
            int managerId, userId, result;
            string str1 = "", str2 = "", str3 = "", str4 = "";

            strXML = eventXML;
            year = sbxpc.SBXPCDLL.XML_ParseInt(ref strXML, "Year");
            month = sbxpc.SBXPCDLL.XML_ParseInt(ref strXML, "Month");
            day = sbxpc.SBXPCDLL.XML_ParseInt(ref strXML, "Day");
            hour = sbxpc.SBXPCDLL.XML_ParseInt(ref strXML, "Hour");
            minute = sbxpc.SBXPCDLL.XML_ParseInt(ref strXML, "Minute");
            second = sbxpc.SBXPCDLL.XML_ParseInt(ref strXML, "Second");
            weekday = sbxpc.SBXPCDLL.XML_ParseInt(ref strXML, "Weekday");

            machinId = sbxpc.SBXPCDLL.XML_ParseInt(ref strXML, "MachineID");
            sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "MachineType", out strMachineType);
            sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "EventType", out strEventType);

            eventItemString = String.Format("{0:D2}-", year);
            eventItemString += String.Format("{0:D2}-", month);
            eventItemString += String.Format("{0:D2} ", day);
            eventItemString += String.Format("{0:D2}:", hour);
            eventItemString += String.Format("{0:D2}:", minute);
            eventItemString += String.Format("{0:D2} ", second);

            eventItemString += "[" + strMachineType + ":";
            eventItemString += machinId + "] ";
            eventItemString += strEventType + ", ";

            switch(strEventType)
            {
                case "Management Log":
                    managerId = sbxpc.SBXPCDLL.XML_ParseInt(ref strXML, "ManagerID");
                    userId = sbxpc.SBXPCDLL.XML_ParseInt(ref strXML, "UserID");
                    sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "Action", out str1);
                    result = sbxpc.SBXPCDLL.XML_ParseLong(ref strXML, "Result");
                    eventItemString += "Manager ID = " + String.Format("{0:D5}, ", managerId);
                    eventItemString += "User ID = " + String.Format("{0:D5}, ", userId);
                    eventItemString += "Action = " + str1 + ", ";
                    eventItemString += "Result = " + String.Format("{0:D}", result);
                    break;
                case "Time Log":
                    userId = sbxpc.SBXPCDLL.XML_ParseInt(ref strXML, "UserID");
                    sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "AttendanceStatus", out str1);
                    sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "VerificationMode", out str2);
                    sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "AntipassStatus", out str3);
                    sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "Photo", out str4);
                    eventItemString += "User ID = " + String.Format("{0:D5}, ", userId);
                    eventItemString += "AttendanceStatus = " + str1 + ", ";
                    eventItemString += "VerificationMode = " + str2 + ", ";
                    eventItemString += "AntipassStatus = " + str3 + ", ";
                    eventItemString += "Photo = " + str4;
                    break;
                case "Verification Success":
                    userId = sbxpc.SBXPCDLL.XML_ParseInt(ref strXML, "UserID");
                    sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "VerificationMode", out str1);
                    eventItemString += "User ID = " + String.Format("{0:D5}, ", userId);
                    eventItemString += "VerificationMode = " + str1;
                    break;
                case "Verification Failure":
                    userId = sbxpc.SBXPCDLL.XML_ParseInt(ref strXML, "UserID");
                    sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "VerificationMode", out str1);
                    sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "ReasonOfFailure", out str2);
                    eventItemString += "User ID = " + String.Format("{0:D5}, ", userId);
                    eventItemString += "VerificationMode = " + str1 + ", ";
                    eventItemString += "ReasonOfFailure = " + str2;
                    break;
                case "Alarm On":
                    userId = sbxpc.SBXPCDLL.XML_ParseInt(ref strXML, "UserID");
                    sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "AlarmType", out str1);
                    eventItemString += "User ID = " + String.Format("{0:D5}, ", userId);
                    eventItemString += "AlarmType = " + str1;
                    break;
                case "Alarm Off":
                    userId = sbxpc.SBXPCDLL.XML_ParseInt(ref strXML, "UserID");
                    sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "AlarmType", out str1);
                    sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "AlarmOffMethod", out str2);
                    eventItemString += "User ID = " + String.Format("{0:D5}", userId);
                    eventItemString += "AlarmType = " + str1 + ", ";
                    eventItemString += "AlarmOffMethod = " + str2;
                    break;
                case "DoorBell":
                    sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "InputType", out str1);
                    eventItemString += "Input Type = " + str1;
                    break;
            }

            lstEvents.Items.Add(eventItemString);
        }

        private void cmbEndMoniter_Click(object sender, EventArgs e)
        {
            sbxpc.SBXPCDLL.StopEventCapture();
            cmdStartMoniter.Enabled = true;
            cmdEndMoniter.Enabled = false;
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            lstEvents.Items.Clear();
        }

        private void cmdStartMoniter_Click(object sender, EventArgs e)
        {
            IntPtr temp = new IntPtr(0);
            if(radioNetworkDevice.Checked)
                sbxpc.SBXPCDLL.StartEventCapture(0, util.pubIPAddrToLong(txtSourceIP.Text), Convert.ToInt32(txtPortNumber.Text), new sbxpc.SBXPCDLL.EventCallback(ReceiveEvent), temp);
            else
                sbxpc.SBXPCDLL.StartEventCapture(1, cmbComPort.SelectedIndex + 1, Convert.ToInt32(cmbBaudrate.Text), new sbxpc.SBXPCDLL.EventCallback(ReceiveEvent), temp);
            cmdStartMoniter.Enabled = false;
            cmdEndMoniter.Enabled = true;
        }

        private void radioNetwork_Click(object sender, EventArgs e)
        {
            if(radioNetworkDevice.Checked)
            {
                SwitchMode(true);
                radioSerialDevice.Checked = false;
            }
        }

        private void radioSerial_Click(object sender, EventArgs e)
        {
            if(radioSerialDevice.Checked)
            {
                SwitchMode(false);
                radioNetworkDevice.Checked = false;
            }
        }
    }
}
