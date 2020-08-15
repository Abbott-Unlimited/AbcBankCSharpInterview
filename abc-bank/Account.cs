using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Account
    {

        public const int CHECKING = 0;
        public const int SAVINGS = 1;
        public const int MAXI_SAVINGS = 2;

        private readonly int accountType;
        public List<Transaction> transactions;

        public Account(int accountType)
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
        }

        public void Deposit(double amount, DateTime? transactionDate = null)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(amount, transactionDate));
            }
        }

        public void Withdraw(double amount, DateTime? transactionDate = null)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(-amount, transactionDate));
            }
        }


        public double InterestEarned()
        {
            var firstDate = transactions.OrderBy(x => x.transactionDate).FirstOrDefault().transactionDate;
            var amount = 0.00;
            while (firstDate <= DateTime.Now)
            {
                var tempAmount = GetDailyInterest(firstDate);
                amount += tempAmount;
                Console.WriteLine(string.Format("{0}-{1}", firstDate, tempAmount));
                firstDate = firstDate.AddDays(1);
            }
            return amount;

        }

        public double sumTransactions(DateTime toDate)
        {
            double amount = 0.0;
            foreach (Transaction t in transactions.Where(x => x.transactionDate <= toDate))
                amount += t.amount;
            return amount;
        }



        public int GetAccountType()
        {
            return accountType;
        }

        private double GetDailyInterest(DateTime interestDate)
        {
            var amount = sumTransactions(interestDate);


            switch (accountType)
            {
                case SAVINGS:
                    if (amount <= 1000)
                        return amount * (0.001 / 365);
                    else
                        return (1.0 / 365) + (amount - 1000) * (0.002 / 365);
                case MAXI_SAVINGS:
                    if (transactions.Where(x => x.amount < 0 && x.transactionDate <= interestDate).Any(x => x.transactionDate >= interestDate.AddDays(-10)))
                    {
                        return amount * 0.001 / 365;
                    }
                    else
                    {
                        return amount * 0.05 / 365;
                    }
                default:
                    return amount * 0.001 / 365;
            }
        }
    }

}

