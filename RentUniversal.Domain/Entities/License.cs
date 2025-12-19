using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentUniversal.Domain.Entities;

/// <summary>
/// Represents a license entity with details about its owner and validity period.
/// </summary>
public class License
{
    /// <summary>
    /// Gets or sets the unique identifier for the license.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the license owner.
    /// </summary>
    public string OwnerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the start date of the license's validity period.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the expiry date of the license's validity period.
    /// </summary>
    public DateTime ExpiryDate { get; set; }
}