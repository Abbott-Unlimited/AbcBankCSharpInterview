namespace abc_bank.Transactions {
  public interface IWithdraw : ITransaction {
    int AccountId { get; }
  }
}
