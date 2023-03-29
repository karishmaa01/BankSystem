using BankSystem.Domain.Enum;
using BankSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Interface
{
    public interface IAmountTransfer
    {
        Task<List<AmountTransfer>> GetAllTransactionAsync();
        Task<List<AmountTransfer>> GetByTransactionDate(int AccountNumber, DateTime Fromdate, DateTime Todate);
        Task<AmountTransfer> AmountTransaction(AmountTransfer ATMapping, TransType type);
        Task<List<AmountTransfer>> GetLastTransactionAsync(int AccountNumber);

    }
}
