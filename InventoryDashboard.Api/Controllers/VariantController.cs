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
        private readonly IOptionInterface _optionInterface;
        private readonly IProductInterface _productInterface;
        private readonly IMapper _mapper;
        public VariantController(IVariantInterface variantInterface, IMapper mapper
            , IOptionInterface optionInterface, IProductInterface productInterface)
        {
            _variantInterface = variantInterface;
            _optionInterface = optionInterface;
            _productInterface = productInterface;
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
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult VariantCategory([FromQuery]int productId, [FromQuery] int optionId,[FromBody] VariantDto variantCreate)
        {
            if (variantCreate == null)
            {
                return BadRequest(ModelState);
            }

            var variant = _variantInterface.GetVariants()
                .Where(c => c.Price == variantCreate.Price && c.ProductId == variantCreate.ProductId && c.OptionId == variantCreate.OptionId)
                .FirstOrDefault();

            if (variant != null)
            {
                ModelState.AddModelError("", "Variant already exists");
                return StatusCode(442, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var variantMap = _mapper.Map<Variant>(variantCreate);
            variantMap.Option = _optionInterface.GetOption(optionId);
            variantMap.Product = _productInterface.GetProduct(productId);

            if (!_variantInterface.CreateVariant(productId, optionId, variantMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saveing pls check :( ");
                return BadRequest(ModelState);
            }

            return Ok("Creation Succ ^_^");
        }
        [HttpPut("variantId")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateVariant(  
                                            int variantId,
                                            [FromBody] VariantDto updatedVariant)
        {
            if (updatedVariant == null)
            {
                return BadRequest(ModelState);
            }
            if (variantId != updatedVariant.VariantId)
            {
                return BadRequest(ModelState);
            }
            if (!_variantInterface.VariantExists(variantId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var variantMap = _mapper.Map<Variant>(updatedVariant);
            if (!_variantInterface.UpdateVariant( 
                                                variantId,
                                                variantMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
        [HttpDelete("{variantId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteVariant(int variantId)
        {
            if (!_variantInterface.VariantExists(variantId))
            {
                return NotFound();
            }
            var variantToDelete = _variantInterface.GetVariant(variantId);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!_variantInterface.DeleteVariant(variantToDelete))
            {
                ModelState.AddModelError("", "Something went wornk !");
            }

            return NoContent();
        }
    }
}
