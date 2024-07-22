using StudentsManager.Common;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SchoolManager.Frontend.Services
{
    public class StudentService
    {
        private readonly HttpClient httpClient;

        public StudentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task CreateAsync(Student student)
        {
            var json = JsonSerializer.Serialize(student);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await httpClient.PostAsync("/api/students", content);
        }

        public async Task DeleteAsync(Guid id)
        {
            await httpClient.DeleteAsync($"/api/students/{id}");
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            var response = await httpClient.GetAsync("/api/students");
            var responseText = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<IEnumerable<Student>>(responseText,options) ?? new List<Student>();
        }
    }
}
