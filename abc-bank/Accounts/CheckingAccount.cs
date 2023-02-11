namespace abc_bank.Accounts {
  public class CheckingAccount : AccountBase {
    public override double InterestEarned {
      get {
        return CalculateInterest(0.001);
      }
    }

    public CheckingAccount(int lastAccountId, double initialDeposit = 0.00) 
      : base(AccountType.CHECKING, lastAccountId, initialDeposit) { }

  }
}
