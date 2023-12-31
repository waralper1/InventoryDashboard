﻿using AutoMapper;
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
        [HttpPut("categoryId")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int categoryId, [FromBody]CategoryDto updatedCategory)
        {
            if (updatedCategory == null)
            {
                return BadRequest(ModelState);
            }
            if(categoryId != updatedCategory.CategoryId)
            {
                return BadRequest(ModelState);
            }
            if(!_categoryInterface.CategoryExists(categoryId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var categoryMap = _mapper.Map<Category>(updatedCategory);
            if (!_categoryInterface.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!_categoryInterface.CategoryExists(categoryId))
            {
                return NotFound();
            }
            var categoryToDelete = _categoryInterface.GetCategory(categoryId);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(!_categoryInterface.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wornk !");
            }

            return NoContent();
        }
    }
}
