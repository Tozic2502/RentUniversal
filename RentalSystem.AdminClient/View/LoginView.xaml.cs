using System.Windows.Controls;
using RentalSystem.AdminClient.ViewModel;

namespace RentalSystem.AdminClient.View
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Register if there is a change in the PasswordBox field
        /// </summary>
        private void PasswordBox_OnPasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm &&
                sender is PasswordBox pb)
            {
                vm.Password = pb.Password;
            }
        }
    }
}