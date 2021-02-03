using ReadingIsGood.Dtos;
using ReadingIsGood.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Services.OrderService
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrder(OrderCreateDto dto);
        Task<List<OrderDto>> GetOrders(OrderFilterDto dto);
        Task<List<OrderDto>> ExecuteOrders(ExecuteOrderDto dto);
    }
}
