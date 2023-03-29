using BankSystem.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Service.Mapping
{
    public class ATMapping
    {
        public TransType TransactionType { get; set; }

        public DateTime TransactionDate { get; set; }

        public long TransactionAmount { get; set; }

        public int DestinationAccountNumber { get; set; }

        public int AccountNumber { get; set; }
    }
}
