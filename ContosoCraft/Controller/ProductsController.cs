using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoCraft.Model;
using ContosoCraft.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContosoCraft.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductService _productService { get; }
        public ProductsController(ProductService productService )
        {
            _productService = productService;
        }
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return _productService.GetProducts();
        }
        [Route("Rate")]
        [HttpGet]
        public ActionResult Get([FromQuery] string productId,[FromQuery] int rating)
        {
            _productService.AddRating(productId,rating);
            return Ok();
        }
    }
}
