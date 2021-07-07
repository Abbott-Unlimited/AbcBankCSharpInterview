namespace ABC_bank
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Transaction
    {
        public readonly double Amount;
        private readonly DateTime transactionDate;

        public Transaction(double amount) 
        {
            this.Amount = amount;
            this.transactionDate = DateProvider.GetInstance().Now();
        }
    }
}
