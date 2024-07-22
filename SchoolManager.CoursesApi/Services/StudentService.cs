using StudentsManager.Common;
using System.Text.Json;

namespace SchoolManager.CoursesApi.Services
{
    public class StudentService
    {
        private readonly HttpClient client;

        public StudentService(HttpClient httpClient)
        {
            this.client = httpClient;
        }

        public async Task<Student> GetByIdAsync(Guid id)
        {
            var response = await client.GetAsync($"/api/students/{id}");
            var responseText = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<Student>(responseText, options);
        }
    }
}
