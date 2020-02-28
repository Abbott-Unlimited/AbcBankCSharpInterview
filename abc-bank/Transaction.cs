using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Transaction : ITransaction
    {
        private readonly double Amount;

        private DateTime TransactionDate;

        public Transaction(double amount) 
        {
            this.Amount = amount;
            this.TransactionDate = DateProvider.GetInstance().Now();
        }

        public override string ToString()
        {
            return String.Format("  {0} {1}\n",
                    (Amount < 0 ? "withdrawal" : "deposit"),
                    ToDollars(Amount));
        }

        private String ToDollars(double d)
        {
            return Math.Abs(d).ToString("c");
        }

        public DateTime GetTransactionDate()
        {
            return TransactionDate;
        }

        public double GetAmount()
        {
            return Amount;
        }
    }
}
