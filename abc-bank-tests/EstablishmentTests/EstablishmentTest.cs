using abc_bank_tests.AbcTestFunctions;
using AbcCompanyEstablishmentApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static AbcCompanyEstablishmentApp.Utilities.AbcCustomValues;

namespace AbcCompanyEstablishmentAppTests
{
    [TestClass]
    public class EstablishmentTest
    {
        private static readonly double INTEREST_DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void Check_That_Customer_Summary_Is_Correct()
        {
            //var establishmentType = EstablishmentType.BANK;


            //Customer customer = new Customer(AccountType.SAVINGS, 0, "John", "Smith");
            //customer.OpenAccount(new Account(Account.CHECKING));
            //establishment.AddCustomer(customer);

            //Assert.AreEqual("Customer Summary\n - John (1 account)", establishment.CustomerSummary());
        }

        //[TestMethod]
        //public void Check_That_CheckingAccount_Interest_Paid_Is_Within_Acceptable_Delta()
        //{
        //    AbcCompanyEstablishmentApp.Establishment bank = new AbcCompanyEstablishmentApp.Establishment();
        //    Account checkingAccount = new Account(Account.CHECKING);
        //    Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
        //    bank.AddCustomer(bill);

        //    checkingAccount.Deposit(100.0);

        //    Assert.AreEqual(0.1, bank.TotalInterestPaid(), INTEREST_DOUBLE_DELTA);
        //}

        //[TestMethod]
        //public void Check_That_SavingsAccount_Interest_Paid_Is_Within_Acceptable_Delta()
        //{
        //    AbcCompanyEstablishmentApp.Establishment bank = new AbcCompanyEstablishmentApp.Establishment();
        //    Account checkingAccount = new Account(Account.SAVINGS);
        //    bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

        //    checkingAccount.Deposit(1500.0);

        //    Assert.AreEqual(2.0, bank.TotalInterestPaid(), INTEREST_DOUBLE_DELTA);
        //}

        //[TestMethod]
        //public void Check_That_MAXI_SavingsAccount_Interest_Paid_Is_Within_Acceptable_Delta()
        //{
        //    AbcCompanyEstablishmentApp.Establishment bank = new AbcCompanyEstablishmentApp.Establishment();
        //    Account checkingAccount = new Account(Account.MAXI_SAVINGS);
        //    bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

        //    checkingAccount.Deposit(3000.0);

        //    Assert.AreEqual(170.0, bank.TotalInterestPaid(), INTEREST_DOUBLE_DELTA);
        //}
    }
}
