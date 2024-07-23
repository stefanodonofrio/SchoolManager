using Microsoft.EntityFrameworkCore;
using SchoolManager.CoursesApi;
using SchoolManager.CoursesApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.AddNpgsqlDbContext<CourseDbContext>("CourseDb", c => c.CommandTimeout = 300);
builder.Services.AddScoped<ICourseService,CourseService>();
builder.Services.AddSingleton<TeacherService>();
builder.Services.AddSingleton<StudentService>();
builder.Services.AddHttpClient<TeacherService>(c => { 
    c.BaseAddress = new ("https://schoolmanager-teachersapi"); 
});
builder.Services.AddHttpClient<StudentService>(c => {
    c.BaseAddress = new("https://schoolmanager-studentsapi");
});

builder.Services.AddControllers();

var app = builder.Build();

app.MapDefaultEndpoints();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<CourseDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
