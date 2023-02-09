namespace abc_bank {

  // TODO:  This entire class is questionable at the moment - but likely needed for a requested feature.
  public class Transaction {
    public readonly double amount;

    //private readonly DateTime transactionDate;

    public Transaction(double amount) {
      this.amount = amount;
      //this.transactionDate = DateProvider.getInstance().Now();
    }
  }
}
