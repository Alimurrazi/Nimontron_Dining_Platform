using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;
using ProductService.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext context;

        public ProductRepository(ProductDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Product product)
        {
           await context.Products.AddAsync(product);
        }

        public async Task<List<Product>> GetAllAsync()
        {
           return await context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await context.Products.FindAsync(id);
        }

        public async Task SaveChangeAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }

    }
}
