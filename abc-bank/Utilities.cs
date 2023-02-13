using abc_bank.Exceptions;

namespace abc_bank {
  public static class Utilities {

    #region Validation Methods

    public static void ValidateFundsAvailable(double currentBalance, double requestedAmount) {
      ValidateTransactionAmount(requestedAmount);

      if (currentBalance < requestedAmount) {
        throw new InsufficientFundsException();
      }
    }

    public static void ValidateTransactionAmount(double amount) {
      if (amount <= 0) {
        throw new InvalidTransactionAmountException();
      }
    }

    #endregion

  }
}
