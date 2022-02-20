using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abc_bank.Utilities.Date;

namespace abc_bank.Accounts
{
    public abstract class Account : List<Transaction>
    {
        /// <summary>
        /// The available account types that can be created through the Account factory.
        /// </summary>
        public enum AccountType
        {
            Checking,
            Savings,
            MaxiSavings
        }

        private IDateProvider _dateProvider;

        /// <summary>
        /// Returns an Account-derived instance based on the AccountType parameter.
        /// </summary>
        /// <param name="accountType"></param>
        /// <returns>An instance of Account</returns>
        public static Account Create(IDateProvider dateProvider, AccountType accountType) 
        {
            Account account = null;
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
            account._dateProvider = dateProvider;
            return account;
        }

        /// <summary>
        /// Saves the amount as a transaction to the account.
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
                this.Add(new Transaction(_dateProvider, amount));
            }
        }

        /// <summary>
        /// Saves the amount as a negative transaction to the account.
        /// </summary>
        /// <param name="amount">The amount of the withdraw. Must be greater than 0</param>
        public void Withdraw(double amount) 
        {
            if (amount <= 0) 
            {
                throw new ArgumentException("amount must be greater than zero");
            } 
            else 
            {
                this.Add(new Transaction(_dateProvider, -amount));
            }
        }

        /// <summary>
        /// Deposits the amount as a positive transaction to the this instance, and a withdraw of the amount from toAccount.
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
                this.Add(new Transaction(_dateProvider, -amount));
                toAccount.Add(new Transaction(_dateProvider, amount));
            }
        }

        /// <summary>
        /// Returns the sum of all the transactions in the account.
        /// </summary>
        /// <returns>The sum of transactions as a double.</returns>
        public double SumTransactions()
        {
            double amount = 0.0;
            // we don't need to check if transactions exist because the use of Enumerator
            foreach (Transaction transaction in this)
                amount += transaction.amount;
            return amount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract AccountType GetAccountType();

        /// <summary>
        /// Implemented by derived classes to calculate the amount of interest earned.
        /// </summary>
        public abstract double InterestEarned();
    }
}
