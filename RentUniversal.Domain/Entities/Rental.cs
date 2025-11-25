using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentUniversal.Domain.Entities;

public class Rental
{
    public string Id { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;
    public string ItemId { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public string StartCondition { get; set; } = string.Empty;
    public string? ReturnCondition { get; set; }

    public double Price { get; set; }
}
