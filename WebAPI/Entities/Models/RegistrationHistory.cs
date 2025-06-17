using MongoDB.Bson.Serialization.Attributes;

namespace WebAPI.Entities.Models
{
    public class RegistrationHistory
    {
        [BsonElement("registrationNumber")]
        public string RegistrationNumber { get; set; } = string.Empty;

        [BsonElement("course")]
        public string Course { get; set; } = string.Empty;

        [BsonElement("year")]
        public int Year { get; set; }

        [BsonElement("registeredAt")]
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        [BsonElement("isActive")]
        public bool IsActive { get; set; } = true;

    }
}
