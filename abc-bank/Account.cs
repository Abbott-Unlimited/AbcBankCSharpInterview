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

        public const double CHECKING_INTEREST_RATE = 0.001;

        public const double FIRST_THOUSAND_SAVINGS_RATE = 0.001;
        public const double OVER_THOUSAND_SAVINGS_RATE = 0.002;

        public const double FIRST_THOUSAND_MAXI_SAVINGS_RATE = 0.02;
        public const double SECOND_THOUSAND_MAXI_SAVINGS_RATE = 0.05;
        public const double OVER_TWO_THOUSAND_MAXI_SAVINGS_RATE = 0.1;

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
                        return amount * FIRST_THOUSAND_SAVINGS_RATE;
                    else
                        return 1000 * FIRST_THOUSAND_SAVINGS_RATE + (amount-1000) * OVER_THOUSAND_SAVINGS_RATE;
    //            case SUPER_SAVINGS:
    //                if (amount <= 4000)
    //                    return 20;
                case MAXI_SAVINGS:
                    if (amount <= 1000)
                        return amount * FIRST_THOUSAND_MAXI_SAVINGS_RATE;
                    if (amount <= 2000)
                        return 1000 * FIRST_THOUSAND_MAXI_SAVINGS_RATE + (amount-1000) * SECOND_THOUSAND_MAXI_SAVINGS_RATE;
                    return 1000 * FIRST_THOUSAND_MAXI_SAVINGS_RATE + 1000 * SECOND_THOUSAND_MAXI_SAVINGS_RATE + (amount-2000) * OVER_TWO_THOUSAND_MAXI_SAVINGS_RATE;
                default:
                    return amount * CHECKING_INTEREST_RATE;
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

    }
}
