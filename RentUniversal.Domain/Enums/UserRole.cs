using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentUniversal.Domain.Enums;

/// <summary>
/// Represents the roles that a user can have within the system.
/// </summary>
/// <remarks>
/// This enumeration defines the different roles available for users:
/// - <see cref="Customer"/>: Represents a standard user who interacts with the system as a customer.
/// - <see cref="Admin"/>: Represents an administrative user with elevated permissions to manage the system.
/// </remarks>
public enum UserRole
{
    Customer,
    Admin
}