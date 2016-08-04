using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsTest
{
    public partial class AddCustomerForm : Form
    {

        public Customer newCustomer;
        public AddCustomerForm()
        {
            InitializeComponent();
        }

        public AddCustomerForm(Customer customer)
        {
            InitializeComponent();
            textBox1.Text = customer.Name;
            textBox2.Text = customer.PhoneNum;
            textBox3.Text = customer.Customer_Address;
            button1.Text = "Edit";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                newCustomer = new Customer();
                newCustomer.Name = textBox1.Text;
                newCustomer.PhoneNum = textBox2.Text;
                newCustomer.Customer_Address = textBox3.Text;
                this.Close();
            }

            else MessageBox.Show("Please fill all fields");
            
        }
    }
}
