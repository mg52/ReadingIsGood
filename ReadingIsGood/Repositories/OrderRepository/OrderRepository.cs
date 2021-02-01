using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadingIsGood.DatabaseContext;
using ReadingIsGood.Dtos;
using ReadingIsGood.Entities;
using Microsoft.EntityFrameworkCore;

namespace ReadingIsGood.Repositories.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ReadingIsGoodDbContext _ctx;

        public OrderRepository(ReadingIsGoodDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddAsync(Order entity)
        {
            _ctx.Orders.Add(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order entity)
        {
            _ctx.Orders.Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            var result = await _ctx.Orders.Include(x => x.OrderItems)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<Order>> GetAllAsync(OrderFilterDto dto)
        {
            var query = _ctx.Orders.Include(x => x.OrderItems).ThenInclude(x => x.Product).Where(x => true);

            if (dto.CustomerId != null && dto.CustomerId != Guid.Empty)
            {
                query = query.Where(x => x.CustomerId == dto.CustomerId);
            }

            var returnValue = await query.ToListAsync();

            return returnValue;
        }

        public async Task UpdateAsync(Order entity)
        {
            _ctx.Orders.Update(entity);
            await _ctx.SaveChangesAsync();
        }
    }
}
