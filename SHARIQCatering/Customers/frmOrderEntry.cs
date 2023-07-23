using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SHARIQCatering.Customers
{
    public partial class frmOrderEntry : Form
    {
        #region datebase string and instance
        string cs = @"Data Source=Shariq-PC\SQLEXPRESS;database=catmandb;Trusted_Connection=yes;connection timeout=30;";
        SqlConnection conloadorders = null;
        SqlCommand cmdloadorders = null;
        SqlDataReader rdrloadorders = null;
        //
        SqlConnection conloadcust = null;
        SqlCommand cmdloadcust = null;
        SqlDataReader rdrloadcust = null;
        //
        SqlConnection conloadprod = null;
        SqlCommand cmdloadprod = null;
        SqlDataReader rdrloadprod = null;
        //
        // insert new record head
        SqlConnection conhead = null;
        SqlCommand cmdhead = null;
        // insert new record det
        SqlConnection condet = null;
        SqlCommand cmddet = null;
        // Insert new record tran table
        SqlConnection contran = null;
        SqlCommand cmdtran = null;
        // Selections
        SqlConnection conseleno = null;
        SqlCommand cmdseleno = null;
        SqlDataReader rdrseleno = null;
        //
        SqlConnection conselcid= null;
        SqlCommand cmdselcid = null;
        SqlDataReader rdrselcid = null;
        //
        SqlConnection conselcname= null;
        SqlCommand cmdselcname = null;
        SqlDataReader rdrselcname = null;
        //
        SqlConnection conselpid = null;
        SqlCommand cmdselpid = null;
        SqlDataReader rdrselpid = null;
        //
        SqlConnection conselpname = null;
        SqlCommand cmdselpname = null;
        SqlDataReader rdrselpname = null;
        //
        SqlConnection conchkcname = null;
        SqlDataReader rdrchkcname = null;
        SqlCommand cmdchkcname = null;
        #endregion datebase string and instence
        #region initiate actions
        string acc_code = "";
        string auth_code = "";
        string ui_code = "";
        string user_name = "";
        string date_string = "";
        #endregion inirtiate actions
        #region form operations
        string atag = "0";
        string ctag = "1";
        string datestring = "";
        #endregion form operations
        public frmOrderEntry(string acccode, string authcode, string userid, string username, string datestring)
        {
            InitializeComponent();
            acc_code = acccode;
            auth_code = authcode;
            ui_code = userid;
            user_name = username;
            date_string = datestring;
        }

        #region date textbox validation
        private void maskdcdate_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if (!e.IsValidInput)
            {
                toolTip1.ToolTipTitle = "Invalid Date Value";
                toolTip1.Show("آپ نے غلط تاریخ کا انتخاب کیا ہے! تریقہ ہے 'تاریخ پھر مہینا پھر سال.", maskdcdate, 5000);
                e.Cancel = true;
            }
        }

        private void maskdcdate_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            toolTip1.ToolTipTitle = "Invalid Input";
            toolTip1.Show("آپ نے غلط تاریخ کا انتخاب کیا ہے! تریقہ ہے 'تاریخ پھر مہینا پھر سال.", maskdcdate, maskdcdate.Location, 5000);
        }
        #endregion date textbox validation

        #region load classes and Counters is here
        private void loadorders()
        {
            cboxeno.Items.Clear();
            conloadorders = new SqlConnection(cs);
            cmdloadorders = null;
            try
            {
                cmdloadorders = new SqlCommand("select bill_no from cbill_head where m_del='0'", conloadorders);
                conloadorders.Open();
                rdrloadorders = cmdloadorders.ExecuteReader();
                while (rdrloadorders.Read() == true)
                {
                    cboxeno.Items.Add((string)rdrloadorders["bill_no"]);
                }
                conloadorders.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                conloadorders.Close();
            }

        }

        private void loadcust()
        {
            cboxcid.Items.Clear();
            cboxcname.Items.Clear();
            conloadcust = new SqlConnection(cs);
            cmdloadcust = null;
            try
            {
                cmdloadcust = new SqlCommand("select cid,cname from cust_mast where m_del='0'", conloadcust);
                conloadcust.Open();
                rdrloadcust = cmdloadcust.ExecuteReader();
                while (rdrloadcust.Read() == true)
                {
                    cboxcid.Items.Add((string)rdrloadcust["cid"]);
                    cboxcname.Items.Add((string)rdrloadcust["cname"]);
                }
                conloadcust.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                conloadcust.Close();
            }
        }

        private void loadprod()
        {
            cboxpid.Items.Clear();
            cboxpname.Items.Clear();
            conloadprod = new SqlConnection(cs);
            cmdloadprod = null;
            try
            {
                cmdloadprod = new SqlCommand("select pid,pname from prod_mast where m_del='0'", conloadprod);
                conloadprod.Open();
                rdrloadprod = cmdloadprod.ExecuteReader();
                while (rdrloadprod.Read() == true)
                {
                    cboxpid.Items.Add((string)rdrloadprod["pid"]);
                    cboxpname.Items.Add((string)rdrloadprod["pname"]);
                }
                conloadprod.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                conloadprod.Close();
            }
        }
        #endregion load classes is here

        #region list view total
        public decimal amttot()
        {
            int i = 0;
            decimal j = 0;
            decimal l = 0;
            i = 0;
            j = 0;
            l = 0;
            try
            {
                j = listView1.Items.Count;
                for (i = 0; i <= j - 1; i++)
                {
                    l = l + Convert.ToDecimal(listView1.Items[i].SubItems[5].Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return l;
        }

        public int itemcount()
        {
            int i = 0;
            int j = 0;
            int l = 0;
            i = 0;
            j = 0;
            l = 0;
            try
            {
                j = listView1.Items.Count;
                for (i = 0; i <= j - 1; i++)
                {
                    l = l + Convert.ToInt32(listView1.Items[i].SubItems[5].Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return l;
        }
        #endregion list view total
        private void frmOrderEntry_Load(object sender, EventArgs e)
        {
            #region load data
            loadorders();
            loadcust();
            loadprod();
            #endregion load data
            lblstatususer.Text = user_name;
            #region masktextbox masking
            maskdcdate.Mask = "00/00/0000";
            maskdcdate.MaskInputRejected += new MaskInputRejectedEventHandler(maskdcdate_MaskInputRejected);
            maskdcdate.ValidatingType = typeof(System.DateTime);
            maskdcdate.TypeValidationCompleted += new TypeValidationEventHandler(maskdcdate_TypeValidationCompleted);
            #endregion masktextbox masking
            btn_add.Focus();
        }

        private void ClearTextBoxes(Control.ControlCollection cc)
        {
            foreach (Control ctrl in cc)
            {
                TextBox tb = ctrl as TextBox;
                if (tb != null)
                    tb.Text = "";
                else
                    ClearTextBoxes(ctrl.Controls);
            }
        }

        private void ClearComboBoxes(Control.ControlCollection cc)
        {
            foreach (Control ctrl in cc)
            {
                ComboBox tb = ctrl as ComboBox;
                if (tb != null)
                    tb.Text = "";
                else
                    ClearComboBoxes(ctrl.Controls);
            }
        }

        #region ADD/EDIT/DELETE/PRINT OPERATION
        private void btn_add_Click(object sender, EventArgs e)
        {
            if (auth_code == "3") { MessageBox.Show("You Are not Authorized"); return; }
            //
            if (btn_add.Text == "&Add")
            {
                ClearTextBoxes(this.Controls);
                ClearComboBoxes(this.Controls);
                cboxeno.Enabled = false;
                maskdcdate.Enabled = true;
                cboxcid.Enabled = true;
                cboxcname.Enabled = true;
                txtbcphone.Enabled = true;
                txtbcadd.Enabled = true;
                cboxpid.Enabled = true;
                cboxpname.Enabled = true;
                txtbirate.Enabled = true;
                txtbiqty.Enabled = true;
                txtbotheramt.Enabled = true;
                txtbAdvance.Enabled = true;
                btniAdd.Enabled = true;
                btniDel.Enabled = true;
                txtbRem.ReadOnly = false;
                txtbtotamt.Text = "0";
                txtbAdvance.Text = "0";
                txtbBalance.Text = "0";
                txtbotheramt.Text = "0";
                listView1.Items.Clear();
                btn_add.Text = "&Save";
                btn_edit.Text = "&Undo";
                maskdcdate.Focus();
                maskdcdate.Select(0, 2);
                atag = "0";
            }
            else if (btn_add.Text == "&Save")
            {
                if (atag == "0")
                {
                    #region Check customer name for not exist
                    #endregion Check customer name for not exist
                    #region add new record
                    #endregion add new record
                }
                else if (atag == "1")
                {
                    #region edit selected record
                    #endregion edit selected record
                }
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (btn_edit.Text == "&Edit")
            {
                if (auth_code == "3")
                {
                    MessageBox.Show("You are not authorized to do that... for more info. contact your software administrator or dial 03422770074");
                    return;
                }
                btn_add.Text = "&Save";
                btn_edit.Text = "&Undo";
                atag = "1";
            }
            else if (btn_edit.Text == "&Undo")
            {
                if (atag == "0")
                {
                    ClearTextBoxes(this.Controls);
                    ClearComboBoxes(this.Controls);
                    txtbtotamt.Text = "0";
                    txtbAdvance.Text = "0";
                    txtbBalance.Text = "0";
                    txtbotheramt.Text = "0";
                    listView1.Items.Clear();
                }
                cboxeno.Enabled = true;
                maskdcdate.Enabled = false;
                cboxcid.Enabled = false;
                cboxcname.Enabled = false;
                txtbcphone.Enabled = false;
                txtbcadd.Enabled = false;
                cboxpid.Enabled = false;
                cboxpname.Enabled = false;
                txtbirate.Enabled = false;
                txtbiqty.Enabled = false;
                txtbotheramt.Enabled = false;
                txtbAdvance.Enabled = false;
                btniAdd.Enabled = false;
                btniDel.Enabled = false;
                txtbRem.ReadOnly = true;
                atag = "0";
                btn_add.Text = "&Add";
                btn_edit.Text = "&Edit";
            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }
        #endregion ADD/EDIT/DELETE OPERATION

        #region Keydown Enter events
        private void maskdcdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cboxcname.Focus();
            }
        }

        private void cboxcname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cboxcname.Text == "")
                {
                    MessageBox.Show("Please Enter Customers Name");
                    cboxcname.Focus();
                }
                else
                {
                    cboxpid.Focus();
                }
            }
        }

        private void cboxcid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cboxcid.Text == "")
                {
                    cboxcname.Focus();
                }
                else
                {
                    txtbcphone.Focus();
                }
            }
        }

        private void txtbcphone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtbcphone.Text == "")
                {
                    MessageBox.Show("Please Enter Customer Phone Number");
                    txtbcphone.Focus();
                }
                else
                {
                    txtbcadd.Focus();
                }
            }
        }

        private void txtbcadd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cboxpid.Focus();
            }
        }

        private void cboxpid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cboxpid.Text == "")
                {
                    cboxpname.Focus();
                }
                else
                {
                    cboxpname.Focus();
                }
            }
        }

        private void cboxpname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cboxpname.Text == "")
                {
                    MessageBox.Show("Please Enter Product OR Dish");
                    cboxpname.Focus();
                }
                else
                {
                    txtbirate.Focus();
                }
            }
        }

        private void txtbirate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtbirate.Text == "")
                {
                    MessageBox.Show("Please Enter Rate of Product/Dish");
                    txtbirate.Focus();
                }
                else if (txtbirate.Text == "0")
                {
                    MessageBox.Show("Please Enter Rate of Product/Dish");
                    txtbirate.Focus();
                }
                else
                {
                    txtbiqty.Focus();
                }
            }
        }

        private void txtbiqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtbiqty.Text == "")
                {
                    MessageBox.Show("Please Enter Quantity of Product/Dish");
                    txtbiqty.Focus();
                }
                else if (txtbirate.Text == "0")
                {
                    MessageBox.Show("Please Enter Quantity of Product/Dish");
                    txtbiqty.Focus();
                }
                else
                {
                    btniAdd.Focus();
                }
            }
        }

        private void txtbAdvance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtbAdvance.Text == "")
                {
                    txtbAdvance.Text = "0";
                    txtbRem.Focus();
                }
                else
                {
                    txtbRem.Focus();
                }
            }
        }
        #endregion Keydown Enter events

        #region Items add / remove
        private void btniAdd_Click(object sender, EventArgs e)
        {
            if (btn_add.Text != "&Add")
            {
                #region check empty
                //if (cboxpname.Text == "") { MessageBox.Show("Please Enter Product"); return; } 
                if (cboxpname.Text == "") { MessageBox.Show("Please Enter Product"); return; }
                if (txtbirate.Text == "") { MessageBox.Show("Please Enter Rate"); return; }
                if (txtbirate.Text == "0") { MessageBox.Show("Please Enter Rate"); return; }
                if (txtbiqty.Text == "") { MessageBox.Show("Please Enter Quantity"); return; }
                if (txtbiqty.Text == "0") { MessageBox.Show("Please Enter Quantity"); return; }
                #endregion check empty
                ListViewItem lst = new ListViewItem();
                lst.SubItems.Add(cboxpid.Text);
                lst.SubItems.Add(cboxpname.Text);
                lst.SubItems.Add(txtbirate.Text);
                lst.SubItems.Add(txtbiqty.Text);
                lst.SubItems.Add(Convert.ToString(Convert.ToDecimal(txtbirate.Text) * Convert.ToDecimal(txtbiqty.Text)));
                listView1.Items.Add(lst);
                // counter qty
                txtbtotamt.Text = Convert.ToString(Convert.ToDecimal(txtbotheramt.Text)+amttot());
                txtbBalance.Text = Convert.ToString(Convert.ToDecimal(txtbotheramt.Text) + amttot()-Convert.ToDecimal(txtbAdvance.Text));
            }
        }

        private void btniDel_Click(object sender, EventArgs e)
        {
            if (btn_add.Text != "&Add")
            {
                if (listView1.Items.Count == 0)
                {
                    MessageBox.Show("No items to remove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    listView1.FocusedItem.Remove();
                    txtbtotamt.Text = Convert.ToString(Convert.ToDecimal(txtbotheramt.Text) + amttot());
                    txtbBalance.Text = Convert.ToString(Convert.ToDecimal(txtbotheramt.Text) + amttot() - Convert.ToDecimal(txtbAdvance.Text));
                }
            }
        }
        #endregion Items add / remove

        #region valid input region
        private void cboxcid_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allow numbers (-) (.)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion valid input region

        #region Got focus and leave focus changes
        private void cboxeno_Enter(object sender, EventArgs e)
        {
            ComboBox tb = (ComboBox)sender;
            if (tb != null)
            {
                tb.BackColor = Color.LightCyan;
                tb.ForeColor = Color.DarkBlue;
            }
        }

        private void cboxeno_Leave(object sender, EventArgs e)
        {
            ComboBox tb = (ComboBox)sender;
            if (tb != null)
            {
                tb.BackColor = Color.White;
                tb.ForeColor = Color.Black;
            }
            #region leave from cboxcname or customer name combobox
            if (tb.Name == "cboxcname")
            {
                if (tb.Text != "")
                {
                    conchkcname = new SqlConnection(cs);
                    cmdchkcname = null;
                    try
                    {
                        cmdchkcname = new SqlCommand("select * from cust_mast where m_del='0' AND cname='"+tb.Text+"'", conchkcname);
                        conchkcname.Open();
                        rdrchkcname = cmdchkcname.ExecuteReader();
                        if (rdrchkcname.Read() == true)
                        {
                            // do nothig fillhaal
                        }
                        else
                        {
                            frmcustpopup frmcpop = new frmcustpopup();
                            frmcpop.ShowDialog();
                            // get fuck value
                            cboxcid.Text= Convert.ToString(frmcpop.cidcounter);
                            cboxcname.Text = frmcpop.cname;
                            txtbcphone.Text = frmcpop.cphone;
                            txtbcadd.Text = frmcpop.cadd;
                            cboxpid.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        conchkcname.Close();
                    }
                }
            }
            #endregion leave from cboxcname or customer name combobox
            #region leave from product name combobox
            if (tb.Name == "cboxpname")
            {
                if (tb.Text != "")
                {
                    conchkcname = new SqlConnection(cs);
                    cmdchkcname = null;
                    try
                    {
                        cmdchkcname = new SqlCommand("select * from prod_mast where m_del='0' AND pname='" + tb.Text + "'", conchkcname);
                        conchkcname.Open();
                        rdrchkcname = cmdchkcname.ExecuteReader();
                        if (rdrchkcname.Read() == true)
                        {
                            // do nothig fillhaal
                        }
                        else
                        {
                            SHARIQCatering.Products.frmprodpopup frmcpop = new SHARIQCatering.Products.frmprodpopup();
                            frmcpop.ShowDialog();
                            // get fuck value
                            cboxpid.Text = Convert.ToString(frmcpop.cidcounter);
                            cboxpname.Text = frmcpop.pname;
                            txtbirate.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        conchkcname.Close();
                    }
                }
            }
            #endregion leave from product name combobox
        }

        private void txtbcphone_Enter(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb != null)
            {
                tb.BackColor = Color.LightCyan;
                tb.ForeColor = Color.DarkBlue;
                tb.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void txtbcphone_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb != null)
            {
                tb.BackColor = Color.White;
                tb.ForeColor = Color.Black;
                tb.BorderStyle = BorderStyle.Fixed3D;
            }
        }

        private void maskdcdate_Enter(object sender, EventArgs e)
        {
            MaskedTextBox tb = (MaskedTextBox)sender;
            if (tb != null)
            {
                tb.BackColor = Color.LightCyan;
                tb.ForeColor = Color.DarkBlue;
                tb.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void maskdcdate_Leave(object sender, EventArgs e)
        {
            MaskedTextBox tb = (MaskedTextBox)sender;
            if (tb != null)
            {
                tb.BackColor = Color.White;
                tb.ForeColor = Color.Black;
                tb.BorderStyle = BorderStyle.Fixed3D;
            }
        }
        #endregion Got focus and leave focus changes

        #region closing events
        private void frmOrderEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btn_add.Text == "&Save")
            {
                e.Cancel = true;
                MessageBox.Show("Save / Undo your work before exit.");
                return;
            }
            else
            {
                if (ctag == "1")
                {
                    ctag = "0";
                    this.Close();
                }
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (btn_add.Text == "&Save")
            {
                MessageBox.Show("Save / Undo your work before exit.");
                return;
            }
            else
            {
                ctag = "0";
                this.Close();
            }
        }
        #endregion closing events

        #region Select Index Change
        private void cboxeno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (btn_add.Text == "&Add")
            {
                if (cboxeno.Text !="")
                {
                    listView1.Items.Clear();
                    #region load head
                    conseleno = new SqlConnection(cs);
                    cmdseleno = null;
                    try
                    {
                        cmdseleno = new SqlCommand("select * from cbill_head where m_del='0' AND bill_no='"+cboxeno.Text+"'", conseleno);
                        conseleno.Open();
                        rdrseleno = cmdseleno.ExecuteReader();
                        rdrseleno.Read();
                        {
                            maskdcdate.Text = ((DateTime)rdrseleno["bill_date"]).ToString("dd/MM/yyyy").Replace("/", "");
                            cboxcid.Text = ((string)rdrseleno["com_id"]);
                            txtbotheramt.Text = ((decimal)rdrseleno["other_amt"]).ToString();
                            txtbtotamt.Text = ((decimal)rdrseleno["tot_amt"]).ToString();
                            txtbAdvance.Text = ((decimal)rdrseleno["adv_amt"]).ToString();
                            txtbBalance.Text = ((decimal)rdrseleno["bal_amt"]).ToString();
                        }
                        conseleno.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        conseleno.Close();
                        return;
                    }
                    #endregion load head
                    #region load det
                    conseleno = new SqlConnection(cs);
                    cmdseleno = null;
                    try
                    {
                        cmdseleno = new SqlCommand("select * from cbill_det where m_del='0' AND bill_no='"+cboxeno.Text+"'", conseleno);
                        conseleno.Open();
                        rdrseleno = cmdseleno.ExecuteReader();
                        while (rdrseleno.Read() == true)
                        {
                            ListViewItem lst = new ListViewItem();
                            lst.SubItems.Add((string)rdrseleno["pid"]);
                            lst.SubItems.Add((string)rdrseleno["pname"]);
                            lst.SubItems.Add(Convert.ToString((decimal)rdrseleno["prate"]));
                            lst.SubItems.Add(Convert.ToString((Int32)rdrseleno["pqty"]));
                            lst.SubItems.Add(Convert.ToString((decimal)rdrseleno["line_amt"]));
                            listView1.Items.Add(lst);
                            //txtbtotamt.Text = amttot().ToString();
                            //txtbtotamt.Text = itemcount().ToString();
                        }
                        conseleno.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        conseleno.Close();
                        return;
                    }
                    #endregion load det
                }
            }
        }

        private void cboxcid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboxcid.Text != "")
            {
                #region select customer details
                conselcid = new SqlConnection(cs);
                cmdselcid = null;
                try
                {
                    cmdselcid = new SqlCommand("select cname,phone,addr from cust_mast where m_del='0' AND cid='" + cboxcid.Text + "'", conselcid);
                    conselcid.Open();
                    rdrselcid = cmdselcid.ExecuteReader();
                    rdrselcid.Read();
                    {
                        cboxcname.Text = ((string)rdrselcid["cname"]);
                        txtbcphone.Text=((string)rdrselcid["phone"]);
                        txtbcadd.Text = ((string)rdrselcid["addr"]);
                    }
                    conselcid.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    conselcid.Close();
                }
                #endregion select customer details
            }
        }

        private void cboxcname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboxcname.Text != "")
            {
                #region select customer details
                conselcname = new SqlConnection(cs);
                cmdselcname = null;
                try
                {
                    cmdselcname = new SqlCommand("select cid,phone,addr from cust_mast where m_del='0' AND cname='" + cboxcname.Text + "'", conselcname);
                    conselcname.Open();
                    rdrselcname = cmdselcname.ExecuteReader();
                    rdrselcname.Read();
                    {
                        cboxcid.Text = ((string)rdrselcname["cid"]);
                        txtbcphone.Text = ((string)rdrselcname["phone"]);
                        txtbcadd.Text = ((string)rdrselcname["addr"]);
                    }
                    conselcname.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    conselcname.Close();
                }
                #endregion select customer details
            }
        }

        private void cboxpid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboxpid.Text != "")
            {
                #region select product details
                conselpid = new SqlConnection(cs);
                cmdselpid = null;
                try
                {
                    cmdselpid = new SqlCommand("select pname from prod_mast where m_del='0' AND pid='" + cboxpid.Text + "'", conselpid);
                    conselpid.Open();
                    rdrselpid = cmdselpid.ExecuteReader();
                    rdrselpid.Read();
                    {
                        cboxpname.Text = ((string)rdrselpid["pname"]);
                    }
                    conselpid.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    conselpid.Close();
                }
                #endregion select product details
            }
        }

        private void cboxpname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboxpname.Text != "")
            {
                #region select customer details
                conselpname = new SqlConnection(cs);
                cmdselpname = null;
                try
                {
                    cmdselpname = new SqlCommand("select pid from prod_mast where m_del='0' AND pname='" + cboxpname.Text + "'", conselpname);
                    conselpname.Open();
                    rdrselpname = cmdselpname.ExecuteReader();
                    rdrselpname.Read();
                    {
                        cboxpid.Text = ((string)rdrselpname["pid"]);
                    }
                    conselpname.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    conselpname.Close();
                }
                #endregion select customer details
            }
        }
        #endregion select Index Change

        #region TextChange 
        private void txtbotheramt_TextChanged(object sender, EventArgs e)
        {
            if (txtbotheramt.Text != "")
            {
                txtbBalance.Text = Convert.ToString(Convert.ToDecimal(txtbotheramt.Text) + amttot() - Convert.ToDecimal(txtbAdvance.Text));
                txtbtotamt.Text = Convert.ToString(Convert.ToDecimal(txtbotheramt.Text) + amttot());
            }
            else
            {
                txtbotheramt.Text = "0";
            }
        }

        private void txtbtotamt_TextChanged(object sender, EventArgs e)
        {
            if (txtbtotamt.Text != "")
            {
                txtbtotamt.Text = Convert.ToString(Convert.ToDecimal(txtbotheramt.Text) + amttot());
            }
            else
            {
                txtbtotamt.Text = "0";
            }
        }

        private void txtbAdvance_TextChanged(object sender, EventArgs e)
        {
            if (txtbAdvance.Text != "")
            {
                txtbBalance.Text = Convert.ToString(Convert.ToDecimal(txtbotheramt.Text) + amttot() - Convert.ToDecimal(txtbAdvance.Text));
            }
            else
            {
                txtbAdvance.Text = "0";
            }
        }
        #endregion TextChange

    }
}
