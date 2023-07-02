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

        /// <summary>
        /// Don't use this in production; just for testing
        /// </summary>
        /// <param name="amount">amount to withdraw</param>
        /// <param name="date">date of withdrawal</param>
        public void WithdrawSetDate(double amount, DateTime date)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                Transaction transaction = new Transaction(-amount);
                transaction.SetDate(date);
                this.Transactions.Add(transaction);
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
            double intearned = 0;
            switch(accountType)
            {
                case SAVINGS:
                    if (amount <= 1000)
                    {
                        
                        intearned = amount * 0.001;
                    }
                    else
                    {
                        // .001 for the first 1000, then .002
                        intearned = 1 + (amount - 1000) * 0.002;
                    }

                    break;
                case MAXI_SAVINGS:
                    //double maxamount = 0;
                    bool withdraw = Check4WithdrawLast10();
                    if (withdraw)
                    {
                        intearned = amount * .001;
                    }
                    else
                    {
                        intearned = amount * .05;
                    }

                    break;
                    //return maxamount;
                    // old way of computing interest
                    //if (amount <= 1000)
                    //{
                    //    return amount * 0.02;
                    //}
                    //if (amount <= 2000)
                    //{
                    //    return 20 + (amount - 1000) * 0.05;
                    //}

                //double maxamount = 70 + (amount-2000) * 0.1;
                //return maxamount;
                default:
                    // checking account
                    // compute daily accrual

                    intearned = amount * 0.001;
                    break;
            }

            return intearned;
        }

        public double InterestEarnedDailyAccrual()
        {
            double amount = this.SumTransactions();
            double intearned = 0;
            switch (accountType)
            {
                case SAVINGS:
                    if (amount <= 1000)
                    {
                        amount += this.ComputeDailyAccrualPerYear(.001, amount);
                        intearned = amount * 0.001;
                    }
                    else
                    {
                        // .001 for the first 1000, then .002
                        amount += this.ComputeDailyAccrualPerYear(.002, amount);
                        intearned = 1 + (amount - 1000) * 0.002;
                    }

                    break;
                case MAXI_SAVINGS:
                    bool withdraw = Check4WithdrawLast10();
                    if (withdraw)
                    {
                        amount += this.ComputeDailyAccrualPerYear(.001, amount);
                        intearned = amount * .001;
                    }
                    else
                    {
                        amount += this.ComputeDailyAccrualPerYear(.05, amount);
                        intearned = amount * .05;
                    }

                    break;
                
                default:
                    // checking account
                    // compute daily accrual
                    amount += this.ComputeDailyAccrualPerYear(.001, amount);
                    intearned = amount * 0.001;
                    break;
            }

            return intearned;
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
       

        public int GetAccountType() 
        {
            return accountType;
        }

        public void DoTransferIntoThisAccount(double transferamount, Account fromaccount)
        {            
            this.DoTransfer(fromaccount, transferamount);            
        }

        /// <summary>
        /// Checks for withdrawal in the last 10 days
        /// </summary>
        /// <returns></returns>
        private bool Check4WithdrawLast10()
        {
            bool withdrawalinlast10days = false;
            DateTime date10 = DateTime.Today.AddDays(-10);

            // are there any withdrawals in the last 10 days?
            var found = this.Transactions.FirstOrDefault(fd => 
            this.IsWithdrawalTran(fd) && fd.GetDate() >= date10);
            withdrawalinlast10days = found == null ? false : true;

            return withdrawalinlast10days;
           
        }

        /// <summary>
        /// Is the transaction a withdrawl? Test for negative number
        /// </summary>
        /// <param name="tran"></param>
        /// <returns></returns>
        private bool IsWithdrawalTran(Transaction tran)
        {
            return tran.amount < 0 ? true : false;
        }

        private double ComputeDailyAccrualPerYear(double yearlyIntRate, double amount)
        {
            double dailyrt = yearlyIntRate / 365;
            double dailytot = 0;
            for (int i = 0; i < 365; i++)
            {
                dailytot += dailyrt * amount;
            }

            return dailytot;
        }
        private bool CheckIfTransactionsExist(bool checkAll)
        {
            bool dotransexist = this.Transactions.Count > 0;
            return dotransexist;
        }
    }
}
