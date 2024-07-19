using KStypemig.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using static System.Net.Mime.MediaTypeNames;

namespace KStypemig.Controllers
{
    public class KhachHangController : Controller
    {
        QLKSpart4Context qlks = new QLKSpart4Context();
        // GET: KhachHang
        public ActionResult ThongtinKhachhang()
        {
            var makh = (String)Session["matk"];
            KhachHang khachHang = new KhachHang();
            if (makh != null)
            {
                var kh = qlks.khachHangs.Where(k => k.IdKhachHang == makh).FirstOrDefault();
                khachHang = kh;
            }
            return View(khachHang);
        }
        public ActionResult Suathongtin(string idkh)
        {

            var k =
            qlks.khachHangs.Where(kh => kh.IdKhachHang == idkh).FirstOrDefault();

            return View(k);
        }
        [HttpPost]
        public ActionResult Suathongtin(KhachHang khach)
        {
            //  if(ModelState.IsReadOnly)
            var makh = (String)Session["matk"];

            var kh = qlks.khachHangs.Where(q => q.IdKhachHang == makh).FirstOrDefault();
            // Update the properties of kh with the properties of khach
            kh.TenTaiKhoan = khach.TenTaiKhoan;
            kh.MatKhau = khach.MatKhau;
            kh.HoTen = khach.HoTen;
            kh.SoDienThoai = khach.SoDienThoai;
            kh.Email = khach.Email;
            kh.Cccd = khach.Cccd;

            qlks.SaveChanges();
            return RedirectToAction("ThongtinKhachhang", "KhachHang");


        }
        [HttpGet]
        public ActionResult Dsdatphong(int? page)
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);

            var makh = (String)Session["matk"];
            //Session.Remove("matk");
            if (TempData["tbao"] != null)
            {
                ViewBag.tbao = TempData["tbao"].ToString();
            }
            var khdatp = qlks.datPhongs.Where(p => p.IdKhachHang == makh).OrderBy(p => p.MaDatPhong).ToPagedList(pageNumber, pageSize);
            // var madp= qlks.datPhongs.Where(p=>p.IdKhachHang==makh).Select(p=>p.MaDatPhong).ToList();
            return View(khdatp);
        }
        [HttpGet]
        public ActionResult xoadatphong(int madp)
        {
            var makh = (String)Session["matk"];
            var dp = qlks.datPhongs.Where(p3 => p3.MaDatPhong == madp && p3.NgayTraDuDinh >= DateTime.Now && p3.IdKhachHang == makh).FirstOrDefault();
            if (dp != null)
            {
                //var p = qlks.datPhongs.Where(ph => ph.MaDatPhong == madp && ph.IdKhachHang == makh).FirstOrDefault();
                //caajp nhaajt thoong tin phongf:
                var p = qlks.phongs.Where(ph => ph.MaPhong == dp.MaPhong).FirstOrDefault();
                if (p != null)
                {
                    p.Tinhtrangphong = "TRONG";
                    qlks.SaveChanges();
                }
                qlks.datPhongs.Remove(dp);
                qlks.SaveChanges();
               
              
                return RedirectToAction("Dsdatphong", "KhachHang");
            }
            TempData["tbao"] = "Bạn không thể hủy đặt phòng này do đã quá hạn!";
            return RedirectToAction("Dsdatphong");


        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string tentk, string matkhau)
        {
            var tk = qlks.khachHangs.FirstOrDefault(kh => kh.TenTaiKhoan == tentk && kh.MatKhau == matkhau);
            if (tk != null)
            {
                Session["tenkh"] = tentk;
                Session["matk"] = tk.IdKhachHang;
                return RedirectToAction("ThongtinKhachhang", "KhachHang");
            }
            else
            {
                ViewBag.saitt = "Ban da nhap sai ten tai khoan hoac mat khau!";
            }
            return View();
        }


        [HttpGet]
        public ActionResult Dangky()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Dangky(string TEN, string email, string diachi, string cccd, string matkhau)
        {
            var kh2 = qlks.khachHangs.Where(kh => kh.Email == email).FirstOrDefault();
            if (kh2 != null)
            {
                ViewBag.tb = "Email này đã tồn tại!";
                return View();
            }

            else
            {
                var maxId =int.Parse( qlks.khachHangs.Max(k => k.IdKhachHang));
                //var khid = int.Parse(qlks.khachHangs.OrderBy(k=>k.IdKhachHang).Select(k=>k.IdKhachHang).FirstOrDefault());
                KhachHang khnew = new KhachHang();
                khnew.IdKhachHang = (maxId + 1).ToString();
                khnew.TenTaiKhoan = TEN;
                khnew.Email = email;
                khnew.MatKhau = matkhau;
                khnew.Cccd = cccd;
                qlks.khachHangs.Add(khnew);
                qlks.SaveChanges();
                ViewBag.tb = " đăng ký thành công!";
                return View();
            }
        }
        public ActionResult dangxuat()
        {
            Session.Remove("tenkh");
            Session.Remove("matk");
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Forgetpw()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Forgetpw(string email)
        {
            var user = qlks.khachHangs.FirstOrDefault(u => u.Email == email); // Tìm khách hàng qua email

            if (user != null)
            {
                // Tạo link reset mật khẩu (không sử dụng token)
                string resetLink = Url.Action("ResetPassword", "KhachHang", new { email = user.Email }, protocol: Request.Url.Scheme);

                // Gửi email
                SendPasswordResetEmail(email, resetLink);
                ViewBag.tb = "Khách hàng hãy kiểm tra email!";
                return View();
            }

            ViewBag.tb = "Email này chưa được đăng ký";
            return View();
        }

        // Phương thức hiển thị form reset mật khẩu
        public ActionResult ResetPassword(string email)
        {ViewBag.Email = email;
           
            return View();
        }

        // Phương thức xử lý đặt lại mật khẩu
        [HttpPost]
        public ActionResult ResetPassword(string email, string newPassword)
        {
            var user = qlks.khachHangs.FirstOrDefault(u => u.Email == email); // Tìm khách hàng qua email

            if (user != null)
            {
                // Cập nhật mật khẩu mới
                user.MatKhau = newPassword;
                qlks.Entry(user).State = EntityState.Modified;
                qlks.SaveChanges();
                     }
           ViewBag.Message = "Mật khẩu đã được thay đổi!";
                return View();
       
        }


        //
        // Phương thức gửi email
        public void SendPasswordResetEmail(string toEmail, string resetLink)
        {

            try
            {
                var fromEmail = "bangbaobinh3@gmail.com";
            var fromPassword = ConfigurationManager.AppSettings["pwem"];

        //    var subject = "Password Reset Request";
        //    var body = $"Please reset your password by clicking <a href='{resetLink}'>here</a>";

        //    var smtp = new SmtpClient
        //    {
        //        Host = "smtp.gmail.com",
        //        Port = 587,
        //        EnableSsl = true,
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        UseDefaultCredentials = false,
        //        Credentials = new NetworkCredential("bangbaobinh3@gmail.com", "vboeoxoscyaurxko");
        //;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toEmail);
            mail.Subject = "Password Reset Request";
            mail.Body = $"Please reset your password by clicking <a href='{resetLink}'>here</a>";
            mail.IsBodyHtml = true;

            // Tạo đối tượng SmtpClient để gửi email
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("bangbaobinh3@gmail.com", "vboeoxoscyaurxko");
            smtp.Send(mail);

            }
            catch (Exception ex)
            {
                // Log hoặc xử lý ngoại lệ
                Console.WriteLine(ex.Message);
            }
        }

    }
}
