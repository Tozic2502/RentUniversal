namespace RentalSystem.AdminClient.ViewModel
{
    /// <summary>
    /// ViewModel providing system statistics for the Admin dashboard.
    /// </summary>
    /// <remarks>
    /// All values are currently hardcoded and should be replaced
    /// with real-time data fetched from an API.
    /// </remarks>

    public class StatsViewModel : BaseViewModel
    {
        /// <summary>
        /// Total number of registered users.
        /// </summary>
        public int UsersCount { get; set; }

        /// <summary>
        /// Number of currently active rentals.
        /// </summary>
        public int ActiveRentals { get; set; }

        /// <summary>
        /// Revenue generated today.
        /// </summary>
        public string RevenueToday { get; set; }

        /// <summary>
        /// System uptime percentage.
        /// </summary>
        public string SystemUptime { get; set; }

        public StatsViewModel()
        {
            // Dummy data for dashboard preview
            UsersCount = 1237;
            ActiveRentals = 34;
            RevenueToday = "122.465 DKK";
            SystemUptime = "99.98%";
        }
    }
}