using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank.Transactions;

using System.Diagnostics;
using System;

namespace abc_bank_tests.Transactions {

  #region Transaction Instance for Testing

  public class TransactionFixture : Transaction {
    public TransactionFixture(double amount, TransactionType transactionType)
      : base(amount, transactionType) { }

    public void SetTransactionDate(DateTime datetime) {
      TransactionDate = datetime;
    }

    public void TransactionDateSubtractDays(uint days) => TransactionDate = TransactionDate.AddDays((int)-days);

  }

  #endregion

  [TestClass]
  public class TransactionTest {

    [TestMethod]
    public void Transaction_date_is_set_EXAMPLE() {
      var transaction = new TransactionFixture(100.00, TransactionType.Deposit);

      Debug.WriteLine($"TransactionDate: {transaction.TransactionDate}");

      transaction.TransactionDateSubtractDays(5);
      Debug.WriteLine($"TransactionDate2: {transaction.TransactionDate}");

      transaction.TransactionDateSubtractDays(10);
      Debug.WriteLine($"TransactionDate3: {transaction.TransactionDate}");

      transaction.TransactionDateSubtractDays(15);
      Debug.WriteLine($"TransactionDate4: {transaction.TransactionDate}");
    }
  }
}
