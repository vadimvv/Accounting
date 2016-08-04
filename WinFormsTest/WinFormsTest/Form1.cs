using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.Linq;

namespace WinFormsTest
{
    public partial class Form1 : Form
    {
        private Accounting_030820162Entities db = new Accounting_030820162Entities();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SelectAllFieldsFromCustomers();
            SelectAllFieldsFromOrders();
        }


        private void AddNewCustomerButton_Click(object sender, EventArgs e)
        {
            AddCustomerForm form = new AddCustomerForm();

            form.ShowDialog();
            if (form.newCustomer != null)
            {
                db.Customers.Add(form.newCustomer);
                db.SaveChanges();
                SelectAllFieldsFromCustomers();
            }
        }


        private void DeleteCustomerButton_Click(object sender, EventArgs e)
        {
            DialogResult deleteThisRecord = MessageBox.Show("Do you want to remove this record?",
                                                    "Important Question",
                                                    MessageBoxButtons.YesNo);

            if (deleteThisRecord == DialogResult.Yes)
            {
                int customerId = Convert.ToInt32(dataGridView1["CustomerId", dataGridView1.CurrentCell.RowIndex].Value.ToString());
                int customerOrders = db.Orders.Count(c => c.CustomerId == customerId);
                string customerName = db.Customers.FirstOrDefault(c => c.CustomerId == customerId).Name;
               
                //Are there any records
                if (customerOrders > 0)
                {
                    DialogResult deleteAnyway = MessageBox.Show("If you delete this record ("+ customerId+"."+customerName+"), you will delete " +
                                                                customerOrders + " records in 'Orders'",
                                                                "Important Question",
                                                                MessageBoxButtons.YesNo);

                    if (deleteAnyway == DialogResult.Yes)
                    {
                        var orders = db.Orders.Where(o => o.CustomerId == customerId);
                        foreach (var o in orders)
                        {
                            db.Orders.Remove(o);
                        }
                        db.SaveChanges();
                        SelectAllFieldsFromCustomers();
                        SelectAllFieldsFromOrders();
                    }
                    else return;
                }

                //if the customer has no records
                Customer customer = db.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                db.Customers.Remove(customer);
                db.SaveChanges();
                SelectAllFieldsFromCustomers();
            }
        }


        private void AddNewOrderButton_Click(object sender, EventArgs e)
        {
            AddOrderForm form = new AddOrderForm(dataGridView1["CustomerId", dataGridView1.CurrentCell.RowIndex].Value.ToString());
            form.ShowDialog();

            if (form.Order != null)
            {
                db.Orders.Add(form.Order);
                db.SaveChanges();
                SelectAllFieldsFromOrders();
            }
        }


        private void DeleteOrderButton_Click(object sender, EventArgs e)
        {
            DialogResult deleteThisRecord = MessageBox.Show("Do you want to remove this record?",
                                                            "Important Question",
                                                            MessageBoxButtons.YesNo);

            if (deleteThisRecord == DialogResult.Yes)
            {
                int orderId = Convert.ToInt32(dataGridView2["OrderId", dataGridView2.CurrentCell.RowIndex].Value.ToString());
                Order order = db.Orders.FirstOrDefault(o => o.OrderId == orderId);
                db.Orders.Remove(order);
                db.SaveChanges();
                SelectAllFieldsFromOrders();
            }
        }


        //Task 2
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            int currentCustomer = Convert.ToInt32(dataGridView1["CustomerId", dataGridView1.CurrentCell.RowIndex].Value.ToString());
            SelectAllFieldsFromOrders(currentCustomer);
        }


        //Edit current Customer
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            int customerId = Convert.ToInt32(dataGridView1["CustomerId", dataGridView1.CurrentCell.RowIndex].Value.ToString());
            Customer editThisCustomer = db.Customers.FirstOrDefault(c => c.CustomerId == customerId);
            AddCustomerForm form = new AddCustomerForm(editThisCustomer);

            form.ShowDialog();
            if (form.newCustomer != null && editThisCustomer != null)
            {
                editThisCustomer.Name = form.newCustomer.Name;
                editThisCustomer.PhoneNum = form.newCustomer.PhoneNum;
                editThisCustomer.Customer_Address = form.newCustomer.Customer_Address;

                db.SaveChanges();
                SelectAllFieldsFromCustomers();
            }
        }


        //Edit current Order
        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            int orderId = Convert.ToInt32(dataGridView2["OrderId", dataGridView2.CurrentCell.RowIndex].Value.ToString());
            Order order = db.Orders.FirstOrDefault(o => o.OrderId == orderId);
            var customers = db.Customers.ToList();
            AddOrderForm form = new AddOrderForm(order,customers);
            form.ShowDialog();
            if (form.DialogResult != DialogResult.OK)
                return;

            if (db.Customers.Any(c => c.CustomerId == form.Order.CustomerId))
            {
                if (form.Order != null && order != null)
                {
                    order.CustomerId = form.Order.CustomerId;
                    order.Amount = form.Order.Amount;
                    order.Number = form.Order.Number;
                    order.DueTime = form.Order.DueTime;
                    order.ProcessedTime = form.Order.ProcessedTime;
                    order.Order_Description = form.Order.Order_Description;


                    db.SaveChanges();
                    SelectAllFieldsFromOrders();
                }
            }
            else MessageBox.Show("Wrong 'CustomerId'");
        }


        private void SelectAllFieldsFromCustomers()
        {
            var customers = db.Customers;
            dataGridView1.DataSource = customers.ToList();
        }


        private void SelectAllFieldsFromOrders()
        {
            var orders = db.Orders;
            dataGridView2.DataSource = orders.ToList();
        }


        private void SelectAllFieldsFromOrders(int customerId)
        {
            var orders = db.Orders.Where(o => o.CustomerId == customerId);
            dataGridView2.DataSource = orders.ToList();
        }

        private void ShowAllButton_Click(object sender, EventArgs e)
        {
            SelectAllFieldsFromOrders();
        }


    }
}
