namespace RentalSystem.AdminClient.Models
{
    /// <summary>
    /// Represents a rental entry retrieved from the API.
    /// Used in the admin panel to display rental information per user.
    /// </summary>
    public class RentalModel
    {
        /// <summary>
        /// Unique identifier of the rental.
        /// </summary>
        public string Id { get; set; } = "";

        /// <summary>
        /// Start date of the rental period.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End date of the rental period.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// ID of the user who created the rental.
        /// </summary>
        public string UserId { get; set; } = "";

        /// <summary>
        /// ID of the rented item.
        /// </summary>
        public string ItemId { get; set; } = "";
    }
}