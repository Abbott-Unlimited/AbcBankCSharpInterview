using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Transaction
    {
        /// <summary>
        /// The dollar amount of the transaction.
        /// </summary>
        public decimal Amount { get; }

        /// <summary>
        /// The transaction timestamp.
        /// </summary>
        public DateTime TransactionDate { get; }

        /// <summary>
        /// Creates a new transaction.
        /// </summary>
        /// <param name="amount">The dollar amount of the transaction.</param>
        public Transaction(decimal amount)
        {
            this.Amount = amount;
            this.TransactionDate = DateProvider.getInstance().Now();
        }
    }
}
