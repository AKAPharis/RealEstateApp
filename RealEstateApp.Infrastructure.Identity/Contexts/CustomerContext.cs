using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Infrastructure.Identity.Models;

namespace RealEstateApp.Infrastructure.Identity.Contexts
{
    public class CustomerContext : IdentityDbContext<Customer>
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options) { }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FLUENT API
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<IdentityRole>().ToTable("CustomerRoles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("CustomerUserRoles");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("CustomerUserLogins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("CustomerRoleClaims");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("AspNetCustomerClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("AspNetCustomerTokens");





        }

    }
}
