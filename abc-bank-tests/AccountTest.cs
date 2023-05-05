using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        //[ExpectedException(typeof(ArgumentException), "amount must be greater than zero")]
        public void DepositAndWithDrawalSuccess()
        {
            Account testAccount = new Account(AccountType.CHECKING);

            testAccount.Deposit(10000);
            testAccount.Withdraw(5000);
        }

        [TestMethod]
        public void DepositAndWithDrawalFailure()
        {
            Account testAccount = new Account(AccountType.CHECKING);

            var result1 = testAccount.Deposit(3000);
            var result2 = testAccount.Withdraw(5000);

            Assert.AreEqual(result2.Success, false);
            Assert.IsTrue(result2.GetType() == typeof(FailureResult));
            if (result2.GetType() == typeof(FailureResult))
            {
                Assert.IsTrue(((FailureResult)result2).Message == "withdrawal greater then balance");
            }

        }

        [TestMethod]
        public void DepositNegativeFailure()
        {
            Account testAccount = new Account(AccountType.CHECKING);

            var result = testAccount.Deposit(-3000);          

            Assert.AreEqual(result.Success, false);
            Assert.IsTrue(result.GetType() == typeof(FailureResult));
            if(result.GetType() == typeof(FailureResult))
            {
                Assert.IsTrue(((FailureResult)result).Message == "amount must be greater than zero");
            }
        }

        [TestMethod]
        public void WithdrawalNegativeFailure()
        {
            Account testAccount = new Account(AccountType.CHECKING);

            var result = testAccount.Withdraw(-3000);

            Assert.AreEqual(result.Success, false);
            Assert.IsTrue(result.GetType() == typeof(FailureResult));
            if (result.GetType() == typeof(FailureResult))
            {
                Assert.IsTrue(((FailureResult)result).Message == "amount must be greater than zero");
            }
        }


    }
}
