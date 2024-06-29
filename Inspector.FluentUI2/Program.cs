using ClassLibrary.persistance;
using Inspector.FluentUI2.Components;
using InspectorServices;
using InspectorServicesInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContextFactory<LibraryContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString(name: "DBConnection"), opt =>
opt.EnableRetryOnFailure(
    maxRetryCount: 5,
    maxRetryDelay: TimeSpan.FromSeconds(30),
    errorNumbersToAdd: null)));

// ??? ???? ?????  

//opt.EnableRetryOnFailure(
//    maxRetryCount: 5, // ???? ?????? ???? ?????????
//    maxRetryDelay: System.TimeSpan.FromSeconds(value: 30), // ???? ?????? ???? ???????? ??? ?????????
//    errorNumbersToAdd: null ) // ????? ??????? ???? ??? ??????? ??? ????? ??????? ???? ??? ???????? ?????
//));

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
