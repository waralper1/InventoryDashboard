using AutoMapper;
using InventoryDashboard.Api.Dto;
using InventoryDashboard.Api.Interfaces;
using InventoryDashboard.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace InventoryDashboard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductInterface _productInterface;
        private readonly IInventoryInterface _inventoryInterface;
        private readonly IDiscountInterface _discountInterface;
        private readonly ICategoryInterface _categoryInterface;
        private readonly IMapper _mapper;
        public ProductController(IProductInterface productInterface, IInventoryInterface inventoryInterface, IDiscountInterface discountInterface, IMapper mapper, ICategoryInterface categoryInterface)
        {
            _productInterface = productInterface;
            _inventoryInterface = inventoryInterface;
            _discountInterface = discountInterface;

            _mapper = mapper;
            _categoryInterface = categoryInterface;
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
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProduct([FromQuery] int discountId, [FromQuery] int inventoryId, [FromQuery] int categoryId, [FromQuery] int variantId,[FromBody] ProductDto productCreate)
        {
            if (productCreate == null)
            {
                return BadRequest(ModelState);
            }

            var product = _productInterface.GetProducts()
                .Where(c => c.Name.Trim().ToUpper() == productCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (product != null)
            {
                ModelState.AddModelError("", "Product already exists");
                return StatusCode(442, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productMap = _mapper.Map<Product>(productCreate);
            productMap.Discount = _discountInterface.GetDiscount(discountId);
            productMap.Inventory = _inventoryInterface.GetInventory(inventoryId);
            productMap.Category = _categoryInterface.GetCategory(categoryId);


            if (!_productInterface.CreateProduct(discountId,inventoryId, categoryId, variantId,productMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saveing pls check :( ");
                return BadRequest(ModelState);
            }

            return Ok("Creation Succ ^_^");
        }
        [HttpPut("productId")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProduct(  
                                            int productId,
                                            [FromBody] ProductDto updatedProduct)
        {
            if (updatedProduct == null)
            {
                return BadRequest(ModelState);
            }
            if (productId != updatedProduct.ProductId)
            {
                return BadRequest(ModelState);
            }
            if (!_productInterface.ProductExists(productId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var productMap = _mapper.Map<Product>(updatedProduct);
            if (!_productInterface.UpdateProduct( 
                                                productId,
                                                productMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
        [HttpDelete("{productId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProduct(int productId)
        {
            if (!_productInterface.ProductExists(productId))
            {
                return NotFound();
            }
            var productToDelete = _productInterface.GetProduct(productId);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!_productInterface.DeleteProduct(productToDelete))
            {
                ModelState.AddModelError("", "Something went wornk !");
            }

            return NoContent();
        }
    }
}
