using System;
using System.Linq;
using abc_bank.Entities;
using abc_bank.Helpers;


namespace abc_bank.Services
{
    public class BankServices
    {
        private Bank _bank = new Bank();

        public void AddCustomer(Customer customer)
        {
            _bank.Customers.Add(customer);
        }

        public void OpenAccount(Customer customer, Account account)
        {
            var c = _bank.Customers.Where(cust => cust.Name == customer.Name).FirstOrDefault();
            if (c == null)
            {
                AddCustomer(customer);
                customer.Accounts.Add(account);
            }
            else
            {
                c.Accounts.Add(account);
            }
        }

        public int GetNumberOfAccounts(Customer customer)
        {
            var c = _bank.Customers.Where(cust => cust.Name == customer.Name).FirstOrDefault();
            if (c == null)
            {
                return customer.Accounts.Count;
            }
            else
            {
                return c.Accounts.Count;
            }
        }

        public String CustomerSummary()
        {
            String summary = "Customer Summary";
            foreach (Customer c in _bank.Customers)
                summary += "\n - " + c.Name + " (" + Language.FormatPlural(c.Accounts.Count, "account") + ")";
            return summary;
        }

        public double TotalInterestPaid()
        {
            double total = 0;
            foreach (Customer c in _bank.Customers)
                total += InterestCalculations.TotalInterestEarned(c);
            return total;
        }

        public String GetFirstCustomer()
        {
                return _bank.Customers.FirstOrDefault() == null? string.Empty : _bank.Customers.FirstOrDefault().Name;
        }

        public void Deposit(Account account, double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                account.Transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(Account account, double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                account.Transactions.Add(new Transaction(-amount));
            }
        }
    }
}
