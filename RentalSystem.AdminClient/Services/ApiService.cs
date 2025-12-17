using RentalSystem.AdminClient.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;

namespace RentalSystem.AdminClient.Services
{
    public class ApiService
    {
        private static ApiService? _instance;
        public static ApiService Instance => _instance ??= new ApiService();

        private readonly HttpClient _http;
        private string uri = "http://localhost:8080";

        public UserModel? CurrentUser { get; private set; }

        private ApiService()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri(uri)
            };
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            var users = await _http.GetFromJsonAsync<List<UserModel>>("/api/Users");
            return users ?? new List<UserModel>();
        }

        public async Task<UserModel?> GetUserByIdAsync(string id)
        {
            return await _http.GetFromJsonAsync<UserModel>($"/api/Users/{id}");
        }
        
        public async Task<bool> LoginAsync(string name, string email, string password)
        {
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
        
        public async Task<bool> UpdateUserRoleAsync(string userId, string role)
        {
            var response = await _http.PutAsJsonAsync(
                $"/api/Users/{userId}/role",
                new { role }
            );

            return response.IsSuccessStatusCode;
        }
        
        // GET all items
        public async Task<List<ItemModel>> GetAllItemsAsync()
        {
            return await _http.GetFromJsonAsync<List<ItemModel>>("/api/Items")
                   ?? new List<ItemModel>();
        }

// CREATE
        public async Task<bool> CreateItemAsync(ItemModel item)
        {
            var res = await _http.PostAsJsonAsync("/api/Items", item);
            return res.IsSuccessStatusCode;
        }

// UPDATE
        public async Task<bool> UpdateItemAsync(string id, ItemModel item)
        {
            var res = await _http.PutAsJsonAsync($"/api/Items/{id}", item);
            return res.IsSuccessStatusCode;
        }

// DELETE
        public async Task<bool> DeleteItemAsync(string id)
        {
            var res = await _http.DeleteAsync($"/api/Items/{id}");
            return res.IsSuccessStatusCode;
        }
        
        public async Task<int> GetUsersCountAsync()
        {
            var users = await GetAllUsersAsync();
            return users.Count;
        }
        
        public async Task<List<RentalModel>> GetRentalsByUserAsync(string userId)
        {
            var rentals = await _http.GetFromJsonAsync<List<RentalModel>>(
                $"/api/Rentals/user/{userId}"
            );

            return rentals ?? new List<RentalModel>();
        }

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

        public async Task<ItemModel?> GetItemByIdAsync(string itemId)
        {
            return await _http.GetFromJsonAsync<ItemModel>(
                $"/api/Items/{itemId}"
            );
        }

        public void Logout()
        {
            CurrentUser = null;
        }
        
        
    }
}