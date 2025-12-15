using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace RentalSystem.AdminClient.ViewModel;
/// <summary>
/// ViewModel responsible for managing rental advertisements (annonces)
/// in the Admin Panel.
/// </summary>
/// <remarks>
/// This ViewModel represents a moderation workflow where administrators
/// can approve or reject submitted advertisements.
///
/// Currently uses dummy data. In a production setup, ads would be loaded
/// from an API and actions would trigger backend updates.
/// </remarks>
public class AnnoncerViewModel : BaseViewModel
{
    /// <summary>
    /// Collection of advertisements pending review.
    /// </summary>
    /// <remarks>
    /// ObservableCollection is used to ensure UI updates automatically
    /// when items are added or removed.
    /// </remarks>
    public ObservableCollection<AdModel> Ads { get; set; }
        = new ObservableCollection<AdModel>();

    private AdModel _selectedAd;
    /// <summary>
    /// The currently selected advertisement in the UI.
    /// </summary>
    /// <remarks>
    /// Used by Approve and Reject commands. Commands are disabled
    /// when this value is null.
    /// </remarks>
    public AdModel SelectedAd
    {
        get => _selectedAd;
        set { _selectedAd = value; OnPropertyChanged(); }
    }

    /// <summary>
    /// Command for approving the selected advertisement.
    /// </summary>
    public ICommand ApproveCommand { get; }

    /// <summary>
    /// Command for rejecting the selected advertisement.
    /// </summary>
    public ICommand RejectCommand { get; }

    /// <summary>
    /// Command for rejecting the selected advertisement.
    /// </summary>
    public AnnoncerViewModel()
    {
        // Dummy advertisements for UI testing
        Ads.Add(new AdModel { Title = "Annonce fra Youssef #1" });
        Ads.Add(new AdModel { Title = "Annonce fra Sebastian #2" });

        // Commands are only enabled when an ad is selected
        ApproveCommand = new RelayCommand(_ => Approve(), _ => SelectedAd != null);
        RejectCommand = new RelayCommand(_ => Reject(), _ => SelectedAd != null);
    }

    /// <summary>
    /// Approves the currently selected advertisement.
    /// </summary>
    /// <remarks>
    /// This is a placeholder implementation. In a real system,
    /// this would call an API endpoint.
    /// </remarks>
    private void Approve()
    {
        MessageBox.Show($"Approved: {SelectedAd.Title}");
    }

    /// <summary>
    /// Rejects the currently selected advertisement.
    /// </summary>
    /// <remarks>
    /// This is a placeholder implementation. In a real system,
    /// this would call an API endpoint.
    /// </remarks>
    private void Reject()
    {
        MessageBox.Show($"Rejected: {SelectedAd.Title}");
    }
}
/// <summary>
/// Lightweight model representing an advertisement.
/// </summary>
/// <remarks>
/// This model is intentionally simple for demo purposes.
/// It should later be replaced by a DTO from the API layer.
/// </remarks>
public class AdModel
{
    /// <summary>
    /// Display title of the advertisement.
    /// </summary>
    public string Title { get; set; }
}
