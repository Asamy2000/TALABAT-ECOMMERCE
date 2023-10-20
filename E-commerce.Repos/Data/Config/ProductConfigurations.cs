using E_commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace E_commerce.Repos.Data.Config
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //with one brand --> many products
            builder.HasOne(p => p.Brand).WithMany().HasForeignKey(p => p.ProductBrandId);
            //with one type --> many products
            builder.HasOne(P => P.ProductType).WithMany().HasForeignKey(p => p.ProductTypeId);
            //configurations for other attributes
            builder.Property(p => p.Name).HasMaxLength(100);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        }
    }
}
