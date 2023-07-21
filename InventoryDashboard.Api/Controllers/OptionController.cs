using AutoMapper;
using InventoryDashboard.Api.Dto;
using InventoryDashboard.Api.Interfaces;
using InventoryDashboard.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryDashboard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController:Controller
    {
        private readonly IOptionInterface _optionInterface;
        private readonly IMapper _mapper;
        public OptionController(IOptionInterface optionInterface, IMapper mapper)
        {
            _optionInterface = optionInterface;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Option>))]
        public IActionResult GetOptions()
        {
            var options = _mapper.Map<List<OptionDto>>(_optionInterface.GetOptions());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(options);
        }
        [HttpGet("{id}")]
        //[Route("api/GetProduct/{id}")]
        [ProducesResponseType(200, Type = typeof(Option))]
        [ProducesResponseType(400)]
        public IActionResult GetOption(int id)
        {
            if (!_optionInterface.OptionExists(id))
            {
                return NotFound();
            }
            var option = _mapper.Map<OptionDto>(_optionInterface.GetOption(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(option);
        }
    }
}
