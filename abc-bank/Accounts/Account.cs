using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Accounts
{
    public abstract class Account
    {
        public List<Transaction> Transactions;
        public string AccountName;
        public AccountType Type;
        public DateTime OpenedDate { get; private set; }
        public double CurrentBalance { get { return Transactions.Sum(t => t.Amount); } }

        protected Account()
        {
            OpenedDate = DateProvider.Now;
            Transactions = new List<Transaction>();
        }

        public void Deposit(double amount)
        {
            if (amount > 0)
            {
                Transactions.Add(new Transaction(amount));
            }
            else
            {
                throw new Exception("Amount must be greater than 0");
            }
        }

        public void Withdraw(double amount)
        {
            if(amount <= 0)
            {
                throw new Exception("Amount must be greater than 0");
            }
            if (amount > CurrentBalance)
            {
                throw new Exception("Insufficient funds");
            }

            Transactions.Add(new Transaction(0 - amount));
        }

        public string GetStatement()
        {
            var sb = new StringBuilder();
            sb.AppendLine(AccountName);
            Transactions.ForEach(t => sb.AppendLine($"  {t.Type} {Math.Abs(t.Amount).ToString("C")}"));
            sb.AppendLine($"Total {CurrentBalance.ToString("C")}");

            Console.WriteLine(sb.ToString());

            return sb.ToString();
        }
        public abstract double CalculateSimpleInterest();
    }
}
