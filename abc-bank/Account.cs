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

        public double InterestEarned(List<Account> accounts) 
        {
            double totalInterest = 0.0;

            foreach (var account in accounts)
            {
                foreach(var transaction in transactions)
                {
                    switch (account.accountType)
                    {
                        case SAVINGS:
                            if (transaction.amount <= 1000)
                                totalInterest += transaction.amount * 0.001;
                            else
                                totalInterest += 1 + (transaction.amount - 1000) * 0.002;
                            //            case SUPER_SAVINGS:
                            //                if (amount <= 4000)
                            //                    return 20;
                            break;
                        case MAXI_SAVINGS:
                            if (transaction.amount <= 1000)
                                totalInterest += transaction.amount * 0.02;
                            if (transaction.amount <= 2000)
                                totalInterest += 20 + (transaction.amount - 1000) * 0.05;
                            totalInterest += 70 + (transaction.amount - 2000) * 0.1;
                            break;
                        case CHECKING:
                            totalInterest += transaction.amount * 0.001;
                            break;
                    }
                }                
            }
            return totalInterest;
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
