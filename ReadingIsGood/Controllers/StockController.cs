using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;
using ReadingIsGood.Dtos;
using ReadingIsGood.Services.CustomerService;
using ReadingIsGood.Entities;
using ReadingIsGood.Services.StockService;
using ReadingIsGood.Helpers;

namespace ReadingIsGood.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        /// <summary>
        /// (ADMIN ONLY) Create new stock 
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] StockCreateDto dto)
        {
            if (!User.IsInRole(Role.Admin))
                return Forbid();

            var response = await _stockService.CreateStock(dto);

            if (response == null)
            {
                return BadRequest(new { message = "This Product already added to stock." });
            }

            return Ok(response);
        }

        /// <summary>
        /// Gets stocks.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetStocks()
        {
            var user = await _stockService.GetStocks(new StockFilterDto());

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
