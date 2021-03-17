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
    public partial class Customer_Add : Form
    {
        SQLConnect r = new SQLConnect();
        String sql;
       
        public Customer_Add()
        {
            InitializeComponent();
        }
        //save code
        private void save()
        {
             
            sql = "Insert into Customers values(1,'" + txtfname.Text + "','" + txtlname.Text + "','" + txtaddress.Text + "')";
            r.Modify(sql);
            MessageBox.Show("Customer has been succesfully added", "Add New record", MessageBoxButtons.OK, MessageBoxIcon.Information);
           
            this.Hide();
            Invoice a = new Invoice(txtfname.Text,txtlname.Text,txtaddress.Text);
            a.Show();

        }

        //validate code
        private void validate()
        {
            if (txtfname.Text == "" && txtlname.Text=="" && txtaddress.Text == "") {
                 MessageBox.Show("Please Complete all fields!", "Add New record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 Label4.Text = "*";
                 Label5.Text = "*";
                 Label6.Text = "*";
            }
            else if (txtfname.Text == "" && txtlname.Text=="")
            {
                MessageBox.Show("Please Complete all fields!", "Add New record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Label4.Text = "*";
                Label5.Text = "*";
              
            }
            else if (txtfname.Text == "")
            {
                MessageBox.Show("Please Complete all fields!", "Add New record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Label4.Text = "*";
                
            }
            else if (txtlname.Text=="") {
                 MessageBox.Show("Please Complete all fields!", "Add New record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                 Label5.Text = "*";
               
            }

            else if (txtaddress.Text == "")
            {
                MessageBox.Show("Please Complete all fields!", "Add New record", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
                Label6.Text = "*";
            }
            else
            {
                save();
            }
      
        }
        //dgcolor
        private void dgColor()
        {
            foreach (DataGridViewRow row in DataGridView1.Rows)
                if (row.Cells["Status"].Value.ToString().Equals("Unpaid"))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                }
        }
        //display to dg

        private void dg()
        {
            sql = @"Select cast(FirstName as varchar(1000))  as [Name],cast(LastName as varchar(1000))  as [Last name],
                    cast(Address as varchar(200)) as 'Address',AcountReceivable as 'Acounts Receivable',DueDate as 'Due Date',Status from BillInfo 
                    inner join Bill on Bill.BillInfoID=BillInfo.ID
                    inner join Customers on Bill.CustomerID=Customers.ID ";
            DataGridView1.DataSource = r.MultipleData(sql).Tables["tbl"];
        }

        //update or 

        //search to dg

        private void search()
        {

            sql = @"Select cast(FirstName as varchar(1000))  as [Name],cast(LastName as varchar(1000))  as [Last name],
                    cast(Address as varchar(200)) as 'Address',AcountReceivable as 'Acounts Receivable',DueDate as 'Due Date',Status from BillInfo 
                    inner join Bill on Bill.BillInfoID=BillInfo.ID 
                    inner join Customers on Bill.CustomerID=Customers.ID where FirstName='" + TextBox1.Text + "'";
        r.DisplaySingle(sql);
        txtfname.Text=r.getf1();
        txtlname.Text = r.getf2();
        txtaddress.Text = r.getf3();
        DataGridView1.DataSource = r.MultipleData(sql).Tables["tbl"];
        dgColor();
        if (txtfname.Text.Equals(r.getf1()) && txtlname.Text.Equals(r.getf2()))
        {
            GroupBox1.Enabled = false;
            Button2.Enabled = false;
        }
        
    
       
     
        }
        private void Button2_Click(object sender, EventArgs e)
        {

            validate();
            
        
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                search();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Billing a = new Billing();
            a.dv = DataGridView1.Rows[DataGridView1.CurrentRow.Index];
            a.Show();
            this.Hide();
        }

        private void Customer_Add_Load(object sender, EventArgs e)
        {
            try
            {
                dg();
                dgColor();
            }
           
              catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            Product a = new Product();
            a.Show();
            this.Hide();
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            main a = new main();
            a.Show();
            this.Hide();
        }
    }
}
