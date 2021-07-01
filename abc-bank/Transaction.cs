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
        public readonly DateTime transactiondate;

        public Transaction(double amount, DateTime transactiondate) 
        {
            this.amount = amount;
            this.transactiondate = transactiondate;
            //this.transactionDate = DateProvider.getInstance().Now();
        }
    }
}
