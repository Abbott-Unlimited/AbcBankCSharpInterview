using System;

namespace abc_bank
{
    /// <summary>
    /// Individual transaction under an account
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Amount of money involved in the transaction
        /// </summary>
        public readonly double amount;

        /// <summary>
        /// Date of the transaction
        /// </summary>
        private DateTime transactionDate;

        /// <summary>
        /// Constructor for transaction, sets amount and current date
        /// </summary>
        /// <param name="amount">Amount of money involved in transaction</param>
        public Transaction(double amount) 
        {
            this.amount = amount;
            transactionDate = DateProvider.getInstance().Now();
        }

        /// <summary>
        /// Getter method for getting the transaction date
        /// </summary>
        /// <returns>Date transaction happened</returns>
        public DateTime GetDate()
        {
            return transactionDate;
        }
    }
}
