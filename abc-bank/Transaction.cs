using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Transaction
    {
        public decimal Amount { get; }
        public DateTime TransactionDate { get; }
        
        public string TransactionType
        {
            get { return Amount < 0 ? "withdrawal" : "deposit"; }
        }

        public Transaction(decimal amount)
        {
            Amount = amount;
            TransactionDate = DateTime.Now;
        }
    }
}
