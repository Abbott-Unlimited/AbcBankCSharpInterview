using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public interface IBank
    {
        void AddCustomer(ICustomer customer);
        string GetCustomerSummary();
        double GetTotalInterestPaid();
        string GetFirstCustomerName();

    }
}
