using System;

namespace RentUniversal.Application.DTOs;

/// <summary>
/// Data Transfer Object (DTO) representing an identification document tied to a user/entity.
/// </summary>
public class IdentificationDTO
{
    /// <summary>
    /// Unique identifier for the identification record.
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    /// The type of identification (Passport, DriverLicense, NationalId).
    /// </summary>
    public string Type { get; set; } = "";

    /// <summary>
    /// URL to the stored identification document (file location or reference).
    /// </summary>
    public string DocumentUrl { get; set; } = "";

    /// <summary>
    /// The date/time when the identification was verified/approved.
    /// If not yet verified, this may be left as default(DateTime) unless you use a nullable DateTime.
    /// </summary>
    public DateTime VerifiedDate { get; set; }
}