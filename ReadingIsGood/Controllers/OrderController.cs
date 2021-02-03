using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;
using ReadingIsGood.Dtos;
using ReadingIsGood.Services.CustomerService;
using ReadingIsGood.Entities;
using ReadingIsGood.Services.OrderService;
using ReadingIsGood.Helpers;

namespace ReadingIsGood.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Creates order. New order is created as true for status.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto dto)
        {
            var currentCustomerId = Guid.Parse(User.Identity.Name);

            dto.CustomerId = currentCustomerId;

            var response = await _orderService.CreateOrder(dto);
            if(response == null)
            {
                return BadRequest(new { message = "Quantity exceeds stock quantity or Product cannot found." });
            }

            return Ok(response);
        }

        /// <summary>
        /// Gets Orders. (Admins can see all orders.)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            Guid? currentCustomerId = Guid.Empty;

            if (User.IsInRole(Role.Admin))
            {
                currentCustomerId = null;
            }
            else
            {
                currentCustomerId = Guid.Parse(User.Identity.Name);
            }

            var user = await _orderService.GetOrders(new OrderFilterDto() { CustomerId = currentCustomerId});

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
