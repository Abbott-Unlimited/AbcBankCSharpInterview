namespace abc_bank.Transactions {
  public interface IDeposit : ITransaction {
    int AccountId { get; }
    bool IsFromTransfer { get; }
    ITransfer TransferDetails { get; }
  }
}
