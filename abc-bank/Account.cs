using System;
using System.Collections.Generic;

namespace abc_bank
{
    public class Account
    {

        public const int CHECKING = 0;
        public const int SAVINGS = 1;
        public const int MAXI_SAVINGS = 2;

        public const double POINT_ONE_PERCENT_INTEREST = 0.001;
        public const double POINT_TWO_PERCENT_INTEREST = 0.002;
        public const double TWO_PERCENT_INTEREST = 0.02;
        public const double FIVE_PERCENT_INTEREST = 0.05;
        public const double TEN_PERCENT_INTEREST = 0.1;


        private readonly int accountType;
        public List<Transaction> transactions;

        public Account(int accountType)
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(-amount));
            }
        }


        /// <summary>
        /// Formulas to calculate interest:
        /// Checking accounts have a flat rate of 0.1% - X
        /// Savings accounts have a rate of 0.1% for the first $1,000 then 0.2% - X
        /// Maxi-Savings accounts have a rate of 2% for the first $1,000 then 5% for the next $1,000 then 10% - X
        /// </summary>
        public double InterestEarned()
        {
            double amount = SumTransactions();
            switch (accountType)
            {
                case CHECKING:
                    return amount * POINT_ONE_PERCENT_INTEREST;
                case SAVINGS:
                    if (amount <= 1000)
                        return amount * POINT_ONE_PERCENT_INTEREST;
                    else
                        return amount * POINT_TWO_PERCENT_INTEREST;
                case MAXI_SAVINGS:
                    if (amount <= 1000)
                        return amount * TWO_PERCENT_INTEREST;
                    if (amount <= 2000)
                        return amount * FIVE_PERCENT_INTEREST;
                    return amount * TEN_PERCENT_INTEREST;
                default:
                    return amount * POINT_ONE_PERCENT_INTEREST;
            }
        }

        public double SumTransactions()
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

        public int GetAccountType()
        {
            return accountType;
        }

    }
}
