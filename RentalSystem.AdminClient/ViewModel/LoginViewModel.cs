using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using RentalSystem.AdminClient.ViewModel;

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

        /// <summary>
        /// Initializes the LoginViewModel with navigation support.
        /// </summary>
        public LoginViewModel(NavigationService nav)
        {
            _nav = nav;
            LoginCommand = new RelayCommand(_ => Login());
        }

        /// <summary>
        /// Initializes the LoginViewModel with navigation support.
        /// </summary>
        private string _username;
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        private string _password;

        /// <summary>
        /// Password entered by the administrator.
        /// </summary>
        /// <remarks>
        /// Stored as plain text only for demo purposes.
        /// NEVER do this in production systems.
        /// </remarks>
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        private string _loginMessage;

        /// <summary>
        /// Message displayed to the user after login attempts.
        /// </summary>
        public string LoginMessage
        {
            get => _loginMessage;
            set { _loginMessage = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Message displayed to the user after login attempts.
        /// </summary>
        public ICommand LoginCommand { get; }

        /// <summary>
        /// Executes the login logic.
        /// </summary>
        /// <remarks>
        /// This method validates input and navigates to the AdminShell
        /// on success. Authentication is not yet implemented.
        /// </remarks> 
        private void Login()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                LoginMessage = "Please enter username and password.";
                return;
            }
            // Navigate to admin dashboard
            _nav.Navigate(new AdminShellViewModel(_nav));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}