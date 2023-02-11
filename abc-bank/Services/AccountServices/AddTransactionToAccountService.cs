using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Services.AccountServices
{
    public class AddTransactionToAccountService
    {
        public void Add(Account account, TransactionType transactionType, decimal amount, DateTime? transactionDate = null)
        {
            if (transactionType == TransactionType.DEPOSIT && amount < 0)
                throw new Exception("You cannot deposit a non-positive amount.");

            if (transactionType == TransactionType.WITHDRAWAL && amount >= 0)
                throw new Exception("You cannot withdraw a non-negative amount.");

            account.AddTransaction(new Transaction(amount, transactionDate));            
        }
    }
}
