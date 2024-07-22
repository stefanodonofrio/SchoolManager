using SchoolManager.Frontend.Components;
using SchoolManager.Frontend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<TeacherService>();
builder.Services.AddSingleton<StudentService>();
builder.Services.AddSingleton<CourseService>();

builder.Services.AddHttpClient<TeacherService>(c => {
    var url = builder.Configuration["Services:TeachersApi"];
    c.BaseAddress = new(url);
});
builder.Services.AddHttpClient<StudentService>(c => {
    var url = builder.Configuration["Services:StudentsApi"];
    c.BaseAddress = new(url);
});
builder.Services.AddHttpClient<CourseService>(c => {
    var url = builder.Configuration["Services:CoursesApi"];
    c.BaseAddress = new(url);
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
