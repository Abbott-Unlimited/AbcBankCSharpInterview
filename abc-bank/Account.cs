using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Account
    {
        public  enum AccountType
        {
            CHECKING,
            SAVINGS,
            MAXI_SAVINGS
        };

        private readonly AccountType accountType;
        public List<Transaction> transactions;

        public Account(AccountType accountType) 
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
        }

        public Account Deposit(decimal amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount));
            }
            return this;
        }

        public Account Withdraw(decimal amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(-amount));
            }
            return this;
        }

        public decimal InterestEarned() 
        {
            decimal amount = sumTransactions();
            switch(accountType){
                case AccountType.CHECKING:
                    return amount * 0.001M;
                case AccountType.SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001M;
                    else
                        return 1 + (amount-1000) * 0.002M;
                case AccountType.MAXI_SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.02M;
                    if (amount <= 2000)
                        return 20 + (amount-1000) * 0.05M;
                    return 70 + (amount-2000) * 0.1M;
                default:
                    throw new Exception("Unknown Account Type");
            }
        }

        public decimal sumTransactions() {
            decimal amount = 0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        public AccountType GetAccountType() 
        {
            return accountType;
        }
    }
}
