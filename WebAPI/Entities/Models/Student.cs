using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebAPI.Entities.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("firstName")]
        public string FirstName { get; set; } = string.Empty;

        [BsonElement("lastName")]
        public string LastName { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("phone")]
        public string Phone { get; set; } = string.Empty;

        [BsonElement("currentRegistrationNumber")]
        public string CurrentRegistrationNumber { get; set; } = string.Empty;

        [BsonElement("currentCourse")]
        public string CurrentCourse { get; set; } = string.Empty;

        [BsonElement("registrationHistory")]
        public List<RegistrationHistory> RegistrationHistory { get; set; } = new();

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
