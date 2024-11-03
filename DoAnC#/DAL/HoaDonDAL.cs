using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DoAnC_.DAL
{
    public class HoaDonDAL
    {
        // Method to retrieve all orders from the database
        public DataTable GetAllOrders()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM Orders"; // Ensure 'Orders' is the correct table name

            try
            {
                ClsKetNoi.ExecuteWithConnection(connection =>
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader); // Load data into the DataTable
                        }
                    }
                });
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi SQL khi lấy danh sách hóa đơn: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách hóa đơn: " + ex.Message);
            }

            return dt; // Return the filled DataTable
        }

        // Method to add a new order to the database
        public bool AddOrder(string staffID, string customerID, string tableID, decimal totalPrice, DateTime createdAt)
        {
            try
            {
                string query = "INSERT INTO Orders (staff_id, customer_id, table_id, total_price, created_at) VALUES (@staffID, @customerID, @tableID, @totalPrice, @createdAt)";

                bool result = false;
                ClsKetNoi.ExecuteWithConnection(connection =>
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@staffID", staffID);
                        cmd.Parameters.AddWithValue("@customerID", customerID);
                        cmd.Parameters.AddWithValue("@tableID", tableID);
                        cmd.Parameters.AddWithValue("@totalPrice", totalPrice);
                        cmd.Parameters.AddWithValue("@createdAt", createdAt);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        result = rowsAffected > 0;
                    }
                });

                return result;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi SQL khi thêm hóa đơn: " + sqlEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm hóa đơn: " + ex.Message);
                return false;
            }
        }

        // Method to update an existing order in the database
        public bool UpdateOrder(int id, string staffID, string customerID, string tableID, decimal totalPrice, DateTime createdAt)
        {
            try
            {
                string query = "UPDATE Orders SET staff_id = @staffID, customer_id = @customerID, table_id = @tableID, total_price = @totalPrice, created_at = @createdAt WHERE id = @id";

                bool result = false;
                ClsKetNoi.ExecuteWithConnection(connection =>
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@staffID", staffID);
                        cmd.Parameters.AddWithValue("@customerID", customerID);
                        cmd.Parameters.AddWithValue("@tableID", tableID);
                        cmd.Parameters.AddWithValue("@totalPrice", totalPrice);
                        cmd.Parameters.AddWithValue("@createdAt", createdAt);
                        cmd.Parameters.AddWithValue("@id", id);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        result = rowsAffected > 0;
                    }
                });

                return result;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi SQL khi cập nhật hóa đơn: " + sqlEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật hóa đơn: " + ex.Message);
                return false;
            }
        }

        // Method to delete an order from the database
        public bool DeleteOrder(int id)
        {
            try
            {
                string query = "DELETE FROM Orders WHERE id = @id"; // Ensure 'Orders' is the correct table name

                bool result = false;
                ClsKetNoi.ExecuteWithConnection(connection =>
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        result = rowsAffected > 0;
                    }
                });

                return result;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi SQL khi xóa hóa đơn: " + sqlEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa hóa đơn: " + ex.Message);
                return false;
            }
        }
    }
}
