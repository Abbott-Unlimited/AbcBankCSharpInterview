using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Transaction
    {
        public Transaction() { }
        public DateTime TransactionDate { get; set; }
        public double Amount { get; set; }

        public Transaction(double amount)
        {
            this.Amount = amount;
            this.TransactionDate = DateProvider.getInstance().Now();
        }
    }
}
