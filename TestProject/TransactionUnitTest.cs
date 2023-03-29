using AutoMapper;
using BankSystem.Controllers;
using BankSystem.Domain.Data;
using BankSystem.Domain.Enum;
using BankSystem.Domain.Model;
using BankSystem.DTO;
using BankSystem.MapperProfile;
using BankSystem.Service.Interface;
using BankSystem.Service.Mapping;
using BankSystem.Service.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Interface;
using BankSystem.Service.Mapper;

namespace TestProject
{
    public class TransactionUnitTest
    {
        private readonly Mock<ITransactionDetRepository> TransService;
        private readonly TransactionDetController _controller;
        private readonly IMapper _mapper;
        private readonly Mock<ITransactionDet> _service;
        private readonly IMapper mapper;
        private readonly TransactionDetService transactionDetService;


        public TransactionUnitTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TransactionDtoProfile());
            });
            var Mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TransactionProfile());
            });
            _mapper = mockMapper.CreateMapper();
            mapper = Mapper.CreateMapper();
            TransService = new Mock<ITransactionDetRepository>();
            _service = new Mock<ITransactionDet>();
            _controller = new TransactionDetController(TransService.Object, _mapper);
            transactionDetService = new TransactionDetService(_service.Object,mapper);
        }


        [Fact(DisplayName = "TransController GET All returns 200 error")]
        public async Task GetAllTransaction_shouldReturn200()
        {
            var Data = new List<TransactionDet>
            {
            new TransactionDet
                {
                TransactionId = 1,
                PayAmount = 1000,
                PaymentMethod = 0,
                Date = DateTime.Now,
                AccountNumber = 1,
                Customer = new Customer()
                {
                    AccountNumber = 1,
                    CustomerName = "ishu",
                    Email ="k@gamil.com",
                    Password = "123",
                    PhoneNumber = 9876543201,
                    CurrentBalance = 100,
                
                },

                },
            };
            //arrange
            TransService.Setup(x => x.GetAllTransactionAsync()).ReturnsAsync(Data);
            _service.Setup(x => x.GetAllTransactionAsync()).ReturnsAsync(Data);

            //act
            var result = await _controller.GetAllTransactions();
            var Response = await transactionDetService.GetAllTransactionAsync();

            //assert
            Assert.IsType<OkObjectResult>(result.Result);
        }


        [Fact(DisplayName = "TransController GET All returns 204 error")]
        public async Task GetAllTransaction_shouldReturn204()
        {
            var transData = new List<TransactionDet>
            {

            };

            //arrange
            TransService.Setup(x => x.GetAllTransactionAsync()).ReturnsAsync(transData);

            //act
            var results = await _controller.GetAllTransactions();

            //assert
            Assert.IsType<NoContentResult>(results.Result);
            var customerlist = results.Result as NoContentResult;
            Assert.Equal("204", (customerlist).StatusCode.ToString());
        }


        [Fact(DisplayName = "TransController GET All returns 500 error")]
        public async Task GetAllTransaction_shouldReturn500()
        {
            // Arrange
            TransService.Setup(x => x.GetAllTransactionAsync()).Throws(new Exception());

            //act
            var ActualResult = await _controller.GetAllTransactions();

            //assert
            Assert.Equal("500", ((ObjectResult)ActualResult.Result).StatusCode.ToString());
        }


        [Fact(DisplayName = "TransController GET ID returns 200 error")]
        public async Task GetByIdTransaction_shouldReturn200()
        {
            List<TransactionDet> Data = new List<TransactionDet>
            {
            new TransactionDet
            {
                TransactionId = 1,
                PayAmount = 1000,
                PaymentMethod = 0,
                Date = DateTime.Now,
                 AccountNumber = 1,
                  Customer = new Customer()
                {
                    AccountNumber = 1,
                    CustomerName = "ishu",
                    Email ="k@gamil.com",
                    Password = "123",
                    PhoneNumber = 9876543201,
                    CurrentBalance = 100,

                },
                },
            };

            //arrange
            TransService.Setup(x => x.GetTransactionByIdAsync(1)).ReturnsAsync(Data);
            _service.Setup(x => x.GetTransactionByIdAsync(1)).ReturnsAsync(Data);

            //act
            var result = await _controller.GetTransactionByIdAsync(1);
            var Response = await transactionDetService.GetTransactionByIdAsync(1);

            //assert
            Assert.IsType<ActionResult<List<TransactionDet>>>(result);
            Assert.IsType<OkObjectResult>(result.Result);
            var item = result.Result as OkObjectResult;
            Assert.IsType<List<TransactionDet>>(item.Value);
        }


        [Fact(DisplayName = "TransController GET ID returns 204 error")]
        public async Task GetByIdTransaction_ShouldReturn204()
        {
            //arrange
            List<TransactionDet> transactionModel = null;
            TransService.Setup(x => x.GetTransactionByIdAsync(It.IsAny<int>())).ReturnsAsync(transactionModel);

            //act            
            var result = await _controller.GetTransactionByIdAsync(It.IsAny<int>());

            //assert                     
            Assert.IsType<NoContentResult>(result.Result);
        }


        [Fact(DisplayName = "TransController GET ID returns 500 error")]
        public async Task GetByIdTransaction_shouldReturn500()
        {
            // Arrange
            TransService.Setup(x => x.GetTransactionByIdAsync(1)).Throws(new Exception());

            //act
            var ActualResult = await _controller.GetTransactionByIdAsync(1);

            //assert
            Assert.Equal("500", ((ObjectResult)ActualResult.Result).StatusCode.ToString());
        }


        [Fact(DisplayName = "TransController GET LAST TRANSACTION returns 200 error")]
        public async Task GetLastTransaction_200()
        {
            var data = new List<TransactionDet>
            {
                new TransactionDet
                {

                TransactionId = 1,
                PayAmount = 1000,
                PaymentMethod = 0,
                Date = DateTime.Now,
                 AccountNumber = 1,
                  Customer = new Customer()
                {
                    AccountNumber = 1,
                    CustomerName = "ishu",
                    Email ="k@gamil.com",
                    Password = "123",
                    PhoneNumber = 9876543201,
                    CurrentBalance = 100,

                },
                },
            };
            //Arrange           
            TransService.Setup(x => x.GetLastTransactionAsync(It.IsAny<int>())).ReturnsAsync(data);
            _service.Setup(x => x.GetLastTransactionAsync(It.IsAny<int>())).ReturnsAsync(data);

            //Act
            var details = await _controller.GetLastTransactionAsync(It.IsAny<int>());
            var Response = await transactionDetService.GetLastTransactionAsync(It.IsAny<int>());

            //Assert
            Assert.IsType<ActionResult<List<TransactionDet>>>(details);
            Assert.IsType<OkObjectResult>(details.Result);
            var response = details.Result as OkObjectResult;
            Assert.IsType<List<TransactionDet>>(response.Value);
            var responseresult = response.Value as TransactionDet;
        }


        [Fact(DisplayName = "TransController GET LAST TRANSACTION returns 204 error")]
        public async Task GetLastTransaction_204()
        {
            //Arrange                 
            List<TransactionDet> data = null;
            TransService.Setup(x => x.GetLastTransactionAsync(It.IsAny<int>())).ReturnsAsync(data);

            //Act       
            var details = await _controller.GetLastTransactionAsync(It.IsAny<int>());

            //Assert            
            Assert.IsType<NoContentResult>(details.Result);
        }


        [Fact(DisplayName = "TransController GET LAST TRANSACTION returns 500 error")]
        public async Task GetLastTransaction_500()
        {
            //arrange           
            TransService.Setup(x => x.GetLastTransactionAsync(It.IsAny<int>())).Throws(new Exception());

            //act           
            var details = await _controller.GetLastTransactionAsync(It.IsAny<int>());

            //assert         
            Assert.Equal("500", ((ObjectResult)details.Result).StatusCode.ToString());
        }


        [Fact(DisplayName = "TransController CREATE TRANSACTION returns 200 error")]
        public async Task DoTransaction_200()
        {
            //arrange          
            var data = new TDetails()
            {
                AccountNumber = 1,
                PayAmount = 1000,
                Date = DateTime.Now,

            };

            var newdata = new TransactionDto()
            {
                AccountNumber = 1,
                PayAmount = 1000,
                Date = DateTime.Now,
            };

            var newres = new TransactionDet()
            {
                TransactionId = 1,
                PayAmount = 1000,
                PaymentMethod = 0,
                Date = DateTime.Now,
                AccountNumber = 1,
                Customer = new Customer()
                {
                    AccountNumber = 1,
                    CustomerName = "ishu",
                    Email = "k@gamil.com",
                    Password = "123",
                    PhoneNumber = 9876543201,
                    CurrentBalance = 100,

                },
            };
            

            TransService.Setup(x => x.DoTransaction(data, It.IsAny<PaymentType>())).ReturnsAsync(newres);
            _service.Setup(x => x.DoTransaction(newres, It.IsAny<PaymentType>())).ReturnsAsync(newres);

            //act           
            var details = await _controller.Balance(newdata, It.IsAny<PaymentType>());
            var Response = await transactionDetService.DoTransaction(data, It.IsAny<PaymentType>());


            //aasert         
            Assert.IsType<OkObjectResult>(details);
            var result = details as OkObjectResult;
            Assert.Equal("Successful Transaction", result.Value);
        }


        [Fact(DisplayName = "TransController CREATE TRANSACTION returns 500 error")]
        public async Task DoTransaction_500()
        {
            //arrange           
            TransService.Setup(x => x.DoTransaction(It.IsAny<TDetails>(), It.IsAny<PaymentType>())).Throws(new Exception());

            //act            
            var data = await _controller.Balance(It.IsAny<TransactionDto>(), It.IsAny<PaymentType>());

            //assert     
            var result = data as ObjectResult;
            Assert.Equal("500", ((ObjectResult)data).StatusCode.ToString());
        }


        [Fact(DisplayName = "TransController GET by Date returns 200 error")]
        public async Task GetByDateTransaction_shouldReturn200()
        {
            var Data = new List<TransactionDet>
            {
            new TransactionDet
                {
                TransactionId = 1,
                PayAmount = 1000,
                PaymentMethod = 0,
                Date = DateTime.Now,
                 AccountNumber = 1,
                  Customer = new Customer()
                {
                    AccountNumber = 1,
                    CustomerName = "ishu",
                    Email ="k@gamil.com",
                    Password = "123",
                    PhoneNumber = 9876543201,
                    CurrentBalance = 100,

                },
                },
            };
            //arrange
            TransService.Setup(x => x.GetByTransactionDate(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(Data);
            _service.Setup(x => x.GetByTransactionDate(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(Data);

            //act
            var result = await _controller.GetByDate(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>());
            var Response = await transactionDetService.GetByTransactionDate(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>());

            //assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact(DisplayName = "TransController GET by Date returns 204 error")]
        public async Task GetByDateTransaction_shouldReturn204()
        {
            //arrange
            List<TransactionDet> Data = null;
            TransService.Setup(x => x.GetByTransactionDate(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(Data);

            //act
            var Response = await _controller.GetByDate(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>());

            //assert
            Assert.IsType<NoContentResult>(Response.Result);
        }

        [Fact(DisplayName = "TransController GET by Date returns 500 error")]
        public async Task GetByDateTransaction_shouldReturn500()
        {
            TransService.Setup(x => x.GetByTransactionDate(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Throws(new Exception());

            var Response = await _controller.GetByDate(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>());

            var Result = Response.Result as ObjectResult;
            Assert.Equal(StatusCodes.Status500InternalServerError, Result.StatusCode);
        }

    }
}

