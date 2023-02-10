namespace abc_bank.Accounts {
  public class CheckingAccount : AccountBase {

    #region Properties

    public override double InterestEarned => CalculateInterest(0.001);

    #endregion

    #region CTOR

    public CheckingAccount() : base(AccountType.CHECKING) { }

    #endregion
  }
}
