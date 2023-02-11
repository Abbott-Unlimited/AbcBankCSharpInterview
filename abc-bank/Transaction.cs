using abc_bank.Services.DateServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Transaction
    {
        public readonly decimal amount;

        private DateTime transactionDate;

        public Transaction(decimal amount, DateTime? transactionDate = null) 
        {
            this.amount = amount;
            this.transactionDate = transactionDate == null ? DateProvider.getInstance().Now() : transactionDate.Value;
        }

        public DateTime GetTransactionDate()
        {
            return this.transactionDate;
        }
    }
}
