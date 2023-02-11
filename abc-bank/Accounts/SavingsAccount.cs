namespace abc_bank.Accounts {
  public class SavingsAccount : AccountBase {
    public override double InterestEarned {
      get {
        return CurrentBalance <= 1000
          ? CalculateInterest(0.001)
          : CalculateInterest(0.002);
      }
    }

    public SavingsAccount(int lastAccountId, double initialDeposit = 0.00) 
      : base(AccountType.SAVINGS, lastAccountId, initialDeposit) { }

  }
}
