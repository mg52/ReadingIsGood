using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadingIsGood.DatabaseContext;
using ReadingIsGood.Dtos;
using ReadingIsGood.Entities;
using Microsoft.EntityFrameworkCore;

namespace ReadingIsGood.Repositories.CustomerRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ReadingIsGoodDbContext _ctx;

        public CustomerRepository(ReadingIsGoodDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddAsync(Customer entity)
        {
            _ctx.Customers.Add(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(Customer entity)
        {
            _ctx.Customers.Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            var result = await _ctx.Customers
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<Customer>> GetAllAsync(CustomerFilterDto dto)
        {
            var query = _ctx.Customers.Where(x => true);

            if (!string.IsNullOrEmpty(dto.Username))
            {
                query = query.Where(x => x.Username == dto.Username);
            }

            var returnValue = await query.ToListAsync();

            return returnValue;
        }

        public async Task UpdateAsync(Customer entity)
        {
            _ctx.Customers.Update(entity);
            await _ctx.SaveChangesAsync();
        }
    }
}
