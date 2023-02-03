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

        public DateTime transactionDate;

        public readonly string transactionType;

        public Transaction(double amount, string transactionType) 
        {
            this.amount = amount;
            this.transactionDate = DateProvider.getInstance().Now();
            this.transactionType = transactionType;
        }
    }
}
