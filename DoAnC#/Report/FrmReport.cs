using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace DoAnC_.Report
{
    public partial class FrmReport : Form
    {
        public FrmReport()
        {
            InitializeComponent();
        }

        private void FrmReport_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
        public void LoadReport(DataTable dtReport, DateTime startDate, DateTime endDate)
        {
            
            try
            {
                // Toàn bộ mã trong đây
                // Xóa các nguồn dữ liệu cũ của ReportViewer
                reportViewer1.LocalReport.DataSources.Clear();

                // Tạo và gán ReportDataSource mới với tên dataset "ThongKe"
                ReportDataSource rds = new ReportDataSource("ThongKe", dtReport);
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Đặt đường dẫn của file RDLC với đường dẫn tương đối
                reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Report\ThongKeReport.rdlc");

                //THiết lập Parameter
                ReportParameter[] parameters = new ReportParameter[]
                {
                new ReportParameter("StartDate", startDate.ToString("dd/MM/yyyy")),
                new ReportParameter("EndDate", endDate.ToString("dd/MM/yyyy"))
                };
                reportViewer1.LocalReport.SetParameters(parameters);

                // Làm mới ReportViewer để hiển thị dữ liệu
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
