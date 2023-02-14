using System.Collections.Generic;

using abc_bank.Accounts;
using abc_bank.Exceptions;

namespace abc_bank.Utilities {

  public static class Validators {

    public static void AccountIsInCollection(IAccount account, List<IAccount> accountsCollection) {
      if (accountsCollection.Count == 0 || !accountsCollection.Contains(account)) {
        throw new InvalidAccountException();
      }
    }

    public static void FundsAvailableForWithdraw(decimal currentBalance, decimal requestedAmount) {
      // validate the requested amount first.
      // Invalid requestedAmount can cause unexpected results
      TransactionAmount(requestedAmount);

      if (currentBalance < requestedAmount) {
        throw new InsufficientFundsException();
      }
    }

    public static void TransactionAmount(decimal amount) {
      if (amount <= 0) {
        throw new InvalidTransactionAmountException();
      }
    }

  }
}
