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
using ReadingIsGood.Services.ProductService;
using ReadingIsGood.Repositories.ProductRepository;

namespace ReadingIsGood.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> CreateProduct(ProductCreateDto dto)
        {
            var product = await _productRepository.GetAllAsync(new ProductFilterDto() { BookCode = dto.BookCode, BookName = dto.BookName });
            if (product?.FirstOrDefault() != null)
            {
                return null;
            }

            var entity = dto.Adapt<Product>();

            await _productRepository.AddAsync(entity);

            var result = entity.Adapt<ProductDto>();

            return result;
        }

        public async Task<List<ProductDto>> GetProducts(ProductFilterDto dto)
        {
            var products = await _productRepository.GetAllAsync(dto);
            return products.Adapt<List<ProductDto>>();
        }
    }
}