using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountTest
    {

        [TestMethod]
        public void Deposit() 
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(1500.0);

            double expectedBalance = 1500.0;
            double actualBalance = checkingAccount.sumAllTransactions();

            Assert.AreEqual(expectedBalance, actualBalance);
        }

        [TestMethod]
        public void Withdraw() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Withdraw(1500.0);

            double expectedBalance = -1500.0;
            double actualBalance = checkingAccount.sumAllTransactions();

            Assert.AreEqual(expectedBalance, actualBalance);
        }

        //ADDITIONAL FEATURE ADDED
        //A customer can transfer between their accounts.
        [TestMethod]
        public void Transfer() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

            bank.AddCustomer(new Customer("Bill")
                .OpenAccount(new Account(Account.CHECKING))
                .OpenAccount(savingsAccount));

            checkingAccount.Deposit(1500.0);
            checkingAccount.Transfer(750.0, savingsAccount);

            double expectedCheckBalance = 750.0;
            double expectedSavBalance = 750.0;
            double actualCheckBalance = checkingAccount.sumAllTransactions();
            double actualSavBalance = checkingAccount.sumAllTransactions();

            Assert.IsTrue(actualCheckBalance == expectedCheckBalance && actualSavBalance == expectedSavBalance ? true : false);
        }

    }
}
