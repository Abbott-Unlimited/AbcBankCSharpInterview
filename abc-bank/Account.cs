using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Account
    {
        private readonly AccountType accountType;
        private readonly string accountName;
        public List<Transaction> transactions = new List<Transaction>();

        public Account(AccountType accountType, string accountName) 
        {
            this.accountType = accountType;
            this.accountName = accountName;
        }

        public void AddTransaction(Transaction transaction)
        {
            this.transactions.Add(transaction);
        }

        public AccountType GetAccountType() 
        {
            return this.accountType;
        }

        public string GetAccountName()
        {
            return this.accountName;
        }
    }
}
