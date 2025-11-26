using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RentalSystem.AdminClient.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private object _currentViewModel;
        public object CurrentViewModel
        {
            get => _currentViewModel;
            set { _currentViewModel = value; OnPropertyChanged(); }
        }

        public NavigationService Navigation { get; }

        public MainWindowViewModel()
        {
            Navigation = new NavigationService();
            Navigation.Configure(vm => CurrentViewModel = vm);

            // Start mit LoginViewModel
            CurrentViewModel = new LoginViewModel(Navigation);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}