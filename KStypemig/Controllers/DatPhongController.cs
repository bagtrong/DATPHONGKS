using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KStypemig.Models.Library;
using KStypemig.Models;
using System.Data.Entity;
using System.Web.Routing;
using KStypemig.Models.ViewModels;
using System.Text.RegularExpressions;
using System.CodeDom;
using KStypemig.Models.Functions;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
using PagedList;

namespace KStypemig.Controllers
{
    public class DatPhongController : Controller
    {
        QLKSpart4Context qLKSpart4 = new QLKSpart4Context();
        [HttpGet]
        public ActionResult TimKiemPhong(string ngayden, string ngaydi, int adult, int trecon, int bed, int? page)
        {
            //lưu dữ liệu vào viewbag
            ViewBag.ngayden = ngayden;
            ViewBag.ngaydi = ngaydi;
            ViewBag.adult = adult;
            ViewBag.trecon = trecon;
            ViewBag.bed = bed;
            //
            int pageSize = 5; // Số lượng mục trên mỗi trang
            int pageNumber = (page ?? 1); // Trang hiện tại, mặc định là trang 1

            var songuoi = adult + trecon;
            var ph = qLKSpart4.phongs.Where(p => p.Songuoitoida >= songuoi && p.Sogiuong == bed).Include(p => p.anhChiTietPhongs).ToList();
            var ph2 = ph.Select(p => new AnhChiTietPhong_ViewModel
            {
                Phong = p,
                AnhChiTietPhongs = p.anhChiTietPhongs
            }).ToList();
            var phongpagelist = ph2.ToPagedList(pageNumber, pageSize);
            var phonglist = new AnhChitietListPhong_Viewmodel
            {
                anhChiTietPhong_ViewModels = phongpagelist,
            };
            var ph3 = qLKSpart4.phongs.Where(p => p.Songuoitoida >= songuoi && p.Sogiuong == bed).Select(p => p.MaLoai).ToList();
            ViewBag.anh = ph3;
            return View(phonglist);
        }
        //[HttpPost]
        //public ActionResult TimKiemPhong(string ngayden, string ngaydi, int adult, int trecon, int bed, int? page)
        //{
        //    //lưu dữ liệu vào viewbag
        //    ViewBag.ngayden = ngayden;
        //    ViewBag.ngaydi = ngaydi;
        //    ViewBag.adult = adult;
        //    ViewBag.trecon= trecon;
        //    ViewBag.bed = bed;
        //    //
        //    int pageSize = 10; // Số lượng mục trên mỗi trang
        //    int pageNumber = (page ?? 1); // Trang hiện tại, mặc định là trang 1

        //    var songuoi = adult + trecon;
        //    var ph = qLKSpart4.phongs.Where(p => p.Songuoitoida >= songuoi && p.Sogiuong == bed).Include(p=>p.anhChiTietPhongs).ToList();
        //    var ph2 = ph.Select(p => new AnhChiTietPhong_ViewModel
        //    {
        //        Phong = p,
        //        AnhChiTietPhongs = p.anhChiTietPhongs
        //    }).ToList();
        //    var phongpagelist = ph2.ToPagedList(pageNumber, pageSize);
        //    var phonglist = new AnhChitietListPhong_Viewmodel
        //    {
        //        anhChiTietPhong_ViewModels = phongpagelist,
        //    };
        //    var ph3 = qLKSpart4.phongs.Where(p => p.Songuoitoida >= songuoi && p.Sogiuong == bed).Select(p => p.MaLoai).ToList();
        //    ViewBag.anh = ph3;
        //    return View( phonglist);
        //}
        //thêm bình luận
        [HttpPost]
        public ActionResult Add_review(string ten, string email, string binhluan, string maph, string rate)
        {
            if (Session["tenkh"] != null)

            {
                var idkh = Session["matk"];
                var datphong = qLKSpart4.datPhongs.Where(dp => dp.IdKhachHang == idkh.ToString() && dp.MaPhong == maph).FirstOrDefault();
                if (datphong != null)
                {
                    Review review = new Review();
                    review.PhongId = maph;
                    review.Username = ten;
                    review.Content = binhluan;
                    review.Email = email;
                    review.Rate = int.Parse (rate);    
                    // Đặt giá trị khác cho review nếu cần thiết
                    review.CreateDate = DateTime.Now;
                    qLKSpart4.reviews.Add(review);
                    qLKSpart4.SaveChanges();
                    return RedirectToAction("DatPhongCuoicung", "DatPhong", new { map = maph });
                }
                TempData["tb"] = "Bạn không có quyền bình luận phòng này do chưa đăng nhập hoặc bạn chưa ở phòng này bao giờ!";
                return RedirectToAction("DatPhongCuoicung", "DatPhong", new { map = maph });

            }
            TempData["tb"] = "Bạn không có quyền bình luận phòng này do chưa đăng nhập hoặc bạn chưa ở phòng này bao giờ!";

            return RedirectToAction("DatPhongCuoicung", "DatPhong", new { map = maph });

           
        }

