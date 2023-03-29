using BankSystem.Domain.Data;
using BankSystem.Domain.Interface;
using BankSystem.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Repository
{
    public class CustomerRepository : ICustomer
    {
        private readonly BankDb _context;
        public CustomerRepository(BankDb context)
        {
            _context = context;
        }
        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> DeleteCustomerAsync(int AccountNumber)
        {
            var customers = await _context.Customers.FirstOrDefaultAsync(customer => customer.AccountNumber == AccountNumber);
            _context.Customers.Remove(customers);
            await _context.SaveChangesAsync();
            return customers;
        }

        public async Task<List<Customer>> GetAllCustomerAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int AccountNumber)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(customer => customer.AccountNumber == AccountNumber);
            return customer;
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer, int AccountNumber)
        {
            var Customer = await _context.Customers.FirstOrDefaultAsync(customer => customer.AccountNumber == AccountNumber); 
            Customer.AccountNumber = customer.AccountNumber;
            Customer.CustomerName = customer.CustomerName;
            Customer.PhoneNumber = customer.PhoneNumber;
            Customer.Email = customer.Email;
            Customer.Password = customer.Password;
            Customer.CurrentBalance = customer.CurrentBalance;
            await _context.SaveChangesAsync();
            return Customer;
        }
    }
}
