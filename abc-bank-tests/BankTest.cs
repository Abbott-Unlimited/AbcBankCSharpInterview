using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        private static readonly double DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void CustomerSummaryOneAccountDoesFormat() 
        {
            IBank bank = new Bank();
            ICustomer john = new Customer("John");
            john.OpenAccount(new CheckingAccount());
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.GetCustomerSummary());
        }

        [TestMethod]
        public void CustomerSummaryTwoAccountsDoesFormat()
        {
            IBank bank = new Bank();
            ICustomer john = new Customer("John");
            john.OpenAccount(new CheckingAccount());
            john.OpenAccount(new SavingsAccount());
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (2 accounts)", bank.GetCustomerSummary());
        }

        [TestMethod]
        public void AddCustomerOneCustomerAddsOneCustomer()
        {
            IBank bank = new Bank();
            ICustomer bill = new Customer("Bill");

            bank.AddCustomer(bill);

            Assert.AreEqual("Bill", bank.GetFirstCustomerName());
        }

        [TestMethod]
        public void GetFirstCustomerNoCustomersReturnsErrorString()
        {
            IBank bank = new Bank();

            Assert.AreEqual("error", bank.GetFirstCustomerName());

        }

        [TestMethod]
        public void TotalInterestPaidCheckingAccountIsOneTenthPercent() {
            IBank bank = new Bank();
            IAccount checkingAccount = new CheckingAccount();
            ICustomer bill = new Customer("Bill");
            bill.OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.1, bank.GetTotalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Savings_account() {
            IBank bank = new Bank();
            IAccount savingsAccount = new SavingsAccount();
            ICustomer bill = new Customer("Bill");
            bank.AddCustomer(bill);
            bill.OpenAccount(savingsAccount);

            savingsAccount.Deposit(1500.0);

            Assert.AreEqual(2.0, bank.GetTotalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_account() {
            IBank bank = new Bank();
            IAccount maxiSavingsAccount = new MaxiSavingsAccount();
            ICustomer bill = new Customer("Bill");
            bank.AddCustomer(bill);
            bill.OpenAccount(maxiSavingsAccount);

            maxiSavingsAccount.Deposit(3000.0);

            Assert.AreEqual(170.0, bank.GetTotalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void GetCustomerStatementShowsCustomersAndNumberOfAccounts()
        {
            IBank bank = new Bank();

            IAccount checkingAccount = new CheckingAccount();
            IAccount savingsAccount = new SavingsAccount();
            IAccount maxiAccount = new MaxiSavingsAccount();

            ICustomer henry = new Customer("Henry");
            ICustomer bill = new Customer("Bill");

            bank.AddCustomer(henry);
            bank.AddCustomer(bill);

            henry.OpenAccount(checkingAccount);
            henry.OpenAccount(savingsAccount);
            bill.OpenAccount(maxiAccount);

            Assert.AreEqual("Customer Summary" +
                    "\n - Henry (2 accounts)" +
                    "\n - Bill (1 account)", bank.GetCustomerSummary());
        }

        [TestMethod]
        public void GetTotalInterestPaidShowsAllInterestPaid()
        {
            IBank bank = new Bank();

            IAccount checkingAccount = new CheckingAccount();
            IAccount savingsAccount = new SavingsAccount();
            IAccount maxiAccount = new MaxiSavingsAccount();

            ICustomer henry = new Customer("Henry");
            ICustomer bill = new Customer("Bill");

            bank.AddCustomer(henry);
            bank.AddCustomer(bill);

            henry.OpenAccount(checkingAccount);
            henry.OpenAccount(savingsAccount);
            bill.OpenAccount(maxiAccount);

            checkingAccount.Deposit(200.00);
            savingsAccount.Deposit(1200.00);
            maxiAccount.Deposit(5000.00);

            Assert.AreEqual(
                (200.00 * .001)
                + (1000.00 * .001) + (200.00 * .002)
                + (1000.00 * .02) + (1000.00 * .05) + (3000.00 * .10),
                bank.GetTotalInterestPaid());
        }
    }
}
