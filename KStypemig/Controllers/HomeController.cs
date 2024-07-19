using KStypemig.Models;
using KStypemig.Models.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KStypemig.Controllers
{
    public class HomeController : Controller
    {
        QLKSpart4Context qlks = new QLKSpart4Context();
        public ActionResult Index()
        {
            int sessionTimeout = Session.Timeout;

            // Hiển thị thời gian sống của session trong view
            ViewBag.SessionTimeout = sessionTimeout;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

       

        public ActionResult DSLoaiPhong(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            var products = qlks.loaiPhongs.OrderBy(p => p.MaLoai).ToPagedList(pageNumber, pageSize);

            return View(products);

        }
        public ActionResult PhongDetail(string malp, int? page)
        {
            ViewBag.malp = malp;
            int pageSize = 2; // Số lượng mục trên mỗi trang
            int pageNumber = (page ?? 1); // Trang hiện tại, mặc định là trang 1


            var phong = qlks.phongs.Where(x => x.MaLoai == malp).Include(x => x.anhChiTietPhongs).ToList();
            var phong1 = phong.Select(p => new AnhChiTietPhong_ViewModel
            {
                Phong = p,
                AnhChiTietPhongs=p.anhChiTietPhongs,
            }).ToList();
            var phongpagelist = phong1.ToPagedList(pageNumber, pageSize);
            var phonglist = new AnhChitietListPhong_Viewmodel
            {

                anhChiTietPhong_ViewModels = phongpagelist,
            };
            return View(phonglist);
           
        }
        public ActionResult DatPhong(string maph)
        {
            //var datphong = qlks.phongs.Where(p1  => p1.MaPhong == maph).ToList();
            //var ph =datphong.Select(p=> new AnhChiTietPhong_ViewModel
            //{
            //    Phong = p,
            //    AnhChiTietPhongs = p.anhChiTietPhongs,
            //}); 
            //var p2 = new AnhChitietListPhong_Viewmodel
            //{
            //    anhChiTietPhong_ViewModels = ph, 
            //}; 
            var dp2 = qlks.phongs.Where(p => p.MaPhong == maph).First();
            var dp  = qlks.anhChiTietPhongs.Where(a=>a.Maphong==maph).Include(p=>p.phong).ToList();

            var tenp = (from p in qlks.phongs
                        join loaip in qlks.loaiPhongs on p.MaLoai equals loaip.MaLoai
                        where (p.MaPhong == maph)
                        select loaip.TenLoai).FirstOrDefault();
                           
                       
            ViewBag.TenP = tenp;

            return View(dp);

        }
        public ActionResult DatPhongCuoiCung(string maph)
        {
            var p = qlks.phongs.Where(p1 => p1.MaPhong == maph);
            return View();
        }
        //blog
        public ActionResult Blog()
        {
            var blog = qlks.blogs?.ToList();
            return View(blog);
        }
        public ActionResult Blog_detail(int blogId)
        {
            var blog_detail = qlks.blogs.Where(x => x.IdBaiviet == blogId).FirstOrDefault();
            return View(blog_detail);
        }
        public ActionResult Contact()
        {
            return View();
        }
    }
}