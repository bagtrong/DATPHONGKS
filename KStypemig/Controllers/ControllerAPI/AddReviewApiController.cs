using KStypemig.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Windows.Forms;

namespace KStypemig.Controllers.ControllerAPI
{
    public class AddReviewApiController : ApiController
    {
        private QLKSpart4Context qlks = new QLKSpart4Context();

        // GET: api/comments
        [HttpGet]
        [Route("api/comments")]
        public IEnumerable<Review> GetComments()
        {
            return qlks.reviews.Include("KhachHang").Include("phongs").ToList();
        }

        // POST: api/comments
        [HttpPost]
        [Route("api/comments")]
        public IHttpActionResult PostComment(Review comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            comment.CreateDate = DateTime.Now;
            qlks.reviews.Add(comment);
            qlks.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = comment.Id }, comment);
        }
    }
}
