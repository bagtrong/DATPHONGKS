using KStypemig.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace KStypemig.Controllers
{
    public class ReviewController : Controller
    {
        QLKSpart4Context qLKSpart4 = new QLKSpart4Context();
        // GET: Review
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet] 
        public ActionResult Add_Review()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Review( Review review)
        {
            if (ModelState.IsValid)
            {
                Review review1 = new Review();
                review1.Username = review.Username;
                review1.Content = review.Content;
                review1.Rate = review.Rate;
                review1.CreateDate = DateTime.Now;
                review1.Email=review.Email;
            }
            return View();
        }
        [HttpGet]
        public ActionResult getbinhluan()
        {
            var cm = qLKSpart4.reviews.ToList();
            return Json(cm,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Addbinhluan(Review review)
        {
            if (ModelState.IsValid)
            {

                review.CreateDate = DateTime.Now;
                qLKSpart4.reviews.Add(review);
                qLKSpart4.SaveChanges();
                return Json(new { success = true, message = "comment added successfully" });


            }
            return Json(new { success = false, message = "error while adding comment." });
        }
        
    }
}