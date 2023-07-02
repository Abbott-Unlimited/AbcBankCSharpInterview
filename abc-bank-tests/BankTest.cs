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
        public void CustomerSummary() 
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new Account(Account.CHECKING));
            
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());

            john.OpenAccount(new Account(Account.MAXI_SAVINGS));

            var banksummarytwoaccts = bank.CustomerSummary();
            var totalnumberaccts = bank.TotalNumberAccounts();

            // test value
            Assert.AreEqual(2, totalnumberaccts);

            // test with text
            Assert.AreEqual("Customer Summary\n - John (2 accounts)", banksummarytwoaccts);

            Customer customerBill = new Customer("Bill Smith");
            customerBill.OpenAccount(new Account(Account.MAXI_SAVINGS));
            bank.AddCustomer(customerBill);

            Assert.AreEqual(3, bank.TotalNumberAccounts());

            // report bank's total interest paid
            bank.totalInterestPaid();
            
        }
        
        [TestMethod]
        public void InterestSummary()
        {
            Bank bank = new Bank();

            Customer john = new Customer("John");
            Account johnacct = new Account(Account.CHECKING);
            john.OpenAccount(johnacct);
            johnacct.Deposit(5000);

            bank.AddCustomer(john);

            var interestjohn = bank.InterestSummary();

            Assert.AreEqual("Bank Paid Interest Summary\r\n\r\nCustomer John: $5.00", interestjohn);


            Customer bill = new Customer("Bill");
            Account billaccount = new Account(Account.SAVINGS);
            bill.OpenAccount(billaccount);
            billaccount.Deposit(10000);


            bank.AddCustomer(bill);

            var interestall = bank.InterestSummary();

            Assert.AreEqual("Bank Paid Interest Summary\r\n\r\nCustomer John: $5.00\r\nCustomer Bill: $19.00", interestall);

        }

        [TestMethod]
        public void CheckingAccount() 
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);

            checkingAccount.Deposit(5289.98);

            var totalinterest = bank.totalInterestPaid();
            Assert.AreEqual(5.38998, totalinterest , DOUBLE_DELTA);


        }

        [TestMethod]
        public void Savings_account() 
        {
            Bank bank = new Bank();
            Account savingsAccount = new Account(Account.SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(savingsAccount));

            savingsAccount.Deposit(1500.0);

            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_account() 
        {
            Bank bank = new Bank();
            Account maxiAccount = new Account(Account.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(maxiAccount));

            maxiAccount.Deposit(3000.0);

            Assert.AreEqual(170.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void MaxiNewInterestMethod()
        {
            Bank bank = new Bank();
            Account maxiAccount = new Account(Account.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(maxiAccount));
            
            maxiAccount.Deposit(3000.0);
            maxiAccount.WithdrawSetDate(500, DateTime.Today);

            double interestearned = maxiAccount.InterestEarned();
            Assert.AreEqual(2.5, interestearned, DOUBLE_DELTA);


            maxiAccount = new Account(Account.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(maxiAccount));

            maxiAccount.Deposit(3000.0);

            // obviously, you can't have a withdrawal date prior to the deposit date.
            maxiAccount.WithdrawSetDate(500, DateTime.Parse("1/1/2023"));

            interestearned = maxiAccount.InterestEarned();

            Assert.AreEqual(125, interestearned, DOUBLE_DELTA);

            // another test
            maxiAccount = new Account(Account.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(maxiAccount));

            maxiAccount.Deposit(3000.0);

            // obviously, you can't have a withdrawal date prior to the deposit date.
            maxiAccount.WithdrawSetDate(500, DateTime.Parse("6/21/2023"));
            maxiAccount.WithdrawSetDate(500, DateTime.Parse("1/1/2023"));

            interestearned = maxiAccount.InterestEarned();

            Assert.AreEqual(2, interestearned, DOUBLE_DELTA);


        }
    }
}
