using RentalSystem.AdminClient.Models;
using RentalSystem.AdminClient.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace RentalSystem.AdminClient.ViewModel
{
    public class SupportViewModel : BaseViewModel
    {
        private readonly ApiService _api = ApiService.Instance;

        public ObservableCollection<ContactModel> Contacts { get; } = new();

        private ContactModel? _selectedContact;
        public ContactModel? SelectedContact
        {
            get => _selectedContact;
            set { _selectedContact = value; OnPropertyChanged(); }
        }

        public ICommand RefreshCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CopyEmailCommand { get; }

        public SupportViewModel()
        {
            RefreshCommand = new RelayCommand(async _ => await LoadAsync());
            DeleteCommand = new RelayCommand(async _ => await Delete(), _ => SelectedContact != null);
            CopyEmailCommand = new RelayCommand(_ => CopyEmail(), _ => SelectedContact != null);

            _ = LoadAsync();
        }

        private async Task LoadAsync()
        {
            Contacts.Clear();
            var data = await _api.GetAllContactsAsync();
            foreach (var c in data)
                Contacts.Add(c);
        }

        private async Task Delete()
        {
            if (SelectedContact == null)
                return;

            var confirm = MessageBox.Show(
                "Delete this support ticket?",
                "Confirm",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirm != MessageBoxResult.Yes)
                return;

            await _api.DeleteContactAsync(SelectedContact.Id);
            await LoadAsync();
            SelectedContact = null;
        }

        private void CopyEmail()
        {
            if (SelectedContact == null)
                return;

            Clipboard.SetText(SelectedContact.Email);
        }
    }
}
