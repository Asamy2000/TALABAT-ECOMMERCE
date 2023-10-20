using E_commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace E_commerce.Repos.Data.Config
{
    public class BrandConfigurations : IEntityTypeConfiguration<ProductBrand>
    {
        public void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            builder.Property(B => B.Name).IsRequired();
            builder.HasIndex(B => B.Name).IsUnique();
        }
    }
}
