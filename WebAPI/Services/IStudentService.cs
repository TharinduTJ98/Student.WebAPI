using WebAPI.Entities.DTOs;

namespace WebAPI.Services
{
    public interface IStudentService
    {
        Task<StudentResponseDto> CreateStudentAsync(CreateStudentDto createStudentDto);
        Task<List<StudentResponseDto>> GetAllStudentsAsync();
        Task<StudentResponseDto> UpdateStudentCourseAsync(UpdateStudentDto updateDto);

    }
}
