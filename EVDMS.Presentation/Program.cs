using EVDMS.BLL.WrapConfiguration;
using EVDMS.Presentation.SignalR;

var builder = WebApplication.CreateBuilder( args );
var passwordToHash = "admin123";
var hashedPassword = BCrypt.Net.BCrypt.HashPassword( passwordToHash );
Console.WriteLine( hashedPassword );

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDatabaseDAL( builder.Configuration );

builder.Services.AddRepositoryDAL();
builder.Services.AddServices();

builder.Services.AddSignalR();

builder.Services.AddAuthentication( "MyCookieAuth" ).AddCookie( "MyCookieAuth", options =>
{
    options.Cookie.Name = "MyCookieAuth";
    options.LoginPath = "/Account/Login"; // Trang sẽ chuyển đến nếu người dùng chưa đăng nhập
    options.AccessDeniedPath = "/Account/AccessDenied"; // Trang báo lỗi khi không có quyền
    options.ExpireTimeSpan = TimeSpan.FromMinutes( 30 ); // Thời gian cookie hết hạn
} );

builder.Logging.AddFilter( "Microsoft.EntityFrameworkCore", LogLevel.None );

var app = builder.Build();

// Configure the HTTP request pipeline.
if( !app.Environment.IsDevelopment() )
{
    app.UseExceptionHandler( "/Error" );
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.MapHub<NotificationHub>( "/chatHub" );
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();
