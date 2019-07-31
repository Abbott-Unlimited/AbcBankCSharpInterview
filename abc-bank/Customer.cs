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

        public Customer TransferBetweenAccounts(int sourceAccountIndex, int destinationAccountIndex, double amount)
        {
            // The customer has no account.
            if (accounts.Count == 0)
            {
                throw new ArgumentException("the user has no accounts");
            }

            // Ensure that amount > 0
            if (amount <= 0.0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }

            // Ensure that 0 <= sourceAccountIndex <= accounts.length - 1
            if (sourceAccountIndex < 0 || sourceAccountIndex > accounts.Count - 1 || accounts[sourceAccountIndex] == null)
            {
                throw new ArgumentException("source account does not exist");
            }

            // Ensure that 0 <= destinationAccountIndex <= accounts.length - 1
            if (destinationAccountIndex < 0 || destinationAccountIndex > accounts.Count - 1 || accounts[destinationAccountIndex] == null)
            {
                throw new ArgumentException("destination account does not exist");
            }

            // Ensure that sourceAccountIndex is NOT the same as the destination account index. 
            if (sourceAccountIndex == destinationAccountIndex)
            {
                throw new ArgumentException("source and destination accounts cannot be the same");
            }

            // Insufficient amount in source account
            if (accounts[sourceAccountIndex].sumTransactions() < amount)
            {
                throw new ArgumentException("insufficient funds for transactions");
            }

            return TransferBetweenAccounts(accounts[sourceAccountIndex], accounts[destinationAccountIndex], amount);
        }

        private Customer TransferBetweenAccounts(Account sourceAccount, Account destinationAccount, double amount)
        {
            sourceAccount.Withdraw(amount);
            destinationAccount.Deposit(amount);

            return this;
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
            double total = 0.0;
            foreach (Transaction t in a.transactions) {
                s += "  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.amount) + "\n";
                total += t.amount;
            }
            s += "Total " + ToDollars(total);
            return s;
        }

        private String ToDollars(double d)
        {
            return String.Format("${0:n}", Math.Round(Math.Abs(d), 2));
        }
    }
}
