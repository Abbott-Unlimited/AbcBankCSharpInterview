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

        public void Deposit(decimal amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(decimal amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(-amount));
            }
        }

        public decimal InterestEarned() 
        {
            decimal amount = sumTransactions();
            switch(accountType){
                case SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001M;
                    else
                        return 1 + (amount-1000) * 0.002M;
    //            case SUPER_SAVINGS:
    //                if (amount <= 4000)
    //                    return 20;
                case MAXI_SAVINGS:
    /*                if (amount <= 1000)
                        return amount * 0.02M;
                    if (amount <= 2000)
                        return 20 + (amount-1000) * 0.05M;
                    return 70 + (amount-2000) * 0.1M;
    */              // new rate - 5% if no transactions for 10 days otherwise .1%
                    if (transactions.Last().transactionDate.AddDays(10) < DateTime.Now)
                    {
                        return amount * .05M;
                    }

                    return amount * .01M;

                default:
                    return amount * 0.001M;
            }
        }

        public decimal sumTransactions() {
           return CheckIfTransactionsExist(true);
        }

        private decimal CheckIfTransactionsExist(bool checkAll) 
        {
            decimal amount = 0.0M;
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
