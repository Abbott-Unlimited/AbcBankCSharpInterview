using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Account
    {
        //TO-DO: Implement GUID system for account IDs

        private readonly AccountType _accountType;
        public List<Transaction> Transactions;

        private const decimal SAVINGSRATE1 = 0.001m;
        private const decimal SAVINGSRATE2 = 0.002m;
        private const decimal MAXI_SAVINGS1 = 0.001m;
        private const decimal MAXI_SAVINGS2 = 0.05m;
        private const decimal CHECKING1 = 0.001m;

        public Account(AccountType accountType) 
        {
            this._accountType = accountType;
            this.Transactions = new List<Transaction>();
        }

        public Result Deposit(decimal amount, DateTime? date = null) 
        {
            if (amount <= 0)
                return new FailureResult
                {
                    Message = "amount must be greater than zero",
                };

            Transactions.Add(new Transaction(amount, date));

            return new SuccessResult();
        }

        public Result Withdraw(decimal amount, DateTime? date = null) 
        {
            if (amount <= 0)
                return new FailureResult
                {
                    Message = "amount must be greater than zero",
                };

            if(amount > SumTransactions())
                return new FailureResult
                {
                    Message = "withdrawal greater then balance",
                };

            Transactions.Add(new Transaction(-amount, date));

            return new SuccessResult();
        }

        public decimal InterestEarned() 
        {
            decimal amount = SumTransactions();
            switch(_accountType){
                case AccountType.SAVINGS:
                    if (amount <= 1000)
                        return amount * (SAVINGSRATE1 / 365);
                    else
                        return 1 + ((amount-1000) * (SAVINGSRATE2 / 365));
                case AccountType.MAXI_SAVINGS:
                    if (TransactionInLastTenDays())
                        return amount * (MAXI_SAVINGS1 / 365);   
                    else
                        return amount * (MAXI_SAVINGS2 / 365);
                default:
                    return amount * (CHECKING1 / 365);
            }
        }

        public decimal SumTransactions() {
            decimal amount = 0.0M;
            foreach (Transaction t in Transactions)
                amount += t.GetAmount();
            return amount;
        }

        public AccountType GetAccountType() 
        {
            return _accountType;
        }

        //Checks for transactions within the last ten days.        
        private bool TransactionInLastTenDays()
        {
            foreach (Transaction t in Transactions)
            {
                if(t.GetDate() >= DateProvider.GetInstance().Now().AddDays(-10) && t.GetDate() <= DateProvider.GetInstance().Now())
                {
                    return true;
                }
            }
            return false;
        }

    }
}
