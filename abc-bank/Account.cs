using System;
using System.Collections.Generic;
using System.Linq;

namespace abc_bank
{
    public class Account
    {
        public int ID;
        private Type _Type;
        private List<Transaction> _Transactions;        

        public Account(Type type) 
        {
            _Type = type;
            _Transactions = new List<Transaction>();
        }

        public void Deposit(double amount) 
        {
            if (amount <= 0) throw new ArgumentException("amount must be greater than zero");
            else _Transactions.Add(new Transaction(amount));
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0) throw new ArgumentException("amount must be greater than zero");
            else _Transactions.Add(new Transaction(-amount));
        }

        public void TransferFundsTo(Account toAccount, double amount)
        {
            if (GetCurrentBalance() >= amount)
            {
                Withdraw(amount);
                toAccount.Deposit(amount);
            }
        }

        public double InterestEarned() 
        {
            var balance = GetCurrentBalance();
            var result = 0.0;
            if (balance <= 0) return result;

            switch (_Type)
            {
                case Type.Checking:
                    return balance * 0.001;
                case Type.Savings:
                    if (balance <= 1000) result = balance * 0.001;
                    else result = 1 + ((balance - 1000) * 0.002);
                    return result;
                case Type.Maxi_Savings:
                    if (balance <= 1000) result = (balance * 0.02);
                    else if (balance <= 2000) result = 20 + ((balance - 1000) * 0.05);
                    else result = 70 + ((balance - 2000) * 0.1);
                    return result;

                    //Feature #2 
                    //result = _Transactions.Any(transaction =>
                    //    (transaction.Amount > 0 &&
                    //     transaction.Date >= DateTime.Now.Subtract(TimeSpan.FromDays(10))))
                    //    ? balance * .05
                    //    : balance * 0.001;
                    //return result;
                default:
                    return result;
            }
        }

        public string GetStatement()
        {
            //Display Account Type
            var result = _Type.ToString().Replace("_", " ") + " Account\n";

            //Display Transactions
            _Transactions.ForEach(transaction =>
                result += "  " + 
                (transaction.Amount < 0 ? "withdrawal" : "deposit") + " " +
                string.Format("{0:C}", Math.Abs(transaction.Amount)) +
                "\n");

            //Display Total
            result += "Total " + GetCurrentBalance().ToString("C");

            return result;
        }

        /// <summary>
        /// Returns the account balance
        /// </summary>
        /// <returns>Returns the current balance for this account.</returns>
        public double GetCurrentBalance()
        {
            return _Transactions.Sum(transaction => transaction.Amount);
        }
    }

    public enum Type { Checking, Savings, Maxi_Savings }
}