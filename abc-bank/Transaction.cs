using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public enum TransactionType
    {
        WITHDRAWEL,
        DEPOSIT
    }
    public class Transaction
    {
        public readonly double amount;

        public DateTime transactionDate;

        public TransactionType transactionType;

        public Transaction(double amount, TransactionType transactionType, DateTime transactionDate) 
        {
            this.amount = amount;
            this.transactionDate = transactionDate;
            this.transactionType = transactionType;
        }
    }
}
