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
    public string Email { get; set; } = string.Empty;

    public UserRole Role { get; set; } = UserRole.Customer;

    public string IdentificationId { get; set; } = string.Empty; 

    public List<string> RentalIds { get; set; } = new();         
    public string PasswordHash { get; set; } = "";
    
    public DateTime? RegisteredDate { get; set; }
    public DateTime? LastLogin { get; set; }

}