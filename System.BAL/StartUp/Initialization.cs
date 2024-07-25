using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.DAL.Data;
using System.DAL.Models.Identity;

namespace System.BAL.StartUp
{
    public class Initialization
    {
        private AppDbContext _context;
        private readonly ApplicationUser _user;

        public Initialization(AppDbContext context, ApplicationUser user)
        {
            _context = context;
            _user = user;

        }

        public async Task InitializeAll()
        {
            await InitializeRoles();

            await InitializeUsers();
        }

        public async Task InitializeRoles()
        {

            if (_context is null)
                return;

            string[] roles = new string[] { Roles.SuperAdmin, Roles.Admin, Roles.Manager, Roles.User };

            foreach (string role in roles)
            {
                RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(_context);

                if (!_context.Roles.Any(r => r.Name == role))
                {
                    IdentityRole newRole = new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = role,
                        NormalizedName = role.ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    };

                    await roleStore.CreateAsync(newRole);
                }

                await _context.SaveChangesAsync();
            }

        }

        public async Task InitializeUsers()
        {
            //TODO
            //Get password from json appsettings
            ApplicationUser user = new ApplicationUser
            {
                FirstName = _user.FirstName,
                LastName = _user.LastName,
                Email = _user.Email,
                NormalizedEmail = _user.Email.ToUpper(),
                UserName = _user.UserName,
                NormalizedUserName = _user.UserName.ToUpper(),
                PhoneNumber = _user.PhoneNumber,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")

            };

            if (await _context.Users.AnyAsync(u => u.UserName == user.UserName))
                return;


            PasswordHasher<ApplicationUser> password = new PasswordHasher<ApplicationUser>();
            string hashed = password.HashPassword(user, "7nJ4oq7@f*Dg");
            user.PasswordHash = hashed;

            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(_context);
            var result = userStore.CreateAsync(user);
            _context.SaveChangesAsync().Wait();

            string? role = await _context.Roles.Where(a => a.Name == Roles.SuperAdmin).Select(a => a.Id).FirstOrDefaultAsync();

            if (role is not null)
                await _context.UserRoles.AddAsync(new IdentityUserRole<string> { UserId = user.Id, RoleId = role });



            await _context.SaveChangesAsync();

        }
    }
}
