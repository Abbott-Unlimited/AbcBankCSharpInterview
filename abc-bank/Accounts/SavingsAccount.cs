using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Accounts
{
    public class SavingsAccount : Account
    {
        private readonly double interestRate = 0.001;
        private readonly double bonusInterestRate = 0.002;

        public SavingsAccount() : base() {
            Type = AccountType.Savings;
            AccountName = "Savings Account";
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

                if (balanceAtDate <= 1000)
                {
                    interest += 1000 * interestRate;
                }
                else
                {
                    interest += 1000 * interestRate;
                    interest += (balanceAtDate - 1000) * bonusInterestRate;
                }

                interestDate = interestDate.AddDays(1);
            }

            return interest;
        }

    }
}
