using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentUniversal.Domain.Entities;

public class Identification
{
    public string Id { get; set; } = string.Empty;
    public string Type { get; set; } = "";                // e.g., "DriverLicense", "Passport", "MitID"
    public string DocumentUrl { get; set; } = "";         // URL to uploaded verification
    public DateTime VerifiedDate { get; set; }
}