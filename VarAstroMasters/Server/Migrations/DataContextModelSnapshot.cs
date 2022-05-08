﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VarAstroMasters.Server.Data;

#nullable disable

namespace VarAstroMasters.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("VarAstroMasters.Shared.Models.Catalog", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Name");

                    b.ToTable("Catalogs");

                    b.HasData(
                        new
                        {
                            Name = "UCAC4"
                        },
                        new
                        {
                            Name = "2MASS"
                        });
                });

            modelBuilder.Entity("VarAstroMasters.Shared.Models.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("VarAstroMasters.Shared.Models.LightCurve", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DataFileContent")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("DeviceId")
                        .HasColumnType("int");

                    b.Property<string>("ImageFileName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("ObservatoryId")
                        .HasColumnType("int");

                    b.Property<int>("PublishVariant")
                        .HasColumnType("int");

                    b.Property<int>("StarId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("ObservatoryId");

                    b.HasIndex("StarId");

                    b.HasIndex("UserId");

                    b.ToTable("LightCurves");
                });

            modelBuilder.Entity("VarAstroMasters.Shared.Models.Observatory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("decimal(10,8)");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("decimal(10,8)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Observatories");
                });

            modelBuilder.Entity("VarAstroMasters.Shared.Models.Star", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Stars");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "CzeV 343"
                        });
                });

            modelBuilder.Entity("VarAstroMasters.Shared.Models.StarCatalog", b =>
                {
                    b.Property<string>("CatalogId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("StarId")
                        .HasColumnType("int");

                    b.Property<string>("CrossId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Dec")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Mag")
                        .HasColumnType("decimal(10,3)");

                    b.Property<bool>("Primary")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Ra")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CatalogId", "StarId");

                    b.HasIndex("StarId");

                    b.ToTable("StarCatalog");

                    b.HasData(
                        new
                        {
                            CatalogId = "UCAC4",
                            StarId = 1,
                            CrossId = "605-025126",
                            Dec = "+30:57:03.59",
                            Mag = 13.71m,
                            Primary = true,
                            Ra = "05:48:24.012"
                        });
                });

            modelBuilder.Entity("VarAstroMasters.Shared.Models.StarDraft", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Dec")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Ra")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("StarsDrafts");
                });

            modelBuilder.Entity("VarAstroMasters.Shared.Models.StarPublish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Discoverer")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PublicationLink")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("StarId")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StarId")
                        .IsUnique();

                    b.ToTable("StarPublish");
                });

            modelBuilder.Entity("VarAstroMasters.Shared.Models.StarVariability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Epoch")
                        .HasColumnType("decimal(18,9)");

                    b.Property<decimal>("Period")
                        .HasColumnType("decimal(18,9)");

                    b.Property<double>("PrimaryMinimum")
                        .HasColumnType("double");

                    b.Property<int>("StarId")
                        .HasColumnType("int");

                    b.Property<int>("VariabilityType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StarId")
                        .IsUnique();

                    b.ToTable("StarVariability");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Epoch = 2455958.36058m,
                            Period = 1.209373m,
                            PrimaryMinimum = 13.720000000000001,
                            StarId = 1,
                            VariabilityType = 1
                        });
                });

            modelBuilder.Entity("VarAstroMasters.Shared.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("Registered")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("VarAstroMasters.Shared.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("VarAstroMasters.Shared.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VarAstroMasters.Shared.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("VarAstroMasters.Shared.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VarAstroMasters.Shared.Models.Device", b =>
                {
                    b.HasOne("VarAstroMasters.Shared.Models.User", "User")
                        .WithMany("Devices")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("VarAstroMasters.Shared.Models.LightCurve", b =>
                {
                    b.HasOne("VarAstroMasters.Shared.Models.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId");

                    b.HasOne("VarAstroMasters.Shared.Models.Observatory", "Observatory")
                        .WithMany()
                        .HasForeignKey("ObservatoryId");

                    b.HasOne("VarAstroMasters.Shared.Models.Star", "Star")
                        .WithMany("LightCurves")
                        .HasForeignKey("StarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VarAstroMasters.Shared.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("Observatory");

                    b.Navigation("Star");

                    b.Navigation("User");
                });

            modelBuilder.Entity("VarAstroMasters.Shared.Models.Observatory", b =>
                {
                    b.HasOne("VarAstroMasters.Shared.Models.User", null)
                        .WithMany("Observatories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VarAstroMasters.Shared.Models.StarCatalog", b =>
                {
                    b.HasOne("VarAstroMasters.Shared.Models.Star", null)
                        .WithMany("StarCatalogs")
                        .HasForeignKey("StarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VarAstroMasters.Shared.Models.StarDraft", b =>
                {
                    b.HasOne("VarAstroMasters.Shared.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("VarAstroMasters.Shared.Models.StarPublish", b =>
                {
                    b.HasOne("VarAstroMasters.Shared.Models.Star", null)
                        .WithOne("StarPublish")
                        .HasForeignKey("VarAstroMasters.Shared.Models.StarPublish", "StarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VarAstroMasters.Shared.Models.StarVariability", b =>
                {
                    b.HasOne("VarAstroMasters.Shared.Models.Star", null)
                        .WithOne("StarVariability")
                        .HasForeignKey("VarAstroMasters.Shared.Models.StarVariability", "StarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VarAstroMasters.Shared.Models.Star", b =>
                {
                    b.Navigation("LightCurves");

                    b.Navigation("StarCatalogs");

                    b.Navigation("StarPublish")
                        .IsRequired();

                    b.Navigation("StarVariability")
                        .IsRequired();
                });

            modelBuilder.Entity("VarAstroMasters.Shared.Models.User", b =>
                {
                    b.Navigation("Devices");

                    b.Navigation("Observatories");
                });
#pragma warning restore 612, 618
        }
    }
}
