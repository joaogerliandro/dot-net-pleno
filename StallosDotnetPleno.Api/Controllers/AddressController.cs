using Microsoft.AspNetCore.Mvc;
using StallosDotnetPleno.Domain.Entities;
using StallosDotnetPleno.Application.Interfaces;

namespace StallosDotnetPleno.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly ILogger<AddressController> _logger;

        public AddressController(IAddressService addressService, ILogger<AddressController> logger)
        {
            _addressService = addressService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddressById(long id)
        {
            var address = await _addressService.GetByIdAsync(id);

            if (address == null)
            {
                _logger.LogWarning($"Address with id {id} not found.");

                return NotFound();
            }

            return Ok(address);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAllAddresses()
        {
            var addresses = await _addressService.GetAllAsync();

            return Ok(addresses);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAddress([FromBody] Address address)
        {
            if (address == null)
            {
                _logger.LogWarning("Address object is null.");

                return BadRequest();
            }

            await _addressService.AddAsync(address);

            return CreatedAtAction(nameof(GetAddressById), new { id = address.Id }, address);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAddress(long id, [FromBody] Address address)
        {
            if (id != address.Id)
            {
                _logger.LogWarning("Address ID mismatch.");

                return BadRequest();
            }

            var existingAddress = await _addressService.GetByIdAsync(id);

            if (existingAddress == null)
            {
                _logger.LogWarning($"Address with id {id} not found.");

                return NotFound();
            }

            await _addressService.UpdateAsync(address);

            return NoContent(); // TODO: Change this for an update message
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAddress(long id)
        {
            var address = await _addressService.GetByIdAsync(id);

            if (address == null)
            {
                _logger.LogWarning($"Address with id {id} not found.");

                return NotFound();
            }

            await _addressService.DeleteAsync(id);

            return NoContent(); // TODO: Change this for a delete message
        }
    }
}
