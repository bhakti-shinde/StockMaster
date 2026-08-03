using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StockMaster
{
    public partial class AddProduct : Form
    {
        public AddProduct()
        {
            InitializeComponent();
        }

         private void btnSave_Click(object sender, EventArgs e)
            {
                SqlConnection con = Database.GetConnection();

                try
                {
                    con.Open();

                    string query = "INSERT INTO Products(ProductName, Category, Quantity, Price) VALUES(@name, @category, @quantity, @price)";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@name", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@category", txtCategory.Text);
                    cmd.Parameters.AddWithValue("@quantity", txtQuantity.Text);
                    cmd.Parameters.AddWithValue("@price", txtPrice.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Product Added Successfully");

                txtProductName.Clear();
                txtCategory.Clear();
                txtQuantity.Clear();
                txtPrice.Clear();

                txtProductName.Focus();
                con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        private void AddProduct_Load(object sender, EventArgs e)
        {

        }
    }
    }
