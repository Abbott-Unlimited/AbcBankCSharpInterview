using AbcCompanyEstablishmentApp.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AbcCompanyEstablishmentApp.Controllers
{
    public static class TransactionController
    {
        public static List<Transaction> transactions = new List<Transaction>();

        private static decimal ProcessTransaction(Transaction transaction)
        {
            transaction.AccountChanged.Transactions.Add(transaction);
            transaction.AccountChanged.AccountAmount += transaction.AccountAmount;
            return transaction.AccountChanged.AccountAmount;
        }

        public static decimal Deposit(Guid accountID, decimal amount)
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
                return ProcessTransaction(transaction);                
            }
        }

        public static void Withdraw(Guid accountID, decimal amount)
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

        public static void Transfer(Guid accountToTransferFrom, Guid accountToTransferTo, decimal amount)
        {
            if (amount <= 0)
                throw new AmountLessThanZeroException("Amount must be greater than zero");

            ValidateTransfer(accountToTransferFrom, accountToTransferTo, amount);
        }

        private static void ValidateTransfer(Guid accountToTransferFrom, Guid accountToTransferTo, decimal amount)
        {
            var fromAccount = AccountController.GetAccountByID(accountToTransferFrom);
            var toAccount = AccountController.GetAccountByID(accountToTransferTo);
            if (fromAccount.Owner == toAccount.Owner)
            {
                ProcessTransaction(new Transaction(amount, toAccount));
                ProcessTransaction(new Transaction(-amount, fromAccount));
            }
        }

        public static bool ValidateTransaction(Account account, decimal amount)
        {
            var amountToCheck = account.AccountAmount += amount;
            if (amountToCheck <= 0)
            {
                return false;
            }
            return true;
        }

        public static decimal TotalTransactionsForCustomer(List<Transaction> transactions, string workingString = "")
        {            
            decimal total = 0.0M;

            foreach (Transaction transaction in transactions)
            {
                total += transaction.AccountAmount;
            }

            return total;
        }
    }
}
