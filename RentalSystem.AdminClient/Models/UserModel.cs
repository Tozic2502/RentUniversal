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
        /// </remarks>
        public string FullName { get; set; }

        /// <summary>
        /// Primary email address for the user.
        /// </summary>
        /// <remarks>
        /// Used for contact and identification. May be null or empty for legacy accounts.
        /// </remarks>
        public string Email { get; set; }

        /// <summary>
        /// The date the user registered, formatted for display (e.g., "2025-06-01", "Jun 1, 2025").
        /// </summary>
        /// <remarks>
        /// Stored as a presentation string so the server or client can control localization/formatting.
        /// Do not parse for business logic unless the format is guaranteed.
        /// </remarks>
        public string RegisteredDate { get; set; }

        /// <summary>
        /// Number of rentals the user currently has active.
        /// </summary>
        /// <remarks>
        /// Expected to be zero or positive.
        /// </remarks>
        public int ActiveRentals { get; set; }

        /// <summary>
        /// Total number of rentals the user has completed or performed.
        /// </summary>
        /// <remarks>
        /// Expected to be zero or positive; can be used for reporting or sorting users by activity.
        /// </remarks>
        public int TotalRentals { get; set; }

        /// <summary>
        /// Indicates whether the user is banned from the service.
        /// </summary>
        /// <remarks>
        /// True when the account is restricted; the admin UI may present different actions
        /// and visual cues when this flag is set.
        /// </remarks>
        public bool IsBanned { get; set; }

        /// <summary>
        /// The user's last login time formatted for display, or an empty/null string if never logged in.
        /// </summary>
        /// <remarks>
        /// As with <see cref="RegisteredDate"/>, this is a presentation string. For relative displays
        /// (e.g., "2 days ago"), the server may already provide the formatted value.
        /// </remarks>
        public string LastLogin { get; set; }
    }
}