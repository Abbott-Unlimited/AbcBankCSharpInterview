using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Customer OpenAccount(Account account)
        {
            accounts.Add(account);
            return this;
        }

        public int GetNumberOfAccounts()
        {
            return accounts.Count;
        }

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
                total += a.sumTransactions();
            }
            statement += "\nTotal In All Accounts " + ToDollars(total);
            return statement;
        }

        private String statementForAccount(Account a) 
        {
            String s = "";

           //Translate to pretty account type
            switch(a.AccountType){
                case AccountType.CHECKING:
                    s += "Checking Account\n";
                    break;
                case AccountType.SAVINGS:
                    s += "Savings Account\n";
                    break;
                case AccountType.MAXI_SAVINGS:
                    s += "Maxi Savings Account\n";
                    break;
            }

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.transactions)
            {
                s += "  " + t.TransactionType.ToString().ToLower() + " " + ToDollars(t.amount) + "\n";
                total += t.amount;
            }

            s += "Total " + ToDollars(total);
            return s;
        }

        private String ToDollars(double d)
        {
            return Math.Abs(d).ToString("C");
        }

        public void Transfer(AccountType accountTypeToTransferFrom, AccountType accountTypeToTransferTo, double tranferAmmout)
        {
            if (tranferAmmout <= 0)
            {
                throw new ArgumentException("Transfer ammount should be positive number");
            }
            Account accountFrom = accounts.Where(a => a.AccountType == accountTypeToTransferFrom).FirstOrDefault();
            if (accountFrom == null)
            {
                throw new ArgumentException("Customer " + name + " doesn't have " + accountTypeToTransferFrom);
            }
            Account accountTo = accounts.Where(a => a.AccountType == accountTypeToTransferTo).FirstOrDefault();
            if (accountTo == null)
            {
                throw new ArgumentException("Customer " + name + " doesn't have " + accountTypeToTransferTo + " account");
            }
            if (accountFrom.Ballance < tranferAmmout)
            {
                throw new ArgumentException("Customer " + name + " doesn't have enough funds on " + accountTypeToTransferFrom);
            }
            if (accountTypeToTransferFrom == accountTypeToTransferTo)
            {
                throw new ArgumentException("Cannot transfer to same account");
            }
            accountFrom.Withdraw(tranferAmmout, true);
            accountTo.Deposit(tranferAmmout, true);     
        }
    }
}
