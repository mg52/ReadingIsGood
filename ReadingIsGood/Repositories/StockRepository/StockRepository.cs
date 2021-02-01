using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadingIsGood.DatabaseContext;
using ReadingIsGood.Dtos;
using ReadingIsGood.Entities;
using Microsoft.EntityFrameworkCore;

namespace ReadingIsGood.Repositories.StockRepository
{
    public class StockRepository : IStockRepository
    {
        private readonly ReadingIsGoodDbContext _ctx;

        public StockRepository(ReadingIsGoodDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddAsync(Stock entity)
        {
            _ctx.Stocks.Add(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(Stock entity)
        {
            _ctx.Stocks.Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Stock> GetByIdAsync(Guid id)
        {
            var result = await _ctx.Stocks
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<Stock>> GetAllAsync(StockFilterDto dto)
        {
            var query = _ctx.Stocks.Include(x => x.Product).Where(x => true);

            if (dto.ProductId != null && dto.ProductId != Guid.Empty)
            {
                query = query.Where(x => x.ProductId == dto.ProductId);
            }

            var returnValue = await query.ToListAsync();

            return returnValue;
        }

        public async Task UpdateAsync(Stock entity)
        {
            _ctx.Stocks.Update(entity);
            await _ctx.SaveChangesAsync();
        }
    }
}
