using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RentUniversal.Domain.Entities;

/// <summary>
/// Represents a rental transaction in the system.
/// </summary>
public class Rental
{
    /// <summary>
    /// The unique identifier for the rental, stored as a MongoDB ObjectId.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    /// <summary>
    /// The unique identifier of the user who initiated the rental.
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// The unique identifier of the item being rented.
    /// </summary>
    public string ItemId { get; set; } = string.Empty;

    /// <summary>
    /// The date and time when the rental started. Defaults to the current UTC time.
    /// </summary>
    public DateTime RentalDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// The date and time when the item was returned. Nullable to indicate that the item may not yet be returned.
    /// </summary>
    public DateTime? ReturnDate { get; set; }

    /// <summary>
    /// The condition of the item at the start of the rental.
    /// </summary>
    public string StartCondition { get; set; } = string.Empty;

    /// <summary>
    /// The condition of the item upon return. Nullable to indicate that the item may not yet be returned.
    /// </summary>
    public string? ReturnCondition { get; set; }

    /// <summary>
    /// The base price of the rental.
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// The price charged per day for the rental.
    /// </summary>
    public double PricePerDay { get; set; }

    /// <summary>
    /// The total price for the rental, calculated based on the duration and price per day.
    /// </summary>
    public double TotalPrice { get; set; }
}
