using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentUniversal.Domain.ValueObjects;

/// <summary>
/// Represents a monetary value with an amount.
/// This class is designed to handle monetary values and includes validation to ensure
/// that the amount cannot be negative. Future enhancements may include support for
/// multiple currencies.
/// </summary>
public class Money
{
    /// <summary>
    /// Gets the monetary amount.
    /// </summary>
    public double Amount { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Money"/> class with the specified amount.
    /// </summary>
    /// <param name="amount">The monetary amount. Must be non-negative.</param>
    /// <exception cref="ArgumentException">Thrown when the amount is negative.</exception>
    public Money(double amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative");

        Amount = amount;
    }

    /// <summary>
    /// Returns a string representation of the monetary value in the format "0.00 DKK".
    /// </summary>
    /// <returns>A string representation of the monetary value.</returns>
    public override string ToString() => $"{Amount:0.00} DKK";
}