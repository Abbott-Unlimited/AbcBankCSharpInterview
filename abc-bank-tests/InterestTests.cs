using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abc_bank.InterestStrategies;
using abc_bank;

namespace abc_bank_tests
{

    [TestClass]
    public class InterestTests
    {
        private static readonly double DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void ShouldReturnInterestPaid()
        {
            var context = new InterestContext();
            context.SetInterestStrategy(new MaxiInterest());
            List<Transaction> transactions = new List<Transaction>()
            {
                new Transaction(1000.00,TransactionType.DEPOSIT, new DateTime(2023,1,1)),
                new Transaction(1000.00,TransactionType.WITHDRAWEL, new DateTime(2023,1,11)),
                new Transaction(1000.00, TransactionType.DEPOSIT, new DateTime(2023,1,12))

            };
            //1000 @ 0.0137% a day 
            //1000 withdrawel >= 10 days @ 0.0003%
            //1000 deposit is final with no days
            var result = context.Calculate(transactions);

            Assert.AreEqual(0.47, result, DOUBLE_DELTA);
        }
    }
}
