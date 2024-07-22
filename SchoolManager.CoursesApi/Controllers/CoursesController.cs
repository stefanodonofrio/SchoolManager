using Microsoft.AspNetCore.Mvc;
using SchoolManager.CoursesApi.Services;
using StudentsManager.Common;

namespace SchoolManager.CoursesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService courseService;

        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        public async Task<IEnumerable<Course>> GetAll() => 
            await courseService.GetAllAsync();
        

        [HttpGet("{id}")]
        public async Task<Course> Get(Guid id) =>
            await courseService.GetByIdAsync(id);
        

        [HttpPost]
        public async Task Post([FromBody] Course course) =>
            await courseService.CreateAsync(course);

        [HttpDelete("{id}")]
        public async Task Delete(Guid id) =>
            await courseService.DeleteAsync(id);
        
    }
}
