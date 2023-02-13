namespace abc_bank.Accounts {
  public class CheckingAccount : AccountBase {
    public override string ReportLabel { get; } = "Checking Account";

    public override double InterestEarned {
      get => CalculateInterest(0.001);
    }

    public CheckingAccount(int accountId, int customerId, double initialDeposit = 0.00)
      : base(AccountType.CHECKING, accountId, customerId, initialDeposit) { }

  }
}
