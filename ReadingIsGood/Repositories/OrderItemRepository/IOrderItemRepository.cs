using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadingIsGood.Dtos;
using ReadingIsGood.Entities;

namespace ReadingIsGood.Repositories.OrderItemRepository
{
    public interface IOrderItemRepository
    {
        Task AddAsync(OrderItem entity);
        Task UpdateAsync(OrderItem entity);
        Task<List<OrderItem>> GetAllAsync(OrderItemFilterDto dto);
        Task<OrderItem> GetByIdAsync(Guid id);
    }
}
