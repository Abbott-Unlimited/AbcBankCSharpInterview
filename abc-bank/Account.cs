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

        public void Deposit(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount));
            }
        }

        public void Deposit(double amount, DateTime dateTime)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(amount, dateTime));
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

        public void Withdraw(double amount, DateTime dateTime)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(-amount, dateTime));
            }
        }

        public double InterestEarned() 
        {
            return SumInterestEarnedDaily();
        }

        public double sumTransactions() {
           return CheckIfTransactionsExist(true);
        }

        private double CheckIfTransactionsExist(bool checkAll) 
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        public int GetAccountType() 
        {
            return accountType;
        }

        private double SumInterestEarnedDaily()
        {
            double balance = 0;
            double interestEarned = 0;

            if (transactions.Count == 0)
            {
                return 0;
            }

            List<Transaction> sequentialTransactions = transactions.OrderBy(t => t.GetDate()).ToList();

            DateTime eachDay = sequentialTransactions[0].GetDate().Date;

            while (eachDay <= sequentialTransactions[sequentialTransactions.Count - 1].GetDate().Date)
            {
                List<Transaction> sameDayTransactions = sequentialTransactions.Where(x => x.GetDate().Date == eachDay).ToList();

                foreach (Transaction transaction in sameDayTransactions)
                {
                    balance += transaction.amount;
                }

                double dailyInterestEarned = 0;

                if (balance <= 0)
                {
                    continue;
                }

                switch (accountType)
                {
                    case SAVINGS:
                        if (balance <= 1000)
                        {
                            dailyInterestEarned = balance * 0.001;
                        }
                        else
                        {
                            dailyInterestEarned = balance * 0.002;
                        }
                        break;
                    case MAXI_SAVINGS:
                        bool withdrawalInPast10Days = sequentialTransactions.Any(t => t.amount < 0 && (t.GetDate().Ticks < eachDay.AddDays(1).Date.Ticks && t.GetDate().Date >= eachDay.AddDays(-10).Date));

                        if (withdrawalInPast10Days)
                        {
                            dailyInterestEarned = balance * 0.001;
                        } else
                        {
                            dailyInterestEarned = balance * 0.05;
                        }
                        break;
                    default:
                        dailyInterestEarned = balance * 0.001;
                        break;
                }

                interestEarned += dailyInterestEarned;
                balance += dailyInterestEarned;


                eachDay = eachDay.AddDays(1).Date;
            }

            return interestEarned;
        }

    }
}
