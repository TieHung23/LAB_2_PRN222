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

builder.Services.AddScoped<IVehicleModelRepository, VehicleModelRepository>();
builder.Services.AddScoped<IVehicleModelService, VehicleModelService>();
builder.Services.AddScoped<IVehicleConfigRepository, VehicleConfigRepository>();
builder.Services.AddScoped<IVehicleConfigService, VehicleConfigService>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();


// Đăng ký các Service của BLL cho Presentation Layer
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IDealerService, DealerService>();
builder.Services.AddScoped<IVehicleModelService, VehicleModelService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPromotionService, PromotionService>();
builder.Services.AddScoped<ITestDriveService, TestDriveService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAIService, AIService>();


builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
{
    options.Cookie.Name = "MyCookieAuth";
    options.LoginPath = "/Account/Login"; // Trang sẽ chuyển đến nếu người dùng chưa đăng nhập
    options.AccessDeniedPath = "/Account/AccessDenied"; // Trang báo lỗi khi không có quyền
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Thời gian cookie hết hạn
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

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();