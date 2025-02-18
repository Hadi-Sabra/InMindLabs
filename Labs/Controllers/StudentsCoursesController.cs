using Lab1.Models;
using Lab1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsCoursesController : ControllerBase
    {
        private readonly ICrudService<StudentsCourses> _crudService;

        public StudentsCoursesController(ICrudService<StudentsCourses> crudService)
        {
            _crudService = crudService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentsCourses>>> GetStudentsCourses()
        {
            var studentsCourses = await _crudService.GetAllAsync();
            return Ok(studentsCourses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentsCourses>> GetStudentCourse(int id)
        {
            var studentCourse = await _crudService.GetByIdAsync(id);
            if (studentCourse == null) return NotFound();
            return Ok(studentCourse);
        }

        [HttpPost]
        public async Task<ActionResult> CreateStudentCourse(StudentsCourses studentsCourses)
        {
            await _crudService.CreateAsync(studentsCourses);
            return CreatedAtAction(nameof(GetStudentCourse), new { id = studentsCourses.StudentsCoursesId }, studentsCourses);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudentCourse(int id, StudentsCourses studentsCourses)
        {
            if (id != studentsCourses.StudentsCoursesId) return BadRequest();
            await _crudService.UpdateAsync(studentsCourses);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudentCourse(int id)
        {
            await _crudService.DeleteAsync(id);
            return NoContent();
        }
    }
}