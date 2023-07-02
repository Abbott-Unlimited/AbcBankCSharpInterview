using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public static class StaticAccountFunctions
    {
        public static void DoTransfer(this Account toaccount, Account fromaccount, double amount)
        {
            fromaccount.Withdraw(amount);
            toaccount.Deposit(amount);
        }
    }
}
