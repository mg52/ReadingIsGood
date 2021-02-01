using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;
using ReadingIsGood.Dtos;
using ReadingIsGood.Services.CustomerService;
using ReadingIsGood.Entities;
using ReadingIsGood.Services.ProductService;

namespace ReadingIsGood.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto dto)
        {
            if (!User.IsInRole(Role.Admin))
                return Forbid();

            var response = await _productService.CreateProduct(dto);

            if (response == null)
            {
                return BadRequest(new { message = "Book Name and Book Code already exist." });
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var user = await _productService.GetProducts(new ProductFilterDto());

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
