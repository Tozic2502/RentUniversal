using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentUniversal.Application.DTOs;

public class IdentificationDTO
{
    public string Id { get; set; } = "";
    public string Type { get; set; } = "";
    public string DocumentUrl { get; set; } = "";
    public DateTime VerifiedDate { get; set; }
}