namespace ABC_bank
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Account
    {
        public const int CHECKING = 0;
        public const int SAVINGS = 1;
        public const int MAXISAVINGS = 2;

        private readonly int accountType;
        private List<Transaction> transactions;

        public Account(int accountType) 
        {
            this.accountType = accountType;
            this.Transactions = new List<Transaction>();
        }

        public List<Transaction> Transactions { get => this.transactions; set => this.transactions = value; }

        public void Deposit(double amount) 
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                this.Transactions.Add(new Transaction(amount));
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
                this.Transactions.Add(new Transaction(-amount));
            }
        }

        public double InterestEarned() 
        {
            double amount = this.SumTransactions();

            switch (this.accountType)
            {
                case SAVINGS:
                    if (amount <= 1000)
                    {
                        amount *= 0.001;
                    }
                    else
                    {
                        amount = ((amount - 1000) * 0.002) + 1;
                    }
                    
                    break;
                case MAXISAVINGS:
                    if (amount <= 1000)
                    {
                        amount *= 0.02;
                    }
                    else if (amount <= 2000)
                    {
                        amount = ((amount - 1000) * 0.05) + 20;
                    }
                    else
                    {
                        amount = ((amount - 2000) * 0.1) + 70;
                    }

                    break;
                default:
                    amount *= 0.001;
                    break;
            }

            return amount;
        }

        public double SumTransactions()
        {
           return this.CheckIfTransactionsExist();
        }

        public int GetAccountType() 
        {
            return this.accountType;
        }

        private double CheckIfTransactionsExist()
        {
            double amount = 0.0;

            foreach (Transaction t in this.Transactions)
            {
                amount += t.Amount;
            }

            return amount;
        }
    }
}
