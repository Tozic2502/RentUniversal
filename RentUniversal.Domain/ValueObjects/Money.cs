using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentUniversal.Domain.ValueObjects;

public class Money
{
    public double Amount { get; private set; }

    public Money(double amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative");

        Amount = amount;
    }

    public override string ToString() => $"{Amount:0.00} DKK";
}

