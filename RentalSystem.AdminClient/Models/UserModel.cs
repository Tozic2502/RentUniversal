namespace RentalSystem.AdminClient.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string RegisteredDate { get; set; }
        public int ActiveRentals { get; set; }
        public int TotalRentals { get; set; }
        public bool IsBanned { get; set; }
        public string LastLogin { get; set; }
    }
}