using System;
using System.Data;
using System.Windows.Forms;
using DoAnC_.DAL;

namespace DoAnC_.BLL
{
    public class HoaDonBLL
    {
        private HoaDonDAL hoaDonDAL = new HoaDonDAL();

        public DataTable GetOrders()
        {
            try
            {
                return hoaDonDAL.GetAllOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách hóa đơn: " + ex.Message);
                return null; // Or return an empty DataTable
            }
        }

        public bool AddOrder(string staffID, string customerID, string tableID, decimal totalPrice, DateTime createdAt)
        {
            return hoaDonDAL.AddOrder(staffID, customerID, tableID, totalPrice, createdAt);
        }

        public bool EditOrder(int id, string staffID, string customerID, string tableID, decimal totalPrice, DateTime createdAt)
        {
            return hoaDonDAL.UpdateOrder(id, staffID, customerID, tableID, totalPrice, createdAt);
        }

        public bool DeleteOrder(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            return hoaDonDAL.DeleteOrder(id);
        }
    }
}
