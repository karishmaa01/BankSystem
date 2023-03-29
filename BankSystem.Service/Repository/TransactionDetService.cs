using AutoMapper;
using BankSystem.Domain.Enum;
using BankSystem.Domain.Interface;
using BankSystem.Domain.Model;
using BankSystem.Service.Interface;
using BankSystem.Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BankSystem.Domain.Interface.ITransactionDet;
using static BankSystem.Service.Interface.ITransactionDetRepository;

namespace BankSystem.Service.Repository
{
    public class TransactionDetService : ITransactionDetRepository
    {

        private readonly ITransactionDet _transaction;
        private readonly IMapper _mapper;

        public TransactionDetService(ITransactionDet transaction, IMapper mapper)
        {
            _transaction = transaction;
            _mapper = mapper;
        }

        public async Task<TransactionDet> DoTransaction(TDetails tdetails, PaymentType type)
        {
            return _mapper.Map<TransactionDet>(await _transaction.DoTransaction(_mapper.Map <TransactionDet> (tdetails), type));
        }
       
        public async Task<List<TransactionDet>> GetAllTransactionAsync()
        {
            return await _transaction.GetAllTransactionAsync();
        }

        public async Task<List<TransactionDet>> GetByTransactionDate(int AccountNumber, DateTime Fromdate, DateTime Todate)
        {
            return await _transaction.GetByTransactionDate(AccountNumber, Fromdate, Todate);
        }

        public async Task<List<TransactionDet>> GetLastTransactionAsync(int AccountNumber)
        {
            return await _transaction.GetLastTransactionAsync(AccountNumber);
        }

        public async Task<List<TransactionDet>> GetTransactionByIdAsync(int AccountNumber)
        {
            return await _transaction.GetTransactionByIdAsync(AccountNumber);
        }
    }
}

