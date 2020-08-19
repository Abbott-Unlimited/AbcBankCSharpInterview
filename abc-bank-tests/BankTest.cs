using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {
        [TestMethod]
        public void CustomerSummary_ReportsOnCustomersAndTheirOpenedAccounts()
        {
            Bank bofa = new Bank();
            Customer mark = new Customer("Mark");
            bofa.AddCustomer(mark);
            mark.OpenAccount(new Account(Account.CHECKING));
            mark.OpenAccount(new Account(Account.SAVINGS));

            string report = bofa.CustomerSummary();

            Assert.AreEqual("Customer Summary\n - Mark (2 accounts)", report);
        }

        [TestMethod]
        public void CustomerSummary_PluralizesAccountsToBeGrammaticallyCorrect()
        {
            Bank capfed = new Bank();
            Customer fred = new Customer("Fred");
            capfed.AddCustomer(fred);
            fred.OpenAccount(new Account(Account.CHECKING));
            Customer jane = new Customer("Jane");
            capfed.AddCustomer(jane);
            jane.OpenAccount(new Account(Account.CHECKING));
            jane.OpenAccount(new Account(Account.SAVINGS));

            string report = capfed.CustomerSummary();

            Assert.AreEqual("Customer Summary\n - Fred (1 account)\n - Jane (2 accounts)", report);
        }

        [TestMethod]
        public void CustomerSummary_HasNothingToReportOnWithoutCustomers()
        {
            Bank lonelyBank = new Bank();

            string report = lonelyBank.CustomerSummary();

            Assert.AreEqual("Customer Summary", report);
        }

        [TestMethod]
        public void TotalInterestPaid_ReportsOnAllCustomersAccounts()
        {
            Bank bofa = new Bank();

            Customer john = new Customer("John");
            bofa.AddCustomer(john);
            Account johnChecking = new Account(Account.CHECKING);
            john.OpenAccount(johnChecking);
            johnChecking.Deposit(10000.0);

            Customer peter = new Customer("Peter");
            bofa.AddCustomer(peter);
            Account peterMaxiSavings = new Account(Account.MAXI_SAVINGS);
            peter.OpenAccount(peterMaxiSavings);
            peterMaxiSavings.Deposit(3000.0);

            double result = bofa.totalInterestPaid();

            // John's $10,000 in checking at 0.01% = 10, plus
            // Peter's $3,000 in maxi savings (first thousand at 2% (20),
            //   second thousand at 5% (50), third at 10% (100)) = 170
            Assert.AreEqual(180.0, result);
        }
    }
}
