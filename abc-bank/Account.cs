using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Account
    {

        private readonly AccountTypeEnum _accountTypeEnum;
        private List<Transaction> _transactions;

        public Account(AccountTypeEnum accountTypeEnum) 
        {
            this._accountTypeEnum = accountTypeEnum;
            this._transactions = new List<Transaction>();
        }

        public AccountTypeEnum AccountType
        {
            get 
            { 
                return _accountTypeEnum; 
            } 
        }

        public List<Transaction> AccountTransactions
        {
            get
            {
                return _transactions;
            }
        }

        public void Deposit(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                _transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                _transactions.Add(new Transaction(-amount));
            }
        }

        public double InterestEarned()
        {
            double amount = sumTransactions();
            switch (_accountTypeEnum)
            {
                case AccountTypeEnum.SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001;
                    else
                        return 1 + (amount - 1000) * 0.002;
                //            case SUPER_SAVINGS:
                //                if (amount <= 4000)
                //                    return 20;
                case AccountTypeEnum.MAXI_SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.02;
                    if (amount <= 2000)
                        return 20 + (amount - 1000) * 0.05;
                    return 70 + (amount - 2000) * 0.1;
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
            foreach (Transaction t in _transactions)
                amount += t.TransactionAmount;
            return amount;
        }
    }
}
