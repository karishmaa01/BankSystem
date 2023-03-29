using AutoMapper;
using Azure.Core;
using BankSystem.Domain.Data;
using BankSystem.Domain.Enum;
using BankSystem.Domain.Model;
using BankSystem.DTO;
using BankSystem.Service.Interface;
using BankSystem.Service.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;

namespace BankSystem.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {

        private readonly ICustomerRepository _customers;
        private readonly IMapper _mapper;


        public CustomerController(ICustomerRepository customers, IMapper mapper)
        {
            _customers = customers;
            _mapper = mapper;
        }



        #region Get customer
        /// <summary>
        /// This method is for GetAllCustomer
        /// </summary>
        /// <response code="200">Returns a list of customer.</response>
        [HttpGet("GetAllCustomer")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CustomerMapping>>> GetCustomers()
        {
            try
            {
                var customer = await _customers.GetAllCustomerAsync();
                //var MapperData = _mapper.Map<List<CustomerDto>>(customer);
                if (customer.Count == 0)
                {
                    return NoContent();
                }
                return Ok(customer);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);

                //return BadRequest(ex.Message);
            }


        }
        #endregion


        #region GetById
        /// <summary>
        /// This method is for Get Customer By ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns a list of customer by ID.</response>
        [HttpGet("GetCustomerById")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Customer>> GetCustomer(int AccountNumber)
        {
            try
            {
                //if (AccountNumber <= 0)
                //    throw new Exception("account number not less than zero");
                var customer = await _customers.GetCustomerByIdAsync(AccountNumber);
                if (customer == null)
                {
                    return NoContent();
                }

                return Ok(customer);
                //return Ok(await _customers.GetCustomerByIdAsync(AccountNumber));
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        #endregion


        #region PostCustomer
        /// <summary>
        /// This method ids for Create a new Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <response code="200">create a customer.</response>
        [HttpPost("CreateCustomer")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            try
            {
                if (string.IsNullOrEmpty(customer.CustomerName))
                    return Problem(statusCode: 400, detail: $"Name should not be empty");

                return Ok(await _customers.CreateCustomerAsync(customer));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        #endregion


        #region DeleteCustomer
        /// <summary>
        /// This method is used to Delete Customer
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Delete the particular customer.</response>
        [HttpDelete("DeleteCustomer")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCustomer(int AccountNumber)
        {
            try
            {
                var customer = await _customers.DeleteCustomerAsync(AccountNumber);
                if (customer == null)
                {
                    return Problem(statusCode: 400, detail: $"unable to delete customer: {AccountNumber}");
                }
                return Ok("Removed Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        #endregion

        #region PutCustomer
        /// <summary>
        /// This method is used to Update Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <response code="200">Update the particular customer.</response>
        [HttpPut("UpdateCustomer")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Customer>> UpdateCustomer(Customer customer, int AccountNumber)
        {
            try
            {
                if (customer.CustomerName == null)
                {
                    return Problem(statusCode: 400, detail: $"Name should not be empty");
                }
                var details = await _customers.UpdateCustomerAsync(customer, AccountNumber);
                return Ok("update Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        #endregion


        ///// <summary>
        ///// This method is for page by page data
        ///// </summary>
        ///// <param name="PageNo"></param>
        ///// <param name="sort_by"></param>
        ///// <param name="Sort"></param>
        ///// <param name="CustomerName"></param>
        ///// <response code="200">Shows particalar page.</response>
        //[HttpGet("GetAllcustomerByPage")]
        //public async Task<ActionResult<List<Customer>>> GetPageCustomer(int PageNo, string Sort_by, SortType Sort, string CustomerName = "")
        //{

        //    IQueryable<Customer> query = _context.Customers;
        //    switch (Sort_by)
        //    {
        //        case "SortType" :
        //            if (Sort == SortType.Ascending)
        //            {
        //                query = query.OrderBy(c => c.CustomerName);
        //            }
        //            else
        //            {
        //                query = query.OrderByDescending(c => c.CustomerName);
        //            }
        //            break;

        //    }
        //    if (!string.IsNullOrEmpty(CustomerName))
        //    {
        //        query = query.Where(c => c.CustomerName.Contains(CustomerName));
        //    }

        //    var customer = await _context.Customers.ToListAsync();
        //    if (CustomerName != "")
        //    {
        //        customer = await _context.Customers
        //       .Where(b => b.CustomerName.Contains(CustomerName))
        //       .ToListAsync();
        //    }
        //    var pageResults = 3f;
        //    var pageCount = Math.Ceiling(customer.Count() / pageResults);
        //    var retcus = customer.Skip((PageNo - 1) * (int)pageResults)
        //        .Take((int)pageResults)
        //        .ToList();
        //    var pagination = new Pagging
        //    {
        //        customer = retcus,
        //        CurrentPage = PageNo,
        //        Pages = (int)pageCount,
        //    };
        //    return Ok(pagination);
        //}
    }
}
