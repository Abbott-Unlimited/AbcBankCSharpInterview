using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Account
    {
        public enum AccountType{
            CHECKING,
            SAVINGS,
            MAXI_SAVINGS
        }

        private readonly AccountType accountType;
        public List<Transaction> transactions;

        public Account(AccountType accountType) 
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
        }

        public void Deposit(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(-amount));
            }
        }

        public double InterestEarned() 
        {
            var amount = SumTransactions();
            switch(accountType){
                case AccountType.CHECKING:
                    return amount * 0.001;
                case AccountType.SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001;
                    return 1 + ((amount-1000) * 0.002);
                case AccountType.MAXI_SAVINGS:
                    if (TransactionsExist(DateTime.Now - TimeSpan.FromDays(10)))
                        return amount * 0.01;
                    else
                        return amount * 0.05;
                default:
                    throw new Exception("INVALID/MISSING ACCOUNT TYPE");
            }
        }

        public void DailyInterestDeposit()
        {
            var days = DateTime.IsLeapYear(DateTime.Now.Year) ? 364 : 365;
            Deposit(InterestEarned()/days);
        }

        public double SumTransactions() 
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        private bool TransactionsExist(DateTime fromDate) 
        {
            var withdrawals = transactions.Where(x => x.amount < 0 
                && x.transactionDate > fromDate).ToList();

            return withdrawals.Count() > 0;
        }

        public AccountType GetAccountType() 
        {
            return accountType;
        }

    }
}
