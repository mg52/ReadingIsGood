using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadingIsGood.DatabaseContext;
using ReadingIsGood.Dtos;
using ReadingIsGood.Entities;
using Microsoft.EntityFrameworkCore;

namespace ReadingIsGood.Repositories.OrderItemRepository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ReadingIsGoodDbContext _ctx;

        public OrderItemRepository(ReadingIsGoodDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddAsync(OrderItem entity)
        {
            _ctx.OrderItems.Add(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(OrderItem entity)
        {
            _ctx.OrderItems.Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<OrderItem> GetByIdAsync(Guid id)
        {
            var result = await _ctx.OrderItems
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<OrderItem>> GetAllAsync(OrderItemFilterDto dto)
        {
            var query = _ctx.OrderItems.Where(x => true);

            if (dto.OrderId != null && dto.OrderId != Guid.Empty)
            {
                query = query.Where(x => x.OrderId == dto.OrderId);
            }
            if (dto.ProductId != null && dto.ProductId != Guid.Empty)
            {
                query = query.Where(x => x.ProductId == dto.ProductId);
            }
            var returnValue = await query.ToListAsync();

            return returnValue;
        }

        public async Task UpdateAsync(OrderItem entity)
        {
            _ctx.OrderItems.Update(entity);
            await _ctx.SaveChangesAsync();
        }
    }
}
