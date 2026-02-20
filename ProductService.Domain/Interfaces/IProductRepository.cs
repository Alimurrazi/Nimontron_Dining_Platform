using ProductService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync();
        Task<List<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
    }
}
