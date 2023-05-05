using System;

namespace abc_bank
{
    public class Transaction
    {
        private decimal _amount;

        private DateTime? _transactionDate;


        //Allows backdating for both testing and corrections
        public Transaction(decimal amount, DateTime? date = null)
        {
            this._amount = amount;
            this._transactionDate = date == null ? DateProvider.GetInstance().Now() : date;
        }

        public DateTime? GetDate()
        {
            return _transactionDate;
        }

        public decimal GetAmount()
        {
            return _amount;
        }
    }
}
