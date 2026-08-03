using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StockMaster
{
    public partial class ProductList : Form
    {
        public ProductList()
        {
            InitializeComponent();
        }

        private void ProductList_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            SqlConnection con = Database.GetConnection();

            try
            {
                con.Open();

                string query = "SELECT * FROM Products";

                SqlDataAdapter da = new SqlDataAdapter(query, con);

                DataTable dt = new DataTable();

                da.Fill(dt);

                dgvProducts.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvProducts.SelectedRows[0].Cells["ProductID"].Value);

                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this product?",
                    "Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SqlConnection con = Database.GetConnection();

                    try
                    {
                        con.Open();

                        SqlCommand cmd = new SqlCommand("DELETE FROM Products WHERE ProductID=@id", con);
                        cmd.Parameters.AddWithValue("@id", id);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Product Deleted Successfully");

                        LoadProducts(); // Grid Refresh
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a product.");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProducts();
            txtSearch.Clear();
        }

            private void btnSearch_Click(object sender, EventArgs e)
            {
                SqlConnection con = Database.GetConnection();

                try
                {
                    con.Open();

                    string query = "SELECT * FROM Products WHERE ProductName LIKE @name";

                    SqlDataAdapter da = new SqlDataAdapter(query, con);

                    da.SelectCommand.Parameters.AddWithValue("@name", "%" + txtSearch.Text + "%");

                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    dgvProducts.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
    
