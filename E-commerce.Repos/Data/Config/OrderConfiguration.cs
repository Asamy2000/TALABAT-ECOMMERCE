using E_commerce.Core.Entities.Order_Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Repos.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //cols of Address class will be in Order table
            builder.OwnsOne(O => O.ShippingAddress, ShippingAddress => ShippingAddress.WithOwner());
            //string in the DB and OrderStatus in the app
            builder.Property(O => O.Status)
                .HasConversion(OStatus => OStatus.ToString(),
                               OStatus =>(OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus.ToString()));

            builder.Property(O => O.SubTotal)
                .HasColumnType("decimal(18,2)");

            builder.HasMany(O => O.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
