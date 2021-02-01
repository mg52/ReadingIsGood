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
using ReadingIsGood.Services.OrderItemService;
using ReadingIsGood.Repositories.OrderItemRepository;
using ReadingIsGood.Repositories.StockRepository;
using ReadingIsGood.Services.StockService;
using ReadingIsGood.Services.ProductService;

namespace ReadingIsGood.Services.OrderItemService
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _OrderItemRepository;
        private readonly IStockService _stockService;
        private readonly IProductService _productService;

        
        public OrderItemService(IOrderItemRepository OrderItemRepository, IStockService stockService, IProductService productService)
        {
            _OrderItemRepository = OrderItemRepository;
            _stockService = stockService;
            _productService = productService;
        }

        //public async Task<OrderItemDto> CreateOrderItem(OrderItemCreateDto dto)
        //{
        //    var stockDto = await _stockService.DecreaseStock(new StockDecreaseDto() { ProductId = dto.ProductId , Value = dto.Quantity});
            
        //    if(stockDto == null)
        //    {
        //        //throw new Exception("Cannot decrase quantity from stock.");
        //        return null;
        //    }

        //    var order = dto.Adapt<OrderItem>();

        //    await _OrderItemRepository.AddAsync(order);

        //    var adapted = order.Adapt<OrderItemDto>();
        //    return adapted;
        //}

        //public async Task<List<OrderItemDto>> GetOrderItems(OrderItemFilterDto dto)
        //{
        //    var OrderItems = await _OrderItemRepository.GetAllAsync(dto);
        //    return OrderItems.Adapt<List<OrderItemDto>>();
        //}

    }
}