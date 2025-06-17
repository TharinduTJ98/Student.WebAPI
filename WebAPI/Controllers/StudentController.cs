using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities.DTOs;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudent()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpPost]
        public async Task<ActionResult<StudentResponseDto>> CreateStudent([FromBody] CreateStudentDto createStudentDto)
        {
            try
            {
                var student = await _studentService.CreateStudentAsync(createStudentDto);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult<StudentResponseDto>> UpdateStudentCourse([FromBody] UpdateStudentDto updateDto)
        {
            try
            {
                var updatedStudent = await _studentService.UpdateStudentCourseAsync(updateDto);
                return Ok(updatedStudent);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
