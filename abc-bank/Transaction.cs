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

        //private DateTime transactionDate;
        public DateTime TransactionDate { get; private set; }
        public double Amount { get { return amount; } }

        public Transaction(double amount) 
        {
            this.amount = amount;
            TransactionDate = DateProvider.getInstance().Now();
        }
    }
}
