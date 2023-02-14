namespace abc_bank.Accounts {
  public class CheckingAccount : AccountBase {
    public override string ReportLabel { get; } = "Checking Account";

    public override decimal InterestEarned {
      get => CalculateInterest(0.001M);
    }

    public CheckingAccount(int accountId, int customerId, decimal initialDeposit = 0)
      : base(AccountType.CHECKING, accountId, customerId, initialDeposit) { }

  }
}
