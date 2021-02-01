using ReadingIsGood.Dtos;
using ReadingIsGood.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<CustomerDto> Authenticate(string username, string password);
        Task<CustomerDto> AddAsync(CustomerCreateDto dto);
        Task<Customer> GetByIdAsync(Guid id);
    }
}
