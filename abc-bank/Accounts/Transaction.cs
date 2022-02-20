using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abc_bank.Utilities.Date;

namespace abc_bank.Accounts
{
    public class Transaction
    {
        public readonly double amount;

        private DateTime transactionDate;

        public Transaction(IDateProvider dateProvider, double amount) 
        {
            this.amount = amount;
            this.transactionDate = dateProvider.Now();
        }
    }
}
