using System;
using System.Collections.Generic;
using System.Configuration;

namespace abc_bank
{
    public class Account
    {

        public const int CHECKING = 0;
        public const int SAVINGS = 1;
        public const int MAXI_SAVINGS = 2;

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
                if (sumTransactions() < amount)
                {
                    throw new ArgumentException("amount must be less than current values");
                }
                else
                {
                    transactions.Add(new Transaction(-amount));
                }
                
            }
        }

        public double InterestEarned()
        {
            double amount = sumTransactions();

            //interest amounts 
            double CheckingRate = Convert.ToDouble(ConfigurationManager.AppSettings["CheckingRate"]);
            double SavingsRateLow = Convert.ToDouble(ConfigurationManager.AppSettings["SavingsRateLow"]);
            double SavingsRateHigh = Convert.ToDouble(ConfigurationManager.AppSettings["SavingsRateHigh"]);
            double MaxSavingsRateLow = Convert.ToDouble(ConfigurationManager.AppSettings["MaxSavingsRateLow"]);
            double MaxSavingsRateMid = Convert.ToDouble(ConfigurationManager.AppSettings["MaxSavingsRateMid"]);
            double MaxSavingsRateMax = Convert.ToDouble(ConfigurationManager.AppSettings["MaxSavingsRateMax"]);

            switch (accountType)
            {
                case SAVINGS:
                    double tempSavingsInterest = 0.00;
                    if (amount <= 1000)
                    {
                        tempSavingsInterest = amount * SavingsRateLow;
                    }
                    else
                    {
                        tempSavingsInterest = (1000 * SavingsRateLow) + ((amount - 1000) * SavingsRateHigh);
                    }
                    return tempSavingsInterest;
                case MAXI_SAVINGS:
                    double tempMaxInterest = 0.00;
                    if (amount <= 1000)
                    {
                        tempMaxInterest = amount * MaxSavingsRateLow;
                    }
                    else if (amount <= 2000)
                    {
                        tempMaxInterest = (1000 * MaxSavingsRateLow) + ((amount - 1000) * MaxSavingsRateMid);
                    }
                    else
                    {
                        tempMaxInterest = (1000 * MaxSavingsRateLow) + ((1000) * MaxSavingsRateMid) + ((amount - 2000) * MaxSavingsRateMax);
                    }
                    return tempMaxInterest;
                default: //checking account
                    return amount * CheckingRate;
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

        public int GetAccountType()
        {
            return accountType;
        }

    }
}
