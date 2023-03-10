using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Accounts
{
    public class CheckingAccount : Account
    {
        private readonly double interestRate = 0.001;
        public CheckingAccount() : base() {
            Type = AccountType.Checking;
            AccountName = "Checking Account";
        }

        public override double CalculateSimpleInterest()
        {
            if(Transactions.Count == 0)
                return 0;

            var interest = 0.00;
            var today = DateProvider.Now.Date;
            var interestDate = Transactions.Min(t => t.Date);

            while(interestDate <= today)
            {
                interest += Transactions.Where(t => t.Date <= interestDate).Sum(t => t.Amount) * interestRate;
                interestDate = interestDate.AddDays(1);
            }

            return interest;
        }
    }
}
