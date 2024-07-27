using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace System.DAL.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-T4OMHBE\\SQLEXPRESS;Initial Catalog=CDMS; Integrated Security = SSPI ; TrustServerCertificate = True");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
