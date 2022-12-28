using abc_bank_tests.AbcTestFunctions;
using AbcCompanyEstablishmentApp;
using AbcCompanyEstablishmentApp.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static AbcCompanyEstablishmentApp.Utilities.AbcCustomValues;

namespace AbcCompanyEstablishmentAppTests
{
    [TestClass]
    public class CustomerTest
    {
        //we'll run this test on app startup just to confirm everything for the day/restart/crash/whatever
        [TestMethod]
        public void AppStartupTest()
        {
            Customer henry = Random.GetRandomCustomer();
            CustomerController.Customers.Add(henry);

            var henryCheckingID = AccountController.AddAccount(AccountType.CHECKING, henry, 0);
            var henrySavingID = AccountController.AddAccount(AccountType.SAVINGS, henry, 0);

            decimal depositValue1 = 100;
            decimal depositValue2 = 4000;
            decimal withdrawalValue = 200;
            
            TransactionController.Deposit(henryCheckingID, depositValue1);
            TransactionController.Deposit(henrySavingID, depositValue2);
            TransactionController.Withdraw(henrySavingID, withdrawalValue);

            var transaction1Amount = TransactionController.transactions[0].AccountAmount.ToString("C");
            var transaction2Amount = TransactionController.transactions[1].AccountAmount.ToString("C");
            var transaction3Amount = TransactionController.transactions[2].AccountAmount.ToString("C");
            var checkingAccountTotal = AccountController.GetAccountAmount(henryCheckingID).ToString("C");
            var savingAccountTotal = AccountController.GetAccountAmount(henrySavingID).ToString("C");
            var totalInAccounts = AccountController.GetTotalForAllAccounts(henry).ToString("C");

            var expected = $"Statement for {henry.FullName}\n\nChecking Account\n  DEPOSIT {transaction1Amount}\nTotal {checkingAccountTotal}\n\nSavings Account\n  DEPOSIT {transaction2Amount}\n  WITHDRAWAL {transaction3Amount}\nTotal {savingAccountTotal}\n\nTotal In All Accounts {totalInAccounts}";

            var queryCustomer = CustomerController.GetCustomerByCustomerID(henry.CustomerID);
            var actual = StatementGenerator.GetCustomerStatementByID(queryCustomer);

            Assert.AreEqual(expected, actual);
        }

        //[TestMethod]
        //public void Check_Savings_Account_Creation_Only_Creates_One_Account()
        //{
        //    Customer oscar = new Customer("Oscar").OpenAccount(new Account(Account.SAVINGS));
        //    Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        //}

            //[TestMethod]
            //public void Check_Savings_And_Checking_Creation_Only_Opens_Two_Accounts()
            //{
            //    Customer oscar = new Customer("Oscar")
            //         .OpenAccount(new Account(Account.SAVINGS));
            //    oscar.OpenAccount(new Account(Account.CHECKING));
            //    Assert.AreEqual(2, oscar.GetNumberOfAccounts());
            //}

            //Placeholder method is for future use and ignored at runtime currently
            //[TestMethod]
            //[Ignore]
            //public void FutureUseThreeAccountTestMethod()
            //{
            //    Customer oscar = new Customer("Oscar")
            //            .OpenAccount(new Account(Account.SAVINGS));
            //    oscar.OpenAccount(new Account(Account.CHECKING));
            //    Assert.AreEqual(3, oscar.GetNumberOfAccounts());
            //}
    }
}
