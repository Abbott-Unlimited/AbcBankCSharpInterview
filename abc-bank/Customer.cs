using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Customer
    {
        private readonly String name;
        private List<Account> accounts;

        public Customer(String name) {
            this.name = name;
            this.accounts = new List<Account>();
        }

        public String GetName() {
            return name;
        }

        public Customer OpenAccount(Account account) {
            accounts.Add(account);
            return this;
        }

        public void MakeDeposit(int accountNumber, decimal amount) {
            if (isValidAccountNumber(accountNumber))
            {
                accounts[accountNumber].Deposit(amount);
            }
            else
            {
                throw new ArgumentException("Account Number Invalid");
            }
        }

        public void MakeWithdrawl(int accountNumber, decimal amount) {
            if (isValidAccountNumber(accountNumber)) {
                accounts[accountNumber].Withdraw(amount);
            } else {
                throw new ArgumentException("Account Number Invalid");
            }
        }

        public void TransferFunds(int from, int to, decimal amount) {
            if (isValidAccountNumber(from) && isValidAccountNumber(to)) {
                if(amount > 0) {

                }
                accounts[from].Withdraw(amount);
                accounts[to].Deposit(amount);
            }
        }

        public int GetNumberOfAccounts() {
            return accounts.Count;
        }

        public decimal getAccountBalance(int accountNumber) {
                return accounts[accountNumber].sumTransactions();
        }

        public decimal TotalInterestEarned() {
            decimal total = 0;
            foreach (Account a in accounts)
                total += a.InterestEarned();
            return total;
        }

        public String GetStatement() {
            String statement = null;
            statement = "Statement for " + name + "\n";
            decimal total = 0;
            foreach (Account a in accounts) {
                statement += "\n" + StatementForAccount(a) + "\n";
                total += a.sumTransactions();
            }
            statement += "\nTotal In All Accounts " + ToDollars(total);
            return statement;
        }

        private String StatementForAccount(Account a) {
            String s = "";

           //Translate to pretty account type
            switch(a.GetAccountType()) {
                case Account.AccountType.CHECKING:
                    s += "Checking Account\n";
                    break;
                case Account.AccountType.SAVINGS:
                    s += "Savings Account\n";
                    break;
                case Account.AccountType.MAXI_SAVINGS:
                    s += "Maxi Savings Account\n";
                    break;
            }

            //Now total up all the transactions
            decimal total = 0;
            foreach (Transaction t in a.transactions) {
                s += "  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.amount) + "\n";
                total += t.amount;
            }
            s += "Total " + ToDollars(total);
            return s;
        }

        private bool isValidAccountNumber(int accountNumber) {
            if (accountNumber < 0 || accountNumber >= accounts.Count()) {
                return false;
            } else {
                return true;
            }
        }

        private String ToDollars(decimal d) {
            return String.Format("${0:0,0.00}", Math.Abs(d));
        }
    }
}
