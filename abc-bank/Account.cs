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
            // It's better to replace magic numbers with constants
            const decimal CheckingsInterest = 0.001M;
            const decimal SavingsInterest = 0.002M;
            const decimal MaxiSavings = 0.05M;

            decimal amount = SumTransactions();
            switch(accountType){
                case SAVINGS:
                    if (amount <= 1000)
                        return amount * CheckingsInterest;
                    else
                        return 1 + (amount-1000) * SavingsInterest;
    //            case SUPER_SAVINGS:
    //                if (amount <= 4000)
    //                    return 20;
                case MAXI_SAVINGS:
                    var qualify = true;
                    foreach (Transaction t in this.transactions)
                    {
                        if (t.amount < 0 && t.transactionDate > System.DateTime.Now.AddDays(-10))
                        {
                            qualify = false;
                            break;
                        }
                    }
                    if (qualify == true)
                        return amount * MaxiSavings;
                    else
                        return amount * CheckingsInterest;
                default:
                    return amount * CheckingsInterest;

                
            }
        }

        public decimal SumTransactions() {
           return CheckIfTransactionsExist(true);
        }

        private decimal CheckIfTransactionsExist(bool checkAll) 
        {
            decimal amount = 0.0M;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        public int GetAccountType() 
        {
            return accountType;
        }

    }
}
