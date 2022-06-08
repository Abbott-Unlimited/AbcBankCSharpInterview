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
        private readonly List<Account> accounts;

        public Customer(string name)
        {
            this.name = name;
            this.accounts = new List<Account>();
        }

        public string GetName()
        {
            return name;
        }

        public Customer OpenAccount(Account account)
        {
            accounts.Add(account);
            return this;
        }

        public void Transfer(Account fromAccount, Account toAccount, decimal amount)
        {
            if (!accounts.Contains(fromAccount) || !accounts.Contains(toAccount))
            {
                throw new ArgumentException("at least one account does not exist");
            } else {
                fromAccount.Withdraw(amount);
                toAccount.Deposit(amount);
            }
        }

        public int GetNumberOfAccounts()
        {
            return accounts.Count;
        }

        public decimal TotalInterestEarned() 
        {
            decimal total = 0;
            foreach (Account a in accounts)
                total += a.InterestEarned();
            return total;
        }

        public string GetStatement() 
        {
            // Setting a string to null can lead to NullReference Exceptions, so I'm opting to implement a StringBuilder 
            var buffer = new StringBuilder($"Statement for {name}\n");
            // Decimals are more procise for currency than using a double
            decimal total = 0.0M;
            foreach (Account a in accounts) 
            {
                buffer = buffer.Append($"\n{StatementForAccount(a)}\n");
                total += a.SumTransactions();
            }
            buffer = buffer.Append($"\nTotal In All Accounts {ToDollars(total)}");
            return buffer.ToString();
        }

        private string StatementForAccount(Account a) 
        {
            string s = "";

           //Translate to pretty account type
            switch(a.GetAccountType()){
                case Account.CHECKING:
                    s += "Checking Account\n";
                    break;
                case Account.SAVINGS:
                    s += "Savings Account\n";
                    break;
                case Account.MAXI_SAVINGS:
                    s += "Maxi Savings Account\n";
                    break;
            }

            //Now total up all the transactions
            decimal total = 0.0M;
            foreach (Transaction t in a.transactions) {
                s += "  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.amount) + "\n";
                total += t.amount;
            }
            s += "Total " + ToDollars(total);
            return s;
        }

        private string ToDollars(decimal d)
        {
            return string.Format("{0:C}", Math.Abs(d));
        }
    }
}
