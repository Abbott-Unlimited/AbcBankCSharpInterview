using System.Collections.Generic;

namespace abc_bank.Accounts {
  public static class AccountFactory {
    public static IAccount GetAccount(AccountType accountType) {
      var creator = new Dictionary<AccountType, IAccount>(){
        { AccountType.CHECKING, new CheckingAccount() },
        { AccountType.SAVINGS, new SavingsAccount() },
        { AccountType.MAXI_SAVINGS, new MaxiSavingsAccount() }
      };

      return creator[accountType];
    }
  }
}
