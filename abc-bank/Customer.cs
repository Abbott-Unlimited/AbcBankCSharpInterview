using System;
using System.Collections.Generic;
using System.Linq;

namespace abc_bank
{
    public class Customer
    {
        #region | Globals |
        private String name;
        private List<Account> accounts;
        #endregion

        #region | Constructors |
        public Customer(String name)
        {
            this.name = name;
            this.accounts = new List<Account>();
        }
        #endregion

        #region | GetName |
        public String GetName()
        {
            return name;
        }
        #endregion

        #region | OpenAccount |
        public Customer OpenAccount(Account account)
        {
            accounts.Add(account);
            return this;
        }
        #endregion

        #region | GetNumberOfAccounts |
        public int GetNumberOfAccounts()
        {
            return accounts.Count;
        }
        #endregion

        #region | TotalInterestEarned |
        //  Used Interest class for Interest Calculation
        public double TotalInterestEarned()
        {
            double total = 0;
            foreach (Account account in accounts)
                //total += a.InterestEarned();
                total += Interest.InterestEarned(account);
            return total;
        }
        #endregion

        #region | GetStatement |
        public String GetStatement()
        {
            String statement = null;
            statement = "Statement for " + name + "\n";
            double total = 0.0;
            foreach (Account a in accounts)
            {
                statement += "\n" + StatementForAccount(a) + "\n";
                //total += a.SumTransactions();
                total += a.balance;
            }
            statement += "\nTotal In All Accounts " + ToDollars(total);
            return statement;
        }
        #endregion

        #region | StatementForAccount |
        private String StatementForAccount(Account a)
        {
            String s = "";

            //Translate to pretty account type
            switch (a.GetAccountType())
            {
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
            foreach (Transaction t in a.transactions)
            {
                s += "  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.amount) + "\n";
                total += t.amount;
            }
            s += "Total " + ToDollars(total);
            return s;
        }
        #endregion

        #region | ToDollars |
        private String ToDollars(double d)
        {
            //return String.Format("$%,.2f", Math.Abs(d));
            return String.Format("{0:C2}", Math.Abs(d));
        }
        #endregion

        #region | Integrated: TransferFunds |
        private Account GetAccount(int accountType)
        {
            return accounts
                    .Where(a => a.GetAccountType() == accountType)
                    .FirstOrDefault();
        }

        public bool TransferFunds(int fromAccountType, int toAccountType, double transferAmount)
        {
            Account fromAccount = GetAccount(fromAccountType);
            Account toAccount = GetAccount(toAccountType);

            if (fromAccount == null)
            {
                throw new ApplicationException("sender account does not exist");
            }
            else if (toAccount == null)
            {
                throw new ApplicationException("receiver account does not exist");
            }

            if (transferAmount <= 0)
            {
                throw new ArgumentException("transfer amount must be greate than 0");
            }
            else if (transferAmount > fromAccount.balance)
            {
                throw new ArgumentException("insufficient funds");
            }

            fromAccount.Withdraw(transferAmount);
            toAccount.Deposit(transferAmount);

            return true;
        }
        #endregion
    }
}
