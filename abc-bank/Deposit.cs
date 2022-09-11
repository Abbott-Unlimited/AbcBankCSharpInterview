using System;
using System.Security.AccessControl;

namespace abc_bank
{
    /// <summary>
    /// Describes a Deposit to an Account
    /// </summary>
    public class Deposit : Transaction
    {
        public Deposit(DateTime date, Account acct, double amount) : base(date, acct, amount){}

        public override double Execute()
        {
            return StartingBalance + Amount;
        }
    }
}
