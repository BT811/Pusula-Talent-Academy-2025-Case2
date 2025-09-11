using StudentAutomation.Frontend.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentAutomation.Frontend.Services
{
    public class CourseService
    {
        private readonly HttpClient _httpClient;
        

        public CourseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // 🔹 Tüm dersleri çek
        public async Task<IEnumerable<CourseModel>> GetCourses()
        {
            return await _httpClient.GetFromJsonAsync<List<CourseModel>>("api/Course") 
                   ?? new List<CourseModel>();
        }

        // 🔹 Yeni ders ekle
        public async Task<bool> CreateCourse(CreateCourseDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Course", dto);
            return response.IsSuccessStatusCode;
        }

        // 🔹 Ders güncelle
        public async Task<bool> UpdateCourseAsync(int id, CourseUpdateDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Course/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        // 🔹 Ders sil
        public async Task<bool> DeleteCourse(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Course/{id}");
            return response.IsSuccessStatusCode;
        }

        // 🔹 Öğretmeni ata
        public async Task<bool> AssignTeacher(int courseId, int teacherId)
        {
            var response = await _httpClient.PutAsync(
                $"api/Course/assign-teacher/{courseId}/{teacherId}", null);
            return response.IsSuccessStatusCode;
        }

        // 🔹 Ders statüsünü güncelle
        public async Task<bool> ChangeStatus(int courseId, CourseStatus status)
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"api/Course/update-status/{courseId}", status);
            return response.IsSuccessStatusCode;
        }
    }
}