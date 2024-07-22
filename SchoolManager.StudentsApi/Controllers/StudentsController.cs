using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsManager.Common;

namespace SchoolManager.StudentsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentDbContext context;

        public StudentsController(StudentDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Student>> GetAll() =>
            await context.Students.ToListAsync<Student>();


        [HttpGet("{id}")]
        public async Task<Student> Get(Guid id) =>
            await context.Students.FindAsync(id);

        [HttpPost]
        public async Task Post([FromBody] Student student)
        {
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var studentToDelete = context.Students.Find(id);
            context.Students.Remove(studentToDelete);
            await context.SaveChangesAsync();
        }
    }
}
