using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Transaction
    {
        public readonly double Amount;
        public readonly TransactionType TransactionType;

        public readonly DateTime TransactionDate;

        public Transaction(double amount, TransactionType transactionType,
            DateTime transactionDate)
        {
            Amount = amount;
            TransactionType = transactionType;
            TransactionDate = transactionDate;
        }
    }
}