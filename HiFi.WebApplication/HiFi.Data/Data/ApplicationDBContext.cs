using HiFi.Common;
using HiFi.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiFi.Data.Data
{
    public class ApplicationDBContext: IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            #region To Modify default Identity tables Names.
            //builder.Entity<ApplicationUser>().ToTable("ApplicationUser"); //ToTable("MyUserTable")
            #endregion
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategoryOne> SubCategoryOne { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<PictureBinary> PictureBinary { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<OrderHeader> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbQuery<CategoryChildsCount> CategoryChildsCounts { get; set; }
        public DbQuery<CategorySubCategoryTable> CategorySubCategoryTable { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Product_Manufacturer_Mapping> Product_Manufacturer_Mapping { get; set; }
    }
}
