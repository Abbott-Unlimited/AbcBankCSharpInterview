using abc_bank.Services.DateServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Customer
    {
        private String name;
        private DateTime dateCreated;
        private List<Account> accounts = new List<Account>();

        public Customer(String name, DateTime? dateCreated = null)
        {
            this.name = name;
            this.dateCreated = dateCreated == null ? DateProvider.getInstance().Now() : dateCreated.Value;
        }

        public String GetName()
        {
            return this.name;
        }

        public DateTime GetDateCreated()
        {
            return this.dateCreated;
        }

        public Customer OpenAccount(Account account)
        {
            this.accounts.Add(account);
            return this;
        }

        public int GetNumberOfAccounts()
        {
            return this.accounts.Count;
        }

        public List<Account> GetAccounts()
        {
            return this.accounts;
        }
    }
}
