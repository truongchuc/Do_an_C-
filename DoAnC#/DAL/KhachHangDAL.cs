using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DoAnC_.DAL
{
    public class KhachHangDAL
    {
        // Phương thức để thêm khách hàng vào cơ sở dữ liệu
        public bool AddKhachHang(string fullName, string address, string email, string phoneNumber, DateTime birthday)
        {
            string query = "INSERT INTO Customer (fullname, address, email, birthday, phone_number) VALUES (@fullname, @address, @email, @birthday, @phone_number)";

            try
            {
                bool result = false;
                ClsKetNoi.ExecuteWithConnection(connection =>
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@fullname", fullName);
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@phone_number", phoneNumber);
                        cmd.Parameters.AddWithValue("@birthday", birthday);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        result = rowsAffected > 0;
                    }
                });

                return result;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi SQL khi thêm khách hàng: " + sqlEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm khách hàng: " + ex.Message);
                return false;
            }
        }

        // Phương thức để cập nhật thông tin khách hàng
        public bool UpdateKhachHang(int id, string fullName, string address, string email, string phone_number, DateTime birthday)
        {
            string query = "UPDATE Customer SET FullName = @FullName, Address = @Address, Email = @Email, phone_number = @phone_number, Birthday = @Birthday WHERE ID = @ID";

            try
            {
                bool result = false;
                ClsKetNoi.ExecuteWithConnection(connection =>
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@FullName", fullName);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@phone_number", phone_number);
                        cmd.Parameters.AddWithValue("@Birthday", birthday);
                        cmd.Parameters.AddWithValue("@ID", id);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        result = rowsAffected > 0;
                    }
                });

                return result;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi SQL khi cập nhật khách hàng: " + sqlEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật khách hàng: " + ex.Message);
                return false;
            }
        }

        // Phương thức để xóa khách hàng khỏi cơ sở dữ liệu
        public bool DeleteKhachHang(int id)
        {
            string query = "DELETE FROM Customer WHERE ID = @ID";

            try
            {
                bool result = false;
                ClsKetNoi.ExecuteWithConnection(connection =>
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        result = rowsAffected > 0;
                    }
                });

                return result;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi SQL khi xóa khách hàng: " + sqlEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa khách hàng: " + ex.Message);
                return false;
            }
        }

        // Phương thức để lấy tất cả khách hàng từ cơ sở dữ liệu
        public DataTable GetAllKhachHang()
        {
            DataTable dt = new DataTable();
            string query = "SELECT id, fullname, address, email, birthday, phone_number FROM Customer"; // Đảm bảo các cột được chọn đầy đủ

            try
            {
                ClsKetNoi.ExecuteWithConnection(connection =>
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                });
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi SQL khi lấy danh sách khách hàng: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách khách hàng: " + ex.Message);
            }

            return dt;
        }

    }
}
