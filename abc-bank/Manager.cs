using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Manager
    {
        private Bank bank { get; set; }
        public Manager(Bank bank)
        {
            this.bank = bank;
        }

        public string GetCustomersReport()
        {
            return bank.CustomerSummary();
        }

        public string GetTotalInterestPaidReport()
        {
            var totalInterest = bank.GetTotalInterestPaid().ToString("C");
            return $"Total Interest Paid: {totalInterest}";
        }
    }
}
