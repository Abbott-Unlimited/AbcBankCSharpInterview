using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public interface Account
    {
        void Deposit(double amount);

        void Withdraw(double amount);

        double InterestEarned();

        double SumTransactions();

        double CheckIfTransactionsExist(bool checkAll);

        Accounts.AccountTypes GetAccountType();

        List<Transaction> GetTransactions();

    }
}
