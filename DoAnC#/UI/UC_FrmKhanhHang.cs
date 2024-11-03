using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DoAnC_.BLL;

namespace DoAnC_.UI
{
    public partial class UC_FrmKhanhHang : UserControl
    {
        private KhachHangBLL khachHangBLL = new KhachHangBLL();

        public UC_FrmKhanhHang()
        {
            InitializeComponent();
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            try
            {
                DataTable customerData = khachHangBLL.GetKhachHang();
                listDSKH.DataSource = customerData;
                listDSKH.Columns["id"].HeaderText = "ID";
                listDSKH.Columns["fullname"].HeaderText = "Họ và tên";
                listDSKH.Columns["address"].HeaderText = "Địa chỉ";
                listDSKH.Columns["email"].HeaderText = "Email";
                listDSKH.Columns["phone_number"].HeaderText = "Số điện thoại";
                listDSKH.Columns["birthday"].HeaderText = "Ngày sinh";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi tải dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ContainsNumbers(string input)
        {
            // Check if the input contains any digits
            return Regex.IsMatch(input, @"\d");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string fullName = txtFullName.Text.Trim();
                string address = txtAddress.Text.Trim();
                string email = txtEmail.Text.Trim();
                string phoneNumber = txtPhoneNumber.Text.Trim();
                DateTime birthday = dtpBirthday.Value;

                // Check for empty fields and numeric characters in full name
                if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(address) ||
                    string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phoneNumber))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (ContainsNumbers(fullName))
                {
                    MessageBox.Show("Tên khách hàng không được chứa ký tự số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool success = khachHangBLL.AddKhachHang(fullName, address, email, phoneNumber, birthday);

                if (success)
                {
                    MessageBox.Show("Khách hàng đã được thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi thêm khách hàng. Vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                ClearInputFields();
                LoadCustomerData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi thêm khách hàng: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtID.Text.Trim(), out int id))
                {
                    MessageBox.Show("ID không hợp lệ. Vui lòng chọn một khách hàng để chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string fullName = txtFullName.Text.Trim();
                string address = txtAddress.Text.Trim();
                string email = txtEmail.Text.Trim();
                string phoneNumber = txtPhoneNumber.Text.Trim();
                DateTime birthday = dtpBirthday.Value;

                // Check for empty fields and numeric characters in full name
                if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(address) ||
                    string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phoneNumber))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (ContainsNumbers(fullName))
                {
                    MessageBox.Show("Tên khách hàng không được chứa ký tự số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool success = khachHangBLL.EditKhachHang(id, fullName, address, email, phoneNumber, birthday);

                if (success)
                {
                    MessageBox.Show("Thông tin khách hàng đã được cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi cập nhật thông tin khách hàng. Vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                ClearInputFields();
                LoadCustomerData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi cập nhật thông tin khách hàng: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtID.Text.Trim(), out int id))
                {
                    MessageBox.Show("ID không hợp lệ. Vui lòng chọn một khách hàng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    bool success = khachHangBLL.DeleteKhachHang(id);

                    if (success)
                    {
                        MessageBox.Show("Khách hàng đã được xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi xóa khách hàng. Vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    ClearInputFields();
                    LoadCustomerData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi xóa khách hàng: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listDSKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < listDSKH.Rows.Count)
            {
                DataGridViewRow row = listDSKH.Rows[e.RowIndex];

                txtID.Text = row.Cells["id"].Value?.ToString();
                txtFullName.Text = row.Cells["fullname"].Value?.ToString();
                txtAddress.Text = row.Cells["address"].Value?.ToString();
                txtEmail.Text = row.Cells["email"].Value?.ToString();
                txtPhoneNumber.Text = row.Cells["phone_number"].Value?.ToString();

                if (row.Cells["birthday"].Value != null && DateTime.TryParse(row.Cells["birthday"].Value.ToString(), out DateTime birthday))
                {
                    dtpBirthday.Value = birthday;
                }
                else
                {
                    dtpBirthday.Value = DateTime.Now;
                }
            }
        }

        private void ClearInputFields()
        {
            txtID.Clear();
            txtFullName.Clear();
            txtAddress.Clear();
            txtEmail.Clear();
            txtPhoneNumber.Clear();
            dtpBirthday.Value = DateTime.Now;
        }
    }
}
