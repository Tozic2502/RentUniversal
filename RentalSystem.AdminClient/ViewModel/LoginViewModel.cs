using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using RentalSystem.AdminClient.Services;

namespace RentalSystem.AdminClient.ViewModel
{
    /// <summary>
    /// ViewModel responsible for handling administrator login.
    /// </summary>
    /// <remarks>
    /// Currently uses a dummy login mechanism. Authentication logic
    /// should later be replaced with a secure API-based login flow.
    /// </remarks>
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly NavigationService _nav;
        private readonly ApiService _api;
        /// <summary>
        /// Initializes the LoginViewModel with navigation support.
        /// </summary>

        public LoginViewModel(NavigationService nav)
        {
            _nav = nav;
            _api = ApiService.Instance;

            // WICHTIG: Passwort über CommandParameter reinholen
            LoginCommand = new RelayCommand(async _ => await LoginAsync());

        }

        // Input

        private string _email = "";
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        private string _password = "";
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        private string _loginMessage = "";
        public string LoginMessage
        {
            get => _loginMessage;
            set { _loginMessage = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }

        // Login logic

        private async Task LoginAsync()
        {
            LoginMessage = "";
            OnPropertyChanged(nameof(LoginMessage));

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                LoginMessage = "Bitte Email und Passwort eingeben.";
                OnPropertyChanged(nameof(LoginMessage));
                return;
            }
            
            var success = await _api.LoginAsync("", Email, Password);
            if (!success || _api.CurrentUser == null)
            {
                LoginMessage = "Login fehlgeschlagen. Bitte Daten prüfen.";
                OnPropertyChanged(nameof(LoginMessage));
                return;
            }

            var role = _api.CurrentUser.Role; // string

            if (!string.Equals(role, "Admin", System.StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(role, "Owner", System.StringComparison.OrdinalIgnoreCase))
            {
                LoginMessage = "Access Denied: Wrong User Role";
                _api.Logout();
                OnPropertyChanged(nameof(LoginMessage));
                return;
            }

            _nav.Navigate(new AdminShellViewModel(_nav));
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
