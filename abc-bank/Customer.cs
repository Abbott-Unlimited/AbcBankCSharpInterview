﻿using System.Collections.Generic;
using System.Linq;

namespace abc_bank
{
    public class Customer
    {
        public int ID;
        public string Name;
        public List<Account> Accounts;

        public Customer(string name)
        {
            Name = name;
            Accounts = new List<Account>();
        }

        public Customer Transfer(Account fromAccount, Account toAccount, double amount)
        {
            if(fromAccount.GetBalance() >= amount)
            {
                fromAccount.Withdraw(amount);
                toAccount.Deposit(amount);
            }

            return this;
        }

        public Customer OpenAccount(Account account)
        {
            Accounts.Add(account);
            return this;
        }

        public double TotalInterestEarned() 
        {
            return Accounts.Sum(account => account.InterestEarned());
        }

        public string GetStatement() 
        {
            var result = "Statement for " + Name + "\n\n";

            double total = 0.0;
            Accounts.ForEach(account =>
            {
                result += account.GetStatement() + "\n\n";
                total += account.GetBalance();
            });

            result += "Total In All Accounts " + total.ToString("C");
            return result;
        }
    }
}
