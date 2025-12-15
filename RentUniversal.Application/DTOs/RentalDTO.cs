using System;

namespace RentUniversal.Application.DTOs;

/// <summary>
/// Data Transfer Object (DTO) representing a rental agreement/transaction.
/// </summary>
public class RentalDTO
{
    /// <summary>
    /// Unique identifier for the rental record.
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    /// Identifier of the user who is renting the item.
    /// </summary>
    public string UserId { get; set; } = "";

    /// <summary>
    /// Identifier of the item being rented.
    /// </summary>
    public string ItemId { get; set; } = "";

    /// <summary>
    /// Date/time when the rental period starts.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Date/time when the rental ends.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// The recorded condition of the item at the start of the rental.
    /// Used as a baseline when the item is returned.
    /// </summary>
    public string StartCondition { get; set; } = "";

    /// <summary>
    /// The recorded condition of the item upon return.
    /// Nullable because it is typically set only when the rental is closed.
    /// </summary>
    public string? ReturnCondition { get; set; }

    /// <summary>
    /// Total price for the rental (or agreed price at creation time, depending on your model).
    /// </summary>
    public double Price { get; set; }
}