using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.BAL.StartUp;
using System.DAL.Data;
using System.MVC.Models.Settings;
using System.MVC.Services;


namespace System.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var localConnectionString = builder.Configuration.GetConnectionString("RemoteConnection") ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");

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

            #region DatabaseInitialization 

            using (var scope = builder.Services.BuildServiceProvider().CreateAsyncScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();


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
            #endregion

            #region EmailService

            HostMail? mail = builder.Configuration.GetSection("HostMail").Get<HostMail>();

            if (mail is not null)
                builder.Services.AddSingleton<IEmailSender>(new EmailSender(mail.Mail, mail.Password));

            #endregion


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
