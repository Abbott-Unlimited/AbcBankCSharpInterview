using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {
        [TestCategory("CustomerSummary"), TestMethod]
        public void CustomerSummary() 
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            
            john.OpenAccount(new Account(Account.CHECKING));
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        
        [TestCategory("TotalInterestPaid"), TestMethod]
        public void CheckingAccount() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer("Bill");
                
            bill.OpenAccount(checkingAccount);
            bank.AddCustomer(bill);
            checkingAccount.Deposit(100.0m);

            Assert.AreEqual(0.1m, bank.TotalInterestPaid());
        }

        [TestCategory("TotalInterestPaid"), TestMethod]
        public void SavingsAccountOver1000() {
            Bank bank = new Bank();
            Account savingsAccount = new Account(Account.SAVINGS);
            Customer bill = new Customer("Bill");

            bill.OpenAccount(savingsAccount);
            bank.AddCustomer(bill);
            savingsAccount.Deposit(1500.0m);

            Assert.AreEqual(2.0m, bank.TotalInterestPaid());
        }
        
        [TestCategory("TotalInterestPaid"), TestMethod]
        public void SavingsAccountUnder1000() {
            Bank bank = new Bank();
            Account savingsAccount = new Account(Account.SAVINGS);
            Customer bill = new Customer("Bill");

            bill.OpenAccount(savingsAccount);
            bank.AddCustomer(bill);
            savingsAccount.Deposit(500.0m);

            Assert.AreEqual(0.5m, bank.TotalInterestPaid());
        }
        
        [TestCategory("TotalInterestPaid"), TestMethod]
        public void MaxiSavingsAccountDepositOnly() {
            Bank bank = new Bank();
            Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
            Customer bill = new Customer("Bill");
            
            
            bill.OpenAccount(maxiSavingsAccount);
            bank.AddCustomer(bill);
            maxiSavingsAccount.Deposit(3000.0m);

            Assert.AreEqual(150.0m, bank.TotalInterestPaid());
        }
        
        [TestCategory("TotalInterestPaid"), TestMethod]
        public void MaxiSavingsAccountWithWithdrawl() {
            Bank bank = new Bank();
            Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
            Customer bill = new Customer("Bill");
            
            bill.OpenAccount(maxiSavingsAccount);
            bank.AddCustomer(bill);
            maxiSavingsAccount.Deposit(3000.0m);
            maxiSavingsAccount.Withdraw(1000.0m);

            Assert.AreEqual(2m, bank.TotalInterestPaid());
        }
        
        [TestCategory("GetFirstCustomer"), TestMethod]
        public void GetFirstCustomer()
        {
            String name = "Bill";
            Bank bank = new Bank();
            Customer bill = new Customer(name);
            
            bank.AddCustomer(bill);

            Assert.AreEqual(name, bank.GetFirstCustomer());
        }
        
        [TestCategory("GetFirstCustomer"), TestMethod]
        public void GetFirstCustomerFailure() {
            Bank bank = new Bank();

            Assert.AreEqual("Error", bank.GetFirstCustomer());
        }
    }
}
