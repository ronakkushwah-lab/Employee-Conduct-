using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.IO;
using System.Xml.Linq;

namespace SBXPCDLLSampleCSharp
{
    public partial class frmEnroll : Form
    {
        public frmEnroll()
        {
            InitializeComponent();
        }

        int DATASIZE = (1404 + 12) / 4;
        int DATASIZE_FACE = (27668) / 4;

        int NAMESIZE = 54;
        int[] gTemplngEnrollData;
        Byte[] gbytEnrollData;
        Byte[] gbytEnrollData1;
        int[] gTempEnrollName;
        int glngEnrollPData;
        //        Boolean gGetState;
        ASCIIEncoding ascii;

        DataSet dsEnrolls;

        private void frmEnroll_Load(object sender, EventArgs e)
        {
            gbytEnrollData = new Byte[DATASIZE_FACE * 5];
            gbytEnrollData1 = new Byte[DATASIZE_FACE * 5];
            gTemplngEnrollData = new int[DATASIZE_FACE];
            gTempEnrollName = new int[NAMESIZE];
            ascii = new ASCIIEncoding();

            EnrollData ed = new EnrollData();
            ed.New("./");
            dsEnrolls = EnrollData.DataModule.GetEnrollDatas();
        }

        private void cmdGetEnrollData_Click(object sender, EventArgs e)
        {
            int vEnrollNumber;
            int vEMachineNumber;
            int vFingerNumber;
            int vPrivilege = 0;
            Boolean vRet;
            int vErrorCode = 0;
            int i;

            GCHandle gh;
            IntPtr AddrOfTemplngEnrollData;

            lblEnrollData.Text = "Enrolled Data";
            lstEnrollData.Items.Clear();
            Label2.Text = "";
            lstEnrollData.Items.Clear();
            lblMessage.Text = "Working...";
            Application.DoEvents();

            vRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            vEnrollNumber = Convert.ToInt32(txtEnrollNumber.Text);
            vFingerNumber = Convert.ToInt32(cmbBackupNumber.Text);
            if (vFingerNumber == 10) vFingerNumber = 15;
            vEMachineNumber = Program.gMachineNumber;

            gh = GCHandle.Alloc(gTemplngEnrollData, GCHandleType.Pinned);
            AddrOfTemplngEnrollData = gh.AddrOfPinnedObject();

            glngEnrollPData = 0;

            //vRet = sbxpc.SBXPCDLL.GetEnrollData1(Program.gMachineNumber, vEnrollNumber, vFingerNumber, out vPrivilege, AddrOfTemplngEnrollData, out glngEnrollPData);
            byte[] enro_bty=null;
            vRet = sbxpc.SBXPCDLL.GetEnrollData(Program.gMachineNumber, vEnrollNumber, Program.gMachineNumber, vFingerNumber, out vPrivilege, enro_bty, out glngEnrollPData);

            //vRet sbxpc.SBXPCDLL.Gee

            gh.Free();


            if (vRet)
            {
                cmbPrivilege.SelectedIndex = vPrivilege;
                lblMessage.Text = "GetEnrollData OK";
                if (vFingerNumber == 11) // Card Number
                {
                    txtCardNumber.Text = Convert.ToString(glngEnrollPData, 16).ToUpper();
                }
                else if(vFingerNumber == 14) // User timezone
                {
                    txtUserTz1.Text = Convert.ToString(glngEnrollPData % 64); glngEnrollPData = glngEnrollPData / 64;
                    txtUserTz2.Text = Convert.ToString(glngEnrollPData % 64); glngEnrollPData = glngEnrollPData / 64;
                    txtUserTz3.Text = Convert.ToString(glngEnrollPData % 64); glngEnrollPData = glngEnrollPData / 64;
                    txtUserTz4.Text = Convert.ToString(glngEnrollPData % 64); glngEnrollPData = glngEnrollPData / 64;
                    txtUserTz5.Text = Convert.ToString(glngEnrollPData % 64); glngEnrollPData = glngEnrollPData / 64;
                }
                else if (vFingerNumber == 15) // Password
                {
                    txtCardNumber.Text = "";
                    while (glngEnrollPData > 0)
                    {
                        i = glngEnrollPData % 16 - 1;
                        txtCardNumber.Text = txtCardNumber.Text + Convert.ToString(i);
                        glngEnrollPData = glngEnrollPData / 16;
                    }
                }
                else if (vFingerNumber == 16) // User department
                {
                    txtDepart.Text = Convert.ToString(glngEnrollPData);
                }
                else if (vFingerNumber == 17) // Face Data
                {
                     for (i = 0; i < DATASIZE_FACE; i++)
                        lstEnrollData.Items.Add(Convert.ToString(gTemplngEnrollData[i]));
                }
                else // other
                {
                    for (i = 0; i < DATASIZE; i++)
                        lstEnrollData.Items.Add(Convert.ToString(gTemplngEnrollData[i]));
                }
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
        }

        private void cmdSetEnrollData_Click(object sender, EventArgs e)
        {
            int vEnrollNumber;
            int vEMachineNumber;
            int vFingerNumber;
            int vPrivilege;
            Boolean vRet;
            int vErrorCode = 0;

            GCHandle gh;
            IntPtr AddrOfTemplngEnrollData;


            lblMessage.Text = "Working...";
            Application.DoEvents();

            if (txtEnrollNumber.Text == "") txtEnrollNumber.Text = "0";
            if (txtCardNumber.Text == "") txtCardNumber.Text = "0";
            
            vEnrollNumber = Convert.ToInt32(txtEnrollNumber.Text);
            vFingerNumber = Convert.ToInt32(cmbBackupNumber.Text);
            if (vFingerNumber == 10) vFingerNumber = 15;
            vPrivilege = Convert.ToInt32(cmbPrivilege.Text);
            vEMachineNumber = Program.gMachineNumber;

            if (vFingerNumber == 11) // Card 
            {
                glngEnrollPData = Convert.ToInt32(txtCardNumber.Text, 16);
            }
            else if (vFingerNumber == 14) // User timezone
            {
                glngEnrollPData = Convert.ToInt32(txtUserTz5.Text);
                glngEnrollPData = glngEnrollPData * 64 + Convert.ToInt32(txtUserTz4.Text);
                glngEnrollPData = glngEnrollPData * 64 + Convert.ToInt32(txtUserTz3.Text);
                glngEnrollPData = glngEnrollPData * 64 + Convert.ToInt32(txtUserTz2.Text);
                glngEnrollPData = glngEnrollPData * 64 + Convert.ToInt32(txtUserTz1.Text);
            }
            else if (vFingerNumber == 15) // Password
            {
                int i = txtCardNumber.Text.Length;
                if (i > 6) i = 6;
                glngEnrollPData = 0;
                while (i > 0)
                {
                    glngEnrollPData = glngEnrollPData * 16 + Convert.ToInt16(txtCardNumber.Text.Substring(i - 1, 1)) + 1;
                    i--;
                }
            }
            else if (vFingerNumber == 16) // User department
            {
                glngEnrollPData = Convert.ToInt32(txtDepart.Text);
            }
            
            vRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : false
            
            if (!vRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            gh = GCHandle.Alloc(gTemplngEnrollData, GCHandleType.Pinned);
            AddrOfTemplngEnrollData = gh.AddrOfPinnedObject();

            vRet = sbxpc.SBXPCDLL.SetEnrollData1(Program.gMachineNumber, vEnrollNumber, vFingerNumber, vPrivilege, AddrOfTemplngEnrollData, glngEnrollPData);

            gh.Free();

            if (vRet)
            {
                lblMessage.Text = "SetEnrollData OK";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
        }

        private void cmdDeleteEnrollData_Click(object sender, EventArgs e)
        {
            int vEnrollNumber;
            int vEMachineNumber;
            int vFingerNumber;
            Boolean vRet;
            int vErrorCode = 0;

            lblMessage.Text = "Working...";
            Application.DoEvents();

            vRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            vEnrollNumber = Convert.ToInt32(txtEnrollNumber.Text);
            vEMachineNumber = Program.gMachineNumber;
            vFingerNumber = Convert.ToInt32(cmbBackupNumber.Text);

            vRet = sbxpc.SBXPCDLL.DeleteEnrollData(Program.gMachineNumber, vEnrollNumber, vEMachineNumber, vFingerNumber);
            if (vRet)
            {
                lblMessage.Text = "DeleteEnrollData OK";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
        }

        private void cmdGetAllEnrollData_Click(object sender, EventArgs e)
        {
            int vEnrollNumber = 0;
            int vEMachineNumber = 0;
            int vFingerNumber = 0;
            int vPrivilege = 0;
            int vEnable = 0;
            Boolean vFlag;
            Boolean vRet;
            int vMsgRet;
            int vErrorCode = 0;
            string vStr = "";
            int i;
            string vTitle;

            DataTable dbEnrollTble;
            DataRow dbRow;
            DataSet dsChange;

            GCHandle gh;

            lstEnrollData.Items.Clear();
            vTitle = this.Text;
            Label2.Text = "";
            lblMessage.Text = "Working...";
            Application.DoEvents();

            vRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            vRet = sbxpc.SBXPCDLL.ReadAllUserID(Program.gMachineNumber);
            if (vRet)
            {
                lblMessage.Text = "ReadAllUserID OK";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
                sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
                return;
            }

            //*'*/---- Get Enroll data and save into database -------------
            Cursor = System.Windows.Forms.Cursors.WaitCursor;
            vFlag = false;


            dbEnrollTble = dsEnrolls.Tables[0];

            //            gGetState = true;

            while (true)
            {
                vRet = sbxpc.SBXPCDLL.GetAllUserID(Program.gMachineNumber, out vEnrollNumber, out vEMachineNumber, out vFingerNumber, out vPrivilege, out vEnable);
                if (!vRet) break;
                vFlag = true;
            EEE:
               if (vFingerNumber == 10) vFingerNumber = 15;     // password

                gh = GCHandle.Alloc(gTemplngEnrollData, GCHandleType.Pinned);
                IntPtr AddrOfTemplngEnrollData = gh.AddrOfPinnedObject();

                if (vFingerNumber >= 50)
                    continue;
                vRet = sbxpc.SBXPCDLL.GetEnrollData1(Program.gMachineNumber, vEnrollNumber, vFingerNumber, out vPrivilege, AddrOfTemplngEnrollData, out glngEnrollPData);

                if (!vRet)
                {
                    vFlag = false;
                    vStr = "GetEnrollData";
                    sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                    vMsgRet = util.MessageBox(new IntPtr(0), util.ErrorPrint(vErrorCode) + ": Continue ?", "GetEnrollData", 4);
                    if (vMsgRet == 6/*MsgBoxResult.Yes*/)
                    {
                        goto EEE;
                    }
                    else if (vMsgRet == 7/*MsgBoxResult.Cancel*/)
                    {
                        Cursor = System.Windows.Forms.Cursors.Default;
                        sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
                        //                        gGetState = false;
                        return;
                    }
                }
                foreach (DataRow dbRow1 in dbEnrollTble.Rows)
                {
                    if ((int)dbRow1["EnrollNumber"] == vEnrollNumber)
                    {
                        if ((int)dbRow1["EMachineNumber"] == vEMachineNumber)
                        {
                            if ((int)dbRow1["FingerNumber"] == vFingerNumber)
                            {
                                lblMessage.Text = "Double ID";
                                goto FFF;
                            }
                        }
                    }
                }

                dbRow = dbEnrollTble.NewRow();
                dbRow["EMachineNumber"] = vEMachineNumber;
                dbRow["EnrollNumber"] = vEnrollNumber;
                dbRow["FingerNumber"] = vFingerNumber;
                dbRow["Privilige"] = vPrivilege;

                if (vFingerNumber == 10)
                {
                    dbRow["Password1"] = glngEnrollPData;
                }
                else if (vFingerNumber == 15)
                {
                    dbRow["Password1"] = glngEnrollPData;
                }
                else if (vFingerNumber == 11)
                {
                    dbRow["Password1"] = glngEnrollPData;
                }
                else if (vFingerNumber == 17)
                {
                    dbRow["Password1"] = 0;

                    for (i = 0; i < DATASIZE_FACE; i++)
                    {
                        gbytEnrollData[i * 5] = 1;
                        if (gTemplngEnrollData[i] == unchecked((int)0x80000000u))
                        {
                            gbytEnrollData[i * 5] = 0;
                            gTemplngEnrollData[i] = 0;
                        }
                        else if (gTemplngEnrollData[i] < 0)
                        {
                            gbytEnrollData[i * 5] = 0;
                            gTemplngEnrollData[i] = -gTemplngEnrollData[i];
                        }
                        gbytEnrollData[i * 5 + 1] = (Byte)(gTemplngEnrollData[i] / 256 / 256 / 256);
                        gbytEnrollData[i * 5 + 2] = (Byte)((gTemplngEnrollData[i] / 256 / 256) % 256);
                        gbytEnrollData[i * 5 + 3] = (Byte)((gTemplngEnrollData[i] / 256) % 256);
                        gbytEnrollData[i * 5 + 4] = (Byte)(gTemplngEnrollData[i] % 256);
                    }

                    Byte[] gbyt = new Byte[DATASIZE_FACE * 5];
                    for (i = 0; i < DATASIZE_FACE * 5; i++)
                        gbyt[i] = gbytEnrollData[i];
                    dbRow["FPdata"] = gbyt;
                }
                else
                {
                    dbRow["Password1"] = 0;

                    for (i = 0; i < DATASIZE; i++)
                    {
                        gbytEnrollData[i * 5] = 1;
                        if (gTemplngEnrollData[i] == unchecked((int)0x80000000u))
                        {
                            gbytEnrollData[i * 5] = 0;
                            gTemplngEnrollData[i] = 0;
                        }
                        else if (gTemplngEnrollData[i] < 0)
                        {
                            gbytEnrollData[i * 5] = 0;
                            gTemplngEnrollData[i] = -gTemplngEnrollData[i];
                        }
                        gbytEnrollData[i * 5 + 1] = (Byte)(gTemplngEnrollData[i] / 256 / 256 / 256);
                        gbytEnrollData[i * 5 + 2] = (Byte)((gTemplngEnrollData[i] / 256 / 256) % 256);
                        gbytEnrollData[i * 5 + 3] = (Byte)((gTemplngEnrollData[i] / 256) % 256);
                        gbytEnrollData[i * 5 + 4] = (Byte)(gTemplngEnrollData[i] % 256);
                    }

                    //dbRow("FPdata") = gbytEnrollData        '<---------- Error

                    Byte[] gbyt = new Byte[DATASIZE * 5];
                    for (i = 0; i < DATASIZE * 5; i++)
                        gbyt[i] = gbytEnrollData[i];
                    dbRow["FPdata"] = gbyt;

                }
                dbEnrollTble.Rows.Add(dbRow);

            FFF:

                lblMessage.Text = String.Format("{0:D2}", vEMachineNumber) + "-" + String.Format("{0:D2}", vEnrollNumber) + "-" + vFingerNumber;
                this.Text = String.Format("{0:D2}", vEnrollNumber);
                txtEnrollNumber.Text = Convert.ToString(vEnrollNumber);
                cmbBackupNumber.Text = Convert.ToString(vFingerNumber);
                cmbPrivilege.Text = Convert.ToString(vPrivilege);
                Application.DoEvents();
            }

            Label2.Text = "Total : " + dsEnrolls.Tables["tblEnroll"].Rows.Count;
            dsChange = dsEnrolls.GetChanges();
            EnrollData.DataModule.SaveEnrolls(dsEnrolls);

            //            gh.Free();


            //            gGetState = false; 

            vTitle = this.Text;
            Cursor = System.Windows.Forms.Cursors.Default;

            if (vFlag)
                lblMessage.Text = "GetAllUserID OK";
            else
                lblMessage.Text = vStr + ":" + util.ErrorPrint(vErrorCode);

            Application.DoEvents();
            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
        }

        private void cmdSetAllEnrollData_Click(object sender, EventArgs e)
        {
            int vEnrollNumber;
            int vEMachineNumber;
            int vFingerNumber;
            int vPrivilege;
            Boolean vRet;
            //            Boolean vFlag;
            int vErrorCode = 0;

            Byte[] vByte;
            int i;
            string vTitle;
            //            string vStr = "";


            DataTable dbEnrollTble;
            //            DataRow dbRow;
            GCHandle gh;
            int num;

            lstEnrollData.Items.Clear();
            vTitle = this.Text;
            lblMessage.Text = "Working...";
            Application.DoEvents();

            vRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            //            vFlag = false;
            //            gGetState = true;
            Cursor = System.Windows.Forms.Cursors.WaitCursor;


            dbEnrollTble = dsEnrolls.Tables[0];

            num = 0;


            if (dbEnrollTble.Rows.Count == 0) goto EEE;

            foreach (DataRow dbRow in dbEnrollTble.Rows)
            {
                vEMachineNumber = (int)dbRow["EMachineNumber"];
                vEnrollNumber = (int)dbRow["EnrollNumber"];
                vFingerNumber = (int)dbRow["FingerNumber"];
                vPrivilege = (int)dbRow["Privilige"];
                glngEnrollPData = (int)dbRow["Password1"];

                num = num + 1;
                if (vFingerNumber == 17)
                {
                    vByte = (Byte[])dbRow["FPData"];

                    for (i = 0; i < DATASIZE_FACE; i++)
                    {
                        gTemplngEnrollData[i] = vByte[i * 5 + 1];
                        gTemplngEnrollData[i] = gTemplngEnrollData[i] * 256 + vByte[i * 5 + 2];
                        gTemplngEnrollData[i] = gTemplngEnrollData[i] * 256 + vByte[i * 5 + 3];
                        gTemplngEnrollData[i] = gTemplngEnrollData[i] * 256 + vByte[i * 5 + 4];
                        if (vByte[i * 5] == 0)
                        {
                            if (gTemplngEnrollData[i] == 0)
                                gTemplngEnrollData[i] = unchecked((int)0x80000000u);
                            else
                                gTemplngEnrollData[i] = -gTemplngEnrollData[i];
                        }
                    }
                }
                else if (vFingerNumber < 10)
                {
                    vByte = (Byte[])dbRow["FPData"];

                    for (i = 0; i < DATASIZE; i++)
                    {
                        gTemplngEnrollData[i] = vByte[i * 5 + 1];
                        gTemplngEnrollData[i] = gTemplngEnrollData[i] * 256 + vByte[i * 5 + 2];
                        gTemplngEnrollData[i] = gTemplngEnrollData[i] * 256 + vByte[i * 5 + 3];
                        gTemplngEnrollData[i] = gTemplngEnrollData[i] * 256 + vByte[i * 5 + 4];
                        if (vByte[i * 5] == 0)
                        {
                            if (gTemplngEnrollData[i] == 0)
                                gTemplngEnrollData[i] = unchecked((int)0x80000000u);
                            else
                                gTemplngEnrollData[i] = 0 - gTemplngEnrollData[i];
                        }
                    }
                }
            FFF:

                gh = GCHandle.Alloc(gTemplngEnrollData, GCHandleType.Pinned);
                IntPtr AddrOfTemplngEnrollData = gh.AddrOfPinnedObject();

                int vFingerNumber2 = vFingerNumber;
                if (vFingerNumber2 >= 0 && vFingerNumber2 <= 9 && !chkDupCheck.Checked)
                    vFingerNumber2 += 20;

                vRet = sbxpc.SBXPCDLL.SetEnrollData1(Program.gMachineNumber, vEnrollNumber, vFingerNumber2, vPrivilege, AddrOfTemplngEnrollData, glngEnrollPData);

                gh.Free();

                if (!vRet)
                {
                    //                    vFlag = false;
                    //                    vStr = "SetEnrollData";
                    sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                    int vMsgRet = util.MessageBox(new IntPtr(0), util.ErrorPrint(vErrorCode) + ": Continue ?", "SetEnrollData", 4);
                    if (vMsgRet == 6/*Yes Button*/) goto FFF;
                    if (vMsgRet == 7/*No Button*/) goto EEE;
                }

                //    LLL:
                lblMessage.Text = "EMachine = " + Convert.ToString(vEMachineNumber) + ", ID = " + String.Format("{0:D5}", vEnrollNumber) + ", FpNo = " + vFingerNumber + ", Count = " + num;

                this.Text = Convert.ToString(num);
                Application.DoEvents();
            }
        EEE:
            vTitle = this.Text;
            Cursor = System.Windows.Forms.Cursors.Default;
            //            gGetState = false;

            lblMessage.Text = "SetAllUserData OK";
            Application.DoEvents();

            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
        }

        private void cmdGetName_Click(object sender, EventArgs e)
        {
            int vEnrollNumber;
            int vEMachineNumber;
            Boolean vRet;
            int vErrorCode = 0;
            string vName = "";


            lblMessage.Text = "Working...";
            Application.DoEvents();

            vRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            vEnrollNumber = 0;
            if (txtEnrollNumber.Text != "")
                vEnrollNumber = Convert.ToInt32(txtEnrollNumber.Text);
            vEMachineNumber = Program.gMachineNumber;

            vRet = sbxpc.SBXPCDLL.GetUserName1(Program.gMachineNumber, vEnrollNumber, out vName);
            if (vRet)
            {
                txtName.Text = vName;
                lblMessage.Text = "GetUserName OK";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
        }

        private void cmdSetName_Click(object sender, EventArgs e)
        {
            int vEnrollNumber;
            int vEMachineNumber;
            Boolean vRet;
            int vErrorCode = 0;
            string vName = "";


            lblMessage.Text = "Working...";
            Application.DoEvents();

            vRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            vEnrollNumber = Convert.ToInt32(txtEnrollNumber.Text);
            vEMachineNumber = Program.gMachineNumber;

            vName = txtName.Text;
            vRet = sbxpc.SBXPCDLL.SetUserName1(Program.gMachineNumber, vEnrollNumber, vName);
            if (vRet)
            {
                lblMessage.Text = "SetUserName OK";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
        }

        private void cmdSetCompany_Click(object sender, EventArgs e)
        {
            int vEMachineNumber;
            Boolean vRet;
            int vErrorCode = 0;
            string vName = "";


            lblMessage.Text = "Working...";
            Application.DoEvents();

            vRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            vEMachineNumber = Program.gMachineNumber;

            vName = txtName.Text;
            vRet = sbxpc.SBXPCDLL.SetCompanyName1(Program.gMachineNumber, 1, vName);
            if (vRet)
            {
                lblMessage.Text = "Set Company Name OK";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
        }

        private void cmdDeleteCompany_Click(object sender, EventArgs e)
        {
            int vEMachineNumber;
            Boolean vRet;
            int vErrorCode = 0;
            string vName = "";


            lblMessage.Text = "Working...";
            Application.DoEvents();

            vRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            vEMachineNumber = Program.gMachineNumber;

            vRet = sbxpc.SBXPCDLL.SetCompanyName1(Program.gMachineNumber, 0, vName);
            if (vRet)
            {
                lblMessage.Text = "Delete Company Name OK";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
        }

        private void cmdGetEnrollInfo_Click(object sender, EventArgs e)
        {
            int vEMachineNumber = 0;
            int vEnrollNumber = 0;
            int vFingerNumber = 0;
            int vPrivilege = 0;
            int vEnable = 0;
            Boolean vRet;
            Boolean vFlag;
            int vErrorCode = 0;
            int i;

            lblEnrollData.Text = "User IDs";
            lstEnrollData.Items.Clear();
            lblMessage.Text = "Working...";
            Application.DoEvents();

            vRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            vRet = sbxpc.SBXPCDLL.ReadAllUserID(Program.gMachineNumber);
            if (vRet)
            {
                lblMessage.Text = "ReadAllUserID OK";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
                sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
                return;
            }

            //------ Show all enroll information ----------
            vFlag = false;
            i = 0;
            lstEnrollData.Items.Add(("No.  EnNo   EMNo   Fp   Priv  Enable Duress"));
            while (true)
            {
                vRet = sbxpc.SBXPCDLL.GetAllUserID(Program.gMachineNumber, out vEnrollNumber, out vEMachineNumber, out vFingerNumber, out vPrivilege, out vEnable);
                if (!vRet) break;
                vFlag = true;
                lstEnrollData.Items.Add((String.Format("{0:D5}", i) + "    " + String.Format("{0:D5}", vEnrollNumber) + "    " + String.Format("{0:D3}", vEMachineNumber) + "    " + String.Format("{0:D2}", vFingerNumber) + "    " + Convert.ToString(vPrivilege) + "    " + Convert.ToString(vEnable % 256) + "     " + Convert.ToString(vEnable / 256)));

                i = i + 1;
                Label2.Text = "Total : " + i;
            }

            if (vFlag)
                lblMessage.Text = "GetAllUserID OK";
            else
                lblMessage.Text = util.ErrorPrint(vErrorCode);

            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
        }

        private void cmdEnableUser_Click(object sender, EventArgs e)
        {
            int vEnrollNumber;
            int vEMachineNumber;
            int vFingerNumber;
            Boolean vRet;
            byte vFlag;
            int vErrorCode = 0;

            lblMessage.Text = "Working...";
            Application.DoEvents();

            vEMachineNumber = Program.gMachineNumber;
            vEnrollNumber = Convert.ToInt32(txtEnrollNumber.Text == "" ? "0" : txtEnrollNumber.Text);
            vFingerNumber = Convert.ToInt32(cmbBackupNumber.Text);
            vFlag = chkDisable.Checked ? (byte)0 : (byte)1;

            vRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            vRet = sbxpc.SBXPCDLL.EnableUser(Program.gMachineNumber, vEnrollNumber, vEMachineNumber, vFingerNumber, vFlag);
            if (vRet)
            {
                lblMessage.Text = "Success!";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
        }

        private void cmdModifyPrivilege_Click(object sender, EventArgs e)
        {
            int vEnrollNumber;
            int vEMachineNumber;
            int vFingerNumber;
            int vMachinePrivilege;
            Boolean vRet;
            int vErrorCode = 0;

            lblMessage.Text = "Working...";
            Application.DoEvents();

            vEMachineNumber = Program.gMachineNumber;
            vEnrollNumber = Convert.ToInt32(txtEnrollNumber.Text);
            vFingerNumber = Convert.ToInt32(cmbBackupNumber.Text);
            vMachinePrivilege = Convert.ToInt32(cmbPrivilege.Text);

            vRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            vRet = sbxpc.SBXPCDLL.ModifyPrivilege(Program.gMachineNumber, vEnrollNumber, vEMachineNumber, vFingerNumber, vMachinePrivilege);
            if (vRet)
            {
                lblMessage.Text = "Success!";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
        }

        private void cmdEmptyEnrollData_Click(object sender, EventArgs e)
        {
            Boolean vRet;
            int vErrorCode = 0;

            lblMessage.Text = "Working...";
            Application.DoEvents();

            vRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            vRet = sbxpc.SBXPCDLL.EmptyEnrollData(Program.gMachineNumber);
            if (vRet)
            {
                lblMessage.Text = "Success!";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
        }

        private void cmdClearData_Click(object sender, EventArgs e)
        {
            Boolean vRet;
            int vErrorCode = 0;

            lblMessage.Text = "Working...";
            Application.DoEvents();

            vRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            vRet = sbxpc.SBXPCDLL.ClearKeeperData(Program.gMachineNumber);
            if (vRet)
            {
                lblMessage.Text = "ClearKeeperData OK!";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : true
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
            Application.OpenForms["frmMain"].Visible = true;
        }

        private void frmEnroll_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.OpenForms["frmMain"].Visible = true;
            ClearUserPhoto();
        }

        private void cmdDel_Click(object sender, EventArgs e)
        {
            EnrollData.DataModule.DeleteDB();

            Label2.Text = "Total : 0";
            lblMessage.Text = "Deleted PC Database";
        }

        private void cmdModifyDuressFP_Click(object sender, EventArgs e)
        {
            int vEnrollNumber;
            int vFingerNumber;
            int vDuressSetting;
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

            vEnrollNumber = Convert.ToInt32(txtEnrollNumber.Text);
            vFingerNumber = Convert.ToInt32(cmbBackupNumber.Text);
            vDuressSetting = Convert.ToInt32(cmbDuressSetting.Text);

            bRet = sbxpc.SBXPCDLL.ModifyDuressFP(Program.gMachineNumber,
                                       vEnrollNumber,
                                       vFingerNumber,
                                       vDuressSetting
                                      );
            if (bRet)
                lblMessage.Text = "ModifyDuressFP OK";
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            bRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : enable
        }

        private void cmdRemoteEnroll(string Backup)
        {
            lblMessage.Text = "Working...";
            Application.DoEvents();

            int vEnrollNumber = Convert.ToInt32(txtEnrollNumber.Text);

            string strXML = null;
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "REQUEST", "RemoteEnroll");
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "MSGTYPE", "request");
            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "MachineID", Program.gMachineNumber);
            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "UserID", vEnrollNumber);
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "Backup", Backup);
            bool bRet = sbxpc.SBXPCDLL.GeneralOperationXML(Program.gMachineNumber, ref strXML);

            if (bRet)
            {
                string strResultCode;
                sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "ResultCode", out strResultCode);
                if (strResultCode == "MenuProcessing")
                    lblMessage.Text = "Machine is now processing menu.";
                else if (strResultCode == "InvalidBackup")
                    lblMessage.Text = "Invalid Remote Enroll Backup";
                else if (strResultCode == "EnrollNumberError")
                    lblMessage.Text = "Invalid Enroll Number";
                else if (strResultCode == "FaceAlreadyEnrolled")
                    lblMessage.Text = "Face already are enrolled for this user";
                else if (strResultCode == "FPAllEnrolled")
                    lblMessage.Text = "All fingerprints are enrolled for this user";
                else if (strResultCode == "CardAlreadyEnrolled")
                    lblMessage.Text = "Card already are enrolled for this user";
                else if (strResultCode == "DatabaseFull")
                    lblMessage.Text = "Fingerprint database is full.";
                else if (strResultCode == "RemoteEnrollAlreadyStarted")
                    lblMessage.Text = "Remote Enroll Already Started.";
                else if (strResultCode == "Success")
                    lblMessage.Text = "Remote Enroll Started.";
                else
                    lblMessage.Text = "Unknown Error";
            }
            else
            {
                int vErrorCode;
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }
        }
        private void cmdRemoteEnroll_Face_Click(object sender, EventArgs e)
        {
            cmdRemoteEnroll("RemoteEnrollFace");
        }
        private void cmdRemoteEnroll_FP_Click(object sender, EventArgs e)
        {
            cmdRemoteEnroll("RemoteEnrollFP");
        }
        private void cmdRemoteEnroll_Card_Click(object sender, EventArgs e)
        {
            cmdRemoteEnroll("RemoteEnrollCard");
        }
        private void cmdExitRemoteEnroll_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "Working...";
            Application.DoEvents();

            string strXML = null;
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "REQUEST", "ExitRemoteEnroll");
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "MSGTYPE", "request");
            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "MachineID", Program.gMachineNumber);
            bool bRet = sbxpc.SBXPCDLL.GeneralOperationXML(Program.gMachineNumber, ref strXML);

            if (bRet)
            {
                string strResultCode;
                sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "ResultCode", out strResultCode);
                if (strResultCode == "SuccessExitRemoteEnroll")
                    lblMessage.Text = "Success Exit RemoteEnroll.";
                else if (strResultCode == "NotStartedRemoteEnroll")
                    lblMessage.Text = "Remote Enroll is not Started.";
                else
                    lblMessage.Text = "Unknown Error";
            }
            else
            {
                int vErrorCode;
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }
        }

