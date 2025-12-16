using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentUniversal.Application.DTOs;

/// <summary>
/// Data Transfer Object (DTO) representing an item that can be listed and rented.
/// Used to move item data between the API/UI and the application layer.
/// </summary>
public class ItemDTO
{
    /// <summary>
    /// Unique identifier for the item.
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    /// Display name/title of the item.
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Category used for grouping and filtering items (e.g., Tools, Electronics, etc.).
    /// </summary>
    public string Category { get; set; } = "";

    /// <summary>
    /// Condition of the item (e.g., New, LikeNew, Good, Used).
    /// </summary>
    public string Condition { get; set; } = "";

    /// <summary>
    /// Estimated value of the item.
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Indicates whether the item is currently available for rent.
    /// </summary>
    public bool IsAvailable { get; set; }
    public double Deposit { get; set; }
    public double PricePerDay { get; set; }
    public double TotalPrice { get; set; }
    // Images
    public List<string> ImageUrls { get; set; } = new();

}
