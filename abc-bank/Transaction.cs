using System;

namespace abc_bank
{
    public class Transaction
    {
        #region | Globals |
        public readonly double amount;

        //private DateTime transactionDate;

        /// <summary>
        /// The field is readonly to be only initialized in Constructor.
        /// The field is public to make transactionDate accessible in other Classes.
        /// </summary>
        public readonly DateTime transactionDate;
        #endregion

        #region | Constructors |
        public Transaction(double amount)
        {
            this.amount = amount;
            this.transactionDate = DateProvider.GetInstance().Now();
        }
        #endregion
    }
}
