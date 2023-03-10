using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Accounts
{
    public class MaxiAccount : Account
    {
        private readonly double interestRate = 0.001;
        private readonly double bonusInterestRate = 0.05;
        //private readonly double maxiBonusInterestRate = 0.05;

        public MaxiAccount() : base() {
            Type = AccountType.Checking;
            AccountName = "Maxi Account";
        }

        public override double CalculateSimpleInterest()
        {
            if (Transactions.Count == 0)
                return 0;

            var interest = 0.00;
            var today = DateProvider.Now.Date;
            var interestDate = Transactions.Min(t => t.Date);
            var balanceAtDate = 0.00;

            while (interestDate <= today)
            {
                balanceAtDate = Transactions.Where(t => t.Date <= interestDate).Sum(t => t.Amount);

                // Check if any withdrawal happened in the previous 10 days and adjust interest rate accordingly
                if(Transactions.Exists(t => t.Type == "withdrawal" && (interestDate - t.Date).TotalDays <= 10))
                {
                    interest += balanceAtDate * interestRate;

                }
                else
                {
                    interest += balanceAtDate * bonusInterestRate;
                }

                interestDate = interestDate.AddDays(1);
            }

            return interest;
        }

        //public override double CalculateSimpleInterest()
        //{
        //    if (Transactions.Count == 0)
        //        return 0;

        //    var interest = 0.00;
        //    var today = DateProvider.getInstance().Now().Date;
        //    var interestDate = Transactions.Min(t => t.Date);
        //    var balanceAtDate = 0.00;

        //    while (interestDate <= today)
        //    {
        //        balanceAtDate = Transactions.Where(t => t.Date <= interestDate).Sum(t => t.Amount);

        //        if (balanceAtDate <= 1000)
        //        {
        //            interest += 1000 * interestRate;
        //        }
        //        else if(balanceAtDate <= 2000)
        //        {
        //            interest += 1000 * interestRate;
        //            interest += (balanceAtDate - 1000) * bonusInterestRate;
        //        }
        //        else
        //        {
        //            interest += 1000 * interestRate;
        //            interest += (balanceAtDate - 1000) * bonusInterestRate;
        //            interest += (balanceAtDate - 2000) * maxiBonusInterestRate;
        //        }

        //        interestDate = interestDate.AddDays(1);
        //    }

        //    return interest;
        //}
    }
}
