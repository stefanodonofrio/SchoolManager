var builder = DistributedApplication.CreateBuilder(args);

var teacher = builder.AddProject<Projects.SchoolManager_TeachersApi>("schoolmanager-teachersapi");

var student = builder.AddProject<Projects.SchoolManager_StudentsApi>("schoolmanager-studentsapi");

var course = builder.AddProject<Projects.SchoolManager_CoursesApi>("schoolmanager-coursesapi").WithReference(teacher).WithReference(student);

builder.AddProject<Projects.SchoolManager_Frontend>("schoolmanager-frontend").WithReference(teacher).WithReference(student).WithReference(course);

builder.Build().Run();
