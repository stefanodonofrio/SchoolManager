using StudentsManager.Common;
using System.Text.Json;
using System.Text;

namespace SchoolManager.Frontend.Services
{
    public class TeacherService
    {
        private readonly HttpClient httpClient;

        public TeacherService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }


        public async Task CreateAsync(Teacher teacher)
        {
            var json = JsonSerializer.Serialize(teacher);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await httpClient.PostAsync("/api/teachers", content);
        }

        public async Task DeleteAsync(Guid id)
        {
            await httpClient.DeleteAsync($"/api/teachers/{id}");
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            var response = await httpClient.GetAsync("/api/teachers");
            var responseText = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<IEnumerable<Teacher>>(responseText, options) ?? new List<Teacher>();
        }
    }
}
