using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.DAL.Data;
using System.DAL.Models.Identity;

namespace System.BAL.StartUp
{
    public class Initialization
    {
        private AppDbContext _context;
        private readonly ApplicationUser _user;
        private readonly string _userPassword;

        public Initialization(AppDbContext context, ApplicationUser user, string userPassword)
        {
            _context = context;
            _user = user;
            _userPassword = userPassword;
        }

        public void InitializeRoles()
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

                    roleStore.CreateAsync(newRole).Wait();
                }

                _context.SaveChanges();
            }

        }

        public void InitializeUsers()
        {
            ApplicationUser user = new ApplicationUser
            {
                Email = _user.Email,
                NormalizedEmail = _user.Email.ToUpper(),
                UserName = _user.UserName,
                NormalizedUserName = _user.UserName.ToUpper(),
                PhoneNumber = _user.PhoneNumber,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")

            };

            if (_context.Users.Any(u => u.UserName == user.UserName))
                return;


            PasswordHasher<ApplicationUser> password = new PasswordHasher<ApplicationUser>();

            string hashed = password.HashPassword(user, _userPassword is null ? "7nJ4oq7@f*Dg" : _userPassword);
            user.PasswordHash = hashed;

            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(_context);
            userStore.CreateAsync(user).Wait();
            _context.SaveChanges();

            string? role = _context.Roles.Where(a => a.Name == Roles.SuperAdmin).Select(a => a.Id).FirstOrDefault();

            if (role is not null)
                _context.UserRoles.Add(new IdentityUserRole<string> { UserId = user.Id, RoleId = role });

            _context.SaveChanges();

        }
    }
}
