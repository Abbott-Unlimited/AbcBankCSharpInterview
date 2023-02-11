using System.Collections.Generic;

namespace abc_bank.Accounts {
  public static class AccountCreator {
    public static IAccount GetAccount(AccountType accountType, int lastAccountId, double initialDeposit = 0.0) {
      var creator = new Dictionary<AccountType, IAccount>(){
        { AccountType.CHECKING, new CheckingAccount(lastAccountId, initialDeposit) },
        { AccountType.SAVINGS, new SavingsAccount(lastAccountId, initialDeposit) },
        { AccountType.MAXI_SAVINGS, new MaxiSavingsAccount(lastAccountId, initialDeposit) }
      };

      return creator[accountType];
    }
  }
}
