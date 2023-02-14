using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.InterestStrategies
{
    class StandardInterest : InterestStrategy
    {
        private readonly double InterestDaily = 0.000003; // 0.1% interest daily 
        public override double CalculateInterest(List<Transaction> transactions)
        {

            DateTime prevDate = transactions.First().transactionDate;
            var accountTotal = 0.0;
            var interestTotal = 0.0;
            
            foreach (var transaction in transactions)
            {
                accountTotal += transaction.amount;
                var days = (transaction.transactionDate - prevDate).Days;
                interestTotal += accountTotal * InterestDaily * (double)days;
                accountTotal += interestTotal;
                
                prevDate = transaction.transactionDate;
            }
            return AccountingLogicHelpers.ReturnCleanValue(interestTotal);
        }
    }
}
