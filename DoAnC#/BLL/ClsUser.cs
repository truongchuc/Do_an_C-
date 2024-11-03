using DoAnC_.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DoAnC_.BLL
{
    public class ClsUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        // Constructor mặc định
        public ClsUser() { }

        // Constructor với tham số
        public ClsUser(string username, string password, string email, string role)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.Role = role;
        }
        public ClsUser(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        // Phương thức để thêm người dùng mới
        public bool InsertUser()
        {
            try
            {
                // Sử dụng phương thức từ lớp Cls_SurportData để gọi stored procedure
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@username", this.Username),
                    new SqlParameter("@password", this.Password),  // Nên mã hóa mật khẩu trước khi lưu
                    new SqlParameter("@email", this.Email),
                    new SqlParameter("@role", this.Role)
                };

                return Cls_SurportData.ExecuteNonQuery("sp_InsertUser", parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm người dùng: " + ex.Message);
                return false;
            }
        }

        public bool ValidateUserLogin()
        {
            // Kiểm tra và khởi tạo chuỗi kết nối nếu chưa có
            if (string.IsNullOrEmpty(ClsKetNoi.str_KetNoi))
            {
                ClsKetNoi.getConectionString();
            }

            // Tiếp tục kiểm tra đăng nhập
            string hashedPassword = HashPassword(this.Password);
            Console.WriteLine($"Đang kiểm tra đăng nhập cho Username: {this.Username}, Password mã hóa: {hashedPassword}");
            Console.WriteLine($"Chuỗi kết nối hiện tại: {ClsKetNoi.str_KetNoi}");

            try
            {
                using (SqlConnection con = new SqlConnection(ClsKetNoi.str_KetNoi))
                {
                    con.Open();
                    Console.WriteLine("Kết nối đến cơ sở dữ liệu thành công.");

                    string query = "SELECT role FROM [User] WHERE username = @username AND password = @password";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", this.Username);
                    cmd.Parameters.AddWithValue("@password", hashedPassword);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        this.Role = reader["role"].ToString();
                        Console.WriteLine("Đăng nhập thành công với role: " + this.Role);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Đăng nhập thất bại: không tìm thấy tài khoản hoặc mật khẩu không đúng.");
                        return false; // Sai thông tin đăng nhập
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Lỗi khi mở kết nối cơ sở dữ liệu: " + ex.Message);
                return false;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Lỗi SQL: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi không xác định khi kiểm tra thông tin đăng nhập: " + ex.Message);
                return false;
            }
        }




        public string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public bool IsUsernameExists()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ClsKetNoi.str_KetNoi))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM [User] WHERE username = @username";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", this.Username);

                    int count = (int)cmd.ExecuteScalar();
                    Console.WriteLine($"Kiểm tra tồn tại username '{this.Username}': {count} kết quả tìm thấy.");
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi kiểm tra username: " + ex.Message);
                return false;
            }
        }

    }
}
