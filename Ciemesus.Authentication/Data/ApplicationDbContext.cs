using Ciemesus.Core.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ciemesus.Authentication.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users", "dbo");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "dbo");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "dbo");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "dbo");
            builder.Entity<IdentityRole>().ToTable("Roles", "dbo");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "dbo");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "dbo");
        }
    }
}
