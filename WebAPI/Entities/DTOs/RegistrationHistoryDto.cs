namespace WebAPI.Entities.DTOs
{
    public class RegistrationHistoryDto
    {
        public string RegistrationNumber { get; set; } 
        public string Course { get; set; }
        public int Year { get; set; }
        public DateTime RegisteredAt { get; set; }
        public bool IsActive { get; set; }
    }
}
