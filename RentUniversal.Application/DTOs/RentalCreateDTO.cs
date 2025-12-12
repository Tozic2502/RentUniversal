
namespace RentUniversal.Application.DTOs;

public class RentalCreateDTO
{
	public string UserId { get; set; } = "";
	public string ItemId { get; set; } = "";
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
