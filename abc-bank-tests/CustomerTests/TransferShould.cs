using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace abc_bank_tests.CustomerTests
{
    [TestClass]
    public class TransferShould
    {
        [TestMethod]
        public void DeductFromOneAccount_AddToAnother()
        {
            var acctFrom = new Account(AccountType.CHECKING);
            acctFrom.AddTransaction(100);
            var acctTo = new Account(AccountType.SAVINGS);
            var sut = new Customer("Giofre");
            sut.OpenAccount(acctFrom);
            sut.OpenAccount(acctTo);

            sut.Transfer(acctFrom, acctTo, 100);

            Assert.AreEqual(0, acctFrom.CalculateBalance());
            Assert.AreEqual(100, acctTo.CalculateBalance(), acctTo.CalculateBalance());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowAnException_IfFromAccountHasLessThanTransferAmount()
        {
            var acctFrom = new Account(AccountType.CHECKING);
            var acctTo = new Account(AccountType.SAVINGS);
            var sut = new Customer("Giofre");
            sut.OpenAccount(acctFrom);
            sut.OpenAccount(acctTo);

            sut.Transfer(acctFrom, acctTo, 100);
        }

        [TestMethod]
        public void IfAcctsAreSame_NoChange()
        {
            var startingBal = 200;
            var acctFrom = new Account(AccountType.CHECKING);
            acctFrom.AddTransaction(startingBal);
            var sut = new Customer("Giofre");
            sut.OpenAccount(acctFrom);

            sut.Transfer(acctFrom, acctFrom, 100);

            Assert.AreEqual(startingBal, acctFrom.CalculateBalance());
        }
    }
}
