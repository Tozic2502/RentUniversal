using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using RentalSystem.AdminClient.ViewModel;

namespace RentalSystem.AdminClient
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly NavigationService _nav;

        public LoginViewModel(NavigationService nav)
        {
            _nav = nav;
            LoginCommand = new RelayCommand(_ => Login());
        }

        private string _username;
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        private string _loginMessage;
        public string LoginMessage
        {
            get => _loginMessage;
            set { _loginMessage = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }

        // Basic dummy login fordi vi ikke har serveren set up 
        private void Login()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                LoginMessage = "Bitte Benutzername und Passwort eingeben.";
                return;
            }
            _nav.Navigate(new AdminShellViewModel(_nav));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}