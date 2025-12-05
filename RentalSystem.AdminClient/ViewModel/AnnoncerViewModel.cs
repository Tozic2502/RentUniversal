using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace RentalSystem.AdminClient.ViewModel;

public class AnnoncerViewModel : BaseViewModel
{
    public ObservableCollection<AdModel> Ads { get; set; }
        = new ObservableCollection<AdModel>();

    private AdModel _selectedAd;
    public AdModel SelectedAd
    {
        get => _selectedAd;
        set { _selectedAd = value; OnPropertyChanged(); }
    }

    public ICommand ApproveCommand { get; }
    public ICommand RejectCommand { get; }

    public AnnoncerViewModel()
    {
        Ads.Add(new AdModel { Title = "Annonce fra Youssef #1" });
        Ads.Add(new AdModel { Title = "Annonce fra Sebastian #2" });

        ApproveCommand = new RelayCommand(_ => Approve(), _ => SelectedAd != null);
        RejectCommand = new RelayCommand(_ => Reject(), _ => SelectedAd != null);
    }

    private void Approve()
    {
        MessageBox.Show($"Approved: {SelectedAd.Title}");
    }

    private void Reject()
    {
        MessageBox.Show($"Rejected: {SelectedAd.Title}");
    }
}

public class AdModel
{
    public string Title { get; set; }
}
