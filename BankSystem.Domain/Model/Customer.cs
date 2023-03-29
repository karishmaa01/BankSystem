using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Model
{
    public class Customer
    {
        [Key]
        public int AccountNumber { get; set; }

        public string CustomerName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [PasswordPropertyText]
        public string Password { get; set; }

        public long PhoneNumber { get; set; }

        public long CurrentBalance { get; set; }
    }
}