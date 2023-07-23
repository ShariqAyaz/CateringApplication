using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SHARIQCatering
{
    public partial class Form1 : Form
    {
        #region initiate actions
        string acc_code = "";
        string auth_code = "";
        string ui_code = "";
        string user_name = "";
        string date_string = "";
        #endregion inirtiate actions
        #region behave setting
        public string setmainstat = "0";
        #endregion behave setting
        public Form1(string acccode,string authcode,string userid,string username,string datestring)
        {
            InitializeComponent();
            acc_code = acccode;
            auth_code = authcode;
            ui_code = userid;
            user_name = username;
            date_string = datestring;
        }

        #region access code loading
        private void loadcustmod()
        {
            // true
            ordersManagementToolStripMenuItem.Enabled = true;
            customerToolStripMenuItem.Enabled = true;
            customerMasterToolStripMenuItem.Enabled = true;
            // false
            supplierToolStripMenuItem.Enabled = false;
            productsToolStripMenuItem.Enabled = false;
            suplierMasterToolStripMenuItem.Enabled = false;
            itemMasterToolStripMenuItem.Enabled = false;
            newOrderToolStripMenuItem.Enabled = true;
        }

        private void loadsupmod()
        {
            // false
            ordersManagementToolStripMenuItem.Enabled = false;
            customerToolStripMenuItem.Enabled = false;
            customerMasterToolStripMenuItem.Enabled = false;
            // true
            supplierToolStripMenuItem.Enabled = true;
            productsToolStripMenuItem.Enabled = true;
            suplierMasterToolStripMenuItem.Enabled = true;
            itemMasterToolStripMenuItem.Enabled = true;
            newOrderToolStripMenuItem.Enabled = false;
        }

        private void loadprodmod()
        {
            // true
            ordersManagementToolStripMenuItem.Enabled = false;
            customerToolStripMenuItem.Enabled = false;
            customerMasterToolStripMenuItem.Enabled = false;
            // false
            supplierToolStripMenuItem.Enabled = false;
            productsToolStripMenuItem.Enabled = true;
            suplierMasterToolStripMenuItem.Enabled = false;
            itemMasterToolStripMenuItem.Enabled = true;
            newOrderToolStripMenuItem.Enabled = false;
        }

        private void loadall()
        {
            // true
            ordersManagementToolStripMenuItem.Enabled = true;
            customerToolStripMenuItem.Enabled = true;
            customerMasterToolStripMenuItem.Enabled = true;
            // false
            supplierToolStripMenuItem.Enabled = true;
            productsToolStripMenuItem.Enabled = true;
            suplierMasterToolStripMenuItem.Enabled = true;
            itemMasterToolStripMenuItem.Enabled = true;
            newOrderToolStripMenuItem.Enabled = true;
        }

        #endregion access code loading

        private void Form1_Load(object sender, EventArgs e)
        {
            if (acc_code == "1")
            {
                loadcustmod();
            }
            else if (acc_code == "2")
            {
                loadsupmod();
            }
            else if (acc_code == "3")
            {
                loadprodmod();
            }
            else if (acc_code == "4")
            {
                loadall();
            }
            lblstatususer.Text = user_name;
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            frmlogin frmlogin = new frmlogin();
            frmlogin.Show();
            this.Close();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmlogin frmlogin = new frmlogin();
            frmlogin.Show();
            this.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Customers.frmOrderEntry frmorderentry = new Customers.frmOrderEntry(acc_code, auth_code, ui_code, user_name, date_string);
            frmorderentry.MdiParent = this;
            frmorderentry.BringToFront();
            frmorderentry.Show();
        }

    }
}
