using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;
using ReadingIsGood.Dtos;
using ReadingIsGood.Services.CustomerService;
using ReadingIsGood.Services.OrderService;
using ReadingIsGood.Entities;
using ReadingIsGood.Helpers;

namespace ReadingIsGood.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _userService;
        private IOrderService _orderService;

        public CustomerController(ICustomerService userService, IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }

        /// <summary>
        /// Authenticate customer. Gets token.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromForm] AuthDto dto)
        {
            var user = await _userService.Authenticate(dto.Username, dto.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(user);
        }

        /// <summary>
        /// Creates customer.
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        [Route("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateDto dto)
        {
            var response = await _userService.AddAsync(dto);

            if (response == null)
            {
                return BadRequest(new { message = "Username is already taken." });
            }

            return Ok(response);
        }

        /// <summary>
        /// Gets logged in customer info.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetCurrentCustomerInfo()
        {
            var currentCustomerId = Guid.Parse(User.Identity.Name);

            var user = await _userService.GetByIdAsync(currentCustomerId);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// (ADMIN ONLY) Executes specific orders which means book is delivered to the customer. Order status sets to false.
        /// </summary>
        [HttpPost]
        [Route("ExecuteOrder")]
        public async Task<IActionResult> ExecuteOrder([FromBody] ExecuteOrderDto dto)
        {
            if (!User.IsInRole(Role.Admin))
                return Forbid();

            var response = await _orderService.ExecuteOrders(dto);

            if (response == null)
            {
                return BadRequest(new { message = "Order cannot found." });
            }

            return Ok(response);
        }
    }
}
