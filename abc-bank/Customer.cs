using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Customer : ICustomer
    {
        private readonly string Name;
        private readonly IList<IAccount> Accounts;

        public Customer(string name)
        {
            Name = name;
            Accounts = new List<IAccount>();
        }

        public string GetName()
        {
            return Name;
        }

        public IList<IAccount> GetAccounts()
        {
            return Accounts;
        }

        public void OpenAccount(IAccount account)
        {
            Accounts.Add(account);
        }

        public int GetNumberOfAccounts()
        {
            return Accounts.Count;
        }

        public double GetTotalInterestEarned() 
        {
            double total = 0;
            foreach (IAccount a in Accounts)
                total += a.GetInterestEarned();
            return total;
        }

        public string GetStatement() 
        {
            string statement = "Statement for " + Name + "\n";
            double total = 0.0;
            foreach (IAccount a in Accounts) 
            {
                statement += "\n" + a.ToString() + "\n";
                total += a.GetAccountBalance();
            }
            statement += "\nTotal In All Accounts " + total.ToString("c");
            return statement;
        }

        public override string ToString()
        {
            return string.Format("\n - {0} ({1} account{2})",
                GetName(),
                GetNumberOfAccounts(), 
                (GetNumberOfAccounts() > 1 ? "s" : ""));    
        }
    }
}
