using System;
using System.Collections.Generic;
using System.Linq;

namespace abc_bank.InterestCalculators
{
    public class CalculateSavingsInterest : ICalculateInterest
    {
        public Account.AccountType AccountType => Account.AccountType.Savings;

        public double Execute(List<Transaction> transactions)
        {
            double amount = 0;

            var orderedTransactions = transactions.OrderBy(x => x.transactionDate);
            var firstTransactionDateTime = orderedTransactions.First().transactionDate.Date;
            var currentDateTime = DateProvider.getInstance().Now();
            var daysToCalculateInterest = (currentDateTime - firstTransactionDateTime).TotalDays;
            
            for(var day = 0; day <= daysToCalculateInterest; day++)
            {
                var currentDate = firstTransactionDateTime.AddDays(day);
                amount += orderedTransactions.Where(x => x.transactionDate.Date == currentDate)
                                             .Sum(x => x.amount);
                if (amount <= 1000)
                {
                    amount = DailyInterestCalculator.Instance.Calculate(amount,.001);
                }
                else
                {
                    amount = DailyInterestCalculator.Instance.Calculate(amount - 1000, 0.002);
                    amount += DailyInterestCalculator.Instance.Calculate(1000, .001);
                }
            }
            return amount;
        }
    }
}
