using System.Data.SqlClient;

namespace StockMaster
{
    internal class Database
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(
                @"Server=.\SQLEXPRESS;Database=StockMasterDB;Trusted_Connection=True;TrustServerCertificate=True;"
            );

            return con;
        }
    }
}