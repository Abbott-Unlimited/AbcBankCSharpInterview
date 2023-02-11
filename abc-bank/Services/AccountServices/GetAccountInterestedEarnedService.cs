using abc_bank.Services.DateServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Services.AccountServices
{
    public class GetAccountInterestedEarnedService
    {
        private readonly IGetCurrentDateService getCurrentDateService;

        public GetAccountInterestedEarnedService() :this(new GetCurrentDateService()) { }
        public GetAccountInterestedEarnedService(IGetCurrentDateService getCurrentDateService)
        {
            this.getCurrentDateService = getCurrentDateService;
        }

        public decimal Get(Account account)
        {
            decimal interestEarned = 0;

            decimal totalTransactionsAmount = account.transactions.Sum(x => x.amount);

            if (totalTransactionsAmount <= 0)
                return interestEarned;

            switch (account.GetAccountType())
            {
                case AccountType.CHECKING:
                    return GetCheckingAccountInterestEarned(totalTransactionsAmount);
                case AccountType.SAVINGS:
                    return this.GetSavingsAccountInterestEarned(totalTransactionsAmount);
                case AccountType.MAXI_SAVINGS:
                    var lastWithdrawalDate = account.transactions
                        .Where(x => x.amount < 0)
                        .OrderByDescending(x => x.GetTransactionDate())
                        .FirstOrDefault()?.GetTransactionDate() ?? DateTime.MinValue;

                    return this.GetMaxiSavingsAccountInterestEarned(totalTransactionsAmount, lastWithdrawalDate);
            }

            return interestEarned;
        }

        private decimal GetCheckingAccountInterestEarned(decimal amount)
        {
            return amount * .001m;
        }

        private decimal GetSavingsAccountInterestEarned(decimal amount)
        {
            return amount <= 1000 ? amount * .001m : 1 + (amount - 1000) * .002m;
        }

        private decimal GetMaxiSavingsAccountInterestEarned(decimal amount, DateTime lastWithdrawalDate)
        {
            var currentDate = this.getCurrentDateService.Get();

            var daysSinceLastWithdrawal = Math.Round((currentDate - lastWithdrawalDate).TotalDays, 5);

            return amount * (daysSinceLastWithdrawal > 10 ? .05m : .01m);
        }
    }
}
