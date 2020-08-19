using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests.CustomerTests
{
    [TestClass]
    public class GetStatementShould
    {
        [TestMethod]
        public void StatementFormattedCorrectly()
        {
            var checkingAccount = new Account(AccountType.CHECKING);
            var savingsAccount = new Account(AccountType.SAVINGS);

            var sut = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.AddTransaction(100.0);
            savingsAccount.AddTransaction(4000.0);
            savingsAccount.AddTransaction(-200.0);

            var result = sut.GetStatement();

            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "\tdeposit $100.00\n" +
                    "Total $100.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "\tdeposit $4,000.00\n" +
                    "\twithdrawal $200.00\n" +
                    "Total $3,800.00\n" +
                    "\n" +
                    "Total In All Accounts $3,900.00", result);
        }
    }
}
