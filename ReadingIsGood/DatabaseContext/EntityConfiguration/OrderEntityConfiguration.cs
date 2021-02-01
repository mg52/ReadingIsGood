using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadingIsGood.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.DatabaseContext.EntityConfiguration
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasMany(x => x.OrderItems).WithOne(xx => xx.Order);
            builder.HasOne(k => k.Customer).WithMany(k => k.Orders).HasForeignKey(k => k.CustomerId);
        }
    }
}
