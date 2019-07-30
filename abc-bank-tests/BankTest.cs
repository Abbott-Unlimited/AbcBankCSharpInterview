using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {
        private static readonly double DOUBLE_DELTA = 1e-15;

        public Bank Bank;
        public Customer Customer;
        public Account Account;

        private void TestInit(AccountType type)
        {
            Bank = new Bank();
            Customer = new Customer("Bill");
            Account = new Account(type);
            Customer.Accounts.Add(Account);
            Bank.AddCustomer(Customer);
        }

        [TestMethod]
        public void CustomersSummaryDisplayed()
        {
            TestInit(AccountType.Checking);

            Assert.AreEqual("Customers Summary\n - Bill (1 account)", Bank.CustomersSummary());
        }

        [TestMethod]
        public void Checking_Interest_Rate_Paid()
        {
            TestInit(AccountType.Checking);
            Account.Deposit(100.0);

            Assert.AreEqual(0.1, Bank.TotalInterestPaid(), DOUBLE_DELTA);
        }
        
        [TestMethod]
        public void Savings_Interest_Rate_Paid()
        {
            TestInit(AccountType.Savings);
            Account.Deposit(1500.0);

            Assert.AreEqual(2.0, Bank.TotalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_Savings_Interest_Rate_Paid()
        {
            TestInit(AccountType.Maxi_Savings);
            Account.Deposit(3000.0);

            Assert.AreEqual(170.0, Bank.TotalInterestPaid(), DOUBLE_DELTA);
        }
    }
}
