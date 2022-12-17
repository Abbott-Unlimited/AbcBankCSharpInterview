using System;
using System.Collections.Generic;
using System.Linq;

namespace abc_bank
{
    public class Account
    {
        #region | Globals |
        public const int CHECKING = 0;
        public const int SAVINGS = 1;
        public const int MAXI_SAVINGS = 2;

        private readonly int accountType;
        public List<Transaction> transactions;

        public double balance
        {
            get
            {
                return SumTransactions();
            }
        }
        #endregion

        #region | Constructor |
        public Account(int accountType)
        {
            //  Validating Account Type
            if (!Enumerable.Range(CHECKING, MAXI_SAVINGS + 1).Contains(accountType))
            {
                throw new ArgumentException("invalid account type");
            }
            else
            {
                this.accountType = accountType;
                this.transactions = new List<Transaction>();
            }
        }
        #endregion

        #region | SumTransactions |
        private double SumTransactions()
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        #region | Commented as it sounds unnecessary |
        //private double CheckIfTransactionsExist(bool checkAll)
        //{
        //    double amount = 0.0;
        //    foreach (Transaction t in transactions)
        //        amount += t.amount;
        //    return amount;
        //}
        #endregion

        #endregion

        #region | GetAccountType |
        public int GetAccountType()
        {
            return accountType;
        }
        #endregion

        #region | Deposit |
        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(amount));
            }
        }
        #endregion

        #region | Withdraw |
        public void Withdraw(double amount)
        {
            // Integrated: Withdrawal amount should not exceed Deposit
            if (amount <= 0)
                throw new ArgumentException("amount must be greater than zero");
            else if (amount > SumTransactions())
                throw new ArgumentException("insufficient balance");
            else
                transactions.Add(new Transaction(-amount));
        }
        #endregion

        #region | Integrated: GetLastWithdrawalDate |
        private DateTime GetLastWithdrawalDate()
        {
            var withdrawals = transactions
                                .Where(t => t.amount < 0)
                                .ToList();

            if (withdrawals.Count == 0)
                return DateTime.MaxValue;

            return withdrawals.Max(t => t.transactionDate);
        }
        #endregion

        #region | Integrated: ExistsLastWithdrawalForPastDays |
        public bool CheckIfLastWithdrawalExistsAfterDays(int pastDays)
        {
            DateTime lastWithdrawalOn = GetLastWithdrawalDate();

            return lastWithdrawalOn.Subtract(DateTime.Now).Days <= pastDays;
        }
        #endregion
    }
}
