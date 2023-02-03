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
        public double balance { get; set; }

        public Account(int accountType) 
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
            this.balance = 0.0;
        }

        public void Deposit(double amount) 
        {
            var transactionType = "Deposit";
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount, transactionType));
                balance += amount;
            }
        }

        public void Withdraw(double amount) 
        {
            var transactionType = "Withdraw";
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(-amount, transactionType));
                balance -= amount;
            }
        }

        public double InterestEarned() 
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
                    if (CheckWithdrawalWithin10Days())
                    {
                        return amount * 0.001;
                    }
                    else
                    {
                        return amount * 0.05;
                    }
                default:
                    return amount * 0.001;
            }
        }

        private bool CheckWithdrawalWithin10Days()
        {
            bool withdrawal = false;
            var transCount  = transactions.Where(tx => tx.transactionDate > DateProvider.getInstance().Now().AddDays(-10) && tx.transactionType == "Withdraw").Count();
            if (transCount > 0) 
            {
                withdrawal = true; 
            }
            return withdrawal;
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

    }
}
