using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentUniversal.Domain.Entities;

/// <summary>
/// Represents an item available for rent in the system.
/// </summary>
public class Item
{
    /// <summary>
    /// Gets or sets the unique identifier for the item.
    /// This identifier is automatically generated as a MongoDB ObjectId.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    /// <summary>
    /// Gets or sets the name of the item.
    /// This is a descriptive name that identifies the item.
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Gets or sets the category to which the item belongs.
    /// Examples of categories include electronics, furniture, etc.
    /// </summary>
    public string Category { get; set; } = "";

    /// <summary>
    /// Gets or sets the condition of the item.
    /// Examples of conditions include new, used, or refurbished.
    /// </summary>
    public string Condition { get; set; } = "";

    /// <summary>
    /// Gets or sets the monetary value of the item.
    /// This represents the item's estimated worth in currency.
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the item is currently available for rent.
    /// A value of true means the item is available, while false means it is not.
    /// </summary>
    public bool IsAvailable { get; set; }

    /// <summary>
    /// Gets or sets the deposit amount required to rent the item.
    /// This is the security deposit that must be paid by the renter.
    /// </summary>
    public double Deposit { get; set; }

    /// <summary>
    /// Gets or sets the price per day for renting the item.
    /// This is the daily rental cost for the item.
    /// </summary>
    public double PricePerDay { get; set; }

    /// <summary>
    /// Gets or sets the total price for renting the item.
    /// This value may be calculated based on the rental duration and other factors.
    /// </summary>
    public double TotalPrice { get; set; }
    
    /// <summary>
    /// Gets or sets the list of image URLs associated with the item.
    /// These URLs point to images that visually represent the item.
    /// </summary>
    public List<string> ImageUrls { get; set; } = new();
}

