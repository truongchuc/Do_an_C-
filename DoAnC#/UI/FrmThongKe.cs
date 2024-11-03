using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAnC_.DAL;
using DoAnC_.Report;
using Microsoft.Reporting.WinForms;

namespace DoAnC_.UI
{
    public partial class FrmThongKe : Form
    {
        public FrmThongKe()
        {
            InitializeComponent();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            DateTime startDate = dtpNgayBatDau.Value.Date;
            DateTime endDate = dtpNgayKetThuc.Value.Date;

            ClsHoTroKetNoi clsHoTroKetNoi = new ClsHoTroKetNoi();
            DataTable dtReport = clsHoTroKetNoi.GetSalesReport(startDate, endDate);

            // Khởi tạo FrmReport và truyền DataTable cùng với startDate và endDate vào phương thức LoadReport
            FrmReport frmReport = new FrmReport();
            frmReport.LoadReport(dtReport, startDate, endDate);
            frmReport.ShowDialog();
        }

        private void rbtThongKeTheoNgay_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtThongKeTheoNgay.Checked)
            {
                // Khi chọn "Thống kê theo ngày", thiết lập ngày kết thúc là cùng ngày với ngày bắt đầu
                dtpNgayKetThuc.Value = dtpNgayBatDau.Value;
            }
        }

        private void rbtThongKeTheoThang_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtThongKeTheoThang.Checked)
            {
                // Khi chọn "Thống kê theo tháng", thiết lập ngày kết thúc là ngày cuối cùng của tháng
                DateTime startDate = dtpNgayBatDau.Value;
                DateTime endDate = new DateTime(startDate.Year, startDate.Month, DateTime.DaysInMonth(startDate.Year, startDate.Month));
                dtpNgayKetThuc.Value = endDate;
            }
        }

        private void FrmThongKe_Load(object sender, EventArgs e)
        {

        }
    }
}
