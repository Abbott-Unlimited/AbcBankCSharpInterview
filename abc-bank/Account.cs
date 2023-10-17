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

                    if (noWithdrawalsInPast10Days())//boolean decides whether or not there has been any withdrawals in the past 10 days
                        return 70 + (amount) * 0.05; // Updated interest rate to 5%

                    //if (amount <= 1000)
                    //    return amount * 0.02;
                    //if (amount <= 2000)
                    //{
                    //    return 20 + (amount - 1000) * 0.05;
                    //}                        
                    return 70 + (amount) * 0.001;
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

        private bool noWithdrawalsInPast10Days()
        {
            return transactions.Where(w => w.transactionDate <= DateTime.Now.AddDays(-10)).ToList().Count() > 0;        

        }


        public int GetAccountType() 
        {
            return accountType;
        }

    }
}
