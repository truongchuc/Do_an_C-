using DoAnC_.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnC_.UI
{
    public partial class FrmKetNoi : Form
    {
        String Servername, Status, Username, Password, Database;

        private void btnLuu_Click(object sender, EventArgs e)
        {
            ClsKetNoi.Luu_XML(Servername, Status, Username, Password, Database);
        }

        public FrmKetNoi()
        {
            InitializeComponent();
            this.Shown += new EventHandler(FrmKetNoi_Shown); // Thêm sự kiện Shown
        }
        private void FrmKetNoi_Shown(object sender, EventArgs e)
        {
            txtServerName.Focus();
        }

        private void btnKetNoi_Click(object sender, EventArgs e)
        {
            Servername = txtServerName.Text;
            if (cobStatus.SelectedIndex == 1)
                Status = "true";
            else
                Status = "false";
            Username = txtUserName.Text;
            Password = txtPass.Text;
            Database = txtDatabase.Text;
            if (ClsKetNoi.TestConnection(Servername, Status, Username, Password, Database))
                MessageBox.Show("Kết nối thành công!");
            else
                MessageBox.Show("Kết nối không thành công!");
        }

        private void FrmKetNoi_Load(object sender, EventArgs e)
        {
            txtServerName.Focus();
            // Chọn lựa chọn đầu tiên trong ComboBox
            if (cobStatus.Items.Count > 0)
            {
                cobStatus.SelectedIndex = 0; // Set lựa chọn đầu tiên
            }
        }
    }
}
