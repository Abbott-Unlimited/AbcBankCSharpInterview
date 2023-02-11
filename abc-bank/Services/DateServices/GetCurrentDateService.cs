using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Services.DateServices
{
    public interface IGetCurrentDateService
    {
        DateTime Get();
    }

    public class GetCurrentDateService : IGetCurrentDateService
    {
        public DateTime Get()
        {
            return DateProvider.getInstance().Now();
        }
    }
}
