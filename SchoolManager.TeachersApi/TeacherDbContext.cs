using Microsoft.EntityFrameworkCore;
using StudentsManager.Common;

namespace SchoolManager.TeachersApi
{
    public class TeacherDbContext(DbContextOptions<TeacherDbContext> options) : DbContext(options)
    {
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>().HasKey(teacher => teacher.Id);
            modelBuilder.Entity<Teacher>().ToTable("Teachers");
        }
    }
}
