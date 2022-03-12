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

        private readonly int accountType;

        private readonly List<Transaction> transactions;
        private double balance;

        public Account(int accountType) 
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
            this.balance = 0.0;
        }

        public void Deposit(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                AddTransaction(new Transaction(amount));
            }
        }

        public void Withdraw(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                AddTransaction(new Transaction(-amount));               
            }
        }

        public double InterestEarned() 
        {            
            switch(accountType){
                case SAVINGS:
                    if (balance <= 1000)
                        return balance * 0.001;
                    else
                        return 1 + (balance - 1000) * 0.002;
                case MAXI_SAVINGS:
                    var withdrawls = transactions.Where(t => t.amount < 0).ToList();
                    if (withdrawls.Exists(t => t.GetDate() >= DateProvider.GetInstance().Now().AddDays(-10)))
                        return balance * 0.001;
                    else
                        return balance * 0.05;
                default:
                    return balance * 0.001;
            }
        }

        public void AddTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
            balance += transaction.amount;
        }

        public double GetBalance() 
        {
            return balance;
        }

        public List<Transaction> GetTransactions()
        {
            return transactions;
        }

        public int GetAccountType() 
        {
            return accountType;
        }

    }
}
