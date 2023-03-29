using BankSystem.Domain.Data;
using BankSystem.Domain.Interface;
using BankSystem.Domain.Migrations;
using BankSystem.Domain.Model;
using BankSystem.Domain.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.DomainTestCase
{
    public class TransactionDomainTest
    {
        private readonly ITransactionDet _Domain;
        private DbContextOptions<BankDb> DbContextOptions = new DbContextOptionsBuilder<BankDb>()
           .UseInMemoryDatabase(databaseName: "TransactionDets").Options;
        public TransactionDomainTest()
        {
            _Domain = new TransactionDetailRepository(new BankDb(DbContextOptions));
        }
        private void SeedDB(int id, BankDb bankdb)
        {
            var Data = new List<TransactionDet>()
            {
            new TransactionDet()
                {
                    TransactionId = id,
                PayAmount = 1000,
                PaymentMethod = 0,
                Date = DateTime.Now,
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
                }
            };
            bankdb.TransactionDets.AddRange(Data);
            bankdb.SaveChanges();
        }


        [Fact]
        public void GetAllTransaction()
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
        public void GetTransactionById()
        {
           
            //arrange              
            using var bankdb = new BankDb(DbContextOptions);
            SeedDB(2, bankdb);
            //act            
            var result = _Domain.GetTransactionByIdAsync(22);
            //assert           
            Assert.NotNull(result);
        }

        //[Fact]
        //public void GetTransactionById_204()
        //{
           
        //    //arrange              
        //    using var bankdb = new BankDb(DbContextOptions);
        //    SeedDB(10, bankdb);
        //    //act            
        //    var result = _Domain.GetTransactionByIdAsync(It.IsAny<int>());
        //    //assert           
        //    Assert.Null(result.Result);
        //}

        [Fact]
        public void CreateTransaction()
        {
            var Data = new TransactionDet()
            {
                TransactionId = 3,
                PayAmount = 1000,
                PaymentMethod = 0,
                Date = DateTime.Now,
                AccountNumber = 33,
                Customer = new Customer()
                {
                    AccountNumber = 33,
                    CustomerName = "ishu",
                    Email ="k@gamil.com",
                    Password = "1234",
                    PhoneNumber = 9876543201,
                    CurrentBalance = 100,

                },
                
            };
            //arrange
            using var bankdb = new BankDb(DbContextOptions);
            SeedDB(3, bankdb);             
            //act
            var result = _Domain.DoTransaction(Data, 0);
            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetRecent5Transaction()
        {
            //arrange
            using var bankdb = new BankDb(DbContextOptions);
            SeedDB(9, bankdb);            
            //act
            var result = _Domain.GetLastTransactionAsync(99).Result;
            //assert
            Assert.NotNull(result);
            Assert.Equal(9, result[0].TransactionId);
            Assert.Equal(1000, result[0].PayAmount);
            Assert.Equal("Deposit", result[0].PaymentMethod.ToString());
        }


        [Fact]
        public void GetRecent5Transaction_204()
        {
            //arrange
            using var context = new BankDb(DbContextOptions);
            SeedDB(7, context);
            //act
            var Result = _Domain.GetLastTransactionAsync(9);
            //assert
            Assert.Null(Result.Result);
        }


        [Fact]
        public void GetTransactionByDate()
        {
            //arrange
            using var bankdb = new BankDb(DbContextOptions);
            DateTime FromDate = new DateTime(2023 - 1 - 1);
            DateTime ToDate = new DateTime(2023 - 3 - 22);
            SeedDB(5, bankdb);            
            //act
            var result = _Domain.GetByTransactionDate(55, DateTime.MinValue, DateTime.MaxValue).Result;
            //assert
            Assert.NotNull(result);
            Assert.Equal(5, result[0].TransactionId);
            Assert.Equal(1000, result[0].PayAmount);
            Assert.Equal("Deposit", result[0].PaymentMethod.ToString());
        }

      

        [Fact]
        public void GetTransactionByDate_204()
        {
            using var context = new BankDb(DbContextOptions);
            SeedDB(8, context);
            var Result = _Domain.GetByTransactionDate(10, DateTime.MinValue, DateTime.MaxValue);
            Assert.Null(Result.Result);
        }

        [Fact]
        public void GetTransactionById_204()
        {

            //arrange              
            using var bankdb = new BankDb(DbContextOptions);
            SeedDB(10, bankdb);
            //act            
            var result = _Domain.GetTransactionByIdAsync(1);
            //assert           
            Assert.Null(result.Result);
        }
    }

}