        public ActionResult VnpayReturn()
        {
            if (Request.QueryString.Count > 0)
            {
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
                var vnpayData = Request.QueryString;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (string s in vnpayData)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                //vnp_TransactionNo: Ma GD tai he thong VNPAY
                //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                //vnp_SecureHash: HmacSHA512 cua du lieu tra ve

                string orderId = Convert.ToString(vnpay.GetResponseData("vnp_TxnRef"));
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                String vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
                String TerminalID = Request.QueryString["vnp_TmnCode"];
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                String bankCode = Request.QueryString["vnp_BankCode"];

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        var order = qLKSpart4.datPhongs.FirstOrDefault(p => p.MaDatPhong.ToString() == orderId);
                        if (order != null)
                        {
                            order.Tinhtrangthanhtoan = "Đã thanh toán";
                            order.Thoigianthanhtoan = DateTime.Now;
                            qLKSpart4.SaveChanges();
                        }
                        //Thanh toan thanh cong
                        ViewBag.TinhTrangThanhToan = "Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ";
                        //    log.InfoFormat("Thanh toan thanh cong, OrderId={0}, VNPAY TranId={1}", orderId, vnpayTranId);
                        //gửi email
                        GenerateRandomPassword pwnew = new GenerateRandomPassword();
                        string matkhau = pwnew.GenerateRandomPassword2(6);
                        var email = (String)Session["email"];
                        Session.Remove("email");
                        HamGuiEmailXacNhan guiem = new HamGuiEmailXacNhan();
                        var dv = TempData["tendv"].ToString();
                        guiem.SendInvoiceEmail(email, order.MaDatPhong, order.TenKhachHang, order.MaPhong, DateTime.Now, order.NgayDenDuDinh, order.NgayTraDuDinh, dv,order.ThanhTien, matkhau, order.Tinhtrangthanhtoan);
                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        ViewBag.TinhTrangThanhToan = "Có lỗi xảy ra trong quá trình xử lý.Mã lỗi: " + vnp_ResponseCode;
                        //Consolog.InfoFormat("Thanh toan loi, OrderId={0}, VNPAY TranId={1},ResponseCode={2}", orderId, vnpayTranId, vnp_ResponseCode);
                    }
                    ViewBag.Ma = "Mã Website (Terminal ID):" + TerminalID;
                    //displayTxnRef.InnerText = "Mã giao dịch thanh toán:" + orderId.ToString();
                    //displayVnpayTranNo.InnerText = "Mã giao dịch tại VNPAY:" + vnpayTranId.ToString();
                    ViewBag.Tien = "Số tiền thanh toán (VND):" + vnp_Amount.ToString();
                    //displayBankCode.InnerText = "Ngân hàng thanh toán:" + bankCode;
                }
                //else
                //{
                // //   log.InfoFormat("Invalid signature, InputData={0}", Request.RawUrl);
                //    ViewBag.InnerText = "Có lỗi xảy ra trong quá trình xử lý";
                //}
            }
            //var a = UrlPayment(0, "DH3574");
           

