using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DoAnC_.DAL
{
    public class ClsXml
    {
        //Mở file xml lên
        public static XmlDocument XMLReader(String filename)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(filename);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return xmlDoc;
        }

        //lưu nội dung vào file xml
        public static bool LuuXML(string filename, string Servername, string status, string username, string password, string database)
        {
            try
            {
                XmlDocument xmlDoc = XMLReader(filename);

                // Kiểm tra xem xmlDoc có null không
                if (xmlDoc == null)
                {
                    Console.WriteLine("Không thể lưu vì file XML không tồn tại hoặc lỗi khi đọc.");
                    return false;
                }

                XmlElement xmlEle = xmlDoc.DocumentElement;

                if (xmlEle != null)
                {
                    var statusNode = xmlEle.SelectSingleNode("status");
                    if (statusNode != null)
                    {
                        statusNode.InnerText = status;
                    }

                    var servernameNode = xmlEle.SelectSingleNode("servername");
                    if (servernameNode != null)
                    {
                        servernameNode.InnerText = Servername;
                    }

                    var usernameNode = xmlEle.SelectSingleNode("username");
                    if (usernameNode != null)
                    {
                        usernameNode.InnerText = username;
                    }

                    var passwordNode = xmlEle.SelectSingleNode("password");
                    if (passwordNode != null)
                    {
                        passwordNode.InnerText = password;
                    }

                    var databaseNode = xmlEle.SelectSingleNode("database");
                    if (databaseNode != null)
                    {
                        databaseNode.InnerText = database;
                    }

                    xmlDoc.Save(filename);
                    return true;
                }
                else
                {
                    Console.WriteLine("Không thể lưu vì file XML không có cấu trúc hợp lệ.");
                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lưu XML: " + ex.Message);
                return false;
            }
        }
    }
}
