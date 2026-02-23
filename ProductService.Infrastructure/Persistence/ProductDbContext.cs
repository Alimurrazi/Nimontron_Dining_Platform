using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.Infrastructure.Persistence
{
    public class ProductDbContext: DbContext
    {
        public ProductDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products {  get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
        }
    }
}
