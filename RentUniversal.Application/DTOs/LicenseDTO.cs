using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentUniversal.Application.DTOs;

public class LicenseDTO
{
    public string Id { get; set; } = "";
    public string OwnerName { get; set; } = "";
    public DateTime StartDate { get; set; }
    public DateTime ExpiryDate { get; set; }
}