        private void cmdQueryRemoteEnrollStatus_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "Working...";
            Application.DoEvents();

            string strXML = null;
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "REQUEST", "QueryRemoteEnrollStatus");
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "MSGTYPE", "request");
            sbxpc.SBXPCDLL.XML_AddLong(ref strXML, "MachineID", Program.gMachineNumber);
            bool bRet = sbxpc.SBXPCDLL.GeneralOperationXML(Program.gMachineNumber, ref strXML);

            if (bRet)
            {
                string strResultCode;
                sbxpc.SBXPCDLL.XML_ParseString(ref strXML, "ResultCode", out strResultCode);
                if (strResultCode == "RemoteEnrollAlreadyStarted")
                    lblMessage.Text = "[QueryRemoteEnrollStatus] RemoteEnrollAlreadyStarted";
                else if (strResultCode == "NotStartedRemoteEnroll")
                    lblMessage.Text = "[QueryRemoteEnrollStatus] NotStartedRemoteEnroll";
                else
                    lblMessage.Text = "Unknown Error";
            }
            else
            {
                int vErrorCode;
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }
        }
        private void btnImportM1001U_Click(object sender, EventArgs e1)
		{
			int i;
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.FileName = "Document"; // Default file name
			dlg.DefaultExt = ".xml"; // Default file extension
			dlg.Filter = "Text documents (.xml)|*.xml"; // Filter files by extension

			// Show open file dialog box
			var result = dlg.ShowDialog();

			// Process save file dialog box results
			if (result == System.Windows.Forms.DialogResult.OK)
			{
				DataTable dbEnrollTble;
				DataRow dbRow;
				DataSet dsChange;
				dbEnrollTble = dsEnrolls.Tables[0];

				// Open document
				string filename = dlg.FileName;

				XDocument doc = XDocument.Load(filename);

				foreach (var u in (from n in doc.Root.Elements()
								   where n.Name == "UserInfo"
								   select n))
				{
					var e = (from n in u.Elements()
							 where n.Name == "id"
							 select n);
					int id = 0;
					if (e.Count() < 1)
						continue;
					if (!int.TryParse(e.FirstOrDefault().Value, out id))
						continue;

					string name = "";
					e = (from n in u.Elements()
						 where n.Name == "name"
						 select n);
					if (e.Count() > 0)
					{
						name = e.FirstOrDefault().Value;
						name.Trim();
					}

					byte[] face_data = null;
					e = (from n in u.Elements()
						 where n.Name == "face_data"
						 select n);
					if (e.Count() > 0)
					{
						face_data = Convert.FromBase64String(e.FirstOrDefault().Value);
					}

					//copy face_data to gTemplngEnrollData
					GCHandle gh = GCHandle.Alloc(gTemplngEnrollData, GCHandleType.Pinned);
					IntPtr AddrOfTemplngEnrollData = gh.AddrOfPinnedObject();
					Marshal.Copy(face_data, 0, AddrOfTemplngEnrollData, face_data.Length);
					gh.Free();

					dbRow = dbEnrollTble.NewRow();
					dbRow["EMachineNumber"] = 1;
					dbRow["EnrollNumber"] = id;
					dbRow["FingerNumber"] = 17;
					dbRow["Privilige"] = 0;
					dbRow["Password1"] = 0;

					for (i = 0; i < DATASIZE_FACE; i++)
					{
						gbytEnrollData[i * 5] = 1;
						if (gTemplngEnrollData[i] == unchecked((int)0x80000000u))
						{
							gbytEnrollData[i * 5] = 0;
							gTemplngEnrollData[i] = 0;
						}
						else if (gTemplngEnrollData[i] < 0)
						{
							gbytEnrollData[i * 5] = 0;
							gTemplngEnrollData[i] = -gTemplngEnrollData[i];
						}
						gbytEnrollData[i * 5 + 1] = (Byte)(gTemplngEnrollData[i] / 256 / 256 / 256);
						gbytEnrollData[i * 5 + 2] = (Byte)((gTemplngEnrollData[i] / 256 / 256) % 256);
						gbytEnrollData[i * 5 + 3] = (Byte)((gTemplngEnrollData[i] / 256) % 256);
						gbytEnrollData[i * 5 + 4] = (Byte)(gTemplngEnrollData[i] % 256);
					}

					Byte[] gbyt = new Byte[DATASIZE_FACE * 5];
					for (i = 0; i < DATASIZE_FACE * 5; i++)
						gbyt[i] = gbytEnrollData[i];
					dbRow["FPdata"] = gbyt;
				}
			}
		}

        private void btnGetUserPeriod_Click(object sender, EventArgs e)
        {
            Boolean bRet;
            int vErrorCode = 0;
            int lUserID, lStartDate, lEndDate;
            int yy, mm, dd;
            String strXML = "";

            lblMessage.Text = "Working...";
            Application.DoEvents();

            bRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0);
            if (!bRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            lUserID = Convert.ToInt32(txtEnrollNumber.Text);

            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "REQUEST", "GetUserPeriod");
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "MSGTYPE", "request");
            sbxpc.SBXPCDLL.XML_AddInt(ref strXML, "MachineID", Program.gMachineNumber);
            sbxpc.SBXPCDLL.XML_AddInt(ref strXML, "UserID", lUserID);

            bRet = sbxpc.SBXPCDLL.GeneralOperationXML(Program.gMachineNumber, ref strXML);

            if (bRet)
            {
                chkUsePeriod.Checked = (sbxpc.SBXPCDLL.XML_ParseInt(ref strXML, "Used") != 0);
                lStartDate = sbxpc.SBXPCDLL.XML_ParseInt(ref strXML, "Start");
                lEndDate = sbxpc.SBXPCDLL.XML_ParseInt(ref strXML, "End");
                if (chkUsePeriod.Checked)
                {
                    yy = lStartDate / 256 / 256;
                    mm = (lStartDate - yy * 256 * 256) / 256;
                    dd = lStartDate & 0xFF;
                    dtPeriodFrom.Value = new DateTime(yy + 2000, mm, dd);

                    yy = lEndDate / 256 / 256;
                    mm = (lEndDate - yy * 256 * 256) / 256;
                    dd = lEndDate & 0xFF;
                    dtPeriodTo.Value = new DateTime(yy + 2000, mm, dd);
                }
                else
                {
                    dtPeriodFrom.Enabled = false;
                    dtPeriodTo.Enabled = false;
                }
                lblMessage.Text = "Success!";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }
            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1);
        }

        private void btnSetUserPeriod_Click(object sender, EventArgs e)
        {
            Boolean bRet;
            int vErrorCode = 0;
            int lUserID;
            int lStartPeriod, lEndPeriod;
            String strXML = "";

            lblMessage.Text = "Waiting...";
            Application.DoEvents();

            bRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0);
            if (!bRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            lUserID = Convert.ToInt32(txtEnrollNumber.Text);

            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "REQUEST", "SetUserPeriod");
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "MSGTYPE", "request");
            sbxpc.SBXPCDLL.XML_AddInt(ref strXML, "MachineID", Program.gMachineNumber);
            sbxpc.SBXPCDLL.XML_AddInt(ref strXML, "UserID", lUserID);

            sbxpc.SBXPCDLL.XML_AddInt(ref strXML, "Used", chkUsePeriod.Checked ? 1 : 0);
            if (chkUsePeriod.Checked)
            {
                lStartPeriod = (dtPeriodFrom.Value.Year - 2000) * 256 * 256 + dtPeriodFrom.Value.Month * 256 + dtPeriodFrom.Value.Day;
                lEndPeriod = (dtPeriodTo.Value.Year - 2000) * 256 * 256 + dtPeriodTo.Value.Month * 256 + dtPeriodTo.Value.Day;
            }
            else
            {
                lStartPeriod = 1 * 256 + 1;
                lEndPeriod = 1 * 256 + 1;
            }
            sbxpc.SBXPCDLL.XML_AddInt(ref strXML, "Start", lStartPeriod);
            sbxpc.SBXPCDLL.XML_AddInt(ref strXML, "End", lEndPeriod);

            bRet = sbxpc.SBXPCDLL.GeneralOperationXML(Program.gMachineNumber, ref strXML);

            if (bRet)
            {
                lblMessage.Text = "Success!";
            }
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }
            sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1);
        }

        private void chkUsePeriod_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUsePeriod.Checked)
            {
                dtPeriodFrom.Enabled = true;
                dtPeriodTo.Enabled = true;
            }
            else
            {
                dtPeriodFrom.Enabled = false;
                dtPeriodTo.Enabled = false;
            }
        }

        private void ClearUserPhoto()
        {
            if (picUserPhoto.Image != null) picUserPhoto.Image.Dispose();
            picUserPhoto.Image = null;
        }

        private void cmdUserPhotoBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDlg.ShowDialog();
            txtUserPhotoFile.Text = OpenFileDlg.FileName;
            ClearUserPhoto();
            if (!File.Exists(txtUserPhotoFile.Text))
                return;

            picUserPhoto.Image = Image.FromFile(txtUserPhotoFile.Text);
        }

        private void cmdGetUserPhoto_Click(object sender, EventArgs e)
        {
            string vEnrollNumber;
            bool bRet;
            string strXML = "";
            int vErrorCode = 0;

            txtUserPhotoFile.Text = "";

            lblMessage.Text = "Working...";
            Application.DoEvents();

            bRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0); // 0 : disable

            if (!bRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            vEnrollNumber = txtEnrollNumber.Text;

            util.MakeXMLRequestHeader(ref strXML, "GetUserPhotoData");
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "UserID", vEnrollNumber);


            bRet = sbxpc.SBXPCDLL.GeneralOperationXML(Program.gMachineNumber, ref strXML);

            if (!bRet)
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
                ClearUserPhoto();
                goto _lexit;
            }

            lblMessage.Text = "GetUserPhotoData OK";
            Byte[] photoData = new Byte[util.gCompressPhotoSize];
            GCHandle gh = GCHandle.Alloc(photoData, GCHandleType.Pinned);
            IntPtr AddrOfPhotoData = gh.AddrOfPinnedObject();

            bRet = sbxpc.SBXPCDLL.XML_ParseBinaryLong(ref strXML, "PhotoData", AddrOfPhotoData, util.gCompressPhotoSize);

            ClearUserPhoto();

            if (!bRet)
            {
                lblMessage.Text = "GetGlogPhotoData - XML Parse Error";
                goto _lexit;
            }

            if (File.Exists(util.gTempPhotoFile))
                File.Delete(util.gTempPhotoFile);
            FileStream FS = File.Create(util.gTempPhotoFile);
            FS.Write(photoData, 0, util.gCompressPhotoSize);
            FS.Close();
            FS.Dispose();
            FS = null;

            picUserPhoto.Image = Image.FromFile(util.gTempPhotoFile);
            txtUserPhotoFile.Text = util.gTempPhotoFile;

        _lexit:
            bRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : enable
        }

        private void cmdSetUserPhoto_Click(object sender, EventArgs e)
        {
            bool bRet;
            int vErrorCode = 0;
            string strXML = "";
            string vEnrollNumber;

            string photoFileName = txtUserPhotoFile.Text;
            if (!File.Exists(photoFileName))
            {
                lblMessage.Text = "Can not find the photo file.";
                return;
            }

            lblMessage.Text = "Working...";
            Application.DoEvents();

            bRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0);

            if (!bRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            ClearUserPhoto();

            FileStream FS = File.Open(photoFileName, FileMode.Open, FileAccess.Read, FileShare.None);
            if (FS.Length != util.gCompressPhotoSize)
            {
                lblMessage.Text = "Photo file size is not" + Convert.ToString(util.gCompressPhotoSize) + "Byte";
                return;
            }

            Byte[] photoData = new Byte[util.gCompressPhotoSize];
            FS.Read(photoData, 0, util.gCompressPhotoSize);
            FS.Close();
            FS.Dispose();
            FS = null;

            picUserPhoto.Image = Image.FromFile(photoFileName);

            GCHandle gh = GCHandle.Alloc(photoData, GCHandleType.Pinned);
            IntPtr AddrOfPhotoData = gh.AddrOfPinnedObject();

            vEnrollNumber = txtEnrollNumber.Text;

            util.MakeXMLRequestHeader(ref strXML, "SetUserPhotoData");
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "UserID", vEnrollNumber);
            sbxpc.SBXPCDLL.XML_AddBinaryLong(ref strXML, "PhotoData", AddrOfPhotoData, util.gCompressPhotoSize);

            bRet = sbxpc.SBXPCDLL.GeneralOperationXML(Program.gMachineNumber, ref strXML);

            if (bRet)
                lblMessage.Text = "SetUserPhotoData OK";
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            bRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : enable
        }

        private void cmdDeleteUserPhoto_Click(object sender, EventArgs e)
        {
            bool bRet;
            int vErrorCode = 0;
            string strXML = "";
            string vEnrollNumber;

            bRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 0);

            if (!bRet)
            {
                lblMessage.Text = util.gstrNoDevice;
                return;
            }

            string photoFileName = txtUserPhotoFile.Text;
            if (!File.Exists(photoFileName))
            {
                lblMessage.Text = "Can not find the photo file.";
                return;
            }

            vEnrollNumber = txtEnrollNumber.Text;

            util.MakeXMLRequestHeader(ref strXML, "SetUserPhotoData");
            sbxpc.SBXPCDLL.XML_AddString(ref strXML, "UserID", vEnrollNumber);
            // Don't make "PhotoData" tag to delete user photo

            bRet = sbxpc.SBXPCDLL.GeneralOperationXML(Program.gMachineNumber, ref strXML);

            if (bRet)
                lblMessage.Text = "DeleteUserPhotoData OK";
            else
            {
                sbxpc.SBXPCDLL.GetLastError(Program.gMachineNumber, out vErrorCode);
                lblMessage.Text = util.ErrorPrint(vErrorCode);
            }

            bRet = sbxpc.SBXPCDLL.EnableDevice(Program.gMachineNumber, 1); // 1 : enable
        }
    }
}
