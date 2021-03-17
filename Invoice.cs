using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BillingSystemv3
{
    public partial class Invoice : Form
    {

        SQLConnect r = new SQLConnect();
        String sql;
        PrintDocument document = new PrintDocument();
        PrintDialog dialog = new PrintDialog();
        public Invoice(string fname,string lname,string addd)
        {
            InitializeComponent();
            customername.Text = fname.ToString() + " " + lname.ToString();
            lbladd.Text = addd.ToString();
            document.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
        }

        //Code for combodis'
        private void cmbdis()
        {
            sql = "select Description from Product";
            comboBox1.DataSource = r.MultipleData(sql).Tables["tbl"];
            comboBox1.DisplayMember = "Description";
            comboBox1.ValueMember = "Description";
            

        }
        //validation
        private void validation()
        {
            int down=1000, monthly=1000;

            if (Convert.ToInt16(txtMonthly.Text) < monthly)
            {
                MessageBox.Show("Payment must not lower than 1000", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMonthly.Text = "";
                ss.Text = "1000";
            }
            else if (Convert.ToInt16(ss.Text) < down)
            {
                MessageBox.Show("Payment must not lower than 1000", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMonthly.Text = "";
                ss.Text = "1000";
            }
            else if ((Convert.ToInt16(txtMonthly.Text) < monthly)||(Convert.ToInt16(ss.Text) < down))
            {
                MessageBox.Show("Downpayment or Monthly must not lower than 1000", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMonthly.Text = "";
                ss.Text = "1000";
            }
            else
            {
                save();
                display();
                prints();

                Customer_Add a = new Customer_Add();
                a.Show();
                this.Hide();
            }
        }
        //dis
        private void dis()
        {
            sql = "SELECT Unit,Model,EngineNo,ChasisNo,Color,Type from Product where Description='" + comboBox1.Text + "'";
            r.DisplaySingle(sql);
            textBox1.Text = r.getf1();
            txtModel.Text = r.getf2();
            txtEngine.Text = r.getf3();
            txtChasis.Text = r.getf4();
            txtColor.Text = r.getf5();
            txtType.Text = r.getf6();
            dates.Text = DateTime.Now.ToShortDateString();
        }
        //random
        private String invoice()
        {
          Random r = new Random();
            string str = "";
            for (int i = 0; i < 6; i++)
                str = str + (r.Next(5)+ 1).ToString();
            return str;
        }
        //save code
        private void save()
        {
            int id = 1;
            try
            {

                //insert to BilInfo
                sql = "Insert into BillInfo values('" + invoice() +
                    "','" + txtTerms.Text +
                    "','" + txtTIP.Text +
                    "','" + ss.Text +
                    "','" + txtPN.Text +
                    "','" + txtMonthly.Text +
                    "')";
                r.Modify(sql);

                //insert to Bill

                sql = @"Insert into Bill values((Select Top 1(ID) from Customers order by ID desc),
                        (Select ID from Product where Description='" + comboBox1.Text +"'),(Select Top 1(ID) from BillInfo order by ID desc)" +
                        ",'"+ dates.Text +"','" 
                        + txtPN.Text +"','"+ DateTimePicker1.Value.ToString("yyyy-MM-dd")+"','Unpaid')";

                r.Modify(sql);

                //update to product

                sql = @"UPDATE Product set QTY=QTY-1 where Description='" + comboBox1.Text + "'";
                r.Modify(sql);

                MessageBox.Show("Successfully Purchase", "Add New record", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
          
        }
        //formula code
        private void formula()
        {
            double price, total, vat;


            price = Convert.ToDouble(unitpr.Text);
            tot.Text = price.ToString();
            vat = price * 0.12;
            less.Text = vat.ToString();
            total = price + vat;
            totalamo.Text = total.ToString();
            txtTIP.Text = total.ToString();

        }
        private void Invoice_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
            try
            {
               cmbdis();
               lblinvoice.Text=invoice();
               
               dis();


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                sql = "select UnitPrice from Product where Description='" + comboBox1.Text + "'";
                r.DisplaySingle(sql);
                unitpr.Text = r.getf1();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void unitpr_TextChanged(object sender, EventArgs e)
        {
            formula();
        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void txtDP_TextChanged(object sender, EventArgs e)
        {
            decimal down, tot,pn;
            tot = Convert.ToDecimal(totalamo.Text);
            down = Convert.ToDecimal(ss.Text);
            pn = tot - down;
            txtPN.Text = pn.ToString();

        }
        //code for printing
        private void display()
        {
            textBox2.AppendText("\t\t" + "         SALES INVOICE" + Environment.NewLine + Environment.NewLine);
            textBox2.AppendText("Invoice No. :" + " " + lblinvoice.Text + "\t\t\t\t" + "Date:" + " " + DateTime.Now.ToShortDateString() + Environment.NewLine + Environment.NewLine);
            textBox2.AppendText("Sold to: " + " " + customername.Text + Environment.NewLine + "\t" +"Address:" + " " + lbladd.Text + Environment.NewLine );
            textBox2.AppendText("TIP:" + txtTIP.Text + Environment.NewLine);
            textBox2.AppendText("Terms:" + " " + txtTerms.Text + Environment.NewLine);
            textBox2.AppendText("QTY: " + " " + txtquant.Text + "\t" + "Down Payment:" + ss.Text + Environment.NewLine);
            textBox2.AppendText("Unit: " + " " + textBox1.Text + "\t\t" + "PN:" + txtPN.Text + Environment.NewLine);
            textBox2.AppendText("Description: " + " " + comboBox1.Text + Environment.NewLine + "Monthly Installment: " + txtMonthly.Text + Environment.NewLine);
            textBox2.AppendText("Model: " + " " + txtModel.Text + "\t\t" + "Engine#: " + txtEngine.Text + Environment.NewLine);
            textBox2.AppendText("Chasis#: " + " " + txtChasis.Text + "\t\t" + "Color: " + txtColor.Text + Environment.NewLine);
            textBox2.AppendText("Type of body: " + " " + txtType.Text + Environment.NewLine);
            textBox2.AppendText("Unit Price" + " " + unitpr.Text + "\t\t");
            textBox2.AppendText("Total Sales (VAT Inclusive):" + " " + tot.Text + Environment.NewLine);
            textBox2.AppendText("Less: VAT" + " " + less.Text + Environment.NewLine + Environment.NewLine);
            textBox2.AppendText("TOTAL AMOUNT DUE: " + " " + totalamo.Text + Environment.NewLine);

           
        }

        //print
        private void prints()
        {
            dialog.Document = document;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                document.Print();
            }
        }
        private void Label11_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if ((DateTimePicker1.Value) <= (DateTime.Now))
            {
                MessageBox.Show("Due date is invalid", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                validation();

               
            }
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            decimal down, tot, pn;
            tot = Convert.ToDecimal(totalamo.Text);
            down = Convert.ToDecimal(ss.Text);
            pn = tot - down;
            txtPN.Text = pn.ToString();
            button3.Enabled = true;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(textBox2.Text, new Font("Arial", 15, FontStyle.Regular),Brushes.Black,20,20);
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTerms_TextChanged(object sender, EventArgs e)
        {
           

        }

        private void txtDP_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void ss_TextChanged(object sender, EventArgs e)
        {
           

          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtquant_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void txtMonthly_TextChanged(object sender, EventArgs e)
        {
            
           
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            display();
            
        }
    }
}
