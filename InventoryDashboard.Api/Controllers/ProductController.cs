using AutoMapper;
using InventoryDashboard.Api.Dto;
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
        private readonly IMapper _mapper;
        public ProductController(IProductInterface productInterface,IMapper mapper)
        {
            _productInterface = productInterface;
            _mapper = mapper;
        }
        [HttpGet]
        //[Route("api/GetProducts")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetProducts()
        {
            var products = _mapper.Map<List<ProductDto>>(_productInterface.GetProducts());
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState); 
            }
            return Ok(products);
        }
        [HttpGet("{id}")]
        //[Route("api/GetProduct/{id}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400)]
        public IActionResult GetProduct(int id)
        {
            if (!_productInterface.ProductExists(id))
            {
                return NotFound();
            }
            var product = _mapper.Map<ProductDto>(_productInterface.GetProduct(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(product);
        }
        ////[HttpGet]
        ////[Route("api/GetProduct/{name}")]
        ////[ProducesResponseType(200, Type = typeof(Product))]
        //[HttpGet("{name}")]
        ////[Route("api/GetProduct/{id}")]
        //[ProducesResponseType(200, Type = typeof(Product))]
        //[ProducesResponseType(400)]
        //public IActionResult GetProduct(string name)
        //{
        //    var product = _productInterface.GetProduct(name);
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    return Ok(product);
        //}
        [HttpGet]
        [Route("api/GetProductDiscount")]
        [ProducesResponseType(200, Type = typeof(Product))]
        public IActionResult GetProductDiscount(int id)
        {
            if (!_productInterface.ProductExists(id))
            {
                return NotFound();
            }
            var product = _productInterface.GetProductDiscount(id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(product);
        }
    }
}
