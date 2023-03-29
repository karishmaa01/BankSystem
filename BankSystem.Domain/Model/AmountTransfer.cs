using BankSystem.Domain.Enum;
using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace BankSystem.Domain.Model
{
    public class AmountTransfer
    {
        [Key]
        public int TransactionId { get; set; }

        public TransType TransactionType { get; set; }

        public DateTime TransactionDate { get; set; }

        public long TransactionAmount { get; set; }

        public int DestinationAccountNumber { get; set; }

        [ForeignKey("Customer")]
        public int AccountNumber { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
