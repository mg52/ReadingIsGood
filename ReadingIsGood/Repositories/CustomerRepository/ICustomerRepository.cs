using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadingIsGood.Dtos;
using ReadingIsGood.Entities;

namespace ReadingIsGood.Repositories.CustomerRepository
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer entity);
        Task UpdateAsync(Customer entity);
        Task DeleteAsync(Customer entity);
        Task<Customer> GetByIdAsync(Guid id);
        Task<List<Customer>> GetAllAsync(CustomerFilterDto dto);
    }
}
