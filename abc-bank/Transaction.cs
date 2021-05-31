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

        public enum DepositType
        {
            DEPOSIT = 1,
            WITHDRAW = 2
        }

        public readonly int depositType;

        public Transaction(double amount, DepositType depositType)
        {
            this.depositType = (int)depositType;
            this.amount = amount;
            this.transactionDate = DateProvider.getInstance().Now();
        }
    }
}
