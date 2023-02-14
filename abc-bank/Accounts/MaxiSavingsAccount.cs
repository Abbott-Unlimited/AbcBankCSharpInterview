namespace abc_bank.Accounts {
  public class MaxiSavingsAccount : AccountBase {
    public override string ReportLabel { get; } = "Maxi Savings Account";

    public override decimal InterestEarned {
      get {
        return CurrentBalance <= 1000
          ? CalculateInterest(0.02M)
          : CalculateInterest(0.05M);
      }
    }
    public MaxiSavingsAccount(int accountId, int customerId, decimal initialDeposit = 0)
      : base(AccountType.MAXI_SAVINGS, accountId, customerId, initialDeposit) { }

  }
}
