using BankSystem.Domain.Data;
using BankSystem.Domain.Interface;
using BankSystem.Domain.Model;
using BankSystem.Domain.Repository;
using BankSystem.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.DomainTestCase
{
    public class AmountTransferDomainTest
    {
        private readonly IAmountTransfer _Domain;
        private readonly ICustomer _customer;
        private DbContextOptions<BankDb> DbContextOptions = new DbContextOptionsBuilder<BankDb>()
        .UseInMemoryDatabase(databaseName: "AmountTransfers").Options;

        public AmountTransferDomainTest()
        {
            _Domain = new AmountTransferRepository(new BankDb(DbContextOptions),_customer);
        }



        private void SeedDB(int id, BankDb bankdb)
        {
            var Data = new List<AmountTransfer>()
            {
            new AmountTransfer()
                {
                   TransactionId = id,
                TransactionType = 0,
                TransactionDate = DateTime.Now,
                TransactionAmount = 100,
                DestinationAccountNumber = 6554,
                AccountNumber = (id*10)+id,
                Customer = new Customer()
                {
                    AccountNumber = (id*10)+id,
                    CustomerName = "ishu",
                    Email ="k@gamil.com",
                    Password = "123",
                    PhoneNumber = 9876543201,
                    CurrentBalance = 100,

                },
                },
            };
            bankdb.AmountTransfers.AddRange(Data);
            bankdb.SaveChanges();
        }

        [Fact]
        public void GetAllAmountTransfer()
        {
            //arrange              
            using var bankdb = new BankDb(DbContextOptions);
            SeedDB(1, bankdb);
            //act            
            var result = _Domain.GetAllTransactionAsync();
            //assert           
            Assert.NotNull(result);
        }

        [Fact]
        public void CreateAmountTransfer()
        {
            var Data = new AmountTransfer()
            {
                TransactionId = 1,
                TransactionType = 0,
                TransactionDate = DateTime.Now,
                TransactionAmount = 100,
                DestinationAccountNumber = 6554,
                AccountNumber = 6401,
                Customer = new Customer()
                {
                    AccountNumber = 22,
                    CustomerName = "ishu",
                    Email = "k@gamil.com",
                    Password = "123",
                    PhoneNumber = 9876543201,
                    CurrentBalance = 100,
                },
            };
            //arrange
            using var bankdb = new BankDb(DbContextOptions);
            SeedDB(2, bankdb);
            //act
            var result = _Domain.AmountTransaction(Data, 0);
            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAmountTransferByDate()
        {
            //arrange
            using var bankdb = new BankDb(DbContextOptions);
            DateTime FromDate = new DateTime(2023 - 1 - 1);
            DateTime ToDate = new DateTime(2023 - 3 - 22);
            SeedDB(3, bankdb);
            //act
            var result = _Domain.GetByTransactionDate(33, DateTime.MinValue, DateTime.MaxValue).Result;
            //assert
            Assert.NotNull(result);
            Assert.Equal(3, result[0].TransactionId);
            Assert.Equal(100, result[0].TransactionAmount);
            Assert.Equal(6554, result[0].DestinationAccountNumber);
            Assert.Equal("Deposit", result[0].TransactionType.ToString());
        }

        [Fact]
        public void GetRecentAmountTransfer()
        {
            //arrange
            using var bankdb = new BankDb(DbContextOptions);
            SeedDB(4, bankdb);
            //act
            var result = _Domain.GetLastTransactionAsync(44).Result;
            //assert
            Assert.NotNull(result);
            Assert.Equal(4, result[0].TransactionId);
            Assert.Equal(100, result[0].TransactionAmount);
            Assert.Equal(6554, result[0].DestinationAccountNumber);
            Assert.Equal("Deposit", result[0].TransactionType.ToString());

        }

        [Fact]
        public void GetRecentAmountTransfer_204()
        {
            using var context = new BankDb(DbContextOptions);
            SeedDB(5, context);
            var Result = _Domain.GetLastTransactionAsync(6).Result;
            Assert.Null(Result);
        }

        [Fact]
        public void GetAmountTransferByDate_204()
        {
            using var context = new BankDb(DbContextOptions);
            DateTime FromDate = new DateTime(2023 - 1 - 1);
            DateTime ToDate = new DateTime(2023 - 3 - 22);
            SeedDB(7, context);
            var Result = _Domain.GetByTransactionDate(4, DateTime.MinValue, DateTime.MaxValue);
            Assert.Null(Result.Result);
        }

    }
}
