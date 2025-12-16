using RentUniversal.Domain.Enums;

namespace RentUniversal.Application.DTOs;


/// <summary>
/// Data Transfer Object (DTO) representing a user in the system.
/// </summary>
public class UserDTO
{
    /// <summary>
    /// Unique identifier for the user.
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    /// User's full name or display name.
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// User's email address (used as a login identifier and for notifications).
    /// </summary>
    public string Email { get; set; } = "";

    /// <summary>
    /// User role within the system (e.g., Admin, Customer, Owner).
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    /// Reference to the user's identification record.
    /// </summary>
    public int IdentificationId { get; set; }
    
    public DateTime? RegisteredDate { get; set; }
    public DateTime? LastLogin { get; set; }
}