using StudentAutomation.Frontend.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;
namespace StudentAutomation.Frontend.Services
{
    public class TeacherService
    {
        private readonly HttpClient _http;
        private readonly AuthService _authService;

        public TeacherService(HttpClient http, AuthService authService)
        {
            _http = http;
            _authService = authService;
        }

        private async Task AddAuthHeader()
        {
            var token = await _authService.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        // 🔹 Öğrencileri çek
        public async Task<IEnumerable<UserModel>> GetStudents()
        {
            await AddAuthHeader();

            string url = "api/User/all";

            var users = await _http.GetFromJsonAsync<List<UserModel>>(url);
            return users ?? new List<UserModel>();
        }

        public async Task<bool> CreateStudent(RegisterModel model)
        {
            await AddAuthHeader();
            var response = await _http.PostAsJsonAsync("api/User", model);
            return response.IsSuccessStatusCode;
        }

        // 🔹 Öğrenci güncelle
        public async Task<bool> UpdateStudent(int userId, UpdateUserModel model)
        {
            await AddAuthHeader();
            var response = await _http.PutAsJsonAsync($"api/User/{userId}", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteStudent(int userId)
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
        // 🔹 Öğretmenin kendi derslerini çek
        public async Task<IEnumerable<CourseModel>> GetMyCourses()
        {
            await AddAuthHeader();
            var courses = await _http.GetFromJsonAsync<List<CourseModel>>("api/Course/my-courses");
            return courses ?? new List<CourseModel>();
        }

        // 🔹 Ders statüsünü güncelle
        public async Task<bool> UpdateCourseStatus(int courseId, CourseStatus status)
        {
            await AddAuthHeader();
            var response = await _http.PutAsJsonAsync($"api/Course/update-status/{courseId}", status);
            return response.IsSuccessStatusCode;
        }

        // 🔹 Derse öğrenci ekle
        public async Task<bool> EnrollStudent(int courseId, int studentId)
        {
            await AddAuthHeader();
            var response = await _http.PostAsync($"api/Course/{courseId}/enroll/{studentId}", null);
            return response.IsSuccessStatusCode;
        }

        // 🔹 Dersten öğrenci çıkar
        public async Task<bool> RemoveStudent(int courseId, int studentId)
        {
            await AddAuthHeader();
            var response = await _http.DeleteAsync($"api/Course/{courseId}/enroll/{studentId}");
            return response.IsSuccessStatusCode;
        }
        
    }
}
