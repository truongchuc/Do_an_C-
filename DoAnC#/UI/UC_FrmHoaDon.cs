using System;
using System.Data;
using System.Windows.Forms;
using DoAnC_.BLL;

namespace DoAnC_.UI
{
    public partial class UC_FrmHoaDon : UserControl
    {
        private HoaDonBLL hoaDonBLL = new HoaDonBLL();

        public UC_FrmHoaDon()
        {
            InitializeComponent();
            LoadOrderData(); // Load data when the form is initialized
        }

        private void LoadOrderData()
        {
            try
            {
                DataTable orderData = hoaDonBLL.GetOrders(); // Get data from BLL
                listOrders.DataSource = orderData; // Bind data to DataGridView
                listOrders.Columns["id"].HeaderText = "Order ID";
                listOrders.Columns["staff_id"].HeaderText = "Staff ID";
                listOrders.Columns["customer_id"].HeaderText = "Customer ID";
                listOrders.Columns["table_id"].HeaderText = "Table ID";
                listOrders.Columns["total_price"].HeaderText = "Total Price";
                listOrders.Columns["created_at"].HeaderText = "Created At";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu hóa đơn: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Get data from input fields
                string staffID = txtStaffID.Text.Trim();
                string customerID = txtCustomerID.Text.Trim();
                string tableID = txtTableID.Text.Trim();
                decimal totalPrice;
                DateTime createdAt = dtpCreatedAt.Value;

                if (string.IsNullOrWhiteSpace(staffID) || string.IsNullOrWhiteSpace(customerID) || string.IsNullOrWhiteSpace(tableID) ||
                    !decimal.TryParse(txtTotalPrice.Text.Trim(), out totalPrice))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ và chính xác thông tin hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool success = hoaDonBLL.AddOrder(staffID, customerID, tableID, totalPrice, createdAt);

                if (success)
                {
                    MessageBox.Show("Hóa đơn đã được thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi thêm hóa đơn. Vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                ClearInputFields(); // Clear input fields after adding
                LoadOrderData(); // Refresh DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm hóa đơn: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtOrderID.Text.Trim(), out int id))
                {
                    MessageBox.Show("ID không hợp lệ. Vui lòng chọn một hóa đơn để chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string staffID = txtStaffID.Text.Trim();
                string customerID = txtCustomerID.Text.Trim();
                string tableID = txtTableID.Text.Trim();
                decimal totalPrice;
                DateTime createdAt = dtpCreatedAt.Value;

                if (string.IsNullOrWhiteSpace(staffID) || string.IsNullOrWhiteSpace(customerID) || string.IsNullOrWhiteSpace(tableID) ||
                    !decimal.TryParse(txtTotalPrice.Text.Trim(), out totalPrice))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ và chính xác thông tin hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool success = hoaDonBLL.EditOrder(id, staffID, customerID, tableID, totalPrice, createdAt);

                if (success)
                {
                    MessageBox.Show("Thông tin hóa đơn đã được cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi cập nhật thông tin hóa đơn. Vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                ClearInputFields();
                LoadOrderData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chỉnh sửa hóa đơn: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtOrderID.Text.Trim(), out int id))
                {
                    MessageBox.Show("ID không hợp lệ. Vui lòng chọn một hóa đơn để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    bool success = hoaDonBLL.DeleteOrder(id);

                    if (success)
                    {
                        MessageBox.Show("Hóa đơn đã được xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi xóa hóa đơn. Vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    ClearInputFields();
                    LoadOrderData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa hóa đơn: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputFields()
        {
            txtOrderID.Clear();
            txtStaffID.Clear();
            txtCustomerID.Clear();
            txtTableID.Clear();
            txtTotalPrice.Clear();
            dtpCreatedAt.Value = DateTime.Now;
        }

        private void listOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < listOrders.Rows.Count)
                {
                    DataGridViewRow row = listOrders.Rows[e.RowIndex];

                    // Check if the column exists before assigning values
                    txtOrderID.Text = listOrders.Columns.Contains("id") && row.Cells["id"].Value != null ? row.Cells["id"].Value.ToString() : string.Empty;
                    txtStaffID.Text = listOrders.Columns.Contains("staff_id") && row.Cells["staff_id"].Value != null ? row.Cells["staff_id"].Value.ToString() : string.Empty;
                    txtCustomerID.Text = listOrders.Columns.Contains("customer_id") && row.Cells["customer_id"].Value != null ? row.Cells["customer_id"].Value.ToString() : string.Empty;
                    txtTableID.Text = listOrders.Columns.Contains("table_id") && row.Cells["table_id"].Value != null ? row.Cells["table_id"].Value.ToString() : string.Empty;
                    txtTotalPrice.Text = listOrders.Columns.Contains("total_price") && row.Cells["total_price"].Value != null ? row.Cells["total_price"].Value.ToString() : string.Empty;

                    // Safely check value of created_at before converting
                    if (listOrders.Columns.Contains("created_at") && row.Cells["created_at"].Value != null && DateTime.TryParse(row.Cells["created_at"].Value.ToString(), out DateTime createdAt))
                    {
                        dtpCreatedAt.Value = createdAt;
                    }
                    else
                    {
                        dtpCreatedAt.Value = DateTime.Now; // Default value if not available
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn hóa đơn: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
