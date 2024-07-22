using Microsoft.EntityFrameworkCore;
using SchoolManager.CoursesApi;
using SchoolManager.CoursesApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CourseDbContext>(options => options.UseNpgsql(builder.Configuration["ConnectionStrings:CourseDb"]));
builder.Services.AddScoped<ICourseService,CourseService>();
builder.Services.AddSingleton<TeacherService>();
builder.Services.AddSingleton<StudentService>();
builder.Services.AddHttpClient<TeacherService>(c => { 
    var url = builder.Configuration["Services:TeachersApi"];
    c.BaseAddress = new (url); 
});
builder.Services.AddHttpClient<StudentService>(c => {
    var url = builder.Configuration["Services:StudentsApi"];
    c.BaseAddress = new(url);
});

builder.Services.AddControllers();

var app = builder.Build();


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
