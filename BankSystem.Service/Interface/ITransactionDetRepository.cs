using BankSystem.Domain.Enum;
using BankSystem.Domain.Model;
using BankSystem.Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Service.Interface
{
    public interface ITransactionDetRepository
    {
        Task<List<TransactionDet>> GetAllTransactionAsync();

        Task<List<TransactionDet>> GetTransactionByIdAsync(int AccountNumber);

        Task<List<TransactionDet>> GetByTransactionDate(int AccountNumber, DateTime Fromdate, DateTime Todate);

        Task<TransactionDet> DoTransaction(TDetails tdetails, PaymentType type);

        Task<List<TransactionDet>> GetLastTransactionAsync(int AccountNumber);



    }
}

