namespace StudentsManager.Common
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }

    public class Teacher
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string LastName { get; set; } 
        public string Email { get; set; } 
        public string Subject { get; set; }
    }

    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Teacher Teacher { get; set; }
        public IList<Student> Students { get; set; }
    }
}
