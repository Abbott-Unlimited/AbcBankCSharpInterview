using System;
using System.Collections.Generic;

namespace abc_bank
{
    public class Account
    {
        public AccountType accountType {get; set;}

        public List<Transaction> transactions;

        public Account(AccountType accountType) 
        {
            this.accountType = accountType;
            transactions = new List<Transaction>();
        }

        public void AddTransaction(double amount)
        {
            if (amount == 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(amount));
            }
        }

        public double CalculateInterestEarned() 
        {
            double amount = CalculateBalance();
            switch(accountType){
                case AccountType.SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001;
                    else
                        return 1 + (amount-1000) * 0.002;
                case AccountType.MAXI_SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.02;
                    if (amount <= 2000)
                        return 20 + (amount-1000) * 0.05;
                    return 70 + (amount-2000) * 0.1;
                default:
                    return amount * 0.001;
            }
        }

        public double CalculateBalance() {
            double amount = 0.0;
            foreach (var t in transactions)
                amount += t.amount;
            return amount;
        }
    }
}
