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
        public List<Transaction> Transactions;

        public Account(int accountType) 
        {
            this.accountType = accountType;
            this.Transactions = new List<Transaction>();
        }

        public void Deposit(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                Transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                Transactions.Add(new Transaction(-amount));
            }
        }

        public double InterestEarned() 
        {
            double amount = this.SumTransactions();
            switch(accountType){
                case SAVINGS:
                    if (amount <= 1000)
                    {
                        return amount * 0.001;
                    }
                    else
                    {
                        // .001 for the first 1000, then .002
                        return 1 + (amount - 1000) * 0.002;
                    }
    //            case SUPER_SAVINGS:
    //                if (amount <= 4000)
    //                    return 20;
                case MAXI_SAVINGS:
                    if (amount <= 1000)
                    {
                        return amount * 0.02;

                    }
                    if (amount <= 2000)
                    {
                        return 20 + (amount - 1000) * 0.05;
                    }

                    double maxamount = 70 + (amount-2000) * 0.1;
                    return maxamount;
                default:
                    // checking account
                    return amount * 0.001;
            }
        }

        
        /// <summary>
        /// Sum the transactions
        /// </summary>
        /// <returns>The sum of the trasactions for either checking or savings</returns>
        public double SumTransactions()
        {
            double amount = 0.0;
            foreach (Transaction t in Transactions)
                amount += t.amount;
            return amount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkAll">Don't know yet how to use checkAll. Either transactions exist or not. Possibly remove.</param>
        /// <returns></returns>
        private bool CheckIfTransactionsExist(bool checkAll) 
        {

            bool dotransexist = this.Transactions.Count > 0;
            return dotransexist;            
        }

        public int GetAccountType() 
        {
            return accountType;
        }

        public void DoTransferIntoThisAccount(double transferamount, Account fromaccount)
        {
            
            this.DoTransfer(fromaccount, transferamount);

            
        }

    }
}
