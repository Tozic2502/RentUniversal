using RentalSystem.AdminClient.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.IO;

namespace RentalSystem.AdminClient.Services
{
    /// <summary>
    /// Central API communication service for the admin client.
    /// Provides methods for authentication and CRUD operations
    /// on users, items and rentals.
    /// </summary>
    public class ApiService
    {
        // Singleton instance to ensure a single HttpClient is used
        private static ApiService? _instance;
        public static ApiService Instance => _instance ??= new ApiService();

        private readonly HttpClient _http;
        private string uri = "http://localhost:8080";

        /// <summary>
        /// Currently authenticated admin user.
        /// Null when no user is logged in.
        /// </summary>
        public UserModel? CurrentUser { get; private set; }

        /// <summary>
        /// Private constructor to enforce singleton usage.
        /// </summary>
        private ApiService()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri(uri)
            };
        }

        /// <summary>
        /// Retrieves all users from the API.
        /// </summary>
        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            var users = await _http.GetFromJsonAsync<List<UserModel>>("/api/Users");
            return users ?? new List<UserModel>();
        }

        /// <summary>
        /// Retrieves a single user by ID.
        /// </summary>
        public async Task<UserModel?> GetUserByIdAsync(string id)
        {
            return await _http.GetFromJsonAsync<UserModel>($"/api/Users/{id}");
        }

        /// <summary>
        /// Authenticates a user via the API.
        /// Stores the authenticated user on success.
        /// </summary>
        public async Task<bool> LoginAsync(string name, string email, string password)
        {
            // Debug helper to verify correct DataContext during login
            System.Diagnostics.Debug.WriteLine(
                $"[DC CHECK] DataContext VM = {GetType().Name}"
            );

            var response = await _http.PostAsJsonAsync("/api/Users/authenticate", new
            {
                name,
                email,
                password
            });

            if (!response.IsSuccessStatusCode)
                return false;

            CurrentUser = await response.Content.ReadFromJsonAsync<UserModel>();
            return CurrentUser != null;
        }

        /// <summary>
        /// Updates the role of a user (Customer, Admin, Owner).
        /// </summary>
        public async Task<bool> UpdateUserRoleAsync(string userId, string role)
        {
            var response = await _http.PutAsJsonAsync(
                $"/api/Users/{userId}/role",
                new { role }
            );

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Retrieves all items (announcements).
        /// </summary>
        public async Task<List<ItemModel>> GetAllItemsAsync()
        {
            return await _http.GetFromJsonAsync<List<ItemModel>>("/api/Items")
                   ?? new List<ItemModel>();
        }

        /// <summary>
        /// Creates a new item.
        /// </summary>
        public async Task<bool> CreateItemAsync(ItemModel item)
        {
            var res = await _http.PostAsJsonAsync("/api/Items", item);
            return res.IsSuccessStatusCode;
        }

        /// <summary>
        /// Updates an existing item.
        /// </summary>
        public async Task<bool> UpdateItemAsync(string id, ItemModel item)
        {
            var res = await _http.PutAsJsonAsync($"/api/Items/{id}", item);
            return res.IsSuccessStatusCode;
        }

        /// <summary>
        /// Deletes an item by ID.
        /// </summary>
        public async Task<bool> DeleteItemAsync(string id)
        {
            var res = await _http.DeleteAsync($"/api/Items/{id}");
            return res.IsSuccessStatusCode;
        }

        /// <summary>
        /// Retrieves the total number of users.
        /// </summary>
        public async Task<int> GetUsersCountAsync()
        {
            var users = await GetAllUsersAsync();
            return users.Count;
        }

        /// <summary>
        /// Retrieves all rentals belonging to a specific user.
        /// </summary>
        public async Task<List<RentalModel>> GetRentalsByUserAsync(string userId)
        {
            var rentals = await _http.GetFromJsonAsync<List<RentalModel>>(
                $"/api/Rentals/user/{userId}"
            );

            return rentals ?? new List<RentalModel>();
        }

        /// <summary>
        /// Uploads an image for a specific item.
        /// </summary>
        public async Task<bool> UploadItemImageAsync(string itemId, string filePath)
        {
            using var content = new MultipartFormDataContent();
            using var stream = File.OpenRead(filePath);

            content.Add(
                new StreamContent(stream),
                "file",
                Path.GetFileName(filePath)
            );

            var response = await _http.PostAsync(
                $"/api/items/{itemId}/images",
                content
            );

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Deletes an image from an item by image URL.
        /// </summary>
        public async Task<bool> DeleteItemImageAsync(string itemId, string imageUrl)
        {
            var fileName = Path.GetFileName(imageUrl);

            var response = await _http.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"/api/items/{itemId}/images", UriKind.Relative),
                Content = JsonContent.Create(fileName)
            });

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Retrieves a single item by ID.
        /// </summary>
        public async Task<ItemModel?> GetItemByIdAsync(string itemId)
        {
            return await _http.GetFromJsonAsync<ItemModel>(
                $"/api/Items/{itemId}"
            );
        }

        /// <summary>
        /// Clears the current user session.
        /// </summary>
        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
