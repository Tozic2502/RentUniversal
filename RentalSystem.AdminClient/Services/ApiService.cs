using RentalSystem.AdminClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

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



        public void Logout()
        {
            CurrentUser = null;
        }
        
        
    }
}