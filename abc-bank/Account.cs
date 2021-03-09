using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    delegate double InterestCalculator(Account account, DateTime asofDate);
    public class Account
    {
        private static readonly InterestCalculator interestCalculator1 = new InterestCalculator(InterestCalculatorSavings);
        private readonly InterestCalculator interestCalculator = interestCalculator1;   

        public Account(AccountType accountType, Customer customer)
        {
            AccountType = accountType;
            Transactions = new List<Transaction>();
            Customer = customer;
            if (accountType == AccountType.SAVINGS)
                interestCalculator = interestCalculator1;
            if (accountType == AccountType.MAXI_SAVINGS)
                interestCalculator = new InterestCalculator(InterestCalculatorMaxi);
            if (accountType == AccountType.CHECKING)
                interestCalculator = new InterestCalculator(InterestCalculatorChecking);
        }

        #region Properties
        public List<Transaction> Transactions { get; private set; }
        public AccountType AccountType { get; private set; }
        public Customer Customer { get; private set; }
        #endregion

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                Transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                Transactions.Add(new Transaction(-amount));
            }
        }

        public void Transfer(Account toAccount, double amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero");

            if (CurrentBalance < amount)
                throw new ArgumentException("Your current balance is less than the amount you want to transfer");

            if (Customer != toAccount.Customer)
                throw new ArgumentException("You can only transfer between your accounts");

            Withdraw(amount);
            toAccount.Deposit(amount);
        }

        public double CurrentBalance => Transactions.Sum(x => x.Amount);


        public double InterestEarned(DateTime asOfDate)
        {
            double amount = 0;
            amount += interestCalculator(this, asOfDate);
            return amount;
        }

        public double SumTransactions()
        {
            return CurrentBalance;
        }

        private static double InterestCalculatorSavings(Account account, DateTime asofDate)
        {
            var amount = 0.0;

            var results = account.Transactions.GroupBy(e => e.TransactionDate.Date)
                .OrderByDescending(
                    g => g.Key)
                 .Select(
                     r => new Transaction() { TransactionDate = r.Key, Amount = r.Sum(e => e.Amount) });

            foreach (var tran in results)
            {
                if (tran.Amount <= 1000)
                {
                    int days = InterestHelper.CalculateDaysToCompoundInterestDaily(1000, tran.Amount, .001, tran.TransactionDate, asofDate);
                    if (days > 1)
                    {
                        amount += InterestHelper.CalculateTotalWithCompoundInterestDaily(tran.Amount, 0.001, asofDate.AddDays(-days).Subtract(tran.TransactionDate).Days);
                        amount += InterestHelper.CalculateTotalWithCompoundInterestDaily((amount - 1000), 0.002, asofDate.Subtract(tran.TransactionDate).Days);
                    }
                    else
                        amount += InterestHelper.CalculateTotalWithCompoundInterestDaily(tran.Amount, 0.001, asofDate.Subtract(tran.TransactionDate).Days);
                }
                else
                {
                    amount += InterestHelper.CalculateTotalWithCompoundInterestDaily(1000, 0.001, asofDate.Subtract(tran.TransactionDate).Days);
                    amount += InterestHelper.CalculateTotalWithCompoundInterestDaily(tran.Amount - (1000 + amount), 0.002, asofDate.Subtract(tran.TransactionDate).Days);
                }
            }
            return amount;
        }

        private static double InterestCalculatorChecking(Account account, DateTime asofDate)
        {
            var amount = 0.0;

            var results = account.Transactions.GroupBy(e => e.TransactionDate.Date)
                .OrderByDescending(
                    g => g.Key)
                 .Select(
                     r => new Transaction() { TransactionDate = r.Key, Amount = r.Sum(e => e.Amount) });
            foreach (var tran in results)
                amount += InterestHelper.CalculateTotalWithCompoundInterestDaily(tran.Amount, .001, asofDate.Subtract(tran.TransactionDate).Days);
            return amount;
        }

        static double InterestCalculatorMaxi(Account account, DateTime asofDate)
        {
            //Assuming that maxi-savings accounts have an interest rate of 5% with no withdrawals within the past 10 days else 0.1%
            var amount = 0.0;

            var results = account.Transactions.GroupBy(e => e.TransactionDate.Date)
                .OrderByDescending(
                    g => g.Key)
                 .Select(
                     r => new Transaction() { TransactionDate = r.Key, Amount = r.Sum(e => e.Amount) });
            foreach (var tran in results)
            {
                if (account.Transactions.Where(x => x.Amount < 0 && x.TransactionDate.AddDays(10) > asofDate.Date).Count() > 0)
                    amount += InterestHelper.CalculateTotalWithCompoundInterestDaily(tran.Amount, 0.001, asofDate.Subtract(tran.TransactionDate).Days);
                else
                    amount += InterestHelper.CalculateTotalWithCompoundInterestDaily(tran.Amount, 0.05, asofDate.Subtract(tran.TransactionDate).Days);
            }
            return amount;
        }
    }

    public enum AccountType
    {
        CHECKING = 0,
        SAVINGS = 1,
        MAXI_SAVINGS = 2
    }

    public class InterestHelper
    {
        public static double CalculateTotalWithCompoundInterest(double principal, double interestRate, int compoundingPeriodsPerYear, double yearCount)
        {
            return principal * (double)Math.Pow((double)(1 + interestRate / compoundingPeriodsPerYear), compoundingPeriodsPerYear * yearCount) - principal;
        }

        public static double CalculateTotalWithCompoundInterestDaily(double principal, double interestRate, double dayCount)
        {
            return principal * (double)Math.Pow((double)(1 + interestRate / (365)), dayCount) - principal;
        }

        public static int CalculateDaysToCompoundInterestDaily(double interestEarned, double principal, double interestRate, DateTime startdate, DateTime endDate)
        {
            var days = Convert.ToInt32(Math.Ceiling(Math.Log(interestEarned / principal) / Math.Log(1 + interestRate / 365)));
            if ((endDate - startdate.Date).Days < days)
                days = -1;
            return days;
        }
    }
}
