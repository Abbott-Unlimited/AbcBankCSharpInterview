using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public abstract class Account
    {
        public List<Transaction> transactions;
        public double Ballance { get; private set; }


        public abstract AccountType AccountType { get; }

        public Account()
        {
            this.transactions = new List<Transaction>();
            this.Ballance = 0.0;
        }
        public void Deposit(double amount, bool isTransferDeposit)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                if (isTransferDeposit) transactions.Add(new Transaction(amount, TransactionType.TransferIn));
                else transactions.Add(new Transaction(amount, TransactionType.Deposit));
                Ballance += amount;
            }
        }

        public void Withdraw(double amount, bool isTransferWithdrawal)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                if (isTransferWithdrawal) transactions.Add(new Transaction(-amount, TransactionType.TransferOut));
                else transactions.Add(new Transaction(-amount, TransactionType.Withdrawal));
                Ballance -= amount;
            }
        }


        public double sumTransactions()
        {
            return CheckIfTransactionsExist(true);
        }

        private double CheckIfTransactionsExist(bool checkAll)
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        } 

        public abstract double InterestEarned();

    }
}
