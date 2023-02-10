using System.Collections.Generic;

namespace abc_bank.Accounts {
  public class MaxiSavingsAccount : AccountBase {
    #region Properties

    public override double InterestEarned {
      get {
        return CurrentBalance <= 1000
          ? CalculateInterest(0.001)
          : CalculateInterest(0.002);
      }
    }

    #endregion

    #region CTOR

    public MaxiSavingsAccount() : base(AccountType.MAXI_SAVINGS) { }

    #endregion
  }
}
