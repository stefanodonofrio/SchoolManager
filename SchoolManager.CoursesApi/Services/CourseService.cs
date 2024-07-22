using Microsoft.EntityFrameworkCore;
using StudentsManager.Common;

namespace SchoolManager.CoursesApi.Services
{
    public class CourseService : ICourseService
    {
        private readonly CourseDbContext dbContext;
        private readonly TeacherService teacherService;
        private readonly StudentService studentService;

        public CourseService(CourseDbContext dbContext, TeacherService teacherService, StudentService studentService)
        {
            this.dbContext = dbContext;
            this.teacherService = teacherService;
            this.studentService = studentService;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            var courses = await dbContext.Courses.ToListAsync();

            var coursesResult = new List<Course>();
            foreach (var course in courses)
            {
                var students = new List<Student>();
                foreach (var studentId in course.StudentIds)
                {
                    students.Add(await studentService.GetByIdAsync(studentId));
                }
                coursesResult.Add(new Course { 
                    Id = course.Id, 
                    Name= course.Name, 
                    Description = course.Description, 
                    Teacher = await teacherService.GetByIdAsync(course.TeacherId), 
                    Students = students }
                );
            }

            return coursesResult;
        }

        public async Task<Course> GetByIdAsync(Guid id)
        {
            var course = await dbContext.Courses.FindAsync(id);

            if (course == null) throw new KeyNotFoundException("Course not found");

            var students = new List<Student>();
            foreach (var studentId in course.StudentIds)
            {
                students.Add(await studentService.GetByIdAsync(studentId));
            }
            return new Course {
                Id =course.Id, 
                Name= course.Name, 
                Description= course.Description, 
                Teacher = await teacherService.GetByIdAsync(course.TeacherId), 
                Students= students 
            };
        }

        public async Task CreateAsync(Course course)
        {
            var studdentIds = course.Students.Select(s => s.Id).ToList();
            await dbContext.Courses.AddAsync(new CourseData
            (
                course.Id,
                course.Name,
                course.Description,
                course.Teacher.Id,
                studdentIds
            ));
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var course = await dbContext.Courses.FindAsync(id);
            if (course == null) throw new KeyNotFoundException("Course not found");

            dbContext.Courses.Remove(course);
            await dbContext.SaveChangesAsync();
        }
    }

    public interface ICourseService
    {
        public Task<IEnumerable<Course>> GetAllAsync();

        public Task<Course> GetByIdAsync(Guid id);

        public Task CreateAsync(Course course);

        public Task DeleteAsync(Guid id);
    }
}
