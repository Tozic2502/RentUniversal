namespace RentalSystem.AdminClient.ViewModel;

public class NavigationService
{
    /// <summary>
    /// Centralized navigation service for switching ViewModels.
    /// </summary>
    /// <remarks>
    /// This service decouples navigation logic from individual ViewModels.
    /// The MainWindow subscribes once and reacts to navigation requests.
    /// </remarks>
    private Action<object> _navigateAction;

    /// <summary>
    /// Configures the navigation callback.
    /// </summary>
    /// <param name="navigateAction">
    /// Action that assigns the active ViewModel in the shell.
    /// </param>
    public void Configure(Action<object> navigateAction)
    {
        _navigateAction = navigateAction;
    }

    /// <summary>
    /// Navigates to a new ViewModel.
    /// </summary>
    public void Navigate(object viewModel)
    {
        _navigateAction?.Invoke(viewModel);
    }
}