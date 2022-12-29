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
        public TransactionType TransactionType { get; }

        public Transaction(double amount, TransactionType transactionType)
        {
            this.amount = amount;
            this.transactionDate = DateProvider.getInstance().Now();
            TransactionType = transactionType;
        }
    }
}
