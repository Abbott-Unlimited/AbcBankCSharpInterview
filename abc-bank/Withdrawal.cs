using System;

namespace abc_bank
{
    /// <summary>
    /// Describes a Withdrawal from an Account
    /// </summary>
    public class Withdrawal : Transaction
    {
        public Withdrawal(DateTime date, Account acct, double amount) : base(date, acct, amount) { }

        public override double Execute()
        {
            var newBalance = StartingBalance - Amount;

            if (newBalance <= 0)
                throw new Exception("Not enough money in account to process the transaction.");

            return newBalance;
        }
    }
}
