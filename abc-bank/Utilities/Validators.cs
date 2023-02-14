using System.Collections.Generic;

using abc_bank.Accounts;
using abc_bank.Exceptions;

namespace abc_bank.Utilities {

  // TODO:  These Validators Methods probably should be extension methods
  //        They're fine here - Just not really a fan.  Not worth changing tho.
  public static class Validators {

    // In hindsight, this at least is probably unnecessary codebase bloat...
    public static void AccountIsInCollection(IAccount account, List<IAccount> accountsCollection) {
      if (accountsCollection.Count == 0 || !accountsCollection.Contains(account)) {
        throw new InvalidAccountException();
      }
    }

    public static void FundsAvailableForWithdraw(double currentBalance, double requestedAmount) {
      // validate the requested amount first.
      // Invalied requestedAmount can cause unexpected results
      TransactionAmount(requestedAmount);

      if (currentBalance < requestedAmount) {
        throw new InsufficientFundsException();
      }
    }

    public static void TransactionAmount(double amount) {
      if (amount <= 0) {
        throw new InvalidTransactionAmountException();
      }
    }

  }
}
