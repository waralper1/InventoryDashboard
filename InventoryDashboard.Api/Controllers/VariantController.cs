using AutoMapper;
using InventoryDashboard.Api.Dto;
using InventoryDashboard.Api.Interfaces;
using InventoryDashboard.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryDashboard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariantController:Controller
    {
        private readonly IVariantInterface _variantInterface;
        private readonly IMapper _mapper;
        public VariantController(IVariantInterface variantInterface, IMapper mapper)
        {
            _variantInterface = variantInterface;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Variant>))]
        public IActionResult GetVariants()
        {
            var variants = _mapper.Map<List<VariantDto>>(_variantInterface.GetVariants());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(variants);
        }
        [HttpGet("{id}")]
        //[Route("api/GetProduct/{id}")]
        [ProducesResponseType(200, Type = typeof(Variant))]
        [ProducesResponseType(400)]
        public IActionResult GetVariant(int id)
        {
            if (!_variantInterface.VariantExists(id))
            {
                return NotFound();
            }
            var variant = _mapper.Map<VariantDto>(_variantInterface.GetVariant(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(variant);
        }
    }
}
