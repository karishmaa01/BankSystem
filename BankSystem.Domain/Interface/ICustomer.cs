using BankSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Interface
{
    public interface ICustomer
    {
        Task<List<Customer>> GetAllCustomerAsync();
        Task<Customer> GetCustomerByIdAsync(int AccountNumber);
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<Customer> UpdateCustomerAsync(Customer customer, int AccountNumber);
        Task<Customer> DeleteCustomerAsync(int AccountNumber);
    }
}
