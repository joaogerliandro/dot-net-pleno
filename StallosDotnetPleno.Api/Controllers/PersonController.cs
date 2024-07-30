using Microsoft.AspNetCore.Mvc;
using StallosDotnetPleno.Domain.Entities;
using StallosDotnetPleno.Application.Interfaces;

namespace StallosDotnetPleno.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonController> _logger;

        public PersonController(IPersonService personService, ILogger<PersonController> logger)
        {
            _personService = personService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(long id)
        {
            var result = await _personService.GetByIdAsync(id);

            if (!result.Success)
            {
                _logger.LogWarning(result.Message);

                return BadRequest(new { 
                    Success = result.Success,
                    Message = result.Message
                });
            }

            return Ok(new
            {
                Success = result.Success,
                Message = result.Message,
                Content = result.Content
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPersons()
        {
            var result = await _personService.GetAllAsync();

            if (!result.Success)
            {
                _logger.LogWarning(result.Message);

                return BadRequest(new
                {
                    Success = result.Success,
                    Message = result.Message
                });
            }

            return Ok(new
            {
                Success = result.Success,
                Message = result.Message,
                Content = result.Content
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] Person person)
        {
            var result = await _personService.AddAsync(person);

            if (!result.Success)
            {
                _logger.LogWarning(result.Message);

                return BadRequest(new
                {
                    Success = result.Success,
                    Message = result.Message,
                    Notifications = result.Notifications
                });
            }

            return Ok(new
            {
                Success = result.Success,
                Message = result.Message,
                Content = result.Content
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(long id, [FromBody] Person person)
        {
            var result = await _personService.UpdateAsync(person);

            if (!result.Success)
            {
                _logger.LogWarning(result.Message);

                return BadRequest(new
                {
                    Success = result.Success,
                    Message = result.Message,
                    Notifications = result.Notifications
                });
            }

            return Ok(new
            {
                Success = result.Success,
                Message = result.Message
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(long id)
        {
            var result = await _personService.DeleteAsync(id);

            if (!result.Success)
            {
                _logger.LogWarning(result.Message);

                return BadRequest(new
                {
                    Success = result.Success,
                    Message = result.Message,
                });
            }

            return Ok(new
            {
                Success = result.Success,
                Message = result.Message
            });
        }
    }
}
