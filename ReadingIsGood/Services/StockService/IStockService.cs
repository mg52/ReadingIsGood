using ReadingIsGood.Dtos;
using ReadingIsGood.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Services.StockService
{
    public interface IStockService
    {
        Task<StockDto> CreateStock(StockCreateDto dto);
        Task<StockDto> UpdateStock(StockUpdateDto dto);
        Task<List<StockDto>> DecreaseStock(List<StockDecreaseDto> stockDecreaseDtoList);
        Task<List<StockDto>> GetStocks(StockFilterDto dto);
    }
}
