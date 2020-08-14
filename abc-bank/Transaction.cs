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

        public Transaction(double amount, DateTime? date = null) 
        {
            this.amount = amount;
            this.transactionDate = date?? DateProvider.getInstance().Now();
        }
    }
}
