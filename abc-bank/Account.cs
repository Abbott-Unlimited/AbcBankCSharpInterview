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

        public string accountName { get; private set; }

        public Account(int accountType, string accountName = "") 
        {
            this.accountType = accountType;
    //      Very rudimentary account naming system. Ideally, this would check for duplicate names and prevent that but that's outside the scope of this.
            if (accountName == "")
                this.accountName = this.GetAccountTypeName();
            else
                this.accountName = accountName;

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
            double amount = SumTransactions();
            switch(accountType){
                case SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001;
                    else
                        return 1 + (amount-1000) * 0.002;
    //            case SUPER_SAVINGS:
    //                if (amount <= 4000)
    //                    return 20;
                case MAXI_SAVINGS:
                    bool hasWithdrawn = false;

    //              Get the date from 10 days ago for easier comparing
                    DateTime cutoff = DateProvider.getInstance().Now().AddDays(-10);

    //              Reverse the transaction list to check the last transactions first, should be faster
                    List<Transaction> reversedTransactions = transactions;
                    reversedTransactions.Reverse();

                    foreach (Transaction t in reversedTransactions)
                    {
                        if(t.amount < 0 && DateTime.Compare(t.transactionDate, cutoff) > 0)
                        {
                            hasWithdrawn = true;
    //                      if any withdrawal is within cutoff, we can bail out. No need to check the rest
                            break;
                        }
                    }
                    if (hasWithdrawn)
                        return amount * 0.001;
                    else
                        return amount * 0.05;
                default:
    //              defaults to Checking account logic
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

        public int GetAccountType() 
        {
            return accountType;
        }

        private string GetAccountTypeName()
        {
            switch (accountType)
            {
                case CHECKING:
                    return "Checking";
                case SAVINGS:
                    return "Savings";
                default:
                    return "Maxi-Savings";
            }
                
        }

    }
}
