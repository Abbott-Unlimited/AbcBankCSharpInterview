using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank.Entities;
using abc_bank.Helpers;
using abc_bank.Enums;

namespace abc_bank_tests
{
    [TestClass]
    public class StatmentCalculationsTests
    {
        [TestMethod]
        public void GetStatementTest()
        {
            
            Account checkingAccount = new Account(AccountTypeEnum.CHECKING);
            Account savingsAccount = new Account(AccountTypeEnum.SAVINGS);

            Customer henry = new Customer("Henry");
            henry.Accounts.Add(checkingAccount);
            henry.Accounts.Add(savingsAccount);

            checkingAccount.Transactions.Add(new Transaction(100.0));
            savingsAccount.Transactions.Add(new Transaction(4000.0));
            savingsAccount.Transactions.Add(new Transaction(-200.0));

            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "Total $100.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $4,000.00\n" +
                    "  withdrawal $200.00\n" +
                    "Total $3,800.00\n" +
                    "\n" +
                    "Total In All Accounts $3,900.00", StatementCalculations.GetStatement(henry));
        }
    }
}
