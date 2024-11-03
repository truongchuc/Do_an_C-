using DoAnC_.BLL;
using System;
using System.Data;
using System.Windows.Forms;

namespace DoAnC_.UI
{
    public partial class UC_FrmNhanVien : UserControl
    {
        private NhanVienBLL nhanVienBLL = new NhanVienBLL();

        public UC_FrmNhanVien()
        {
            InitializeComponent();
            LoadStaffData();
        }

        private void LoadStaffData()
        {
            try
            {
                DataTable staffData = nhanVienBLL.GetNhanVien();
                listDSNV.DataSource = staffData;
                listDSNV.Columns["id"].HeaderText = "ID";
                listDSNV.Columns["fullname"].HeaderText = "Họ và tên";
                listDSNV.Columns["address"].HeaderText = "Địa chỉ";
                listDSNV.Columns["email"].HeaderText = "Email";
                listDSNV.Columns["role"].HeaderText = "Chức vụ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tải dữ liệu nhân viên: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text.Trim();
                string role = txtRole.Text.Trim();
                string address = txtAddress.Text.Trim();
                string email = txtEmail.Text.Trim();

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(role) ||
                    string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if the name contains any digits
                if (ContainsDigits(name))
                {
                    MessageBox.Show("Tên nhân viên không được chứa số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool success = nhanVienBLL.AddNhanVien(name, role, address, email);

                if (success)
                {
                    MessageBox.Show("Nhân viên đã được thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi thêm nhân viên. Vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                ClearInputFields();
                LoadStaffData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi thêm nhân viên: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtID.Text.Trim(), out int id))
                {
                    MessageBox.Show("ID không hợp lệ. Vui lòng chọn một nhân viên để chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string name = txtName.Text.Trim();
                string role = txtRole.Text.Trim();
                string address = txtAddress.Text.Trim();
                string email = txtEmail.Text.Trim();

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(role) ||
                    string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if the name contains any digits
                if (ContainsDigits(name))
                {
                    MessageBox.Show("Tên nhân viên không được chứa số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool success = nhanVienBLL.EditNhanVien(id, name, role, address, email);

                if (success)
                {
                    MessageBox.Show("Thông tin nhân viên đã được cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi cập nhật thông tin nhân viên. Vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                ClearInputFields();
                LoadStaffData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật thông tin nhân viên: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtID.Text.Trim(), out int id))
                {
                    MessageBox.Show("ID không hợp lệ. Vui lòng chọn một nhân viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    bool success = nhanVienBLL.DeleteNhanVien(id);

                    if (success)
                    {
                        MessageBox.Show("Nhân viên đã được xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi xóa nhân viên. Vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    ClearInputFields();
                    LoadStaffData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi xóa nhân viên: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listDSNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < listDSNV.Rows.Count)
                {
                    DataGridViewRow row = listDSNV.Rows[e.RowIndex];
                    txtID.Text = row.Cells["id"].Value?.ToString();
                    txtName.Text = row.Cells["fullname"].Value?.ToString();
                    txtRole.Text = row.Cells["role"].Value?.ToString();
                    txtAddress.Text = row.Cells["address"].Value?.ToString();
                    txtEmail.Text = row.Cells["email"].Value?.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi chọn nhân viên: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputFields()
        {
            txtID.Clear();
            txtName.Clear();
            txtRole.Clear();
            txtAddress.Clear();
            txtEmail.Clear();
        }

        private void UC_FrmNhanVien_Load(object sender, EventArgs e)
        {
            LoadStaffData();
        }

        private void listDSNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        // Helper method to check if a string contains any digits
        private bool ContainsDigits(string str)
        {
            foreach (char c in str)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }

        private void lblrole_Click(object sender, EventArgs e)
        {

        }
    }
}
