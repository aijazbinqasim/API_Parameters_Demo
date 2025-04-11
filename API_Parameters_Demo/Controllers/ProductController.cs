using Microsoft.AspNetCore.Mvc;
using API_Parameters_Demo.Models;

namespace API_Parameters_Demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class ProductController : ControllerBase
    {

        // Following API endpoint/method binds/receive params (primitive types) from  Query string.
        [Route("GetProductByQueryString")]
        [HttpGet]
        public IActionResult GetProductByQueryString([FromQuery(Name = "id")] int pid, 
            [FromQuery(Name = "title")] string pname, 
            [FromQuery(Name = "price")] decimal price)
        {
            // return Ok(new { ID = pid, name = pname, productPrice = price });

            var product = new Product
            {
                ID = pid,
                Title = pname,
                Price = price
            };

            return Ok(product);
        }




        // Following API endpoint/method binds/receive params from Route.
        [Route("GetProductById/{pid}/{ptitle?}")]
        [HttpGet]
        public IActionResult GetProductById([FromRoute(Name = "pid")] int id, [FromRoute(Name = "ptitle")] string? title)
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




        // Following API endpoint/method binds/receive params (Model Binder) from Query string.
        [Route("GetProductByModelBinder")]
        [HttpGet]
        public IActionResult GetProductByModelBinder([FromQuery] ProductFilter filter)
        {
            var products = new List<Product> {
                new() { ID = 1, Title = "Laptop", Price = 100.32m},
                new() { ID = 2, Title = "Perfume", Price = 200m},
                new() { ID = 3, Title = "Mobile", Price = 5000m},
                new() { ID = 4, Title = "Game", Price = 85.00m}
            };

            var singleProduct = products.FirstOrDefault(p => p.Title == filter.ByTitle);

            if (singleProduct == null)
                return NotFound(new { Message = $"Product with Title: {filter.ByTitle} not found" });

            return Ok(singleProduct);
        }




        // Following API endpoint/method binds/receive params from http headers format.
        [Route("GetProductByHeder")]
        [HttpGet]
        public IActionResult GetProductByHeder([FromHeader] int? id, [FromHeader] string? title)
        {
            var products = new List<Product> {
                new() { ID = 1, Title = "Laptop", Price = 100.32m},
                new() { ID = 2, Title = "Perfume", Price = 200m},
                new() { ID = 3, Title = "Mobile", Price = 5000m},
                new() { ID = 4, Title = "Game", Price = 85.00m}
            };

            var singleProduct = products.FirstOrDefault(p => p.ID == id || p.Title == title);

            if (singleProduct == null)
                return NotFound(new { Message = $"Product with ID: {id} not found" });

            return Ok(singleProduct);
        }



        // Following API endpoint/method binds/receive different ways of params mix ways.
        [Route("GetProductByCombinedParams/{id}")]
        [HttpGet]
        public IActionResult GetProductByCombinedParams([FromRoute] int id, [FromQuery] string title, [FromHeader] string token)
        {
            var products = new List<Product> {
                new() { ID = 1, Title = "Laptop", Price = 100.32m, Token="1T55-OI-8858-A"},
                new() { ID = 2, Title = "Perfume", Price = 200m, Token="1F00-OI-8858-R"},
                new() { ID = 3, Title = "Mobile", Price = 5000m, Token="1C96-OI-8896-Z"},
                new() { ID = 4, Title = "Game", Price = 85.00m, Token="1X012-OI-0958-T"}
            };

            var singleProduct = products.FirstOrDefault(p => p.ID == id && p.Title == title && p.Token == token);

            if (singleProduct == null)
                return NotFound(new { Message = $"Product with ID: {id} not found" });

            return Ok(singleProduct);
        }




        // Following API endpoint/method binds/receive params from Request Body in Http GET.
        [Route("GetProductByRequestBody")]
        [HttpGet]
        public IActionResult GetProductByRequestBody([FromBody] Product p, [FromQuery] string title)
        {
            if (title == "Iron")
                return Ok(p);

            return NotFound(new { message = "Product Not Found!" });           
        }




        // Following API endpoint/method binds/receive params from Request Body in Http POST.
        [Route("SaveProduct")]
        [HttpPost]
        public IActionResult SaveProduct([FromBody] Product product)
        {
            return Created(string.Empty, product);
        }



        // Following API endpoint/method binds/receive params from FromForm in Http POST.

        [Route("SaveUser")]
        [HttpPost]
        public IActionResult SaveUser([FromForm] int id, [FromForm] string name, [FromForm] string address)
        {
            //return Created(string.Empty, new { ID = id, Name = name, Address = address });

            return Created(string.Empty, new User { ID = id, Name = name, Address = address });
        }

    }

    // Following User is called Model/Template/Blue Print/Idea/Complex type or User defined new type. That is class!!
    public class User
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
    }
}
