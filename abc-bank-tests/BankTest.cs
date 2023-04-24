using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {
        [TestMethod]
        public void Get_Customer_Summary_Report_For_One_Customer() 
        {
            // Arraange
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new CheckingAccount());
            bank.AddCustomer(john);
            
            string expected = "Customer Summary\n - John (1 account)";

            // Act
            string actual = bank.PrintCustomerSummary();
            
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Get_Customer_Summary_Report_For_Three_Customers()
        {
            // Arraange
            Bank bank = new Bank();
        
            Customer john = new Customer("John");
            john.OpenAccount(new CheckingAccount());
            bank.AddCustomer(john);

            Customer bill = new Customer("Bill");
            bill.OpenAccount(new CheckingAccount());
            bill.OpenAccount(new SavingsAccount());
            bank.AddCustomer(bill);

            Customer oscar = new Customer("Oscar");
            oscar.OpenAccount(new CheckingAccount());
            oscar.OpenAccount(new MaxiSavingsAccount());
            oscar.OpenAccount(new SavingsAccount());
            bank.AddCustomer(oscar);

            string expected = "Customer Summary" +
                "\n - John (1 account)" +
                "\n - Bill (2 accounts)" +
                "\n - Oscar (3 accounts)";

            // Act
            string actual = bank.PrintCustomerSummary();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Get_Total_Interest_Paid_For_Checking_Account()
        {
            // Arrange
            Bank bank = new Bank();
            var checkingAccount = new CheckingAccount();
            Customer bill = new Customer("Bill");
            bill.OpenAccount(checkingAccount);
            bank.AddCustomer(bill);
            checkingAccount.RequestDeposit(100.0m);
            
            decimal expected = 0.1m;

            // Act
            decimal actual = bank.GetTotalInterestPaid();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Get_Total_Interest_Paid_For_Savings_Account()
        {
            // Arrange
            Bank bank = new Bank();
            var savingsAccount = new SavingsAccount();
            bank.AddCustomer(new Customer("Bill").OpenAccount(savingsAccount));
            savingsAccount.RequestDeposit(1500.0m);
            
            decimal expected = 2.0m;

            // Act
            decimal actual = bank.GetTotalInterestPaid();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Get_Total_Interest_Paid_For_Maxi_Savings_Account() 
        {
            // Arange
            Bank bank = new Bank();
            var maxiCheckingAccount = new MaxiSavingsAccount();
            bank.AddCustomer(new Customer("Bill").OpenAccount(maxiCheckingAccount));
            maxiCheckingAccount.RequestDeposit(3000.0m);
            
            decimal expected = 17.0000m;

            // Act
            decimal actual = bank.GetTotalInterestPaid();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
