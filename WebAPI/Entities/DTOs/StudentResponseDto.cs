namespace WebAPI.Entities.DTOs
{
    public class StudentResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string CurrentRegistrationNumber { get; set; } = string.Empty;
        public string CurrentCourse { get; set; } = string.Empty;
        public List<RegistrationHistoryDto> RegistrationHistory { get; set; } = new();
        public DateTime CreatedAt { get; set; }
    }
}
