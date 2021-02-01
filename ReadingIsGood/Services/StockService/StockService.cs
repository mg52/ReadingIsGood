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
using ReadingIsGood.Services.StockService;
using ReadingIsGood.Repositories.StockRepository;

namespace ReadingIsGood.Services.StockService
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;

        
        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<StockDto> CreateStock(StockCreateDto dto)
        {
            var stock = await _stockRepository.GetAllAsync(new StockFilterDto() { ProductId = dto.ProductId });
            if (stock?.FirstOrDefault() != null)
            {
                return null;
            }

            var entity = dto.Adapt<Stock>();

            await _stockRepository.AddAsync(entity);

            var result = entity.Adapt<StockDto>();

            return result;
        }

        public async Task<List<StockDto>> DecreaseStock(List<StockDecreaseDto> stockDecreaseDtoList)
        {
            var returnDto = new List<StockDto>();
            foreach (var stockDecreaseDto in stockDecreaseDtoList)
            {
                var stock = await _stockRepository.GetByIdAsync(stockDecreaseDto.Id);
                if (stock == null)
                {
                    //throw new Exception("Cannot find stock record.");
                    return null;
                }
                if (stockDecreaseDto.Value > stock.Quantity)
                {
                    //throw new Exception("Order quantity exceeds stock quantity.");
                    return null;
                }

                stock.Quantity -= stockDecreaseDto.Value;

                await _stockRepository.UpdateAsync(stock);

                returnDto.Add(stock.Adapt<StockDto>());
            }
            return returnDto;
        }

        public async Task<List<StockDto>> GetStocks(StockFilterDto dto)
        {
            var stocks = await _stockRepository.GetAllAsync(dto);
            return stocks.Adapt<List<StockDto>>();
        }

        public async Task<StockDto> UpdateStock(StockUpdateDto dto)
        {
            var stock = await _stockRepository.GetByIdAsync(dto.Id);
            if(stock == null)
            {
                return null;
            }
            stock.Quantity += dto.Value;

            await _stockRepository.UpdateAsync(stock);

            var result = stock.Adapt<StockDto>();
            return result;
        }
    }
}