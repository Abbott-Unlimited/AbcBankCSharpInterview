using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    /// <summary>
    /// Describes a Bank Transaction
    /// </summary>
    public class Transaction
    {        
        public Transaction(DateTime date, Account account, double amount) 
        {
            AccountNumber = account.AccountNumber;
            StartingBalance = account.CurrentBalance;
            Amount = amount;

            // If you are adding a transaction retroactively (i.e., to correct an error), the date may NOT be the current date
            TransactionDate = date == null ? DateProvider.getInstance().Now() : date;
        }

        public DateTime? TransactionDate { get; set; }
        public int AccountNumber { get; set; }
        public double StartingBalance { get; set; }
        public double Amount { get; set; }

        public virtual double Execute()
        {
            return Amount;
        }
    }
}
