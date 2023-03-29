using BankSystem.Domain.Data;
using BankSystem.Domain.Interface;
using BankSystem.Domain.Model;
using BankSystem.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.DomainTestCase
{
    public class CustomerDomainTest
    {
        private readonly ICustomer _Domain;
        private DbContextOptions<BankDb> DbContextOptions = new DbContextOptionsBuilder<BankDb>()
           .UseInMemoryDatabase(databaseName: "Customers").Options;
        public CustomerDomainTest()
        {
            _Domain = new CustomerRepository(new BankDb(DbContextOptions));
        }
        private void SeedDB(int id, BankDb bankdb)
        {
            var Data = new List<Customer>()
            {
            new Customer()
                {
                    AccountNumber = id,
                    CustomerName = "ishu",
                    Email = "ishu@gmail.com",
                    PhoneNumber = 8072249622,
                    Password = "12ishu",
                    CurrentBalance = new long(),
                }
            };
            bankdb.Customers.AddRange(Data);
            bankdb.SaveChanges();
        }


        [Fact]
        public void GetAllCustomer()
        {
            //arrange              
            using var bankdb = new BankDb(DbContextOptions);
            SeedDB(1, bankdb);
            //act            
            var result = _Domain.GetAllCustomerAsync();
            //assert           
            Assert.NotNull(result);
            //var res = result.Result;
            //  Assert.Equal("8", res.Count.ToString());
        }

        [Fact]
        public void GetByIdCustomer()
        {
            //arrange 
            using var bankdb = new BankDb(DbContextOptions);
            SeedDB(2, bankdb);
            //act            
            var result = _Domain.GetCustomerByIdAsync(2);
            //assert           
            Assert.NotNull(result);
        }


        [Fact]
        public void DeleteCustomer()
        {
            //arrange 
            using var bankdb = new BankDb(DbContextOptions);
            SeedDB(3, bankdb);
            //act            
            var result = _Domain.DeleteCustomerAsync(3);
            //assert           
            Assert.NotNull(result);
        }


        [Fact]
        public void UpdateCustomer()
        {
            var Data = new Customer()
            {
                    AccountNumber = 4,
                    CustomerName = "ishu",
                    Email = "ishu@gmail.com",
                    PhoneNumber = 8072249622,
                    Password = "12ishu",
                    CurrentBalance = new long(),
                
            };
            //arrange 
            using var bankdb = new BankDb(DbContextOptions);
            SeedDB(4, bankdb);
            //act            
            var result = _Domain.UpdateCustomerAsync(Data,4);
            //assert           
            Assert.NotNull(result);
        }

        [Fact]
        public void CreateCustomer()
        {
            var Data = new Customer()
            {
                AccountNumber = 8,
                CustomerName = "ishu",
                Email = "ishu@gmail.com",
                PhoneNumber = 8072249622,
                Password = "12ishu",
                CurrentBalance = new long(),

            };
            //arrange 
            using var bankdb = new BankDb(DbContextOptions);
            SeedDB(5, bankdb);
            //act            
            var result = _Domain.CreateCustomerAsync(Data);
            //assert           
            Assert.NotNull(result);
        }
    }
}
