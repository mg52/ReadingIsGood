using ReadingIsGood.Dtos;
using ReadingIsGood.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Services.ProductService
{
    public interface IProductService
    {
        Task<ProductDto> CreateProduct(ProductCreateDto dto);
        Task<List<ProductDto>> GetProducts(ProductFilterDto dto);
    }
}
