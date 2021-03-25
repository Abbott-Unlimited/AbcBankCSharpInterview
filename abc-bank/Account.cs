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

        /// <summary>
        /// The current balance of this account.
        /// </summary>
        public decimal Balance { get; private set; }

        /// <summary>
        /// This account's transaction history.
        /// </summary>
        public List<Transaction> transactions;

        /// <summary>
        /// Creates a new account of the specified type.
        /// </summary>
        /// <param name="accountType">The type of account to create.</param>
        public Account(AccountType accountType) 
        {
            this.Type = accountType;
            this.transactions = new List<Transaction>();
        }

        /// <summary>
        /// Processes a deposit transaction.
        /// </summary>
        /// <param name="amount">The amount to deposit.</param>
        public void Deposit(decimal amount) 
        {
            if (amount <= 0) {
                throw new ArgumentOutOfRangeException("Deposit amount must be greater than zero.");
            } 
            
            transactions.Add(new Transaction(amount));
            this.Balance += amount;
        }

        /// <summary>
        /// Processes a withdrawal transaction, if funds are available.
        /// </summary>
        /// <param name="amount">The amount to withdraw.</param>
        public void Withdraw(decimal amount) 
        {
            if (amount <= 0) {
                throw new ArgumentOutOfRangeException("Withdrawal amount must be greater than zero.");
            }

            if (amount > this.Balance) {
                throw new ArgumentOutOfRangeException("Withdrawal amount is greater than account balance.");
            }
            
            transactions.Add(new Transaction(-amount));
            this.Balance -= amount;
        }

        public decimal InterestEarned() 
        {
            switch(Type){
                case AccountType.SAVINGS:
                    if (Balance <= 1000)
                        return Balance * 0.001m;
                    else
                        return 1 + (Balance-1000) * 0.002m;
    //            case SUPER_SAVINGS:
    //                if (Balance <= 4000)
    //                    return 20;
                case AccountType.MAXI_SAVINGS:
                    if (Balance <= 1000)
                        return Balance * 0.02m;
                    if (Balance <= 2000)
                        return 20 + (Balance-1000) * 0.05m;
                    return 70 + (Balance-2000) * 0.10m;
                default:
                    return Balance * 0.001m;
            }
        }
    }
}
