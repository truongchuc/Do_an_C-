using DoAnC_.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnC_.BLL
{
    public class ClsProduct
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }  // Đường dẫn ảnh
        public string Description { get; set; }
        public decimal Price { get; set; }  // Kiểu decimal cho giá
        public int CategoryId { get; set; }

        public ClsProduct() { }

        public ClsProduct(int id, string title, string thumbnail, string description, decimal price, int categoryId)
        {
            Id = id;
            Title = title;
            Thumbnail = thumbnail;
            Description = description;
            Price = price;
            CategoryId = categoryId;
        }

        public static List<ClsProduct> GetAllProducts()
        {
            List<ClsProduct> products = new List<ClsProduct>();

            try
            {
                // Gọi thủ tục lưu trữ và lấy dữ liệu vào DataTable
                DataTable dt = Cls_SurportData.ExecuteQuery("GetAllProducts");
                Console.WriteLine($"Số lượng dòng trong DataTable: {dt?.Rows.Count}");

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Duyệt qua từng dòng dữ liệu và tạo đối tượng ClsProduct
                    foreach (DataRow row in dt.Rows)
                    {
                        ClsProduct product = new ClsProduct
                        {
                            Id = Convert.ToInt32(row["id"]),
                            Title = row["title"] != DBNull.Value ? row["title"].ToString() : null,
                            Thumbnail = row["thumbnail"] != DBNull.Value ? row["thumbnail"].ToString() : null,
                            Description = row["description"] != DBNull.Value ? row["description"].ToString() : null,
                            Price = row["price"] != DBNull.Value ? Convert.ToDecimal(row["price"]) : 0,
                            CategoryId = row["id_cat"] != DBNull.Value ? Convert.ToInt32(row["id_cat"]) : 0
                        };

                        // In ra thông tin sản phẩm để kiểm tra
                        Console.WriteLine($"Sản phẩm: Id={product.Id}, Title={product.Title}, Thumbnail={product.Thumbnail}, Price={product.Price}");

                        products.Add(product);
                    }
                }
                else
                {
                    Console.WriteLine("Không có sản phẩm nào để hiển thị.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách sản phẩm: " + ex.Message);
                Console.WriteLine("Chi tiết lỗi: " + ex.ToString());
            }

            return products;
        }
    }
}
