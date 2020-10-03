using System;
using System.Collections.Generic;

namespace abc_bank
{
    /// <summary>
    /// Type of account an account can be
    /// </summary>
    public enum AccountType
    {
        CHECKING,
        SAVINGS,
        MAXI_SAVINGS
    }

    /// <summary>
    /// Account under a customer
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Type of account this account is
        /// </summary>
        private readonly AccountType accountType;
        /// <summary>
        /// Transaction history for this account
        /// </summary>
        public List<Transaction> transactions;

        /// <summary>
        /// Contstructor setting defaults for this account (account type and empty transaction history)
        /// </summary>
        /// <param name="accountType">Account type this account is</param>
        public Account(AccountType accountType) 
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
        }

        /// <summary>
        /// Deposits money into the account
        /// </summary>
        /// <param name="amount">Amount of money being deposited</param>
        public void Deposit(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount));
            }
        }

        /// <summary>
        /// Withdraws money into the account
        /// </summary>
        /// <param name="amount">Amount of money being taken out</param>
        public void Withdraw(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(-amount));
            }
        }

        /// <summary>
        /// Amount of interest this account has accrued 
        /// </summary>
        /// <returns>Total interest for the account</returns>
        public double InterestEarned() 
        {
            double amount = sumTransactions();
            if (amount < 0)
            {
                // no interest on debt
                return 0;
            }

            var dateProvider = new DateProvider();
            var today = dateProvider.Now();
            var date = transactions[transactions.Count - 1].GetDate();
            switch (accountType){
                case AccountType.SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001;
                    else
                        return 1 + (amount-1000) * 0.002;
    //            case SUPER_SAVINGS:
    //                if (amount <= 4000)
    //                    return 20;
                case AccountType.MAXI_SAVINGS:
                    
                    if (date.AddDays(10) < today)
                        return amount * 0.05;
                    return amount * 0.001;
                default:
                    return amount * 0.001;
            }
        }

        /// <summary>
        /// Gets sum of all transactions for the account
        /// </summary>
        /// <returns>Total sum of all trasnsactions</returns>
        public double sumTransactions() {
           return CheckIfTransactionsExist();
        }

        /// <summary>
        /// Gets sum of transactions, helper method for sumTransactions()
        /// </summary>
        /// <returns>Total sum of all trasnsactions</returns>
        private double CheckIfTransactionsExist() 
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        /// <summary>
        /// Getter method for the type of account this is
        /// </summary>
        /// <returns>The AccountType for this account</returns>
        public AccountType GetAccountType() 
        {
            return accountType;
        }
    }
}
