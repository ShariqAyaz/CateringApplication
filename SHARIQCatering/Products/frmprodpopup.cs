using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SHARIQCatering.Products
{
    public partial class frmprodpopup : Form
    {
        #region datebase string and instance
        string cs = @"Data Source=Shariq-PC\SQLEXPRESS;database=catmandb;Trusted_Connection=yes;connection timeout=30;";
        SqlConnection concount = null;
        SqlCommand cmdcount = null;
        SqlDataReader rdrcount = null;
        SqlConnection coninsc = null;
        SqlCommand cmdinsc = null;
        #endregion datebase string and instance
        public frmprodpopup()
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
        public string pname = "";
        private void counterc()
        {
            concount = new SqlConnection(cs);
            cmdcount = null;
            try
            {
                cmdcount = new SqlCommand("select count(id) AS COUNT from prod_mast where m_del='0'",concount);
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
            pname = txtbuid.Text;
            counterc();
            coninsc = new SqlConnection(cs);
            cmdinsc = null;
            try
            {
                cmdinsc = new SqlCommand("insert into prod_mast (pid,pname,pcat,uid,m_del) values('"+cidcounter+"','" + txtbuid.Text + "','','','0')", coninsc);
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
    }
}
