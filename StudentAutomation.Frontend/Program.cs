using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StudentAutomation.Frontend;
using StudentAutomation.Frontend.Services;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Sadece backend için HttpClient ve AuthService’i register et
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5072/") });
builder.Services.AddScoped<AuthService>();

// ✅ AdminService
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<TeacherService>();

await builder.Build().RunAsync();
