using System;

namespace abc_bank
{
    public class Transaction
    {
        public readonly double              Amount;
        public readonly DateTime            TransactionDate;
        public readonly TransactionTypeEnum TransactionType;

        public Transaction(double amount, DateTime transactionDate, TransactionTypeEnum transactionType)
        {
            Amount          = amount;
            TransactionDate = transactionDate;
            TransactionType = transactionType;
        }

        public enum TransactionTypeEnum
        {
            Deposit,
            Withdraw,
            Transfer
        }
    }
}