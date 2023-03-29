using BankSystem.Domain.Data;
using BankSystem.Domain.Enum;
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
    public class AmountTransferRepository : IAmountTransfer
    {
        private readonly BankDb _context;
        private readonly ICustomer _service;

        public AmountTransferRepository(BankDb context, ICustomer service)
        {
            _context = context;
            _service = service;
        }

        public async Task<AmountTransfer> AmountTransaction(AmountTransfer ATMapping, TransType type) //dont use minimum
        {
            var trans = new AmountTransfer();
            if (type == TransType.Deposit)
            {
                var AccountNumber = await _service.GetCustomerByIdAsync(ATMapping.AccountNumber);
                var DestinationAccount = await _service.GetCustomerByIdAsync(ATMapping.DestinationAccountNumber);
                //decimal min = 100;
                if (AccountNumber == null)
                { 
                    return null;
                }
                else
                {
                    var Details = new AmountTransfer
                    {
                        AccountNumber = ATMapping.AccountNumber,
                        DestinationAccountNumber = ATMapping.DestinationAccountNumber,
                        TransactionDate = DateTime.Now,
                        TransactionAmount = ATMapping.TransactionAmount,
                        TransactionType = type,
                        Customer = AccountNumber
                    };
                    //var cc = AccountNumber.CurrentBalance - ATMapping.TransactionAmount;
                    //if (cc <= min)
                    //{
                    //    response.ResponseMessage = "Insufficient Balance";
                    //}
                    //else
                    //{
                    //    AccountNumber.CurrentBalance -= ATMapping.TransactionAmount;
                    //    DestinationAccount.CurrentBalance += ATMapping.TransactionAmount;
                        await _context.AmountTransfers.AddAsync(Details);
                        await _context.SaveChangesAsync();
                    //    ///response.ResponseMessage = "Transaction Successfull";
                    //}
                    return Details;
                }
                
            }
            return (ATMapping);
        }


        public async Task<List<AmountTransfer>> GetAllTransactionAsync()
        {
            return await _context.AmountTransfers.Include(u => u.Customer).ToListAsync();
        }

        public async Task<List<AmountTransfer>> GetByTransactionDate(int AccountNumber, DateTime Fromdate, DateTime Todate)
        {
            var acc = await _context.Customers.FindAsync(AccountNumber);
            if (acc == null)
                return null;
            var Transaction = _context.AmountTransfers.Where(b => b.AccountNumber == AccountNumber).Where(i => i.TransactionDate >= Fromdate && (i.TransactionDate <= Todate)).ToList();
            return (Transaction);
        }

        public async Task<List<AmountTransfer>> GetLastTransactionAsync(int AccountNumber)
        {
            var acc = await _context.Customers.FindAsync(AccountNumber);
            if (acc == null)
                return null;
            var Transaction = _context.AmountTransfers.Where(b => b.AccountNumber == AccountNumber).OrderByDescending(x => x.TransactionId).Take(5).ToList();
            return (Transaction);
        }
    }
}

