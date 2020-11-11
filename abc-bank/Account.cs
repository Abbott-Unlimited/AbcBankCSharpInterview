using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Account
    {
        /* Current Features
        A customer can open an account - DONE.
        A customer can deposit / withdraw funds from an account - DONE.
        A customer can request a statement that shows transactions and totals for each of their accounts - DONE.
        Different accounts have interest calculated in different ways:
            Checking accounts have a flat rate of 0.1% - DONE.
            Savings accounts have a rate of 0.1% for the first $1,000 then 0.2% - DONE.
            Maxi-Savings accounts have a rate of 2% for the first $1,000 then 5% for the next $1,000 then 10% - DONE.
        A bank manager can get a report showing the list of customers and how many accounts they have - DONE.
        A bank manager can get a report showing the total interest paid by the bank on all accounts - DONE method in Customer.

        Additional Features
        A customer can transfer between their accounts - DONE.
        Change Maxi-Savings accounts to have an interest rate of 5% assuming no withdrawals in the past 10 days otherwise 0.1%.
        Interest rates should accrue daily including weekends, rates above are per-annum (for each year).*/

        /*I have converted this to enum and updated all references*/
        //public const int CHECKING = 0;
        //public const int SAVINGS = 1;
        //public const int MAXI_SAVINGS = 2;

        public enum AccountType
        {
            CHECKING,
            SAVINGS,
            MAXI_SAVINGS
        };

        private readonly AccountType accountType;
        public List<Transaction> transactions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountType"></param>
        public Account(AccountType accountType) 
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
        }

        /// <summary>
        /// A customer can deposit funds into an account.
        /// </summary>
        /// <param name="amount"></param>
        public void Deposit(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount));
            }
        }

        /// <summary>
        /// A customer can withdraw funds from an account.
        /// </summary>
        /// <param name="amount"></param>
        public void Withdraw(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(-amount));
            }
        }

        /// <summary>
        /// Different accounts have interest calculated in different ways:
        //Checking accounts have a flat rate of 0.1%.
        //Savings accounts have a rate of 0.1% for the first $1,000 then 0.2%.
        //Maxi-Savings accounts have a rate of 2% for the first $1,000 then 5% for the next $1,000 then 10%.
        /// </summary>
        /// <returns></returns>
        public double InterestEarned() 
        {
            double amount = sumTransactions();
            
            switch(accountType){
                case AccountType.SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001; //Savings accounts have a rate of 0.1% for the first $1,000.
                    else
                        return 1 + (amount-1000) * 0.002;  //first $1000 have a flat rate of 0.1% which is 1$ then 0.2%.

                case AccountType.MAXI_SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.02; //Maxi-Savings accounts have a rate of 2% for the first $1,000 which is 0.02.
                    if (amount <= 2000)
                        return 20 + (amount-1000) * 0.05; //then 5% which is 0.05 for the second $1000.
                    return 70 + (amount-2000) * 0.1; //  then 10%

                default:   //if AccountType.CHECKING:
                    return amount * 0.001; //Checking accounts have a flat rate of 0.1%.
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double sumTransactions()
        {
            double amount = 0.0;
            if (CheckIfTransactionsExist())
            {
                // We need to loop through all transactions and calculate the total amount
                foreach (Transaction t in transactions)
                    amount += t.amount;
                return amount;
            }
            else
            {
                return amount;
            }
        }

        /// <summary>
        /// check to see if transactions exist or not
        /// </summary>
        /// <returns></returns>
        private bool CheckIfTransactionsExist() => transactions.Count > 0 ? true : false;

        public AccountType GetAccountType() 
        {
            return accountType;
        }



        /// <summary>
        /// Additional Features: Transfer between accounts
        /// </summary>
        /// <param name="fromAccount"></param>
        /// <param name="toAccount"></param>
        /// <param name="amount"></param>
        public void TransferBetweenAccounts(Account toAccount, double amount)
        {
            if(amount <= 0)
            {
                throw new ArgumentException("amount should be greater than zero");
            }
            //else if(amount > totoalTransactions())
            //{
            //    // log a mesage to the user
            //}
            else
            {
                transactions.Add(new Transaction(-amount));
                toAccount.Deposit(amount);
            }
                
        }
    }
}
