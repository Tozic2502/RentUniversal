using RentUniversal.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentUniversal.Domain.Entities;

public class User
{
    public string Id { get; set; } = string.Empty;   // Mongo ID
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public UserRole Role { get; set; } = UserRole.Customer;

    public string IdentificationId { get; set; } = string.Empty; // FK -> Identification

    public List<string> RentalIds { get; set; } = new();         // FK -> Rental
}

