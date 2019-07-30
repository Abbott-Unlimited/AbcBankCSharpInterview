using System;
using System.Collections.Generic;
using System.Linq;

namespace abc_bank
{
    public class Account
    {
        public int ID;
        public AccountType Type;
        public List<Transaction> Transactions;        

        public Account(AccountType type) 
        {
            Type = type;
            Transactions = new List<Transaction>();
        }

        public void Deposit(double amount) 
        {
            if (amount <= 0) throw new ArgumentException("amount must be greater than zero");
            else Transactions.Add(new Transaction(amount));
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0) throw new ArgumentException("amount must be greater than zero");
            else Transactions.Add(new Transaction(-amount));
        }

        public double InterestEarned() 
        {
            switch(Type)
            {
                case AccountType.Checking:
                    if (GetBalance() < 0) return GetBalance();
                    else return GetBalance() * 0.001;
                case AccountType.Savings:
                    if (GetBalance() < 0) return GetBalance();
                    if (GetBalance() <= 1000) return GetBalance() * 0.001;
                    else return (10 + (GetBalance() - 1000)) * 0.002;
                case AccountType.Maxi_Savings:
                    if (GetBalance() < 0) return GetBalance();
                    else if (GetBalance() <= 2000) return (20 + (GetBalance() - 1000)) * 0.05;
                    else if (GetBalance() <= 3000) return (70 + (GetBalance() - 2000)) * 0.1;
                    else if (Transactions.Any(transaction => transaction.Date >= DateTime.Now.Subtract(TimeSpan.FromDays(10)))) return GetBalance() * .5;
                    return GetBalance() * .001;
                default:
                    return GetBalance();
            }
        }

        public string GetStatement()
        {
            //Display Account Type
            var result = Type.ToString().Replace("_", " ") + " Account\n";

            //Display Transactions
            Transactions.ForEach(transaction =>
                result += "  " + 
                (transaction.Amount < 0 ? "withdrawal" : "deposit") + " " +
                string.Format("{0:C}", Math.Abs(transaction.Amount)) +
                "\n");

            //Display Total
            result += "Total " + GetBalance().ToString("C");

            return result;
        }
        public double GetBalance()
        {
            return Transactions.Sum(transaction => transaction.Amount);
        }
    }

    public enum AccountType { Checking, Savings, Maxi_Savings }
}