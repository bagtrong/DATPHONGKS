using KStypemig.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KStypemig.Controllers.ControllerAPI
{
    public class AddReviewController : Controller
    {
        // GET: AddReview
        public ActionResult Index()
        {
            return View();
        }

        private string apiUrl = "https://localhost:44356/api/comments"; // Thay đổi 'port' thành cổng thực tế của bạn

        // GET: DatPhong/Reviews
        public async Task<ActionResult> Reviews()
        {
            List<Review> comments = new List<Review>();
            using (var handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;
                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(apiUrl);
                    HttpResponseMessage response = await client.GetAsync("comments");
                    if (response.IsSuccessStatusCode)
                    {
                        comments = await response.Content.ReadAsAsync<List<Review>>();
                    }
                    else
                    {
                        // Log response or handle the case when the response is not successful
                        throw new Exception($"API call failed with status code: {response.StatusCode}");
                        //log chi tiết lỗi
                        var responseContent = await response.Content.ReadAsStringAsync();
                        System.Diagnostics.Debug.WriteLine($"API call failed with status code: {response.StatusCode}, Content: {responseContent}");
                        throw new Exception($"API call failed with status code: {response.StatusCode}, Content: {responseContent}");
                    }
                }
            }
            return View(comments);
        }

        // POST: DatPhong/AddReview
        [HttpPost]
        public async Task<ActionResult> AddReview(Review comment, string maph)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    HttpResponseMessage response = await client.PostAsJsonAsync("comments", comment);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Reviews");
                    }
                }
            }
            return View("Error");
        }
    }
}