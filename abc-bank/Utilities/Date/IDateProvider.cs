using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Utilities.Date
{
    /// <summary>
    /// An interface to provide the current DateTime.
    /// </summary>
    public interface IDateProvider
    {
        DateTime Now();
    }
}
