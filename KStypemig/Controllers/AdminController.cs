using KStypemig.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace KStypemig.Controllers
{
    public class AdminController : Controller
    {
        QLKSpart4Context qlks
            = new QLKSpart4Context();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DsTaikhoan(int? page)
        {
            if (TempData["tb"] != null)
            {
                ViewBag.tbao = TempData["tb"];
            }
            int pageSize = 2;
            int pageNumber = (page ?? 1);

            var products = qlks.khachHangs.OrderBy(p => p.IdKhachHang).ToPagedList(pageNumber, pageSize);

            return View(products);
            
        }
        public ActionResult DsNhanvien(int? page)
        {
            if (Session["matk"]!=null&& Session["matk"].ToString() == "qly")
            {
                int pageSize = 2;
                int pageNumber=(page ?? 1);
                var products = qlks.nhanViens.OrderBy(p => p.IdNhanVien).ToPagedList(pageNumber, pageSize);
                return View(products);
            }
            ViewBag.tb = "bạn không có quyền truy cập thông tin này!";
            return View();
        }
         public ActionResult DsLoaiphong(int? page
             )
        {
            if (TempData["tb"] != null)
            {
                ViewBag.tbao = TempData["tb"];
            }
            if (Session["matk"] != null && Session["matk"].ToString() == "qly")
            {

                int pageSize = 2;
                int pageNumber = (page ?? 1);
                var loaiphong = qlks.loaiPhongs.OrderBy(p => p.MaLoai).ToPagedList(pageNumber, pageSize);
                return View(loaiphong);
            }
            ViewBag.tb = "bạn không có quyền truy cập thông tin này!";
            return View();


        }
        public ActionResult DsPhong(int? page) 
        {
            if (Session["matk"] != null && Session["matk"].ToString() == "qly")
            {
                if (TempData["tbao"] != null)
                {
                    ViewBag.tb = TempData["tbao"];
                   
                }
                int pageSize = 2;
                int pageNumber = (page ?? 1);
                var loaiphong = qlks.phongs.OrderBy(p => p.MaPhong).ToPagedList(pageNumber, pageSize);
                return View(loaiphong);
            }
            ViewBag.tb = "bạn không có quyền truy cập thông tin này!";
            return View();

        }
        public ActionResult DsDichvu(int? page )
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            var dichvu = qlks.dichVus.OrderBy(p => p.MaDichVu).ToPagedList(pageNumber, pageSize);
            return View(dichvu
                );
           
        }
        public ActionResult Dsdatphong(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var datPhongs = qlks.datPhongs.OrderBy(p => p.MaDatPhong).ToPagedList(pageNumber, pageSize);
            return View(datPhongs  );
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangXuat()
        {
            Session.Remove("matk");
            Session.Remove("tentk");
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public ActionResult DangNhap(string tentk, string matkhau)
        {
            var tk = qlks.nhanViens.FirstOrDefault(kh => kh.HoTen == tentk && kh.MatKhau == matkhau);
            if (tk != null &&tk.LaAdmin==false)
            {
                Session["tentk"] = tentk;
                Session["matk"] = "nhanvien";
                return RedirectToAction("Index", "Admin");
            }
            else if(tk!=null&&tk.LaAdmin==true)
            {
                Session["tentk"] = tentk;
                Session["matk"] = "qly";
                return RedirectToAction("Index", "Admin");

            }
            else
            {
                ViewBag.saitt = "Ban da nhap sai ten tai khoan hoac mat khau!";
            }
             return View();
        }
        [HttpGet]
        public ActionResult ThemPhong()
        {
            var loaiPhongList = qlks.loaiPhongs.Select(lp => new { lp.MaLoai, lp.TenLoai }).ToList();
            ViewBag.MaLoai = new SelectList(loaiPhongList, "MaLoai", "TenLoai");

            return View();
        }
        [HttpPost]
        public ActionResult ThemPhong(Phong ph, string maphong,string tenphong, string maloai, string dientich, string tinhtrangphong, string songuoitoida, string giathue ,string sogiuong)
        {
            var loaiPhongList = qlks.loaiPhongs.Select(lp => new { lp.MaLoai, lp.TenLoai }).ToList();
            ViewBag.MaLoai = new SelectList(loaiPhongList, "MaLoai", "TenLoai");
            Phong phnew = new Phong();
            phnew.MaPhong = maphong;
            phnew.MaLoai = maloai;
            phnew.TenPhong = tenphong;
            phnew.Sogiuong = int.Parse(sogiuong);
            phnew.Songuoitoida = int.Parse(songuoitoida);
            phnew.DienTich = int.Parse(dientich);
            phnew.Tinhtrangphong = tinhtrangphong
                ;
            phnew.GiaThue = int.Parse(giathue);
            //var phong = ph;
            qlks.phongs.Add(phnew);
            qlks.SaveChanges();
            return RedirectToAction("DsPhong");
        }
        [HttpGet]
        public ActionResult SuaPhong(string map)
        {
            var phong = qlks.phongs.FirstOrDefault(p2 => p2.MaPhong == map);
            var loaiPhongList = qlks.loaiPhongs.Select(lp => new { lp.MaLoai, lp.TenLoai }).ToList();
            ViewBag.MaLoai = new SelectList(loaiPhongList, "MaLoai", "TenLoai");


            var p = qlks.phongs.FirstOrDefault(ph => ph.MaPhong == map);
            return View(phong);
        }
        [HttpPost]
        public ActionResult SuaPhong(Phong phong)
        {
            var loaiPhongList = qlks.loaiPhongs.Select(lp => new { lp.MaLoai, lp.TenLoai }).ToList();
            ViewBag.MaLoai = new SelectList(loaiPhongList, "MaLoai", "TenLoai" );

            var p = qlks.phongs.Where(ph => ph.MaPhong == phong.MaPhong).FirstOrDefault();
            p.Tinhtrangphong = phong.Tinhtrangphong;
            p.TenPhong = phong.TenPhong;
            p.DienTich = phong.DienTich;
            p.GiaThue = phong.GiaThue;
            p.MaLoai = phong.MaLoai;
            p.Ghichu = phong.Ghichu;
            p.Sogiuong = phong.Sogiuong;
            p.Songuoitoida = phong.Songuoitoida;
            qlks.SaveChanges();
            return RedirectToAction("DsPhong");
        }
        //[HttpGet]
        //public ActionResult XoaPhong(string maphong)
        //{
        //    var p = qlks.phongs.Where(ph => ph.MaPhong == maphong).FirstOrDefault();
        //    qlks.phongs.Remove(p);
        //    qlks.SaveChanges();
        //   return  RedirectToAction("DsPhong");
        //}
        //
        [HttpGet]
        public ActionResult ThemLoaiPhong()
        {
            var loaiPhongList = qlks.loaiPhongs.Select(lp => new { lp.MaLoai, lp.TenLoai }).ToList();
            ViewBag.MaLoai = new SelectList(loaiPhongList, "MaLoai", "TenLoai");

            return View();
        }
        [HttpPost]
        public ActionResult ThemLoaiPhong(LoaiPhong lph, HttpPostedFileBase hinhanh, string maloai, string tenloai, string ghichu, string soluong)
        {
            LoaiPhong lpnew = new LoaiPhong();
            lpnew.MaLoai = maloai;
            lpnew.TenLoai = tenloai;
            lpnew.GhiChu = ghichu;
            lpnew.SoluongPhong = int.Parse(soluong);
            if (hinhanh != null && hinhanh.ContentLength > 0)  
            {
            // Lưu hình ảnh vào thư mục trên server
            var fileName = Path.GetFileName(hinhanh.FileName);
            var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
            hinhanh.SaveAs(path);

            // Cập nhật đường dẫn hình ảnh trong model
            lph.DuongDanAnh = "/Content/images/" + fileName;
                lpnew.DuongDanAnh= "/Content/images/" + fileName; 
                //
            }
                var loaiPhongList = qlks.loaiPhongs.Select(lp => new { lp.MaLoai, lp.TenLoai }).ToList();
            ViewBag.MaLoai = new SelectList(loaiPhongList, "MaLoai", "TenLoai");
            //
           
            ////
            var phong = lph;
            qlks.loaiPhongs .Add(lpnew);
            qlks.SaveChanges();
            return RedirectToAction("DsLoaiphong");
        }
        [HttpGet]
        public ActionResult SuaLoaiPhong(string map)
        {
            var phong = qlks.phongs.FirstOrDefault(p2 => p2.MaPhong == map);
            var loaiPhongList = qlks.loaiPhongs.Select(lp => new { lp.MaLoai, lp.TenLoai }).ToList();
            ViewBag.MaLoai = new SelectList(loaiPhongList, "MaLoai", "TenLoai");


            var p = qlks.loaiPhongs.FirstOrDefault(ph => ph.MaLoai== map);
            return View(p);
        }
        [HttpPost]
        public ActionResult SuaLoaiPhong(LoaiPhong phong)
        {
            var loaiPhongList = qlks.loaiPhongs.Select(lp => new { lp.MaLoai, lp.TenLoai }).ToList();
            ViewBag.MaLoai = new SelectList(loaiPhongList, "MaLoai", "TenLoai");

            var p = qlks.loaiPhongs.Where(ph => ph.MaLoai == phong.MaLoai).FirstOrDefault();
            p.GhiChu = phong.GhiChu;
            p.SoluongPhong = phong.SoluongPhong;
            p.TenLoai = phong.TenLoai;
            p.DuongDanAnh = phong.DuongDanAnh;
            //p.MaLoai = phong.MaLoai;
            //p. = phong.Ghichu;
            //p.Sogiuong = phong.Sogiuong;
            //p.Songuoitoida = phong.Songuoitoida;
            qlks.SaveChanges();
            return RedirectToAction("DsLoaiphong");
        }
        //[HttpGet]
        //public ActionResult XoaLoaiPhong(string maphong)
        //{
        //    var p = qlks.loaiPhongs.Where(ph => ph.MaLoai == maphong).FirstOrDefault();
        //    qlks.loaiPhongs.Remove(p);
        //    qlks.SaveChanges();
        //    return RedirectToAction("DsLoaiphong");
        //}
        // dịch vụ
        [HttpGet]
        public ActionResult ThemDichVu()
        {
            //var loaiPhongList = qlks.loaiPhongs.Select(lp => new { lp.MaLoai, lp.TenLoai }).ToList();
            //ViewBag.MaLoai = new SelectList(loaiPhongList, "MaLoai", "TenLoai");

            return View();
        }
        [HttpPost]
        public ActionResult ThemDichVu(DichVu dv)
        {
         
            var phong = dv;
            qlks.dichVus.Add(dv);
            qlks.SaveChanges();
            return RedirectToAction("DsDichvu");
        }
        [HttpGet]
        public ActionResult SuaDichVu(string madv)
        {
          //  var phong = qlks.dichVus.FirstOrDefault(p2 => p2.MaDichVu == madv);
            //var loaiPhongList = qlks.loaiPhongs.Select(lp => new { lp.MaLoai, lp.TenLoai }).ToList();
            //ViewBag.MaLoai = new SelectList(loaiPhongList, "MaLoai", "TenLoai");


            var p = qlks.dichVus.FirstOrDefault(ph => ph.MaDichVu == madv);
            return View(p);
        }
        [HttpPost]
        public ActionResult SuaDichVu(DichVu dichVu)
        {
            //var loaiPhongList = qlks.dichVus.Select(lp => new { lp.MaLoai, lp.TenLoai }).ToList();
            //ViewBag.MaLoai = new SelectList(loaiPhongList, "MaLoai", "TenLoai");

            var p = qlks.dichVus.Where(ph => ph.MaDichVu == dichVu.MaDichVu).FirstOrDefault();
            p.TenDichVu = dichVu.TenDichVu;
            p.GiaDichVu = dichVu.GiaDichVu;
            
            //p.MaLoai = phong.MaLoai;
            //p. = phong.Ghichu;
            //p.Sogiuong = phong.Sogiuong;
            //p.Songuoitoida = phong.Songuoitoida;
            qlks.SaveChanges();
            return RedirectToAction("DsDichvu");
        }
        [HttpGet]
        public ActionResult XoaDichvu(string madv)
        {
            var p = qlks.dichVus.Where(ph => ph.MaDichVu == madv).FirstOrDefault();
            qlks.dichVus.Remove(p);
            qlks.SaveChanges();
            return RedirectToAction("DsDichvu");
        }
        // hóa đơn đặt phòng
        [HttpGet]
        public ActionResult ThemHoadon()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemHoadon(DatPhong dp)
        {
            var dph = dp;
            qlks.datPhongs.Add(dp);
            qlks.SaveChanges();
            return RedirectToAction("Dsdatphong");      
        }
        [HttpGet]
        public ActionResult SuaHoadon(int mahd)
        { 
            var dp=
            qlks.datPhongs.Where(d => d.MaDatPhong == mahd).FirstOrDefault();
            return View(dp);
        }
        [HttpPost]
        public ActionResult SuaHoadon(DatPhong dp)
        {
            var p = qlks.datPhongs.Where(ph => ph.MaDatPhong == dp.MaDatPhong).FirstOrDefault();
           // p.MaPhong = dp.MaPhong;
            p.NgayDat = dp.NgayDat;
            p.TenKhachHang = dp.TenKhachHang;
            p.Cancuoccd = dp.Cancuoccd;
            p.DichVu = dp.DichVu;
            p.NgayDenDuDinh = dp.NgayDenDuDinh;
            p.NgayTraDuDinh = dp.NgayTraDuDinh;
            p.Tinhtrangthanhtoan = dp.Tinhtrangthanhtoan;
            p.Thoigianthanhtoan = dp.Thoigianthanhtoan;
            p.Phuongthucthanhtoan = dp.Phuongthucthanhtoan;
            //p.MaLoai = phong.MaLoai;
            //p. = phong.Ghichu;
            //p.Sogiuong = phong.Sogiuong;
            //p.Songuoitoida = phong.Songuoitoida;
            qlks.SaveChanges();
            return RedirectToAction("Dsdatphong");
        }
        [HttpGet]
        public ActionResult XoaHoadon(int madp)
        {
            var p = qlks.datPhongs.FirstOrDefault(ph => ph.MaDatPhong == madp);
            qlks.datPhongs.Remove(p);
            qlks.SaveChanges();
            return RedirectToAction("Dsdatphong");
        }
        [HttpGet]
        public ActionResult ThemNhanvien()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemNhanvien(NhanVien nv)
        {
            qlks.nhanViens.Add(nv);
            qlks.SaveChanges();

            return RedirectToAction("DsNhanvien");
        }
        [HttpGet]
        public ActionResult SuaNhanvien(string manv)
        {
            var nv = qlks.nhanViens.FirstOrDefault(n => n.IdNhanVien == manv);

            return View(nv);
        }
        [HttpPost]
        public ActionResult SuaNhanvien(NhanVien nv)
        {
            var nvien = qlks.nhanViens.FirstOrDefault(n => n.IdNhanVien == nv.IdNhanVien);
            nvien.HoTen = nv.HoTen;
            nvien.Cccd = nv.Cccd;
            nvien.LaAdmin =         nv.LaAdmin;
            nvien.SoDienThoai = nv.SoDienThoai;
            nvien.MatKhau = nv.MatKhau;
            nvien.Email = nv.Email;
            nvien.Vaitro = nv.Vaitro;
            qlks.SaveChanges();
            return RedirectToAction("DsNhanvien");
        }
        [HttpGet]
        public ActionResult XoaNhanvien(string manv)
        {
            var nv = qlks.nhanViens.FirstOrDefault(n => n.IdNhanVien == manv);
            qlks.nhanViens.Remove(nv);
            qlks.SaveChanges();
            return RedirectToAction("DsNhanvien");
        }
        
        //
        [HttpGet]
        public ActionResult Themkhachhang()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Themkhachhang(KhachHang kh)
        {
            var loaiPhongList = qlks.loaiPhongs.Select(lp => new { lp.MaLoai, lp.TenLoai }).ToList();
            ViewBag.MaLoai = new SelectList(loaiPhongList, "MaLoai", "TenLoai");

            var phong = kh;
            qlks.khachHangs.Add(phong);
            qlks.SaveChanges();
            return RedirectToAction("DsTaiKhoan");
        }
        [HttpGet]
        public ActionResult Suakhachhang(string makh)
        {
            //var phong = qlks.phongs.FirstOrDefault(p2 => p2.MaPhong == map);
            //var loaiPhongList = qlks.loaiPhongs.Select(lp => new { lp.MaLoai, lp.TenLoai }).ToList();
            //ViewBag.MaLoai = new SelectList(loaiPhongList, "MaLoai", "TenLoai");


            var kh = qlks.khachHangs.FirstOrDefault(ph => ph.IdKhachHang == makh);
            return View(kh);
        }
        [HttpPost]
        public ActionResult Suakhachhang(KhachHang kh)
        {
            //var loaiPhongList = qlks.khachHangs.Select(lp => new { lp.MaLoai, lp.TenLoai }).ToList();
            //ViewBag.MaLoai = new SelectList(loaiPhongList, "MaLoai", "TenLoai");

            var p = qlks.khachHangs.Where(ph => ph.IdKhachHang == kh.IdKhachHang).FirstOrDefault();
            p.MatKhau =kh.MatKhau;
            p.Cccd = kh.Cccd;
            p.TenTaiKhoan = kh.TenTaiKhoan;
            p.Email = kh.Email;
            p.SoDienThoai = kh.SoDienThoai;
            qlks.SaveChanges();
            return RedirectToAction("DsTaiKhoan");
        }
        //[HttpGet]
        //public ActionResult Xoakhachhang(string id)
        //{
        //    var p = qlks.khachHangs.Where(ph => ph.IdKhachHang == id).FirstOrDefault();
        //    qlks.khachHangs.Remove(p);
        //    qlks.SaveChanges();
        //    return RedirectToAction("DsTaiKhoan");
        //}
        //thống kê
        public ActionResult Doanhthutheongay()
        {
            Dictionary<string, int> dicday = new Dictionary<string, int >();
            var dttuan = qlks.datPhongs?.ToList();
            if (dttuan != null)
            {
                foreach (var ngay in dttuan)
                {
                    if(ngay.Tinhtrangthanhtoan=="Đã thanh toán")
                    { 
                    var keyngay = ngay.NgayTraDuDinh?.ToString("yyyy-MM-dd");
                    //   string keyngay = ngay.NgayTra?.ToString("yyyy/MM/dd");
                    if (!dicday.ContainsKey(keyngay))
                    {
                        dicday[keyngay] = 0;
                    }
                    dicday[keyngay] += (int)ngay.ThanhTien;
                    }
                }

                ViewBag.keyngay = dicday.Keys.ToList();
                ViewBag.dtngay = dicday.Values.ToList();
                //  return View();

            }
            else
            {
                Debug.WriteLine("dttuan null");
            }
            //ViewBag.keyngay = dicday.Keys.ToList();
            //ViewBag.dtngay = dicday.Values.ToList();
            ViewBag.sldtngay = dicday.Count();
            return View();

        }
        //doanh thu theo tháng
        public ActionResult doanhthutheothang()
        {
            if (qlks.datPhongs != null)
            {
                //lấy dữ liệu từ bảng đặt ph
                var doanhthu = qlks.datPhongs?.ToList();
                //Tổng hopwh dữ liệu theo tháng
                var monthpayment = new Dictionary<string, int>();
                foreach (var doanhthu1 in doanhthu)
                {
                    if(doanhthu1.Tinhtrangthanhtoan== "Đã thanh toán") 
                    { 
                    var doanhthu2 = doanhthu1.NgayTraDuDinh?.ToString("yyyy-MM");
                    if (!monthpayment.ContainsKey(doanhthu2))
                    {
                        monthpayment[doanhthu2] = 0;
                    }
                    monthpayment[doanhthu2] += (int)doanhthu1.ThanhTien;
                    }
                }
                ViewBag.Thang = monthpayment.Keys.ToList();
                ViewBag.TienofMonth = monthpayment.Values.ToList();
                return View();
            }
            else
            {
                Debug.WriteLine("modelQLKS is null");
            }
            return RedirectToAction("DSDichVu", "Admin");
        }
        //thống kê theo năm
        public ActionResult Doanhthunam()
        {
            Dictionary<string, int> dicday = new Dictionary<string, int>();
            var dtnam = qlks.datPhongs?.ToList();
            if (dtnam != null)
            {
                foreach (var nam in dtnam)
                {
                    if(nam.Tinhtrangthanhtoan== "Đã thanh toán") 
                    { 
                    var keynam = nam.NgayTraDuDinh?.ToString("yyyy");
                    //   string keyngay = ngay.NgayTra?.ToString("yyyy/MM/dd");
                    if (!dicday.ContainsKey(keynam))
                    {
                        dicday[keynam] = 0;
                    }
                    dicday[keynam] += (int)nam.ThanhTien;
                    }
                }

                ViewBag.keynam = dicday.Keys.ToList();
                ViewBag.dtnam = dicday.Values.ToList();
                //  return View();

            }
            else
            {
                Debug.WriteLine("dttuan null");
            }
            //ViewBag.keyngay = dicday.Keys.ToList();
            //ViewBag.dtngay = dicday.Values.ToList();
            ViewBag.sldtngay = dicday.Count();
            return View();

        }
        public ActionResult xoaphong(string maphong)
        {
            var dp = qlks.datPhongs.Where(x => x.MaPhong == maphong).FirstOrDefault();
            if (dp!=null) {
                TempData["tbao"] = "Phòng này không thể xóa được do đã có đơn đặt!";
                return RedirectToAction ("Dsphong","admin");
            }
            var ph = qlks.phongs.Where(p => p.MaPhong == maphong).FirstOrDefault();
            qlks.phongs.Remove(ph);
            qlks.SaveChanges();
            return RedirectToAction("Dsphong", "admin");
        }
        public ActionResult xoaloaiphong(string maloai)
        { 
        var lph=
            qlks.loaiPhongs.Where(lp => lp.MaLoai == maloai).FirstOrDefault();
            var p=qlks.phongs.Where(ph=>ph.MaLoai== maloai).FirstOrDefault();
            if (p != null)
            {
                TempData["tb"] = "Không thể xóa loại phòng này do đang có phòng thuộc loại này!";
                return RedirectToAction("Dsloaiphong");
            }
            qlks.loaiPhongs.Remove(lph);
            qlks.SaveChanges();
            return RedirectToAction("Dsloaiphong");
        }
        public ActionResult xoakhachhang(string makh)
        {
            var khdp = qlks.datPhongs.Where(k => k.IdKhachHang == makh).FirstOrDefault();
            if (khdp != null)
            {
                TempData["tb"] = "Không thể xóa khách hàng này do họ đã đặt phòng!";
                return RedirectToAction("Dstaikhoan");
            }
            var kh =
            qlks.khachHangs.Where(k => k.IdKhachHang == makh).FirstOrDefault();
            qlks.khachHangs.Remove(kh);
            qlks.SaveChanges();
            return RedirectToAction("Dstaikhoan");
        }
        //tin tuc 
        public ActionResult Dstintuc(int? page)
        {
            int pageSize = 2;
            int pageNumber= (page ?? 1);
            var t =
            qlks.blogs.OrderBy(p => p.IdBaiviet).ToPagedList(pageNumber, pageSize);
            return View(t);
        }
        [HttpGet]
        public ActionResult ThemTintuc()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ThemTintuc(Blog tnew)
        {
            qlks.blogs.Add(tnew);
            qlks.SaveChanges();
            return RedirectToAction("dstintuc");
        }

        [HttpGet]
        public ActionResult Suatintuc(int matt)
        {
           

            var p = qlks.blogs.FirstOrDefault(ph => ph.IdBaiviet ==matt);
            return View(p);
        }
        [HttpPost]
        public ActionResult Suatintuc(Blog blog)
        {
            var tin = new Blog();
            var p = qlks.blogs.Where(ph => ph.IdBaiviet ==blog.IdBaiviet).FirstOrDefault();
            p.Tieude = blog.Tieude;
            p.Noidung = blog.Noidung;
            p.Tacgia = blog.Tacgia;
            p.Ngaydang = blog.Ngaydang; 
            qlks.SaveChanges();
            return RedirectToAction("dstintuc");
        }
        [HttpGet]
        public ActionResult xoatintuc(int matt)
        {
            var t = qlks.blogs.Where(T => T.IdBaiviet  == matt).FirstOrDefault();
            qlks.blogs.Remove(t);
            qlks.SaveChanges();
            return RedirectToAction("dstintuc");
        }

       
        


    }
}