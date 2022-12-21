using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static abc_bank.Utilities.BankValues;

namespace abc_bank.Controllers
{
    internal class InterestController
    {
        /// <summary>
        /// Formulas to calculate interest:
        /// Checking accounts have a flat rate of 0.1% - X
        /// Savings accounts have a rate of 0.1% for the first $1,000 then 0.2% - X        
        /// Maxi-Savings accounts have an interest rate of 5% if no withdrawals in the past 10 days otherwise 0.1%
        /// </summary>
        public static double CalculateInterestEarned(Guid accountID)
        {
            var account = AccountController.GetAccountByID(accountID);

            double amount = account.AccountAmount;
            DateTime tenDaysAgo = DateProvider.GetInstance().Now().AddDays(-10);
            bool hasWithdrawnWithinLast10Days = account.Transactions.Any(x => x.transactionDate > tenDaysAgo);
            var interest = 0.0d;

            switch (account.Type)
            {
                case AccountType.CHECKING:
                    interest = amount * POINT_ONE_PERCENT_INTEREST;
                    account.TotalInterestEarned += interest;
                    return interest;

                case AccountType.SAVINGS:
                    if (amount <= 1000)
                    {
                        interest = amount * POINT_ONE_PERCENT_INTEREST;
                        account.TotalInterestEarned += interest;
                        return interest;
                    }
                    else
                    {
                        interest = amount * POINT_TWO_PERCENT_INTEREST;
                        account.TotalInterestEarned += interest;
                        return interest;

                    }
                case AccountType.MAXI_SAVINGS:      
                    if (hasWithdrawnWithinLast10Days)
                    {
                        interest = amount * POINT_ONE_PERCENT_INTEREST;
                        account.TotalInterestEarned += interest;
                        return interest;
                    }

                    interest = amount * TEN_PERCENT_INTEREST;
                    account.TotalInterestEarned += interest;
                    return interest;

                default:
                    return amount * POINT_ONE_PERCENT_INTEREST;
            }
        }
    }
}
