using System.Collections.Generic;
using abc_bank.Enums;

namespace abc_bank.Entities
{
    public class Account
    {
        public Account(AccountTypeEnum accountType)
        {
            this.AccountType = accountType;
            this.Transactions = new List<Transaction>();
        }

        public AccountTypeEnum AccountType { get; }
      
        public List<Transaction> Transactions { get; set; }
    }
}
