using StudentsManager.Common;
using System.Text.Json;

namespace SchoolManager.CoursesApi.Services
{
    public class TeacherService
    {
        private readonly HttpClient client;

        public TeacherService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Teacher> GetByIdAsync(Guid id)
        {
            var response = await client.GetAsync($"/api/teachers/{id}");
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<Teacher>(responseText, options);
        }
    }
}
