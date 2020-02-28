using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public interface ICustomer
    {
        string GetName();
        IList<IAccount> GetAccounts();
        void OpenAccount(IAccount account);
        int GetNumberOfAccounts();
        double GetTotalInterestEarned();
        string GetStatement();
    }
}
