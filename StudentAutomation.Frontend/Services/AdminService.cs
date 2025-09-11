using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using StudentAutomation.Frontend.Models;

namespace StudentAutomation.Frontend.Services
{
    public class AdminService
    {
        private readonly HttpClient _http;
        private readonly AuthService _authService;

        public AdminService(HttpClient http, AuthService authService)
        {
            _http = http;
            _authService = authService;
        }

        // Header ekleme helper
        private async Task AddAuthHeader()
        {
            var token = await _authService.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<List<UserModel>> GetUsers(string role = null)
        {
            await AddAuthHeader();

            string url = "api/User/all";
            if (!string.IsNullOrEmpty(role))
            {
                url += $"?role={role}";
            }

            var users = await _http.GetFromJsonAsync<List<UserModel>>(url);
            return users ?? new List<UserModel>();
        }

        public async Task<bool> CreateUser(RegisterModel model)
        {
            await AddAuthHeader();
            var response = await _http.PostAsJsonAsync("api/User", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUser(int userId, UpdateUserModel model)
        {
            await AddAuthHeader();
            var response = await _http.PutAsJsonAsync($"api/User/{userId}", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            await AddAuthHeader();
            var response = await _http.DeleteAsync($"api/User/{userId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<UserModel?> GetUserById(int userId)
        {
            await AddAuthHeader();
            return await _http.GetFromJsonAsync<UserModel>($"api/User/{userId}");
        }
    }
}
