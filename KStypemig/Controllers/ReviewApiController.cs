using KStypemig.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KStypemig.Controllers
{
    public class ReviewApiController : ApiController
    {
        QLKSpart4Context qLKSpart4 = new QLKSpart4Context();
       
        [HttpGet]
        [Route ("api/reviewapi/getbinhluan")]
        public IHttpActionResult getcomments()
        {
            var cmts = qLKSpart4.reviews.ToList();
            return Ok(cmts);
        }
        [HttpPost]
        [Route("api/reviewapi/addbinhluan")]
        public IHttpActionResult addbinhluan([FromBody] Review review  )
        {
            if ( ModelState.IsValid)
            {
                review.CreateDate = DateTime.Now;
                qLKSpart4.reviews.Add(review);
                qLKSpart4.SaveChanges();
                return Ok(new { success = true, message = "comment added successfully" });
            }
            return BadRequest(ModelState);
        }
        [HttpDelete]
        [Route("api/reviewapi/delete/{id}")]
        public IHttpActionResult DeleteReview(int id)
        {
            var review = qLKSpart4.reviews.FirstOrDefault(r => r.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            qLKSpart4.reviews.Remove(review);
            qLKSpart4.SaveChanges();
            return Ok(new { success = true, message = "Review deleted successfully" });
        }


    }
}
