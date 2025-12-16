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