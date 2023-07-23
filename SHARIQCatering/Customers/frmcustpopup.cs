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
    public partial class frmcustpopup : Form
    {
        #region datebase string and instance
        string cs = @"Data Source=Shariq-PC\SQLEXPRESS;database=catmandb;Trusted_Connection=yes;connection timeout=30;";
        SqlConnection concount = null;
        SqlCommand cmdcount = null;
        SqlDataReader rdrcount = null;
        SqlConnection coninsc = null;
        SqlCommand cmdinsc = null;
        #endregion datebase string and instance
        public frmcustpopup()
        {
            InitializeComponent();
        }

        private void txtbuid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void txtbupass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        public int cidcounter = 0;
        public string cname = "";
        public string cphone = "";
        public string cadd = "";
        private void counterc()
        {
            concount = new SqlConnection(cs);
            cmdcount = null;
            try
            {
                cmdcount = new SqlCommand("select count(id) AS COUNT from cust_mast where m_del='0'",concount);
                concount.Open();
                rdrcount = cmdcount.ExecuteReader();
                rdrcount.Read();
                {
                    cidcounter = ((Int32)rdrcount["count"]);
                }
                concount.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                concount.Close();
            }
            cidcounter += 1;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtbuid.Text == "") { MessageBox.Show("Enter Customer Name"); return; }
            if (txtbupass.Text == "") { MessageBox.Show("Enter Phone Number Or leave 0"); return; }
            if (txtbupass1.Text == "") { MessageBox.Show("Enter Address OR Dispatch address"); return; }
            cname = txtbuid.Text;
            cphone = txtbupass.Text;
            cadd = txtbupass1.Text;
            counterc();
            coninsc = new SqlConnection(cs);
            cmdinsc = null;
            try
            {
                cmdinsc = new SqlCommand("insert into cust_mast (cid,cname,phone,addr,m_del) values('"+cidcounter+"','" + txtbuid.Text + "','" + txtbupass.Text + "','" + txtbupass1.Text + "','0')", coninsc);
                coninsc.Open();
                cmdinsc.ExecuteNonQuery();
                coninsc.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                coninsc.Close();
            }
            this.Close();
        }

        private void frmcustpopup_Load(object sender, EventArgs e)
        {

        }
    }
}
