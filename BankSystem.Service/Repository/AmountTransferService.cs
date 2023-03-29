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

namespace BankSystem.Service.Repository
{
    public class AmountTransferService : IAmountTransferRepository
    {

        private readonly IAmountTransfer _transaction;
        public readonly IMapper _mapper;
        

        public AmountTransferService(IAmountTransfer transaction, IMapper mapper)
        {
            _transaction = transaction;
            _mapper = mapper;
            
        }
        public async Task<AmountTransfer> AmountTransaction(ATMapping ATMapping, TransType type)
        {
            return _mapper.Map<AmountTransfer> (await _transaction.AmountTransaction(_mapper.Map<AmountTransfer>(ATMapping), type));
           
        }

        public async Task<List<ATMapping>> GetAllTransactionAsync()
        {
            var amount = await _transaction.GetAllTransactionAsync();
            return _mapper.Map<List<ATMapping>>(amount);
        }

        public async Task<List<AmountTransfer>> GetByTransactionDate(int AccountNumber, DateTime Fromdate, DateTime Todate)
        {
            return await _transaction.GetByTransactionDate(AccountNumber, Fromdate, Todate);
        }

        public async Task<List<AmountTransfer>> GetLastTransactionAsync(int AccountNumber)
        {
            return await _transaction.GetLastTransactionAsync(AccountNumber);
        }
    }
}
