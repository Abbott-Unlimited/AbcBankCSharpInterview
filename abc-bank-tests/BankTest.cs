using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {
        [TestMethod]
        public void CustomerSummary() 
        {
            var bank = new Bank();
            var john = new Customer("John");
            john.OpenAccount(new Account(Account.Checking));
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void AddTwoCustomers()
        {
            var bank = new Bank();
            var john = new Customer("John");
            var mary = new Customer("Mary");

            john.OpenAccount(new Account(Account.MaxiSavings));
            mary.OpenAccount(new Account(Account.Checking));

            var customers = new List<Customer>
            {
                new Customer(john.GetName()),
                new Customer(mary.GetName())
            };

            bank.AddCustomers(customers);
        }

        [TestMethod]
        public void CustomerSummary_TwoAccounts()
        {
            var bank = new Bank();
            var john = new Customer("John");
            john.OpenAccount(new Account(Account.Checking));
            john.OpenAccount(new Account(Account.Savings));
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (2 accounts)", bank.CustomerSummary());
        }


        [TestMethod]
        public void CheckingAccount() {
            var bank = new Bank();
            var checkingAccount = new Account(Account.Checking);
            var bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.1, bank.TotalInterestPaid());
        }

        [TestMethod]
        public void Savings_account() {
            var bank = new Bank();
            var checkingAccount = new Account(Account.Savings);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(1500.0);

            Assert.AreEqual(2.0, bank.TotalInterestPaid());
        }

        [TestMethod]
        public void Maxi_savings_account() {
            var bank = new Bank();
            var checkingAccount = new Account(Account.MaxiSavings);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(3000.0);

            Assert.AreEqual(150.0, bank.TotalInterestPaid());
        }
    }
}
