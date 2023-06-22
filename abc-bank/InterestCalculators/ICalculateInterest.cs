using System;
using System.Collections.Generic;

namespace abc_bank.InterestCalculators
{
    public interface ICalculateInterest
    {
        double Execute(List<Transaction> transactions);
        Account.AccountType AccountType { get; }
    }
}
