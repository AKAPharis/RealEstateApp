using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RealEstateApp.Core.Domain.Common;
using RealEstateApp.Core.Domain.Models;
using System.Data.Common;

namespace RealEstateApp.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<RealEstateProperty> Properties { get; set; }
        public DbSet<TypeOfSale> TypeOfSales { get; set; }
        public DbSet<TypeOfProperty> TypeOfProperties { get; set; }
        public DbSet<Upgrade> Upgrades { get; set; }
        public DbSet<PropertyUpgrade> PropertyUpgrades { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Tables and keys
            modelBuilder.Entity<RealEstateProperty>(opt =>
            {
                opt.ToTable("Properties");
                opt.HasKey(x => x.Id);
            });
            modelBuilder.Entity<TypeOfSale>(opt =>
            {
                opt.ToTable("TypeOfSales");
                opt.HasKey(x => x.Id);
            });
            modelBuilder.Entity<TypeOfProperty>(opt =>
            {
                opt.ToTable("TypeOfProperties");
                opt.HasKey(x => x.Id);
            });
            modelBuilder.Entity<Upgrade>(opt =>
            {
                opt.ToTable("Upgrades");
                opt.HasKey(x => x.Id);
            });
            modelBuilder.Entity<PropertyUpgrade>(opt =>
            {
                opt.ToTable("PropertyUpgrades");
                opt.HasKey(x => new { x.PropertyId, x.UpgradeId});
            });
            #endregion

            #region Relationships

            //RealStateProperty - PropertyUpgrade
            modelBuilder.Entity<RealEstateProperty>()
                .HasMany(x => x.Upgrades)
                .WithOne(x => x.Property)
                .HasForeignKey(x => x.PropertyId)
                .HasConstraintName("FK_RealEstateProperty_PropertyUpgrade");
            //RealStateProperty - TypeOfProperty
            modelBuilder.Entity<RealEstateProperty>()
                .HasOne(x => x.TypeProperty)
                .WithMany(x => x.Properties)
                .HasForeignKey(x => x.TypePropertyId)
                .HasConstraintName("FK_RealEstateProperty_TypeProperty");
            //RealStateProperty - TypeOfSale
            modelBuilder.Entity<RealEstateProperty>()
                .HasOne(x => x.TypeOfSale)
                .WithMany(x => x.Properties)
                .HasForeignKey(x => x.TypeOfSaleId)
                .HasConstraintName("FK_RealEstateProperty_TypeOfSale");
            //Upgrade - PropertyUpgrade
            modelBuilder.Entity<Upgrade>()
                .HasMany(x => x.Properties)
                .WithOne(x => x.Upgrade)
                .HasForeignKey(x => x.UpgradeId)
                .HasConstraintName("FK_Upgrade_PropertyUpgrade");

            #endregion





        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.Updated = DateTime.Now;
                        break; 
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
