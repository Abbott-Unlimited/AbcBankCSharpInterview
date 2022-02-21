using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abc_bank.Utilities.Date;

namespace abc_bank.Accounts
{
    /// <summary>
    /// The super class of all account classes.
    /// </summary>
    public abstract class Account : List<Transaction>
    {
        private protected IDateProvider dateProvider;
        private protected DateTime dateOpened;

        /// <summary>
        /// The available account types that can be created through the Account factory.
        /// </summary>
        public enum AccountType
        {
            Checking,
            Savings,
            MaxiSavings
        }

        /// <summary>
        /// Returns an Account-derived instance based on the AccountType parameter.
        /// </summary>
        /// <param name="dateProvider">A date provider interface to retrieve transaction dates</param>
        /// <param name="accountType">Indicates the account type to create</param>
        /// <returns>An instance of a class derived from Account</returns>
        public static Account Create(IDateProvider dateProvider, AccountType accountType) 
        {
            Account account;
            switch (accountType)
            {
                case AccountType.Checking:
                    account = new CheckingAccount();
                    break;
                case AccountType.Savings:
                    account = new SavingsAccount();
                    break;
                case AccountType.MaxiSavings:
                    account = new MaxiSavingsAccount();
                    break;
                default:
                    throw new ArgumentException(accountType.ToString() + " is not a known AccountType");
            }
            account.dateProvider = dateProvider ?? throw new ArgumentNullException("dateProvider can't be null.");
            account.dateOpened = dateProvider.Now();
            return account;
        }

        /// <summary>
        /// Saves the amount as a transaction in this account.
        /// </summary>
        /// <param name="amount">The amount of the deposit. Must be greater than 0.</param>
        public void Deposit(double amount) 
        {
            if (amount <= 0) 
            {
                throw new ArgumentException("amount must be greater than zero");
            } 
            else 
            {
                this.Add(new Transaction(this.dateProvider, amount, Transaction.TransactionType.Deposit));
            }
        }

        /// <summary>
        /// Saves the amount as a negative transaction to the account.
        /// </summary>
        /// <param name="amount">The amount of the withdraw. Must be greater than 0.</param>
        public void Withdraw(double amount) 
        {
            if (amount <= 0) 
            {
                throw new ArgumentException("amount must be greater than zero");
            } 
            else 
            {
                this.Add(new Transaction(this.dateProvider, -amount, Transaction.TransactionType.Withdrawal));
            }
        }

        /// <summary>
        /// Withdraws the amount from this account, and deposits the amount in toAccount.
        /// </summary>
        /// <param name="amount">The amount of the transfer. Must be greater than 0</param>
        public void Transfer(double amount, Account toAccount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                this.Add(new Transaction(this.dateProvider, -amount, Transaction.TransactionType.Withdrawal));
                toAccount.Add(new Transaction(this.dateProvider, amount, Transaction.TransactionType.Deposit));
            }
        }

        /// <summary>
        /// Returns the sum of all transactions in the account.
        /// </summary>
        /// <returns>The sum of all transactions.</returns>
        public double SumTransactions()
        {
            double amount = 0.0;
            foreach (Transaction transaction in this)
                amount += transaction.Amount;
            return amount;
        }

