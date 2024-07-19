using KStypemig.Models.Botmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KStypemig.Controllers.ControllerAPI
{
    //  [Route("api/[controller]")]
    public class ProductsController : ApiController
    {
        // // GET api/<controller>
        //// [HttpGet]
        // public IEnumerable<string> Get()
        // {
        //     return new string[] { "value1", "value2" };
        // }
        // //[HttpGet]
        // // GET api/<controller>/5
        // public string Get(int id)
        // {
        //     return "value";
        // }

        // // POST api/<controller>
        // public void Post([FromBody] string value)
        // {
        // }

        // // PUT api/<controller>/5
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // // DELETE api/<controller>/5
        // public void Delete(int id)
        // {
        // }
        Product[] products = new Product[]
      {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
      };

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}