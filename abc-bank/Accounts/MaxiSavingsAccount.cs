namespace abc_bank.Accounts {
  public class MaxiSavingsAccount : AccountBase {
    public override string ReportLabel { get; } = "Maxi Savings Account";

    public override double InterestEarned {
      get {
        return CurrentBalance <= 1000
          ? CalculateInterest(0.02)
          : CalculateInterest(0.05);
      }
    }
    public MaxiSavingsAccount(int accountId, int customerId, double initialDeposit = 0.00)
      : base(AccountType.MAXI_SAVINGS, accountId, customerId, initialDeposit) { }

  }
}
