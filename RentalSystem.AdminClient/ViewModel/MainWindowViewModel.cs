using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RentalSystem.AdminClient.ViewModel
{
    /// <summary>
    /// Root ViewModel for the application window.
    /// </summary>
    /// <remarks>
    /// Holds the currently active ViewModel and wires up navigation.
    /// This is the entry point for the Admin Client UI.
    /// </remarks>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private object _currentViewModel;
        /// <summary>
        /// The ViewModel currently displayed in the main window.
        /// </summary>
        public object CurrentViewModel
        {
            get => _currentViewModel;
            set { _currentViewModel = value; OnPropertyChanged(); }
        }
        /// <summary>
        /// Global navigation service instance.
        /// </summary>
        public NavigationService Navigation { get; }

        public MainWindowViewModel()
        {
            Navigation = new NavigationService();
            // Connect navigation logic to this ViewModel
            Navigation.Configure(vm => CurrentViewModel = vm);

            // Initial screen is Login
            CurrentViewModel = new LoginViewModel(Navigation);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}