using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RentUniversal.Application.DTOs;

public class CreateUserRequestDTO
{
    [Required]
    [MinLength(2)]
    public string Name { get; set; } = "";

    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = "";
}

