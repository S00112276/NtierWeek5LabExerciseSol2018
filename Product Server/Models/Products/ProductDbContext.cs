using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Product_Server.Models.Products
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext() : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}