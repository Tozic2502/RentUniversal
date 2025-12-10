using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentUniversal.Domain.Entities;

public class License
{
    public string Id { get; set; } = string.Empty;

    public string OwnerName { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }
    public DateTime ExpiryDate { get; set; }
}