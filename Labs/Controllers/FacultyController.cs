using Lab1.Models;
using Lab1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly ICrudService<Faculty> _crudService;

        public FacultyController(ICrudService<Faculty> crudService)
        {
            _crudService = crudService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Faculty>>> GetFaculties()
        {
            var faculties = await _crudService.GetAllAsync();
            return Ok(faculties);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Faculty>> GetFaculty(int id)
        {
            var faculty = await _crudService.GetByIdAsync(id);
            if (faculty == null) return NotFound();
            return Ok(faculty);
        }

        [HttpPost]
        public async Task<ActionResult> CreateFaculty(Faculty faculty)
        {
            await _crudService.CreateAsync(faculty);
            return CreatedAtAction(nameof(GetFaculty), new { id = faculty.FacultyId }, faculty);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFaculty(int id, Faculty faculty)
        {
            if (id != faculty.FacultyId) return BadRequest();
            await _crudService.UpdateAsync(faculty);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFaculty(int id)
        {
            await _crudService.DeleteAsync(id);
            return NoContent();
        }
    }
}