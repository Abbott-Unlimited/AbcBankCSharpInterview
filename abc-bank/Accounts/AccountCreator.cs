using System.Collections.Generic;

namespace abc_bank.Accounts {
  public static class AccountCreator {
    public static IAccount GetAccount(AccountType accountType, double initialDeposit = 0.0) {
      var creator = new Dictionary<AccountType, IAccount>(){
        { AccountType.CHECKING, new CheckingAccount(initialDeposit) },
        { AccountType.SAVINGS, new SavingsAccount(initialDeposit) },
        { AccountType.MAXI_SAVINGS, new MaxiSavingsAccount(initialDeposit) }
      };

      return creator[accountType];
    }
  }
}
