using Lab1.Models;
using Lab1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ICrudService<Student> _crudService;

        public StudentController(ICrudService<Student> crudService)
        {
            _crudService = crudService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var students = await _crudService.GetStudentsAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _crudService.GetByIdAsync(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult> CreateStudent(Student student)
        {
            await _crudService.CreateAsync(student);
            return CreatedAtAction(nameof(GetStudent), new { id = student.StudentId }, student);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudent(int id, Student student)
        {
            if (id != student.StudentId) return BadRequest();
            await _crudService.UpdateAsync(student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            await _crudService.DeleteAsync(id);
            return NoContent();
        }
    }
}