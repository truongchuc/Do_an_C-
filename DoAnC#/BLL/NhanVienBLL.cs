using System;
using System.Data;
using System.Windows.Forms;
using DoAnC_.DAL;

namespace DoAnC_.BLL
{
    public class NhanVienBLL
    {
        private NhanVienDAL nhanVienDAL = new NhanVienDAL();

        public DataTable GetNhanVien()
        {
            try
            {
                return nhanVienDAL.GetAllNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách nhân viên: " + ex.Message);
                return null; // Or return an empty DataTable, depending on your design choice
            }
        }

        public bool AddNhanVien(string name, string role, string address, string email)
        {
            // Call the DAL method to add the employee
            return nhanVienDAL.AddNhanVien(name, role, address, email);
        }

        public bool EditNhanVien(int id, string name, string role, string address, string email)
        {
            // Gọi phương thức DAL để cập nhật nhân viên
            return nhanVienDAL.UpdateNhanVien(id, name, role, address, email);
        }
        public bool DeleteNhanVien(int id)
        {
            if (id <= 0)
            {
                // Optionally handle invalid IDs before passing to DAL
                return false;
            }

            return nhanVienDAL.DeleteNhanVien(id);
        }
    }

}
