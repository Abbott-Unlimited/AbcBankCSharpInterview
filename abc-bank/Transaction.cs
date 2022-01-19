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

        private DateTime transactionDate;

        public Transaction(double amount) 
        {
            this.amount = amount;
            this.transactionDate = DateProvider.getInstance().Now();
        }

        // Added for additional features
        public Transaction(double amount, DateTime testingDate)
        {
            this.amount=amount;
            this.transactionDate = testingDate;
        }

        public int DaysSinceTransaction()
        {
            return Math.Abs(DateProvider.getInstance().Now().Subtract(this.transactionDate).Days);
        }
    }
}