            return View();
            
        }
        // GET: DatPhong
        [HttpGet]
        public ActionResult DatPhongCuoicung(string map)
        {
            // quyền review
            if (TempData["tb"] != null)
            {

                ViewBag.loi = TempData["tb"].ToString();
            }
            //  = qLKSpart4.phongs.Where(x => x.MaPhong == map).FirstOrDefault();
            ViewBag.tb = Session["EmailErrorMessage"];
            Session.Remove("EmailErrorMessage");
            //lay duw lieuj tu idphong
            var rv= qLKSpart4.reviews.Where(x=>x.PhongId==map).ToList();
            var p=   qLKSpart4.anhChiTietPhongs.Where(a => a.Maphong == map).Include(p5 => p5.phong).ToList();
            //luwu data tuw form khachs hangf nhap
            var p1 = (from p2 in qLKSpart4.phongs join p3 in qLKSpart4.loaiPhongs on p2.MaLoai equals p3.MaLoai where (p2.MaPhong == map) select p3.TenLoai).First();
            ViewBag.TenLoai = p1;
            //
            var p9 = (from p2 in qLKSpart4.phongs join p3 in qLKSpart4.loaiPhongs on p2.MaLoai equals p3.MaLoai where (p2.MaPhong == map) select p3.GhiChu).First();
            ViewBag.gioith  = p9;
                //
            ViewBag.maphong = map;
            ViewBag.em = TempData["em"];
            ViewBag.cc = TempData["cc"];
            ViewBag.ten = TempData["ten"];
            ViewBag.ngayden = TempData["ngayden"];
            ViewBag.ngaydi = TempData["ngaydi"];

            ViewBag.typepayment = TempData["typepayment"];
            ViewBag.typepaymentVN= TempData["typepaymentVN"];
            //
            ViewBag.tb = TempData["tb"];
            var dp = new AnhchitietphongVaReview
            {
                anhChiTietPhongs = p,
                Reviews = rv
            };
            //thêm dịch vụ
            //var services = qLKSpart4.dichVus.ToList();
            //ViewBag.Services = new SelectList(services, "MaDichVu", "Tendichvu");
            //return View();
            //
            return View(dp);
        }
        [HttpPost]
        public ActionResult DatPhongCuoicung(string NgayDen, string NgayDi, string Ten, string CC, int TypePaymentVN, int TypePayment,string map,string em, int madichvu, int songuoiden
            )
        {
            //lấy số người
            var maxng = qLKSpart4.phongs.Where(p => p.MaPhong == map).Select(p => p.Songuoitoida).FirstOrDefault();
            if (maxng < songuoiden)
            {
                TempData["tb"] = "phòng này không đủ chứa số người như bạn muốn";
                return RedirectToAction("Datphongcuoicung",new {map=map});
            }
            //lấy giá của dịch vụ
            var giadv= qLKSpart4.dichVus.FirstOrDefault(d=>d.MaDichVu==madichvu.ToString());

            decimal? giatiendv;
            string dv = "";
            if (giadv == null)
            {
                 dv = "Không dùng";
                 giatiendv = 0;
                
            }
            else
            {
                 dv = giadv.TenDichVu;
                 giatiendv = giadv.GiaDichVu;
            }
            TempData["tendv"] = dv;
            

            //DateTime ngayDenDate = DateTime.ParseExact(NgayDen, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DateTime ngayDiDate = DateTime.ParseExact(NgayDi, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //string formattedNgayDen = ngayDenDate.ToString("yyyy-MM-dd");
            //string formattedNgayDi = ngayDiDate.ToString("yyyy-MM-dd");

            // Bạn có thể sử dụng formattedNgayDen và formattedNgayDi theo yêu cầu của bạn
            //ViewBag.FormattedNgayDen = formattedNgayDen;
            //ViewBag.FormattedNgayDi = formattedNgayDi;
            //
            //
            TempData["em"] = em;
            TempData["cc"] = CC;
            TempData["ten"] = Ten;
            TempData["ngaydi"] = NgayDi;
            TempData["ngayden"] = NgayDen;
            TempData["typepayment"] = TypePayment;
            TempData["typepaymentVN"]=TypePaymentVN;
             
            Session["email"] = em;

            // Tạo biểu thức chính quy để kiểm tra định dạng email
            Regex emailRegex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");
            if(!emailRegex.IsMatch(em)){
                Session["EmailErrorMessage"] = "email khong hop le";
                return RedirectToAction("DatPhongCuoicung", new { map = map });
            }
            //if (ModelState.IsValid)
            //
           //đoạn này có thể cho
            ////var trangthaip =( from p in qLKSpart4.phongs join dp in qLKSpart4.datPhongs on p.MaPhong equals dp.MaPhong where (dp.NgayTraDuDinh < DateTime.Now) orderby dp.NgayTraDuDinh descending select p).FirstOrDefault();


            //    trangthaip.Tinhtrangphong = "TRONG";

            //qLKSpart4.SaveChanges();


            //thêm khách hàng mới:
            int MaKhachHang;
          //  int makh2;
           // var kh = qLKSpart4.khachHangs.Where(x => x.Cccd == CC).FirstOrDefault();
            var listkh = qLKSpart4.khachHangs;
            if (listkh.ToList().Count() == 0)
            {
                
                MaKhachHang = 1;
                KhachHang khnew = new KhachHang
                {
                    IdKhachHang = MaKhachHang.ToString(),
                    Cccd = CC,
                    Email = em,
                    TenTaiKhoan = Ten,
                };
                qLKSpart4.khachHangs.Add(khnew);
                qLKSpart4.SaveChanges();


            }
            else
            {
               var list= listkh.Where(x => x.Cccd == CC).ToList().Count();
                if (list == 0)
                {
                    MaKhachHang = (Convert.ToInt16(listkh.ToList().Last().IdKhachHang) + 1);

                    KhachHang khachHang = new KhachHang
                    {
                        IdKhachHang = MaKhachHang.ToString(),
                        TenTaiKhoan = Ten,
                        Email = em,
                        Cccd = CC,
                    };
                    qLKSpart4.khachHangs.Add(khachHang);
                    qLKSpart4.SaveChanges();
                    }
                
            }
          //  if(listkh.Count()>0);
        //    makh2 == Convert.ToInt16(qLKSpart4.khachHangs.ToList().Last().IdKhachHang) + 1;

            //if (kh == null){
            //   var listkh= qLKSpart4.khachHangs.ToList().Count();
            //    if (listkh == 0) MaKhachHang = 1;
            //    MaKhachHang = Convert.ToInt16(qLKSpart4.khachHangs.ToList().Last().IdKhachHang) + 1;
            //    KhachHang khnew = new KhachHang
            //    {
            //        IdKhachHang = MaKhachHang.ToString(),
            //        Cccd = CC,
            //        Email = em,
            //        TenTaiKhoan = Ten,
            //    };
            //    qLKSpart4.khachHangs.Add(khnew);
            //    qLKSpart4.SaveChanges();
                
            //}
            var Idkh= qLKSpart4.khachHangs.Where(k=>k.Cccd==CC).Select(k=>k.IdKhachHang ).FirstOrDefault();

            //
            var id = map;
            

            var phong = qLKSpart4.phongs.Where(p => p.MaPhong == id && p.Tinhtrangphong == "TRONG").FirstOrDefault();
            var ph = qLKSpart4.phongs.Where(p => p.MaPhong == id).FirstOrDefault();
            //var phong2 = qLKSpart4.phongs.Where(p => p.MaPhong == id && p.Tinhtrangphong == "Dadat").FirstOrDefault();
            //if (phong2!=null) { 
            //    var thoigiandi = phong2.
            //    if(phong2.<DateTime.Now )
            //      }

            //đoạn này tạm cmt

            //var outdate = qLKSpart4.datPhongs.Where(p => p.MaPhong == id).Select(p => p.NgayTraDuDinh).FirstOrDefault();
            //if (outdate != null) 
            //{
            //    DateTime ngaytra = (DateTime)outdate;

            //    int check_han = (DateTime.Now - ngaytra).Days;
            //}
            //else
            //{

            //}
            //var ngaytra = qLKSpart4.datPhongs.Where(p => p.MaPhong == id).Select(l => l.NgayTraDuDinh).OrderByDescending(dp=>dp.Nga).LastOrDefault();
            var donDatPhongGanNhat = qLKSpart4.datPhongs
    .Where(dp => dp.MaPhong == id)
    .OrderByDescending(dp => dp.MaDatPhong)
    .FirstOrDefault();  
            int check_han=0;
            if(donDatPhongGanNhat  != null)
            {
                 check_han = ((DateTime.Now - (DateTime)donDatPhongGanNhat.NgayTraDuDinh)).Days;
            }
            //if (check_han < 0)
            //{
            //    check_han 
            //}
            if ((phong != null)||(check_han>0))
            {
            //    var ngaydi1 = Convert.ToDateTime(NgayDi);
                DateTime ngaydi = DateTime.ParseExact(NgayDi,"yyyy-MM-dd", null);
                DateTime ngayden = DateTime.ParseExact(NgayDen, "yyyy-MM-dd", null);
                int songay = (ngaydi - ngayden).Days;
                var dongiaph = ph.GiaThue*1000;
                var tongtien = songay * dongiaph+giatiendv * songay*songuoiden;
                
                ph.Tinhtrangphong = "Dadat";
                qLKSpart4.SaveChanges();


                //phong.Tinhtrangphong = "trong";
                //var checkphongbidatchua = qLKSpart4.phongs.Where(p => p.MaPhong == id&&p.Tinhtrangphong=="Da dat" ).First();
                //if (checkphongbidatchua != null)
                int MaDatPhong;
                var listDatPhong = qLKSpart4.datPhongs.ToList();
                if (listDatPhong.Count == 0) MaDatPhong = 1;
                else  MaDatPhong = listDatPhong.Last().MaDatPhong + 1;
                DatPhong dp = new DatPhong
                {
                    MaDatPhong = MaDatPhong,
                    Cancuoccd = CC,
                    TenKhachHang = Ten,
                    Tinhtrangthanhtoan = "Chờ thanh toán",
                    DichVu = dv,
                    Thoigianthanhtoan = DateTime.Now,
                    Phuongthucthanhtoan = TypePayment == 1 ? "Cod" : "Chuyển khoản",
                    ThanhTien = tongtien,
                    NgayDenDuDinh = ngayden,
                    NgayTraDuDinh = ngaydi,
                    NgayDat = DateTime.Now,
                    IdKhachHang = Idkh,
                    MaPhong = map

                    

                 };
                if (TypePayment == 1)
                {
                    dp.Thoigianthanhtoan = null;
                };

             
                //datPhong.Tinhtrangthanhtoan = "Chờ thanh toán";
                //datPhong.Thoigianthanhtoan = DateTime.Now;
                //datPhong.Phuongthucthanhtoan = TypePayment == 1 ? "COD" : "Chuyển khoản";

                //model.Phuongthucthanhtoan = TypePaymentVN == 1 ? "VNPAYQR" : TypePaymentVN == 2 ? "ATM" : "Thẻ quốc tế";

                qLKSpart4.datPhongs.Add(dp);
                qLKSpart4.SaveChanges();

                if (TypePayment != 1) 
                {
                    var urlPayment = UrlPayment(TypePaymentVN, dp.MaDatPhong);
                    return Redirect(urlPayment);
                }
                //send email
                GenerateRandomPassword pwnew = new GenerateRandomPassword();
                string matkhau = pwnew.GenerateRandomPassword2(6);
                //Cập nhật mật khẩu tạm thời của khách
             //   KhachHang kh = new KhachHang();
               var kh= qLKSpart4.khachHangs.Where(k => k.IdKhachHang == dp.IdKhachHang&&k.MatKhau==null).FirstOrDefault();
                if (kh != null)
                {
                    kh.MatKhau = matkhau;

                    qLKSpart4.SaveChanges();
                }
                //

                var email = em;
                HamGuiEmailXacNhan guiem= new HamGuiEmailXacNhan();
                guiem.SendInvoiceEmail(email, MaDatPhong, Ten, map, DateTime.Now, ngayden, ngaydi, dv,dp.ThanhTien, matkhau, dp.Tinhtrangthanhtoan);
                //
                return View("Datphongchuathanhtoan");
               
             }
            else
            {
                //  phong.Tinhtrangphong = "trong";
                var maloaiph = qLKSpart4.phongs.Where(p => p.MaPhong == id).Select(p => p.MaLoai).First();
                var phongchuabidat = qLKSpart4.phongs.Where(p => p.MaLoai == maloaiph && p.Tinhtrangphong == "TRONG").ToList();
                var anhloaip = qLKSpart4.loaiPhongs.Where(p => p.MaLoai == maloaiph).Select(lp => lp.DuongDanAnh).First();
                ViewBag.anhlp = anhloaip;

                //var anhphong = phongchuabidat.First().anhChiTietPhongs;
                //ViewBag.ap = anhphong;

                return View("GoiYPhongDat", phongchuabidat);


            }

            //return View(dp);
        }

        #region Thanh toán vnpay
        public string UrlPayment(int TypePaymentVN, int orderCode)
        {
            var urlPayment = "";
            var order = qLKSpart4.datPhongs.FirstOrDefault(x => x.MaDatPhong == orderCode);
            //Get Config Info
            string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Secret Key

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();
            var Price = (long)order.ThanhTien * 100;
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", Price.ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            if (TypePaymentVN == 1)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            }
            else if (TypePaymentVN == 2)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            }
            else if (TypePaymentVN == 3)
            {
                vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            }

            vnpay.AddRequestData("vnp_CreateDate", order.NgayDat?.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don dat phong:" + order.MaDatPhong.ToString());
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.MaDatPhong.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            string txtExpire = DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss");
            vnpay.AddRequestData("vnp_ExpireDate", txtExpire);
            //Billing

            urlPayment = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            //log.InfoFormat("VNPAY URL: {0}", paymentUrl);
            return urlPayment;
        }
        #endregion
    }
}