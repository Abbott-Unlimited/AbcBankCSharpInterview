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
        public enum TransactionType
        {
            Deposit,
            Withdrawal
        }
        
        public readonly double Amount;

        public DateTime TransactionDate { get; private set; }

        public TransactionType Type { get; private set; }

        public Transaction(IDateProvider dateProvider, double amount, TransactionType type) 
        {
            if (amount < double.MinValue || amount > double.MaxValue)
            {
                throw new ArgumentOutOfRangeException("Amount is out of the range of the type double.");
            }
            
            this.Amount = amount;
            this.Type = type;
            this.TransactionDate = dateProvider.Now();
        }
    }
}
