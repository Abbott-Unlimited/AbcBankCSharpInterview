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

        private DateTime transactionDate;

        public Transaction(double amount) 
        {
            this.amount = amount;
            this.transactionDate = DateProvider.getInstance().Now();
        }
        public DateTime GetDate() => transactionDate;

        /// <summary>
        /// Only use this for testing.
        /// </summary>
        /// <param name="dateTestOnly">only a test date</param>
        public void SetDate(DateTime dateTestOnly) => this.transactionDate = dateTestOnly;
    }
}
