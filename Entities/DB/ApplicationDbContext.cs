﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DB
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Products>().ToTable("Products");

            modelBuilder.Entity<Products>().HasData(new Entities.Products() { ProductID = 1001, ProductName = "TestProduct", Description = "Test Description", ProductPrice = 999, ProductSalePrice = 699, Stock = 100, SkuID = "Test100ML", ShortDescription = "TestProduct Short Description" });
          
        }

       
    }
}
