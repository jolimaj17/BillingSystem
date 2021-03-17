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
    public partial class user : Form
    {

        String sql;
        SQLConnect r = new SQLConnect();
        public user()
        {
            InitializeComponent();
        }

        //code for search
        private void search()
        {
            sql = @"SELECT FirstName as 'First Name',LastName as 'Last Name',Username,Password 
                    from Users where FirstName='" + txtSearch.Text + "'";
            r.DisplaySingle(sql);
            txtfname.Text = r.getf1();
            txtlname.Text = r.getf2();
            txtuname.Text = r.getf3();
            txtpassword.Text = r.getf4();

            btnadd.Enabled = false;

            DataGridView1.DataSource = r.MultipleData(sql).Tables["tbl"];
        
           

        }


        //code for datgrid display
        private void dg()
        {
            sql = "SELECT FirstName as 'First Name',LastName as 'Last Name',Username FROM Users";
            DataGridView1.DataSource = r.MultipleData(sql).Tables["tbl"];
        }

        //code for add user
        private void add()
        {

            sql = @"SELECT Username,Password from Users where Username='" + txtuname.Text + "'and Password='"+txtpassword.Text+"'";
            r.DisplaySingle(sql);

            if (txtuname.Text.Equals(r.getf1()) && txtpassword.Text.Equals(r.getf2()))
            {
                MessageBox.Show("Username is already exist!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            else
            {

                sql = @"INSERT INTO Users values('" + txtfname.Text + "','"
                + txtlname.Text + "','"
                + txtuname.Text + "','"
                + txtpassword.Text + "')";
                r.Modify(sql);
                MessageBox.Show("Users successfully added!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
                dg();
            }
            
        }

        //code for clear textbox
        private void clear()
        {
            txtfname.Text = "";
            txtlname.Text = "";
            txtuname.Text = "";
            txtpassword.Text = "";
            txtSearch.Text = "";
        }
      

        //code for update 
        private void update()
        {
            sql = @"UPDATE Users set FirstName='" + txtfname.Text + "',LastName='" + txtlname.Text + 
                "',Username='" + txtuname.Text + "',Password='"+txtpassword.Text+"' where FirstName='"+txtSearch.Text+"'or LastName='"+txtSearch.Text+"'";
            r.Modify(sql);
            MessageBox.Show("Users successfully update!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            clear();
            dg();
        }

        //code for delete
        private void delete()
        {
            sql = "DELETE from Users where FirstName='" + txtSearch.Text + "'or LastName='" + txtSearch.Text + "'";
            r.Modify(sql);
            MessageBox.Show("Users successfully delete!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            clear();
            dg();
        }
        private void user_Load(object sender, EventArgs e)
        {
            dg();
            btnadd.Enabled = true;
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            add();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            update();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            main a = new main();
            a.Show();
            this.Hide();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           
            search();
        }
    }
}
