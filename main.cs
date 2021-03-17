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
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void btnuser_Click(object sender, EventArgs e)
        {
            user a = new user();
            a.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Customer_Add a = new Customer_Add();
            a.Show();
            this.Hide();
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            Product a = new Product();
            a.Show();
            this.Hide();
        }

        private void main_Load(object sender, EventArgs e)
        {
 
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
