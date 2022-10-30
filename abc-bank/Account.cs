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
        public readonly DateTime openingDate; //calculating APR for daily we need to add date of opening each account

        public Account(int accountType)
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
            this.openingDate = DateProvider.getInstance().Now();
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(-amount));
            }
        }

        public double InterestEarned()
        {
            double amount = sumTransactions();
            switch (accountType)
            {
                case SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001;
                    else
                        return 1 + (amount - 1000) * 0.002;
                #region oldMAXI_SAVINGS AND OTHER
                //            case SUPER_SAVINGS:
                //                if (amount <= 4000)
                //                    return 20;
                //case MAXI_SAVINGS:
                //    if (amount <= 1000)
                //        return amount * 0.02;
                //    if (amount <= 2000)
                //        return 20 + (amount-1000) * 0.05;
                //    return 70 + (amount-2000) * 0.1;
                #endregion

                case MAXI_SAVINGS:
                    bool hasWithdrawed = this.transactions.
                                         Where(t => t.amount < 0 && t.transactionDate >= DateTime.Now.AddDays(-10))
                                         .Count() > 0 ? true : false;
                    return hasWithdrawed ? amount * 0.001 : amount * 0.05;

                default:
                    return amount * 0.001;
            }
        }

        public double sumTransactions()
        {
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

    }
}
