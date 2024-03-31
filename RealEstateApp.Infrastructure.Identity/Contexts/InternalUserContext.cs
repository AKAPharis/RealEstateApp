using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Infrastructure.Identity.Models;

namespace RealEstateApp.Infrastructure.Identity.Contexts
{
    public class InternalUserContext : IdentityDbContext<InternalUser>
    {
        public InternalUserContext(DbContextOptions<InternalUserContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FLUENT API
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<InternalUser>().ToTable("InternalUsers");
            modelBuilder.Entity<IdentityRole>().ToTable("InternalRoles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("InternalUserRoles");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("InternalUserLogins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("InternalRoleClaims");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("AspNetInternalUserClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("AspNetInternalUserTokens");




        }

    }
}
