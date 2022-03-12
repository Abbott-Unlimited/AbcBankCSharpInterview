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

        private readonly DateTime transactionDate;

        public Transaction(double amount) 
        {
            this.amount = amount;
            this.transactionDate = DateProvider.GetInstance().Now();
        }

        public Transaction(double amount, DateTime transDate)
        {
            this.amount = amount;
            this.transactionDate = transDate;
        }

        public DateTime GetDate()
        {
            return transactionDate;
        }
    }
}
