using Azure;
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
//using Response = BankSystem.Domain.Model.Response;

namespace BankSystem.Domain.Repository
{
    public class TransactionDetailRepository : ITransactionDet
    {
        private readonly BankDb _context;

        public TransactionDetailRepository(BankDb context)
        {
            _context = context;
        }

        public async Task<TransactionDet> DoTransaction(TransactionDet tdetails, PaymentType type)
        {

            var response = new TransactionDet();
                if (type == PaymentType.Deposit)
                {
                    var Account = _context.Customers.Find(tdetails.AccountNumber);
                    if (Account != null)
                    {
                        var newdetail = new TransactionDet()
                        {
                            PayAmount = tdetails.PayAmount,
                            Date = DateTime.Now,
                            Customer = Account,
                        };

                        Account.CurrentBalance += tdetails.PayAmount;
                        newdetail.PaymentMethod = type;
                        
                        _context.TransactionDets.Add(newdetail);
                        _context.SaveChanges();
                        //response.ResponseMessage = "Transaction Successful!";
                        return newdetail;
                    }
                    //else
                    //{
                    //    response.ResponseMessage = "Transaction Failed!";
                    //    return response;
                    //}
                }
                else
                {
                    var Account = _context.Customers.Find(tdetails.AccountNumber);
                    if (Account != null)
                    {
                        var newdetail = new TransactionDet()
                        {
                            PayAmount = tdetails.PayAmount,
                            Date = DateTime.Now,
                            Customer = Account,
                        };

                        Account.CurrentBalance -= tdetails.PayAmount;
                        newdetail.PaymentMethod = type;

                        _context.TransactionDets.Add(newdetail);
                        _context.SaveChanges();
                        //response.ResponseMessage = "Transaction Successful!";
                        return newdetail;
                    }
                    //else
                    //{
                    //    response.ResponseMessage = "Transaction Failed!";
                    //    return response;
                    //}
                }
            return tdetails;
        }

        public async Task<List<TransactionDet>> GetAllTransactionAsync()
        {
            return await _context.TransactionDets.Include(u=>u.Customer).ToListAsync();
        }

        public async Task<List<TransactionDet>> GetByTransactionDate(int AccountNumber, DateTime Fromdate, DateTime Todate)
        {

            var acc = await _context.Customers.FindAsync(AccountNumber);
            if (acc == null)
                return null;
            var Transaction =await _context.TransactionDets.Where(b => b.AccountNumber == AccountNumber).Where(i => i.Date >= Fromdate && (i.Date <= Todate)).ToListAsync();
            return (Transaction);
        }

        public async Task<List<TransactionDet>> GetLastTransactionAsync(int AccountNumber)
        {
            var acc = await _context.Customers.FindAsync(AccountNumber);
            if (acc == null)
                return null;
            var Transaction =await _context.TransactionDets.Where(b => b.AccountNumber == AccountNumber).OrderByDescending(x => x.TransactionId).Take(5).ToListAsync();
            return (Transaction);
        }

        public async Task<List<TransactionDet>> GetTransactionByIdAsync(int AccountNumber)
        {
            var acc = await _context.Customers.FindAsync(AccountNumber);
            if (acc == null)
                return null;
            var cc = await _context.TransactionDets.Include(a => a.Customer).Include(b => b.Customer).Where(b => b.AccountNumber == AccountNumber).ToListAsync();

            return (cc);
            //var cc = await _context.TransactionDets.Include(a => a.Customer).Include(b => b.Customer).Where(b => b.AccountNumber == AccountNumber).ToListAsync();
            //return (cc);
            ////var user = await _context.Customers.FindAsync(AccountNumber);
            //var Transaction = await _context.TransactionDets.Where(b => b.AccountNumber == AccountNumber).ToListAsync();
            //if (Transaction == null)
            //    return null;
            //return (Transaction);

        }
    }
}
