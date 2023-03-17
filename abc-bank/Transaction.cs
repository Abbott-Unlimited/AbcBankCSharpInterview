using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Transaction
    {
        private readonly double _amount;
        private DateTime _transactionDate;

        public Transaction(double amount) 
        {
            _amount = amount;
            _transactionDate = DateProvider.getInstance().Now();
        }

        public Transaction(double amount, DateTime transactionDate)
        {
            _amount = amount;
            _transactionDate = transactionDate;
        }

        public double TransactionAmount
        {
            get { return _amount; }
        }

        public DateTime TransactionDate
        {
            get { return _transactionDate; }
        }
    }
}
