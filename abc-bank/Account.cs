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

        public string accountNumber;

        public Account(int accountType) 
        {
            this.accountType = accountType;
            this.accountNumber = GenerateAccountNumber();
            this.transactions = new List<Transaction>();
        }

        public void Deposit(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount, Transaction.DEPOSIT));
            }
        }

        public void Withdraw(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else if (amount > this.sumTransactions()) {
                throw new ArgumentException("insufficient funds to make this withdraw");
            } else {
                transactions.Add(new Transaction(-amount, Transaction.WITHDRAW));
            }
        }

        public void Transfer(double amount, Account toAcct)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            } else if (amount > this.sumTransactions())
            {
                throw new ArgumentException("insufficient funds to make this transfer");
            }
            else if (toAcct == this)
            {
                throw new ArgumentException("cannot tranfer to the same account");
            }
            else
            {
                transactions.Add(new Transaction(-amount, Transaction.TRANSFER));
                toAcct.transactions.Add(new Transaction(amount, Transaction.TRANSFER));
            }
        }

        public double InterestEarned() 
        {
            double amount = sumTransactions();
            switch(accountType){
                case SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001;
                    else
                        return 1 + (amount-1000) * 0.002;
                case MAXI_SAVINGS:
                    bool last10 = transactions.Any(x => x.transactionDate.AddDays(10) > DateTime.Now && x.transactionType == Transaction.WITHDRAW);
                    if (last10) 
                        return amount * 0.001;
                    return amount * 0.05;
                default:
                    return amount * 0.001;
            }
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

        public int GetAccountType() 
        {
            return accountType;
        }

        private string GenerateAccountNumber()
        {
            string s = "";
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                int n = r.Next(10);
                s += n.ToString();
            }
            return s;
        }

    }
}
