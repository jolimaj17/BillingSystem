using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
namespace BillingSystemv3
{
    public partial class Billing : Form
    {

        SQLConnect r = new SQLConnect();
        String sql;
        public DataGridViewRow dv;
        PrintDocument document = new PrintDocument();
        PrintDialog dialog = new PrintDialog();
        public Billing()
        {
            InitializeComponent();
            document.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
        }
        //print
        private void print()
        {
            dialog.Document = document;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                document.Print();
            }
        }

        //display to text
        private void displayToText()
        {
            double less,i,be;
            dates.Text = DateTime.Now.ToShortDateString();
            txtReceived.Text= dv.Cells[0].Value.ToString() + " " + dv.Cells[1].Value.ToString();
            txtaddress.Text= dv.Cells[2].Value.ToString();
            
            //display monthly
            sql = @"SELECT Monthly from BillInfo 
                    inner join Bill on Bill.BillInfoID=BillInfo.ID
                    inner join Customers on Bill.CustomerID=Customers.ID
                    where FirstName='" + dv.Cells[0].Value.ToString() + "'";
            r.DisplaySingle(sql);

            //formula
            monthly.Text = r.getf1();
            be = Convert.ToDouble(monthly.Text);
            less = Convert.ToDouble(264.64);
            lblles.Text = less.ToString();
            i = be - less;
            lbltst.Text = i.ToString();
            lbltd.Text = be.ToString();

            

            //display date
            
            double rebate,mon;
            string monthlys;
            double emp, dp, total,penalty;
            sql = @"SELECT CAST(DueDate as varchar) from Bill
                 inner join Customers on Bill.CustomerID=Customers.ID
                 where FirstName='" + dv.Cells[0].Value.ToString() + "'";
            r.DisplaySingle(sql);
            duedateexact.Text = r.getf1();

            //display monthly
            sql = @"Select cast(Monthly as decimal(10,2)) from BillInfo
                    inner join Bill on BillInfo.ID=Bill.BillInfoID
                    inner join Customers on Bill.CustomerID=Customers.ID
                    where FirstName='" + dv.Cells[0].Value.ToString() + "'";
            r.DisplaySingle(sql);
            monthlys = r.getf1();

            if (DateTime.Now < duedateexact.Value)
            {
                
                rebate = 250;
                lblrebate.Text = rebate.ToString();
                i = (be - less) - rebate;
                tttotal.Text = i.ToString();
            }
            else 
            {
                double delay,to;


                delay = (DateTime.Now - duedateexact.Value).TotalDays;
                mon = Convert.ToDouble(monthlys);
                penalty = Convert.ToDouble(.04);
                emp = (mon * penalty) / 30*delay;
                total = emp;
                lblpenalty.Text= total.ToString();
                to = mon + total;
                tttotal.Text = to.ToString();
    
            }

        }
        //display bala
        private void disbal()
        {
            //display balance
            sql = @"SELECT cast(AcountReceivable as decimal(10,2)) from Bill
                inner join Customers on Bill.CustomerID=Customers.ID
                where FirstName='" + dv.Cells[0].Value.ToString() + "'";
            r.DisplaySingle(sql);
            lblreceivable.Text = r.getf1();

            if (lblreceivable.Text == "0.00")
            {
                //id
                sql = @"Select CustomerID from Bill
                inner join Customers on Bill.CustomerID=Customers.ID
                where FirstName='" + dv.Cells[0].Value.ToString() + "'";
                r.DisplaySingle(sql);

                sql = "Update Bill set Status='Paid' where CustomerID='" + r.getf1().ToString() + "'";
                r.Modify(sql);
            }
            else
            {

            }
        }
        //total code
        private void total()
        {
            
            decimal tot, bal, cash;
            cash = Convert.ToDecimal(txtCash.Text);
            bal = Convert.ToDecimal(lblreceivable.Text);
            tot = bal - cash;
            totalamo.Text =  Convert.ToDecimal(tot).ToString();
        }

        //save code
        private void save()
        {
            try
            {
                string id;
                sql = "SELECT ID from Customers where FirstName='" + dv.Cells[0].Value.ToString() + "'";
                r.DisplaySingle(sql);
                id = r.getf1();

                sql = @"INSERT INTO Receipt values(
                     '" + id.ToString() + "','"
                                       + lblinvoice.Text + "','"
                                       + dates.Text + "','"
                                       + wordAMount.Text + "','"
                                       + txtCash.Text + "','"
                                       + DateTimePicker1.Value.ToString("yyyy-MM-dd") + "')";
                r.Modify(sql);


                sql = @"Update Bill set AcountReceivable='" + totalamo.Text + "',DueDate='"+DateTimePicker1.Value.ToString("yyyy-MM-dd")+"' where CustomerID='" + id.ToString() + "'";
                r.Modify(sql);
                disbal();


                
                
                
   

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //code for printing
        private void display()
        {
                textBox3.AppendText("\t\t" + "       COLLECTION RECEIPT" + Environment.NewLine + Environment.NewLine);
                textBox3.AppendText("No." + " " + lblinvoice.Text + "\t\t\t" + "Date:" + " " + DateTime.Now.ToShortDateString() + Environment.NewLine);
                textBox3.AppendText("Received from" + " " + txtReceived.Text + "and Address at" +" "+txtaddress.Text + Environment.NewLine);
                textBox3.AppendText("engaged in" +" "+"and business style of______________the sum of" + Environment.NewLine +
                    " " + wordAMount.Text + " " + "pesos(" + lbltd.Text + ")" + Environment.NewLine);    
                
           


        }
       
        //random
        private String invoice()
        {
            Random r = new Random();
            string str = "";
            for (int i = 0; i < 6; i++)
                str = str + (r.Next(5) + 1).ToString();
            return str;
        }

        private void validation()
        {
            if(wordAMount.Text==""){
                MessageBox.Show("Please add amount in words", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                display();
                MessageBox.Show("Done!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                save();
                print();
                
            }
        }
       
      
        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (DateTimePicker1.Value <= DateTime.Now)
            {
                MessageBox.Show("Due date is invalid", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            else
            {


                validation();
              
                main a = new main();
                a.Show();
                this.Hide();
               
            }
           
        }

        private void Billing_Load(object sender, EventArgs e)
        {
            displayToText();
            lblinvoice.Text=invoice();
            
            disbal();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Customer_Add a = new Customer_Add();
            a.Show();
            this.Hide();

        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            total();
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(textBox3.Text, new Font("Arial", 18, FontStyle.Regular), Brushes.Black, 20, 20);

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

    }
}
