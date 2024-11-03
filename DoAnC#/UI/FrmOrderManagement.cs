using DoAnC_.UI.UI_UserControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.Design.WebControls;
using System.Windows.Forms;

namespace DoAnC_.UI
{
    public partial class FrmOrderManagement : Form
    {
        FrmDSMon GoiMon;
        FrmThongKe ThongKe;
        public FrmOrderManagement()
        {
            InitializeComponent();
        }
        private void addNhanVienControl(UserControl userControl) 
        {
            userControl.Dock = DockStyle.Fill;
            panTongNhanVien.Controls.Clear();
            panTongNhanVien.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void addKhanhHangControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panTongKhacHang.Controls.Clear();
            panTongKhacHang.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void addHoaDonControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panTongHoaDon.Controls.Clear();
            panTongHoaDon.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        bool menuExpand = false;
        private void menuTransition_Tick(object sender, EventArgs e)
        {
            if (menuExpand == false) 
            {
                pandropGoiMon.Height += 10;
                if(pandropGoiMon.Height >= 172)
                {
                    menuTransition.Stop();
                    menuExpand = true;
                }

            }
            else 
            {
                pandropGoiMon.Height -= 10;
                if (pandropGoiMon.Height <= 54)
                {
                    menuTransition.Stop();
                    menuExpand = false;
                }
            }
        }

        private void btnGoiMon_Click(object sender, EventArgs e)
        {
            if (GoiMon == null || GoiMon.IsDisposed)
            {
                // Khởi tạo FrmDSMon
                GoiMon = new FrmDSMon();

                // Đăng ký sự kiện ProductClicked từ FrmDSMon
                GoiMon.ProductClicked += GoiMon_ProductClicked;

                // Đặt các thuộc tính Dock và TopLevel để FrmDSMon lấp đầy panel
                GoiMon.TopLevel = false;
                GoiMon.Dock = DockStyle.Fill;

                // Đặt kích thước cố định cho FrmDSMon
                GoiMon.Width = 700;
                GoiMon.Height = 655;
                GoiMon.MaximumSize = new Size(700, 655);

                //// Thêm FrmDSMon vào panel và hiển thị
                panGoiFrmDSMon.Controls.Clear(); // Xóa các control cũ trong panel nếu có
                panGoiFrmDSMon.Controls.Add(GoiMon);
                GoiMon.Show();
            }
            else
            {
                GoiMon.BringToFront();
            }

            // Thiết lập chiều cao của các dòng trong DataGridView
            DGVHTMonCuaBan.RowTemplate.Height = 100; // Đặt chiều cao của mỗi dòng thành 100
            DGVHTMonCuaBan.Columns["Anh"].Width = 120; // Đặt chiều rộng của cột hình ảnh thành 120

        }
        private void GoiMon_ProductClicked(object sender, ButtonProduct.ProductEventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            DialogResult result = MessageBox.Show($"Bạn có muốn thêm sản phẩm '{e.ProductName}' vào bàn không?", "Xác nhận thêm sản phẩm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return; // Không thêm sản phẩm nếu người dùng chọn "No"
            }

            bool isProductExists = false;
            foreach (DataGridViewRow row in DGVHTMonCuaBan.Rows)
            {
                if (row.Cells["Ten"].Value != null && row.Cells["Ten"].Value.ToString() == e.ProductName)
                {
                    // Nếu sản phẩm đã tồn tại, tăng số lượng và cập nhật tổng tiền
                    int currentQuantity = Convert.ToInt32(row.Cells["SoLuong"].Value);
                    row.Cells["SoLuong"].Value = currentQuantity + 1;
                    row.Cells["TongTienSP"].Value = (currentQuantity + 1) * Convert.ToDecimal(e.ProductPrice.Replace(" VND", "").Replace(",", ""));
                    isProductExists = true;
                    break;
                }
            }

            // Nếu sản phẩm chưa tồn tại, thêm dòng mới vào DataGridView
            if (!isProductExists)
            {
                int rowIndex = DGVHTMonCuaBan.Rows.Add();
                DataGridViewRow row = DGVHTMonCuaBan.Rows[rowIndex];

                row.Cells["Anh"].Value = e.ProductImage ?? Properties.Resources.macdinh;
                row.Cells["Ten"].Value = e.ProductName;
                row.Cells["Gia"].Value = e.ProductPrice;
                row.Cells["SoLuong"].Value = 1;
                row.Cells["TongTienSP"].Value = Convert.ToDecimal(e.ProductPrice.Replace(" VND", "").Replace(",", ""));
            }
        }

        private void GoiMon_FormColsed(object sender, EventArgs e)
        {
            GoiMon = null;
        }
        bool sidebarExpand = false;
        private void sidebarTransition_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand) 
            {
                flowpanMenuChucNang.Width -= 10;   
                if(flowpanMenuChucNang.Width <= 45)
                {
                    sidebarExpand = false;
                    sidebarTransition.Stop();

                    
                }
            }
            else
            {
                flowpanMenuChucNang.Width += 10;
                if (flowpanMenuChucNang.Width >= 150)
                {
                    sidebarExpand = true;
                    sidebarTransition.Stop();
                }
            }
        }

        private void PictbMenuHamperger_Click(object sender, EventArgs e)
        {
            sidebarTransition.Start();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            // Khởi tạo FrmDSMon
            ThongKe = new FrmThongKe();

            // Đặt các thuộc tính Dock và TopLevel để FrmDSMon lấp đầy panel
            ThongKe.TopLevel = false;
            ThongKe.Dock = DockStyle.Fill;

            // Đặt kích thước cố định cho FrmDSMon
            ThongKe.Width = 700;
            ThongKe.Height = 655;
            ThongKe.MaximumSize = new Size(700, 655);

            panTongGoiMon.Visible = true;
            panTongNhanVien.Visible = false;

            //// Thêm FrmDSMon vào panel và hiển thị
            panGoiFrmDSMon.Controls.Clear(); // Xóa các control cũ trong panel nếu có
            panGoiFrmDSMon.Controls.Add(ThongKe);
            ThongKe.Show();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {      
            panTongGoiMon.Visible=false;
            panTongThongKe.Visible=false;
            panTongHoaDon.Visible=false;
            panTongKhacHang.Visible=false;
            panTongNhanVien.Visible=true;
            UC_FrmNhanVien uc = new UC_FrmNhanVien();
            addNhanVienControl(uc);
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            UC_FrmKhanhHang uc = new UC_FrmKhanhHang();
            addKhanhHangControl(uc);
            panTongGoiMon.Visible = false;
            panTongThongKe.Visible = false;
            panTongHoaDon.Visible = false;
            panTongKhacHang.Visible = true;
            panTongNhanVien.Visible = false;

        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            UC_FrmHoaDon uc = new UC_FrmHoaDon();
            addHoaDonControl(uc);
            panTongGoiMon.Visible = false;
            panTongThongKe.Visible = false;
            panTongHoaDon.Visible = true;
            panTongKhacHang.Visible = false;
            panTongNhanVien.Visible = false;
        }

        // Phương thức xử lý sự kiện ProductClicked cho FrmDSMon

    }
}
