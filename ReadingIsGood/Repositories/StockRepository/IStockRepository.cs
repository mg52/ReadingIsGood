using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadingIsGood.Dtos;
using ReadingIsGood.Entities;

namespace ReadingIsGood.Repositories.StockRepository
{
    public interface IStockRepository
    {
        Task AddAsync(Stock entity);
        Task UpdateAsync(Stock entity);
        Task<List<Stock>> GetAllAsync(StockFilterDto dto);
        Task<Stock> GetByIdAsync(Guid id);
    }
}
