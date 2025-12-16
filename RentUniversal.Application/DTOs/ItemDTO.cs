using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentUniversal.Application.DTOs;

public class ItemDTO
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Category { get; set; } = "";
    public string Condition { get; set; } = "";
    public double Value { get; set; }
    public bool IsAvailable { get; set; }
    public double Deposit { get; set; }
    public double PricePerDay { get; set; }
    public double TotalPrice { get; set; }
    // Images
    public List<string> ImageUrls { get; set; } = new();

}
