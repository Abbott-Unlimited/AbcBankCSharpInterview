using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank.Entities;
using abc_bank.Enums;
using abc_bank.Services;
using abc_bank.Helpers;


namespace abc_bank_tests
{
    [TestClass]
    public class BankServicesTests
    {

        private static readonly double DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void AddCustomerTest()
        {
            BankServices bs = new BankServices();
            Customer billy = new Customer("Billy");
            bs.AddCustomer(billy);

            Assert.AreEqual("Billy", bs.GetFirstCustomer());
        }

        [TestMethod]
        public void CustomerSummaryTest()
        {
            BankServices bs = new BankServices();
            Customer john = new Customer("John");
            bs.AddCustomer(john);

            bs.OpenAccount(john, new Account(AccountTypeEnum.CHECKING));

            Assert.AreEqual("Customer Summary\n - John (1 account)", bs.CustomerSummary());
        }

        [TestMethod]
        public void CheckingAccountDepositTest()
        {
            BankServices bs = new BankServices();
            Account checkingAccount = new Account(AccountTypeEnum.CHECKING);
            Customer bill = new Customer("Bill");
            bs.OpenAccount(bill, checkingAccount);

            bs.Deposit(checkingAccount, 100.0);

            Assert.AreEqual(0.1, bs.TotalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void SavingsAccountDepositTest()
        {
            BankServices bs = new BankServices();
            Account savingsAccount = new Account(AccountTypeEnum.SAVINGS);
            var cust = new Customer("Bill");
            bs.AddCustomer(cust);
            bs.OpenAccount(cust, savingsAccount);

            bs.Deposit(savingsAccount, 1500.0);

            Assert.AreEqual(2.0, bs.TotalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void MaxiSavingsAccountDepositTest()
        {
            BankServices bs = new BankServices();
            Account maxiSavings = new Account(AccountTypeEnum.MAXI_SAVINGS);
            var cust = new Customer("Bill");
            bs.AddCustomer(cust);
            bs.OpenAccount(cust, maxiSavings);

            bs.Deposit(maxiSavings, 3000.00);

            Assert.AreEqual(150.0, bs.TotalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void CheckingAccountWithdrawTest()
        {
            BankServices bs = new BankServices();
            Account checkingAccount = new Account(AccountTypeEnum.CHECKING);
            Customer bill = new Customer("Bill");
            bs.OpenAccount(bill, checkingAccount);
            checkingAccount.Transactions.Add(new Transaction(200.00));

            bs.Withdraw(checkingAccount, 100.0);

            Assert.AreEqual(0.1, bs.TotalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void SavingsAccountWithdrawTest()
        {
            BankServices bs = new BankServices();
            Account savingsAccount = new Account(AccountTypeEnum.SAVINGS);
            var cust = new Customer("Bill");
            bs.AddCustomer(cust);
            bs.OpenAccount(cust, savingsAccount);
            savingsAccount.Transactions.Add(new Transaction(1700.00));

            bs.Withdraw(savingsAccount, 200.00);

            Assert.AreEqual(2.0, bs.TotalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void MaxiSavingsAccountWithdrawTest()
        {
            BankServices bs = new BankServices();
            Account maxiSavings = new Account(AccountTypeEnum.MAXI_SAVINGS);
            var cust = new Customer("Bill");
            bs.AddCustomer(cust);
            bs.OpenAccount(cust, maxiSavings);
            maxiSavings.Transactions.Add(new Transaction(3200.00));

            bs.Withdraw(maxiSavings, 200.00);

            Assert.AreEqual(3, bs.TotalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void GetNumberOfAccountsTestOneAccount()
        {
            BankServices bs = new BankServices();

            Customer oscar = new Customer("Oscar");
            bs.OpenAccount(oscar, new Account(AccountTypeEnum.SAVINGS));

            Assert.AreEqual(1, bs.GetNumberOfAccounts(oscar));
        }

        [TestMethod]
        public void GetNumberOfAccountsTestTwoAccount()
        {
            BankServices bs = new BankServices();

            Customer oscar = new Customer("Oscar");
            bs.OpenAccount(oscar, new Account(AccountTypeEnum.SAVINGS)); 
            bs.OpenAccount(oscar, new Account(AccountTypeEnum.CHECKING));

            Assert.AreEqual(2, bs.GetNumberOfAccounts(oscar));
        }

        [TestMethod]
        public void GetNumberOfAccountsTestThreeAccounts()
        {
            BankServices bs = new BankServices();

            Customer oscar = new Customer("Oscar");
            bs.OpenAccount(oscar, new Account(AccountTypeEnum.SAVINGS));
            bs.OpenAccount(oscar, new Account(AccountTypeEnum.CHECKING));
            bs.OpenAccount(oscar, new Account(AccountTypeEnum.MAXI_SAVINGS));

            Assert.AreEqual(3, bs.GetNumberOfAccounts(oscar));
        }

        [TestMethod]
        public void GetFirstCustomerTest()
        {
            BankServices bs = new BankServices();
            Customer billy = new Customer("Billy");
            bs.AddCustomer(billy);
            Customer hank = new Customer("Hank");
            bs.AddCustomer(hank);
            Customer waylen = new Customer("Waylen");
            bs.AddCustomer(waylen);

            Assert.AreEqual("Billy", bs.GetFirstCustomer());
        }
        [TestMethod]
        public void MaxiSavingsAccountTransferTest()
        {
            BankServices bs = new BankServices();
            Account maxiSavings = new Account(AccountTypeEnum.MAXI_SAVINGS);
            var cust = new Customer("Bill");
            bs.AddCustomer(cust);
            bs.OpenAccount(cust, maxiSavings);

            Account savingsAccount = new Account(AccountTypeEnum.SAVINGS);
            bs.OpenAccount(cust, savingsAccount);

            bs.Deposit(maxiSavings, 3000.00);
            bs.Deposit(savingsAccount, 400.00);

            bs.Transfer(maxiSavings, savingsAccount, 600.00);

            Assert.AreEqual(2400.00, InterestCalculations.SumTransactions(maxiSavings), DOUBLE_DELTA);
            Assert.AreEqual(1000.00, InterestCalculations.SumTransactions(savingsAccount), DOUBLE_DELTA);
        }
    }
}
