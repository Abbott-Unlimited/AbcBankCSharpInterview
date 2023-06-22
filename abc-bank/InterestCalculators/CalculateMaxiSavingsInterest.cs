using System;
using System.Collections.Generic;
using System.Linq;

namespace abc_bank.InterestCalculators
{
    public class CalculateMaxiSavingsInterest : ICalculateInterest
    {
        public Account.AccountType AccountType => Account.AccountType.MaxiSavings;

        public double Execute(List<Transaction> transactions)
        {
            double amount = 0;

            var orderedTransactions = transactions.OrderBy(x => x.transactionDate);
            var firstTransactionDateTime = orderedTransactions.First().transactionDate.Date;
            var currentDateTime = DateProvider.getInstance().Now();
            var daysToCalculateInterest = (currentDateTime - firstTransactionDateTime).TotalDays;

            for (var day = 0; day <= daysToCalculateInterest; day++)
            {
                var currentDate = firstTransactionDateTime.AddDays(day);
                amount += orderedTransactions.Where(x => x.transactionDate.Date == currentDate)
                                             .Sum(x => x.amount);
                double interest = 0.001;
                //get the last withdraw from the current date
                var lastWithdrawDateTime = transactions.Where(x => x.transactionDate.Date <= currentDate
                                                            && x.amount < 0)
                                                        .OrderBy(x => x.transactionDate)
                                                        .Select(x => x.transactionDate)
                                                        .LastOrDefault();

                if ((lastWithdrawDateTime - DateTime.Now).TotalDays < 10)
                {
                    interest = 0.05;
                }
                amount = DailyInterestCalculator.Instance.Calculate(amount, interest);
            }
            return amount;
        }
    }
}
