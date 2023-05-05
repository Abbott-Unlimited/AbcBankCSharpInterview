using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Transaction
    {
        public const int WITHDRAWAL = 0;
        public const int DEPOSIT = 1;

        public readonly double amount;
        private readonly int transactionType;
        private DateTime transactionDate;


        public Transaction(double amount, int transactionType) 
        {
            this.amount = amount;
            this.transactionDate = DateProvider.GetInstance().Now();
            this.transactionType = transactionType;
        }

        public int GetTransactionType()
        {
            return transactionType;
        }

        public DateTime GetTransactionDate()
        {
            return transactionDate;
        }
    }
}
