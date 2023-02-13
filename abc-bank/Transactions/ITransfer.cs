namespace abc_bank.Transactions {
  public interface ITransfer : ITransaction {

    int OriginAccountId { get; }

    int DestinationAccountId { get; }

  }
}
