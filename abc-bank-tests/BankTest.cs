using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        private static readonly double DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void AddCustomerSuccessfully()
        {
            Bank bank = new Bank();
            Customer michael = new Customer("Michael");
            bank.AddCustomer(michael);

            Assert.IsTrue(bank.DoesCustomerExist(michael));
            Assert.IsTrue(bank.Customers.Exists(x => x.CustomerName == "Michael"));
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void AddCustomerThrowsError()
        {
            Bank bank = new Bank();
            Customer michael = new Customer("Michael");
            bank.AddCustomer(michael);

            Assert.IsTrue(bank.DoesCustomerExist(michael));
            Assert.IsTrue(bank.Customers.Exists(x => x.CustomerName == "Michael"));

            bank.AddCustomer(michael);
        }

        [TestMethod]
        public void CustomerSummary() 
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            bank.OpenAccount(john, new Account(AccountTypeEnum.CHECKING));
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void FormatPluralizesCorrectly()
        {
            Bank bank = new Bank();
            Customer michael = new Customer("Michael");
            Customer joe = new Customer("Joe");
            bank.AddCustomer(michael);
            bank.AddCustomer(joe);

            string result = bank.CustomerSummary(); 

            Assert.IsTrue(bank.DoesCustomerExist(michael));
            Assert.IsTrue(bank.DoesCustomerExist(joe));
            Assert.IsTrue(bank.Customers.Exists(x => x.CustomerName == "Michael"));
            Assert.IsTrue(bank.Customers.Exists(x => x.CustomerName == "Joe"));
            Assert.IsTrue(result.Contains("accounts"));
        }

        [TestMethod]
        public void OpenCheckingAccountSuccessfully()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountTypeEnum.CHECKING);
            Customer bill = new Customer("Bill");
            bank.OpenAccount(bill, checkingAccount);
            bank.AddCustomer(bill);

            Assert.IsTrue(bank.DoesCustomerExist(bill));
            Assert.IsTrue(bill.Accounts.Exists(x => x.AccountType == AccountTypeEnum.CHECKING));
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void OpenMultipleCheckingAccountThrowsError()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountTypeEnum.CHECKING);
            Customer bill = new Customer("Bill");
            bank.OpenAccount(bill, checkingAccount);
            bank.AddCustomer(bill);

            Assert.IsTrue(bank.DoesCustomerExist(bill));
            Assert.IsTrue(bill.Accounts.Exists(x => x.AccountType == AccountTypeEnum.CHECKING));

            bank.OpenAccount(bill, checkingAccount);
        }

        [TestMethod]
        public void CheckingAccountTotalInterestPaid() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountTypeEnum.CHECKING);
            Customer bill = new Customer("Bill");
            bank.OpenAccount(bill, checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Savings_accountTotalInterestPaid() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountTypeEnum.SAVINGS);
            Customer bill = new Customer("Bill");
            bank.OpenAccount(bill, checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(1500.0);

            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_accountTotalInterestPaid() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountTypeEnum.MAXI_SAVINGS);
            Customer bill = new Customer("Bill");
            bank.OpenAccount(bill, checkingAccount);
            bank.AddCustomer(bill);
            

            checkingAccount.Deposit(3000.0);
            double interest = bank.totalInterestPaid();

            Assert.AreEqual(170.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void CheckingForCustomerWorksSuccessfully()
        {
            Bank bank = new Bank();
            Customer michael = new Customer("Michael");
            bank.AddCustomer(michael);
            bool doesExist = bank.DoesCustomerExist(michael);

            Assert.IsTrue(doesExist);
        }

        [TestMethod]
        public void CheckingForCustomerNotExistingWorksSuccessfully()
        {
            Bank bank = new Bank();
            Customer michael = null;
            bool doesExist = bank.DoesCustomerExist(michael);

            Assert.IsFalse(doesExist);
        }
    }
}
