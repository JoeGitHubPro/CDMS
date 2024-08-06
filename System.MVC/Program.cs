using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.BAL.StartUp;
using System.DAL.Data;
using System.MVC.Services;


namespace System.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var localConnectionString = builder.Configuration.GetConnectionString("LocalConnection") ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");

            builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
            optionsBuilder.UseSqlServer(localConnectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            var lockoutOptions = new LockoutOptions()
            {
                AllowedForNewUsers = true,
                DefaultLockoutTimeSpan = TimeSpan.FromHours(1),
                MaxFailedAccessAttempts = 5
            };

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => { options.SignIn.RequireConfirmedAccount = false; options.Lockout = lockoutOptions; })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            //Database Initialization 
            using (var scope = builder.Services.BuildServiceProvider().CreateAsyncScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // var user = scope.ServiceProvider.GetRequiredService<ApplicationUser>();

                ApplicationUser? user = builder.Configuration.GetSection("User").Get<ApplicationUser>();
                string? userPassword = builder.Configuration.GetSection("Password").Get<string>();

                if (context.Database.GetPendingMigrations().Any())
                    context.Database.Migrate();

                if (context is not null && user is not null)
                {
                    Initialization initialize = new Initialization(context, user, userPassword);
                    initialize.InitializeRoles();
                    initialize.InitializeUsers();
                }
            }

            //EmailService
            builder.Services.AddTransient<IEmailSender, EmailSender>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
