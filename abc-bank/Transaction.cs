using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    /// <summary>
    /// Transaction Object
    /// </summary>
    public class Transaction
    {
        public readonly double amount;

        private DateTime transactionDate;

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public Transaction(double amount)
        {
            this.amount = amount;
            this.transactionDate = DateProvider.getInstance().Now();
        }

        /// <summary>
        /// Days since the transaction.
        /// </summary>
        /// <returns></returns>
        public int DaysSinceTransaction()
        {
            return Math.Abs(DateProvider.getInstance().Now().Subtract(this.transactionDate).Days);
        }
    }
}
