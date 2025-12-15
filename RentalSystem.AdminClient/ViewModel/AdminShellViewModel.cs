using System.Windows.Input;

namespace RentalSystem.AdminClient.ViewModel
{
    /// <summary>
    /// ViewModel for the main Admin dashboard shell.
    /// </summary>
    /// <remarks>
    /// Hosts navigation commands for all admin sections and manages
    /// which section ViewModel is currently active.
    /// </remarks>
    public class AdminShellViewModel : BaseViewModel
    {
        private object _currentAdminViewModel;
        /// <summary>
        /// Currently active admin section ViewModel.
        /// </summary>
        public object CurrentAdminViewModel
        {
            get => _currentAdminViewModel;
            set { _currentAdminViewModel = value; OnPropertyChanged(); }
        }

        public ICommand NavigateSettings { get; }
        public ICommand NavigateUser { get; }
        public ICommand NavigateAnnoncer { get; }
        public ICommand NavigateStats { get; }
        public ICommand NavigateUpload { get; }
        public ICommand Logout { get; }

        private readonly NavigationService _nav;

        public AdminShellViewModel(NavigationService nav)
        {
            _nav = nav;

            // Default admin landing page
            CurrentAdminViewModel = new SettingsViewModel();
            
            NavigateSettings = new RelayCommand(_ => CurrentAdminViewModel = new SettingsViewModel());
            NavigateUser = new RelayCommand(_ => CurrentAdminViewModel = new UserViewModel());
            NavigateAnnoncer = new RelayCommand(_ => CurrentAdminViewModel = new AnnoncerViewModel());
            NavigateStats = new RelayCommand(_ => CurrentAdminViewModel = new StatsViewModel());

            NavigateUpload = new RelayCommand(_ => CurrentAdminViewModel = new UploadViewModel());

            // Return to login
            Logout = new RelayCommand(_ => _nav.Navigate(new LoginViewModel(_nav)));
        }
    }
}