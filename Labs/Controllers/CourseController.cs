using Lab1.Models;
using Lab1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICrudService<Course> _crudService;

        public CourseController(ICrudService<Course> crudService)
        {
            _crudService = crudService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            var courses = await _crudService.GetAllAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _crudService.GetByIdAsync(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCourse(Course course)
        {
            await _crudService.CreateAsync(course);
            return CreatedAtAction(nameof(GetCourse), new { id = course.CourseId }, course);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCourse(int id, Course course)
        {
            if (id != course.CourseId) return BadRequest();
            await _crudService.UpdateAsync(course);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            await _crudService.DeleteAsync(id);
            return NoContent();
        }
    }
}