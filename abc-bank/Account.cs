using System;
using System.Collections.Generic;
using System.Linq;

namespace abc_bank
{
    public abstract class Account
    {
        public virtual string Name => "Account";

        public virtual double InterestEarned => Balance * 0.001;

        public List<Transaction> Transactions { get; } = new List<Transaction>();

        public double Balance => Transactions.Sum(transaction => transaction.Amount);

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "must be greater than zero");
            }

            Transactions.Add(new Transaction(amount));
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "must be greater than zero");
            }

            Transactions.Add(new Transaction(-amount));
        }
    }
}
