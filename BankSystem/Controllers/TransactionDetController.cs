using BankSystem.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BankSystem.Service.Interface.ITransactionDetRepository;
using Swashbuckle.AspNetCore.Annotations;
using BankSystem.Domain.Data;
using BankSystem.Service.Interface;
using Microsoft.EntityFrameworkCore;
using BankSystem.Domain.Enum;
using BankSystem.Domain.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using BankSystem.DTO;
using AutoMapper;
using Castle.Core.Resource;
using BankSystem.Service.Repository;
using BankSystem.Service.Mapping;

namespace BankSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionDetController : Controller
    {

        private readonly ITransactionDetRepository _transactions;
        private readonly IMapper _mapper;

        public TransactionDetController(ITransactionDetRepository transactions, IMapper mapper)
        {

            _transactions = transactions;
            _mapper = mapper;

        }


        #region Get Transaction 
        /// <summary>
        /// This method is used to Get a transaction for particular customer
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <response code="200">Shows all Transaction.</response>
        [HttpGet("GetTransaction")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TransactionDet>> GetAllTransactions()
        {
            try
            {
                var trans = await _transactions.GetAllTransactionAsync();
                if (trans.Count == 0)
                {
                    return NoContent();
                }
                return Ok(trans);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        #endregion


        #region Getlast5Transaction
        /// <summary>
        /// This method is to Get last 5 Transaction
        /// </summary>
        /// <param name="customerId"></param>
        /// <response code="200">Shows Last 5 Transaction.</response>
        [HttpGet("Last5TransactionId")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<TransactionDet>>> GetLastTransactionAsync(int AccountNumber)
        {
            try
            {
                var Transaction = await _transactions.GetLastTransactionAsync(AccountNumber);
                if (Transaction == null)
                {
                    return NoContent();
                }
                return Ok(Transaction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        #endregion

        #region DoTransaction
        /// <summary>
        /// This method is used to Do transaction
        /// </summary>
        /// <param name="tdetails"></param>
        /// <param name="type"></param>
        /// <response code="200">Do Transaction for particular ID.</response>
        [HttpPost("DoTransaction")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Balance(TransactionDto tdetails, PaymentType type)
        {
            try
            {

                var res = await _transactions.DoTransaction(_mapper.Map<TDetails>(tdetails), type);

                return Ok("Successful Transaction");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        #endregion

        #region Get Transaction By Date
        /// <summary>
        /// This method is to Get transaction By Date
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Fromdate"></param>
        /// <param name="Todate"></param>
        /// <response code="200">Get Transaction By Date.</response>
        [HttpGet("GetByDate")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TransactionDet>> GetByDate(int AccountNumber, DateTime Fromdate, DateTime Todate)
        {
            try
            {
                var Transaction = await _transactions.GetByTransactionDate(AccountNumber, Fromdate, Todate);
                if (Transaction == null)
                {
                    return NoContent();
                }
                return Ok(Transaction);
                //return Ok(await _transactions.GetByTransactionDate(AccountNumber, Fromdate, Todate));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        #endregion


        #region Get Transaction By ID
        /// <summary>
        ///  This method is to Get transaction By ID
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <response code="200">Shows id Transaction.</response>
        [HttpGet("GetByID")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<TransactionDet>>> GetTransactionByIdAsync(int AccountNumber)
        {
            try
            {
                var Transaction = await _transactions.GetTransactionByIdAsync(AccountNumber);
                if (Transaction == null)
                {
                    return NoContent();
                }
                return Ok(Transaction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        #endregion
    }
}