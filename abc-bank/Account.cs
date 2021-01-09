using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Account
    {
        public enum AccountType
        {
            CHECKING,
            SAVINGS,
            MAXI_SAVINGS
        }

        private readonly AccountType accountType;
        public List<Transaction> transactions;

        public Account(AccountType accountType) 
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
        }

        public void Deposit(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(double amount, double currentBalance) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            }
            if (amount > currentBalance) {
                throw new ArgumentException("insuffecient funds");
            } else {
                transactions.Add(new Transaction(-amount));
            }
        }

        public void TransferFunds(double amount, double accountSendingBalance, Account accountRecieving)
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            }
            if (amount > accountSendingBalance) {
                throw new ArgumentException("insuffecient funds");
            } else {
                transactions.Add(new Transaction(-amount));
                accountRecieving.Deposit(amount);
            }
        }

        public double InterestEarned() 
        {
            double amount = SumTransactions();
            switch(accountType){
                case AccountType.SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001;
                    else
                        return 1 + (amount-1000) * 0.002;
    //            case SUPER_SAVINGS:
    //                if (amount <= 4000)
    //                    return 20;
                case AccountType.MAXI_SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.02;
                    if (amount <= 2000)
                        return 20 + (amount-1000) * 0.05;
                    return 70 + (amount-2000) * 0.1;
                default:
                    return amount * 0.001;
            }
        }

        public double SumTransactions() {
           return CheckIfTransactionsExist(true);
        }

        private double CheckIfTransactionsExist(bool checkAll) 
        {
            double amount = 0.0;
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
