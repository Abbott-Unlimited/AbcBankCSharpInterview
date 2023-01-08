using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void Transfer_Deposit_First_Account_Transaction_Amounts_Are_Equal_True()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);

            checkingAccount.Transfer(Enums.TransferType.Deposit, 900, savingsAccount);
            
            Assert.AreEqual(900, checkingAccount.transactions[1].amount);
        }

        [TestMethod]
        public void Transfer_Deposit_Second_Account_Transaction_Amounts_Are_Equal_True()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            
            checkingAccount.Transfer(Enums.TransferType.Deposit, 900, savingsAccount);
            
            Assert.AreEqual(-900, savingsAccount.transactions[1].amount);
        }

        [TestMethod]
        public void Transfer_Withdrawl_First_Account_Transaction_Amounts_Are_Equal_True()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            
            checkingAccount.Transfer(Enums.TransferType.Withdrawl, 100, savingsAccount);

            Assert.AreEqual(-100, checkingAccount.transactions[1].amount);
        }

        [TestMethod]
        public void Transfer_Withdrawl_Second_Account_Transaction_Amounts_Are_Equal_True()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            
            checkingAccount.Transfer(Enums.TransferType.Withdrawl, 100, savingsAccount);
            
            Assert.AreEqual(100, savingsAccount.transactions[1].amount);
        }

        [TestMethod]
        public void Transfer_Deposit_First_Account_Same_Account_Are_Equal_True()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            
            Customer henry = new Customer("Henry").OpenAccount(checkingAccount);

            checkingAccount.Deposit(100.0);
            
            checkingAccount.Transfer(Enums.TransferType.Withdrawl, 100, checkingAccount);
            
            Assert.AreEqual(-100, checkingAccount.transactions[1].amount);
        }

        [TestMethod]
        public void Transfer_Deposit_Second_Account_Same_Account_Are_Equal_True()
        {
            Account checkingAccount = new Account(Account.CHECKING);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount);

            checkingAccount.Deposit(100.0);

            checkingAccount.Transfer(Enums.TransferType.Withdrawl, 100, checkingAccount);

            Assert.AreEqual(100, checkingAccount.transactions[2].amount);
        }
    }
}
