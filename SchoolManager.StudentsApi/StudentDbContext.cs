using Microsoft.EntityFrameworkCore;
using StudentsManager.Common;

namespace SchoolManager.StudentsApi
{
    public class StudentDbContext(DbContextOptions<StudentDbContext> options) : DbContext(options)
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasKey(student => student.Id);
            modelBuilder.Entity<Student>().ToTable("Students");
        }
    }
}
