using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ReadingIsGood.Entities;
using ReadingIsGood.Helpers;
using BC = BCrypt.Net.BCrypt;
using ReadingIsGood.Dtos;
using Mapster;
using System.Threading.Tasks;
using ReadingIsGood.Repositories.CustomerRepository;
using ReadingIsGood.Services.OrderService;
using ReadingIsGood.Repositories.OrderRepository;
using ReadingIsGood.Repositories.StockRepository;
using ReadingIsGood.Services.StockService;
using ReadingIsGood.Services.ProductService;
using ReadingIsGood.Services.OrderItemService;

namespace ReadingIsGood.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _OrderRepository;
        private readonly IStockService _stockService;
        private readonly IProductService _productService;
        private readonly IOrderItemService _orderItemService;

        
        public OrderService(IOrderRepository OrderRepository, IStockService stockService, IProductService productService, IOrderItemService orderItemService)
        {
            _OrderRepository = OrderRepository;
            _stockService = stockService;
            _productService = productService;
            _orderItemService = orderItemService;
        }

        public async Task<OrderDto> CreateOrder(OrderCreateDto dto)
        {
            var decreaseStockDto = new List<StockDecreaseDto>();
            foreach (var orderItem in dto.OrderItems)
            {
                var stockDto = (await _stockService.GetStocks(new StockFilterDto() { ProductId = orderItem.ProductId })).FirstOrDefault();
                if(stockDto == null)
                {
                    //cannot find stock record for this product.
                    return null;
                }
                if(orderItem.Quantity > stockDto.Quantity)
                {
                    //order quantity exceeds stock quantity.
                    return null;
                }
                decreaseStockDto.Add(new StockDecreaseDto() { Id = stockDto.Id, Value = orderItem.Quantity });
            }

            var retVal = await _stockService.DecreaseStock(decreaseStockDto);

            if(retVal == null)
            {
                return null;
            }

            var order = dto.Adapt<Order>();
            order.Status = true;
            order.OrderDateTime = DateTime.UtcNow;

            await _OrderRepository.AddAsync(order);

            var adapted = order.Adapt<OrderDto>();
            return adapted;
        }

        public async Task<List<OrderDto>> GetOrders(OrderFilterDto dto)
        {
            var Orders = await _OrderRepository.GetAllAsync(dto);
            return Orders.Adapt<List<OrderDto>>();
        }

    }
}