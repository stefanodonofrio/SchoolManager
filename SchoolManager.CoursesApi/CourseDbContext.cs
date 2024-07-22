using Microsoft.EntityFrameworkCore;

namespace SchoolManager.CoursesApi
{
    public class CourseDbContext(DbContextOptions<CourseDbContext> options) : DbContext(options)
    {
        public DbSet<CourseData> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseData>().HasKey(course => course.Id);
            modelBuilder.Entity<CourseData>().ToTable("Courses");
        }
    }

    public record CourseData(Guid Id, string Name, string Description, Guid TeacherId, IList<Guid> StudentIds);

}
