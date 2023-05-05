using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Customer
    {
        private String _name;
        private List<Account> _accounts;

        public Customer(String name)
        {
            this._name = name;
            this._accounts = new List<Account>();
        }

        public String GetName()
        {
            return _name;
        }

        public Customer OpenAccount(Account account)
        {
            _accounts.Add(account);
            return this;
        }

        public int GetNumberOfAccounts()
        {
            return _accounts.Count;
        }

        public decimal TotalInterestEarned() 
        {
            decimal total = 0;
            foreach (Account a in _accounts)
                total += a.InterestEarned();
            return total;
        }

        public String GetStatement() 
        {
            String statement = null;
            statement = "Statement for " + _name + "\n";
            decimal total = 0.0M;
            foreach (Account a in _accounts) 
            {
                statement += "\n" + StatementForAccount(a) + "\n";
                total += a.SumTransactions();
            }
            statement += "\nTotal In All Accounts " + ToDollars(total);
            return statement;
        }

        public List<Account> GetAccounts()
        {
            return _accounts;
        }

        //TO-DO: Update to use GUID ids for verifying account transfers
        public Result TransferMoney(AccountType fromAccount, AccountType toAccount, decimal amount)
        {
            if (amount <= 0)
                return new FailureResult
                {
                    Message = "amount must be greater than zero",
                };

            Account _fromAccount = null;
            Account _toAccount = null;

            foreach (Account a in _accounts)
            {
                if (a.GetAccountType() == fromAccount) { _fromAccount = a; }
                if(a.GetAccountType() == toAccount) { _toAccount = a; }
            }

            if(_fromAccount == null ||  _toAccount == null)
                return new FailureResult
                {
                    Message = "One Or Both of these accounts do not exist",
                };

            var result = _fromAccount.Withdraw(amount);

            if(result.GetType() == typeof(FailureResult))
            {
                return result;
            }

            result = _toAccount.Deposit(amount);

            if(result.GetType() == typeof(FailureResult))
            { 
                _fromAccount.Deposit(amount);
                return result; 
            }

            return new SuccessResult();
        }

        private String StatementForAccount(Account a) 
        {
            String s = "";

           //Translate to pretty account type
            switch(a.GetAccountType()){
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
            decimal total = 0.0M;
            foreach (Transaction t in a.Transactions) {
                s += "  " + (t.GetAmount() < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.GetAmount()) + "\n";
                total += t.GetAmount();
            }
            s += "Total " + ToDollars(total);
            return s;
        }

        private String ToDollars(decimal d)
        {
            return String.Format("{0:C2}", Math.Abs(d));
        }
    }
}
