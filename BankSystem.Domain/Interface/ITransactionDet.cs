using Azure;
using BankSystem.Domain.Enum;
using BankSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Response = BankSystem.Domain.Model.Response;

namespace BankSystem.Domain.Interface
{
    public interface ITransactionDet
    {
        Task<List<TransactionDet>> GetAllTransactionAsync();

        Task<List<TransactionDet>> GetTransactionByIdAsync(int AccountNumber);

        Task<List<TransactionDet>> GetByTransactionDate(int AccountNumber, DateTime Fromdate, DateTime Todate);

        Task<TransactionDet> DoTransaction(TransactionDet tdetails, PaymentType type);

        Task<List<TransactionDet>> GetLastTransactionAsync(int AccountNumber);

    }
}

