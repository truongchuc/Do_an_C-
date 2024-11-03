using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DoAnC_.DAL
{
    public class ClsHoTroKetNoi
    {
        public DataTable GetSalesReport(DateTime startDate, DateTime endDate)
        {
            DataTable dtReport = new DataTable();

            // Sử dụng ClsKetNoi để mở kết nối và thực hiện truy vấn
            ClsKetNoi.ExecuteWithConnection (conn =>
            {
                using (SqlCommand cmd = new SqlCommand("spGetSalesReportByDateRange", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtReport); // Lấy dữ liệu vào DataTable
                    }
                }
            });

            return dtReport;
        }
    }
}
