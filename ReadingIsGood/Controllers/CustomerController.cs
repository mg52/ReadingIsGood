using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;
using ReadingIsGood.Dtos;
using ReadingIsGood.Services.CustomerService;

namespace ReadingIsGood.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _userService;

        public CustomerController(ICustomerService userService)
        {
            _userService = userService;
        }

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

        [HttpGet]
        public async Task<IActionResult> GetCurrentCustomerInfo()
        {
            var currentCustomerId = Guid.Parse(User.Identity.Name);

            var user = await _userService.GetByIdAsync(currentCustomerId);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
