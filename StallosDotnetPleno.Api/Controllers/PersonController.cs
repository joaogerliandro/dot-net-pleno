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
        public async Task<ActionResult<Person>> GetPersonById(long id)
        {
            var person = await _personService.GetByIdAsync(id);

            if (person == null)
            {
                _logger.LogWarning($"Person with id {id} not found.");
                return NotFound();
            }

            return Ok(person);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetAllPersons()
        {
            var persons = await _personService.GetAllAsync();

            return Ok(persons);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePerson([FromBody] Person person)
        {
            if (person == null)
            {
                _logger.LogWarning("Person object is null.");

                return BadRequest();
            }

            await _personService.AddAsync(person);

            return CreatedAtAction(nameof(GetPersonById), new { id = person.Id }, person);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePerson(long id, [FromBody] Person person)
        {
            if (id != person.Id)
            {
                _logger.LogWarning("Person ID mismatch.");

                return BadRequest();
            }

            var existingPerson = await _personService.GetByIdAsync(id);

            if (existingPerson == null)
            {
                _logger.LogWarning($"Person with id {id} not found.");

                return NotFound();
            }

            await _personService.UpdateAsync(person);

            return NoContent(); // TODO: Change this for an update message
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePerson(long id)
        {
            var person = await _personService.GetByIdAsync(id);

            if (person == null)
            {
                _logger.LogWarning($"Person with id {id} not found.");

                return NotFound();
            }

            await _personService.DeleteAsync(id);

            return NoContent(); // TODO: Change this for a delete message
        }
    }
}
