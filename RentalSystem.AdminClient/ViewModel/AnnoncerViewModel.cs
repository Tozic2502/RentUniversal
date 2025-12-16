using RentalSystem.AdminClient.Models;
using RentalSystem.AdminClient.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RentalSystem.AdminClient.ViewModel
{
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
        private readonly ApiService _api = ApiService.Instance;

        public ObservableCollection<ItemModel> Items { get; } = new();

        private ItemModel? _selectedItem;
        public ItemModel? SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem == value)
                    return;

                _selectedItem = value;
                OnPropertyChanged();

                if (value != null)
                    LoadSelectedItem();
                
                CommandManager.InvalidateRequerySuggested();
            }
        }

        // form fields
        private string _name = "";
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        private string _category = "";
        public string Category
        {
            get => _category;
            set { _category = value; OnPropertyChanged(); }
        }

        private string _condition = "";
        public string Condition
        {
            get => _condition;
            set { _condition = value; OnPropertyChanged(); }
        }

        private decimal _value;
        public decimal Value
        {
            get => _value;
            set { _value = value; OnPropertyChanged(); }
        }

        private bool _isAvailable = true;
        public bool IsAvailable
        {
            get => _isAvailable;
            set { _isAvailable = value; OnPropertyChanged(); }
        }

        // Commands
        public ICommand RefreshCommand { get; }
        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public AnnoncerViewModel()
        {
            RefreshCommand = new RelayCommand(async _ => await LoadItemsAsync());

            CreateCommand = new RelayCommand(
                async _ => await CreateItem(),
                _ => CanCreate());

            UpdateCommand = new RelayCommand(
                async _ => await UpdateItem(),
                _ => SelectedItem != null);

            DeleteCommand = new RelayCommand(
                async _ => await DeleteItem(),
                _ => SelectedItem != null);

            _ = LoadItemsAsync();
        }

        // 
        // Load
        // 

        private async Task LoadItemsAsync()
        {
            Items.Clear();
            var items = await _api.GetAllItemsAsync();
            foreach (var item in items)
                Items.Add(item);
        }

        private void LoadSelectedItem()
        {
            if (SelectedItem == null)
                return;

            Name = SelectedItem.Name;
            Category = SelectedItem.Category;
            Condition = SelectedItem.Condition;
            Value = SelectedItem.Value;
            IsAvailable = SelectedItem.IsAvailable;
        }

        // 
        // CRUD
        //

        private bool CanCreate()
        {
            return !string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(Category)
                && !string.IsNullOrWhiteSpace(Condition)
                && Value > 0;
        }

        private async Task CreateItem()
        {
            var item = new ItemModel
            {
                Name = Name,
                Category = Category,
                Condition = Condition,
                Value = Value,
                IsAvailable = IsAvailable
            };

            var success = await _api.CreateItemAsync(item);
            if (!success)
            {
                MessageBox.Show("Failed to create item.", "Error");
                return;
            }

            await LoadItemsAsync();
            ClearForm();
        }

        private async Task UpdateItem()
        {
            if (SelectedItem == null)
                return;

            SelectedItem.Name = Name;
            SelectedItem.Category = Category;
            SelectedItem.Condition = Condition;
            SelectedItem.Value = Value;
            SelectedItem.IsAvailable = IsAvailable;

            var success = await _api.UpdateItemAsync(SelectedItem.Id, SelectedItem);
            if (!success)
            {
                MessageBox.Show("Failed to update item.", "Error");
                return;
            }

            await LoadItemsAsync();
        }

        private async Task DeleteItem()
        {
            if (SelectedItem == null)
                return;

            var confirm = MessageBox.Show(
                $"Delete item '{SelectedItem.Name}'?",
                "Confirm delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirm != MessageBoxResult.Yes)
                return;

            await _api.DeleteItemAsync(SelectedItem.Id);
            await LoadItemsAsync();
            ClearForm();
        }

        // 
        // Util / helpers
        // 

        private void ClearForm()
        {
            Name = "";
            Category = "";
            Condition = "";
            Value = 0;
            IsAvailable = true;
        }
    }
}
