using Microsoft.AspNetCore.Mvc;
using API_Parameters_Demo.Models;

namespace API_Parameters_Demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class ProductController : ControllerBase
    {
        [Route("GetProductByQueryString")]
        [HttpGet]
        public IActionResult GetProductByQueryString(int? pid, string pname, decimal price)
        {
            return Ok(new { ID = pid, name = pname, productPrice = price });
        }


        [Route("GetProductById/{id}/{title}")]
        [HttpGet]
        public IActionResult GetProductById(int id, string title)
        {
            var products = new List<Product> {
                new() { ID = 1, Title = "Laptop", Price = 100.32m},
                new() { ID = 2, Title = "Perfume", Price = 200m},
                new() { ID = 3, Title = "Mobile", Price = 5000m},
                new() { ID = 4, Title = "Game", Price = 85.00m}
            };

            var singleProduct = products.FirstOrDefault(p => p.ID == id && p.Title == title);

            if (singleProduct == null)
                return NotFound(new { Message = $"Product with ID: {id} not found" });

            return Ok(singleProduct);
        }

    }
}
