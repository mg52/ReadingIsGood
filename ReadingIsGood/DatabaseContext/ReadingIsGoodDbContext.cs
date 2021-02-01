using Microsoft.EntityFrameworkCore;
using ReadingIsGood.DatabaseContext.EntityConfiguration;
using ReadingIsGood.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReadingIsGood.DatabaseContext
{
    public class ReadingIsGoodDbContext : DbContext
    {
        public ReadingIsGoodDbContext(DbContextOptions<ReadingIsGoodDbContext> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new StockEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
