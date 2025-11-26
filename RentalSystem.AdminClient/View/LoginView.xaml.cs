using System.Windows;
using System.Windows.Controls;

namespace RentalSystem.AdminClient.View;

public partial class LoginView
{
    public LoginView()
    {
        InitializeComponent();
    }
    
    private void PasswordBox_OnChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is LoginViewModel vm)
        {
            vm.Password = ((PasswordBox)sender).Password;
        }
    }
}