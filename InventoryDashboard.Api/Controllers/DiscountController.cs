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
    }
}
