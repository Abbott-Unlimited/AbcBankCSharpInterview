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

            Account checkingAccount = new Account(AccountType.CHECKING, 0, henry);
            Account savingsAccount = new Account(AccountType.SAVINGS, 0, henry);

            var henryCheckingID = AccountController.AddAccount(AccountType.CHECKING, henry, 0);
            var henrySavingID = AccountController.AddAccount(AccountType.SAVINGS, henry, 0);

            decimal depositValue1 = 100;
            decimal depositValue2 = 4000;
            decimal withdrawalValue = 200;
            
            TransactionController.Deposit(henryCheckingID, depositValue1);
            TransactionController.Deposit(henrySavingID, depositValue2);
            TransactionController.Withdraw(henrySavingID, withdrawalValue);

            var transaction1Amount = TransactionController.transactions[0].amount.ToString("C");
            var transaction2Amount = TransactionController.transactions[1].amount.ToString("C");
            var transaction3Amount = TransactionController.transactions[2].amount.ToString("C");
            var checkingAccountTotal = AccountController.GetAccountAmount(henryCheckingID).ToString("C");
            var savingAccountTotal = AccountController.GetAccountAmount(henrySavingID).ToString("C");

            var expected = $"Statement for {henry.FullName}\n\nChecking Account\n  deposit {transaction1Amount}\nTotal {checkingAccountTotal}\n\nSavings Account\n  deposit $4,000.00\n  withdrawal $200.00\nTotal $3,800.00\n\nTotal In All Accounts $3,900.00";
            var actual = StatementGenerator.GetCustomerStatementByID(henry.AccountID);

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
