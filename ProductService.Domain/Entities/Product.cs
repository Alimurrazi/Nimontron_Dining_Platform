using ProductService.Domain.Enums;
using ProductService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Money Price { get; private set; }
        public ProductStatus Status { get; private set; }

        public Product(string name, Money price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Status = ProductStatus.Active;
        }

        public void Deactivate()
        {
            Status = ProductStatus.Inactive;
        }
    }
}
