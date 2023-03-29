using AutoMapper;
using BankSystem.Domain.Enum;
using BankSystem.Domain.Interface;
using BankSystem.Domain.Model;
using BankSystem.DTO;
using BankSystem.Service.Interface;
using BankSystem.Service.Mapping;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BankSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmountTransferController : Controller
    {
        private readonly IAmountTransferRepository _transactions;
        private readonly IMapper _mapper;

        public AmountTransferController(IAmountTransferRepository transactions, IMapper mapper)
        {
            _transactions = transactions;
            _mapper = mapper;

        }

        #region Get All Amount Transfer
        /// <summary>
        /// This method is for Get All AmountTransfer
        /// </summary>
        /// <response code="200">Returns a list of Amount Transactions.</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]

        [HttpGet("GetAllTransaction")]
        public async Task<ActionResult<List<ATDto>>> GetAllTransactions()
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

        #region Get Recent Amount Transfer
        /// <summary>
        /// This method is for Get Recent AmountTransfer
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <response code="200">Returns a list of Recent Amount Transactions.</response>
        [HttpGet("GetLastTransaction")]
        public async Task<ActionResult<List<AmountTransfer>>> GetLastTransactionAsync(int AccountNumber)
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

        #region Create Amount Transfer
        /// <summary>
        /// This method is to create AmountTransfer
        /// </summary>
        /// <param name="atdto"></param>
        /// <param name="type"></param>
        /// <response code="200">Returns a Amount Transactions.</response>
        [HttpPost("Transfer")]
        public async Task<ActionResult> Transfer(ATDto atdto, TransType type)
        {
            try
            {
                var Transaction = await _transactions.AmountTransaction(_mapper.Map<ATMapping>(atdto), type);
                //if (Transaction == null)
                //{
                //    return NoContent();
                //}
                return Ok("Transaction Successful");
               
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        #endregion

        #region Get By Date Amount Transfer
        /// <summary>
        /// This method is for Get by date AmountTransfer
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <param name="Fromdate"></param>
        /// <param name="Todate"></param>
        /// <response code="200">Returns a List of Date Amount Transactions.</response>
        [HttpGet("GetByDate")]
        public async Task<ActionResult<AmountTransfer>> GetByDate(int AccountNumber, DateTime Fromdate, DateTime Todate)
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
    }
}
