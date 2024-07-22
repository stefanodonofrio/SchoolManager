using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsManager.Common;

namespace SchoolManager.TeachersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly TeacherDbContext context;

        public TeachersController(TeacherDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Teacher>> GetAll() =>
            await context.Teachers.ToListAsync<Teacher>();

        [HttpGet("{id}")]
        public async Task<Teacher> Get(Guid id) =>
           await context.Teachers.FindAsync(id);
 

        [HttpPost]
        public async Task Post([FromBody] Teacher teacher)
        {
            await context.Teachers.AddAsync(teacher);
            await context.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var teacherToDelete = context.Teachers.Find(id);
            context.Teachers.Remove(teacherToDelete);
            await context.SaveChangesAsync();
        }
    }
}
