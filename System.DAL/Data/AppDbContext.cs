using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.DAL.Models;
using System.DAL.Models.Identity;

namespace System.DAL.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<DeviceCategory> DeviceCategories { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Sponsorship> Sponsorships { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<DeviceSpecifications> DeviceSpecifications { get; set; }
        public DbSet<Activity> Activities { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Device>()
                .HasOne(d => d.Category)
                .WithMany(c => c.Devices)
                .HasForeignKey(d => d.DeviceCategory);

            builder.Entity<Device>()
                .HasOne(d => d.Specification)
                .WithMany()
                .HasForeignKey(d => d.DeviceSpecification);

            builder.Entity<Sponsorship>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId);

            builder.Entity<Sponsorship>()
                .HasOne(s => s.Device)
                .WithMany()
                .HasForeignKey(s => s.DeviceId);

            builder.Entity<Sponsorship>()
                .HasOne(s => s.Location)
                .WithMany()
                .HasForeignKey(s => s.LocationId);


            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO : move connection string into presentation layer
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-T4OMHBE\\SQLEXPRESS;Initial Catalog=CDMS; Integrated Security = SSPI ; TrustServerCertificate = True");

            base.OnConfiguring(optionsBuilder);
        }

    }
}
