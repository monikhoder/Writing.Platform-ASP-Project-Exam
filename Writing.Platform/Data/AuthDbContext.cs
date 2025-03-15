using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Writing.Platform.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Create Role (Admin, Author, User)
            var AdminRoleId = "fa582db7-3a42-48ba-96e9-5335cadf6dd5";
            var AuthorRoleId = "904ff30d-a9ab-4f8f-9358-798c0e97bd1f";
            var UserRoleId = "9366c526-1cb9-49e5-be7f-674a0a5db4e3";
            var roles = new List<IdentityRole>
            {
                new IdentityRole { Name = "Admin", NormalizedName = "Admin", Id =  AdminRoleId, ConcurrencyStamp = AdminRoleId},
                new IdentityRole { Name = "Author", NormalizedName = "Author", Id = AuthorRoleId, ConcurrencyStamp = AuthorRoleId },
                new IdentityRole { Name = "User", NormalizedName = "User", Id = UserRoleId, ConcurrencyStamp = UserRoleId }
            };
            builder.Entity<IdentityRole>().HasData(roles);

            //Create Admin User
            var AdminId = "b1b3b3b3-3b3b-3b3b-3b3b-3b3b3b3b3b3b";
            var AdminUser = new IdentityUser
            {
                Id = AdminId,
                UserName = "admin",
                NormalizedUserName = "admin".ToUpper(),
                Email = "admin@writing.platform",
                NormalizedEmail = "admin@writing.platform".ToUpper(),
            };
            AdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(AdminUser, "Admin123Password");
            builder.Entity<IdentityUser>().HasData(AdminUser);

            //Add all role to Admin User
            var AdminRole = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string> { UserId = AdminId, RoleId = AdminRoleId },
                new IdentityUserRole<string> { UserId = AdminId, RoleId = AuthorRoleId },
                new IdentityUserRole<string> { UserId = AdminId, RoleId = UserRoleId }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(AdminRole);
        }
    }
}
