using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.InterestStrategies
{
    class SavingsInterest : InterestStrategy
    {
        private readonly double InterestDailyTier1 = 0.000003; // 0.1% interest daily 
        private readonly double InterestDailyTier2 = 0.000005; // 0.2% interest daily 
        public override double CalculateInterest(List<Transaction> transactions)
        {

            DateTime prevDate = transactions.First().transactionDate;
            var accountTotal = 0.0;
            var interestTotal = 0.0;

            foreach (var transaction in transactions)
            {
                accountTotal += transaction.amount;
                var days = (transaction.transactionDate - prevDate).Days;
                if (accountTotal < 1000)
                {
                    interestTotal += accountTotal * InterestDailyTier1 * (double)days;
                    accountTotal += interestTotal;
                }
                else
                {
                    var tempTotal = accountTotal - 1000;
                    interestTotal += accountTotal * InterestDailyTier2 * (double)days;
                    accountTotal += interestTotal;
                }
                
                

                prevDate = transaction.transactionDate;
            }
            return AccountingLogicHelpers.ReturnCleanValue(interestTotal);
        }
    }
}
