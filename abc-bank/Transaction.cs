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
            //I think we can simply call DateTime.UtcNow and storing in UTC is a good practice I believe
            this.transactionDate = DateTime.UtcNow; // DateProvider.getInstance().Now();
        }
    }
}
