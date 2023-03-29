using BankSystem.Domain.Enum;

namespace BankSystem.DTO
{
    public class ATDto
    {
        public TransType TransactionType { get; set; }

        public DateTime TransactionDate { get; set; }

        public long TransactionAmount { get; set; }

        public int DestinationAccountNumber { get; set; }

        public int AccountNumber { get; set; }
    }
}
