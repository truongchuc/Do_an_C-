using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DoAnC_.DAL
{
    public class NhanVienDAL
    {
        public bool DeleteNhanVien(int id)
        {
            string query = "DELETE FROM Staff WHERE ID = @ID"; // Ensure 'Staff' is the correct table name

            try
            {
                bool result = false; // Variable to hold the result of the operation

                // Execute the query using the connection from ClsKetNoi
                ClsKetNoi.ExecuteWithConnection(connection =>
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Add parameters to the command
                        cmd.Parameters.AddWithValue("@ID", id);

                        // Execute the command and check rows affected
                        int rowsAffected = cmd.ExecuteNonQuery();
                        result = rowsAffected > 0; // Set result to true if the delete was successful
                    }
                });

                if (result)
                {
                    MessageBox.Show("Nhân viên đã được xóa thành công!");
                }
                else
                {
                    MessageBox.Show("Không có nhân viên nào được xóa.");
                }

                return result; // Return the result of the deletion
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi SQL khi xóa nhân viên: " + sqlEx.Message);
                return false; // Return false on SQL errors
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa nhân viên: " + ex.Message);
                return false; // Return false on general errors
            }
        }
        
        public bool UpdateNhanVien(int id, string name, string role, string address, string email)
        {
            try
            {
                // Define the SQL query for updating an employee
                string query = "UPDATE Staff SET fullname = @fullname, role = @role, address = @address, email = @email WHERE id = @id";

                bool result = false;
                ClsKetNoi.ExecuteWithConnection(connection =>
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Add parameters to the command
                        cmd.Parameters.AddWithValue("@fullname", name);
                        cmd.Parameters.AddWithValue("@role", role);
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@id", id); // Include the ID parameter

                        // Execute the command and check rows affected
                        int rowsAffected = cmd.ExecuteNonQuery();
                        result = rowsAffected > 0; // Set result to true if the update was successful
                    }
                });

                return result; // Return the result of the update operation
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi SQL khi cập nhật nhân viên: " + sqlEx.Message);
                return false; // Return false on SQL errors
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật nhân viên: " + ex.Message);
                return false; // Return false on general errors
            }
        }


        // Method to add a new employee to the database
        public bool AddNhanVien(string name, string role, string address, string email)
        {
            try
            {
                // Define the SQL query for inserting a new employee
                string query = "INSERT INTO Staff (fullname, role, address, email) VALUES (@fullname, @role, @address, @email)";

                // Execute the query using the connection from ClsKetNoi
                bool result = false; // Variable to hold the result of the operation
                ClsKetNoi.ExecuteWithConnection(connection =>
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Add parameters to the command
                        cmd.Parameters.AddWithValue("@fullname", name);
                        cmd.Parameters.AddWithValue("@role", role);
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@email", email);

                        // Execute the command and return the result
                        int rowsAffected = cmd.ExecuteNonQuery();
                        result = rowsAffected > 0; // Set the result based on rows affected
                    }
                });

                if (result)
                {
                    MessageBox.Show("Nhân viên đã được thêm thành công!");
                }
                else
                {
                    MessageBox.Show("Không có nhân viên nào được thêm.");
                }

                return result; // Return the result of the insertion
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi SQL khi thêm nhân viên: " + sqlEx.Message);
                return false; // Return false on SQL errors
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm nhân viên: " + ex.Message);
                return false; // Return false on general errors
            }
        }

        // Method to retrieve all employees from the database
        public DataTable GetAllNhanVien()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM Staff"; // Ensure this is your actual table name

            try
            {
                // Open the connection and execute the command
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
                MessageBox.Show("Lỗi SQL khi lấy danh sách nhân viên: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách nhân viên: " + ex.Message);
            }

            return dt; // Return the filled DataTable
        }
    }
}
