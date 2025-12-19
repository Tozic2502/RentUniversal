using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RentUniversal.Domain.Entities
{
    public class Contact
    {
        // Unique identifier for the contact message, initialized with a new GUID.
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        // Name of the person who sent the contact message.
        public string Name { get; set; } = string.Empty;

        // Email address of the person who sent the contact message.
        public string Email { get; set; } = string.Empty;

        // The content of the contact message.
        public string Message { get; set; } = string.Empty;

        // The date and time when the contact message was created, set to the current UTC time by default.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}