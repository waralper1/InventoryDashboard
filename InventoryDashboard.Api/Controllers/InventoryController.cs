using AutoMapper;
using InventoryDashboard.Api.Dto;
using InventoryDashboard.Api.Interfaces;
using InventoryDashboard.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryDashboard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : Controller
    {
        private readonly IInventoryInterface _inventoryInterface;
        private readonly IMapper _mapper;
        public InventoryController(IInventoryInterface inventoryInterface, IMapper mapper)
        {
            _inventoryInterface = inventoryInterface;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Inventory>))]
        public IActionResult GetInventories()
        {
            var inventories = _mapper.Map<List<InventoryDto>>(_inventoryInterface.GetInventories());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(inventories);
        }
        [HttpGet("{id}")]
        //[Route("api/GetProduct/{id}")]
        [ProducesResponseType(200, Type = typeof(Inventory))]
        [ProducesResponseType(400)]
        public IActionResult GetInventory(int id)
        {
            if (!_inventoryInterface.InventoryExists(id))
            {
                return NotFound();
            }
            var inventory = _mapper.Map<InventoryDto>(_inventoryInterface.GetInventory(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(inventory);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult InventoryCategory([FromBody] InventoryDto inventoryCreate)
        {
            if (inventoryCreate == null)
            {
                return BadRequest(ModelState);
            }

            var inventory = _inventoryInterface.GetInventories()
                .Where(c => c.Name.Trim().ToUpper() == inventoryCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (inventory != null)
            {
                ModelState.AddModelError("", "Inventory already exists");
                return StatusCode(442, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var inventoryMap = _mapper.Map<Inventory>(inventoryCreate);

            if (!_inventoryInterface.CreateInventory(inventoryMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saveing pls check :( ");
                return BadRequest(ModelState);
            }

            return Ok("Creation Succ ^_^");
        }
    }
}
