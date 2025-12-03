namespace RentalSystem.AdminClient.ViewModel
{
    public class StatsViewModel : BaseViewModel
    {
        public int UsersCount { get; set; }
        public int ActiveRentals { get; set; }
        public string RevenueToday { get; set; }
        public string SystemUptime { get; set; }

        public StatsViewModel()
        {
            // DUMMY-DATA Has to be replaced by real API data
            UsersCount = 1237;
            ActiveRentals = 34;
            RevenueToday = "122.465 DKK";
            SystemUptime = "99.98%";
        }
    }
}