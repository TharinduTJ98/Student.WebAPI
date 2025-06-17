using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebAPI.Entities.Models
{
    public class RegistrationCounter
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("course")]
        public string Course { get; set; } = string.Empty;

        [BsonElement("year")]
        public int Year { get; set; }

        [BsonElement("counter")]
        public int Counter { get; set; } = 0;

        [BsonElement("lastUpdated")]
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
