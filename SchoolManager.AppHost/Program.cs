var builder = DistributedApplication.CreateBuilder(args);

var teacherDb = builder.AddConnectionString("TeacherDb");
var teacher = builder.AddProject<Projects.SchoolManager_TeachersApi>("schoolmanager-teachersapi").WithReference(teacherDb);

var studentDb = builder.AddConnectionString("StudentDb");
var student = builder.AddProject<Projects.SchoolManager_StudentsApi>("schoolmanager-studentsapi").WithReference(studentDb);

var courseDb = builder.AddConnectionString("CourseDb");
var course = builder.AddProject<Projects.SchoolManager_CoursesApi>("schoolmanager-coursesapi").WithReference(teacher).WithReference(student).WithReference(courseDb);

var mailDev = builder.AddMailDev("maildev");

builder.AddProject<Projects.SchoolManager_Frontend>("schoolmanager-frontend").WithReference(teacher).WithReference(student).WithReference(course).WithReference(mailDev);

builder.Build().Run();
