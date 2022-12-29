using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class SavingAccount : Account
    { 
        public override AccountType AccountType { get; } = AccountType.SAVINGS;

        public override double InterestEarned()
        {
            DateTime lastDepositDate = transactions.Where(t => t.TransactionType == TransactionType.Deposit || t.TransactionType == TransactionType.TransferIn).OrderByDescending(t => t.transactionDate).FirstOrDefault().transactionDate;
            TimeSpan timeSpan = DateTime.Now - lastDepositDate;
            int numOfDays = timeSpan.Days;
            return (sumTransactions() * Math.Pow((1 + 0.02 / 365), numOfDays)) - sumTransactions();

        }
    }
}
