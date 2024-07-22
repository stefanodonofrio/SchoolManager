using StudentsManager.Common;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace SchoolManager.Frontend.Services
{
    public class CourseService
    {
        private readonly HttpClient httpClient;

        public CourseService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            var response = await httpClient.GetAsync("/api/courses");
            var responseText = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var result = JsonSerializer.Deserialize<IEnumerable<Course>>(responseText, options) ?? new List<Course>();
            return result;
        }

        public async Task CreateAsync(Course course)
        {
            var json = JsonSerializer.Serialize(course);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await httpClient.PostAsync("/api/courses", content);
        }

        public async Task DeleteAsync(Guid id)
        {
            await httpClient.DeleteAsync($"/api/courses/{id}");
        }
    }
}
