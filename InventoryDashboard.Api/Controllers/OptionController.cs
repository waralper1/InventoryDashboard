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
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult OptionCategory([FromBody] OptionDto optionCreate)
        {
            if (optionCreate == null)
            {
                return BadRequest(ModelState);
            }

            var option = _optionInterface.GetOptions()
                .Where(c => c.Name.Trim().ToUpper() == optionCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (option != null)
            {
                ModelState.AddModelError("", "Option already exists");
                return StatusCode(442, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var optionMap = _mapper.Map<Option>(optionCreate);

            if (!_optionInterface.CreateOption(optionMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saveing pls check :( ");
                return BadRequest(ModelState);
            }

            return Ok("Creation Succ ^_^");
        }
    }
}
