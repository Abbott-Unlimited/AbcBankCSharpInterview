using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Services.CustomerServices
{
    public class AddCustomerTransactionService
    {
        public void Add(Customer customer, string accountName, decimal amount, TransactionType transactionType)
        {
            var account = customer.GetAccounts().FirstOrDefault(x => x.GetAccountName() == accountName);

            if (account == null)
                throw new Exception($"Account with name {accountName} not found.");

            if (transactionType == TransactionType.DEPOSIT && amount <= 0)
                throw new Exception("You cannot create a deposit transaction with a non-positive amount.");

            if (transactionType == TransactionType.WITHDRAWAL && amount >= 0)
                throw new Exception("You cannot create a withdrawal transaction with a non-negative amount.");

            account.AddTransaction(new Transaction(amount));
        }
    }
}
