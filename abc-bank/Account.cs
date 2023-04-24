using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public abstract class Account
    {
        public abstract string AccountName { get; }

        private readonly List<Transaction> _transactions  = new List<Transaction>();

        public TransactionResult RequestDeposit(decimal amount)
        {
            if (amount <= 0)
            { return TransactionResult.Fail("amount must be greater than zero"); }

            _transactions.Add(new Transaction(amount));

            return TransactionResult.Ok();
        }

        public TransactionResult RequestWithdrawal(decimal amount)
        {
            if (amount <= 0)
            { return TransactionResult.Fail("amount must be greater than zero"); }

            decimal currentBalance = SumTransactions();
            if (currentBalance - amount < 0)
            { return TransactionResult.Fail("not enough money in account for withdrawal"); }

            _transactions.Add(new Transaction(-amount));

            return TransactionResult.Ok();
        }

        public decimal SumTransactions() 
        {
            decimal sum = 0;

            foreach (Transaction transaction in _transactions)
            {
                sum += transaction.Amount;
            }
            return sum;
        }

        public List<Transaction> GetTransactions()
        {
            return _transactions;
        }

        public abstract decimal GetInterestEarned();
    }
}
