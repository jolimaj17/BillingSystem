using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BillingSystemv3
{

 
    public partial class Form1 : Form
    {
        SQLConnect r = new SQLConnect();
        String sql;
        public Form1()
        {
            InitializeComponent();
        }


       //validation code
        private void validate()
        {
            
            if (txtuname.Text == "" && txtpass.Text == "")
            {
                MessageBox.Show("Please complete all fields!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtuname.Text == "")
            {
                MessageBox.Show("Please Enter your username!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (txtpass.Text == "")
            {
                MessageBox.Show("Please Enter your password!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                login();
            }
        }


        //login code
        private void login()
        {
            string unmae, pass;
            sql = "Select Username,Password from Users where Username='" + txtuname.Text + "'and Password='" + txtpass.Text + "'";
            r.DisplaySingle(sql);


            unmae = r.getf1();
            pass = r.getf2();

        

            
             if ( unmae==txtuname.Text &&  pass==txtpass.Text ) 
             {
                 MessageBox.Show("Access Granted", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);


                 this.Hide();
                 main a = new main();
                 a.Show();
                 
             }
             else 
             {
                 MessageBox.Show("Access Denied", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 txtuname.Clear();
                 txtpass.Clear();
                 
             }
            

           



        }
        private void btnlog_Click(object sender, EventArgs e)
        {
            validate();

        }

        private void btncan_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
