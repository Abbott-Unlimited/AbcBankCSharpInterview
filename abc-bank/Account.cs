using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Account
    {
        /// <summary>
        /// The type of this account.
        /// </summary>
        public AccountType Type { get; private set; }
        public List<Transaction> transactions;

        public Account(AccountType accountType) 
        {
            this.Type = accountType;
            this.transactions = new List<Transaction>();
        }

        public void Deposit(decimal amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(decimal amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(-amount));
            }
        }

        public decimal InterestEarned() 
        {
            decimal amount = sumTransactions();
            switch(Type){
                case AccountType.SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001m;
                    else
                        return 1 + (amount-1000) * 0.002m;
    //            case SUPER_SAVINGS:
    //                if (amount <= 4000)
    //                    return 20;
                case AccountType.MAXI_SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.02m;
                    if (amount <= 2000)
                        return 20 + (amount-1000) * 0.05m;
                    return 70 + (amount-2000) * 0.10m;
                default:
                    return amount * 0.001m;
            }
        }

        /// <summary>
        /// Calculates the sum of all transactions on this account (the current balance).
        /// </summary>
        /// <returns>The sum of all transaction amounts.</returns>
        public decimal sumTransactions() {
            decimal amount = 0.00m;
            foreach (Transaction t in transactions)
                amount += t.Amount;
            return amount;
        }
    }
}
