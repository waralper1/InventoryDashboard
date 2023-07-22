using AutoMapper;
using InventoryDashboard.Api.Dto;
using InventoryDashboard.Api.Interfaces;
using InventoryDashboard.Api.Models;
using InventoryDashboard.Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InventoryDashboard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryInterface _categoryInterface;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryInterface categoryInterface, IMapper mapper)
        {
            _categoryInterface = categoryInterface;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryInterface.GetCategories());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categories);
        }
        [HttpGet("{id}/Product ")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetProductsByCategory(int id)
        {
            var products = _mapper.Map<List<ProductDto>>(_categoryInterface.GetProductsByCategory(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(products);
        }
        [HttpGet("{id}")]
        //[Route("api/GetProduct/{id}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int id)
        {
            if (!_categoryInterface.CategoryExists(id))
            {
                return NotFound();
            }
            var category = _mapper.Map<CategoryDto>(_categoryInterface.GetCategory(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(category);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if (categoryCreate == null)
            {
                return BadRequest(ModelState);
            }

            var category = _categoryInterface.GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(442, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryMap = _mapper.Map<Category>(categoryCreate);

            if(!_categoryInterface.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saveing pls check :( ");
                return BadRequest(ModelState);
            }

            return Ok("Creation Succ ^_^");
        }
    }
}
