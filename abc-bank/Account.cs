using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Account
    {
        private readonly int _accountType;

        public const int CHECKING = 0;
        public const int SAVINGS = 1;
        public const int MAXI_SAVINGS = 2;

        public List<Transaction> transactions;

        private int _accountNumber;

        private static int _lastAccountNumber = 0;

        public Account(int accountType) 
        {
            _accountNumber = ++_lastAccountNumber;
            _accountType = accountType;
            transactions = new List<Transaction>();
            CurrentBalance = 0.0;
        }

        public int AccountNumber { get; set; }
        public double CurrentBalance { get; set; }

        public void Deposit(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                //transactions.Add(new Transaction(amount));
                var dep = new Deposit(DateTime.Now, this, amount);
                transactions.Add(dep);
                CurrentBalance = dep.Execute();
            }
        }

        public void Withdraw(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                //transactions.Add(new Withdrawal(_currentBalance, amount));
                var wd = new Withdrawal(DateTime.Now, this, amount);
                transactions.Add(wd);
                CurrentBalance = wd.Execute();
            }
        }

        public double InterestEarned() 
        {
            double amount = sumTransactions();
            switch(_accountType){
                case SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001;
                    else
                        return 1 + (amount-1000) * 0.002;
    //            case SUPER_SAVINGS:
    //                if (amount <= 4000)
    //                    return 20;
                case MAXI_SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.02;
                    if (amount <= 2000)
                        return 20 + (amount-1000) * 0.05;
                    return 70 + (amount-2000) * 0.1;
                default:
                    return amount * 0.001;
            }
        }

        public double sumTransactions() {
           return CheckIfTransactionsExist(true);
        }

        private double CheckIfTransactionsExist(bool checkAll) 
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount = t.Amount;
            return amount;
        }

        public int GetAccountType() 
        {
            return _accountType;
        }

    }
}
