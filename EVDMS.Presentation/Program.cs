using EVDMS.BLL.Services.Abstractions;
using EVDMS.BLL.Services.Implementations;
using EVDMS.BLL.WrapConfiguration;
using EVDMS.DAL.Repositories.Abstractions;
using EVDMS.DAL.Repositories.Implementations;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
var passwordToHash = "admin123";
var hashedPassword = BCrypt.Net.BCrypt.HashPassword(passwordToHash);
Console.WriteLine(hashedPassword);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDatabaseDAL(builder.Configuration);

builder.Services.AddRepositoryDAL();
builder.Services.AddServices();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.AccessDeniedPath = "/Home/AccessDenied";
    });
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.None);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();