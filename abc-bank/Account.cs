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

        public void Transfer(double amount, Account destinationAccount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }

            double sourceBalance = GetAccountBalance();

            if (amount > sourceBalance)
            {
                throw new ArgumentException("amount must be greater than zero");
            }

            LockTransactions();

            if (Withdraw(amount))
                destinationAccount.Deposit(amount);

            ReleaseTransactions();
        }

        private void ReleaseTransactions()
        {
            throw new NotImplementedException();
        }

        private void LockTransactions()
        {
            throw new NotImplementedException();
        }

        private double GetAccountBalance()
        {
            throw new NotImplementedException();
        }

        public Boolean Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(amount,"D"));
                return true;
            }
        }

        public Boolean Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(-amount,"W"));
                return true;
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
                //            case SUPER_SAVINGS:
                //                if (amount <= 4000)
                //                    return 20;
                case MAXI_SAVINGS:

                    if (transactions.Count > 0)
                    {
                        double interestEarned = 0;

                        foreach (Transaction t in transactions)
                        {
                            interestEarned += amount * .001;
                        }

                        return interestEarned;
                    }
                    else
                    {
                        return amount * .05;
                    }

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
