using StudentsManager.Common;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Net.Mail;

namespace SchoolManager.Frontend.Services
{
    public class CourseService
    {
        private readonly HttpClient httpClient;
        private readonly SmtpClient smtpClient;

        public CourseService(HttpClient httpClient, SmtpClient smtpClient)
        {
            this.httpClient = httpClient;
            this.smtpClient = smtpClient;
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

            using var message = new MailMessage("newsletter@school.com", "everyone@world.com")
            {
                Subject = "New Course being created",
                Body = $"A new course has been created! Watch out {json}"
            };

            await smtpClient.SendMailAsync(message);
        }

        public async Task DeleteAsync(Guid id)
        {
            await httpClient.DeleteAsync($"/api/courses/{id}");
        }
    }
}
