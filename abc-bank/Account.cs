using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public abstract class Account : IAccount
    {

        private readonly IList<ITransaction> Transactions;

        public Account()
        {
            Transactions = new List<ITransaction>();
        }

        public void Deposit(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                Transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                Transactions.Add(new Transaction(-amount));
            }
        }

        public abstract double GetInterestEarned();

        public double GetAccountBalance()
        {
            return Transactions.Sum((tran) => tran.GetAmount());
        }

        public IList<ITransaction> GetTransactions()
        {
            return Transactions;
        }

        protected abstract string GetAccountTypeString();

        public override string ToString()
        {
            String s = "";

            s += (GetAccountTypeString() + "\n");

            double total = 0.0;
            foreach (ITransaction t in GetTransactions())
            {
                s += t.ToString();
                total += t.GetAmount();
            }
            s += "Total " + total.ToString("c");
            return s;
        }

    }
}
