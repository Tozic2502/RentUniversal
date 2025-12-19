using System;

namespace RentUniversal.Application.Exceptions;

/// <summary>
/// Exception thrown when a requested resource cannot be found.
/// Used in the application layer to signal a 404-style situation,
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// Creates a "NotFoundException" with a descriptive message.
    /// </summary>
    public NotFoundException(string message) : base(message)
    {
    }
}