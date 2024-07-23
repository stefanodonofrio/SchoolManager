using Microsoft.EntityFrameworkCore;
using SchoolManager.TeachersApi;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.AddNpgsqlDbContext<TeacherDbContext>("TeacherDb", c => c.CommandTimeout=300);

builder.Services.AddControllers();

var app = builder.Build();

app.MapDefaultEndpoints();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<TeacherDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
