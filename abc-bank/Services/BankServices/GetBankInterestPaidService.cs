using abc_bank.Services.AccountServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Services.BankServices
{
    public class GetBankInterestPaidService
    {
        private readonly GetAccountInterestedEarnedService getAccountInterestedEarnedService;

        public GetBankInterestPaidService()
        {
            this.getAccountInterestedEarnedService = new GetAccountInterestedEarnedService();
        }

        public decimal Get(Bank bank)
        {
            var totalInterestPaid = 0m;

            foreach (var customer in bank.GetCustomers())
            {
                foreach (var account in customer.GetAccounts())
                {
                    totalInterestPaid += this.getAccountInterestedEarnedService.Get(account);
                }
            }

            return totalInterestPaid;
        }
    }
}
