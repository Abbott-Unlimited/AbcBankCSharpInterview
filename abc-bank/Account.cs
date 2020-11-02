using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace abc_bank
{
    public class Account
    {
        public enum AccountType {
            CHECKING,
            SAVINGS,
            MAXI_SAVINGS
        }

        private readonly AccountType accountType;
        public List<Transaction> transactions;

        public Account(AccountType accountType) {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
        }

        public void Deposit(decimal amount) {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(decimal amount) {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(-amount));
            }
        }

        public decimal InterestEarned() {
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
                    if (WithdrawlWithinGivenDays(10)) {
                        return amount * 0.001M;
                    }

                    return amount * 0.05M;
                default:
                    throw new Exception("Invalid Account Type");
            }
        }

        public decimal sumTransactions() {
            decimal amount = 0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        public AccountType GetAccountType() {
            return accountType;
        }

        public bool WithdrawlWithinGivenDays(int days){

            DateTime oldestDate = DateTime.Now.AddDays(-days);

            if(transactions.Count() == 0) { return false; }

            for(int i = transactions.Count() -1; i > -1; i--) {
                if(transactions[i].transactionDate > oldestDate) {
                    if(transactions[i].amount < 0){
                        return true;
                    }
                } else {
                    return false;
                }
            }

            return false;
        }
    }
}
