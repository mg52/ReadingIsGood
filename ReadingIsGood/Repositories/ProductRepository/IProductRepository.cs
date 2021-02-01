using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadingIsGood.Dtos;
using ReadingIsGood.Entities;

namespace ReadingIsGood.Repositories.ProductRepository
{
    public interface IProductRepository
    {
        Task AddAsync(Product entity);
        Task UpdateAsync(Product entity);
        Task<List<Product>> GetAllAsync(ProductFilterDto dto);
        Task<Product> GetByIdAsync(Guid id);
    }
}
