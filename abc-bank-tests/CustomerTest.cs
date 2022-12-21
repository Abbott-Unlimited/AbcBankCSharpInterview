using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestApp()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            var expected = @"Statement for Henry\n\nChecking Account\n  deposit $100.00\nTotal $100.00\n\nSavings Account\n  deposit $4,000.00\n  withdrawal $200.00\nTotal $3,800.00\n\nTotal In All Accounts $3,900.00";
            var actual = henry.GetStatement();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Check_Savings_Account_Creation_Only_Creates_One_Account()
        {
            Customer oscar = new Customer("Oscar").OpenAccount(new Account(Account.SAVINGS));
            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void Check_Savings_And_Checking_Creation_Only_Opens_Two_Accounts()
        {
            Customer oscar = new Customer("Oscar")
                 .OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));
            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        //Placeholder method is for future use and ignored at runtime currently
        [TestMethod]
        [Ignore]
        public void FutureUseThreeAccountTestMethod()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }
    }
}
