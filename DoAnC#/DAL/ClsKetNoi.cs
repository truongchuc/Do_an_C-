using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DoAnC_.DAL
{
    public class ClsKetNoi
    {
        public static SqlConnection con;
        public static string str_KetNoi = "";  // Chuỗi kết nối, sẽ được đọc từ file XML
        public bool kt = false;

        // Constructor
        public ClsKetNoi()
        {
            try
            {
                if (string.IsNullOrEmpty(str_KetNoi))
                {
                    getConectionString(); // Đọc chuỗi kết nối nếu chưa được thiết lập
                }

                if (string.IsNullOrEmpty(str_KetNoi))
                {
                    MessageBox.Show("Chuỗi kết nối không được khởi tạo từ file cấu hình.");
                    return;
                }

                con = new SqlConnection(str_KetNoi);
                con.Open();
                kt = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được CSDL!!! Lỗi: " + ex.Message);
            }
        }

        // Phương thức kiểm tra kết nối
        public static bool TestConnection(string Servername, string status, string username, string password, string database)
        {
            try
            {
                if (status == "true")
                    str_KetNoi = "Data Source=" + Servername + ";Initial Catalog=" + database + ";User ID=" + username + ";Password=" + password + ";TrustServerCertificate=True";
                else
                    str_KetNoi = "Data Source=" + Servername + ";Initial Catalog=" + database + ";Integrated Security=True;TrustServerCertificate=False";

                con = new SqlConnection(str_KetNoi);
                con.Open();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kiểm tra kết nối thất bại. Lỗi: " + ex.Message);
                return false;
            }
        }

        // Lưu cấu hình vào file XML
        public static void Luu_XML(string Servername, string status, string username, string password, string database)
        {
            string filename = "config.xml";
            if (ClsXml.LuuXML(filename, Servername, status, username, password, database))
                MessageBox.Show("Lưu cấu hình thành công!");
            else
                MessageBox.Show("Lưu cấu hình không thành công!");
        }

        // Phương thức lấy chuỗi kết nối từ file XML
        public static void getConectionString()
        {
            try
            {
                XmlDocument xmlDoc = ClsXml.XMLReader("config.xml");
                XmlElement xmlEle = xmlDoc.DocumentElement;

                if (xmlEle.SelectSingleNode("status").InnerText == "true")
                {
                    string Servername = xmlEle.SelectSingleNode("servername").InnerText;
                    string username = xmlEle.SelectSingleNode("username").InnerText;
                    string password = xmlEle.SelectSingleNode("password").InnerText;
                    string database = xmlEle.SelectSingleNode("database").InnerText;
                    str_KetNoi = $"Data Source={Servername};Initial Catalog={database};User ID={username};Password={password};TrustServerCertificate=True";
                }
                else
                {
                    string Servername = xmlEle.SelectSingleNode("servername").InnerText;
                    string database = xmlEle.SelectSingleNode("database").InnerText;
                    str_KetNoi = $"Data Source={Servername};Initial Catalog={database};Integrated Security=True";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể đọc file cấu hình. Lỗi: " + ex.Message);
            }
        }

        // Mở kết nối với cơ sở dữ liệu
        public static bool OpenConnection()
        {
            try
            {
                if (string.IsNullOrEmpty(str_KetNoi))
                    getConectionString();

                con = new SqlConnection(str_KetNoi);
                con.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở kết nối tới CSDL. Lỗi: " + ex.Message);
                return false;
            }
        }
        public static void ExecuteWithConnection(Action<SqlConnection> action)
        {
            if (OpenConnection()) // Kiểm tra và mở kết nối
            {
                try
                {
                    action(con); // Thực hiện hành động với kết nối
                }
                finally
                {
                    con.Close(); // Đảm bảo đóng kết nối sau khi hoàn thành
                }
            }
        }

    }
}
