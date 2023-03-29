using BankSystem.Domain.Model;
using BankSystem.Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Service.Interface
{
    public interface ICustomerRepository
    {
        Task<List<CustomerMapping>> GetAllCustomerAsync();
        Task<Customer> GetCustomerByIdAsync(int AccountNumber);
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<Customer> UpdateCustomerAsync(Customer customer, int AccountNumber);
        Task<Customer> DeleteCustomerAsync(int AccountNumber);
    }
}
