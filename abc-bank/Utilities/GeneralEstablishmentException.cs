using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcCompanyEstablishmentApp.Utilities
{
    [Serializable]
    public class GeneralEstablishmentException : Exception
    {
        public GeneralEstablishmentException() : base() { }
        public GeneralEstablishmentException(string message) : base(message) { }
        public GeneralEstablishmentException(string message, Exception inner) : base(message, inner) { }
    }

    [Serializable]
    public class AmountLessThanZeroException : Exception
    {
        public AmountLessThanZeroException() : base() { }
        public AmountLessThanZeroException(string message) : base(message) { }
        public AmountLessThanZeroException(string message, Exception inner) : base(message, inner) { }
    }

    [Serializable]
    public class AccountAmountTooLow : Exception
    {
        public AccountAmountTooLow() : base() { }
        public AccountAmountTooLow(string message) : base(message) { }
        public AccountAmountTooLow(string message, Exception inner) : base(message, inner) { }
    }

}
