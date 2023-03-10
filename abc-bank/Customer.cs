using abc_bank.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Customer
    {
        private readonly string name;
        private List<Account> accounts;

        public Customer(string name)
        {
            this.name = name;
            accounts = new List<Account>();
        }

        public String GetName()
        {
            return name;
        }

        public Account OpenAccount(AccountType accountType)
        {
            Account newAccount;

            switch (accountType)
            {
                case AccountType.Savings:
                    newAccount = new SavingsAccount();
                    break;
                case AccountType.Maxi:
                    newAccount = new MaxiAccount();
                    break;
                default:
                    newAccount = new CheckingAccount();
                    break;
            }

            accounts.Add(newAccount);
            return newAccount;
        }

        public int GetNumberOfAccounts()
        {
            return accounts.Count;
        }

        public double TotalInterestEarned() 
        {
            double total = 0;
            foreach (Account a in accounts)
                total += a.CalculateSimpleInterest();
            return total;
        }

        public string GetStatement() 
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Statement for {name}{Environment.NewLine}");
            accounts.ForEach(a => sb.AppendLine(statementForAccount(a)));
            sb.Append($"Total In All Accounts {accounts.Sum(a => a.CurrentBalance).ToString("C")}");
            return sb.ToString();
        }

        public void Transfer(Account fromAccount, Account toAccount, double amount)
        {
            fromAccount.Withdraw(amount);
            toAccount.Deposit(amount);
        } 

        private string statementForAccount(Account account) 
        {
            return account.GetStatement();
        }
    }
}
