using ClassLibrary.persistance;
using Inspector.FluentUI2.Components;
using InspectorServices;
using InspectorServicesInterfaces;
using Microsoft.AspNetCore.Diagnostics;
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
    app.UseExceptionHandler(exceptionHandlerApp =>
    {
        exceptionHandlerApp.Run(async context =>
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            // using static System.Net.Mime.MediaTypeNames;
            context.Response.ContentType = Text.Plain;

            await context.Response.WriteAsync("An exception was thrown.");

            var exceptionHandlerPathFeature =
                context.Features.Get<IExceptionHandlerPathFeature>();

            await context.Response.WriteAsync(exceptionHandlerPathFeature.Error.ToString());

            if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
            {
                await context.Response.WriteAsync(" The file was not found.");
            }

            if (exceptionHandlerPathFeature?.Path == "/")
            {
                await context.Response.WriteAsync(" Page: Home.");
            }
        });
    });
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
