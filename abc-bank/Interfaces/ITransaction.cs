using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public interface ITransaction
    {
        DateTime GetTransactionDate();
        double GetAmount();
    }
}
