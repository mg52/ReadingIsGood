using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadingIsGood.DatabaseContext;
using ReadingIsGood.Dtos;
using ReadingIsGood.Entities;
using Microsoft.EntityFrameworkCore;

namespace ReadingIsGood.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ReadingIsGoodDbContext _ctx;

        public ProductRepository(ReadingIsGoodDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddAsync(Product entity)
        {
            _ctx.Products.Add(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product entity)
        {
            _ctx.Products.Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var result = await _ctx.Products
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<Product>> GetAllAsync(ProductFilterDto dto)
        {
            var query = _ctx.Products.Where(x => true);

            if (dto.BookCode != null)
            {
                query = query.Where(x => x.BookCode == dto.BookCode);
            }
            if (!string.IsNullOrEmpty(dto.BookName))
            {
                query = query.Where(x => x.BookName == dto.BookName);
            }

            var returnValue = await query.ToListAsync();

            return returnValue;
        }

        public async Task UpdateAsync(Product entity)
        {
            _ctx.Products.Update(entity);
            await _ctx.SaveChangesAsync();
        }
    }
}
