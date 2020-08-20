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

        public virtual void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "must be greater than zero");
            }

            Transactions.Add(new Transaction(amount));
        }

        public virtual void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "must be greater than zero");
            }

            Transactions.Add(new Transaction(-amount));
        }

        public virtual void Transfer(Account accountTransferingTo, double amount)
        {
            Transaction withdrawal = new Transaction(-amount);
            Transactions.Add(withdrawal);

            try
            {
                accountTransferingTo.Deposit(amount);
            }
            catch
            {
                // Ensure that if something goes wrong during the deposit that the original
                // withdraw transaction on this account is removed so funds are not lost.
                Transactions.Remove(withdrawal);
                throw;
            }
        }
    }
}
