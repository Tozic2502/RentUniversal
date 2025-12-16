using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RentalSystem.AdminClient.ViewModel;
/// <summary>
/// Base class for all ViewModels in the Admin Client.
/// </summary>
/// <remarks>
/// Implements <see cref="INotifyPropertyChanged"/> to support WPF data binding.
/// All ViewModels should inherit from this class to avoid duplicating
/// property change notification logic.
/// </remarks>
public class BaseViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// Raised when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Notifies the UI that a property value has changed.
    /// </summary>
    /// <param name="propertyName">
    /// The name of the property that changed.
    /// Automatically supplied by the compiler when omitted.
    /// </param>
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}