        /// <summary>
        /// Calculates the total daily interest accrual for the account.
        /// </summary>
        /// <returns>The total daily interest accrued. 0.0 if no transactions exist.</returns>
        public virtual double InterestEarned()
        {
            // if no transactions exist then no interest earned
            if (this.Count() == 0) return 0.0;

            // initialize variables
            double totalInterestAccrual = 0.0; // return value
            double accountSum = 0.0; // sum of transactions up through current iteration
            DateTime latestWithdrawDate = default; // last withdraw date
            int totalDaysOpen = GetDaysSinceAccountOpen(); // number of days since opening account

            // initialize enumerator and get the first transaction
            List<Transaction>.Enumerator transactions = this.GetEnumerator();
            transactions.MoveNext();
            Transaction enumeratedTransaction = transactions.Current;
            DateTime firstTransactionDate = enumeratedTransaction.TransactionDate;

            // calculate total interest accrued
            for (int currentDay = 0; currentDay <= totalDaysOpen; currentDay++)
            {
                // get the current day datetime
                DateTime currentDate = firstTransactionDate.AddDays(currentDay);

                // sum transactions for the current day
                accountSum += SumTransactionsForDate(currentDate, ref latestWithdrawDate, ref enumeratedTransaction, ref transactions);

                // get the current interest rate to be used
                double currentInterestRate = GetCurrentRate(latestWithdrawDate, currentDate);

                // calculate the current daily interest and add to the total
                totalInterestAccrual += CalculateDailyInterestAccrual(accountSum, currentInterestRate);
            }

            // assumption: interest earned will only be accurate to the 2nd decimal
            return Math.Round(totalInterestAccrual, 2);
        }

        /// <summary>
        /// Returns the number of days the account has been open.
        /// </summary>
        /// <returns>An integer with total number of days the account has been open.</returns>
        private int GetDaysSinceAccountOpen()
        {
            return this.dateProvider.Now().Subtract(this.dateOpened).Days;
        }

        /// <summary>
        /// Calculates the interest given an account sum and a rate.
        /// </summary>
        /// <param name="accountSum">A double with the amount the interest rate should be applied.</param>
        /// <param name="rate"></param>
        /// <returns></returns>
        private double CalculateDailyInterestAccrual(double accountSum, double rate)
        {
            return accountSum * (rate / 365.0);
        }

        /// <summary>
        /// Sums the transactions for the current date referenced by the enumerator.
        /// </summary>
        /// <param name="currentDate">The date to check for transactions in the account.</param>
        /// <param name="latestWithdrawDate">A reference to the latest withdraw datetime enumerated.</param>
        /// <param name="enumeratedTransaction">A reference to the most recent transaction enumerated.</param>
        /// <param name="transactions">This Account's enumerator to iterate through the transactions.</param>
        /// <returns>The sum of all transactions for DateTime defined by currentDate.</returns>
        private double SumTransactionsForDate(DateTime currentDate, ref DateTime latestWithdrawDate, ref Transaction enumeratedTransaction, ref Enumerator transactions)
        {
            double sum = 0.0;

            // add transactions for the current day
            if (enumeratedTransaction.TransactionDate.Date == currentDate.Date)
            {
                // check to see if the transaction is a withdraw, if so save the transaction date
                if (enumeratedTransaction.Type == Transaction.TransactionType.Withdrawal)
                {
                    latestWithdrawDate = enumeratedTransaction.TransactionDate.Date;
                }

                // the current transaction must be the current date, so add to the account sum
                sum += enumeratedTransaction.Amount;

                while (transactions.MoveNext())
                {
                    enumeratedTransaction = transactions.Current;

                    // break the loop if out enumerator goes beyond the current date
                    if (enumeratedTransaction.TransactionDate.Date > currentDate.Date)
                        break;

                    // check to see if the transaction is a withdraw, if so save the transaction date
                    if (transactions.Current.Type == Transaction.TransactionType.Withdrawal)
                    {
                        latestWithdrawDate = enumeratedTransaction.TransactionDate.Date;
                    }

                    // the current transaction must be the current date, so add to the account sum
                    sum += enumeratedTransaction.Amount;
                }
            }

            return sum;
        }

        /// <summary>
        /// Gets the current interest rate defined by the deriving class.
        /// </summary>
        /// <param name="latestWithdrawDate"></param>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        protected abstract double GetCurrentRate(DateTime latestWithdrawDate = default, DateTime currentDate = default);

        /// <summary>
        /// Gets the AccountType defined by the deriving class.
        /// </summary>
        /// <returns>An Account.AccountType indicating what kind of account it is.</returns>
        public abstract AccountType GetAccountType();
    }
}
