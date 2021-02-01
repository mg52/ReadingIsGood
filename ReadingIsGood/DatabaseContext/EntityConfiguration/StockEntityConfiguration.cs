using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadingIsGood.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.DatabaseContext.EntityConfiguration
{
    public class StockEntityConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasOne(k => k.Product).WithOne(k => k.Stock).HasForeignKey<Stock>(k => k.ProductId);
        }
    }
}
