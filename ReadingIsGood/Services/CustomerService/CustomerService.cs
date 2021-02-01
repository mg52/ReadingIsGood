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

namespace ReadingIsGood.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _userRepository;
        private readonly AppSettings _appSettings;

        
        public CustomerService(IOptions<AppSettings> appSettings, ICustomerRepository userRepository)
        {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
        }

        public async Task<CustomerDto> AddAsync(CustomerCreateDto dto)
        {
            var users = await _userRepository.GetAllAsync(new CustomerFilterDto() { Username = dto.Username });
            if(users?.FirstOrDefault() != null)
            {
                return null;
            }

            var entity = dto.Adapt<Customer>();
            entity.Password = BC.HashPassword(dto.Password);

            await _userRepository.AddAsync(entity);

            var result = await Authenticate(entity.Username, dto.Password);

            return result;
        }

        public async Task<CustomerDto> Authenticate(string username, string password)
        {
            var users = await _userRepository.GetAllAsync(new CustomerFilterDto() { Username = username });
            var user = users?.FirstOrDefault();

            if (user == null || !BC.Verify(password, user.Password))
            {
                // authentication failed
                return null;
            }

            var userDto = user.Adapt<CustomerDto>();

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userDto.Id.ToString()),
                    new Claim(ClaimTypes.Role, userDto.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userDto.AccessToken = tokenHandler.WriteToken(token);
            userDto.TokenType = "Bearer";

            return userDto.WithoutPassword();
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            var entity = await _userRepository.GetByIdAsync(id);

            return entity.WithoutPassword();
        }
    }
}