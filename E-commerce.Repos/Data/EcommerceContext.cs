using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce.Core.Entities;
using E_commerce.Repos.Data.Config;
using System.Reflection;
using E_commerce.Core.Entities.Order_Aggregate;

namespace E_commerce.Repos.Data
{
    public class EcommerceContext : DbContext
    {
        //DbContextOptions of my DbContext chain on ctor of parent
        public EcommerceContext(DbContextOptions<EcommerceContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration(new ProductConfigurations());

            //apply all configurations for the project
            //[reflection] you can dynamically discover information about types (classes, interfaces, structs), methods, fields, properties, events, and more within an assembly.
            // get all classes which implements IEntityTypeConfigurations
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Product> products { get; set; }
        public DbSet<ProductBrand> productBrands { get; set; }
        public DbSet<ProductType> productTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    }
}
