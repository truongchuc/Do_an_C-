using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnC_.UI.UI_UserControl
{
    public partial class ButtonProduct : UserControl
    {
        // Delegate và Event để truyền dữ liệu khi ButtonProduct được click
        public delegate void ProductClickedEventHandler(object sender, ProductEventArgs e);
        public event ProductClickedEventHandler ProductClicked;
        public ButtonProduct()
        {
            InitializeComponent();
            // Gắn sự kiện click cho toàn bộ ButtonProduct và các thành phần con của nó
            this.Click += ButtonProduct_Click;
            pictSP.Click += ButtonProduct_Click;
            lbltenSP.Click += ButtonProduct_Click;
            lblgiaSP.Click += ButtonProduct_Click;
        }

        // Đảm bảo đây là phần trên cùng của file, trước khi khai báo class ButtonProduct
        public class ProductEventArgs : EventArgs
        {
            public string ProductName { get; set; }
            public string ProductPrice { get; set; }
            public Image ProductImage { get; set; }
        }

        public new string ProductName
        {
            get { return lbltenSP.Text; }
            set { lbltenSP.Text = value; }
        }

        public string ProductPrice
        {
            get { return lblgiaSP.Text; }
            set
            {
                // Hiển thị "Không có giá" nếu giá trị là null hoặc chuỗi trống
                lblgiaSP.Text = !string.IsNullOrEmpty(value) ? value : "Không có giá";
            }
        }

        public Image ProductImage
        {
            get { return pictSP.Image; }
            set
            {
                if (value != null)
                {
                    pictSP.Image = value;
                }
                else
                {
                    // Sử dụng ảnh mặc định từ tài nguyên nếu không có ảnh
                    pictSP.Image = Properties.Resources.macdinh;

                }
            }
        }
        private void ButtonProduct_Click(object sender, EventArgs e)
        {
            ProductClicked?.Invoke(this, new ProductEventArgs
            {
                ProductName = this.ProductName,
                ProductPrice = this.ProductPrice,
                ProductImage = this.ProductImage
            });
        }
    }
}
