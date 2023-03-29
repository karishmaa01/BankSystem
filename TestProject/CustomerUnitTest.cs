using AutoMapper;
using BankSystem;
using BankSystem.Controllers;
using BankSystem.Domain.Data;
using BankSystem.Domain.Interface;
using BankSystem.Domain.Model;
using BankSystem.DTO;
using BankSystem.MapperProfile;
using BankSystem.Service.Interface;
using BankSystem.Service.Mapper;
using BankSystem.Service.Mapping;
using BankSystem.Service.Repository;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class CustomerUnitTest
    {
        private readonly Mock<ICustomer> Service;
        private readonly Mock<ICustomerRepository> CustomerRepos;
        private readonly IMapper _mapper;
        //private readonly IMapper mapper;
        private readonly CustomerService _service;
        private readonly CustomerController controller;
        

        public CustomerUnitTest()
        {

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });
            //var Mapper = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile(new CustomerDtoProfile());
            //});
            _mapper = mockMapper.CreateMapper();
            //mapper = Mapper.CreateMapper();
            Service = new Mock<ICustomer>();
            CustomerRepos = new Mock<ICustomerRepository>();
            _service = new CustomerService(Service.Object, _mapper);
            controller = new CustomerController(CustomerRepos.Object, _mapper);
        }


        [Fact(DisplayName = "Controller GET All returns 200 error")]
        public async Task GetAllCustomer_shouldReturn200()
        {
            var Data = new List<Customer>
            {
                new Customer
                {
                    AccountNumber = new int(),
                    CustomerName = "ishu",
                    Email = "ishu@gmail.com",
                    PhoneNumber = 8072249622,
                    Password = "12ishu",
                    CurrentBalance = new long(),
                },

            };
            var newData = new List<CustomerMapping>
            {
            new CustomerMapping
                {
                AccountNumber = 1,
                CustomerName = "karishmaa",
                Email = "karish@gmail.com",

                },
            };

            //arrange
            Service.Setup(x => x.GetAllCustomerAsync()).ReturnsAsync(Data);
            CustomerRepos.Setup(x => x.GetAllCustomerAsync()).ReturnsAsync(newData);

            //act
            var Res = await controller.GetCustomers();
            var Response = await _service.GetAllCustomerAsync();

            //assert
            Assert.IsType<OkObjectResult>(Res.Result);
            // Assert.IsType<ActionResult<List<CustomerDto>>>(Response);
            // Assert.IsType<OkObjectResult>(Response.Result);
            // var item = Response.Result as OkObjectResult;
            // Assert.IsType<List<CustomerDto>>(item.Value);
            // //var Item = item.Value as List<CustomerDto>;
            //// Assert.Equal(Data.Count, Item.Count);
        }


        [Fact(DisplayName = "Controller GET All returns 204 error")]
        public async Task GetAllCustomer_shouldReturn204()
        {
            var customerData = new List<CustomerMapping>
            {

            };

            //arrange
            CustomerRepos.Setup(x => x.GetAllCustomerAsync()).ReturnsAsync(customerData);

            //act
            var result = await controller.GetCustomers();

            //assert
            Assert.IsType<NoContentResult>(result.Result);
            var customerlist = result.Result as NoContentResult;
            Assert.Equal("204", (customerlist).StatusCode.ToString());
        }


        [Fact(DisplayName = "Controller GET All returns 500 error")]
        public async Task GetAllCustomer_shouldReturn500()
        {
            // Arrange
            CustomerRepos.Setup(x => x.GetAllCustomerAsync()).Throws(new Exception());

            //act
            var ActualResult = await controller.GetCustomers();

            //assert
            Assert.Equal("500", ((ObjectResult)ActualResult.Result).StatusCode.ToString());
        }

        [Fact(DisplayName = "Controller GET ID returns 200 error")]
        public async Task GetById_shouldReturn200()
        {
            var customerData = new List<Customer>
            {
            new Customer
                {
                AccountNumber = new int(),
                CustomerName = "ishu",
                Email = "ishu@gmail.com",
                PhoneNumber = 8072249622,
                Password = "12ishu",
                CurrentBalance = new long(),
                },
            };

            //arrange
            Service.Setup(x => x.GetCustomerByIdAsync(1)).ReturnsAsync(customerData[0]);
            CustomerRepos.Setup(x => x.GetCustomerByIdAsync(1)).ReturnsAsync(customerData[0]);

            //act
            var Response = await _service.GetCustomerByIdAsync(1);
            var result = await controller.GetCustomer(1);
            Assert.IsType<OkObjectResult>(result.Result);
            var customer = result.Result as OkObjectResult;

            //assert
            Assert.Equal("200", (customer).StatusCode.ToString());
            Assert.IsType<Customer>(customer.Value);
            var cus = customer.Value as Customer;
            Assert.Equal(customerData[0].AccountNumber, cus.AccountNumber);
        }

        [Fact(DisplayName = "Controller GET ID returns 204 error")]
        public async Task GetById_ShouldReturn204()
        {
            List<Customer> customerData = new List<Customer>
            {
            new Customer
                {
                AccountNumber = new int(),
                CustomerName = "ishu",
                Email = "ishu@gmail.com",
                PhoneNumber = 8072249622,
                Password = "12ishu",
                CurrentBalance = new long(),
                },
            };

            //arrange
            CustomerRepos.Setup(x => x.GetCustomerByIdAsync(1)).ReturnsAsync(customerData[0]);

            //act            
            var result = await controller.GetCustomer(3);

            //assert                     
            Assert.IsType<NoContentResult>(result.Result);
            var result1 = result.Result as NoContentResult;
            Assert.Equal("204", (result1).StatusCode.ToString());
        }


        [Fact(DisplayName = "Controller GET ID returns 500 error")]
        public async Task GetById_shouldReturn500()
        {
            // Arrange
            CustomerRepos.Setup(x => x.GetCustomerByIdAsync(1)).Throws(new Exception());

            //act
            var ActualResult = await controller.GetCustomer(1);

            //assert
            Assert.Equal("500", ((ObjectResult)ActualResult.Result).StatusCode.ToString());
        }


        [Fact(DisplayName = "Controller POST returns 200 error")]
        public async Task CreateCustomer_ShouldReturn200()
        {
            //arrange
            var customer = new Customer()
            {
                AccountNumber = new int(),
                CustomerName = "ishu",
                Email = "ishu@gmail.com",
                PhoneNumber = 8072249622,
                Password = "12ishu",
                CurrentBalance = new long(),
            };
            //Arrange
            Service.Setup(x => x.CreateCustomerAsync(customer)).ReturnsAsync(customer);
            CustomerRepos.Setup(x => x.CreateCustomerAsync(customer)).ReturnsAsync(customer);

            //act    
            var Response = await _service.CreateCustomerAsync(customer);
            var result = await controller.PostCustomer(customer);

            //assert
            Assert.IsType<OkObjectResult>(result.Result);
            var result1 = result.Result as OkObjectResult;
            Assert.IsType<Customer>(result1.Value);
            var customerlist = result1.Value as Customer;
            Assert.Equal(customer.CustomerName, customerlist.CustomerName);
            Assert.Equal(customer.Email, customerlist.Email);
            Assert.Equal(customer.PhoneNumber, customer.PhoneNumber);
            Assert.Equal(customer.Password, customerlist.Password);
        }


        [Fact(DisplayName = "Controller POST returns 400 error")]
        public async Task Postustomer_ShouldReturn400()
        {
            //arrange
            var postcus = new Customer()
            {
                AccountNumber = new int(),
                Email = "demo@gmail.com",
                PhoneNumber = 8073324567,
                Password = "Demo"
            };
            //Arrange
            CustomerRepos.Setup(x => x.CreateCustomerAsync(postcus)).ReturnsAsync(postcus);

            //act            
            var result = await controller.PostCustomer(postcus);

            //assert 
            Assert.IsType<ObjectResult>(result.Result);
            var result1 = result.Result as ObjectResult;
            var actualresult = result1.Value as ProblemDetails;
            Assert.Equal("Name should not be empty", actualresult.Detail.ToString());
            Assert.Equal("400", actualresult.Status.ToString());
        }


        [Fact(DisplayName = "Controller POST returns 500 error")]
        public async Task CreateCustomer_shouldReturn500()
        {
            // Arrange
            CustomerRepos.Setup(x => x.CreateCustomerAsync(It.IsAny<Customer>())).Throws(new Exception());

            //act
            var ActualResult = await controller.PostCustomer(It.IsAny<Customer>());

            //assert
            Assert.Equal("500", ((ObjectResult)ActualResult.Result).StatusCode.ToString());
        }

        [Fact(DisplayName = "Controller PUT returns 200 error")]
        public async Task UpdateCustomer_ShouldReturn200Status()
        {
            //arrange
            var customer = new Customer()
            {
                AccountNumber = new int(),
                CustomerName = "ishu",
                Email = "demo411@gmail.com",
                PhoneNumber = 9486562973,
                Password = "Demo@411",
                CurrentBalance = 0
            };
            Service.Setup(x => x.UpdateCustomerAsync(customer,1)).ReturnsAsync(customer);
            CustomerRepos.Setup(x => x.UpdateCustomerAsync(customer, 1)).ReturnsAsync(customer);
            //act       
            var Response = await _service.UpdateCustomerAsync(customer,1);
            var result = await controller.UpdateCustomer(customer, 1);
            //assert            
            Assert.IsType<OkObjectResult>(result.Result);
            var result1 = result.Result as OkObjectResult;
            Assert.Equal("update Successfully", result1.Value);
        }


        [Fact(DisplayName = "Controller PUT returns 400 error")]
        public async Task UpdateCustomer_ShouldReturn400Status()
        {
            //arrange
            var customer = new Customer()
            {
                AccountNumber = new int(),
                //CustomerName = "ishu",
                Email = "demo411@gmail.com",
                PhoneNumber = 9486562973,
                Password = "Demo@411",
                CurrentBalance = 0
            };
            CustomerRepos.Setup(x => x.UpdateCustomerAsync(customer, 1)).ReturnsAsync(customer);
            //act            
            var result = await controller.UpdateCustomer(customer, 1);
            //assert

            Assert.IsType<ObjectResult>(result.Result);
            var result1 = result.Result as ObjectResult;
            var actualresult = result1.Value as ProblemDetails;
            Assert.Equal("400", actualresult.Status.ToString());
            Assert.Equal("Name should not be empty", actualresult.Detail.ToString());
        }

        [Fact(DisplayName = "Controller PUT returns 500 error")]
        public async Task UpdateCustomer_shouldReturn500()
        {
            //Arrange
            CustomerRepos.Setup(x => x.UpdateCustomerAsync(It.IsAny<Customer>(), It.IsAny<int>())).Throws(new Exception());
            //act
            var ActualResult = await controller.UpdateCustomer(It.IsAny<Customer>(), It.IsAny<int>());
            //assert
            Assert.Equal("500", ((ObjectResult)ActualResult.Result).StatusCode.ToString());
        }

        [Fact(DisplayName = "Controller DELETE returns 200 error")]
        public async Task Deletecustomer_ShouldReturn200Status()
        {
            var customerData = new List<Customer>
            {
            new Customer
                {
                AccountNumber = 1,
                CustomerName = "karishmaa",
                Email = "karish@gmail.com",
                Password = "karish",
                PhoneNumber = 9876543021,
                CurrentBalance = 10000

                },
            };
            //arrange
            Service.Setup(x => x.DeleteCustomerAsync(1)).ReturnsAsync(customerData[0]);
            CustomerRepos.Setup(x => x.DeleteCustomerAsync(1)).ReturnsAsync(customerData[0]);

            //act         
            var Response = await _service.DeleteCustomerAsync(1);
            var result = await controller.DeleteCustomer(1);

            //assert            
            Assert.IsType<OkObjectResult>(result);
            var result1 = result as OkObjectResult;
            Assert.Equal("Removed Successfully", result1.Value);
        }


        [Fact(DisplayName = "Controller DELETE returns 400 error")]

        public async Task DeleteCustomer_ShouldReturn400Status()
        {
            var customerData = new List<Customer>
            {
            new Customer
                {
                AccountNumber = 1,
                CustomerName = "karishmaa",
                Email = "karish@gmail.com",
                Password = "karish",
                PhoneNumber = 9876543021,
                CurrentBalance = 10000

                },
            };

            //arrange
            CustomerRepos.Setup(x => x.DeleteCustomerAsync(1)).ReturnsAsync(customerData[0]);

            //act            
            var result = await controller.DeleteCustomer(5);

            //assert            
            Assert.IsType<ObjectResult>(result);
            var result1 = result as ObjectResult;
            var actualresult = result1.Value as ProblemDetails;
            Assert.Equal("400", actualresult.Status.ToString());
            Assert.Equal("unable to delete customer: 5", actualresult.Detail.ToString());
        }


        [Fact(DisplayName = "Controller DELETE returns 500 error")]
        public async Task DeleteCustomer_shouldReturn500()
        {
            // Arrange
            CustomerRepos.Setup(x => x.DeleteCustomerAsync(1)).Throws(new Exception());

            //act
            var ActualResult = await controller.DeleteCustomer(1);

            //assert
            Assert.Equal("500", ((ObjectResult)ActualResult).StatusCode.ToString());
        }

    }
}
