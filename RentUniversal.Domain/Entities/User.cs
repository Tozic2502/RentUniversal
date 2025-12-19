using RentUniversal.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RentUniversal.Domain.Entities;

/// <summary>
/// Represents a user entity in the RentUniversal domain.
/// </summary>
public class User
{
    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// This is stored as a MongoDB ObjectId and is automatically generated.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the role of the user.
    /// The default role is set to Customer.
    /// </summary>
    public UserRole Role { get; set; } = UserRole.Customer;

    /// <summary>
    /// Gets or sets the identification ID of the user.
    /// This can be used for additional identification purposes.
    /// </summary>
    public int IdentificationId { get; set; }

    /// <summary>
    /// Gets or sets the list of rental IDs associated with the user.
    /// This represents the rentals the user has made.
    /// </summary>
    public List<string> RentalIds { get; set; } = new();

    /// <summary>
    /// Gets or sets the hashed password of the user.
    /// This is used for authentication purposes.
    /// </summary>
    public string PasswordHash { get; set; } = "";

    /// <summary>
    /// Gets or sets the date and time when the user registered.
    /// This is nullable to account for cases where the registration date is unknown.
    /// </summary>
    public DateTime? RegisteredDate { get; set; }

    /// <summary>
    /// Gets or sets the date and time of the user's last login.
    /// This is nullable to account for cases where the user has not logged in yet.
    /// </summary>
    public DateTime? LastLogin { get; set; }
}