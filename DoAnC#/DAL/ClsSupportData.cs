using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnC_.DAL
{
    public class Cls_SurportData
    {
        // Phương thức thực thi các câu lệnh INSERT, UPDATE, DELETE
        public static bool ExecuteNonQuery(string storedProcedureName, params SqlParameter[] parameters)
        {
            try
            {
                // Mở kết nối từ ClsKetNoi
                if (ClsKetNoi.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, ClsKetNoi.con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Thêm các tham số nếu có
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        Console.WriteLine($"Thực thi thủ tục lưu trữ: {storedProcedureName}");
                        int result = cmd.ExecuteNonQuery();

                        Console.WriteLine($"Số dòng bị ảnh hưởng: {result}");
                        return result > 0;
                    }
                }
                else
                {
                    Console.WriteLine("Không thể mở kết nối với CSDL.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thực thi câu lệnh: " + ex.Message);
                return false;
            }
            finally
            {
                // Đóng kết nối sau khi hoàn thành
                if (ClsKetNoi.con != null && ClsKetNoi.con.State == ConnectionState.Open)
                {
                    ClsKetNoi.con.Close();
                }
            }
        }


        // Phương thức trả về DataTable từ câu lệnh SELECT
        public static DataTable ExecuteQuery(string storedProcedureName, params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                // Mở kết nối từ ClsKetNoi
                if (ClsKetNoi.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, ClsKetNoi.con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Thêm các tham số nếu có
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        Console.WriteLine($"Thực thi thủ tục lưu trữ: {storedProcedureName}");

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }

                        // Kiểm tra số lượng dòng trong DataTable
                        Console.WriteLine($"Số dòng trong DataTable: {dt.Rows.Count}");
                    }
                }
                else
                {
                    Console.WriteLine("Không thể mở kết nối với CSDL.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thực thi câu lệnh: " + ex.Message);
                return null;
            }
            finally
            {
                // Đóng kết nối sau khi hoàn thành
                if (ClsKetNoi.con != null && ClsKetNoi.con.State == ConnectionState.Open)
                {
                    ClsKetNoi.con.Close();
                }
            }

            return dt;
        }

    }
}
