﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealEstateApp.Infrastructure.Persistence.Contexts;

#nullable disable

namespace RealEstateApp.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240418022917_AddingAddressProperty")]
    partial class AddingAddressProperty
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RealEstateApp.Core.Domain.Models.FavoriteProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.ToTable("FavoriteProperties", (string)null);
                });

            modelBuilder.Entity("RealEstateApp.Core.Domain.Models.PropertyImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.ToTable("PropertyImages", (string)null);
                });

            modelBuilder.Entity("RealEstateApp.Core.Domain.Models.PropertyUpgrade", b =>
                {
                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<int>("UpgradeId")
                        .HasColumnType("int");

                    b.HasKey("PropertyId", "UpgradeId");

                    b.HasIndex("UpgradeId");

                    b.ToTable("PropertyUpgrades", (string)null);
                });

            modelBuilder.Entity("RealEstateApp.Core.Domain.Models.RealEstateProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AgentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AgentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Guid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfBathrooms")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfBedrooms")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Size")
                        .HasColumnType("float");

                    b.Property<int>("TypeOfSaleId")
                        .HasColumnType("int");

                    b.Property<int>("TypePropertyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TypeOfSaleId");

                    b.HasIndex("TypePropertyId");

                    b.ToTable("Properties", (string)null);
                });

            modelBuilder.Entity("RealEstateApp.Core.Domain.Models.TypeOfProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("TypeOfProperties", (string)null);
                });

            modelBuilder.Entity("RealEstateApp.Core.Domain.Models.TypeOfSale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("TypeOfSales", (string)null);
                });

            modelBuilder.Entity("RealEstateApp.Core.Domain.Models.Upgrade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Upgrades", (string)null);
                });

            modelBuilder.Entity("RealEstateApp.Core.Domain.Models.FavoriteProperty", b =>
                {
                    b.HasOne("RealEstateApp.Core.Domain.Models.RealEstateProperty", "Property")
                        .WithMany("FavoriteProperties")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_FavoriteProperties_RealEstateProperty");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("RealEstateApp.Core.Domain.Models.PropertyImage", b =>
                {
                    b.HasOne("RealEstateApp.Core.Domain.Models.RealEstateProperty", "Property")
                        .WithMany("Images")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_RealEstateProperty_ImageProperty");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("RealEstateApp.Core.Domain.Models.PropertyUpgrade", b =>
                {
                    b.HasOne("RealEstateApp.Core.Domain.Models.RealEstateProperty", "Property")
                        .WithMany("Upgrades")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_RealEstateProperty_PropertyUpgrade");

                    b.HasOne("RealEstateApp.Core.Domain.Models.Upgrade", "Upgrade")
                        .WithMany("Properties")
                        .HasForeignKey("UpgradeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_Upgrade_PropertyUpgrade");

                    b.Navigation("Property");

                    b.Navigation("Upgrade");
                });

            modelBuilder.Entity("RealEstateApp.Core.Domain.Models.RealEstateProperty", b =>
                {
                    b.HasOne("RealEstateApp.Core.Domain.Models.TypeOfSale", "TypeOfSale")
                        .WithMany("Properties")
                        .HasForeignKey("TypeOfSaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_RealEstateProperty_TypeOfSale");

                    b.HasOne("RealEstateApp.Core.Domain.Models.TypeOfProperty", "TypeProperty")
                        .WithMany("Properties")
                        .HasForeignKey("TypePropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_RealEstateProperty_TypeProperty");

                    b.Navigation("TypeOfSale");

                    b.Navigation("TypeProperty");
                });

            modelBuilder.Entity("RealEstateApp.Core.Domain.Models.RealEstateProperty", b =>
                {
                    b.Navigation("FavoriteProperties");

                    b.Navigation("Images");

                    b.Navigation("Upgrades");
                });

            modelBuilder.Entity("RealEstateApp.Core.Domain.Models.TypeOfProperty", b =>
                {
                    b.Navigation("Properties");
                });

            modelBuilder.Entity("RealEstateApp.Core.Domain.Models.TypeOfSale", b =>
                {
                    b.Navigation("Properties");
                });

            modelBuilder.Entity("RealEstateApp.Core.Domain.Models.Upgrade", b =>
                {
                    b.Navigation("Properties");
                });
#pragma warning restore 612, 618
        }
    }
}
