using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadingIsGood.Dtos;
using ReadingIsGood.Entities;

namespace ReadingIsGood.Repositories.OrderRepository
{
    public interface IOrderRepository
    {
        Task AddAsync(Order entity);
        Task UpdateAsync(Order entity);
        Task<List<Order>> GetAllAsync(OrderFilterDto dto);
        Task<Order> GetByIdAsync(Guid id);
    }
}
