using System;

namespace RentUniversal.Application.DTOs;

/// <summary>
/// Data Transfer Object (DTO) representing a license/subscription in the system.
/// Used to transfer license details between layers (e.g., API, application, persistence).
/// </summary>
public class LicenseDTO
{
    /// <summary>
    /// Unique identifier for the license.
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    /// Name of the license owner (e.g., user or organization holding the license).
    /// </summary>
    public string OwnerName { get; set; } = "";

    /// <summary>
    /// The date/time when the license becomes active.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// The date/time when the license expires.
    /// </summary>
    public DateTime ExpiryDate { get; set; }
}