﻿// <auto-generated />
using System;
using HiFi.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HiFi.Data.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20190815181341_ShoppingCartTableRelationWithProduct")]
    partial class ShoppingCartTableRelationWithProduct
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HiFi.Data.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address1")
                        .IsRequired();

                    b.Property<string>("Address2");

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Company");

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOnUtc");

                    b.Property<string>("CustomAttributes");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FaxNumber");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.Property<string>("StateOrProvince");

                    b.Property<string>("ZipPostalCode")
                        .IsRequired();

                    b.HasKey("AddressId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("HiFi.Data.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .IsRequired();

                    b.Property<string>("CreatedByUserId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("DisplayOrder");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("CategoryId");

                    b.HasIndex("CreatedByUserId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("HiFi.Data.Models.OrderDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count");

                    b.Property<string>("Description");

                    b.Property<int>("OrderId");

                    b.Property<double>("Price");

                    b.Property<int>("ProductId");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("HiFi.Data.Models.OrderHeader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("AddressLine2")
                        .HasMaxLength(255);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Comments");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("CreatedByUserId");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("OrderPlacedTime");

                    b.Property<decimal>("OrderTotal");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Status");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("HiFi.Data.Models.PictureBinary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("BinaryData");

                    b.Property<int>("PictureId");

                    b.HasKey("Id");

                    b.ToTable("PictureBinary");
                });

            modelBuilder.Entity("HiFi.Data.Models.Product", b =>
                {
                    b.Property<int>("PKProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedByUserId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("DisplayOrder");

                    b.Property<bool>("IsActive");

                    b.Property<decimal>("Price");

                    b.Property<string>("ProductName")
                        .IsRequired();

                    b.Property<int>("Quantity");

                    b.Property<string>("ShortDescription")
                        .IsRequired();

                    b.Property<int>("SubCategoryOneId");

                    b.Property<string>("UpdatedByUserId");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("PKProductId");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("SubCategoryOneId");

                    b.HasIndex("UpdatedByUserId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("HiFi.Data.Models.ProductImage", b =>
                {
                    b.Property<int>("PKImageId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("FKProductId");

                    b.Property<string>("ImageName")
                        .IsRequired();

                    b.Property<string>("ImagePath");

                    b.Property<bool>("IsMainImage");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("PKImageId");

                    b.HasIndex("FKProductId");

                    b.ToTable("ProductImage");
                });

            modelBuilder.Entity("HiFi.Data.Models.ShoppingCartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationUserId");

                    b.Property<int>("ProductId");

                    b.Property<int>("Qunatity");

                    b.Property<string>("ShoppingCartId")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("ProductId");

                    b.ToTable("ShoppingCartItems");
                });

            modelBuilder.Entity("HiFi.Data.Models.SubCategoryOne", b =>
                {
                    b.Property<int>("SubCategoryOneId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<string>("CreatedByUserId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("DisplayOrder");

                    b.Property<bool>("IsActive");

                    b.Property<string>("SC_ImageName")
                        .IsRequired();

                    b.Property<string>("SC_ImagePath");

                    b.Property<string>("SubCategoryName")
                        .IsRequired();

                    b.Property<string>("UpdatedByUserId");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("SubCategoryOneId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("UpdatedByUserId");

                    b.ToTable("SubCategoryOne");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("HiFi.Data.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<int?>("AddressId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasIndex("AddressId");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("HiFi.Data.Models.Category", b =>
                {
                    b.HasOne("HiFi.Data.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");
                });

            modelBuilder.Entity("HiFi.Data.Models.OrderDetails", b =>
                {
                    b.HasOne("HiFi.Data.Models.OrderHeader", "OrderHeader")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HiFi.Data.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HiFi.Data.Models.OrderHeader", b =>
                {
                    b.HasOne("HiFi.Data.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");
                });

            modelBuilder.Entity("HiFi.Data.Models.Product", b =>
                {
                    b.HasOne("HiFi.Data.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("HiFi.Data.Models.SubCategoryOne", "SubCategoryOne")
                        .WithMany()
                        .HasForeignKey("SubCategoryOneId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HiFi.Data.Models.ApplicationUser", "ApplicationUser1")
                        .WithMany()
                        .HasForeignKey("UpdatedByUserId");
                });

            modelBuilder.Entity("HiFi.Data.Models.ProductImage", b =>
                {
                    b.HasOne("HiFi.Data.Models.Product", "Product")
                        .WithMany("ProductImage")
                        .HasForeignKey("FKProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HiFi.Data.Models.ShoppingCartItem", b =>
                {
                    b.HasOne("HiFi.Data.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("HiFi.Data.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HiFi.Data.Models.SubCategoryOne", b =>
                {
                    b.HasOne("HiFi.Data.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HiFi.Data.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("HiFi.Data.Models.ApplicationUser", "ApplicationUser1")
                        .WithMany()
                        .HasForeignKey("UpdatedByUserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HiFi.Data.Models.ApplicationUser", b =>
                {
                    b.HasOne("HiFi.Data.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");
                });
#pragma warning restore 612, 618
        }
    }
}
