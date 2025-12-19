using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentUniversal.Domain.Entities;

/// <summary>
/// Represents an identification document used for verification purposes.
/// </summary>
public class Identification
{
    /// <summary>
    /// Gets or sets the unique identifier for the identification document.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of identification document.
    /// Examples include "DriverLicense", "Passport", or "MitID".
    /// </summary>
    public string Type { get; set; } = "";                

    /// <summary>
    /// Gets or sets the URL pointing to the uploaded document for verification.
    /// </summary>
    public string DocumentUrl { get; set; } = "";         

    /// <summary>
    /// Gets or sets the date when the identification document was verified.
    /// </summary>
    public DateTime VerifiedDate { get; set; }
}