using System.Windows.Input;

namespace RentalSystem.AdminClient.ViewModel
{
    public class AdminShellViewModel : BaseViewModel
    {
        private object _currentAdminViewModel;
        public object CurrentAdminViewModel
        {
            get => _currentAdminViewModel;
            set { _currentAdminViewModel = value; OnPropertyChanged(); }
        }

        public ICommand NavigateSettings { get; }
        public ICommand NavigateUser { get; }
        public ICommand NavigateAnnoncer { get; }
        public ICommand NavigateStats { get; }
        public ICommand Logout { get; }

        private readonly NavigationService _nav;

        public AdminShellViewModel(NavigationService nav)
        {
            _nav = nav;
            
            CurrentAdminViewModel = new SettingsViewModel();
            
            NavigateSettings = new RelayCommand(_ => CurrentAdminViewModel = new SettingsViewModel());
            NavigateUser = new RelayCommand(_ => CurrentAdminViewModel = new UserViewModel());
            NavigateAnnoncer = new RelayCommand(_ => CurrentAdminViewModel = new AnnoncerViewModel());
            NavigateStats = new RelayCommand(_ => CurrentAdminViewModel = new StatsViewModel());

            Logout = new RelayCommand(_ => _nav.Navigate(new LoginViewModel(_nav)));
        }
    }
}