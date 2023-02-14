using abc_bank.InterestStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Accounts
{
    class MaxiSavingsAccount : Account
    {
        public List<Transaction> transactions;

        public MaxiSavingsAccount()
        {
            if(transactions == null)
            {
                transactions = new List<Transaction>();
            }
        }
        
        public double CheckIfTransactionsExist(bool checkAll)
        {
            throw new NotImplementedException();
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(amount, TransactionType.DEPOSIT, DateTime.Now));
            }
        }

        public AccountTypes GetAccountType()
        {
            return AccountTypes.MAXI_SAVINGS;
        }

        public List<Transaction> GetTransactions()
        {
            return transactions;
        }

        public double InterestEarned()
        {
            InterestContext context = new InterestContext();
            context.SetInterestStrategy(new MaxiInterest());

            return context.Calculate(transactions);


            /*
             * I removed this section due to the plus 20 and plus 70 amounts. 
             * Anti-Pattern: Magic Numbers
             * Hard coding to get the right results is bad!!!
             */
            //double amount = SumTransactions();

            //if (amount <= 1000)
            //    return amount * 0.02;
            //if (amount <= 2000)
            //    return 20 + (amount - 1000) * 0.05;
            //return 70 + (amount - 2000) * 0.1;
        }

        public double SumTransactions()
        {
            double total = 0.0;

            foreach (var t in transactions)
            {
                total += t.amount;
            }
            return AccountingLogicHelpers.ReturnCleanValue(total);
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            // keep user from putting account in negative
            if(SumTransactions() < amount)
            {
                throw new ArgumentException("not enough funds");
            }
            else
            {
                transactions.Add(new Transaction(-amount, TransactionType.WITHDRAWEL, DateTime.Now));
            }
        }
    }
}
