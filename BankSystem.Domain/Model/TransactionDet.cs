using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Enum;

namespace BankSystem.Domain.Model
{
    public class TransactionDet
    {
        [Key]
        public int TransactionId { get; set; }

        public long PayAmount { get; set; }

        public PaymentType PaymentMethod { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("Customer")]
        public int AccountNumber { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
