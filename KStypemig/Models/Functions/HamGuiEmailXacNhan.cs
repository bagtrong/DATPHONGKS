using KStypemig.Migrations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace KStypemig.Models.Functions
{
    public class HamGuiEmailXacNhan
    {
        public void SendInvoiceEmail(string recipientEmail, int MaDP, string TENtk, string MaPh, DateTime? NgayDat, DateTime? Ngayden, DateTime? NgayTra,  string dichvu,decimal? Thanhtien,string MatKhau,string trangthai)
        {
            try
            {
                // Thông tin đăng nhập email
                string email = "bangbaobinh3@gmail.com";
                string password = ConfigurationManager.AppSettings["pwem"];

                // Tạo đối tượng MailMessage
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(email);
                mail.To.Add(recipientEmail);
                mail.Subject = "Hóa đơn đặt phòng";
                mail.Body = GenerateInvoiceContent(MaDP, TENtk, MaPh, NgayDat, Ngayden, NgayTra, dichvu,Thanhtien, MatKhau, trangthai);
                mail.IsBodyHtml = true;

                // Tạo đối tượng SmtpClient để gửi email
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = new System.Net.NetworkCredential("bangbaobinh3@gmail.com", "vboeoxoscyaurxko");
                smtp.Send(mail);
                //
                //SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                //smtpClient.Port = 587;
                //smtpClient.UseDefaultCredentials = false;
                //smtpClient.Credentials = new NetworkCredential(email, password);
                //smtpClient.EnableSsl = true;

                // Gửi email
                //smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private string GenerateInvoiceContent(int MaDatPhong, string TenTaiKhoan, string MaPhong, DateTime? NgayDat, DateTime? NgayDen, DateTime? NgayTra,string dichvu,  decimal? ThanhTien, string MatKhau,string trangthai)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<h2>Hóa đơn đặt phòng</h2>");
            sb.AppendLine("<p><strong>Mã đặt phòng:</strong> " + MaDatPhong + "</p>");
            sb.AppendLine("<p><strong>Tên tài khoản:</strong> " + TenTaiKhoan + "</p>");
            sb.AppendLine("<p><strong>Mã phòng:</strong> " + MaPhong + "</p>");
            sb.AppendLine("<p><strong>Ngày đặt:</strong> " + NgayDat?.ToShortDateString() + "</p>");
            sb.AppendLine("<p><strong>Ngày đến:</strong> " + NgayDen?.ToShortDateString() + "</p>");
            sb.AppendLine("<p><strong>Ngày trả:</strong> " + NgayTra?.ToShortDateString() + "</p>");
            sb.AppendLine("<p><strong>Dịch vụ sử dụng:</strong> " + dichvu + "</p>");
            sb.AppendLine("<p><strong>Thành tiền:</strong> " + ThanhTien + "</p>");
            sb.AppendLine("<p><strong>Trạng thái thanh toán:</strong> " + trangthai + "</p>");

            sb.AppendLine("<p><strong>Mật khẩu tạm thời của quý khách:</strong> " + MatKhau + "</p>");

            return sb.ToString();
        }

    }
}