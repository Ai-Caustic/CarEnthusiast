//using CarEnthusiast.Data;
//using Microsoft.AspNetCore;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using System;

//namespace CarEnthusiast
//{
//    public class Program
//    {

//        public static void Main(string[] args)
//        {

//            var host = CreateHostBuilder(args).Build();

//            using (var scope = host.Services.CreateScope())
//            {
//                var services = scope.ServiceProvider;
//                try
//                {
//                    var context = services.GetRequiredService<UserContext>();
//                    DbInitializer.Initialize(context);
//                }
//                catch (Exception ex)
//                {
//                    var logger = services.GetRequiredService<ILogger<Program>>();
//                    logger.LogError(ex, "An error occurred while creating the database");
//                }
//            }

//            host.Run();
//        }

//        public static IHostBuilder CreateHostBuilder(string[] args) =>
//            Host.CreateDefaultBuilder(args)
//            .ConfigureWebHostDefaults(webbuilder =>
//            {
//                webbuilder.UseStartup<Startup>();
//            });
//    }
//}
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CarEnthusiast.Data;
using CarEnthusiast.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(30));
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<UserContext>();
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<Role>()
    .AddEntityFrameworkStores<UserContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password Settings
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;

    // Lockout Settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = false;


    // User settings
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;

});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie Settings
    options.Cookie.HttpOnly = false;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Views/Users/Login";
    options.AccessDeniedPath = "/Views/Shared/Error";
    options.SlidingExpiration = true;
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
//app.UseIdentity();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

