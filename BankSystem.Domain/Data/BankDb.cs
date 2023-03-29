using BankSystem.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Data
{
    public class BankDb : DbContext
    {
        public BankDb(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<TransactionDet> TransactionDets { get; set; }

        public DbSet<AmountTransfer> AmountTransfers { get; set; }
    }
}
