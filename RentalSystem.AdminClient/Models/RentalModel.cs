namespace RentalSystem.AdminClient.Models;

public class RentalModel
{
        public string Id { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserId { get; set; } = "";
        public string ItemId { get; set; } = "";
    
}