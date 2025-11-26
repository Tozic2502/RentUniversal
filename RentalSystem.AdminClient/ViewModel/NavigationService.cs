namespace RentalSystem.AdminClient.ViewModel;

public class NavigationService
{
    private Action<object> _navigateAction;

    public void Configure(Action<object> navigateAction)
    {
        _navigateAction = navigateAction;
    }

    public void Navigate(object viewModel)
    {
        _navigateAction?.Invoke(viewModel);
    }
}