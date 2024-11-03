using DoAnC_.BLL;
using DoAnC_.DAL;
using DoAnC_.UI.UI_UserControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnC_.UI
{
    public partial class FrmDSMon : Form
    {
        public delegate void ProductClickedEventHandler(object sender, ButtonProduct.ProductEventArgs e);
        public event ProductClickedEventHandler ProductClicked;
        public FrmDSMon()
        {
            InitializeComponent();
            ClsKetNoi.getConectionString();
            LoadProducts();
            // Đặt kích thước cố định cho FrmDSMon
            this.Width = 700;
            this.Height = 655;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Đặt DockStyle và AutoSize cho flPanDSTD
            flPanDSTD.Dock = DockStyle.Fill;
            flPanDSTD.AutoSize = false;
        }

        private void LoadProducts()
        {
            try
            {
                List<ClsProduct> products = ClsProduct.GetAllProducts();
                Debug.WriteLine($"Số lượng sản phẩm lấy được: {products.Count}");
                flPanDSTD.Controls.Clear();

                if (products.Count == 0)
                {
                    MessageBox.Show("Không có sản phẩm nào để hiển thị.");
                    return;
                }

                foreach (var product in products)
                {
                    try
                    {
                        ButtonProduct buttonProduct = new ButtonProduct
                        {
                            ProductName = product.Title,
                            ProductPrice = product.Price != 0 ? product.Price.ToString("N0", CultureInfo.InvariantCulture) + " VND" : "Không có giá",
                            Size = new Size(180, 270), // Kích thước của ButtonProduct
                            Margin = new Padding(10, 10, 10, 10) // Khoảng cách giữa các ButtonProduct
                        };

                        string projectDirectory = Directory.GetParent(Application.StartupPath).Parent.FullName;
                        string imagePath = Path.Combine(projectDirectory, product.Thumbnail.Replace("/", "\\"));
                        Debug.WriteLine($"Đường dẫn ảnh đã chỉnh sửa: {imagePath}");

                        if (!string.IsNullOrEmpty(product.Thumbnail) && File.Exists(imagePath))
                        {
                            using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                            {
                                buttonProduct.ProductImage = new Bitmap(fs);
                            }
                            Debug.WriteLine($"Đã tải ảnh cho sản phẩm: {product.Title}");
                        }
                        else
                        {
                            buttonProduct.ProductImage = Properties.Resources.macdinh;
                            Debug.WriteLine($"Sử dụng ảnh mặc định cho sản phẩm: {product.Title}");
                        }
                        // Đăng ký sự kiện ProductClicked và thêm vào FlowLayoutPanel
                        buttonProduct.ProductClicked += (s, e) => ProductClicked?.Invoke(s, e);

                        flPanDSTD.Controls.Add(buttonProduct);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xử lý sản phẩm: {product.Title}. Chi tiết lỗi: {ex.Message}");
                        Debug.WriteLine($"Lỗi khi xử lý sản phẩm: {product.Title}. Chi tiết lỗi: {ex}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy danh sách sản phẩm. Chi tiết lỗi: {ex.Message}");
                Debug.WriteLine($"Lỗi khi lấy danh sách sản phẩm. Chi tiết lỗi: {ex}");
            }
        }


        private void FrmDSMon_Load(object sender, EventArgs e)
        {
            // Đặt chiều rộng cố định cho FlowLayoutPanel
            flPanDSTD.Width = 700;
            flPanDSTD.Anchor = AnchorStyles.Top; // Giữ cố định chỉ ở phía trên, không giãn ra hai bên

            // Thiết lập kích thước của FrmDSMon để phù hợp với flPanDSTD và đảm bảo nó không thay đổi khi thêm sản phẩm
            this.AutoSize = true; // Tự động điều chỉnh kích thước form dựa trên nội dung bên trong
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

        }

        private void FrmDSMon_Shown(object sender, EventArgs e)
        {
            // Xuất thông báo về kích thước của FrmDSMon, panDSTD, và flPanDSTD khi FrmDSMon được gọi từ FrmOrderManagement
            string message = $"FrmDSMon - Width: {this.Width}, Height: {this.Height}\n" +
                             $"panDSTD - Width: {panDSTD.Width}, Height: {panDSTD.Height}\n" +
                             $"flPanDSTD - Width: {flPanDSTD.Width}, Height: {flPanDSTD.Height}";

            MessageBox.Show(message, "Kích Thước FrmDSMon và Panel", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Ghi thông tin kích thước vào bảng điều khiển Debug
            Debug.WriteLine(message);
        }

    }
}
