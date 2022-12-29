using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        private static readonly double DOUBLE_DELTA = 1e-13;

        [TestMethod]
        public void CustomerSummary()
        {
            //Bank bank = new Bank();
            //Customer john = new Customer("John");
            //john.OpenAccount(new CheckingAccount());
            //bank.AddCustomer(john);
            Customer john = new Customer("John");
            Account johnCheckingAccount = new CheckingAccount();
            Account johnSavingAccount = new SavingAccount();
            Account johnMaxiSavingAccount = new MaxiSavingAccount();
            john.OpenAccount(johnCheckingAccount);
            john.OpenAccount(johnSavingAccount);
            john.OpenAccount(johnMaxiSavingAccount);

            Customer sara = new Customer("Sara");
            Account saraCheckingAccount = new CheckingAccount();
            Account saraSavingAccount = new SavingAccount();
            sara.OpenAccount(saraCheckingAccount);
            sara.OpenAccount(saraSavingAccount);

            Customer bob = new Customer("Bob");
            Account bobCheckingAccount = new CheckingAccount();
            bob.OpenAccount(bobCheckingAccount);

            Bank bank = new Bank();
            bank.AddCustomer(john);
            bank.AddCustomer(sara);
            bank.AddCustomer(bob);

            Assert.AreEqual("Customer Summary\n - John (3 accounts)" +
                             "\n - Sara (2 accounts)" +
                             "\n - Bob (1 account)", bank.CustomerSummary());
            //Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void CheckingAccount() {
            Bank bank = new Bank();
            Account checkingAccount = new CheckingAccount();
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0, false);

            Assert.AreEqual(0.1, bank.TotalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Savings_account() {
            Bank bank = new Bank();
            Account saving = new SavingAccount();
            bank.AddCustomer(new Customer("Bill").OpenAccount(saving));

            saving.Deposit(3000.0, false);
            saving.transactions[0].transactionDate = DateTime.Now.AddDays(-100);

            Assert.AreEqual(16.4830223041808, bank.TotalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_account() {
            Bank bank = new Bank();
            Account maxiSaving = new MaxiSavingAccount();
            bank.AddCustomer(new Customer("Bill").OpenAccount(maxiSaving));

            maxiSaving.Deposit(3000.0, false);
            maxiSaving.transactions[0].transactionDate = DateTime.Now.AddDays(-11);

            Assert.AreEqual(150.0, bank.TotalInterestPaid());
        }

        [TestMethod]
        public void TotalInterestPaidReportTest()
        {
            Customer john = new Customer("John");
            Account johnCheckingAccount = new CheckingAccount();
            johnCheckingAccount.Deposit(1000.00, false);
            john.OpenAccount(johnCheckingAccount);

            Customer sara = new Customer("Sara");
            Account saraCheckingAccount = new CheckingAccount();
            saraCheckingAccount.Deposit(2000.00, false); 
            sara.OpenAccount(saraCheckingAccount);

            Customer bob = new Customer("Bob");
            Account bobCheckingAccount = new CheckingAccount();
            bobCheckingAccount.Deposit(3000.00, false);
            bob.OpenAccount(bobCheckingAccount);

            Bank bank = new Bank();
            bank.AddCustomer(john);
            bank.AddCustomer(sara);
            bank.AddCustomer(bob);

            Assert.AreEqual("Total Interet Paid:\n6", bank.TotalInterestPaidReport());

        }

    }
}
