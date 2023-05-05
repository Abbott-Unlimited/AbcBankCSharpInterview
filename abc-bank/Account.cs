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
        public List<Transaction> transactions;

        public Account(int accountType) 
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
        }

        public void Deposit(double amount) 
        {
            IsPositiveNumber(amount);
            transactions.Add(new Transaction(amount, Transaction.DEPOSIT));
        }

        public void Withdraw(double amount) 
        {
            IsPositiveNumber(amount);
            transactions.Add(new Transaction(-amount, Transaction.WITHDRAWAL));
        }

        public void Transfer(double amount, Account receivingAccount)
        {
            IsPositiveNumber(amount);
            //withdrawal from calling account
            transactions.Add(new Transaction(-amount, Transaction.WITHDRAWAL));
            //deposit to account receiving the transfer
            receivingAccount.transactions.Add(new Transaction(amount, Transaction.DEPOSIT));
        }

        //ADDITIONAL FEATURE
        //Interest rates should accrue daily including weekends, rates IN METHOD are per-annum (for each year).
        //I don't think this calculation is actually correct since it's just flat percentage calculations... The original calculation I think is wrong too
        //https://programcsharp.com/blog/post/calculating-compound-interest-dotnet-cs looks more accurate compared to source code
        public double InterestEarnedForOneDay() 
        {
            double amount = sumAllTransactions();
            switch(accountType){
                case SAVINGS:
                    if (amount <= 1000)
                        return (amount * 0.001)/365;
                    else
                        return (1 + (amount-1000) * 0.002)/365;
                case MAXI_SAVINGS:
                    //ADDITIONAL FEATURE
                    //Change Maxi-Savings accounts to have an interest rate of 5% assuming no withdrawals in the past 10 days otherwise 0.1%.
                    foreach (Transaction t in transactions)
                    {
                        //checks if Transaction Type is a withdrawal and within the last 10 days to give 0.1% return
                        if(t.GetTransactionType() == Transaction.WITHDRAWAL && (t.GetTransactionDate() - DateTime.Now).TotalDays <= 10)
                            return (amount * 0.01)/365;
                    }
                    return (amount * 0.05)/365;
                case CHECKING:
                    return (amount * 0.001)/365;
                default:
                    throw new NotImplementedException(accountType + " has no implementation for interest calculation.");
            }
        }

        public double sumAllTransactions() {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        public int GetAccountType() 
        {
            return accountType;
        }

        private void IsPositiveNumber(double number)
        {
            //number is expected to always be a positive number and above zero
            if (number < 0)
            {
                throw new ArgumentException("Number is negative. Positive number is required.");
            }
        }

    }
}
