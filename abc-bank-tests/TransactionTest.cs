using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace abc_bank_tests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void Transaction()
        {
            Transaction t = new Transaction(5);
            //t instanceOf Transaction
            Assert.IsTrue(t.GetType() == typeof(Transaction));
        }

        [TestMethod]
        public void TestAccountTransfer()
        {
            Customer oscar = new Customer("Oscar").OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));

            List<Account> Accounts = oscar.GetAccounts();

            Account checkingAccount = Accounts.First(a => a.GetAccountType().Equals(Account.CHECKING));
            Account savingsAccount = Accounts.First(a => a.GetAccountType().Equals(Account.SAVINGS));

            checkingAccount.Deposit(600.0);
            savingsAccount.Deposit(600.0);

            oscar.TransferFunds(checkingAccount, savingsAccount, 200.0);

            Assert.AreEqual(400.0, checkingAccount.sumTransactions());
            Assert.AreEqual(800.0, savingsAccount.sumTransactions());
        }
    }
}
