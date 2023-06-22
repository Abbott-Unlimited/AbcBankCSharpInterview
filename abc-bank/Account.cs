using abc_bank.InterestCalculators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Account
    {
        public enum AccountType
        {
            Checking = 0,
            Savings,
            MaxiSavings
        }
        private readonly ICalculateInterest _calculateInterest;
        private readonly List<Transaction> _transactions;

        public Account(AccountType accountType) 
        {
            _calculateInterest = CalculateInterestFactory.Instance.GetNew(accountType);
            _transactions = new List<Transaction>();
        }

        public void Deposit(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            }
            _transactions.Add(new Transaction(amount));
        }

        public void Withdraw(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            }
            _transactions.Add(new Transaction(-amount));
        }

        public double InterestEarned() 
        {
            double amount = SumTransactions();
            double interest = _calculateInterest.Execute(_transactions);
            return interest;
        }
        public List<Transaction> GetTransactions()
        {
            return _transactions;
        }
        public double SumTransactions() {
            double sum = _transactions.Sum(x => x.amount);
            return sum;
        }
        public AccountType GetAccountType() 
        {
            return _calculateInterest.AccountType;
        }
    }
}
