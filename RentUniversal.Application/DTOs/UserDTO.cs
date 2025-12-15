using RentUniversal.Domain.Enums;

namespace RentUniversal.Application.DTOs;

public class UserDTO
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public UserRole Role {  get; set; }
    public int IdentificationId { get; set; }
    public DateTime? RegisteredDate { get; set; }
    public DateTime? LastLogin { get; set; }
}