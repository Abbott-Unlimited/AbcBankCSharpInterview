using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class AccountOld
    {
        private readonly AccountType accountType;
        public List<Transaction> transactions;
        public double Ballance { get; private set; }

        public AccountOld(AccountType accountType) 
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
            this.Ballance = 0.0;
        }

        public void Deposit(double amount, bool transferDeposit) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                if (transferDeposit) transactions.Add(new Transaction(amount, TransactionType.TransferIn));
                else transactions.Add(new Transaction(amount, TransactionType.Deposit));
                Ballance += amount;
            }
        }

        public void Withdraw(double amount, bool transferWithdrawal) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                if (transferWithdrawal) transactions.Add(new Transaction(-amount, TransactionType.TransferOut));
                else transactions.Add(new Transaction(-amount, TransactionType.Withdrawal));
                Ballance -= amount;
            }
        }



        public double InterestEarned() 
        {
            double interest = 0.0;
            double amount = sumTransactions();
            switch(accountType){
                case AccountType.SAVINGS:
                    if (amount <= 1000)
                        interest = amount * 0.001;
                    else
                        interest = 1 + (amount-1000) * 0.002;
                    break;
    //            case SUPER_SAVINGS:
    //                if (amount <= 4000)
    //                    return 20;
                case AccountType.MAXI_SAVINGS:
                    if (amount <= 1000)
                        interest = amount * 0.02;
                    else if (amount <= 2000)
                        interest = 20 + (amount-1000) * 0.05;
                    else interest = 70 + (amount-2000) * 0.1;
                    break;
                default:
                    interest = amount * 0.001;
                    break;
            }
            //Ballance += interest;
            return interest;
        }

        //new method with changes to Maxi Saving 
        public double InterestEarnedNewMaxiSaving()
        {
            double interest = 0.0;
            double amount = sumTransactions();
            switch (accountType)
            {
                case AccountType.SAVINGS:
                    if (amount <= 1000)
                        interest = amount * 0.001;
                    else
                        interest = 1 + (amount - 1000) * 0.002;
                    break;
                //            case SUPER_SAVINGS:
                //                if (amount <= 4000)
                //                    return 20;
                case AccountType.MAXI_SAVINGS:
                    if (transactions.OrderByDescending(t => t.transactionDate).FirstOrDefault().transactionDate < DateTime.Now.AddDays(-10))
                        interest = amount * 0.05;
                    else
                        interest = amount * 0.001;
                    break;
                default:
                    interest = amount * 0.001;
                    break;
            }
            //Ballance += interest;
            return interest;
        }


        //New method. I'm making saving have compound interest assuming the no withdrawals are made
        public double CompoundInterestEarnedSaving()
        {
            double interest = 0.0;
            double amount = sumTransactions();
            switch (accountType)
            {
                case AccountType.SAVINGS:
                    DateTime lastDepositDate = this.transactions.Where(t => t.TransactionType == TransactionType.Deposit || t.TransactionType == TransactionType.TransferIn).OrderByDescending(t => t.transactionDate).FirstOrDefault().transactionDate;
                    TimeSpan timeSpan = DateTime.Now - lastDepositDate;
                    int numOfDays = timeSpan.Days;
                    interest = amount * Math.Pow((1 + 0.002 / 265), numOfDays);
                    break;

                case AccountType.MAXI_SAVINGS:
                    if (transactions.OrderByDescending(t => t.transactionDate).FirstOrDefault().transactionDate < DateTime.Now.AddDays(-10))
                        interest = amount * 0.05;
                    else
                        interest = amount * 0.001;
                    break;
                default:
                    interest = amount * 0.001;
                    break;
            }
            //Ballance += interest;
            return interest;
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

        public AccountType GetAccountType() 
        {
            return accountType;
        }


    }
}
