using abc_bank.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Controllers
{
    internal class TransactionController
    {
        public static List<Transaction> transactions = new List<Transaction>();

        private static void ProcessTransaction(Transaction transaction)
        {
            transaction.accountChanged.Transactions.Add(transaction);
            transaction.accountChanged.AccountAmount += transaction.amount;
        }

        public static void Deposit(Guid accountID, double amount)
        {
            var account = AccountController.GetAccountByID(accountID);

            if (amount <= 0)
            {
                throw new AmountLessThanZeroException("Amount must be greater than zero");
            }
            else
            {
                var transaction = new Transaction(amount, account);
                transactions.Add(transaction);
                ProcessTransaction(transaction);
            }
        }

        public static void Withdraw(Guid accountID, double amount)
        {
            var account = AccountController.GetAccountByID(accountID);

            if (amount <= 0)
            {
                throw new AmountLessThanZeroException("Amount must be greater than zero");
            }
            else
            {
                var transaction = new Transaction(-amount, account);
                transactions.Add(transaction);
                ProcessTransaction(transaction);                
            }
        }

        public static void Transfer(Guid accountToTransferFrom, Guid accountToTransferTo, double amount)
        {
            if (amount <= 0)
                throw new AmountLessThanZeroException("Amount must be greater than zero");

            ValidateTransfer(accountToTransferFrom, accountToTransferTo, amount);
        }

        private static void ValidateTransfer(Guid accountToTransferFrom, Guid accountToTransferTo, double amount)
        {
            var fromAccount = AccountController.GetAccountByID(accountToTransferFrom);
            var toAccount = AccountController.GetAccountByID(accountToTransferTo);
            if (fromAccount.OwnerName == toAccount.OwnerName)
            {
                ProcessTransaction(new Transaction(amount, toAccount));
                ProcessTransaction(new Transaction(-amount, fromAccount));
            }
        }

        public static bool ValidateTransaction(Account account, double amount)
        {
            if ((account.AccountAmount += amount) <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
