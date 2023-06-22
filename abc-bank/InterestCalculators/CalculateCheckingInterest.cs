using System;
using System.Collections.Generic;
using System.Linq;

namespace abc_bank.InterestCalculators
{
    public class CalculateCheckingInterest : ICalculateInterest
    {
        public Account.AccountType AccountType => Account.AccountType.Checking;
        public double Execute(List<Transaction> transactions)
        {
            double amount = 0;

            var orderedTransactions = transactions.OrderBy(x => x.transactionDate);
            var firstTransactionDateTime = orderedTransactions.First().transactionDate.Date;
            var currentDateTime = DateProvider.getInstance().Now();
            var daysToCalculateInterest = (currentDateTime.Date - firstTransactionDateTime).TotalDays;

            for (var day = 0; day <= daysToCalculateInterest; day++)
            {
                var currentDate = firstTransactionDateTime.AddDays(day);
                amount += orderedTransactions.Where(x => x.transactionDate.Date == currentDate)
                                             .Sum(x => x.amount);
                amount = DailyInterestCalculator.Instance.Calculate(amount, 0.001);
            }
            return amount;
        }
    }
}
