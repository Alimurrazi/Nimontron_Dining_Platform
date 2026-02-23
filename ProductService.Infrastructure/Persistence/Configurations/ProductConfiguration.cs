using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.OwnsOne(p => p.Price, price =>
            {
                price.Property(p => p.Amount).HasColumnName("Price");
            });

            builder.Property(p => p.Status)
                .IsRequired();
        }
    }
}
