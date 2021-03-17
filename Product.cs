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
    public partial class Product : Form
    {

        SQLConnect r = new SQLConnect();
        String sql;
        public Product()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            Customer_Add a = new Customer_Add();
            a.Show();
        }
        //validation
        private void val()
        {
            if(txtQTY.Text == "" &
            txtUnit.Text  == "" &
            txtDescription.Text == "" &
             txtModel.Text   == "" &
            txtEngine.Text  == "" &
            txtChasis.Text  == "" &
            txtColor.Text  == "" &
            txtType.Text == "")
            {
                MessageBox.Show("Please complete all fields!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                save();
            }
        
        }
        //I
        //clear code
        private void clear()
        {
            txtQTY.Text="";
            txtUnit.Text = "";
            txtDescription.Text = "";
            txtRegPrice.Text = "";
            txtModel.Text = "";
            txtEngine.Text = "";
            txtChasis.Text = "";
            txtColor.Text = "";
            txtType.Text = "";
        }
        //INSERT CODE
        private void save()
        {
            try
            {


                sql = @"Insert into Product values('" + txtQTY.Text +
                    "','" + txtUnit.Text +
                    "','" + txtDescription.Text +
                    "','" + txtRegPrice.Text +
                    "','" + txtModel.Text +
                    "','" + txtEngine.Text +
                    "','" + txtChasis.Text +
                    "','" + txtColor.Text +
                    "','" + txtType.Text +
                    "')";
               
                r.Modify(sql);
                MessageBox.Show("Product successfully added!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dg();
                clear();
                this.Hide();
                Customer_Add a = new Customer_Add();
                a.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //dg
        private void dg()
        {

            try
            {
                sql = @"SELECT QTY,Unit,Description,UnitPrice as 'Unit Price',Model as 'Model',
                        EngineNO as 'Engine#',ChasisNO as 'Chasis#',Color ,Type as 'Type of Body' FROM Product";
                DataGridView1.DataSource = r.MultipleData(sql).Tables["tbl"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

   
        //search
        private void search()
        {
            try
            {
                sql = @"SELECT QTY,Unit,Description,UnitPrice as 'Unit Price',Model as 'Model',
                        EngineNO as 'Engine#',ChasisNO as 'Chasis#',Color ,Type as 'Type of Body' 
                        FROM Product where Model='" + txtSearch.Text + "'";
                DataGridView1.DataSource = r.MultipleData(sql).Tables["tbl"];
                r.DisplaySingle(sql);
                    txtQTY.Text=r.getf1();
                    txtUnit.Text = r.getf2();
                    txtDescription.Text = r.getf3();
                    txtRegPrice.Text = r.getf4();
                    txtModel.Text = r.getf5();
                    txtEngine.Text = r.getf6();
                    txtChasis.Text = r.getf7();
                    txtColor.Text = r.getf8();
                    txtType.Text = r.getf9();
       
                
            }
           
             catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //update
        private void update()
        {

            try
            {
                sql = @"Update Product set QTY='" + txtQTY.Text + "', Unit='" + txtUnit.Text +
                "',Description='" + txtDescription.Text + "',UnitPrice='" + txtRegPrice.Text +
                "',Model='" + txtModel.Text + "',EngineNo='" + txtEngine.Text +
                "',ChasisNo='" + txtChasis.Text + "',Color='" + txtColor.Text +
                "',Type='" + txtType.Text + "' where Model='" + txtSearch.Text + "'";

                r.Modify(sql);
                MessageBox.Show("Product successfully update!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dg();
                this.Hide();
                Customer_Add a = new Customer_Add();
                a.Show();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //delete code
        private void delete()
        {

            try
            {
                sql = @"Delete from Product where Model='" + txtSearch.Text + "'";

                r.Modify(sql);
                MessageBox.Show("Product successfully delete!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dg();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
      
        private void Product_Load(object sender, EventArgs e)
        {
            dg();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            val();
          
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtUnit_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            update();
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            delete();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            search();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
