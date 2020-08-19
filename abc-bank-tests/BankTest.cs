using abc_bank;
using abc_bank.Accounts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {
        [TestMethod]
        public void GetCustomerSummaryReport_ListsCustomersAndTheirOpenedAccounts()
        {
            Bank bofa = new Bank();
            Customer mark = new Customer("Mark");
            bofa.AddCustomer(mark);
            mark.OpenAccount(new CheckingAccount());
            mark.OpenAccount(new SavingsAccount());
            Customer jim = new Customer("Jim");
            bofa.AddCustomer(jim);
            jim.OpenAccount(new SavingsAccount());

            string report = bofa.GetCustomerSummaryReport();

            Assert.AreEqual("Customer Summary\n - Mark (2 accounts)\n - Jim (1 account)", report);
        }

        [TestMethod]
        public void GetCustomerSummaryReport_HasNothingToReportOnWithoutCustomers()
        {
            Bank lonelyBank = new Bank();

            string report = lonelyBank.GetCustomerSummaryReport();

            Assert.AreEqual("Customer Summary", report);
        }

        [TestMethod]
        public void TotalInterestPaid_ReportsOnAllCustomersAccounts()
        {
            Bank bofa = new Bank();

            Customer john = new Customer("John");
            bofa.AddCustomer(john);
            Account johnChecking = new CheckingAccount();
            john.OpenAccount(johnChecking);
            johnChecking.Deposit(10000.0);

            Customer peter = new Customer("Peter");
            bofa.AddCustomer(peter);
            Account peterMaxiSavings = new MaxiSavingsAccount();
            peter.OpenAccount(peterMaxiSavings);
            peterMaxiSavings.Deposit(3000.0);

            double result = bofa.TotalInterestPaid;

            // John's $10,000 in checking at 0.01% = 10, plus
            // Peter's $3,000 in maxi savings (first thousand at 2% (20),
            //   second thousand at 5% (50), third at 10% (100)) = 170
            Assert.AreEqual(180.0, result);
        }
    }
}
