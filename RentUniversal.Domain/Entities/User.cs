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
    /// Defaults to <see cref="UserRole.Customer"/>.
    /// </summary>
    public UserRole Role { get; set; } = UserRole.Customer;

    /// <summary>
    /// Gets or sets the identification ID of the user.
    /// This could represent a government-issued ID or another unique identifier.
    /// </summary>
    public int IdentificationId { get; set; }

    /// <summary>
    /// Gets or sets the list of rental IDs associated with the user.
    /// Each ID represents a rental transaction or record.
    /// </summary>
    public List<string> RentalIds { get; set; } = new();

    /// <summary>
    /// Gets or sets the hashed password of the user.
    /// This is used for authentication purposes.
    /// </summary>
    public string PasswordHash { get; set; } = "";
}