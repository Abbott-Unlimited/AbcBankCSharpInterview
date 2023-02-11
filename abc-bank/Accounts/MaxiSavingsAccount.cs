namespace abc_bank.Accounts {
  public class MaxiSavingsAccount : AccountBase {

    public override double InterestEarned {
      get {
        return CurrentBalance <= 1000
          ? CalculateInterest(0.02)
          : CalculateInterest(0.05);
      }
    }
    public MaxiSavingsAccount(int lastAccountId, double initialDeposit = 0.00) 
      : base(AccountType.MAXI_SAVINGS, lastAccountId, initialDeposit) { }

  }
}
