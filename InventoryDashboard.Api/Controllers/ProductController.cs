using InventoryDashboard.Api.Interfaces;
using InventoryDashboard.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryDashboard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductInterface _productInterface;
        public ProductController(IProductInterface productInterface)
        {
            _productInterface = productInterface;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetProducts()
        {
            var products = _productInterface.GetProducts();
            if (ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(products);
        }
    }
}
