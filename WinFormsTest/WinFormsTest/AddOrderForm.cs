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
    public partial class AddOrderForm : Form
    {
        public Order Order { get; set; }
        public AddOrderForm(string customerId)
        {
            InitializeComponent();
            comboBox1.Items.Add(customerId);
            comboBox1.SelectedItem = customerId;
            comboBox1.Enabled = false;
        }

        public AddOrderForm(Order order,List<Customer> customers )
        {
            InitializeComponent();
            foreach (var c in customers)
            {
                comboBox1.Items.Add(c.CustomerId);
            }
            comboBox1.SelectedItem = order.CustomerId;

            textBox2.Text = order.Number;
            textBox3.Text = order.Amount.ToString();
            if (order.DueTime != null)
                dateTimePicker1.Value = order.DueTime.Value;
            if (order.ProcessedTime != null)
                dateTimePicker2.Value = order.ProcessedTime.Value;
            textBox6.Text = order.Order_Description;
            AddNewOrder.Text = "Edit";
        }

        private void AddNewOrder_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox3.Text != ""
               && textBox6.Text != "")
            {
                Order = new Order();
                Order.CustomerId = Convert.ToInt32(comboBox1.SelectedItem);
                Order.Number = textBox2.Text;
                Order.Amount = Convert.ToInt32(textBox3.Text);
                Order.DueTime = Convert.ToDateTime(dateTimePicker1.Value);
                Order.ProcessedTime = Convert.ToDateTime(dateTimePicker2.Value);
                Order.Order_Description = textBox6.Text;
                this.Close();
            }
            else MessageBox.Show("Please fill all fields");
        }
    }
}
