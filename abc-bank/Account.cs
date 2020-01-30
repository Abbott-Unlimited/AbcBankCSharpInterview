using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Account
    {

        public const int CHECKING = 0;
        public const int SAVINGS = 1;
        public const int MAXI_SAVINGS = 2;

        private readonly int accountType;
        public List<Transaction> transactions;

        public Account(int accountType) 
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
        }

        public void Deposit(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(-amount));
            }
        }

        public void Transfer(Account transferFrom, Account transferTo, double amount)
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transferFrom.Withdraw(amount);
                transferTo.Deposit(amount);
            }
        }

        public double InterestEarned(Account acc) 
        {
            double amount = sumTransactions();
            switch(accountType){
                case SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001;
                    else
                        return 1 + (amount-1000) * 0.002;
    //            case SUPER_SAVINGS:
    //                if (amount <= 4000)
    //                    return 20;
                case MAXI_SAVINGS:
                    // Change Maxi-Savings accounts to have an interest rate of 5% assuming 
                    // no withdrawals in the past 10 days otherwise 0.1%.
                    if (isEligible(acc) == true)
                        return amount * 0.05;
                    return amount * 0.001;
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
                amount += t.amount;
            return amount;
        }

        public int GetAccountType() 
        {
            return accountType;
        }

        public bool isEligible(Account acc) {
            bool eligible = true;
            if (acc.accountType == 2) {
                foreach (Transaction t in acc.transactions) {
                    DateTime d = t.transactionDate;
                    // If transaction was made in last 10 days
                    // then customer is ineligible.
                    if (d >= DateTime.Now.AddDays(-10)) {
                        eligible = false;
                    }
                }
            }
            return eligible;
        }
    }
}
