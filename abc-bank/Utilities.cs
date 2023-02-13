using System.Collections.Generic;

using abc_bank.Accounts;
using abc_bank.Exceptions;

namespace abc_bank {

  public static class Utilities {

    #region Validation Methods
    // TODO:  These Validation Methods probably should be extension methods
    //        They're fine here - Just not really a fan.  Not worth moving tho.

    public static void ValidateAccountInCollection(IAccount account, List<IAccount> accountsCollection) {
      if (accountsCollection.Count == 0 || !accountsCollection.Contains(account)) {
        throw new InvalidAccountException();
      }
    }

    public static void ValidateFundsAvailable(double currentBalance, double requestedAmount) {
      // sanity check - make sure the requested amount is a valid transaction amount.
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
