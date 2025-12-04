using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RentUniversal.Domain.Entities;

public class Rental
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    public string UserId { get; set; } = string.Empty;
    public string ItemId { get; set; } = string.Empty;

    public DateTime RentalDate { get; set; } = DateTime.UtcNow;
    public DateTime? ReturnDate { get; set; } = null;

    public string StartCondition { get; set; } = string.Empty;
    public string? ReturnCondition { get; set; }

    public double Price { get; set; }
}
