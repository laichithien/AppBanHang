using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBanHang.DAO
{
    internal class Data_Provider
    {
        private string StringConnection = "Data Source=ANDREWL\\SQLEXPRESS;Initial Catalog=QLBH_NC;Integrated Security=True";

        // SELECT
        public DataTable ExecuteQuery(string query)
        {
            SqlConnection connection = new SqlConnection(StringConnection);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(data);
            connection.Close();
            return data;
        }

        // UPDATE / DELETE / INSERT
        public int ExecuteNonQuery(string query)
        {
            int data = 0;
            SqlConnection connection = new SqlConnection(StringConnection);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            data = command.ExecuteNonQuery();
            connection.Close();
            return data;
        }

        // COUNT
        public object ExecuteScalar(string query)
        {
            object data = 0;
            SqlConnection connection = new SqlConnection(StringConnection);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            data = (int)command.ExecuteScalar();
            connection.Close();
            return data;
        }
    }
}
