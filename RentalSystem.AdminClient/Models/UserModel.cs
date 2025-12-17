namespace RentalSystem.AdminClient.Models
{
    /// <summary>
    /// Represents a user displayed in the Admin Panel.
    /// </summary>
    /// <remarks>
    /// This model is a lightweight view model intended for administrative UI lists and detail views.
    /// Properties use simple CLR types for easy data binding in WPF. Date/time values are stored as
    /// formatted strings to allow the server to control display formatting; callers should treat
    /// them as presentation values (not for date arithmetic). Counts are expected to be non-negative.
    /// </remarks>
    public class UserModel
    {
        /// <summary>
        /// Unique identifier for the user (platform-specific, e.g., GUID).
        /// </summary>
        /// <remarks>
        /// Used for lookups and navigation. Should be non-empty when returned from the server.
        /// </remarks>
        public string Id { get; set; }  
        /// <summary>
        /// User's full name suitable for display in lists and detail panes.
        /// </summary>
        /// <remarks>
        /// May be an empty string if the user has not provided a name.
        /// </remarks>// id
        public string FullName { get; set; }          // name
        /// <summary>
        /// Primary email address for the user.
        /// </summary>
        /// <remarks>
        /// Used for contact and identification. May be null or empty for legacy accounts.
        /// </remarks>
        public string Email { get; set; }         // email
        public string Role { get; set; } // role
        /// <summary>
        /// The date the user registered, formatted for display (e.g., "2025-06-01", "Jun 1, 2025").
        /// </summary>
        /// <remarks>
        /// Stored as a presentation string so the server or client can control localization/formatting.
        /// Do not parse for business logic unless the format is guaranteed.
        /// </remarks>
        public DateTime? RegisteredDate { get; set; }
        /// <summary>
        /// The user's last login time formatted for display, or an empty/null string if never logged in.
        /// </summary>
        /// <remarks>
        /// As with <see cref="RegisteredDate"/>, this is a presentation string. For relative displays
        /// (e.g., "2 days ago"), the server may already provide the formatted value.
        /// </remarks>
        public DateTime? LastLogin { get; set; } 
        public int IdentificationId { get; set; }  // identificationId
        
        public int ActiveRentals { get; set; }
        public int TotalRentals { get; set; }

    }
}