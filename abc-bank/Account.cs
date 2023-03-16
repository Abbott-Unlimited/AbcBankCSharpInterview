using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Account
    {

        public const int CHECKING = 0;
        public const int SAVINGS = 1;
        public const int MAXI_SAVINGS = 2;
        private Customer customer;
        private readonly int accountType;
        public List<Transaction> transactions;
        private DateTime lastWithdrawalDate;

        public Account(int accountType) 
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
            this.lastWithdrawalDate = DateTime.MinValue; // Set the last withdrawal date to the minimum possible value
        }

        public void Deposit(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(-amount));
                lastWithdrawalDate = DateTime.Now; // Update the last withdrawal date to the current time
            }
        }

        public void Transfer(Account destinationAccount, double amount)
        {
            if (destinationAccount == null)
            {
                throw new ArgumentNullException(nameof(destinationAccount));
            }

            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero.", nameof(amount));
            }

            // Check that the destination account belongs to the same customer as this account
            if (!IsSameCustomer(destinationAccount))
            {
                throw new ArgumentException("Destination account must belong to the same customer as this account.");
            }

            Withdraw(amount);
            destinationAccount.Deposit(amount);
        }

        private bool IsSameCustomer(Account account)
        {
            return account != null && account.GetCustomer() == this.GetCustomer();
        }


        public double InterestEarned() 
        {

            double maxi_rate = 0.0;
            double amount = sumTransactions();
            double interestEarned = 0.0;
            switch (accountType)
            {
                case SAVINGS:
                    if (amount <= 1000)
                        interestEarned = amount * 0.001;
                    else
                        interestEarned = 1 + (amount - 1000) * 0.002;
                    break;
                //            case SUPER_SAVINGS:
                //                if (amount <= 4000)
                //                    return 20;
                case MAXI_SAVINGS:
                    if (lastWithdrawalDate.AddDays(10) < DateTime.Now)
                        maxi_rate = 0.05;
                    else
                        maxi_rate = 0.001;
                    double q = (Math.Pow(1 + maxi_rate / 365, 365) - 1);
                    interestEarned += amount * (Math.Pow(1 + maxi_rate/365, 365) - 1);
                    break;
                default: // CHECKING
                    interestEarned = amount * 0.001;
                    break;

            }
             return interestEarned;
            

        }


        public Customer GetCustomer()
        {
            return customer;
        }


        public void SetCustomer(Customer customer)
        {
            this.customer = customer;
        }

        public double sumTransactions() {
           return CheckIfTransactionsExist(true);
        }

        private double CheckIfTransactionsExist(bool checkAll) 
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        public int GetAccountType() 
        {
            return accountType;
        }


        public void UpdateLastWithdrawalDate(DateTime date)
        {
            this.lastWithdrawalDate = date;
        }
    }
}
