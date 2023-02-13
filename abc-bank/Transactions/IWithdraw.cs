namespace abc_bank.Transactions {
  public interface IWithdraw : ITransaction {
    int AccountId { get; }
    bool IsFromTransfer { get; }
    ITransfer TransferDetails { get; }
  }
}
