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
        public const double TRXNGRACEPERIOD = 10;

        private readonly int accountType;
        
        public List<Transaction> transactions;

        public Account(int accountType) 
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

        public void Withdraw(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(-amount));
            }
        }

        public double InterestEarned() 
        {
            double amount = sumTransactions();
            switch(accountType){
                case SAVINGS:
                    if (amount <= 1000)
                    {
                        amount = amount * 0.001;
                    }
                    else
                    {
                        amount = 1 + (amount - 1000) * 0.002;
                    }
                    break;
                case MAXI_SAVINGS:
                    DateTime today = DateTime.Today;
                    if (GetLastTransactionDate() > today.AddDays(-TRXNGRACEPERIOD-1))
                    {
                        amount = amount * 0.001;
                    }
                    else
                    {
                        amount = amount * 0.05;
                    }
                    break;
                default:
                    amount = amount * 0.001;
                    break;
            }
            return amount;
        }

        public double sumTransactions() {
           return CheckIfTransactionsExist(true);
        }

        public int GetAccountType() 
        {
            return accountType;
        }

        private double CheckIfTransactionsExist(bool checkAll)
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        private DateTime GetLastTransactionDate()
        {
            DateTime lastDate = DateProvider.getInstance().Now();
            lastDate = lastDate.AddDays(-TRXNGRACEPERIOD - 1);
            foreach(Transaction t in transactions)
            {
                if (t.transactionDate > lastDate)
                {
                    lastDate = t.transactionDate;
                }
            }
            return lastDate;
        }

        //Actual compounding would include beginning balances and  
        //date of each current period transaction
        //private double CalcCompoundInterest(double amt, float roi, int numPeriods, int age)
        //{
        //    roi = roi / numPeriods;
        //    return amt * Math.Pow((1 + roi), age);
        //}
    }
}
