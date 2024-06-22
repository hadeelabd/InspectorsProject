using ClassLibrary.persistance;
using Inspector.FluentUI2.Components;
using InspectorServices;
using InspectorServicesInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContextFactory<LibraryContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString(name: "DBConnection")));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddFluentUIComponents();

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
