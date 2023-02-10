namespace abc_bank.Accounts {
  public class CheckingAccount : AccountBase {
    public override double InterestEarned {
      get {
        return CalculateInterest(0.001);
      }
    }

    public CheckingAccount(double initialDeposit = 0.00) : base(AccountType.CHECKING, initialDeposit) { }

  }
}
