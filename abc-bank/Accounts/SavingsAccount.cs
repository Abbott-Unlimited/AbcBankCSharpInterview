namespace abc_bank.Accounts {
  public class SavingsAccount : AccountBase {

    public override string ReportLabel { get; } = "Savings Account";

    public override decimal InterestEarned {
      get {
        return CurrentBalance <= 1000
          ? CalculateInterest(0.001M)
          : CalculateInterest(0.002M);
      }
    }

    public SavingsAccount(int accountId, int customerId, decimal initialDeposit = 0)
      : base(AccountType.SAVINGS, accountId, customerId, initialDeposit) { }

  }
}
