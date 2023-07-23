using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace SHARIQCatering
{
    public partial class frmlogin : Form
    {
        string cs = @"Data Source=Shariq-PC\SQLEXPRESS;database=catmandb;Trusted_Connection=yes;connection timeout=30;";
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        SqlConnection con1 = null;
        SqlCommand cmd1 = null;
        SqlDataReader rdr1 = null;
        //
        string acc_code = "";
        string auth_code = "";
        string ui_code = "";
        string user_name = "";
        string date_string = "";
        public frmlogin()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (txtbuid.Text == "")
            {
                MessageBox.Show("Enter Valid User ID", "User ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtbuid.Focus();
                return;
            }
            if (txtbupass.Text == "")
            {
                MessageBox.Show("Enter Valid Password", "User Pass", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtbupass.Focus();
                return;
            }
            try
            {
                con = new SqlConnection(cs);
                con1 = new SqlConnection(cs);
                try
                {
                    cmd = new SqlCommand("SELECT * FROM u_code WHERE userid = '" + txtbuid.Text + "' AND password= '" + txtbupass.Text + "'", con);
                    con.Open();
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read() == true)
                    {
                        cmd1 = new SqlCommand("SELECT * FROM urms where uicode='" + Convert.ToString((string)rdr["uicode"]) + "'", con1);
                        con1.Open();
                        rdr1 = cmd1.ExecuteReader();
                        if (rdr1.Read())
                        {
                            acc_code = (Convert.ToString((string)rdr1["acc_level"]));
                            auth_code = (Convert.ToString((string)rdr1["Auth_code"]));
                            ui_code = (Convert.ToString((string)rdr1["uicode"]));
                            user_name = (Convert.ToString((string)rdr1["userid"]));
                        }
                        con1.Close();
                        //
                        Form1 mainfrm = new Form1(acc_code,auth_code,ui_code,user_name,date_string);
                        mainfrm.Show();
                        this.Hide();
                        //
                    }
                    else
                    {
                        MessageBox.Show("Please Enter valid id/password","Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtbuid.Clear();
                        txtbupass.Clear();
                        txtbuid.Focus();
                        con.Close();
                        con1.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            
        }

        private void frmlogin_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
        }

        private void txtbuid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtbuid.Text == "")
                {
                    txtbuid.Focus();
                }
                else
                {
                    txtbupass.Focus();
                }
            }
        }

        private void txtbupass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtbupass.Text == "")
                {
                    txtbupass.Focus();
                }
                else
                {
                    btnlogin.PerformClick();
                }
            }
        }
    }
}
