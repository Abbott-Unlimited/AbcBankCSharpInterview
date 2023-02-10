namespace abc_bank.Accounts {
  public class SavingsAccount : AccountBase {
    public override double InterestEarned {
      get {
        return CurrentBalance <= 1000
          ? CalculateInterest(0.001)
          : CalculateInterest(0.002);
      }
    }

    public SavingsAccount() : base(AccountType.SAVINGS) { }

  }
}
