using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {
        /// <summary>
        /// Tests that the customer summary is generated correctly.
        /// </summary>
        [TestMethod]
        public void CustomerSummary() 
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new Account(AccountType.CHECKING));
            bank.AddCustomer(john);
            Customer bob = new Customer("Bob");
            bob.OpenAccount(new Account(AccountType.SAVINGS))
               .OpenAccount(new Account(AccountType.CHECKING));
            bank.AddCustomer(bob);

            Assert.AreEqual("Customer Summary" + Environment.NewLine +
                    " - John (1 account)" + Environment.NewLine +
                    " - Bob (2 accounts)",
                bank.CustomerSummary());
        }

        /// <summary>
        /// Tests that interest is calculated correctly for checking accounts.
        /// </summary>
        [TestMethod]
        public void TestCheckingAccountInterest() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountType.CHECKING);
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.00m);

            Assert.AreEqual(0.10m, bank.totalInterestPaid());
        }

        /// <summary>
        /// Tests that interest is calculated correctly for savings accounts.
        /// </summary>
        [TestMethod]
        public void TestSavingsAccountInterest() {
            Bank bank = new Bank();
            Account savingsAccount = new Account(AccountType.SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(savingsAccount));

            savingsAccount.Deposit(1500.00m);

            Assert.AreEqual(2.00m, bank.totalInterestPaid());
        }

        /// <summary>
        /// Tests that interest is calculated correctly for Maxi-Savings accounts.
        /// </summary>
        [TestMethod]
        public void TestMaxiSavingsAccountInterest() {
            Bank bank = new Bank();
            Account maxiSavingsAccount = new Account(AccountType.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(maxiSavingsAccount));

            maxiSavingsAccount.Deposit(3000.00m);

            Assert.AreEqual(170.00m, bank.totalInterestPaid());
        }
    }
}
