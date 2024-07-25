using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.DAL.Models.Identity;

namespace System.DAL.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO : move connection string into presentation layer
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-T4OMHBE\\SQLEXPRESS;Initial Catalog=N-TierDB; Integrated Security = SSPI ; TrustServerCertificate = True");

            base.OnConfiguring(optionsBuilder);
        }

    }
}
