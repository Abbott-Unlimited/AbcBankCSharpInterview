using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank;
using abc_bank.Exceptions;

namespace abc_bank_tests {

  [TestClass]
  public class UtilitiesTests {

    #region ValidateFundsAvailable

    [TestMethod]
    [ExpectedException(typeof(InsufficientFundsException))]
    public void ValidateFundsAvailable_throws_error_if_amount_arg_is_negative() {
      Utilities.ValidateFundsAvailable(0, 1000);

      Assert.Fail("Expected exception not thrown: abc_bank.Exceptions.InsufficientFundsException");
    }

    #endregion

    #region ValidateTransactionAmount

    [TestMethod]
    [ExpectedException(typeof(InvalidTransactionAmountException))]
    public void ValidateTransactionAmount_throws_error_if_amount_arg_is_negative() {
      Utilities.ValidateTransactionAmount(-0.01);

      Assert.Fail("Expected exception not thrown: abc_bank.Exceptions.InvalidTransactionAmountException");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidTransactionAmountException))]
    public void ValidateTransactionAmount_throws_error_if_amount_arg_is_0() {
      Utilities.ValidateTransactionAmount(0.0);

      Assert.Fail("Expected exception not thrown: abc_bank.Exceptions.InvalidTransactionAmountException");
    }

    #endregion

  }
}
