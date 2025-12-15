using RentalSystem.AdminClient.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Threading;

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
        private readonly ApiService _api = ApiService.Instance;

        private int _usersCount;
        public int UsersCount
        {
            get => _usersCount;
            set { _usersCount = value; OnPropertyChanged(); }
        }

        private int _activeRentals;
        public int ActiveRentals
        {
            get => _activeRentals;
            set { _activeRentals = value; OnPropertyChanged(); }
        }

        private decimal _revenueToday;
        public decimal RevenueToday
        {
            get => _revenueToday;
            set { _revenueToday = value; OnPropertyChanged(); }
        }

        private string _systemUptime = "";
        public string SystemUptime
        {
            get => _systemUptime;
            set { _systemUptime = value; OnPropertyChanged(); }
        }

        private readonly DateTime _startTime;
        private readonly DispatcherTimer _uptimeTimer;

        public StatsViewModel()
        {
            _startTime = DateTime.Now;

            _uptimeTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _uptimeTimer.Tick += (_, _) => UpdateUptime();
            _uptimeTimer.Start();

            _ = LoadStatsAsync();
        }

        private async Task LoadStatsAsync()
        {
            UsersCount = await _api.GetUsersCountAsync();

            ActiveRentals = 0;
            RevenueToday = 0m;
        }

        private void UpdateUptime()
        {
            var uptime = DateTime.Now - _startTime;
            SystemUptime =
                $"{uptime.Days}d {uptime.Hours}h {uptime.Minutes}m {uptime.Seconds}s";
        }
    }
}
