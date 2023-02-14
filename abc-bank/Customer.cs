using System;
using System.Collections.Generic;
using System.Linq;
using abc_bank.Accounts;
using abc_bank.Models;

namespace abc_bank
{
    
    public class Customer
    {
        private String name;
        private List<Account> accounts;

        public Customer(String name)
        {
            this.name = name;
            this.accounts = new List<Account>();
        }

        public String GetName()
        {
            return name;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        public void AddAccount(Account account)
        {
            accounts.Add(account);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List<Account> accounts</Account></returns>
        public List<Account> GetAccounts()
        {
            return accounts;
        }

        public Account GetAccount(AccountTypes accountType)
        {
            return accounts.Where(x => x.GetAccountType() == accountType).FirstOrDefault();
        }

        //public Customer OpenAccount(Account account)
        //{
        //    accounts.Add(account);
        //    return this;
        //}

        //public Account OpenAccount(AccountTypes accountType)
        //{
        //    var account = accounts.Where(x => x.GetAccountType() == accountType).FirstOrDefault();
        //    if(account == null)
        //    {
        //        return null;
        //    }
        //    return account;
        //}

        public TransactionResponse TransferAccountValue(Account fromAccount, double amount, Account toAccount)
        {

            if (fromAccount == null)
            {
                return new TransactionResponse($"Account {fromAccount.GetType()} does not exist.", false);
            }
            if (toAccount == null)
            {
                return new TransactionResponse($"Account {toAccount.GetType()} does not exist.", false);
            }

            try
            {
                fromAccount.Withdraw(amount);
                toAccount.Deposit(amount);
            }
            catch (Exception e)
            {
                //This would go to some alert
                Console.WriteLine(e.Message.ToString());
            }

            return new TransactionResponse(GetStatement(), true);
        }

        //public int GetNumberOfAccounts()
        //{
        //    return accounts.Count;
        //}

        public double TotalInterestEarned() 
        {
            double total = 0;
            foreach (Account a in accounts)
                total += a.InterestEarned();
            return total;
        }

        public String GetStatement()
        {
            String statement = null;
            statement = "Statement for " + name + "\n";
            double total = 0.0;
            foreach (Account a in accounts)
            {
                statement += "\n" + statementForAccount(a) + "\n";
                total += a.SumTransactions();
            }
            statement += "\nTotal In All Accounts " + ToDollars(total);
            return statement;
        }

        private String statementForAccount(Account a)
        {
            String s = "";

            //Translate to pretty account type
            switch (a.GetAccountType())
            {
                case Accounts.AccountTypes.CHECKING:
                    s += "Checking Account\n";
                    break;
                case Accounts.AccountTypes.SAVINGS:
                    s += "Savings Account\n";
                    break;
                case Accounts.AccountTypes.MAXI_SAVINGS:
                    s += "Maxi Savings Account\n";
                    break;
            }

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.GetTransactions())
            {
                s += "  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.amount) + "\n";
                total += t.amount;
            }
            s += "Total " + ToDollars(total);
            return s;
        }

        private String ToDollars(double d)
        {
            return String.Format("{0:C}", Math.Abs(d));
        }
    }
}
