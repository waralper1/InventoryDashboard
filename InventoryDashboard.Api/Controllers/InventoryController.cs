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
    }
}
