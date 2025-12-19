using RentalSystem.AdminClient.Models;
using RentalSystem.AdminClient.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RentalSystem.AdminClient.ViewModel
{
    public class UserViewModel : BaseViewModel
    {
        private readonly ApiService _api = ApiService.Instance;

        public ObservableCollection<UserModel> AllUsers { get; set; }
        public ObservableCollection<UserModel> FilteredUsers { get; set; }

        private UserModel _selectedUser;
        public UserModel SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
                LoadSelectedUserDetails();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterUsers();
            }
        }

        // Commands
        public ICommand RefreshCommand { get; }
        public ICommand PromoteUserCommand { get; }
        public ICommand DemoteUserCommand { get; }

        public UserViewModel()
        {
            AllUsers = new ObservableCollection<UserModel>();
            FilteredUsers = new ObservableCollection<UserModel>();

            RefreshCommand = new RelayCommand(async _ => await LoadUsersAsync());

            PromoteUserCommand = new RelayCommand(
                async _ => await PromoteUser(),
                _ => CanPromote());

            DemoteUserCommand = new RelayCommand(
                async _ => await DemoteUser(),
                _ => CanDemote());

            _ = LoadUsersAsync();
        }

        // LOAD USERS

        private async Task LoadUsersAsync()
        {
            var users = await _api.GetAllUsersAsync();

            AllUsers.Clear();
            foreach (var u in users)
                AllUsers.Add(u);

            FilterUsers();
        }

        private void FilterUsers()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredUsers = new ObservableCollection<UserModel>(AllUsers);
            }
            else
            {
                var query = SearchText.ToLower();
                FilteredUsers = new ObservableCollection<UserModel>(
                    AllUsers.Where(u =>
                        u.FullName.ToLower().Contains(query) ||
                        u.Email.ToLower().Contains(query))
                );
            }

            OnPropertyChanged(nameof(FilteredUsers));
        }

        // USER DETAILS

        private async void LoadSelectedUserDetails()
        {
            if (SelectedUser == null)
                return;

            var detailed = await _api.GetUserByIdAsync(SelectedUser.Id);
            if (detailed == null)
                return;

            SelectedUser.FullName = detailed.FullName;
            SelectedUser.Email = detailed.Email;
            SelectedUser.Role = detailed.Role;
            SelectedUser.IdentificationId = detailed.IdentificationId;
            
            var rentals = await _api.GetRentalsByUserAsync(SelectedUser.Id);

            var now = DateTime.UtcNow;

            SelectedUser.TotalRentals = rentals.Count;

            SelectedUser.ActiveRentals = rentals.Count(r =>
                r.StartDate <= now &&
                r.EndDate >= now
            );

            OnPropertyChanged(nameof(SelectedUser));
            CommandManager.InvalidateRequerySuggested();
        }

        // ROLE LOGIC

        private bool IsCurrentUserSelected =>
            SelectedUser != null &&
            _api.CurrentUser != null &&
            SelectedUser.Id == _api.CurrentUser.Id;

        private bool CanPromote()
        {
            if (SelectedUser == null) return false;
            if (IsCurrentUserSelected) return false;

            return SelectedUser.Role switch
            {
                "Customer" => true,
                "Admin" => true,
                _ => false
            };
        }

        private bool CanDemote()
        {
            if (SelectedUser == null) return false;
            if (IsCurrentUserSelected) return false;

            return SelectedUser.Role switch
            {
                "Owner" => true,
                "Admin" => true,
                _ => false // Customer
            };
        }

        // Promote / Demote handlers

        private async Task PromoteUser()
        {
            if (SelectedUser == null)
                return;

            string newRole = SelectedUser.Role switch
            {
                "Customer" => "Admin",
                "Admin" => "Owner",
                _ => SelectedUser.Role
            };

            if (newRole == SelectedUser.Role)
                return;

            var confirm = MessageBox.Show(
                $"Promote {SelectedUser.FullName} to {newRole}?",
                "Confirm Promotion",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirm != MessageBoxResult.Yes)
                return;

            await UpdateUserRoleAsync(newRole);
        }

        private async Task DemoteUser()
        {
            if (SelectedUser == null)
                return;

            string newRole = SelectedUser.Role switch
            {
                "Owner" => "Admin",
                "Admin" => "Customer",
                _ => SelectedUser.Role
            };

            if (newRole == SelectedUser.Role)
                return;

            var confirm = MessageBox.Show(
                $"Demote {SelectedUser.FullName} to {newRole}?",
                "Confirm Demotion",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirm != MessageBoxResult.Yes)
                return;

            await UpdateUserRoleAsync(newRole);
        }

        private async Task UpdateUserRoleAsync(string newRole)
        {
            var success = await _api.UpdateUserRoleAsync(SelectedUser.Id, newRole);

            if (!success)
            {
                MessageBox.Show(
                    "Failed to update user role.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            SelectedUser.Role = newRole;
            OnPropertyChanged(nameof(SelectedUser));
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
