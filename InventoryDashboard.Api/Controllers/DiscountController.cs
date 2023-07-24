using AutoMapper;
using InventoryDashboard.Api.Dto;
using InventoryDashboard.Api.Interfaces;
using InventoryDashboard.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryDashboard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : Controller
    {
        private readonly IDiscountInterface _discountInterface;
        private readonly IMapper _mapper;
        public DiscountController(IDiscountInterface discountInterface, IMapper mapper)
        {
            _discountInterface = discountInterface;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Discount>))]
        public IActionResult GetDiscounts()
        {
            var discounts = _mapper.Map<List<DiscountDto>>(_discountInterface.GetDiscounts());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(discounts);
        }
        [HttpGet("{id}")]
        //[Route("api/GetProduct/{id}")]
        [ProducesResponseType(200, Type = typeof(Discount))]
        [ProducesResponseType(400)]
        public IActionResult GetDiscount(int id)
        {
            if (!_discountInterface.DiscountExists(id))
            {
                return NotFound();
            }
            var discount = _mapper.Map<DiscountDto>(_discountInterface.GetDiscount(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(discount);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DiscountCategory([FromBody] DiscountDto discountCreate)
        {
            if (discountCreate == null)
            {
                return BadRequest(ModelState);
            }

            var discount = _discountInterface.GetDiscounts()
                .Where(c => c.Name.Trim().ToUpper() == discountCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (discount != null)
            {
                ModelState.AddModelError("", "Discount already exists");
                return StatusCode(442, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var discountMap = _mapper.Map<Discount>(discountCreate);

            if (!_discountInterface.CreateDiscount(discountMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saveing pls check :( ");
                return BadRequest(ModelState);
            }

            return Ok("Creation Succ ^_^");
        }
        [HttpPut("discountId")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDiscount(int discountId, [FromBody] DiscountDto updatedDiscount)
        {
            if (updatedDiscount == null)
            {
                return BadRequest(ModelState);
            }
            if (discountId != updatedDiscount.DiscountId)
            {
                return BadRequest(ModelState);
            }
            if (!_discountInterface.DiscountExists(discountId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var discountMap = _mapper.Map<Discount>(updatedDiscount);
            if (!_discountInterface.UpdateDiscount(discountMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
    }
}
