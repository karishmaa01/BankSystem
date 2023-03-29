using AutoMapper;
using BankSystem.Domain.Interface;
using BankSystem.Domain.Model;
using BankSystem.Service.Interface;
using BankSystem.Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Service.Repository
{
    public class CustomerService : ICustomerRepository
    {
        private readonly ICustomer _customer;
        private readonly IMapper _mapper;

       public CustomerService(ICustomer customer, IMapper mapper)
        {
            _customer = customer;
            _mapper = mapper;
        }
        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            return await _customer.CreateCustomerAsync(customer);
        }

        public async Task<Customer> DeleteCustomerAsync(int AccountNumber)
        {
            return await _customer.DeleteCustomerAsync(AccountNumber);
        }

        public async Task<List<CustomerMapping>> GetAllCustomerAsync()
        {
            var customer = await _customer.GetAllCustomerAsync();
            return _mapper.Map<List<CustomerMapping>>(customer);
           
        }

        public async Task<Customer> GetCustomerByIdAsync(int AccountNumber)
        {
            return await _customer.GetCustomerByIdAsync(AccountNumber);
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer, int AccountNumber)
        {
            return await _customer.UpdateCustomerAsync(customer, AccountNumber);
        }
    }
}
