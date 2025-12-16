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
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    /// Gets or sets the name of the item.
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Gets or sets the category to which the item belongs (e.g., electronics, furniture).
    /// </summary>
    public string Category { get; set; } = "";

    /// <summary>
    /// Gets or sets the condition of the item (e.g., new, used, refurbished).
    /// </summary>
    public string Condition { get; set; } = "";

    /// <summary>
    /// Gets or sets the monetary value of the item.
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the item is currently available for rent.
    /// </summary>
    public bool IsAvailable { get; set; }

    /// <summary>
    /// Gets or sets the deposit amount required to rent the item.
    /// </summary>
    public double Deposit { get; set; }

    /// <summary>
    /// Gets or sets the price per day for renting the item.
    /// </summary>
    public double PricePerDay { get; set; }

    /// <summary>
    /// Gets or sets the total price for renting the item, which may be calculated based on the rental duration.
    /// </summary>
    public double TotalPrice { get; set; }
    
    public List<string> ImageUrls { get; set; } = new();
}

