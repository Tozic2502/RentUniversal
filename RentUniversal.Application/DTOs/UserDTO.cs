using RentUniversal.Domain.Enums;

namespace RentUniversal.Application.DTOs;

/// <summary>
/// Data Transfer Object (DTO) representing a user in the system.
/// </summary>
public class UserDTO
{
    /// <summary>
    /// Unique identifier for the user.
    /// This is a required field and is used to uniquely identify a user in the system.
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    /// User's full name or display name.
    /// This field is used for displaying the user's name in the system.
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// User's email address.
    /// This is used as a login identifier and for sending notifications to the user.
    /// </summary>
    public string Email { get; set; } = "";

    /// <summary>
    /// User role within the system.
    /// Indicates the user's role, such as Admin, Customer, or Owner.
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    /// Reference to the user's identification record.
    /// This field links the user to their identification details stored in the system.
    /// </summary>
    public int IdentificationId { get; set; }
    
    /// <summary>
    /// The date and time when the user registered in the system.
    /// This field is optional and may be null if the registration date is not recorded.
    /// </summary>
    public DateTime? RegisteredDate { get; set; }
    
    /// <summary>
    /// The date and time of the user's last login.
    /// This field is optional and may be null if the user has never logged in.
    /// </summary>
    public DateTime? LastLogin { get; set; }
}