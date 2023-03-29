using AutoMapper;
using BankSystem.Controllers;
using BankSystem.Domain.Enum;
using BankSystem.Domain.Interface;
using BankSystem.Domain.Model;
using BankSystem.DTO;
using BankSystem.MapperProfile;
using BankSystem.Service.Interface;
using BankSystem.Service.Mapper;
using BankSystem.Service.Mapping;
using BankSystem.Service.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class AmountTransferUnitTest
    {
        private readonly Mock<IAmountTransferRepository> AmountTransferService;
        private readonly IMapper _mapper;
        private readonly AmountTransferController controller;
        private readonly Mock<IAmountTransfer> _service;
        private readonly IMapper mapper;
        private readonly AmountTransferService amount;

        public AmountTransferUnitTest()
        {

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ATDtoProfile());
            });
            var Mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ATProfile());
            });
            _mapper = mockMapper.CreateMapper();
            mapper = Mapper.CreateMapper();
            AmountTransferService = new Mock<IAmountTransferRepository>();
            _service = new Mock<IAmountTransfer>();
            controller = new AmountTransferController(AmountTransferService.Object, _mapper);
            amount = new AmountTransferService(_service.Object, mapper);
        }

        [Fact(DisplayName = "Controller GET All returns 200 error")]
        public async Task GetAllAmountTransfer_shouldReturn200()
        {
            var newdata = new List<AmountTransfer>
            {
                new AmountTransfer
                {
                    TransactionId = 1,
                TransactionType = 0,
                TransactionDate = DateTime.Now,
                TransactionAmount = 100,
                DestinationAccountNumber = new int(),
                AccountNumber = new int(),
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

            var Data = new List<ATMapping>
            {
            new ATMapping
                {
                TransactionType = 0,
                TransactionDate = DateTime.Now,
                TransactionAmount = 100,
                DestinationAccountNumber = new int(),
                AccountNumber = new int(),

                },
            };

            //arrange
            AmountTransferService.Setup(x => x.GetAllTransactionAsync()).ReturnsAsync(Data);
            _service.Setup(x => x.GetAllTransactionAsync()).ReturnsAsync(newdata);

            //act
            var Response = await controller.GetAllTransactions();
            var Res = await amount.GetAllTransactionAsync();

            //assert
            Assert.IsType<OkObjectResult>(Response.Result);
        }

        [Fact(DisplayName = "Controller GET All returns 204 error")]
        public async Task GetAllAmountTransfer_shouldReturn204()
        {
            var transData = new List<ATMapping>
            {

            };

            //arrange
            AmountTransferService.Setup(x => x.GetAllTransactionAsync()).ReturnsAsync(transData);

            //act
            var results = await controller.GetAllTransactions();

            //assert
            Assert.IsType<NoContentResult>(results.Result);
            var customerlist = results.Result as NoContentResult;
            Assert.Equal("204", (customerlist).StatusCode.ToString());
        }


        [Fact(DisplayName = "Controller GET All returns 500 error")]
        public async Task GetAllAmountTransfer_shouldReturn500()
        {
            // Arrange
            AmountTransferService.Setup(x => x.GetAllTransactionAsync()).Throws(new Exception());

            //act
            var ActualResult = await controller.GetAllTransactions();

            //assert
            Assert.Equal("500", ((ObjectResult)ActualResult.Result).StatusCode.ToString());
        }


        [Fact(DisplayName = "Controller GET LAST TRANSACTION returns 200 error")]
        public async Task GetLastAmountTransaction_200()
        {
            var data = new List<AmountTransfer>
            {
                new AmountTransfer
                {
                    TransactionId = 1,
                TransactionType = 0,
                TransactionDate = DateTime.Now,
                TransactionAmount = 100,
                DestinationAccountNumber = new int(),
                AccountNumber = new int(),
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
            AmountTransferService.Setup(x => x.GetLastTransactionAsync(It.IsAny<int>())).ReturnsAsync(data);
            _service.Setup(x => x.GetLastTransactionAsync(It.IsAny<int>())).ReturnsAsync(data);

            //Act
            var details = await controller.GetLastTransactionAsync(It.IsAny<int>());
            var Res = await amount.GetLastTransactionAsync(It.IsAny<int>());

            //Assert
            Assert.IsType<ActionResult<List<AmountTransfer>>>(details);
            Assert.IsType<OkObjectResult>(details.Result);
            var response = details.Result as OkObjectResult;
            Assert.IsType<List<AmountTransfer>>(response.Value);
            var responseresult = response.Value as AmountTransfer;
        }


        [Fact(DisplayName = "Controller GET LAST TRANSACTION returns 204 error")]
        public async Task GetLastAmountTransaction_204()
        {
            //Arrange                 
            List<AmountTransfer> data = null;
            AmountTransferService.Setup(x => x.GetLastTransactionAsync(It.IsAny<int>())).ReturnsAsync(data);

            //Act       
            var details = await controller.GetLastTransactionAsync(It.IsAny<int>());

            //Assert            
            Assert.IsType<NoContentResult>(details.Result);
        }


        [Fact(DisplayName = "Controller GET LAST TRANSACTION returns 500 error")]
        public async Task GetLastAmountTransaction_500()
        {
            //arrange           
            AmountTransferService.Setup(x => x.GetLastTransactionAsync(It.IsAny<int>())).Throws(new Exception());

            //act           
            var details = await controller.GetLastTransactionAsync(It.IsAny<int>());

            //assert         
            Assert.Equal("500", ((ObjectResult)details.Result).StatusCode.ToString());
        }

        [Fact(DisplayName = "Controller GET by Date returns 200 error")]
        public async Task GetByDateAmountTransaction_shouldReturn200()
        {
            var Data = new List<AmountTransfer>
            {
            new AmountTransfer
                {
                 TransactionId = 1,
                TransactionType = 0,
                TransactionDate = DateTime.Now,
                TransactionAmount = 100,
                DestinationAccountNumber = new int(),
                AccountNumber = new int(),
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
            AmountTransferService.Setup(x => x.GetByTransactionDate(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(Data);
            _service.Setup(x => x.GetByTransactionDate(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(Data);

            //act
            var result = await controller.GetByDate(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>());
            var Res = await amount.GetByTransactionDate(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>());

            //assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact(DisplayName = "Controller GET by Date returns 204 error")]
        public async Task GetByDateAmountTransaction_shouldReturn204()
        {
            //arrange
            List<AmountTransfer> Data = null;
            AmountTransferService.Setup(x => x.GetByTransactionDate(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(Data);

            //act
            var Response = await controller.GetByDate(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>());

            //assert
            Assert.IsType<NoContentResult>(Response.Result);
        }

        [Fact(DisplayName = "Controller GET by Date returns 500 error")]
        public async Task GetByDateAmountTransaction_shouldReturn500()
        {
            //arrange
            AmountTransferService.Setup(x => x.GetByTransactionDate(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Throws(new Exception());

            //act
            var Response = await controller.GetByDate(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>());

            //assert
            var Result = Response.Result as ObjectResult;
            Assert.Equal(StatusCodes.Status500InternalServerError, Result.StatusCode);
        }


        [Fact(DisplayName = "Controller CREATE TRANSACTION returns 200 error")]
        public async Task ATTransaction_200()
        {
            //arrange          
            var data = new ATMapping()
            {
                TransactionType = 0,
                TransactionDate = DateTime.Now,
                TransactionAmount = 100,
                DestinationAccountNumber = new int(),
                AccountNumber = new int(),

            };

            var newdata = new ATDto()
            {
                TransactionType = 0,
                TransactionDate = DateTime.Now,
                TransactionAmount = 100,
                DestinationAccountNumber = new int(),
                AccountNumber = new int(),
            };


            var ATData = new AmountTransfer
            {
                TransactionId = 1,
                TransactionType = 0,
                TransactionDate = DateTime.Now,
                TransactionAmount = 100,
                DestinationAccountNumber = new int(),
                AccountNumber = new int(),
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


            AmountTransferService.Setup(x => x.AmountTransaction(data, It.IsAny<TransType>())).ReturnsAsync(ATData);
            _service.Setup(x => x.AmountTransaction(ATData, It.IsAny<TransType>())).ReturnsAsync(ATData);

            //act           
            var details = await controller.Transfer(newdata, It.IsAny<TransType>());
            var Res = await amount.AmountTransaction(data, It.IsAny<TransType>());

            //aasert         
            Assert.IsType<OkObjectResult>(details);
            var result = details as OkObjectResult;
            Assert.Equal("Transaction Successful", result.Value);
        }


        [Fact(DisplayName = "Controller CREATE TRANSACTION returns 500 error")]
        public async Task ATTransaction_500()
        {
            //arrange           
            AmountTransferService.Setup(x => x.AmountTransaction(It.IsAny<ATMapping>(), It.IsAny<TransType>())).Throws(new Exception());

            //act            
            var data = await controller.Transfer(It.IsAny<ATDto>(), It.IsAny<TransType>());

            //assert     
            var result = data as ObjectResult;
            Assert.Equal("500", ((ObjectResult)data).StatusCode.ToString());
        }
    }
}
