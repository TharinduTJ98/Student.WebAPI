namespace WebAPI.Entities.DTOs
{
    public class UpdateStudentDto
    {
        public string StudentId { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Course { get; set; }
    }
}
