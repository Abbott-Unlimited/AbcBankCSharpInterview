namespace abc_bank.Transactions {
  public interface IDeposit : ITransaction {
    int AccountId { get; }
  }
}
