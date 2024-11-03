using DoAnC_.BLL;
using DoAnC_.DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnC_.UI
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            ClsKetNoi.getConectionString();
            CustomizeGradientPanel();
        }
        private void CustomizeGradientPanel()
        {
            
        }
        private void btnchuyendenRegister_Click(object sender, EventArgs e)
        {
            ShadowPanRegister.Visible = true;
            TransitionFrmLogin.ShowSync(ShadowPanRegister);
        }

        private void btnchuyendenLogin_Click(object sender, EventArgs e)
        {
           ShadowPanRegister.Visible = false;
            TransitionFrmLogin.HideSync(ShadowPanRegister);
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            CustomizeGradientPanel();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các trường trên form
            string username = txtUserNameRegister.Text.Trim();
            string password = txtPassRegister.Text.Trim();
            string email = txtemailRegister.Text.Trim();
            string role = "user";  // Giá trị mặc định là 'user', có thể tùy chỉnh

            // Kiểm tra hợp lệ
            if (string.IsNullOrWhiteSpace(username))
            {
                ShowMessageDialog("Tên đăng nhập không được để trống.", "Thông báo", Guna.UI2.WinForms.MessageDialogIcon.Warning, Color.LightCoral);
                return;
            }

            // Tạo đối tượng ClsUser và kiểm tra xem tên người dùng đã tồn tại chưa
            ClsUser user = new ClsUser { Username = username };
            if (user.IsUsernameExists())
            {
                ShowMessageDialog("Tên người dùng đã được sử dụng. Vui lòng chọn tên khác.", "Thông báo", Guna.UI2.WinForms.MessageDialogIcon.Warning, Color.LightCoral);
                txtUserNameRegister.SelectAll();  // Bôi đen username
                txtUserNameRegister.Focus();  // Đặt con trỏ vào trường username
                return;
            }

            if (!IsPasswordComplex(password))
            {
                ShowMessageDialog("Mật khẩu phải có ít nhất 8 ký tự, chứa chữ cái viết hoa, chữ cái viết thường, chữ số và ký tự đặc biệt.", "Thông báo", Guna.UI2.WinForms.MessageDialogIcon.Warning, Color.LightCoral);
                return;
            }

            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                ShowMessageDialog("Email không hợp lệ.", "Thông báo", Guna.UI2.WinForms.MessageDialogIcon.Warning, Color.LightCoral);
                return;
            }

            // Mã hóa mật khẩu trước khi gọi
            string hashedPassword = user.HashPassword(password);

            // Cập nhật các thuộc tính của đối tượng `user` và thêm vào cơ sở dữ liệu
            user.Password = hashedPassword;
            user.Email = email;
            user.Role = role;

            if (user.InsertUser())
            {
                ShowMessageDialog("Đăng ký người dùng thành công!", "Thông báo", Guna.UI2.WinForms.MessageDialogIcon.Information, Color.FromArgb(110, 237, 254));
            }
            else
            {
                ShowMessageDialog("Đăng ký người dùng không thành công!", "Thông báo", Guna.UI2.WinForms.MessageDialogIcon.Error, Color.LightCoral);
            }
        }

        private void ShowMessageDialog(string message, string caption, Guna.UI2.WinForms.MessageDialogIcon icon, Color backgroundColor)
        {
            MessageDialogFrmLogin.Caption = caption;
            MessageDialogFrmLogin.Text = message;
            MessageDialogFrmLogin.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
            MessageDialogFrmLogin.Icon = icon;
            MessageDialogFrmLogin.Parent = this;
            //guna2MessageDialog.BackColor = backgroundColor;
            MessageDialogFrmLogin.Show();
        }
        private bool IsPasswordComplex(string password)
        {
            // Kiểm tra mật khẩu có ít nhất 8 ký tự
            if (password.Length < 8)
                return false;

            // Kiểm tra có ít nhất 1 ký tự chữ thường
            if (!password.Any(char.IsLower))
                return false;

            // Kiểm tra có ít nhất 1 ký tự chữ hoa
            if (!password.Any(char.IsUpper))
                return false;

            // Kiểm tra có ít nhất 1 chữ số
            if (!password.Any(char.IsDigit))
                return false;

            // Kiểm tra có ít nhất 1 ký tự đặc biệt
            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                return false;

            return true;
        }
        // Hàm kiểm tra định dạng email hợp lệ
        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        // Hàm mã hóa mật khẩu sử dụng SHA-256
        private string HashPassword(string password)
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ form
            string username = txtUserNameLogin.Text.Trim();
            string password = txtPassLogin.Text.Trim();

            // Kiểm tra xem username và password có bị để trống hay không
            if (string.IsNullOrWhiteSpace(username))
            {
                ShowMessageDialog("Tên đăng nhập không được để trống.", "Thông báo", Guna.UI2.WinForms.MessageDialogIcon.Warning, Color.LightCoral);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ShowMessageDialog("Mật khẩu không được để trống.", "Thông báo", Guna.UI2.WinForms.MessageDialogIcon.Warning, Color.LightCoral);
                return;
            }

            // Tạo đối tượng ClsUser và thực hiện kiểm tra thông tin đăng nhập
            ClsUser user = new ClsUser(username, password);
            if (user.ValidateUserLogin())
            {
                // Nếu đăng nhập thành công, kiểm tra role
                if (user.Role == "admin")
                {
                    ShowMessageDialog("Đăng nhập thành công! Bạn là Admin.", "Thông báo", Guna.UI2.WinForms.MessageDialogIcon.Information, Color.FromArgb(110, 237, 254));
                    //// Điều hướng tới trang admin
                    //FrmAdmin adminForm = new FrmAdmin();
                    //adminForm.Show();
                    //this.Hide();
                }
                else if (user.Role == "user")
                {
                    ShowMessageDialog("Đăng nhập thành công! Bạn là User.", "Thông báo", Guna.UI2.WinForms.MessageDialogIcon.Information, Color.FromArgb(110, 237, 254));
                    //Điều hướng tới trang user
                   FrmOrderManagement orderManagement = new FrmOrderManagement();
                    orderManagement.Show();
                    this.Hide();
                }
            }
            else
            {
                ShowMessageDialog("Tên đăng nhập hoặc mật khẩu không đúng.", "Thông báo", Guna.UI2.WinForms.MessageDialogIcon.Warning, Color.LightCoral);
                txtUserNameLogin.SelectAll();  // Bôi đen username
                txtUserNameLogin.Focus();  // Đặt con trỏ vào trường username
            }
        }
    }
}
