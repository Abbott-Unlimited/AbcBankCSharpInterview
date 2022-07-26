using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Transaction
    {

        public const int DEPOSIT = 0;
        public const int WITHDRAW = 1;
        public const int TRANSFER = 2;

        public readonly int transactionType;

        public readonly double amount;

        //RPM 20220726 changing from private in order to make accessible for MAXISAVINGS calculation
        public readonly DateTime transactionDate;

        public Transaction(double amount, int transType=0) 
        {
            this.amount = amount;
            this.transactionType = transType;
            this.transactionDate = DateProvider.getInstance().Now();
        }

    }
}
