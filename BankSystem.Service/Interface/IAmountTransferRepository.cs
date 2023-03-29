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
    public interface IAmountTransferRepository
    {
        Task<List<ATMapping>> GetAllTransactionAsync();
        Task<List<AmountTransfer>> GetByTransactionDate(int AccountNumber, DateTime Fromdate, DateTime Todate);
        Task<AmountTransfer> AmountTransaction(ATMapping ATMapping, TransType type);
        Task<List<AmountTransfer>> GetLastTransactionAsync(int AccountNumber);
    }
}
