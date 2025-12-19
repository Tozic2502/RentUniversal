namespace RentUniversal.Application.DTOs;

/// <summary>
/// Data Transfer Object (DTO) for creating a rental.
/// This class is used to encapsulate the data required to create a new rental record.
/// </summary>
public class RentalCreateDTO
{
    /// <summary>
    /// Gets or sets the unique identifier of the user who is creating the rental.
    /// </summary>
    public string UserId { get; set; } = "";

    /// <summary>
    /// Gets or sets the unique identifier of the item being rented.
    /// </summary>
    public string ItemId { get; set; } = "";

    /// <summary>
    /// Gets or sets the start date of the rental period.
    /// This value is optional and can be null.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the rental period.
    /// This value is optional and can be null.
    /// </summary>
    public DateTime? EndDate { get; set; }
}
