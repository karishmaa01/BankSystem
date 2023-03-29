using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Service.Mapping
{
    public class CustomerMapping
    {
        public int AccountNumber { get; set; }
        public string? CustomerName { get; set; }
        public string? Email { get; set; }
    }
}
