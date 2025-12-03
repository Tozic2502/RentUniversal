using RentalSystem.AdminClient.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace RentalSystem.AdminClient.ViewModel
{
    public class UserViewModel : BaseViewModel
    {
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

        private UserModel _selectedUser;
        public UserModel SelectedUser
        {
            get => _selectedUser;
            set { _selectedUser = value; OnPropertyChanged(); }
        }

        public ObservableCollection<UserModel> AllUsers { get; set; }
        public ObservableCollection<UserModel> FilteredUsers { get; set; }

        public ICommand ResetPasswordCommand { get; }
        public ICommand ToggleBanCommand { get; }
        public ICommand DeleteUserCommand { get; }

        public UserViewModel()
        {
            // Dummy
            AllUsers = new ObservableCollection<UserModel>
            {
                new UserModel { Id="1", FullName="Youssef El Soueissi", Email="max@mail.com", RegisteredDate="2025-10-21", ActiveRentals=1, TotalRentals=4, LastLogin="2025-01-10", IsBanned=false },
                new UserModel { Id="2", FullName="Mikkel Doe", Email="mikkel@mail.com", RegisteredDate="2025-11-04", ActiveRentals=0, TotalRentals=2, LastLogin="2025-01-09", IsBanned=false },
                new UserModel { Id="3", FullName="Sebastian Doe", Email="seb@mail.com", RegisteredDate="2025-05-11", ActiveRentals=2, TotalRentals=8, LastLogin="2025-01-10", IsBanned=true },
                new UserModel { Id="4", FullName="Thommy Hansen", Email="thommy@easv365.dk", RegisteredDate="2025-05-11", ActiveRentals=8, TotalRentals=8, LastLogin="2025-01-10", IsBanned=true },

            };

            FilteredUsers = new ObservableCollection<UserModel>(AllUsers);

            ResetPasswordCommand = new RelayCommand(_ => ResetPassword());
            ToggleBanCommand = new RelayCommand(_ => ToggleBan());
            DeleteUserCommand = new RelayCommand(_ => DeleteUser());
        }

        private void FilterUsers()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredUsers = new ObservableCollection<UserModel>(AllUsers);
            }
            else
            {
                var search = SearchText.ToLower();
                FilteredUsers = new ObservableCollection<UserModel>(
                    AllUsers.Where(u =>
                        u.FullName.ToLower().Contains(search) ||
                        u.Email.ToLower().Contains(search)
                    ));
            }

            OnPropertyChanged(nameof(FilteredUsers));
        }

        private void ResetPassword()
        {
            if (SelectedUser == null) return;

            System.Windows.MessageBox.Show(
                $"Password reset initiated for {SelectedUser.FullName} (Dummy).",
                "Reset Password"
            );
        }

        private void ToggleBan()
        {
            if (SelectedUser == null) return;

            SelectedUser.IsBanned = !SelectedUser.IsBanned;
            OnPropertyChanged(nameof(SelectedUser));
        }

        private void DeleteUser()
        {
            if (SelectedUser == null) return;

            System.Windows.MessageBox.Show(
                $"Deleting user {SelectedUser.FullName} is not implemented (Dummy).",
                "Delete User"
            );
        }
    }
}
