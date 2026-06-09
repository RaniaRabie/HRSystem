using HRSystem.BLL.Common;
using HRSystem.BLL.Services.Abstraction;
using HRSystem.BLL.Services.Implementation;
using HRSystem.BLL.Setting;
using HRSystem.DAL.Common;
using HRSystem.DAL.Context;
using HRSystem.DAL.Models.Entities;
using HRSystem.DAL.Seeding;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.MVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /* --------------------------------------------------------------------------*/

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            /* --------------------------------------------------------------------------*/

            // Enhancement ConnectionString
            var connectionString = builder.Configuration.GetConnectionString("defaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

            /* --------------------------------------------------------------------------*/

            // Identity 
            builder.Services
                .AddIdentity<HRSystemUser, IdentityRole>(options =>
                {
                    // Password
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 8;

                    // User
                    options.User.RequireUniqueEmail = true;

                    // Sign in — no email confirmation needed
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedEmail = false;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            /* --------------------------------------------------------------------------*/

            // Cookie config
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromHours(8);
            });

            /* --------------------------------------------------------------------------*/

            // Dependency Injection
            // first register the repository, then the service that depends on it, so that when the service is instantiated, it can receive the repository instance through constructor injection.
            //builder.Services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>(); 
            builder.Services.AddServicesInDAL();

            //builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddServicesInBLL();

            /* --------------------------------------------------------------------------*/

            // Email Settings 
            builder.Services.Configure<EmailSettings>(
                builder.Configuration.GetSection("EmailSettings"));

            /* --------------------------------------------------------------------------*/

            // HttpContextAccessor — needed for audit fields in DbContext
            builder.Services.AddHttpContextAccessor();

            /* --------------------------------------------------------------------------*/

            var app = builder.Build();

            /* --------------------------------------------------------------------------*/

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Dashboard}/{action=Index}/{id?}");
           

            await SeedDatabase(app);

            app.Run();
        }

            static async Task SeedDatabase(WebApplication app)
            {
                using var scope = app.Services.CreateScope();
                var services = scope.ServiceProvider;

                try
                {
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var userManager = services.GetRequiredService<UserManager<HRSystemUser>>();

                    await RoleSeeder.SeedRolesAsync(roleManager);
                    await AdminSeeder.SeedAdminAsync(userManager);
                    //await HRSeeder.SeedHRAsync(userManager);
                    
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database");
                }
            }
    }
}
