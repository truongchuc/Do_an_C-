using System;
using System.Data;
using DoAnC_.DAL;

namespace DoAnC_.BLL
{
    public class KhachHangBLL
    {
        private KhachHangDAL khachHangDAL = new KhachHangDAL();

        // Phương thức để thêm khách hàng mới
        public bool AddKhachHang(string fullName, string address, string email, string phoneNumber, DateTime birthday)
        {
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentException("Dữ liệu khách hàng không hợp lệ. Vui lòng điền đầy đủ thông tin.");
            }

            return khachHangDAL.AddKhachHang(fullName, address, email, phoneNumber, birthday);
        }

        // Phương thức để cập nhật thông tin khách hàng
        public bool EditKhachHang(int id, string fullName, string address, string email, string phoneNumber, DateTime birthday)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentException("Dữ liệu khách hàng không hợp lệ. Vui lòng kiểm tra ID và thông tin.");
            }

            return khachHangDAL.UpdateKhachHang(id, fullName, address, email, phoneNumber, birthday);
        }

        // Phương thức để xóa khách hàng
        public bool DeleteKhachHang(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID không hợp lệ.");
            }

            return khachHangDAL.DeleteKhachHang(id);
        }

        // Phương thức để lấy tất cả khách hàng
        public DataTable GetKhachHang()
        {
            return khachHangDAL.GetAllKhachHang();
        }
    }
}
