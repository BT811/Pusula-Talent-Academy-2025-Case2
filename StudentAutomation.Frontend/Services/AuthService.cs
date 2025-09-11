using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using StudentAutomation.Frontend.Models;

namespace StudentAutomation.Frontend.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;
        public event Action? OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();


        public AuthService(HttpClient http, IJSRuntime js)
        {
            _http = http;
            _js = js;
        }

        public async Task<LoginResponse?> Login(string email, string password)
        {
            var loginDto = new { Email = email, Password = password };
            var response = await _http.PostAsJsonAsync("api/Auth/login", loginDto);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (result != null)
                {

                    await _js.InvokeVoidAsync("localStorage.setItem", "jwtToken", result.Token);
                    await _js.InvokeVoidAsync("localStorage.setItem", "userName", result.FirstName + " " + result.LastName);
                    await _js.InvokeVoidAsync("localStorage.setItem", "userRole", result.Role);
                }
                NotifyStateChanged();
                return result;
            }

            return null;
        }

        public async Task<string?> Register(RegisterModel registerModel)
{
            try
            {
                var response = await _http.PostAsJsonAsync("api/Auth/register", registerModel);
                if (response.IsSuccessStatusCode)
                    return null; // Hata yok, kayıt başarılı

                // Backend’den dönen hata mesajını oku
                var error = await response.Content.ReadAsStringAsync();
                return error;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task Logout()
        {
            // Token'ı temizle
            await _js.InvokeVoidAsync("localStorage.removeItem", "jwtToken");
            await _js.InvokeVoidAsync("localStorage.removeItem", "userName");
            await _js.InvokeVoidAsync("localStorage.removeItem", "userRole");
            NotifyStateChanged();
        }


        public async Task<string?> GetToken()
        {
            return await _js.InvokeAsync<string>("localStorage.getItem", "jwtToken");
        }

        public async Task<bool> IsLoggedIn()
        {
            var token = await GetToken();
            return !string.IsNullOrEmpty(token);
        }
        public async Task<string?> GetUserName()
        {
            return await _js.InvokeAsync<string>("localStorage.getItem", "userName");
        }

        public async Task<string?> GetUserRole()
        {
            return await _js.InvokeAsync<string>("localStorage.getItem", "userRole");
        }

            
    }
}
