using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Transaction
    {
        public readonly double amount;

        public DateTime TransactionDate;

        public Transaction(double amount) 
        {
            this.amount = amount;
            this.TransactionDate = DateProvider.getInstance().Now();
        }
    }
}
