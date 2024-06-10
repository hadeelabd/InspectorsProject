using InspectorServices;
using InspectorServicesInterfaces;
using InspectorUI.Components;
using ClassLibrary.persistance;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<LibraryContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString(name: "DBConnection")));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<IInspectorService, InspectorService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
